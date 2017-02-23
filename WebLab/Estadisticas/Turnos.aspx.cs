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
using System.Data.SqlClient;
using InfoSoftGlobal;
using Business.Data.Laboratorio;
using CrystalDecisions.Shared;
using System.IO;
using CrystalDecisions.Web;
using System.Text;

namespace WebLab.Estadisticas
{
    public partial class Turnos : System.Web.UI.Page
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
                VerificaPermisos("De Turnos");
                CargarListas();
                txtFechaDesde.Value = DateTime.Now.AddDays(-30).ToShortDateString();
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


        private void CargarListas()
        {
            Utility oUtil = new Utility();
            ///Carga de combos de tipos de servicios
            string m_ssql = "select idTipoServicio, nombre from Lab_TipoServicio WHERE (baja = 0)";
            oUtil.CargarCombo(ddlServicio, m_ssql, "idTipoServicio", "nombre");
            ddlServicio.Items.Insert(0, new ListItem("Todos", "0"));

       
            m_ssql = null;
            oUtil = null;
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            MostrarReporte();

        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtFechaDesde.Value == "")
                args.IsValid = false;
            else
                if (txtFechaHasta.Value == "") args.IsValid = false;
                else args.IsValid = true;
        }

    
        private void MostrarReporte()
        {
            DataTable dt= getDatosEstadisticos("G");

            if (dt.Rows[2][1].ToString() != "0")
            {
                pnlDatos.Visible = true;
                pnlSinDatos.Visible = false;
                gvLista.DataSource = dt;
                gvLista.DataBind();

                FCLiteral.Text = CreateChart1(dt);
            }
            else
            {
                pnlDatos.Visible = false;
                pnlSinDatos.Visible = true;
            }
          


        }

        private DataTable getDatosEstadisticos(string tipo)
        {
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[LAB_EstadisticaTurnos]";

            ///Parametros de fechas           
            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            cmd.Parameters.Add("@fechaDesde", SqlDbType.NVarChar);
            cmd.Parameters["@fechaDesde"].Value = fecha1.ToString("yyyyMMdd");
            cmd.Parameters.Add("@fechaHasta", SqlDbType.NVarChar);
            cmd.Parameters["@fechaHasta"].Value = fecha2.ToString("yyyyMMdd");
            /////
            ///
            ///Parametro Servicio
            cmd.Parameters.Add("@idTipoServicio", SqlDbType.NVarChar);
            cmd.Parameters["@idTipoServicio"].Value = ddlServicio.SelectedValue;
            ///


            ///Parametro tipo de agrupacion
            cmd.Parameters.Add("@agrupado", SqlDbType.NVarChar);
            cmd.Parameters["@agrupado"].Value = tipo;

            cmd.Connection = conn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(Ds);
            return Ds.Tables[0];
        }

        private string CreateChart1(DataTable dataTable)
        {
            string strXML = "<graph caption='Asistencias de turnos dados' subCaption='' showPercentageInLabel='1' pieSliceDepth='10'  decimalPrecision='2' showNames='1'>";

            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count-1; i++)
                {
                    strXML += "<set name='" + dataTable.Rows[i][0].ToString() + "' value='" + dataTable.Rows[i][1].ToString() + "' />";
                }
            }            


            strXML += "</graph>";

            return FusionCharts.RenderChart("../FusionCharts/FCF_Pie3D.swf", "", strXML, "Sales", "400", "200", false, false);
        }

        protected void imgPdf_Click(object sender, ImageClickEventArgs e)
        {
            MostrarPDF();
        }

        private void ExportarExcel()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            gvLista.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(gvLista);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=estadistica_turnos.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        private void MostrarPDF()
        {

            //DataTable dt = new DataTable();
              DataTable dt= getDatosEstadisticos("C");
            if (dt.Rows.Count > 0)
            {
                Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

                ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
                encabezado1.Value = oCon.EncabezadoLinea1;

                ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
                encabezado2.Value = oCon.EncabezadoLinea2;

                ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
                encabezado3.Value = oCon.EncabezadoLinea3;

                ParameterDiscreteValue encabezado4 = new ParameterDiscreteValue();
                encabezado4.Value = "ASISTENCIAS DE TURNOS";

                oCr.Report.FileName = "Turno.rpt";
              
                        
                  

                
                ParameterDiscreteValue subTitulo = new ParameterDiscreteValue();
                subTitulo.Value = txtFechaDesde.Value + " - " + txtFechaHasta.Value;

               

                oCr.ReportDocument.SetDataSource(dt);
                oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
                oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
                oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
                oCr.ReportDocument.ParameterFields[3].CurrentValues.Add(encabezado4);
                oCr.ReportDocument.ParameterFields[4].CurrentValues.Add(subTitulo);
                
                oCr.DataBind();

                oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Estadistica_Turno.pdf");


                //MemoryStream oStream; // using System.IO
                //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("Content-Disposition", "attachment;filename=Estadistica_Turno.pdf");

                //Response.BinaryWrite(oStream.ToArray());
                //Response.End();

            }
            
        }

        protected void imgExcel_Click(object sender, ImageClickEventArgs e)
        {
            ExportarExcel();
        }

       
    }
}
