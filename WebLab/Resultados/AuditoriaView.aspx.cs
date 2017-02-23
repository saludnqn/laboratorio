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
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.IO;

namespace WebLab.Resultados
{
    public partial class AuditoriaView : System.Web.UI.Page
    {
        CrystalReportSource oCr = new CrystalReportSource();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Protocolo oProtocolo = new Protocolo();
               string s_idProtocolo = Request["idProtocolo"].ToString();

                oProtocolo = (Protocolo)oProtocolo.Get(typeof(Protocolo), int.Parse(s_idProtocolo));//int.Parse(Request["idProtocolo"].ToString()));r();
                Label1.Text = "Protocolo Nro. " + oProtocolo.GetNumero();
             gvLista.DataSource = GetDataSetAuditoria();
            gvLista.DataBind();
            }

        }

        private object GetDataSetAuditoria()
        {

            string m_strSQL = " SELECT  dbo.NumeroProtocolo(A.idProtocolo) AS numero, U.username, A.fecha AS fecha, A.hora, A.accion, A.analisis, A.valor, A.valorAnterior" +
" FROM         LAB_AuditoriaProtocolo AS A INNER JOIN Sys_Usuario AS U ON A.idUsuario = U.idUsuario" +
" where idProtocolo=" + Request["idProtocolo"].ToString()+" ORDER BY A.idAuditoriaProtocolo";


            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds, "auditoria");


            DataTable data = Ds.Tables[0];
            return data;


        }


        private void ImprimirAuditoria()
        {
            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

            ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
            encabezado1.Value = oCon.EncabezadoLinea1;

            ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
            encabezado2.Value = oCon.EncabezadoLinea2;

            ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
            encabezado3.Value = oCon.EncabezadoLinea3;


            oCr.Report.FileName = "../Informes/AuditoriaProtocolo.rpt";
            oCr.ReportDocument.SetDataSource(GetDataSetAuditoria());
            oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
            oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
            oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
            oCr.DataBind();


            oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Auditoria.pdf");

            //MemoryStream oStream; // using System.IO
            //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment;filename=Auditoria.pdf");

            //Response.BinaryWrite(oStream.ToArray());
            //Response.End();

        }

        protected void imgPdf_Click(object sender, ImageClickEventArgs e)
        {
            ImprimirAuditoria();
        }
    }
}