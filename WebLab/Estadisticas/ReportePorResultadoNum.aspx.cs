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
using System.Data.SqlClient;
using Business;
using System.Text;
using System.IO;
using Business.Data;
using CrystalDecisions.Shared;
using Business.Data.Laboratorio;

namespace WebLab.Estadisticas
{
    public partial class ReportePorResultadoNum : System.Web.UI.Page
    {
        public CrystalReportSource oCr = new CrystalReportSource();
        int suma1 = 0;
        int suma2 = 0;
        int suma3 = 0; int suma4 = 0; int suma5 = 0; int suma6 = 0; int suma7 = 0; int suma8 = 0; int suma9 = 0; int suma10 = 0; int suma11 = 0;



        protected void Page_PreInit(object sender, EventArgs e)
        {

            oCr.CacheDuration = 0;
            oCr.EnableCaching = false;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarListas();
                txtFechaDesde.Value = DateTime.Now.AddDays(-7).ToShortDateString();
                txtFechaHasta.Value = DateTime.Now.ToShortDateString();
            }
        }
        private void CargarListas()
        {
            Utility oUtil = new Utility();
            ///Carga de combos de tipos de servicios
            string m_ssql = "SELECT  idArea, nombre from LAB_Area where baja=0 ORDER BY nombre";

            oUtil.CargarCombo(ddlArea, m_ssql, "idArea", "nombre");
            ddlArea.Items.Insert(0, new ListItem("--Todas--", "0"));


            CargarAnalisis();
            m_ssql = null;
            oUtil = null;
        }
        private void MostrarReporteGeneral()
        {
            lblAnalisis.Text = ddlAnalisis.SelectedItem.Text;
            gvEstadistica.DataSource = GetDatosEstadistica("GV");
            gvEstadistica.DataBind();
            if (gvEstadistica.Rows.Count == 0) Response.Redirect("SinDatos.aspx?Desde=ReportePorResultadoNum.aspx", false);
            else pnlResultado.Visible = true;
        }
        
        protected void lnkPdf_Click(object sender, EventArgs e)
        {
                    }

        private DataTable GetDatosEstadistica(string s_tipo)
        {
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "[LAB_EstadisticaPorResultadosNum]";



            ///Parametros de fechas           
            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            cmd.Parameters.Add("@fechaDesde", SqlDbType.NVarChar);
            cmd.Parameters["@fechaDesde"].Value = fecha1.ToString("yyyyMMdd");
            cmd.Parameters.Add("@fechaHasta", SqlDbType.NVarChar);
            cmd.Parameters["@fechaHasta"].Value = fecha2.ToString("yyyyMMdd");
            /////


            cmd.Parameters.Add("@idAnalisis", SqlDbType.NVarChar);
            cmd.Parameters["@idAnalisis"].Value =int.Parse( ddlAnalisis.SelectedValue);
            
            cmd.Parameters.Add("@tipoPaciente", SqlDbType.NVarChar);
            cmd.Parameters["@tipoPaciente"].Value =rdbPaciente.SelectedValue;

            cmd.Parameters.Add("@valorDesde", SqlDbType.NVarChar);
            cmd.Parameters["@valorDesde"].Value = txtValorDesde.Text;
            cmd.Parameters.Add("@valorHasta", SqlDbType.NVarChar);
            cmd.Parameters["@valorHasta"].Value = txtValorHasta.Text;

            cmd.Parameters.Add("@imprimir", SqlDbType.NVarChar);
            cmd.Parameters["@imprimir"].Value = s_tipo;


            cmd.Parameters.Add("@grupoEtareo", SqlDbType.Int);
            cmd.Parameters["@grupoEtareo"].Value = int.Parse(ddlGrupoEtareo.SelectedValue);

            cmd.Parameters.Add("@sexo", SqlDbType.Int);
            cmd.Parameters["@sexo"].Value = int.Parse(ddlSexo.SelectedValue);

            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Ds);
            return Ds.Tables[0];

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


