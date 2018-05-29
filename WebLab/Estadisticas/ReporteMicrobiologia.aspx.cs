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
using CrystalDecisions.Web;
using System.Data.SqlClient;
using Business;
using System.Text;
using System.IO;
using Business.Data;
using CrystalDecisions.Shared;
using Business.Data.Laboratorio;
using InfoSoftGlobal;

namespace WebLab.Estadisticas
{
    public partial class ReporteMicrobiologia : System.Web.UI.Page
    {
        public CrystalReportSource oCr = new CrystalReportSource();
        int suma1 = 0;
        int grupo1 = 0; int grupo2 = 0; int grupo3 = 0; int grupo4=0; int grupo5=0; int grupo6=0; int grupo7=0; int grupo8=0; int grupo9=0; int grupo10=0;
        int masc = 0; int fem = 0; int ind = 0; int emb = 0;


        private enum TabIndex
        {
            DEFAULT = 0,
            ONE = 1,
            TWO = 2,
            THREE = 3,
            CUARTO = 4,
            QUINTO = 5
            // you can as many as you want here
        }
        private void SetSelectedTab(TabIndex tabIndex)
        {
            HFCurrTabIndex.Value = ((int)tabIndex).ToString();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            oCr.CacheDuration = 0;
            oCr.EnableCaching = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                VerificaPermisos("De Microbiologia");
                CargarListas();
                txtFechaDesde.Value = DateTime.Now.AddDays(-7).ToShortDateString();
                txtFechaHasta.Value = DateTime.Now.ToShortDateString();
            }
        }

        private void VerificaPermisos(string sObjeto)
        {
            if (Session["idUsuario"] != null)
            {
                if (Session["s_permiso"] != null)
                {
                    Utility oUtil = new Utility();
                    int i_permiso = oUtil.VerificaPermisos((ArrayList)Session["s_permiso"], sObjeto);
                    switch (i_permiso)
                    {
                        case 0: Response.Redirect("../AccesoDenegado.aspx", false); break;
                        case 1: Response.Redirect("../AccesoDenegado.aspx", false); break;
                    }
                }
                else Response.Redirect("../FinSesion.aspx", false);
            }
            else Response.Redirect("../FinSesion.aspx", false);
        }

        private string getListaOrigen()
        {

            string lista = "";
            for (int i = 0; i < this.ChckOrigen.Items.Count; i++)
            {
                if (ChckOrigen.Items[i].Selected)
                {
                    if (lista == "")
                        lista = ChckOrigen.Items[i].Value;
                    else
                        lista += "," + ChckOrigen.Items[i].Value;
                }

            }
            return lista;
        }
        private void CargarListas()
        {
            Utility oUtil = new Utility();
            ///Carga de combos de tipos de servicios
            string m_ssql = "SELECT     I.idItem, I.nombre + ' (' + I.codigo + ')' AS nombre " +
            " FROM         LAB_Item as I" +
            " INNER JOIN LAB_Area  as A ON A.idArea= I.idArea  " +
            " WHERE   I.disponible=1 AND (I.idEfectorDerivacion = I.idEfector) AND (I.baja = 0) and A.idTipoServicio=3 AND (tipo = 'P')  " +
            " ORDER BY I.nombre";

            oUtil.CargarCombo(ddlAnalisis, m_ssql, "idItem", "nombre");
            ddlAnalisis.Items.Insert(0, new ListItem("--Seleccione--", "0"));


             m_ssql = "SELECT     idOrigen, nombre  AS nombre FROM         LAB_Origen  ORDER BY nombre";
            oUtil.CargarCheckBox(ChckOrigen, m_ssql, "idOrigen", "nombre");
            for (int i = 0; i < ChckOrigen.Items.Count; i++)
                ChckOrigen.Items[i].Selected = true;
            m_ssql = null;
            oUtil = null;
        }

        private void MostrarReporteGeneral()
        {
            DataTable dtTipoMuestra = MostrarDatos("Tipo de Muestra");
            DataTable dtMicroorganismo = MostrarDatos("Aislamiento");
            DataTable dtResultado = MostrarDatos("Resultado");
            
            gvTipoMuestra.DataSource =dtTipoMuestra;
            gvTipoMuestra.DataBind();

            HFTipoMuestra.Value = getValoresTipoMuestra();

            //////////////////////Solapa microorganismos
            ddlTipoMuestra.DataTextField = "Tipo Muestra";
            ddlTipoMuestra.DataValueField = "idMuestra";

            ddlTipoMuestra.DataSource = dtTipoMuestra;
            ddlTipoMuestra.DataBind();
            ddlTipoMuestra.Items.Insert(0, new ListItem("--Todas--", "0"));

            gvMicroorganismos.DataSource = dtMicroorganismo;
            gvMicroorganismos.DataBind();

            HFMicroorganismo.Value = getValoresMicroorganismos();
            gvMicroorganismos.Visible = true;
            lblFiltroMicroorganismo.Text = "Tipo de Muestra: " + ddlTipoMuestra.SelectedItem.Text + " - ATB: " + ddlATB.SelectedValue;

        //    gvMicroorganismos.UpdateAfterCallBack = true;
        //    lblFiltroMicroorganismo.UpdateAfterCallBack = true;

            
            
                ddlTipoMuestraAntibioticos.DataTextField = "Tipo Muestra";
                ddlTipoMuestraAntibioticos.DataValueField = "idMuestra";
                ddlTipoMuestraAntibioticos.DataSource =dtTipoMuestra;
                ddlTipoMuestraAntibioticos.DataBind();
                ddlTipoMuestraAntibioticos.Items.Insert(0, new ListItem("--Todas--", "0"));

                ddlMicroorganismosAntibioticos.DataTextField = "Microorganismo";
                ddlMicroorganismosAntibioticos.DataValueField = "idGermen";
                ddlMicroorganismosAntibioticos.DataSource =dtMicroorganismo;
                ddlMicroorganismosAntibioticos.DataBind();
                ddlMicroorganismosAntibioticos.Items.Insert(0, new ListItem("--Todos--", "0"));

            

            //gvAntibiotico.DataSource = Ds.Tables[2];
            //gvAntibiotico.DataBind();

            //lblFiltroAntibiotico.Text = "Tipo de Muestra: " + ddlTipoMuestraAntibioticos.SelectedItem.Text + " - Aislamiento: " + ddlMicroorganismosAntibioticos.SelectedItem.Text;
            //lblFiltroAntibiotico.UpdateAfterCallBack = true;


            ///////////////////
            gvResultado.DataSource = dtResultado;
            gvResultado.DataBind();


            gvMicroorganismosATB.Visible = false;
            gvAntibioticoResistencia.Visible = false;

            //FCLiteralMicroorganismo.Text = "";
            //FCLiteralTipoMuestra.Text = "";
            //gvAntibioticoResistencia.DataSource = Ds.Tables[4];
            //gvAntibioticoResistencia.DataBind();

            //if (tipoAntibiotico > 0)
            //{
            //    Antibiotico oAnti = new Antibiotico();
            //    oAnti = (Antibiotico)oAnti.Get(typeof(Antibiotico), tipoAntibiotico);

            //    string seleccion = "Tipo de Muestra:" + ddlTipoMuestraAntibioticos.SelectedItem.Text + " - " + " Microorganismo: " + ddlMicroorganismosAntibioticos.SelectedItem.Text;
            //    lblResistenciaAntibiotico.Text = seleccion + " - Resistencia de " + oAnti.Descripcion;
            //    lblResistenciaAntibiotico.UpdateAfterCallBack = true;

            //}



        if (gvTipoMuestra.Rows.Count == 0) Response.Redirect("SinDatos.aspx?Desde=ReporteMicrobiologia.aspx", false);
            else
            {
                lblAnalisis.Text = ddlAnalisis.SelectedItem.Text;
                pnlResultado.Visible = true;
                SetSelectedTab(TabIndex.ONE);
            }
        }

        private string getValoresMicroorganismos()
        {
            string s_valores = "";

            for (int i = 0; i < gvMicroorganismos.Rows.Count; i++)
            {
                string s_nombre = gvMicroorganismos.Rows[i].Cells[0].Text.Replace(";", "");
                s_nombre = s_nombre.Replace("&#", "");
                    
                if (s_valores == "")

                    s_valores = "name='" + s_nombre + "' value='" + gvMicroorganismos.Rows[i].Cells[1].Text + "'";
                else
                    s_valores += ";" + "name='" + s_nombre + "' value='" + gvMicroorganismos.Rows[i].Cells[1].Text + "'";
            }
            
            return s_valores;
        }

        private string getValoresTipoMuestra()
        {
            string s_valores = "";
           
                for (int i = 0; i < gvTipoMuestra.Rows.Count; i++)
                {
                    string s_nombre = gvTipoMuestra.Rows[i].Cells[0].Text.Replace(";", "");
                    s_nombre = s_nombre.Replace("&#", "");
                    if (s_valores=="")
                        s_valores = "name='" + s_nombre + "' value='" + gvTipoMuestra.Rows[i].Cells[1].Text + "'";
                    else
                        s_valores += ";" + "name='" + s_nombre + "' value='" + gvTipoMuestra.Rows[i].Cells[1].Text + "'";
                }
                
            return  s_valores;
        }




        protected void lnkPdf_Click(object sender, EventArgs e)
        {

        }




