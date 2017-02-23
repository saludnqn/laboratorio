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
using Business.Data.Laboratorio;
using System.IO;
using System.Drawing;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

namespace WebLab.Protocolos
{
    public partial class ProtocoloMensaje : System.Web.UI.Page
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
                //Business.Data.Laboratorio.Protocolo oP = new Business.Data.Laboratorio.Protocolo();
                //oP= (Business.Data.Laboratorio.Protocolo)oP.Get(typeof(Business.Data.Laboratorio.Protocolo),int.Parse(Request["id"].ToString()));
                
//                Configuracion oCon = new Configuracion();              oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
//                lblNumero.Text = "Se ha guardado el protocolo Nro. ";
//                if (oCon.TipoNumeracionProtocolo==0)
//lblNumero.Text +=   oP.Numero.ToString();
//                else
//                    lblNumero.Text += oP.NumeroDiario.ToString();


//                if (oCon.GeneraComprobanteProtocolo)
//                    Panel1.Visible = true;
//                else
//                    Panel1.Visible = false;

                Imprimir(Request["id"].ToString(), "Pantalla");
            }
            
        }

        private void Imprimir(string p, string p_2)
        {
            Business.Data.Laboratorio.Protocolo oProt = new Business.Data.Laboratorio.Protocolo();
            oProt = (Business.Data.Laboratorio.Protocolo)oProt.Get(typeof(Business.Data.Laboratorio.Protocolo), int.Parse(p));

            oCr.Report.FileName = "../Informes/Protocolo.rpt";
            oCr.ReportDocument.SetDataSource(oProt.GetDataSet("Protocolo", "", oProt.IdTipoServicio.IdTipoServicio));
            oCr.DataBind();
         

                switch (p_2)
                {
                    case "Pantalla":
                        {
                            CrystalReportViewer1.ReportSourceID = oCr.ID;
                            CrystalReportViewer1.ReportSource = oCr;
                            Condiciones_del_visualizador();
                        }
                        break;
                    case "I":
                        {oCr.ReportDocument.PrintToPrinter(1, true, 1, 100);
                        }
                        break;
                         case "PDF":
                        {

                        oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Protocolo.pdf");
                        //        MemoryStream oStream; // using System.IO
                        //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        //Response.Clear();
                        //Response.Buffer = true;
                        //Response.ContentType = "application/pdf";
                        //Response.AddHeader("Content-Disposition", "attachment;filename=Protocolo.pdf");

                        //Response.BinaryWrite(oStream.ToArray());
                        //Response.End();
                    }
                        break;
                }



               
            
        }

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(Request["id"].ToString(), "I");
        }

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            Imprimir(Request["id"].ToString(), "PDF");
        }


        private void Condiciones_del_visualizador()
        {
            this.CrystalReportViewer1.BestFitPage = false;
            this.CrystalReportViewer1.BorderColor = Color.FromArgb(0x78006599);
            this.CrystalReportViewer1.BorderStyle = BorderStyle.Solid;
            this.CrystalReportViewer1.BorderWidth = 1;

            this.CrystalReportViewer1.DisplayGroupTree = false;
            this.CrystalReportViewer1.DisplayToolbar = false;
            this.CrystalReportViewer1.DisplayPage = true;

            this.CrystalReportViewer1.HasDrillUpButton = false;
            this.CrystalReportViewer1.HasGotoPageButton = false;
            this.CrystalReportViewer1.HasPageNavigationButtons = true;
            this.CrystalReportViewer1.HasRefreshButton = false;
            this.CrystalReportViewer1.HasSearchButton = false;
            this.CrystalReportViewer1.HasZoomFactorList = true;

            this.CrystalReportViewer1.SeparatePages = true;
            this.CrystalReportViewer1.ShowFirstPage();
            this.CrystalReportViewer1.PageZoomFactor = 100;
            this.CrystalReportViewer1.Height = 460;
            this.CrystalReportViewer1.Width = 780;
        }

        protected void lnkRegresar_Click(object sender, EventArgs e)
        {
            if (Request["Operacion"].ToString() == "Modifica")
                Response.Redirect("ProtocoloList.aspx", false);
            else
            {
                if (Request["Operacion"].ToString() == "AltaTurno")
                    Response.Redirect("../turnos/Turnolist.aspx", false);
                else
                    Response.Redirect("Default.aspx", false);
            }
        }
    }
}
