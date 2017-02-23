﻿using System;
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
using Business.Data.Laboratorio;
using CrystalDecisions.Shared;
using System.IO;
using System.Data.SqlClient;
using Business;
using System.Text;

namespace WebLab.Estadisticas
{
    public partial class Produccion1 : System.Web.UI.Page
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
                VerificaPermisos("De Produccion");
                txtFechaDesde.Value = DateTime.Now.AddDays(-30).ToShortDateString();
                txtFechaHasta.Value = DateTime.Now.ToShortDateString();
             
                CargarListas();
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
            string m_ssql = "SELECT     idOrigen, nombre  AS nombre FROM         LAB_Origen  ORDER BY nombre";
            oUtil.CargarCheckBox(ChckOrigen, m_ssql, "idOrigen", "nombre");
            for (int i = 0; i < ChckOrigen.Items.Count; i++)
                ChckOrigen.Items[i].Selected = true;

            m_ssql = "SELECT     idArea, nombre  AS nombre FROM         LAB_Area where baja=0  ORDER BY nombre";
            oUtil.CargarCheckBox(ChckArea, m_ssql, "idArea", "nombre");
            for (int i = 0; i < ChckArea.Items.Count; i++)
                ChckArea.Items[i].Selected = true;


            ///Carga de Sectores          
            m_ssql = "SELECT idSectorServicio, prefijo + ' - ' + nombre as nombre  FROM LAB_SectorServicio WHERE (baja = 0) order by nombre";
            oUtil.CargarListBox(lstSector, m_ssql, "idSectorServicio", "nombre");
            for (int i = 0; i < lstSector.Items.Count; i++)            
                lstSector.Items[i].Selected = true;
            

            m_ssql = null;
            oUtil = null;
        }
        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) ImprimirReporte();
        }

        private void ImprimirReporte()
        {


            string informe = "Produccion2.rpt";
            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

            ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
            encabezado1.Value = oCon.EncabezadoLinea1;

            ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
            encabezado2.Value = oCon.EncabezadoLinea2;

            ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
            encabezado3.Value = oCon.EncabezadoLinea3;


            ParameterDiscreteValue rangeFechas = new ParameterDiscreteValue();
            rangeFechas.Value = txtFechaDesde.Value + " - " + txtFechaHasta.Value;



            oCr.Report.FileName = informe;
            oCr.ReportDocument.SetDataSource(GetDataSet());
            oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
            oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
            oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
            oCr.ReportDocument.ParameterFields[3].CurrentValues.Add(rangeFechas);
            oCr.DataBind();


            oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ProduccionLaboratorio.pdf");
            //MemoryStream oStream; // using System.IO
            //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment;filename=ProduccionLaboratorio.pdf");

            //Response.BinaryWrite(oStream.ToArray());
            //Response.End();


        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtFechaDesde.Value == "")
                args.IsValid = false;
            else
                if (txtFechaHasta.Value == "") args.IsValid = false;
                else args.IsValid = true;
        }
        public DataTable GetDataSet()
        {       
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[LAB_EstadisticaProduccion]";

            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            cmd.Parameters.Add("@fechaDesde", SqlDbType.NVarChar);
            cmd.Parameters["@fechaDesde"].Value = fecha1.ToString("yyyyMMdd");
            cmd.Parameters.Add("@fechaHasta", SqlDbType.NVarChar);
            cmd.Parameters["@fechaHasta"].Value = fecha2.ToString("yyyyMMdd");

            cmd.Parameters.Add("@idArea", SqlDbType.NVarChar);
            cmd.Parameters["@idArea"].Value = getListaAreas();

            cmd.Parameters.Add("@idOrigen", SqlDbType.NVarChar);
            cmd.Parameters["@idOrigen"].Value = getListaOrigen();

            cmd.Parameters.Add("@idSector", SqlDbType.NVarChar);
            cmd.Parameters["@idSector"].Value = getListaSectores();

            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Ds);

            return Ds.Tables[0];
        }
        private object getListaSectores()
        {
            string m_lista = "";
            for (int i = 0; i < lstSector.Items.Count; i++)
            {
                if (lstSector.Items[i].Selected)
                {
                    if (m_lista == "")
                        m_lista = lstSector.Items[i].Value;
                    else
                        m_lista += "," + lstSector.Items[i].Value;
                }

            }
            return m_lista;
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

        private string getListaAreas()
        {
            string lista = "";
            for (int i = 0; i < this.ChckArea.Items.Count; i++)
            {
                if (ChckArea.Items[i].Selected)
                {
                    if (lista == "")
                        lista = ChckArea.Items[i].Value;
                    else
                        lista += ","+ChckArea.Items[i].Value;
                }

            }
            return lista;
        }

        protected void imgExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (Page.IsValid)
            {
                dataTableAExcel(GetDataSet(), "ProduccionLaboratorio");
            }
        }

        private void dataTableAExcel(DataTable tabla, string nombreArchivo)
        {
            if (tabla.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                HtmlForm form = new HtmlForm();
                GridView dg = new GridView();
                dg.EnableViewState = false;
                dg.DataSource = tabla;
                dg.DataBind();
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(dg);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + nombreArchivo + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
            }
        }

        protected void lnkMarcar_Click(object sender, EventArgs e)
        {
            MarcarSeleccionados(true);
        }
        private void MarcarSeleccionados(bool p)
        {
            for (int i = 0; i < ChckOrigen.Items.Count; i++)
                ChckOrigen.Items[i].Selected = p;

        }

        protected void lnkDesmarcar_Click(object sender, EventArgs e)
        {
            MarcarSeleccionados(false);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            MarcarAreasSeleccionados(true);
        }

        private void MarcarAreasSeleccionados(bool p)
        {
            for (int i = 0; i < ChckArea.Items.Count; i++)
                ChckArea.Items[i].Selected = p;

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            MarcarAreasSeleccionados(false);
        }

        protected void cvOrigen_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool hay = false;
            for (int i = 0; i < ChckOrigen.Items.Count; i++)
            { if (ChckOrigen.Items[i].Selected) { hay = true; break; } }

           
                args.IsValid = hay;
                
        }

        protected void cvOrigen0_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool hay = false;
            for (int i = 0; i < ChckArea.Items.Count; i++)
            { if (ChckArea.Items[i].Selected) { hay = true; break; } }


            args.IsValid = hay;
        }
      
    }
}