          private DataTable MostrarDatos(string s_tipo)
        {
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "[LAB_EstadisticaMicrobiologia]";

            int tipoM = 0;
            int conATB=0;
            if (s_tipo=="Aislamiento")            
            {
                if (ddlTipoMuestra.SelectedValue != "") tipoM= int.Parse(ddlTipoMuestra.SelectedValue);
                if (ddlATB.SelectedValue != "Todos")
                {
                    if (ddlATB.SelectedValue == "Con ATB") conATB = 1;  ///si 
                    else conATB = 2;//no
                }
            }
            if (s_tipo == "Antibiotico")
            { if (ddlTipoMuestraAntibioticos.SelectedValue != "") tipoM = int.Parse(ddlTipoMuestraAntibioticos.SelectedValue); }

            int tipoGermen = 0;
            if (s_tipo == "Antibiotico") { if (ddlMicroorganismosAntibioticos.SelectedValue != "") tipoGermen = int.Parse(ddlMicroorganismosAntibioticos.SelectedValue); }

            
            int tipoAntibiotico = 0;
            if (s_tipo == "Resistencia") {
                if (ddlTipoMuestraAntibioticos.SelectedValue != "") tipoM = int.Parse(ddlTipoMuestraAntibioticos.SelectedValue); 
                if (ddlMicroorganismosAntibioticos.SelectedValue != "") tipoGermen = int.Parse(ddlMicroorganismosAntibioticos.SelectedValue); 
                if (hdfidAntibiotico.Value != "") tipoAntibiotico = int.Parse(hdfidAntibiotico.Value); }
            
            
            int idsubitem=0;

            //if (s_tipo == "Parametro")
            //{

            //    if (ddlSubItem.SelectedValue != "") idsubitem = int.Parse(ddlSubItem.SelectedValue);
            //}
            ///Parametros de fechas           
            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            cmd.Parameters.Add("@fechaDesde", SqlDbType.NVarChar);
            cmd.Parameters["@fechaDesde"].Value = fecha1.ToString("yyyyMMdd");
            cmd.Parameters.Add("@fechaHasta", SqlDbType.NVarChar);
            cmd.Parameters["@fechaHasta"].Value = fecha2.ToString("yyyyMMdd");
            ///////


            cmd.Parameters.Add("@idAnalisis", SqlDbType.Int);
            cmd.Parameters["@idAnalisis"].Value =int.Parse( ddlAnalisis.SelectedValue);

            cmd.Parameters.Add("@idOrigen", SqlDbType.NVarChar);
            cmd.Parameters["@idOrigen"].Value =getListaOrigen();

            cmd.Parameters.Add("@idTipoMuestra", SqlDbType.Int);
            cmd.Parameters["@idTipoMuestra"].Value = tipoM;


            cmd.Parameters.Add("@idGermen", SqlDbType.Int);
            cmd.Parameters["@idGermen"].Value = tipoGermen;

            cmd.Parameters.Add("@idAntibiotico", SqlDbType.Int);
            cmd.Parameters["@idAntibiotico"].Value = tipoAntibiotico;

            cmd.Parameters.Add("@idsubitem", SqlDbType.Int);
            cmd.Parameters["@idsubitem"].Value = idsubitem;
            
            
            cmd.Parameters.Add("@tipoReporte", SqlDbType.NVarChar);
            cmd.Parameters["@tipoReporte"].Value = s_tipo;

            cmd.Parameters.Add("@conATB", SqlDbType.Int);
            cmd.Parameters["@conATB"].Value = conATB;


            //cmd.Parameters.Add("@grupoEtareo", SqlDbType.Int);
            //cmd.Parameters["@grupoEtareo"].Value = int.Parse(ddlGrupoEtareo.SelectedValue);

            //cmd.Parameters.Add("@sexo", SqlDbType.Int);
            //cmd.Parameters["@sexo"].Value = int.Parse(ddlSexo.SelectedValue);


            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Ds);

            return Ds.Tables[0];
            
    

          //FCLiteralTipoMuestra.Text = mostrarGrafico(0);
          //FCLiteralMicroorganismo.Text = mostrarGrafico(1);
            

        }


//        private void MostrarDatos(string s_tipo)
//        {
//            DataSet Ds = new DataSet();
//            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
//            SqlCommand cmd = new SqlCommand();
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.CommandText = "[LAB_EstadisticaMicrobiologia]";

//            int tipoM = 0;
//            int conATB=0;
//            if (s_tipo=="Aislamiento")            
//            {
//                if (ddlTipoMuestra.SelectedValue != "") tipoM= int.Parse(ddlTipoMuestra.SelectedValue);
//                if (ddlATB.SelectedValue != "Todos")
//                {
//                    if (ddlATB.SelectedValue == "Con ATB") conATB = 1;  ///si 
//                    else conATB = 2;//no
//                }
//            }
//            if (s_tipo == "Antibiotico")
//            { if (ddlTipoMuestraAntibioticos.SelectedValue != "") tipoM = int.Parse(ddlTipoMuestraAntibioticos.SelectedValue); }

//            int tipoGermen = 0;
//            if (s_tipo == "Antibiotico") { if (ddlMicroorganismosAntibioticos.SelectedValue != "") tipoGermen = int.Parse(ddlMicroorganismosAntibioticos.SelectedValue); }

            
//            int tipoAntibiotico = 0;
//            if (s_tipo == "Resistencia") {
//                if (ddlTipoMuestraAntibioticos.SelectedValue != "") tipoM = int.Parse(ddlTipoMuestraAntibioticos.SelectedValue); 
//                if (ddlMicroorganismosAntibioticos.SelectedValue != "") tipoGermen = int.Parse(ddlMicroorganismosAntibioticos.SelectedValue); 
//                if (hdfidAntibiotico.Value != "") tipoAntibiotico = int.Parse(hdfidAntibiotico.Value); }
            
            
//            int idsubitem=0;

//            //if (s_tipo == "Parametro")
//            //{

//            //    if (ddlSubItem.SelectedValue != "") idsubitem = int.Parse(ddlSubItem.SelectedValue);
//            //}
//            ///Parametros de fechas           
//            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
//            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

//            cmd.Parameters.Add("@fechaDesde", SqlDbType.NVarChar);
//            cmd.Parameters["@fechaDesde"].Value = fecha1.ToString("yyyyMMdd");
//            cmd.Parameters.Add("@fechaHasta", SqlDbType.NVarChar);
//            cmd.Parameters["@fechaHasta"].Value = fecha2.ToString("yyyyMMdd");
//            ///////


//            cmd.Parameters.Add("@idAnalisis", SqlDbType.Int);
//            cmd.Parameters["@idAnalisis"].Value =int.Parse( ddlAnalisis.SelectedValue);

//            cmd.Parameters.Add("@idOrigen", SqlDbType.NVarChar);
//            cmd.Parameters["@idOrigen"].Value =getListaOrigen();

//            cmd.Parameters.Add("@idTipoMuestra", SqlDbType.Int);
//            cmd.Parameters["@idTipoMuestra"].Value = tipoM;


//            cmd.Parameters.Add("@idGermen", SqlDbType.Int);
//            cmd.Parameters["@idGermen"].Value = tipoGermen;

//            cmd.Parameters.Add("@idAntibiotico", SqlDbType.Int);
//            cmd.Parameters["@idAntibiotico"].Value = tipoAntibiotico;

//            cmd.Parameters.Add("@idsubitem", SqlDbType.Int);
//            cmd.Parameters["@idsubitem"].Value = idsubitem;
            
            
//            cmd.Parameters.Add("@tipoReporte", SqlDbType.NVarChar);
//            cmd.Parameters["@tipoReporte"].Value = s_tipo;

//            cmd.Parameters.Add("@conATB", SqlDbType.Int);
//            cmd.Parameters["@conATB"].Value = conATB;


//            //cmd.Parameters.Add("@grupoEtareo", SqlDbType.Int);
//            //cmd.Parameters["@grupoEtareo"].Value = int.Parse(ddlGrupoEtareo.SelectedValue);

//            //cmd.Parameters.Add("@sexo", SqlDbType.Int);
//            //cmd.Parameters["@sexo"].Value = int.Parse(ddlSexo.SelectedValue);


//            cmd.Connection = conn;
//            SqlDataAdapter da = new SqlDataAdapter(cmd);
//            da.Fill(Ds);

//            Ds.Tables[0]
            
//          gvTipoMuestra.DataSource= Ds.Tables[0];
//          gvTipoMuestra.DataBind();

   
     
           
////////////////////////Solapa microorganismos
//            ddlTipoMuestra.DataTextField = "Tipo Muestra";
//            ddlTipoMuestra.DataValueField = "idMuestra";
//            ddlTipoMuestra.DataSource = Ds.Tables[0];
//            ddlTipoMuestra.DataBind();
//            ddlTipoMuestra.Items.Insert(0, new ListItem("--Todas--", "0"));

//          gvMicroorganismos.DataSource = Ds.Tables[1];
//          gvMicroorganismos.DataBind();

//          gvMicroorganismos.Visible = true;

//          lblFiltroMicroorganismo.Text = "Tipo de Muestra: " + ddlTipoMuestra.SelectedItem.Text + " - ATB: " + ddlATB.SelectedValue;

//          gvMicroorganismos.UpdateAfterCallBack = true;
//          lblFiltroMicroorganismo.UpdateAfterCallBack = true;

