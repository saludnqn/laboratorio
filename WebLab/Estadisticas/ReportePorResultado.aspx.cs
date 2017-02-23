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

namespace WebLab.Estadisticas
{
    public partial class ReportePorResultado : System.Web.UI.Page
    {
        public CrystalReportSource oCr = new CrystalReportSource();
        int suma1 = 0;
        int grupo1 = 0; int grupo2 = 0; int grupo3 = 0; int grupo4=0; int grupo5=0; int grupo6=0; int grupo7=0; int grupo8=0; int grupo9=0; int grupo10=0;
        int masc = 0; int fem = 0; int ind = 0;
        
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

            m_ssql = "SELECT     idOrigen, nombre  AS nombre FROM         LAB_Origen  ORDER BY nombre";
            oUtil.CargarCheckBox(ChckOrigen, m_ssql, "idOrigen", "nombre");
            for (int i = 0; i < ChckOrigen.Items.Count; i++)
                ChckOrigen.Items[i].Selected = true;
            m_ssql = null;
            oUtil = null;

        }
        private void CargarAnalisis()
        {
            Utility oUtil = new Utility();
            string m_condicion = " and 1=1 ";
            if (ddlArea.SelectedValue != "0") m_condicion = " and idArea=" + ddlArea.SelectedValue;

            ///Carga de combos de tipos de servicios
            string m_ssql = "SELECT     idItem, nombre + ' (' + codigo + ')' AS nombre " +
            " FROM         LAB_Item " +
            " WHERE     (idTipoResultado = 3) and disponible=1 AND (idEfectorDerivacion = idEfector) AND (baja = 0) " + m_condicion  + //AND (tipo = 'P') 
            " ORDER BY nombre";

            oUtil.CargarCombo(ddlAnalisis, m_ssql, "idItem", "nombre");
            ddlAnalisis.Items.Insert(0, new ListItem("--Seleccione--", "0"));

          

            m_ssql = null;
            oUtil = null;
        }

        private void MostrarReporteGeneral()
        {
            gvEstadistica.DataSource = GetDatosEstadistica("GV");
            gvEstadistica.DataBind();
            HFTipoMuestra.Value = getValoresGrilla();
            if (gvEstadistica.Rows.Count == 0) Response.Redirect("SinDatos.aspx?Desde=ReportePorResultado.aspx", false);
            else
            {
                lblAnalisis.Text = ddlAnalisis.SelectedItem.Text;
                pnlResultado.Visible = true;
            }
        }

        private string getValoresGrilla()
        {
            string s_valores = "";

            for (int i = 0; i < gvEstadistica.Rows.Count; i++)
            {
                string s_nombre = gvEstadistica.Rows[i].Cells[0].Text.Replace(";", "");
                s_nombre = s_nombre.Replace("&#", "");
                if (s_valores == "")
                    s_valores = "name='" + s_nombre + "' value='" + gvEstadistica.Rows[i].Cells[1].Text + "'";
                else
                    s_valores += ";" + "name='" + s_nombre + "' value='" + gvEstadistica.Rows[i].Cells[1].Text + "'";
            }

            return s_valores;
        }



        protected void lnkPdf_Click(object sender, EventArgs e)
        {

        }



