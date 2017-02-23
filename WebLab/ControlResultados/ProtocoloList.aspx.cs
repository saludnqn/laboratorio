using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Business;
using Business.Data.Laboratorio;
using System.Data.SqlClient;

namespace WebLab.ControlResultados
{
    public partial class ProtocoloList : System.Web.UI.Page
    {
      

       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                VerificaPermisos("Calcular Formulas");        
                CargarGrilla();
                MarcarSeleccionados(false);
            }
        }

        private void VerificaPermisos(string sObjeto)
        {
            if (Session["s_permiso"] != null)
            {
                Utility oUtil = new Utility();
                int i_permiso = oUtil.VerificaPermisos((ArrayList)Session["s_permiso"], sObjeto);
                switch (i_permiso)
                {
                    case 0: Response.Redirect("../AccesoDenegado.aspx", false); break;
                    //case 1:
                    //    lnkMarcarImpresos.Visible = false; break;
                }
            }
            else Response.Redirect("../FinSesion.aspx", false);
        }

        private void MarcarSeleccionados(bool p)
        {           
            foreach (GridViewRow row in gvLista.Rows )
            {
                CheckBox a = ((CheckBox)(row.Cells[0].FindControl("CheckBox1")));         
                if (a.Checked==!p)                
                    ((CheckBox)(row.Cells[0].FindControl("CheckBox1"))).Checked = p;                
            }
            
        }

        
        private void CargarGrilla()
        {
            gvLista.DataSource = LeerDatos();
            gvLista.DataBind();
            PonerImagenes();
        }

        private object LeerDatos()
        {
            /*Filtra los protocolos en proceso: por que debe tener valores cargados para aplicar las formulas*/
            /*  Filtra los protocolos con analisis con formulas a calcular sin resultados*/
            

            string m_strSQL = " SELECT DISTINCT  P.idProtocolo, " +
                              " dbo.NumeroProtocolo(P.idProtocolo)  as numero," +
                              " CONVERT(varchar(10),P.fecha,103) as fecha, Pa.numeroDocumento as dni,Pa.apellido+ ' ' + Pa.nombre as paciente," +
                              " O.nombre as origen, Pri.nombre as prioridad, SS.nombre as sector,P.estado, P.impreso " +
                              " FROM Lab_Protocolo P" +
                              " INNER JOIN Lab_Origen O on O.idOrigen= P.idOrigen" +
                              " INNER JOIN Lab_Prioridad Pri on Pri.idPrioridad= P.idPrioridad" +
                              " INNER JOIN Sys_Paciente Pa on Pa.idPaciente= P.idPaciente " +
                              
                                 " INNER JOIN LAB_SectorServicio SS  ON P.idSector= SS.idSectorServicio " +
                                 "  INNER JOIN    LAB_DetalleProtocolo AS DP ON P.idProtocolo = DP.idProtocolo" +
                                 " INNER JOIN           LAB_Item AS I ON DP.idSubItem = I.idItem " +
                                 " INNER JOIN  LAB_Formula AS F ON I.idItem = F.idItem"+

                              " WHERE  (F.idTipoFormula = 1) AND (P.estado =1) AND (DP.conResultado = 0) AND " + Request["Parametros"].ToString(); // +str_orden;
                              
            //" INNER JOIN Lab_Configuracion Con ON Con.idEfector= P.idEfector " +
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);

            CantidadRegistros.Text = Ds.Tables[0].Rows.Count.ToString() + " protocolos con fórmulas pendientes de calcular ";
             
            return Ds.Tables[0];
        }

     

       

        private string GenerarListaProtocolos()
        {
            string m_lista = "";
            foreach (GridViewRow row in gvLista.Rows)
            {

                CheckBox a = ((CheckBox)(row.Cells[0].FindControl("CheckBox1")));
                if (a.Checked == true)
                {
                    if (m_lista == "")
                        m_lista += gvLista.DataKeys[row.RowIndex].Value.ToString();
                    else
                        m_lista += "," + gvLista.DataKeys[row.RowIndex].Value.ToString();
                }
            }
            return m_lista;
        }

        

        protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
        {                                   
        }

        private void PonerImagenes()
        {
            foreach (GridViewRow row in gvLista.Rows)
            {                
                    switch (row.Cells[9].Text)
                    {
                        case "0": ///Abierto
                            {
                                Image hlnk = new Image();
                                hlnk.ImageUrl = "~/App_Themes/default/images/rojo.gif";
                                row.Cells[9].Controls.Add(hlnk);
                            }
                            break;
                        case "1": //en proceso
                            {
                                Image hlnk = new Image();
                                hlnk.ImageUrl = "~/App_Themes/default/images/amarillo.gif";
                               row.Cells[9].Controls.Add(hlnk);
                            }
                            break;
                        case "2": //terminado
                            {
                                Image hlnk = new Image();
                                hlnk.ImageUrl = "~/App_Themes/default/images/verde.gif";
                                row.Cells[9].Controls.Add(hlnk);
                            }
                            break;
                    }

                     

            }
        }



      

        protected void lnkMarcar_Click(object sender, EventArgs e)
        {
            MarcarSeleccionados(true);
            PonerImagenes();
        }

        protected void lnkDesmarcar_Click(object sender, EventArgs e)
        {
            MarcarSeleccionados(false);
            PonerImagenes();
        }

       

        protected void lnkRegresar_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("ControlPlanilla.aspx?tipo=formula",false);            

        }

        protected void btnCalcularFormula_Click(object sender, EventArgs e)
        {
            int i_idUsuario = int.Parse(Session["idUsuario"].ToString());
            foreach (GridViewRow row in gvLista.Rows)
            {

                CheckBox a = ((CheckBox)(row.Cells[0].FindControl("CheckBox1")));
                if (a.Checked == true)
                {
                    Protocolo oProtocolo = new Protocolo();
                    oProtocolo= (Protocolo) oProtocolo.Get(typeof(Protocolo),int.Parse( gvLista.DataKeys[row.RowIndex].Value.ToString()));
                    oProtocolo.CalcularFormulas("Carga",i_idUsuario,true);
                }
            }
            string popupScript = "<script language='JavaScript'> alert('Las fórmulas se calcularon correctamente. Si alguno de los protocolos no fue cargado con todos los analisis necesarios para el calculo de formula, los analisis con formula no se calcularan. Verifique sus datos.')</script>";
            Page.RegisterClientScriptBlock("PopupScript", popupScript);
            CargarGrilla();

        }
    }
   
}