//            ////////////////////////////

//        //  FCLiteralMicroorganismo.Text = mostrarGrafico(1);
//            /////////////////////////

//            ////Solapa Antibioticos
//          if (tipoGermen == 0)
//          {
//              ddlTipoMuestraAntibioticos.DataTextField = "Tipo Muestra";
//              ddlTipoMuestraAntibioticos.DataValueField = "idMuestra";
//              ddlTipoMuestraAntibioticos.DataSource = Ds.Tables[0];
//              ddlTipoMuestraAntibioticos.DataBind();
//              ddlTipoMuestraAntibioticos.Items.Insert(0, new ListItem("--Todas--", "0"));

//              ddlMicroorganismosAntibioticos.DataTextField = "Microorganismo";
//              ddlMicroorganismosAntibioticos.DataValueField = "idGermen";
//              ddlMicroorganismosAntibioticos.DataSource = Ds.Tables[1];
//              ddlMicroorganismosAntibioticos.DataBind();
//              ddlMicroorganismosAntibioticos.Items.Insert(0, new ListItem("--Todos--", "0"));

//          } 

//          gvAntibiotico.DataSource = Ds.Tables[2];
//          gvAntibiotico.DataBind();

//          lblFiltroAntibiotico.Text = "Tipo de Muestra: " + ddlTipoMuestraAntibioticos.SelectedItem.Text + " - Aislamiento: " + ddlMicroorganismosAntibioticos.SelectedItem.Text;
//          lblFiltroAntibiotico.UpdateAfterCallBack = true;


//            ///////////////////
//          gvResultado.DataSource = Ds.Tables[3];
//          gvResultado.DataBind();

//          gvAntibioticoResistencia.DataSource = Ds.Tables[4];
//          gvAntibioticoResistencia.DataBind();

//          if (tipoAntibiotico > 0)
//          {
//              Antibiotico oAnti = new Antibiotico();
//              oAnti = (Antibiotico)oAnti.Get(typeof(Antibiotico), tipoAntibiotico);

//              string seleccion = "Tipo de Muestra:" + ddlTipoMuestraAntibioticos.SelectedItem.Text + " - " + " Microorganismo: " + ddlMicroorganismosAntibioticos.SelectedItem.Text;
//              lblResistenciaAntibiotico.Text = seleccion+ " - Resistencia de " + oAnti.Descripcion;
//              lblResistenciaAntibiotico.UpdateAfterCallBack = true;

//          }

//          //FCLiteralTipoMuestra.Text = mostrarGrafico(0);
//          //FCLiteralMicroorganismo.Text = mostrarGrafico(1);
            

