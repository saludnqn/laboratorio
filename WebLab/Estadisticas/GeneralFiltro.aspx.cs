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
using CrystalDecisions.Shared;
using System.IO;
using System.Data.SqlClient;
using CrystalDecisions.Web;

namespace WebLab.Estadisticas
{
    public partial class GeneralFiltro : System.Web.UI.Page
    {
        public CrystalReportSource oCr = new CrystalReportSource();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            oCr.Report.FileName = "";
            oCr.CacheDuration = 0;
            oCr.EnableCaching = false;
            //Configuracion oC = new Configuracion();
            //oC = (Configuracion)oC.Get(typeof(Configuracion), "IdConfiguracion", 1);
            //oCr.ReportDocument.PrintOptions.PrinterName = oC.NombreImpresora;// System.Configuration.ConfigurationSettings.AppSettings["Impresora"]; 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarListas();                
                txtFechaDesde.Value = DateTime.Now.AddDays(-30).ToShortDateString();
                txtFechaHasta.Value = DateTime.Now.ToShortDateString();

            }
        }

      


        private void CargarListas()
        {
            Utility oUtil = new Utility();
            ///Carga de combos de tipos de servicios
            string m_ssql = "select idTipoServicio, nombre from Lab_TipoServicio WHERE (baja = 0)";
            oUtil.CargarCombo(ddlServicio, m_ssql, "idTipoServicio", "nombre");
            ddlServicio.Items.Insert(0, new ListItem("Todos", "0"));

            CargarArea();

            ///Carga de combos de Efector
            m_ssql = "SELECT idEfector, nombre FROM sys_Efector order by nombre ";
            oUtil.CargarCombo(ddlEfector, m_ssql, "idEfector", "nombre");
            ddlEfector.Items.Insert(0, new ListItem("Todos", "0"));

            ///Carga de combos de Medicos Solicitantes
            m_ssql = "SELECT idProfesional, apellido + ' ' + nombre AS nombre FROM Sys_Profesional ORDER BY apellido, nombre ";
            oUtil.CargarCombo(ddlEspecialista, m_ssql, "idProfesional", "nombre");
            ddlEspecialista.Items.Insert(0, new ListItem("Todos", "0"));

            m_ssql = null;
            oUtil = null;
        }



        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarArea();

            ddlArea.UpdateAfterCallBack = true;
        }

        private void CargarArea()
        {
            Utility oUtil = new Utility();
            ///Carga de combos de areas
            string m_ssql ="";
            if (ddlServicio.SelectedValue =="0")
                m_ssql = "select idArea, nombre from Lab_Area where baja=0  order by nombre";
            else
             m_ssql = "select idArea, nombre from Lab_Area where baja=0 and idTipoServicio=" + ddlServicio.SelectedValue + " order by nombre";
            oUtil.CargarCombo(ddlArea, m_ssql, "idArea", "nombre");
            ddlArea.Items.Insert(0, new ListItem("Todas", "0"));

            m_ssql = null;
            oUtil = null;

        }

        

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Response.Redirect("GeneralReporte.aspx", false);
            Response.Redirect("Reporte.aspx", false);
        }

     

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtFechaDesde.Value == "")
                args.IsValid = false;
            else
                if (txtFechaHasta.Value == "") args.IsValid = false;
                else args.IsValid = true;
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                //Response.Redirect("GeneralReporte.aspx?informe=General&tipo="+rdbTipoInforme.SelectedValue +"&fechaDesde=" + txtFechaDesde.Value + "&fechaHasta=" + txtFechaHasta.Value + "&Agrupado=" + rdbAgrupacion.SelectedValue + "&idTipoServicio="+ ddlServicio.SelectedValue + "&idArea=" + ddlArea.SelectedValue + "&grafico="+ rdbGrafico.SelectedValue  , false);
                Response.Redirect("Reporte.aspx?informe=General&tipo=" + rdbTipoInforme.SelectedValue + "&fechaDesde=" + txtFechaDesde.Value + "&fechaHasta=" + txtFechaHasta.Value + "&Agrupado=" + rdbAgrupacion.SelectedValue + "&idTipoServicio=" + ddlServicio.SelectedValue + "&idArea=" + ddlArea.SelectedValue + "&grafico=" + rdbGrafico.SelectedValue+"&estado="+ rdbEstado.SelectedValue+"&idEfector="+ ddlEfector.SelectedValue+ "&idEspecialista="+ ddlEspecialista.SelectedValue, false);
            }
        }

        protected void rdbTipoInforme_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlArea.Enabled = true; rdbGrafico.Enabled = true;

            if (rdbTipoInforme.SelectedValue == "0") lblDescripcion.Text = "Conteo por Areas: Muestra la cantidad de analisis solicitados, discriminados por areas del laboratorio.";
            if (rdbTipoInforme.SelectedValue == "1") lblDescripcion.Text = "Conteo por Análisis: Muestra la cantidad de analisis solicitados, discriminados por analisis del laboratorio..";
            if (rdbTipoInforme.SelectedValue == "2")
            {
                lblDescripcion.Text = "Conteo por Médico: Muestra la cantidad de protocolos solicitados, discriminados por médico solicitante.";
                ddlArea.Enabled = false;

            }

            if (rdbTipoInforme.SelectedValue == "3")
            {
                lblDescripcion.Text = "Conteo por Efector Solicitante: Muestra la cantidad de protocolos solicitados, discriminados por efector solicitante.";
                ddlArea.Enabled = false;
            }


            if (rdbTipoInforme.SelectedValue == "4") lblDescripcion.Text = "Conteo de Derivaciones: Muestra la cantidad de analisis que fueron derivados, discriminados por efector de derivación.";
            if (rdbTipoInforme.SelectedValue == "5")
            {
                lblDescripcion.Text = "Totalizado: Muestra la cantidad de analisis, cantidad de protocolos y el promedio de analisis por protocolo.";
                ddlArea.Enabled = false;
            }

            if (rdbTipoInforme.SelectedValue == "6") lblDescripcion.Text = "Conteo por Diagnósticos: Muestra la cantidad de protocolos, discriminados por diagnóstico.";

            if (rdbTipoInforme.SelectedValue == "7")
            {///no muestra grafico
                lblDescripcion.Text = "Protocolos por Día: Muestra para cada dia, dentro de un rango de fechas seleccionado, la cantidad de protocolos recepcionados.";
                ddlArea.Enabled = false;
                rdbGrafico.Enabled = false;

            }

            if (rdbTipoInforme.SelectedValue == "8")
            {
                lblDescripcion.Text = "Conteo por Sector/Servicio: Muestra la cantidad de protocolos recepcionados, discriminados por sector/servicio de origen.";
                ddlArea.Enabled = false;

            }

            if (rdbTipoInforme.SelectedValue == "9")
            {
                lblDescripcion.Text = "Ranking por día de la semana: Muestra la cantidad de protocolos recepcionados en cada día de la semana.";
                ddlArea.Enabled = false;

            }
            rdbGrafico.UpdateAfterCallBack=true;
            ddlArea.UpdateAfterCallBack = true;
            lblDescripcion.Visible = true;
            lblDescripcion.UpdateAfterCallBack = true;

        }
    }
}
