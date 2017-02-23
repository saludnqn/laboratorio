using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.Data.Laboratorio;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.Web;
using InfoSoftGlobal;
using Business.Data;
using CrystalDecisions.Shared;
using System.IO;

namespace WebLab.Resultados
{
    public partial class AntecedentesAnalisisView : System.Web.UI.Page
    {
      
        Protocolo oProtocolo = new Protocolo();

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
                DataTable dt = new DataTable();
                dt = LlenarDatos();
                if (dt.Rows.Count > 0)
                { 
                    gvHistorico.DataSource = dt;
                    gvHistorico.DataBind();
                    Item oItem = new Item();
                    oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["idAnalisis"].ToString()));
                    
                    Paciente oPaciente = new Paciente();
                    oPaciente = (Paciente)oPaciente.Get(typeof(Paciente), int.Parse(Request["idPaciente"].ToString()));
                    lblPaciente.Text = oPaciente.Apellido + " " + oPaciente.Nombre;

                    if (oItem.IdTipoResultado == 1)
                    {
                        if (coincideUnidadMedida(dt))
                        {
                            string valorminimo = Math.Round(oItem.ValorMinimo, 0).ToString();
                            FCLiteral.Text = CreateChart(dt, oItem.Nombre + " [" + oItem.Codigo + "]", valorminimo);
                        }
                    }
                }                
            }
        }
        private bool coincideUnidadMedida(DataTable dt)
        {
            bool hay = true;
            string unidad = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                        unidad = dt.Rows[i][6].ToString();
                    else
                    {
                        if (unidad != dt.Rows[i][6].ToString())
                            hay = false; break;
                    }
                }
            }
            return hay;

        }

        private string CreateChart(DataTable dt, string nombre, string valorminino)
        {
            string strXML = "<graph caption='" + nombre.ToUpper() + "'  xAxisName='Protocolo' yAxisMinValue='" + valorminino + "' yAxisName='Resultado' decimalPrecision='2' formatNumberScale='1' showNames='1' " +
                " showValues='0' showAlternateHGridColor='1'  AlternateHGridColor='ff5904' divLineColor='ff5904' divLineAlpha='20' alternateHGridAlpha='5'>";
                        
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strXML += "<set name='" + dt.Rows[i][2].ToString() + "' value='" + dt.Rows[i][4].ToString().Replace(",", ".") + "' hoverText='" + dt.Rows[i][2].ToString() + "' />";
            }           

            strXML += "</graph>";
            return FusionCharts.RenderChart("../FusionCharts/FCF_Line.swf", "", strXML, "Sales", "700", "250", false, false);
        }


        protected void imgPdf_Click(object sender, ImageClickEventArgs e)
        {
            ExportarPDF();
        }

        private void ExportarPDF()
        {
            DataTable dt = new DataTable();
            dt = LlenarDatos();
            if (dt.Rows.Count > 0)
            {

                Paciente oPaciente = new Paciente();
                oPaciente = (Paciente)oPaciente.Get(typeof(Paciente), int.Parse(Request["idPaciente"].ToString()));

                Item oItem = new Item();
                oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["idAnalisis"].ToString()));
                if (oItem.IdTipoResultado == 1)
                    oCr.Report.FileName = "../Informes/HistoricoAnalisis.rpt";
                else
                    oCr.Report.FileName = "../Informes/HistoricoAnalisisNoNumerico.rpt";

                Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

                ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
                encabezado1.Value = oCon.EncabezadoLinea1;

                ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
                encabezado2.Value = oCon.EncabezadoLinea2;

                ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
                encabezado3.Value = oCon.EncabezadoLinea3;

                ParameterDiscreteValue encabezado4 = new ParameterDiscreteValue();
                encabezado4.Value = oItem.Descripcion;


                

                ParameterDiscreteValue encabezado5 = new ParameterDiscreteValue();
                encabezado5.Value = oPaciente.NumeroDocumento;

                ParameterDiscreteValue encabezado6 = new ParameterDiscreteValue();
                encabezado6.Value = oPaciente.Apellido + " " + oPaciente.Nombre;

                ParameterDiscreteValue encabezado7 = new ParameterDiscreteValue();
                encabezado7.Value = oPaciente.FechaNacimiento.ToShortDateString();

                ParameterDiscreteValue encabezado8 = new ParameterDiscreteValue();
                encabezado8.Value = oPaciente.getSexo();


         

                ParameterDiscreteValue subTitulo = new ParameterDiscreteValue();               
                subTitulo.Value = "";


                oCr.ReportDocument.SetDataSource(dt);
                oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
                oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
                oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
                oCr.ReportDocument.ParameterFields[3].CurrentValues.Add(encabezado4);
                oCr.ReportDocument.ParameterFields[4].CurrentValues.Add(subTitulo);
                oCr.ReportDocument.ParameterFields[5].CurrentValues.Add(encabezado5);
                oCr.ReportDocument.ParameterFields[6].CurrentValues.Add(encabezado6);
                oCr.ReportDocument.ParameterFields[7].CurrentValues.Add(encabezado7);
                oCr.ReportDocument.ParameterFields[8].CurrentValues.Add(encabezado8);


                oCr.DataBind();
                oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Historial_Resultados.pdf");

                //MemoryStream oStream; // using System.IO
                //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("Content-Disposition", "attachment;filename=Historial_Resultados.pdf");

                //Response.BinaryWrite(oStream.ToArray());
                //Response.End();
            }
            else
                Response.Redirect("SinDatos.aspx", false);
        }

        private DataTable LlenarDatos()
        {
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlCommand cmd = new SqlCommand();            
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "LAB_HistoricoAnalisis";
            cmd.Parameters.Add("@fechaDesde", SqlDbType.NVarChar); cmd.Parameters["@fechaDesde"].Value = "";
            cmd.Parameters.Add("@fechaHasta", SqlDbType.NVarChar); cmd.Parameters["@fechaHasta"].Value = "";                      
            cmd.Parameters.Add("@idAnalisis", SqlDbType.NVarChar); cmd.Parameters["@idAnalisis"].Value = Request["idAnalisis"].ToString();
            cmd.Parameters.Add("@idPaciente", SqlDbType.NVarChar); cmd.Parameters["@idPaciente"].Value = Request["idPaciente"].ToString();

            cmd.Connection = conn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(Ds);
            return Ds.Tables[0];
        }

    }
}