//        }


                private string mostrarGrafico(int p)
        {
            string s_titulo="";
            string s_tipo="";
            string s_tipografico = "../FusionCharts/FCF_Pie3D.swf";
            DataTable dt = new DataTable();
            //dt = (DataTable)gvTipoMuestra.DataSource;
            string strXML = "";
            string ancho = "500";
            if (p == 0)
            {
            //    s_tipografico = "../FusionCharts/FCF_Pie3D.swf";
                s_titulo = ddlAnalisis.SelectedItem.Text;
                s_tipo = "Casos por tipo de muestra";
                 strXML = "<graph caption='" + s_titulo + "' subCaption='" + s_tipo + "' showPercentageInLabel='1' pieSliceDepth='10'  decimalPrecision='0' showNames='1'>";

                if (gvTipoMuestra.Rows.Count > 0)
                {
                    for (int i = 0; i < gvTipoMuestra.Rows.Count; i++)
                    {
                        strXML += "<set name='" + gvTipoMuestra.Rows[i].Cells[0].Text + "' value='" + gvTipoMuestra.Rows[i].Cells[1].Text + "' />";
                    }
                }
                strXML += "</graph>";
            }

            if (p == 1)
            {

                ancho = "1000";

            //    s_tipografico = "../FusionCharts/FCF_Column2D.swf";

                s_titulo = ddlAnalisis.SelectedItem.Text +" " +  ddlTipoMuestra.SelectedItem.Text;
                s_tipo = "Casos por Aislamiento";
                strXML = "<graph caption='" + s_titulo + "' subCaption='" + s_tipo + "' showPercentageInLabel='1' pieSliceDepth='10'  decimalPrecision='0' showNames='1'>";

                if (gvMicroorganismos.Rows.Count > 0)
                {
                    for (int i = 0; i < gvMicroorganismos.Rows.Count; i++)
                    {
                        strXML += "<set name='" + gvMicroorganismos.Rows[i].Cells[0].Text.Substring(0,5) + "' value='" + gvMicroorganismos.Rows[i].Cells[1].Text + "' />";
                    }
                }
                strXML += "</graph>";
            }
            //else
            //    Response.Redirect("SinDatos.aspx", false);


          

            return FusionCharts.RenderChart(s_tipografico, p.ToString(), strXML, "Sales"+p.ToString(), ancho, "200", false, false);
        }


        protected void lnkExcel_Click1(object sender, EventArgs e)
        {


        }

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {

        }

        protected void lnkRegresar_Click(object sender, EventArgs e)
        {
            if (Request["informe"].ToString() == "General")
                Response.Redirect("Filtro.aspx", false);
            else
                Response.Redirect("PorResultado.aspx", false);

        }

        protected void gvEstadistica_RowDataBound(object sender, GridViewRowEventArgs e)
        {


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton CmdProtocolo = (ImageButton)e.Row.Cells[15].Controls[1];
                    CmdProtocolo.CommandArgument = ddlAnalisis.SelectedValue +"~" + e.Row.Cells[0].Text; ///Codigo1 + ";" + codigo2
                    CmdProtocolo.CommandName = "Pacientes";
                    CmdProtocolo.ToolTip = "Ver Pacientes";
              
                    if (e.Row.Cells[1].Text != "&nbsp;") suma1 += int.Parse(e.Row.Cells[1].Text);
                    
                    if (e.Row.Cells[2].Text != "&nbsp;") grupo1 += int.Parse(e.Row.Cells[2].Text);                    
                    if (e.Row.Cells[3].Text != "&nbsp;") grupo2 += int.Parse(e.Row.Cells[3].Text);                    
                    if (e.Row.Cells[4].Text != "&nbsp;") grupo3 += int.Parse(e.Row.Cells[4].Text);                    
                    if (e.Row.Cells[5].Text != "&nbsp;") grupo4 += int.Parse(e.Row.Cells[5].Text);                    
                    if (e.Row.Cells[6].Text != "&nbsp;") grupo5 += int.Parse(e.Row.Cells[6].Text);                    
                    if (e.Row.Cells[7].Text != "&nbsp;") grupo6 += int.Parse(e.Row.Cells[7].Text);                    
                    if (e.Row.Cells[8].Text != "&nbsp;") grupo7 += int.Parse(e.Row.Cells[8].Text);                    
                    if (e.Row.Cells[9].Text != "&nbsp;") grupo8 += int.Parse(e.Row.Cells[9].Text);
                    if (e.Row.Cells[10].Text != "&nbsp;") grupo9 += int.Parse(e.Row.Cells[10].Text);
                    if (e.Row.Cells[11].Text != "&nbsp;") grupo10 += int.Parse(e.Row.Cells[11].Text);

                    if (e.Row.Cells[12].Text != "&nbsp;") masc += int.Parse(e.Row.Cells[12].Text);
                    if (e.Row.Cells[13].Text != "&nbsp;") fem += int.Parse(e.Row.Cells[13].Text);
                    if (e.Row.Cells[14].Text != "&nbsp;") ind += int.Parse(e.Row.Cells[14].Text);
                    

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "TOTAL CASOS";
                    e.Row.Cells[1].Text = suma1.ToString();
                    e.Row.Cells[2].Text = grupo1.ToString();
                    e.Row.Cells[3].Text = grupo2.ToString();
                    e.Row.Cells[4].Text = grupo3.ToString();
                    e.Row.Cells[5].Text = grupo4.ToString();
                    e.Row.Cells[6].Text = grupo5.ToString();
                    e.Row.Cells[7].Text = grupo6.ToString();
                    e.Row.Cells[8].Text = grupo7.ToString();
                    e.Row.Cells[9].Text = grupo8.ToString();
                    e.Row.Cells[10].Text = grupo9.ToString();
                    e.Row.Cells[11].Text = grupo10.ToString();
                    e.Row.Cells[12].Text = masc.ToString();
                    e.Row.Cells[13].Text = fem.ToString();
                    e.Row.Cells[14].Text = ind.ToString();
                    
                }

          
        }





        protected void imgExcel_Click(object sender, ImageClickEventArgs e)
        {

            ExportarExcelTipoMuestra();

        }

        private void ExportarExcelTipoMuestra()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            gvTipoMuestra.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(gvTipoMuestra);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + ddlAnalisis.SelectedItem.Text + "_TipoMuestra.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void ddlAnalisis_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    gvTipoMuestra.DataSource = GetDatosEstadistica("GV");
        //    gvTipoMuestra.DataBind();
        }

        protected void gvEstadistica_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Pacientes")
            //    InformePacientes(e.CommandArgument.ToString());

        }

        //private void InformePacientes(string p)
        //{
        //    Utility oUtil = new Utility();
        //    string[] arr = p.ToString().Split(("~").ToCharArray());


        //    string m_analisis = arr[0].ToString();
        //    string m_resultado =oUtil.RemoverSignosAcentos( arr[1].ToString());


        //    Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

        //    ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
        //    encabezado1.Value = oCon.EncabezadoLinea1;

        //    ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
        //    encabezado2.Value = oCon.EncabezadoLinea2;

        //    ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
        //    encabezado3.Value = oCon.EncabezadoLinea3;
        //    ////////////////////////////
      

        //    ParameterDiscreteValue titulo = new ParameterDiscreteValue();
        //    titulo.Value = "INFORME DE PACIENTES ";

        //    //if (rdbPaciente.SelectedValue == "1") titulo.Value = "INFORME DE PACIENTES EMBARAZADAS";
            
        //    //if (ddlGrupoEtareo.SelectedValue != "0") titulo.Value += " - Grupo Etareo: " + ddlGrupoEtareo.SelectedItem.Text;
        //    //if (ddlSexo.SelectedValue != "0") titulo.Value += " - Sexo: " + ddlSexo.SelectedItem.Text;

        //    ParameterDiscreteValue fechaDesde = new ParameterDiscreteValue();
        //    fechaDesde.Value = txtFechaDesde.Value;

        //    ParameterDiscreteValue fechaHasta = new ParameterDiscreteValue();
        //    fechaHasta.Value = txtFechaHasta.Value;


        //    oCr.Report.FileName = "Pacientes.rpt";
        //    oCr.ReportDocument.SetDataSource(GetDataPacientes(m_analisis, m_resultado));
        //    oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
        //    oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
        //    oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
        //    oCr.ReportDocument.ParameterFields[3].CurrentValues.Add(titulo);
        //    oCr.ReportDocument.ParameterFields[4].CurrentValues.Add(fechaDesde);
        //    oCr.ReportDocument.ParameterFields[5].CurrentValues.Add(fechaHasta);
        //    oCr.DataBind();

        //    MemoryStream oStream; // using System.IO
        //    oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("Content-Disposition", "attachment;filename=Pacientes.pdf");

        //    Response.BinaryWrite(oStream.ToArray());
        //    Response.End();  
        //}

        //private DataTable GetDataPacientes(string m_analisis, string m_resultado)
        //{
        //    string m_strCondicion="";

        //    if (rdbPaciente.SelectedValue == "1")
        //        m_strCondicion += " and (PD.iddiagnostico = 11999)  ";

        //    if (ddlGrupoEtareo.SelectedValue != "0")
        //    {
        //        if (ddlGrupoEtareo.SelectedValue == "1") m_strCondicion += " and P.unidadEdad>0";
        //        if (ddlGrupoEtareo.SelectedValue == "2") m_strCondicion += " and P.edad=1  and P.unidadedad=0";
        //        if (ddlGrupoEtareo.SelectedValue == "3") m_strCondicion += " and P.edad>=2 and P.edad<=4 and P.unidadedad=0   ";
        //        if (ddlGrupoEtareo.SelectedValue == "4") m_strCondicion += " and P.edad>=5 and P.edad<=9 and P.unidadedad=0    ";
        //        if (ddlGrupoEtareo.SelectedValue == "5") m_strCondicion += " and P.edad>=10 and P.edad<=14 and P.unidadedad=0   ";
        //        if (ddlGrupoEtareo.SelectedValue == "6") m_strCondicion += " and P.edad>=15 and P.edad<=24 and P.unidadedad=0   ";
        //        if (ddlGrupoEtareo.SelectedValue == "7") m_strCondicion += " and P.edad>=25 and P.edad<=34 and P.unidadedad=0   ";
        //        if (ddlGrupoEtareo.SelectedValue == "8") m_strCondicion += " and P.edad>=35 and P.edad<=44 and P.unidadedad=0   ";
        //        if (ddlGrupoEtareo.SelectedValue == "9") m_strCondicion += " and P.edad>=45 and P.edad<=64 and P.unidadedad=0   ";
        //        if (ddlGrupoEtareo.SelectedValue == "10") m_strCondicion += " and P.edad>=65  and P.unidadedad=0  ";

        //    }

        //    if (ddlSexo.SelectedValue != "0")
        //    {
        //        if (ddlSexo.SelectedValue == "1")
        //            m_strCondicion += " and P.sexo='F'";
        //        if (ddlSexo.SelectedValue == "2")
        //            m_strCondicion += " and P.sexo='M' ";
        //    }

        //    DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
        //    DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);


        //    string m_strSQL = " SELECT I.nombre AS ANALISIS, DP.resultadoCar AS RESULTADO, Pa.numeroDocumento, Pa.apellido, Pa.nombre, CONVERT(VARCHAR(10), Pa.fechaNacimiento, " +
        //                    " 103) AS FECHANACIMIENTO, Pa.referencia AS domicilio, CONVERT(varchar(10), P.fecha, 103) AS fecha, P.edad, dbo.NumeroProtocolo(P.idProtocolo) as numero " +
        //                    " FROM LAB_DetalleProtocolo AS DP " +
        //                    " INNER JOIN LAB_Protocolo AS P ON DP.idProtocolo = P.idProtocolo " +
        //                    " INNER JOIN LAB_Item AS I ON DP.idSUBItem = I.idItem " +
        //                    " INNER JOIN Sys_Paciente AS Pa ON P.idPaciente = Pa.idPaciente" +
        //                    " left JOIN vta_LAB_Embarazadas AS PD ON PD.idProtocolo = P.idProtocolo " +
        //                    " WHERE (I.idItem=" + m_analisis + ") AND (DP.resultadoCar = '" + m_resultado + "') AND (P.fecha >= '" + fecha1.ToString("yyyyMMdd") + "') AND (P.fecha <= '" + fecha2.ToString("yyyyMMdd") + "') and " +
        //                    " ( DP.conresultado=1) " + m_strCondicion +
        //                    " order by P.fecha  ";

   
            
        //    DataSet Ds = new DataSet();
        //    SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
        //    adapter.Fill(Ds);


        //    DataTable data = Ds.Tables[0];
        //    return data;
        //}

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            MostrarReporteGeneral();
        }

        protected void imgPdf_Click(object sender, ImageClickEventArgs e)
        {
         //   MostrarPdf();
        }

        protected void imgExcel0_Click(object sender, ImageClickEventArgs e)
        {
            ExportarExcelMicroorganismos();
        }

        private void ExportarExcelMicroorganismos()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            gvMicroorganismos.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(gvMicroorganismos);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + ddlAnalisis.SelectedItem.Text + "_Microorganismos.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void imgExcel1_Click(object sender, ImageClickEventArgs e)
        {
            ExportarExcelAntibioticos();
        }

        private void ExportarExcelAntibioticos()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            gvAntibiotico.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(gvAntibiotico);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + ddlAnalisis.SelectedItem.Text + "_Antibiotico.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void imgExcel2_Click(object sender, ImageClickEventArgs e)
        {
            ExportarExcelResultados();
        }

        private void ExportarExcelResultados()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            gvResultado.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(gvResultado);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + ddlAnalisis.SelectedItem.Text + "_Resultado.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void gvTipoMuestra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                suma1 = 0;
                grupo1 = 0; grupo2 = 0; grupo3 = 0; grupo4 = 0; grupo5 = 0; grupo6 = 0; grupo7 = 0; grupo8 = 0; grupo9 = 0; grupo10 = 0;
                masc = 0; fem = 0; ind = 0; emb = 0;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              

                if (e.Row.Cells[1].Text != "&nbsp;") suma1 += int.Parse(e.Row.Cells[1].Text);

                if (e.Row.Cells[2].Text != "&nbsp;") grupo1 += int.Parse(e.Row.Cells[2].Text);
                if (e.Row.Cells[3].Text != "&nbsp;") grupo2 += int.Parse(e.Row.Cells[3].Text);
                if (e.Row.Cells[4].Text != "&nbsp;") grupo3 += int.Parse(e.Row.Cells[4].Text);
                if (e.Row.Cells[5].Text != "&nbsp;") grupo4 += int.Parse(e.Row.Cells[5].Text);
                if (e.Row.Cells[6].Text != "&nbsp;") grupo5 += int.Parse(e.Row.Cells[6].Text);
                if (e.Row.Cells[7].Text != "&nbsp;") grupo6 += int.Parse(e.Row.Cells[7].Text);
                if (e.Row.Cells[8].Text != "&nbsp;") grupo7 += int.Parse(e.Row.Cells[8].Text);
                if (e.Row.Cells[9].Text != "&nbsp;") grupo8 += int.Parse(e.Row.Cells[9].Text);
                if (e.Row.Cells[10].Text != "&nbsp;") grupo9 += int.Parse(e.Row.Cells[10].Text);
                if (e.Row.Cells[11].Text != "&nbsp;") grupo10 += int.Parse(e.Row.Cells[11].Text);

                if (e.Row.Cells[12].Text != "&nbsp;") masc += int.Parse(e.Row.Cells[12].Text);
                if (e.Row.Cells[13].Text != "&nbsp;") fem += int.Parse(e.Row.Cells[13].Text);
                if (e.Row.Cells[14].Text != "&nbsp;") emb += int.Parse(e.Row.Cells[14].Text);
                if (e.Row.Cells[15].Text != "&nbsp;") ind += int.Parse(e.Row.Cells[15].Text);


                


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTAL CASOS";
                e.Row.Cells[1].Text = suma1.ToString();
                e.Row.Cells[2].Text = grupo1.ToString();
                e.Row.Cells[3].Text = grupo2.ToString();
                e.Row.Cells[4].Text = grupo3.ToString();
                e.Row.Cells[5].Text = grupo4.ToString();
                e.Row.Cells[6].Text = grupo5.ToString();
                e.Row.Cells[7].Text = grupo6.ToString();
                e.Row.Cells[8].Text = grupo7.ToString();
                e.Row.Cells[9].Text = grupo8.ToString();
                e.Row.Cells[10].Text = grupo9.ToString();
                e.Row.Cells[11].Text = grupo10.ToString();
                e.Row.Cells[12].Text = masc.ToString();
                e.Row.Cells[13].Text = fem.ToString();
                e.Row.Cells[14].Text = emb.ToString();
                e.Row.Cells[15].Text = ind.ToString();

            }
            for (int i = 1; i <= 15; i++) if (e.Row.Cells[i].Text == "0") e.Row.Cells[i].Text = "";

          
        }

        protected void gvMicroorganismos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton CmdModificar = (LinkButton)e.Row.Cells[20].Controls[1];
                CmdModificar.CommandArgument = gvMicroorganismos.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdModificar.CommandName = "Resistencia";
                CmdModificar.ToolTip = "Resistencia";
                for (int i = 1; i <= 15; i++) if (e.Row.Cells[i].Text == "0") e.Row.Cells[i].Text = "";
            }
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    suma1 = 0;
            //    grupo1 = 0; grupo2 = 0; grupo3 = 0; grupo4 = 0; grupo5 = 0; grupo6 = 0; grupo7 = 0; grupo8 = 0; grupo9 = 0; grupo10 = 0;
            //    masc = 0; fem = 0; ind = 0; emb = 0;
            //}
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //    if (e.Row.Cells[1].Text != "&nbsp;") suma1 += int.Parse(e.Row.Cells[1].Text);

            //    if (e.Row.Cells[2].Text != "&nbsp;") grupo1 += int.Parse(e.Row.Cells[2].Text);
            //    if (e.Row.Cells[3].Text != "&nbsp;") grupo2 += int.Parse(e.Row.Cells[3].Text);
            //    if (e.Row.Cells[4].Text != "&nbsp;") grupo3 += int.Parse(e.Row.Cells[4].Text);
            //    if (e.Row.Cells[5].Text != "&nbsp;") grupo4 += int.Parse(e.Row.Cells[5].Text);
            //    if (e.Row.Cells[6].Text != "&nbsp;") grupo5 += int.Parse(e.Row.Cells[6].Text);
            //    if (e.Row.Cells[7].Text != "&nbsp;") grupo6 += int.Parse(e.Row.Cells[7].Text);
            //    if (e.Row.Cells[8].Text != "&nbsp;") grupo7 += int.Parse(e.Row.Cells[8].Text);
            //    if (e.Row.Cells[9].Text != "&nbsp;") grupo8 += int.Parse(e.Row.Cells[9].Text);
            //    if (e.Row.Cells[10].Text != "&nbsp;") grupo9 += int.Parse(e.Row.Cells[10].Text);
            //    if (e.Row.Cells[11].Text != "&nbsp;") grupo10 += int.Parse(e.Row.Cells[11].Text);

            //    if (e.Row.Cells[12].Text != "&nbsp;") masc += int.Parse(e.Row.Cells[12].Text);
            //    if (e.Row.Cells[13].Text != "&nbsp;") fem += int.Parse(e.Row.Cells[13].Text);
            //    if (e.Row.Cells[14].Text != "&nbsp;") emb += int.Parse(e.Row.Cells[14].Text);
            //    if (e.Row.Cells[15].Text != "&nbsp;") ind += int.Parse(e.Row.Cells[15].Text);


            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[0].Text = "TOTAL CASOS";
            //    e.Row.Cells[1].Text = suma1.ToString();
            //    e.Row.Cells[2].Text = grupo1.ToString();
            //    e.Row.Cells[3].Text = grupo2.ToString();
            //    e.Row.Cells[4].Text = grupo3.ToString();
            //    e.Row.Cells[5].Text = grupo4.ToString();
            //    e.Row.Cells[6].Text = grupo5.ToString();
            //    e.Row.Cells[7].Text = grupo6.ToString();
            //    e.Row.Cells[8].Text = grupo7.ToString();
            //    e.Row.Cells[9].Text = grupo8.ToString();
            //    e.Row.Cells[10].Text = grupo9.ToString();
            //    e.Row.Cells[11].Text = grupo10.ToString();
            //    e.Row.Cells[12].Text = masc.ToString();
            //    e.Row.Cells[13].Text = fem.ToString();
            //    e.Row.Cells[14].Text = emb.ToString();
            //    e.Row.Cells[15].Text = ind.ToString();

            //}
         
        }


        protected void gvAntibiotico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Resistencia")
            {
                hdfidAntibiotico.Value= e.CommandArgument.ToString();
               
               // MostrarDatos("Resistencia");


                DataTable dt = MostrarDatos("Resistencia");
                gvAntibioticoResistencia.DataSource =dt;
                gvAntibioticoResistencia.DataBind();
                gvAntibioticoResistencia.Visible = true;
                
                    Antibiotico oAnti = new Antibiotico();
                    oAnti = (Antibiotico)oAnti.Get(typeof(Antibiotico),int.Parse( hdfidAntibiotico.Value));

                    string seleccion = "Tipo de Muestra:" + ddlTipoMuestraAntibioticos.SelectedItem.Text + " - " + " Microorganismo: " + ddlMicroorganismosAntibioticos.SelectedItem.Text;
                    lblResistenciaAntibiotico.Text = seleccion + " - Resistencia de " + oAnti.Descripcion;
                    lblResistenciaAntibiotico.Visible = true;

                    //gvAntibioticoResistencia.UpdateAfterCallBack = true;
                    //lblResistenciaAntibiotico.UpdateAfterCallBack = true;

                

            }
        }

        
        
        
        protected void gvAntibiotico_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                 suma1 = 0;
                 grupo1 = 0; grupo2 = 0; grupo3 = 0; grupo4 = 0;  grupo5 = 0; grupo6 = 0;  grupo7 = 0; grupo8 = 0; grupo9 = 0; grupo10 = 0;
                 masc = 0; fem = 0;  ind = 0;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               LinkButton CmdModificar = (LinkButton)e.Row.Cells[16].Controls[1];
               CmdModificar.CommandArgument = gvAntibiotico.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdModificar.CommandName = "Resistencia";
                CmdModificar.ToolTip = "Resistencia";
                for (int i = 1; i <= 16; i++) if (e.Row.Cells[i].Text == "0") e.Row.Cells[i].Text = "";
            }
            
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //  if (e.Row.Cells[15].Text != "&nbsp;") suma1 += int.Parse(e.Row.Cells[15].Text);

            //    if (e.Row.Cells[2].Text != "&nbsp;") grupo1 += int.Parse(e.Row.Cells[2].Text);
            //    if (e.Row.Cells[3].Text != "&nbsp;") grupo2 += int.Parse(e.Row.Cells[3].Text);
            //    if (e.Row.Cells[4].Text != "&nbsp;") grupo3 += int.Parse(e.Row.Cells[4].Text);
            //    if (e.Row.Cells[5].Text != "&nbsp;") grupo4 += int.Parse(e.Row.Cells[5].Text);
            //    if (e.Row.Cells[6].Text != "&nbsp;") grupo5 += int.Parse(e.Row.Cells[6].Text);
            //    if (e.Row.Cells[7].Text != "&nbsp;") grupo6 += int.Parse(e.Row.Cells[7].Text);
            //    if (e.Row.Cells[8].Text != "&nbsp;") grupo7 += int.Parse(e.Row.Cells[8].Text);
            //    if (e.Row.Cells[9].Text != "&nbsp;") grupo8 += int.Parse(e.Row.Cells[9].Text);
            //    if (e.Row.Cells[10].Text != "&nbsp;") grupo9 += int.Parse(e.Row.Cells[10].Text);
            //    if (e.Row.Cells[11].Text != "&nbsp;") grupo10 += int.Parse(e.Row.Cells[11].Text);

            //    if (e.Row.Cells[12].Text != "&nbsp;") masc += int.Parse(e.Row.Cells[12].Text);
            //    if (e.Row.Cells[13].Text != "&nbsp;") fem += int.Parse(e.Row.Cells[13].Text);
            //    if (e.Row.Cells[14].Text != "&nbsp;") ind += int.Parse(e.Row.Cells[14].Text);


            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[0].Text = "TOTAL CASOS";
            //    e.Row.Cells[15].Text = suma1.ToString();
            //    e.Row.Cells[2].Text = grupo1.ToString();
            //    e.Row.Cells[3].Text = grupo2.ToString();
            //    e.Row.Cells[4].Text = grupo3.ToString();
            //    e.Row.Cells[5].Text = grupo4.ToString();
            //    e.Row.Cells[6].Text = grupo5.ToString();
            //    e.Row.Cells[7].Text = grupo6.ToString();
            //    e.Row.Cells[8].Text = grupo7.ToString();
            //    e.Row.Cells[9].Text = grupo8.ToString();
            //    e.Row.Cells[10].Text = grupo9.ToString();
            //    e.Row.Cells[11].Text = grupo10.ToString();
            //    e.Row.Cells[12].Text = masc.ToString();
            //    e.Row.Cells[13].Text = fem.ToString();
            //    e.Row.Cells[14].Text = ind.ToString();

            //}
        }

        protected void gvResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            //    ImageButton CmdProtocolo = (ImageButton)e.Row.Cells[17].Controls[1];
            //    CmdProtocolo.CommandArgument = e.Row.Cells[0].Text + "~" + e.Row.Cells[1].Text; ///Codigo1 + ";" + codigo2
            //    CmdProtocolo.CommandName = "Pacientes";
            //    CmdProtocolo.ToolTip = "Ver Pacientes";
                for (int i = 1; i <= 16; i++) if (e.Row.Cells[i].Text == "0") e.Row.Cells[i].Text = "";
            }
            
        }

        protected void btnBuscarAislamiento_Click(object sender, EventArgs e)
        {
            //MostrarDatos("Aislamiento");

            DataTable dt = MostrarDatos("Aislamiento");

            gvMicroorganismos.DataSource =dt;          
            gvMicroorganismos.DataBind();
            HFMicroorganismo.Value = getValoresMicroorganismos();
            //gvMicroorganismos.Visible = true;

            gvMicroorganismosATB.Visible = false;
            lblFiltroMicroorganismoATB.Visible = false;
            btnGraficoResistencia.Visible = false;
            lblFiltroMicroorganismo.Text = "Tipo de Muestra: " + ddlTipoMuestra.SelectedItem.Text + " - ATB: " + ddlATB.SelectedValue;

          //  gvMicroorganismos.UpdateAfterCallBack = true;
          //  lblFiltroMicroorganismo.UpdateAfterCallBack = true;
            

            SetSelectedTab(TabIndex.TWO);
            
        }

        protected void btnBuscarAntibioticos_Click(object sender, EventArgs e)
        {
         


            

            DataTable dt = MostrarDatos("Antibiotico");


            gvAntibiotico.DataSource = dt;
            gvAntibiotico.DataBind();
            lblFiltroAntibiotico.Text = "Tipo de Muestra: " + ddlTipoMuestraAntibioticos.SelectedItem.Text + " - Aislamiento: " + ddlMicroorganismosAntibioticos.SelectedItem.Text;
            gvAntibioticoResistencia.Visible = false;
            
            //lblFiltroAntibiotico.UpdateAfterCallBack = true;
            //gvAntibiotico.UpdateAfterCallBack = true;
            //gvAntibioticoResistencia.UpdateAfterCallBack = true;


            

            //SetSelectedTab(TabIndex.TWO);

            SetSelectedTab(TabIndex.THREE);

        }

     

        protected void btnVerParametro_Click(object sender, EventArgs e)
        {
            MostrarDatos("Parametro");
        }

        //protected void btnGraficoMicroorganismos_Click(object sender, EventArgs e)
        //{
        //    FCLiteralMicroorganismo.Text = mostrarGrafico(1);
        //    SetSelectedTab(TabIndex.THREE);
        //}

        protected void gvAntibioticoResistencia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    suma1 = 0;
                    grupo1 = 0; grupo2 = 0; grupo3 = 0; grupo4 = 0; grupo5 = 0; grupo6 = 0; grupo7 = 0; grupo8 = 0; grupo9 = 0; grupo10 = 0;
                    masc = 0; fem = 0; ind = 0;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {


                    if (e.Row.Cells[1].Text != "&nbsp;") suma1 += int.Parse(e.Row.Cells[1].Text);

                    if (e.Row.Cells[2].Text != "&nbsp;") grupo1 += int.Parse(e.Row.Cells[2].Text);
                    if (e.Row.Cells[3].Text != "&nbsp;") grupo2 += int.Parse(e.Row.Cells[3].Text);
                    if (e.Row.Cells[4].Text != "&nbsp;") grupo3 += int.Parse(e.Row.Cells[4].Text);
                    if (e.Row.Cells[5].Text != "&nbsp;") grupo4 += int.Parse(e.Row.Cells[5].Text);
                    if (e.Row.Cells[6].Text != "&nbsp;") grupo5 += int.Parse(e.Row.Cells[6].Text);
                    if (e.Row.Cells[7].Text != "&nbsp;") grupo6 += int.Parse(e.Row.Cells[7].Text);
                    if (e.Row.Cells[8].Text != "&nbsp;") grupo7 += int.Parse(e.Row.Cells[8].Text);
                    if (e.Row.Cells[9].Text != "&nbsp;") grupo8 += int.Parse(e.Row.Cells[9].Text);
                    if (e.Row.Cells[10].Text != "&nbsp;") grupo9 += int.Parse(e.Row.Cells[10].Text);
                    if (e.Row.Cells[11].Text != "&nbsp;") grupo10 += int.Parse(e.Row.Cells[11].Text);

                    if (e.Row.Cells[12].Text != "&nbsp;") masc += int.Parse(e.Row.Cells[12].Text);
                    if (e.Row.Cells[13].Text != "&nbsp;") fem += int.Parse(e.Row.Cells[13].Text);
                    if (e.Row.Cells[14].Text != "&nbsp;") ind += int.Parse(e.Row.Cells[14].Text);


                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "TOTAL CASOS";
                    e.Row.Cells[1].Text = suma1.ToString();
                    e.Row.Cells[2].Text = grupo1.ToString();
                    e.Row.Cells[3].Text = grupo2.ToString();
                    e.Row.Cells[4].Text = grupo3.ToString();
                    e.Row.Cells[5].Text = grupo4.ToString();
                    e.Row.Cells[6].Text = grupo5.ToString();
                    e.Row.Cells[7].Text = grupo6.ToString();
                    e.Row.Cells[8].Text = grupo7.ToString();
                    e.Row.Cells[9].Text = grupo8.ToString();
                    e.Row.Cells[10].Text = grupo9.ToString();
                    e.Row.Cells[11].Text = grupo10.ToString();
                    e.Row.Cells[12].Text = masc.ToString();
                    e.Row.Cells[13].Text = fem.ToString();
                    e.Row.Cells[14].Text = ind.ToString();

                }
                for (int i = 1; i <= 14; i++) if (e.Row.Cells[i].Text == "0") e.Row.Cells[i].Text = "";
            }
            catch (Exception ex)
            {
                string exception = "";
                //while (ex != null)
                //{
                //    exception = ex.Message + "<br>";

                //}
            }
        }

        protected void btnDescargarDetallePacientes_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            GridView dg = new GridView();
            dg.EnableViewState = false;
            dg.DataSource = GetDataPacientes("General");
            dg.DataBind();

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(dg);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + ddlAnalisis.SelectedItem.Text + "_Pacientes.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void gvResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Pacientes")
            //    InformePacientes(e.CommandArgument.ToString());

        }

        private void InformePacientes(string p)
        {
          
        }

        private DataTable GetDataPacientes(string tipo)
        {
            string m_strCondicion = " and P.baja=0 ";

            //if (rdbPaciente.SelectedValue == "1")
            //    m_strCondicion += " and (PD.iddiagnostico = 11999)  ";

            //if (ddlGrupoEtareo.SelectedValue != "0")
            //{
            //    if (ddlGrupoEtareo.SelectedValue == "1") m_strCondicion += " and P.unidadEdad>0";
            //    if (ddlGrupoEtareo.SelectedValue == "2") m_strCondicion += " and P.edad=1  and P.unidadedad=0";
            //    if (ddlGrupoEtareo.SelectedValue == "3") m_strCondicion += " and P.edad>=2 and P.edad<=4 and P.unidadedad=0   ";
            //    if (ddlGrupoEtareo.SelectedValue == "4") m_strCondicion += " and P.edad>=5 and P.edad<=9 and P.unidadedad=0    ";
            //    if (ddlGrupoEtareo.SelectedValue == "5") m_strCondicion += " and P.edad>=10 and P.edad<=14 and P.unidadedad=0   ";
            //    if (ddlGrupoEtareo.SelectedValue == "6") m_strCondicion += " and P.edad>=15 and P.edad<=24 and P.unidadedad=0   ";
            //    if (ddlGrupoEtareo.SelectedValue == "7") m_strCondicion += " and P.edad>=25 and P.edad<=34 and P.unidadedad=0   ";
            //    if (ddlGrupoEtareo.SelectedValue == "8") m_strCondicion += " and P.edad>=35 and P.edad<=44 and P.unidadedad=0   ";
            //    if (ddlGrupoEtareo.SelectedValue == "9") m_strCondicion += " and P.edad>=45 and P.edad<=64 and P.unidadedad=0   ";
            //    if (ddlGrupoEtareo.SelectedValue == "10") m_strCondicion += " and P.edad>=65  and P.unidadedad=0  ";

            //}

            
            //    if (ddlSexo.SelectedValue == "1")
                    m_strCondicion += " and P.idOrigen in (" + getListaOrigen() +")";
            //    if (ddlSexo.SelectedValue == "2")
            //        m_strCondicion += " and P.sexo='M' ";
            

            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            string m_strSQL = "";


            if (tipo == "General")
            {      m_strSQL = @"SELECT distinct
dbo.NumeroProtocolo(P.idProtocolo) as [Numero Protocolo], CONVERT(varchar(10), P.fecha, 103) AS fecha,M.nombre as muestra, case when Pa.idestado=1 then Pa.numeroDocumento else 0 end as [Nro. Documento], 
Pa.apellido, Pa.nombre, CONVERT(VARCHAR(10), Pa.fechaNacimiento,  103) AS FECHANACIMIENTO, Pa.referencia AS domicilio, P.edad  , case P.unidadEdad when 0 then 'años' when 1 then 'meses' when 2 then 'días' end tipoEdad ,P.sexo ,
case when PD.iddiagnostico is null then 'No' else 'Si' end as embarazada

FROM LAB_DetalleProtocolo AS DP  
INNER JOIN LAB_Protocolo AS P ON DP.idProtocolo = P.idProtocolo  
INNER JOIN Sys_Paciente AS Pa ON P.idPaciente = Pa.idPaciente 
inner join lab_muestra as M on M.idmuestra=P.idmuestra
left JOIN vta_LAB_Embarazadas AS PD ON PD.idProtocolo = P.idProtocolo  
WHERE dp.idItem=" + ddlAnalisis.SelectedValue + " AND (P.fecha >= '" + fecha1.ToString("yyyyMMdd") + "') AND (P.fecha <= '" + fecha2.ToString("yyyyMMdd") + "')  and P.idtiposervicio=3 and P.estado=2" + m_strCondicion;
        }
            if (tipo == "Resultado")
            {
                m_strSQL = @" SELECT 
dbo.NumeroProtocolo(P.idProtocolo) as [Numero Protocolo], CONVERT(varchar(10), P.fecha, 103) AS fecha, case when Pa.idestado=1 then Pa.numeroDocumento else 0 end as [Nro. Documento], 
Pa.apellido, Pa.nombre, CONVERT(VARCHAR(10), Pa.fechaNacimiento,  103) AS FECHANACIMIENTO, Pa.referencia AS domicilio, P.edad  , case P.unidadEdad when 0 then 'años' when 1 then 'meses' when 2 then 'días' end tipoEdad,P.sexo ,
case when PD.iddiagnostico is null then 'No' else 'Si' end as embarazada, I1.nombre AS ANALISIS, DP.resultadoCar AS RESULTADO  

FROM LAB_DetalleProtocolo AS DP  
INNER JOIN LAB_Protocolo AS P ON DP.idProtocolo = P.idProtocolo  
INNER JOIN LAB_Item AS I ON DP.idItem = I.idItem  
inner join lab_item as I1 on Dp.idsubitem= I1.iditem
INNER JOIN Sys_Paciente AS Pa ON P.idPaciente = Pa.idPaciente 
left JOIN vta_LAB_Embarazadas AS PD ON PD.idProtocolo = P.idProtocolo  

WHERE dp.idItem=" + ddlAnalisis.SelectedValue + " AND (P.fecha >= '" + fecha1.ToString("yyyyMMdd") + "') AND (P.fecha <= '" + fecha2.ToString("yyyyMMdd") + "')  and P.idtiposervicio=3 " +
   " and I1.idtiporesultado=3 and resultadocar<>'' and idusuariovalida>0   " + m_strCondicion + " order by P.idprotocolo ,P.fecha  ";

            }

            if (tipo == "Aislamiento")
            {
                m_strSQL = @"SELECT DISTINCT 
                      dbo.NumeroProtocolo(P.idProtocolo) AS [Numero Protocolo], CONVERT(varchar(10), P.fecha, 103) AS fecha, M.nombre AS muestra, 
                      Pa.numeroDocumento AS [Nro. Documento], Pa.apellido, Pa.nombre, CONVERT(VARCHAR(10), Pa.fechaNacimiento, 103) AS FECHANACIMIENTO, 
                      Pa.referencia AS domicilio, P.edad,
 case P.unidadEdad when 0 then 'años' when 1 then 'meses' when 2 then 'días' end tipoEdad, P.sexo, case when PD.iddiagnostico is null then 'No' else 'Si' end as embarazada,
 AIS.nombre AS aislamiento, case when AIS.atb =1 then 'Si'  else 'No' end as atb
FROM         LAB_Protocolo AS P INNER JOIN
                      Sys_Paciente AS Pa ON P.idPaciente = Pa.idPaciente INNER JOIN
                      LAB_Muestra AS M ON M.idMuestra = P.idMuestra INNER JOIN
                      vta_LAB_Aislamiento AS AIS ON P.idProtocolo = AIS.idProtocolo LEFT OUTER JOIN
                      vta_LAB_Embarazadas AS PD ON PD.idProtocolo = P.idProtocolo
WHERE ais.idItem=" + ddlAnalisis.SelectedValue + " AND (P.fecha >= '" + fecha1.ToString("yyyyMMdd") + "') AND (P.fecha <= '" + fecha2.ToString("yyyyMMdd") + "')  and P.idtiposervicio=3 "  + m_strCondicion; // +" order by P.idprotocolo ,P.fecha  ";

            }


            if (tipo == "ATB")
            {
                m_strSQL = @"SELECT DISTINCT 
                      dbo.NumeroProtocolo(P.idProtocolo) AS [Numero Protocolo], CONVERT(varchar(10), P.fecha, 103) AS fecha, M.nombre AS muestra, 
                      Pa.numeroDocumento AS [Nro. Documento], Pa.apellido, Pa.nombre, CONVERT(VARCHAR(10), Pa.fechaNacimiento, 103) AS FECHANACIMIENTO, 
                      Pa.referencia AS domicilio, P.edad, CASE P.unidadEdad WHEN 0 THEN 'años' WHEN 1 THEN 'meses' WHEN 2 THEN 'días' END AS tipoEdad, P.sexo,
                      CASE WHEN PD.iddiagnostico IS NULL THEN 'No' ELSE 'Si' END AS embarazada, ATB.germen AS [Mricroorganismo], ATB.antibiotico , ATB.resultado as [Resistencia]
FROM         LAB_Protocolo AS P INNER JOIN
                      Sys_Paciente AS Pa ON P.idPaciente = Pa.idPaciente INNER JOIN
                      LAB_Muestra AS M ON M.idMuestra = P.idMuestra INNER JOIN
                      vta_LAB_Antibiograma AS ATB ON P.idProtocolo = ATB.idProtocolo LEFT OUTER JOIN
                      vta_LAB_Embarazadas AS PD ON PD.idProtocolo = P.idProtocolo
WHERE ATB.idItem=" + ddlAnalisis.SelectedValue + " AND (P.fecha >= '" + fecha1.ToString("yyyyMMdd") + "') AND (P.fecha <= '" + fecha2.ToString("yyyyMMdd") + "')  and P.idtiposervicio=3 " + m_strCondicion;// +" order by P.idprotocolo ,P.fecha  ";

            }
          



            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);


            DataTable data = Ds.Tables[0];
            return data;
        }

      

        protected void imgExcelResultadoPacientes_Click1(object sender, ImageClickEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            GridView dg = new GridView();
            dg.EnableViewState = false;
            dg.DataSource = GetDataPacientes("Resultado");
            dg.DataBind();

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(dg);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + ddlAnalisis.SelectedItem.Text+ "_Resultados.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void imgExcelDetallePacientes_Click(object sender, ImageClickEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            GridView dg = new GridView();
            dg.EnableViewState = false;
            dg.DataSource = GetDataPacientes("General");
            dg.DataBind();

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(dg);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + ddlAnalisis.SelectedItem.Text + "_Pacientes.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void imgExcelDetallePacientesAislamientos_Click(object sender, ImageClickEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            GridView dg = new GridView();
            dg.EnableViewState = false;
            dg.DataSource = GetDataPacientes("Aislamiento");
            dg.DataBind();

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(dg);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + ddlAnalisis.SelectedItem.Text.Trim() + "_Pacientes.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void imgExcelDetalleAtb_Click(object sender, ImageClickEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            GridView dg = new GridView();
            dg.EnableViewState = false;
            dg.DataSource = GetDataPacientes("ATB");
            dg.DataBind();

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(dg);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + ddlAnalisis.SelectedItem.Text + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();

        }

        protected void gvMicroorganismos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Resistencia")
            {
                try
                {
                    int idGermen = int.Parse(e.CommandArgument.ToString());

                    // MostrarDatos("Resistencia");

                    Germen oAnti = new Germen();
                    oAnti = (Germen)oAnti.Get(typeof(Germen), idGermen);
                    //  DataTable dt = MostrarDatos("Resistencia");
                    gvMicroorganismosATB.DataSource = getATB(oAnti.Nombre);
                    gvMicroorganismosATB.DataBind();
                    gvMicroorganismosATB.Visible = true;
                    lblFiltroMicroorganismoATB.Visible = true;
                    btnGraficoResistencia.Visible = true;

                    HFResistencia.Value = getValoresResistencia();


                    //string seleccion = "Tipo de Muestra:" + ddlTipoMuestraAntibioticos.SelectedItem.Text + " - " + " Microorganismo: " + ddlMicroorganismosAntibioticos.SelectedItem.Text;
                    lblFiltroMicroorganismoATB.Text = oAnti.Nombre;
                    //lblResistenciaAntibiotico.Visible = true;

                    //gvMicroorganismosATB.UpdateAfterCallBack = true;
                    //lblFiltroMicroorganismoATB.UpdateAfterCallBack = true;
                }
                catch (Exception ex)
                {
                    string exception = "";
                    while (ex != null)
                    {
                        exception = ex.Message + "<br>";

                    }
                }
                SetSelectedTab(TabIndex.TWO);

            }
        }

        private string getValoresResistencia()
        {
            string s_valores = "";
            

            for (int i = 0; i < gvMicroorganismosATB.Rows.Count; i++)
            {
                string s_nombre = gvMicroorganismosATB.Rows[i].Cells[0].Text.Replace(";", "");
                s_nombre = s_nombre.Replace("&#", "");
              
                if (s_valores == "")
                    s_valores =s_nombre + ";" + gvMicroorganismosATB.Rows[i].Cells[1].Text + ";"+gvMicroorganismosATB.Rows[i].Cells[2].Text +";"+ gvMicroorganismosATB.Rows[i].Cells[3].Text;
                else
                    s_valores += "=" + s_nombre + ";" + gvMicroorganismosATB.Rows[i].Cells[1].Text + ";" + gvMicroorganismosATB.Rows[i].Cells[2].Text + ";" + gvMicroorganismosATB.Rows[i].Cells[3].Text;
            }
            s_valores = s_valores.Replace("&#", "");
            return s_valores;
        }

        private DataTable getATB(string s_germen)
        {

            string m_strCondicion = " and P.baja=0 ";

          
            m_strCondicion += " and P.idOrigen in (" + getListaOrigen() + ")";

            if (ddlTipoMuestra.SelectedValue != "0") m_strCondicion += " and P.idMuestra=" + ddlTipoMuestra.SelectedValue;
            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            string                 m_strSQL = @"SELECT Child.* FROM (
SELECT  
  Antibiotico ,sensibilidad
FROM             vta_LAB_Antibiograma A INNER JOIN
                      LAB_Protocolo P ON A.idProtocolo = P.idProtocolo

where A.resultado<>'' and  A.germen like '%" + s_germen +"%' and A.idItem=" + ddlAnalisis.SelectedValue + " AND (P.fecha >= '" + fecha1.ToString("yyyyMMdd") + "') AND (P.fecha <= '" + fecha2.ToString("yyyyMMdd") + "')  and P.idtiposervicio=3 and P.estado=2" + m_strCondicion + //; germen='Escherichia coli' and P.fecha>='20130101'
")  pvt PIVOT (count(sensibilidad) FOR sensibilidad IN ([Resistente],[Sensible],[Intermedio],[No Probado],[Apto para Sinergia],[Sensibilidad Disminuida],[Sin Reactivo]))  AS Child order by antibiotico";
 
                              


            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);


            DataTable data = Ds.Tables[0];
            return data;
        }

        protected void gvMicroorganismosATB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                for (int i = 1; i <=4; i++) if (e.Row.Cells[i].Text == "0") e.Row.Cells[i].Text = "";
            }
            
        }
        //private void MostrarDatosPacientes()
        //{
        //    DataSet Ds = new DataSet();
        //    SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.CommandText = "[LAB_EstadisticaMicrobiologia]";

      
        //    DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
        //    DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

        //    cmd.Parameters.Add("@fechaDesde", SqlDbType.NVarChar);
        //    cmd.Parameters["@fechaDesde"].Value = fecha1.ToString("yyyyMMdd");
        //    cmd.Parameters.Add("@fechaHasta", SqlDbType.NVarChar);
        //    cmd.Parameters["@fechaHasta"].Value = fecha2.ToString("yyyyMMdd");
        //    ///////


        //    cmd.Parameters.Add("@idAnalisis", SqlDbType.Int);
        //    cmd.Parameters["@idAnalisis"].Value = int.Parse(ddlAnalisis.SelectedValue);

        //    //cmd.Parameters.Add("@idTipoMuestra", SqlDbType.Int);
        //    //cmd.Parameters["@idTipoMuestra"].Value = tipoM;


        //    //cmd.Parameters.Add("@idGermen", SqlDbType.Int);
        //    //cmd.Parameters["@idGermen"].Value = tipoGermen;

        //    //cmd.Parameters.Add("@idAntibiotico", SqlDbType.Int);
        //    //cmd.Parameters["@idAntibiotico"].Value = tipoAntibiotico;

        //    //cmd.Parameters.Add("@idsubitem", SqlDbType.Int);
        //    //cmd.Parameters["@idsubitem"].Value = idsubitem;


        //    //cmd.Parameters.Add("@tipoReporte", SqlDbType.NVarChar);
        //    //cmd.Parameters["@tipoReporte"].Value = s_tipo;


        //    //cmd.Parameters.Add("@grupoEtareo", SqlDbType.Int);
        //    //cmd.Parameters["@grupoEtareo"].Value = int.Parse(ddlGrupoEtareo.SelectedValue);

        //    //cmd.Parameters.Add("@sexo", SqlDbType.Int);
        //    //cmd.Parameters["@sexo"].Value = int.Parse(ddlSexo.SelectedValue);


        //    cmd.Connection = conn;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(Ds);

        //    gvTipoMuestra.DataSource = Ds.Tables[0];
        //    gvTipoMuestra.DataBind();



        //    //////////////////////Solapa microorganismos
        //    ddlTipoMuestra.DataTextField = "Tipo Muestra";
        //    ddlTipoMuestra.DataValueField = "idMuestra";
        //    ddlTipoMuestra.DataSource = Ds.Tables[0];
        //    ddlTipoMuestra.DataBind();
        //    ddlTipoMuestra.Items.Insert(0, new ListItem("--Todas--", "0"));

        //    gvMicroorganismos.DataSource = Ds.Tables[1];
        //    gvMicroorganismos.DataBind();

        //    //  FCLiteralMicroorganismo.Text = mostrarGrafico(1);
        //    /////////////////////////

        //    ////Solapa Antibioticos
        //    ddlTipoMuestraAntibioticos.DataTextField = "Tipo Muestra";
        //    ddlTipoMuestraAntibioticos.DataValueField = "idMuestra";
        //    ddlTipoMuestraAntibioticos.DataSource = Ds.Tables[0];
        //    ddlTipoMuestraAntibioticos.DataBind();
        //    ddlTipoMuestraAntibioticos.Items.Insert(0, new ListItem("--Todas--", "0"));

        //    ddlMicroorganismosAntibioticos.DataTextField = "Microorganismo";
        //    ddlMicroorganismosAntibioticos.DataValueField = "idGermen";
        //    ddlMicroorganismosAntibioticos.DataSource = Ds.Tables[1];
        //    ddlMicroorganismosAntibioticos.DataBind();
        //    ddlMicroorganismosAntibioticos.Items.Insert(0, new ListItem("--Todos--", "0"));



        //    gvAntibiotico.DataSource = Ds.Tables[2];
        //    gvAntibiotico.DataBind();


        //    ///////////////////
        //    gvResultado.DataSource = Ds.Tables[3];
        //    gvResultado.DataBind();

        //    gvAntibioticoResistencia.DataSource = Ds.Tables[4];
        //    gvAntibioticoResistencia.DataBind();

        //    if (tipoAntibiotico > 0)
        //    {
        //        Antibiotico oAnti = new Antibiotico();
        //        oAnti = (Antibiotico)oAnti.Get(typeof(Antibiotico), tipoAntibiotico);
        //        lblResistenciaAntibiotico.Text = "Resistencia de " + oAnti.Descripcion;
        //        lblResistenciaAntibiotico.UpdateAfterCallBack = true;
        //    }



        //}

        //private void MostrarPdf()
        //{
          


        //    Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

        //    ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
        //    encabezado1.Value = oCon.EncabezadoLinea1;

        //    ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
        //    encabezado2.Value = oCon.EncabezadoLinea2;

        //    ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
        //    encabezado3.Value = oCon.EncabezadoLinea3;
        //    ////////////////////////////


        //    ParameterDiscreteValue titulo = new ParameterDiscreteValue();
        //    titulo.Value = "INFORME DE RESULTADOS ";

        //    if (rdbPaciente.SelectedValue == "1") titulo.Value = "INFORME DE RESULTADOS (EMBARAZADAS)";
        //    if (ddlGrupoEtareo.SelectedValue != "0") titulo.Value += " - Grupo Etareo: " + ddlGrupoEtareo.SelectedItem.Text;
        //    if (ddlSexo.SelectedValue != "0") titulo.Value += " - Sexo: " + ddlSexo.SelectedItem.Text;

        //    ParameterDiscreteValue fechaDesde = new ParameterDiscreteValue();
        //    fechaDesde.Value = txtFechaDesde.Value;

        //    ParameterDiscreteValue fechaHasta = new ParameterDiscreteValue();
        //    fechaHasta.Value = txtFechaHasta.Value;


        //    oCr.Report.FileName = "ResultadoPredefinido2.rpt";
        //    oCr.ReportDocument.SetDataSource(GetDatosEstadistica("GV"));
        //    oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
        //    oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
        //    oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
        //    oCr.ReportDocument.ParameterFields[3].CurrentValues.Add(titulo);
        //    oCr.ReportDocument.ParameterFields[4].CurrentValues.Add(fechaDesde);
        //    oCr.ReportDocument.ParameterFields[5].CurrentValues.Add(fechaHasta);
        //    oCr.DataBind();

        //    MemoryStream oStream; // using System.IO
        //    oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("Content-Disposition", "attachment;filename="+ ddlAnalisis.SelectedItem.Text +".pdf");

        //    Response.BinaryWrite(oStream.ToArray());
        //    Response.End();  
        //}
    }
}