            try
            {
             

                if ((e.Row.RowType == DataControlRowType.DataRow))
                {

                    ImageButton CmdPdf = (ImageButton)e.Row.Cells[12].Controls[1];
                    CmdPdf.CommandArgument = ddlAnalisis.SelectedValue + "~" + e.Row.Cells[0].Text; ///Codigo1 + ";" + codigo2
                    CmdPdf.CommandName = "PDF";
                    CmdPdf.ToolTip = "Ver Pacientes";

                    ImageButton CmdExcel = (ImageButton)e.Row.Cells[13].Controls[1];
                    CmdExcel.CommandArgument = ddlAnalisis.SelectedValue + "~" + e.Row.Cells[0].Text; ///Codigo1 + ";" + codigo2
                    CmdExcel.CommandName = "EXCEL";
                    CmdExcel.ToolTip = "Ver Pacientes";


                    if (e.Row.Cells[1].Text != "&nbsp;") suma1 += int.Parse(e.Row.Cells[1].Text);                    
                    if (e.Row.Cells[2].Text != "&nbsp;") suma2 += int.Parse(e.Row.Cells[2].Text);                    
                    if (e.Row.Cells[3].Text != "&nbsp;") suma3 += int.Parse(e.Row.Cells[3].Text);
                    if (e.Row.Cells[4].Text != "&nbsp;") suma4 += int.Parse(e.Row.Cells[4].Text);
                    if (e.Row.Cells[5].Text != "&nbsp;") suma5 += int.Parse(e.Row.Cells[5].Text);
                    if (e.Row.Cells[6].Text != "&nbsp;") suma6 += int.Parse(e.Row.Cells[6].Text);
                    if (e.Row.Cells[7].Text != "&nbsp;") suma7 += int.Parse(e.Row.Cells[7].Text);
                    if (e.Row.Cells[8].Text != "&nbsp;") suma8 += int.Parse(e.Row.Cells[8].Text);
                    if (e.Row.Cells[9].Text != "&nbsp;") suma9 += int.Parse(e.Row.Cells[9].Text);
                    if (e.Row.Cells[10].Text != "&nbsp;") suma10 += int.Parse(e.Row.Cells[10].Text);
                    if (e.Row.Cells[11].Text != "&nbsp;") suma11 += int.Parse(e.Row.Cells[11].Text);
                    
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "TOTAL CASOS";
                    e.Row.Cells[1].Text = suma1.ToString();
                    e.Row.Cells[2].Text = suma2.ToString();
                    e.Row.Cells[3].Text = suma3.ToString();
                    e.Row.Cells[4].Text = suma4.ToString();
                    e.Row.Cells[5].Text = suma5.ToString();
                    e.Row.Cells[6].Text = suma6.ToString();
                    e.Row.Cells[7].Text = suma7.ToString();
                    e.Row.Cells[8].Text = suma8.ToString();
                    e.Row.Cells[9].Text = suma9.ToString();
                    e.Row.Cells[10].Text = suma10.ToString();
                    e.Row.Cells[11].Text = suma11.ToString();

                }

            }
            catch
            { }

        }





        protected void imgExcel_Click(object sender, ImageClickEventArgs e)
        {

            ExportarExcel();

        }

        private void ExportarExcel()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            gvEstadistica.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(gvEstadistica);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=reporteresultados.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void ddlAnalisis_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEstadistica.DataSource = GetDatosEstadistica("GV");
            gvEstadistica.DataBind();
        }

        protected void gvEstadistica_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Pacientes")
            //    InformePacientes(e.CommandArgument.ToString());

            if (e.CommandName == "PDF")
                InformePacientes(e.CommandArgument.ToString(), "PDF");
            if (e.CommandName == "EXCEL")
                InformePacientes(e.CommandArgument.ToString(), "EXCEL");

        }

        private void InformePacientes(string p, string reporte)
        {
            string[] arr = p.ToString().Split(("~").ToCharArray());

            string m_analisis = arr[0].ToString();
            string m_sexo= arr[1].ToString();

            Item oItem = new Item();
            oItem =(Item) oItem.Get(typeof(Item),int.Parse( m_analisis));
            
            if (reporte == "PDF")
            {
                Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

                ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
                encabezado1.Value = oCon.EncabezadoLinea1;

                ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
                encabezado2.Value = oCon.EncabezadoLinea2;

                ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
                encabezado3.Value = oCon.EncabezadoLinea3;
                ////////////////////////////


                ParameterDiscreteValue titulo = new ParameterDiscreteValue();
                string s_unidad="";
                string s_valores ="REPORTE ESTADISTICO " +  oItem.Descripcion;
                if (oItem.IdUnidadMedida > 0)
                {
                    UnidadMedida oUnidad = new UnidadMedida();
                    oUnidad = (UnidadMedida)oUnidad.Get(typeof(UnidadMedida), oItem.IdUnidadMedida);
                    s_unidad= oUnidad.Nombre;
                }
                if (txtValorDesde.Text != "") s_valores += " - Valores de Desde : " + txtValorDesde.Text + " " + s_unidad;
                if (txtValorHasta.Text != "") s_valores += " - Valores Hasta : " + txtValorHasta.Text + " " + s_unidad;
                if (rdbPaciente.SelectedValue == "1") s_valores += " Pacientes Embarazadas";

                titulo.Value = s_valores;

                ParameterDiscreteValue fechaDesde = new ParameterDiscreteValue();
                fechaDesde.Value = txtFechaDesde.Value;

                ParameterDiscreteValue fechaHasta = new ParameterDiscreteValue();
                fechaHasta.Value = txtFechaHasta.Value;


                oCr.Report.FileName = "Pacientes.rpt";
                oCr.ReportDocument.SetDataSource(GetDataPacientes(m_analisis, m_sexo.Substring(0,1), txtValorDesde.Text, txtValorHasta.Text));
                oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
                oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
                oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
                oCr.ReportDocument.ParameterFields[3].CurrentValues.Add(titulo);
                oCr.ReportDocument.ParameterFields[4].CurrentValues.Add(fechaDesde);
                oCr.ReportDocument.ParameterFields[5].CurrentValues.Add(fechaHasta);
                oCr.DataBind();

                oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Pacientes.pdf");
                //MemoryStream oStream; // using System.IO
                //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("Content-Disposition", "attachment;filename=Pacientes.pdf");

                //Response.BinaryWrite(oStream.ToArray());
                //Response.End();
            }
            if (reporte == "EXCEL")
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                Page page = new Page();
                HtmlForm form = new HtmlForm();
                GridView dg = new GridView();
                dg.EnableViewState = false;
                dg.DataSource = GetDataPacientes(m_analisis, m_sexo.Substring(0, 1), txtValorDesde.Text, txtValorHasta.Text);
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
                Response.AddHeader("Content-Disposition", "attachment;filename=DetallePacientes.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
            }
        }

        private DataTable GetDataPacientes(string m_analisis,string m_sexo, string m_resultadoDesde, string m_resultadoHasta)
        {

            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            string m_condicion = "";


            string m_codigopaciente = " '' as codigoPaciente";

            Item oItem = new Item();
            oItem = (Item)oItem.Get(typeof(Item), int.Parse(ddlAnalisis.SelectedValue));
            if (oItem != null)
            {
                if (oItem.CodificaHiv) m_codigopaciente = " [dbo].[BuscarPaciente] (Pa.idpaciente, 1) as codigoPaciente ";
            }


            switch (m_sexo) { case "M":m_condicion = " and Pa.idSexo=3"; break; case "F":m_condicion = " and Pa.idSexo=2"; break; case "I":m_condicion = " and Pa.idSexo=1"; break; }

            if (m_resultadoDesde != "") m_condicion += " AND DP.resultadoNum>=" + decimal.Parse(m_resultadoDesde, System.Globalization.CultureInfo.InvariantCulture);
            if (m_resultadoHasta != "") m_condicion += " and  DP.resultadoNum<=" + decimal.Parse(m_resultadoHasta, System.Globalization.CultureInfo.InvariantCulture);

            if (rdbPaciente.SelectedValue == "1")
                m_condicion += " and (PD.iddiagnostico = 11999)  ";

            if (ddlGrupoEtareo.SelectedValue != "0")
            {
                if (ddlGrupoEtareo.SelectedValue == "1") m_condicion += " and P.unidadEdad>0";
                if (ddlGrupoEtareo.SelectedValue == "2") m_condicion += " and P.edad=1  and P.unidadedad=0";
                if (ddlGrupoEtareo.SelectedValue == "3") m_condicion += " and P.edad>=2 and P.edad<=4 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "4") m_condicion += " and P.edad>=5 and P.edad<=9 and P.unidadedad=0    ";
                if (ddlGrupoEtareo.SelectedValue == "5") m_condicion += " and P.edad>=10 and P.edad<=14 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "6") m_condicion += " and P.edad>=15 and P.edad<=24 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "7") m_condicion += " and P.edad>=25 and P.edad<=34 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "8") m_condicion += " and P.edad>=35 and P.edad<=44 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "9") m_condicion += " and P.edad>=45 and P.edad<=64 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "10") m_condicion += " and P.edad>=65  and P.unidadedad=0  ";

            }


            string m_strSQL = @" SELECT I.nombre AS ANALISIS, CASE I.formatoDecimal
 WHEN 0 then CONVERT(int,DP.resultadoNum) 
 WHEN 1 then convert(decimal(19,1), DP.resultadoNum) 
 WHEN 2 then convert(decimal(19,2), DP.resultadoNum) 
 WHEN 3 then convert(decimal(19,3), DP.resultadoNum) 
 WHEN 4 then convert(decimal(19,4), DP.resultadoNum) 
  end 
 AS RESULTADO,  Pa.numeroDocumento, Pa.apellido, Pa.nombre, CONVERT(VARCHAR(10), Pa.fechaNacimiento, 103) AS FECHANACIMIENTO, Pa.referencia AS domicilio, CONVERT(varchar(10), P.fecha, 103) AS fecha, dbo.NumeroProtocolo(P.idProtocolo) as numero ,"+ m_codigopaciente+
 @"                            FROM LAB_DetalleProtocolo AS DP 
                             INNER JOIN LAB_Protocolo AS P ON DP.idProtocolo = P.idProtocolo 
                             INNER JOIN LAB_Item AS I ON DP.idsubItem = I.idItem
                             INNER JOIN Sys_Paciente AS Pa ON P.idPaciente = Pa.idPaciente
                             left JOIN LAB_ProtocoloDiagnostico AS PD ON PD.idProtocolo = P.idProtocolo 
                             WHERE P.baja=0 and  I.iditem=" + m_analisis +  m_condicion+ 
             " AND (P.fecha >= '" + fecha1.ToString("yyyyMMdd") + "') AND (P.fecha <= '" + fecha2.ToString("yyyyMMdd") + "') and " +
                             " ( DP.conresultado=1)  order by P.fecha  ";

              //(PD.iddiagnostico = 11999) AND
             //convert(decimal(19,4), '+ @valorDesde +'))  '
            
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);


            DataTable data = Ds.Tables[0];
            return data;
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            MostrarReporteGeneral();
        }

        protected void imgPdf_Click(object sender, ImageClickEventArgs e)
        {
            MostrarPdf();
        }

        private void MostrarPdf()
        {
          


            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

            ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
            encabezado1.Value = oCon.EncabezadoLinea1;

            ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
            encabezado2.Value = oCon.EncabezadoLinea2;

            ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
            encabezado3.Value = oCon.EncabezadoLinea3;
            ////////////////////////////


            ParameterDiscreteValue titulo = new ParameterDiscreteValue();


            string s_valores = ddlAnalisis.SelectedItem.Text;
            if (txtValorDesde.Text != "")
                s_valores += " Desde : " + txtValorDesde.Text;

            if (txtValorHasta.Text != "")
                s_valores += " Hasta : " + txtValorHasta.Text;

            if (rdbPaciente.SelectedValue == "1") s_valores += " Pacientes Embarazadas";

            titulo.Value = s_valores;

         

            ParameterDiscreteValue fechaDesde = new ParameterDiscreteValue();
            fechaDesde.Value = txtFechaDesde.Value;

            ParameterDiscreteValue fechaHasta = new ParameterDiscreteValue();
            fechaHasta.Value = txtFechaHasta.Value;


            oCr.Report.FileName = "ResultadoNumerico2.rpt";
            oCr.ReportDocument.SetDataSource(GetDatosEstadistica("CR"));
            oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
            oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
            oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
            oCr.ReportDocument.ParameterFields[3].CurrentValues.Add(titulo);
            oCr.ReportDocument.ParameterFields[4].CurrentValues.Add(fechaDesde);
            oCr.ReportDocument.ParameterFields[5].CurrentValues.Add(fechaHasta);
            oCr.DataBind();
            oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "reporteresultados.pdf");

            //MemoryStream oStream; // using System.IO
            //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment;filename=reporteresultados.pdf");

            //Response.BinaryWrite(oStream.ToArray());
            //Response.End();  
        }

        protected void cvFechas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (txtFechaDesde.Value == "")
                    args.IsValid = false;
                else
                {
                    args.IsValid = true;
                    DateTime f1 = DateTime.Parse(txtFechaDesde.Value);
                    if (txtFechaHasta.Value == "")
                        args.IsValid = false;
                    else
                    {
                        DateTime f2 = DateTime.Parse(txtFechaHasta.Value);
                        args.IsValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = "";
                //while (ex != null)
                //{
                //    exception = ex.Message + "<br>";

                //} 
                args.IsValid = false;
            }


        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAnalisis();
        }

        private void CargarAnalisis()
        {
            Utility oUtil = new Utility();
            string m_condicion = " and 1=1 ";
            if (ddlArea.SelectedValue != "0") m_condicion = " and idArea=" + ddlArea.SelectedValue;

            ///Carga de combos de tipos de servicios
            string m_ssql = "SELECT     idItem, nombre + ' (' + codigo + ')' AS nombre " +
            " FROM         LAB_Item " +
            " WHERE     (idTipoResultado = 1) and disponible=1 AND (idEfectorDerivacion = idEfector) AND (baja = 0) " + m_condicion + //AND (tipo = 'P') 
            " ORDER BY nombre";

            oUtil.CargarCombo(ddlAnalisis, m_ssql, "idItem", "nombre");
            ddlAnalisis.Items.Insert(0, new ListItem("--Seleccione--", "0"));



            m_ssql = null;
            oUtil = null;
        }
    }
}
