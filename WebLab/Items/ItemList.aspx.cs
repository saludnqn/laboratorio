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
using System.Data.SqlClient;
using Business.Data.Laboratorio;
using Business;
using System.Drawing;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using Business.Data;
using CrystalDecisions.Web;
//using System.Web.UI.WebControls;

namespace WebLab.Items
{
    public partial class ItemList : System.Web.UI.Page
    {
        public CrystalReportSource oCr = new CrystalReportSource();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            oCr.Report.FileName = "";
            oCr.CacheDuration = 0;
            oCr.EnableCaching = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    VerificaPermisos("Analisis");
                    CargarListas();

                    if (Request["Codigo"] != null) txtCodigo.Text = Request["Codigo"].ToString();
                    if (Request["Nombre"] != null) txtNombre.Text = Request["Nombre"].ToString();
                    if (Request["Servicio"] != null) ddlServicio.SelectedValue = Request["Servicio"].ToString();
                    if (Request["Area"] != null) ddlArea.SelectedValue = Request["Area"].ToString();
                    if (Request["Orden"] != null) ddlTipo.SelectedValue = Request["Orden"].ToString();


                    CargarGrilla();
                }
                else Response.Redirect("../FinSesion.aspx", false);
            }
        }

        private int Permiso /*el permiso */
        {
            get { return ViewState["Permiso"] == null ? 0 : int.Parse(ViewState["Permiso"].ToString()); }
            set { ViewState["Permiso"] = value; }
        }

        private void VerificaPermisos(string sObjeto)
        {
            if (Session["s_permiso"] != null)
            {
                Utility oUtil = new Utility();
                Permiso = oUtil.VerificaPermisos((ArrayList)Session["s_permiso"], sObjeto);
                switch (Permiso)
                {
                    case 0: Response.Redirect("../AccesoDenegado.aspx", false); break;
                    case 1: btnNuevo.Visible = false; break;
                }
            }
            else
                Response.Redirect("../FinSesion.aspx", false);

        }

        private void CargarGrilla()
        {           
          
            gvLista.DataSource = LeerDatos("Extendido");
            gvLista.DataBind();
            PonerImagenes();
           
        }


        private void PonerImagenes()
        {
            foreach (GridViewRow row in gvLista.Rows)
            {
                
                switch (row.Cells[5].Text)
                {
                    case "False":
                        {
                            System.Web.UI.WebControls.Image hlnk = new System.Web.UI.WebControls.Image();
                            hlnk.ImageUrl = "~/App_Themes/default/images/noDisponible.gif";
                            hlnk.ToolTip = "No disponible";
                            row.Cells[5].Controls.Add(hlnk);
                        }
                        break;
                    case "True":
                        {
                            System.Web.UI.WebControls.Image hlnk = new System.Web.UI.WebControls.Image();
                            hlnk.ImageUrl = "~/App_Themes/default/images/transparente.jpg";
                            row.Cells[5].Controls.Add(hlnk);
                        }
                        break;

                }

            }
        }

        private object LeerDatos(string tipo)
        {
            string m_condicion = "";
            if (txtCodigo.Text != "")  m_condicion += " and I.codigo like '%" + txtCodigo.Text + "%'";
            if (txtNombre.Text != "") m_condicion += " and I.nombre like '%" + txtNombre.Text + "%'";
            if (ddlArea.SelectedValue != "0") m_condicion += " and I.idArea=" + ddlArea.SelectedValue;
            if (ddlServicio.SelectedValue != "0") m_condicion += " and A.idTipoServicio='" + ddlServicio.SelectedValue + "'";

             string m_strSQL ="";
            string m_orden = "";
            if (ddlTipo.SelectedValue == "0") m_orden = " ORDER BY i.CODIGO";
            if (ddlTipo.SelectedValue == "1") m_orden = " ORDER BY i.NOMBRE";
            if (ddlTipo.SelectedValue == "2") m_orden = " ORDER BY A.NOMBRE";

            if (tipo=="Extendido")
             m_strSQL = " select I.idItem,I.codigo, I.nombre as nombre, " +
                "case when  I.idCategoria='0' then 'Simple' else 'Compuesta' end  as tipo, I.codigoNomenclador ,   CASE WHEN A.baja = 1 THEN '' ELSE A.nombre END AS area," +
                " I.disponible "+
                              " from Lab_Item I" +
                              " left join Lab_Area A on A.idArea= I.idArea" +
                              " where I.baja=0  " + m_condicion + m_orden;

              //m_strSQL= " SELECT Codigo, Descrip AS nombre FROM LAB_Nomenclador ORDER BY nombre" ;
            else
                 m_strSQL = " SELECT I.idItem, I.codigo, I.nombre, CASE WHEN I.idCategoria = '0' THEN 'Simple' ELSE 'Compuesta' END AS tipo, A.nombre AS area, UM.nombre AS umedida, "+
                            " I.requiereMuestra, I2.codigo AS referencia, CASE WHEN con.idefector <> I.idefectorderivacion THEN 'Si' ELSE 'No' END AS derivacion "  +
                            " FROM         LAB_Item AS I " +
                            " LEFT OUTER JOIN     LAB_Item AS I2 ON I.idItemReferencia = I2.idItem " +
                            " LEFT OUTER JOIN    LAB_UnidadMedida AS UM ON I.idUnidadMedida = UM.idUnidadMedida " +
                            " INNER JOIN      LAB_Area AS A ON A.idArea = I.idArea " +
                            " INNER JOIN         LAB_Configuracion AS Con ON I.idEfector = Con.idEfector " +
                            " where I.baja=0 and A.baja=0 " + m_condicion + m_orden;

                             
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);

            // 
            // 
             
             
            return Ds.Tables[0];
        }



        private object LeerDatosNomenclador()
        {
           
            string m_strSQL =  " SELECT Codigo, Descrip AS nombre FROM LAB_Nomenclador ORDER BY nombre" ;            

            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);



            return Ds.Tables[0];
        }


        private void CargarListas()
        {

            Utility oUtil = new Utility();
            ///Carga de combos de servicios
            ///
            string m_ssql = "select idTipoServicio, nombre from Lab_TipoServicio WHERE (baja = 0)";
            oUtil.CargarCombo(ddlServicio, m_ssql, "idTipoServicio", "nombre");
            ddlServicio.Items.Insert(0, new ListItem("Todos", "0"));
            CargarArea();
                     
                      

            m_ssql = null;
            oUtil = null;
        }

        private void CargarArea()
        {
            Utility oUtil = new Utility();
            string m_ssql="";
            if (ddlServicio.SelectedValue !="0")
                m_ssql = "select idArea, nombre from Lab_Area where baja=0  and idTipoServicio=" + ddlServicio.SelectedValue + " order by nombre";
            else
                m_ssql = "select idArea, nombre from Lab_Area where baja=0  order by nombre";
            oUtil.CargarCombo(ddlArea, m_ssql, "idArea", "nombre");
            ddlArea.Items.Insert(0, new ListItem("Todas", "0"));
            ddlArea.UpdateAfterCallBack = true;     
            
        }

      

        protected void gvLista_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

            string m_parametroFiltro= "&Codigo=" +  txtCodigo.Text + "&Nombre=" + txtNombre.Text + "&Servicio=" + ddlServicio.SelectedValue +
            "&Area="+ ddlArea.SelectedValue +"&Orden=" + ddlTipo.SelectedValue;

            if (e.CommandName != "Page")
            {
                switch (e.CommandName)
                {
                    case "Modificar":
                        Response.Redirect("ItemEdit2.aspx?id=" + e.CommandArgument+ m_parametroFiltro);
                        break;
                    //case "VR":
                    //    Response.Redirect("ItemValorReferencia.aspx?id=" + e.CommandArgument);
                    //    break;
                    //case "Diagramacion":
                    //    Response.Redirect("ItemDiagramacion.aspx?id=" + e.CommandArgument);
                    //    break;
                    //case "Resultado":
                    //    Response.Redirect("ItemResultado2.aspx?id=" + e.CommandArgument);
                    //    break;
                    //case "Recomendacion":
                    //    Response.Redirect("ItemRecomendaciones.aspx?id=" + e.CommandArgument);
                    //    break;
                    case "Eliminar":
                        Eliminar(e.CommandArgument);
                        CargarGrilla();
                        break;
                }
            }
        }

        private void Eliminar(object idItem)
        { 
            Usuario oUser = new Usuario();
            Item oRegistro = new Item();
            oRegistro = (Item)oRegistro.Get(typeof(Item), int.Parse(idItem.ToString()));
           
            oRegistro.Baja = true;
           oRegistro.IdUsuarioRegistro = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
            oRegistro.FechaRegistro = DateTime.Now;

            oRegistro.Save();
        }

       

        protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                


                ImageButton CmdModificar = (ImageButton)e.Row.Cells[6].Controls[1];
                CmdModificar.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdModificar.CommandName = "Modificar";
                CmdModificar.ToolTip = "Modificar";

           

                ImageButton CmdEliminar = (ImageButton)e.Row.Cells[7].Controls[1];
                CmdEliminar.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdEliminar.CommandName = "Eliminar";
                CmdEliminar.ToolTip = "Eliminar";

                if (Permiso == 1)
                {
                    CmdEliminar.Visible = false;
                    CmdModificar.ToolTip = "Consultar";
                }

             

           
            }  

        }

       

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            gvLista.PageIndex = 0;
            CargarGrilla();            
            CurrentPageLabel.Text = " ";
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvLista_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
           
            
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;

            int currentPage = gvLista.PageIndex+1;


            CurrentPageLabel.Text = "Página " + currentPage.ToString() +
              " de " + gvLista.PageCount.ToString();

            CargarGrilla();

           
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            
        }

        private void MostrarInforme(string tipo)
        {

           
         

            
            if (tipo == "Nomenclador")
            {
                oCr.Report.FileName =  "../Informes/ListaItem.rpt";
                oCr.ReportDocument.SetDataSource(LeerDatosNomenclador());
            }
            else
            {
                oCr.Report.FileName = "../Informes/ListaItemReducido.rpt";
                oCr.ReportDocument.SetDataSource(LeerDatos(tipo));
            }


            
   
            //this.CrystalReportSource1.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
            //this.CrystalReportSource1.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
            //this.CrystalReportSource1.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
            oCr.DataBind();

            //if (rdbImprimir.Items[0].Selected == true)//imprimir
            //    this.CrystalReportSource1.ReportDocument.PrintToPrinter(1, true, 1, 100);
            //else
            //{
            oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Listado.pdf");
            //MemoryStream oStream; // using System.IO
            //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment;filename=Listado.pdf");

            //Response.BinaryWrite(oStream.ToArray());
            //Response.End();
            // }

        }

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            MostrarInforme("Nomenclador");
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {   gvLista.PageIndex = 0;
            CargarGrilla();
         
            CurrentPageLabel.Text = " ";
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        { gvLista.PageIndex = 0;
            CargarGrilla();
           
            CurrentPageLabel.Text = " ";
        }

        protected void lnkPdfReducido_Click(object sender, EventArgs e)
        {
            MostrarInforme("Reducido");
        }

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarArea();
            
            gvLista.PageIndex = 0;
            CargarGrilla();
            
            CurrentPageLabel.Text = " ";
        }

        protected void ddlArea_SelectedIndexChanged1(object sender, EventArgs e)
        { gvLista.PageIndex = 0;
            CargarGrilla();
           
            CurrentPageLabel.Text = " ";
        }

        protected void gvLista_DataBound(object sender, EventArgs e)
        {

          

            
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
              string m_parametroFiltro= "?Codigo=" +  txtCodigo.Text + "&Nombre=" + txtNombre.Text + "&Servicio=" + ddlServicio.SelectedValue +
            "&Area="+ ddlArea.SelectedValue +"&Orden=" + ddlTipo.SelectedValue;

          
                        Response.Redirect("ItemEdit2.aspx"+ m_parametroFiltro,false);
        }
    }
}