        protected void cvFechas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (getListaOrigen() == "") { args.IsValid = false; cvFechas.ErrorMessage = "Seleccione un origen"; }
                else { 


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
            }
            catch (Exception ex)
            {
                string exception = "";
                //while (ex != null)
                //{
                    exception = ex.Message + "<br>";

                //} 
            args.IsValid = false;
            }

        }


        private DataTable GetDatosEstadistica(string s_tipo)
        {
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "[LAB_EstadisticaPorResultados2]";



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


            cmd.Parameters.Add("@grupoEtareo", SqlDbType.Int);
            cmd.Parameters["@grupoEtareo"].Value = int.Parse(ddlGrupoEtareo.SelectedValue);

            cmd.Parameters.Add("@sexo", SqlDbType.Int);
            cmd.Parameters["@sexo"].Value = int.Parse(ddlSexo.SelectedValue);

            cmd.Parameters.Add("@idOrigen", SqlDbType.NVarChar);
            cmd.Parameters["@idOrigen"].Value = getListaOrigen();

            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Ds);
            return Ds.Tables[0];

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
                    ImageButton CmdPdf = (ImageButton)e.Row.Cells[15].Controls[1];
                    CmdPdf.CommandArgument = ddlAnalisis.SelectedValue + "~" + e.Row.Cells[0].Text; ///Codigo1 + ";" + codigo2
                    CmdPdf.CommandName = "PDF";
                    CmdPdf.ToolTip = "Ver Pacientes";

                    ImageButton CmdExcel = (ImageButton)e.Row.Cells[16].Controls[1];
                    CmdExcel.CommandArgument = ddlAnalisis.SelectedValue + "~" + e.Row.Cells[0].Text; ///Codigo1 + ";" + codigo2
                    CmdExcel.CommandName = "EXCEL";
                    CmdExcel.ToolTip = "Ver Pacientes";
              
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
            if (e.CommandName == "PDF")
                InformePacientes(e.CommandArgument.ToString(),"PDF");
            if (e.CommandName == "EXCEL")
                InformePacientes(e.CommandArgument.ToString(),"EXCEL");

        }

        private void InformePacientes(string p, string reporte)
        {
            Utility oUtil = new Utility();
            string[] arr = p.ToString().Split(("~").ToCharArray());


            string m_analisis = arr[0].ToString();
            string m_resultado =oUtil.RemoverSignosAcentos( arr[1].ToString());

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
            titulo.Value = "INFORME DE PACIENTES ";

            if (rdbPaciente.SelectedValue == "1") titulo.Value = "INFORME DE PACIENTES EMBARAZADAS";
            
            if (ddlGrupoEtareo.SelectedValue != "0") titulo.Value += " - Grupo Etareo: " + ddlGrupoEtareo.SelectedItem.Text;
            if (ddlSexo.SelectedValue != "0") titulo.Value += " - Sexo: " + ddlSexo.SelectedItem.Text;

            ParameterDiscreteValue fechaDesde = new ParameterDiscreteValue();
            fechaDesde.Value = txtFechaDesde.Value;

            ParameterDiscreteValue fechaHasta = new ParameterDiscreteValue();
            fechaHasta.Value = txtFechaHasta.Value;


            oCr.Report.FileName = "Pacientes.rpt";
            oCr.ReportDocument.SetDataSource(GetDataPacientes(m_analisis, m_resultado));
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
                dg.DataSource = GetDataPacientes(m_analisis, m_resultado);
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

        private DataTable GetDataPacientes(string m_analisis, string m_resultado)
        {
            string m_strCondicion="";
            string m_codigopaciente = " '' as codigoPaciente";

            Item oItem = new Item();
            oItem = (Item)oItem.Get(typeof(Item), int.Parse( ddlAnalisis.SelectedValue));
            if (oItem != null)
            {
                if (oItem.CodificaHiv) m_codigopaciente = " [dbo].[BuscarPaciente] (Pa.idpaciente, 1) as codigoPaciente ";
            }

            if (rdbPaciente.SelectedValue == "1")
                m_strCondicion += " and (PD.iddiagnostico = 11999)  ";

            if (ddlGrupoEtareo.SelectedValue != "0")
            {
                if (ddlGrupoEtareo.SelectedValue == "1") m_strCondicion += " and P.unidadEdad>0";
                if (ddlGrupoEtareo.SelectedValue == "2") m_strCondicion += " and P.edad=1  and P.unidadedad=0";
                if (ddlGrupoEtareo.SelectedValue == "3") m_strCondicion += " and P.edad>=2 and P.edad<=4 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "4") m_strCondicion += " and P.edad>=5 and P.edad<=9 and P.unidadedad=0    ";
                if (ddlGrupoEtareo.SelectedValue == "5") m_strCondicion += " and P.edad>=10 and P.edad<=14 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "6") m_strCondicion += " and P.edad>=15 and P.edad<=24 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "7") m_strCondicion += " and P.edad>=25 and P.edad<=34 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "8") m_strCondicion += " and P.edad>=35 and P.edad<=44 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "9") m_strCondicion += " and P.edad>=45 and P.edad<=64 and P.unidadedad=0   ";
                if (ddlGrupoEtareo.SelectedValue == "10") m_strCondicion += " and P.edad>=65  and P.unidadedad=0  ";

            }

            if (ddlSexo.SelectedValue != "0")
            {
                if (ddlSexo.SelectedValue == "1")
                    m_strCondicion += " and P.sexo='F'";
                if (ddlSexo.SelectedValue == "2")
                    m_strCondicion += " and P.sexo='M' ";
            }

            m_strCondicion += " and P.idOrigen in (" + getListaOrigen() + ")";

            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);


            string m_strSQL = " SELECT I.nombre AS ANALISIS, DP.resultadoCar AS RESULTADO, Pa.numeroDocumento, Pa.apellido, Pa.nombre, CONVERT(VARCHAR(10), Pa.fechaNacimiento, " +
                            " 103) AS FECHANACIMIENTO, Pa.referencia AS domicilio, CONVERT(varchar(10), P.fecha, 103) AS fecha, P.edad, dbo.NumeroProtocolo(P.idProtocolo) as numeroProtocolo, " + m_codigopaciente + ", O.nombre as Origen " +
                            " FROM LAB_DetalleProtocolo AS DP " +
                            " INNER JOIN LAB_Protocolo AS P ON DP.idProtocolo = P.idProtocolo " +
                            " INNER JOIN LAB_Item AS I ON DP.idSUBItem = I.idItem " +
                            " INNER JOIN Sys_Paciente AS Pa ON P.idPaciente = Pa.idPaciente" +
                            " left JOIN vta_LAB_Embarazadas AS PD ON PD.idProtocolo = P.idProtocolo " +
                            " INNER JOIN LAB_Origen as O on O.idOrigen= P.idOrigen " +
                            " WHERE (I.idItem=" + m_analisis + ") AND (DP.resultadoCar = '" + m_resultado + "') AND (P.fecha >= '" + fecha1.ToString("yyyyMMdd") + "') AND (P.fecha <= '" + fecha2.ToString("yyyyMMdd") + "') and " +
                            " ( DP.conresultado=1) " + m_strCondicion +
                            " order by P.fecha  ";

   
            
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
            titulo.Value = "INFORME DE RESULTADOS ";

            if (rdbPaciente.SelectedValue == "1") titulo.Value = "INFORME DE RESULTADOS (EMBARAZADAS)";
            if (ddlGrupoEtareo.SelectedValue != "0") titulo.Value += " - Grupo Etareo: " + ddlGrupoEtareo.SelectedItem.Text;
            if (ddlSexo.SelectedValue != "0") titulo.Value += " - Sexo: " + ddlSexo.SelectedItem.Text;

            ParameterDiscreteValue fechaDesde = new ParameterDiscreteValue();
            fechaDesde.Value = txtFechaDesde.Value;

            ParameterDiscreteValue fechaHasta = new ParameterDiscreteValue();
            fechaHasta.Value = txtFechaHasta.Value;


            oCr.Report.FileName = "ResultadoPredefinido2.rpt";
            oCr.ReportDocument.SetDataSource(GetDatosEstadistica("GV"));
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

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAnalisis();
        }
    }
}
