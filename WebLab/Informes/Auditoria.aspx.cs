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
using CrystalDecisions.Shared;
using Business;
using CrystalDecisions.Web;
using Business.Data.Laboratorio;
using System.IO;
using System.Data.SqlClient;
using System.Text;

namespace WebLab.Informes
{
    public partial class Auditoria : System.Web.UI.Page
    {
        CrystalReportSource oCr = new CrystalReportSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["idUsuario"] != null)
                {
                    Inicializar();

                }
                else
                    Response.Redirect("../FinSesion.aspx", false);

            }
        }
     
        private void Inicializar()
        {
            CargarListas();

            switch (Request["tipo"].ToString())
            {
                case "controlAcceso": {
                    lblTitulo.Text = "AUDITORIA DE ACCESOS";
                    txtFechaDesde.Value = DateTime.Now.ToShortDateString();
                    txtFechaHasta.Value = DateTime.Now.ToShortDateString();
                    pnlControlProtocolo.Visible = false;
                    pnlControlAcceso.Visible = true;
                  
                } break;
                case "controlProtocolo":
                    {
                        Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
                        if (oCon.TipoNumeracionProtocolo == 3)
                            ddlTipoServicio.Visible = true;
                        else
                            ddlTipoServicio.Visible = false;
                        lblTitulo.Text = "AUDITORIA DE PROTOCOLO";
                        pnlControlProtocolo.Visible = true;
                        pnlControlAcceso.Visible = false;
                  
                    } break;
            }
        }
        private void CargarListas()
        {
            Utility oUtil = new Utility();
            ///Carga de combos de tipos de servicios
            string m_ssql = "select idusuario, apellido + ' ' +nombre  as nombre from sys_usuario order by apellido, nombre";
            oUtil.CargarCombo(ddlUsuario, m_ssql, "idusuario", "nombre");
            oUtil.CargarCombo(ddlUsuario2, m_ssql, "idusuario", "nombre");

            ddlUsuario.Items.Insert(0, new ListItem("--Todos--", "0"));
            ddlUsuario2.Items.Insert(0, new ListItem("--Todos--", "0"));

           
        }

        protected void btnControlProtocolo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
                ImprimirAuditoria();
         

        }

        private void ImprimirAuditoria()
        {

            DataTable dtAuditoria = GetDataSetAuditoria();
              if (dtAuditoria.Columns.Count > 2)
            {
            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
            
            ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
            encabezado1.Value = oCon.EncabezadoLinea1;

            ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
            encabezado2.Value = oCon.EncabezadoLinea2;

            ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
            encabezado3.Value = oCon.EncabezadoLinea3;


            oCr.Report.FileName = "AuditoriaProtocolo.rpt";
            oCr.ReportDocument.SetDataSource(dtAuditoria);
            oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
            oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
            oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
            oCr.DataBind();

                oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Auditoria" + txtProtocolo.Text + ".pdf");


                //MemoryStream oStream; // using System.IO
                //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("Content-Disposition", "attachment;filename=Auditoria" + txtProtocolo.Text + ".pdf");

                //Response.BinaryWrite(oStream.ToArray());
                //Response.End();

            }
              else
              {
                  string popupScript = "<script language='JavaScript'> alert('No se encontraron datos para el numero de protocolo ingresado.'); </script>";
                  Page.RegisterStartupScript("PopupScript", popupScript);
              }

        }

        private DataTable GetDataSetAuditoria()
        {
            string m_strSQL = " SELECT  idProtocolo  FROM   LAb_Protocolo  where  dbo.NumeroProtocolo(idProtocolo)='" + this.txtProtocolo.Text.Trim() + "'";

            if (ddlTipoServicio.Visible) m_strSQL += " and idtipoServicio= " + ddlTipoServicio.SelectedValue;

            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds, "auditoria1");


            DataTable data1 = Ds.Tables[0];
            //return data;

            if (data1.Rows.Count > 0)
            {
                string idProtocolo = Ds.Tables[0].Rows[0][0].ToString();

                string m_strCondicion = "";

                if (ddlUsuario.SelectedValue != "0")
                    m_strCondicion = " and U.idusuario=" + ddlUsuario.SelectedValue;

                m_strSQL = " SELECT  '" + this.txtProtocolo.Text.Trim() + "' AS numero,U.apellido  as username, A.fecha AS fecha, A.hora, A.accion, A.analisis, A.valor, A.valorAnterior" +
   " FROM         LAB_AuditoriaProtocolo AS A INNER JOIN Sys_Usuario AS U ON A.idUsuario = U.idUsuario" +
   " where  A.idProtocolo=" + idProtocolo + m_strCondicion+  " ORDER BY A.idAuditoriaProtocolo";
                
                DataSet Ds1 = new DataSet();            
                adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
                adapter.Fill(Ds1, "auditoria");


                DataTable data = Ds1.Tables[0];
                return data;
            }
            else return data1;
            
        }


      


        protected void btnControlAcceso_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
                ImprimirAuditoriaAcceso();
         

        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtFechaDesde.Value == "")
                args.IsValid = false;
            else
                if (txtFechaHasta.Value == "") args.IsValid = false;
                else args.IsValid = true;

        }
        private void ImprimirAuditoriaAcceso()
        {

            DataTable dtAuditoria = GetDataSetAuditoriaAcceso();
            if (dtAuditoria.Columns.Count > 0)
            {
                Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

                ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
                encabezado1.Value = oCon.EncabezadoLinea1;

                ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
                encabezado2.Value = oCon.EncabezadoLinea2;

                ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
                encabezado3.Value = oCon.EncabezadoLinea3;


                oCr.Report.FileName = "AuditoriaAcceso.rpt";
                oCr.ReportDocument.SetDataSource(dtAuditoria);
                oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
                oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
                oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
                oCr.DataBind();




                MemoryStream oStream; // using System.IO
                oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=AuditoriaAcceso.pdf");

                Response.BinaryWrite(oStream.ToArray());
                Response.End();
            }
            else
            {
                string popupScript = "<script language='JavaScript'> alert('No se encontraron datos para el numero de protocolo ingresado.'); </script>";
                Page.RegisterStartupScript("PopupScript", popupScript);
            }

        }

        private DataTable GetDataSetAuditoriaAcceso()
        {  
            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            string m_strCondicion = " Where 1=1 ";
            if (txtFechaDesde.Value != "") m_strCondicion += " and convert(varchar(10),fecha,103) >= convert(varchar(10), convert(datetime,'" + fecha2.ToString("yyyyMMdd") + "'),103) ";
            if (txtFechaHasta.Value != "") m_strCondicion += " AND convert(varchar(10),fecha,103)<= convert(varchar(10), convert(datetime,'" + fecha2.ToString("yyyyMMdd") + "'),103) ";
            if (ddlUsuario2.SelectedValue != "0")
                m_strCondicion = " and U.idUsuario=" + ddlUsuario2.SelectedValue;
            string m_strSQL = " SELECT   U.apellido + ' ' + U.nombre as username , LA.fecha AS fecha " +
" FROM         LAB_LogAcceso AS LA INNER JOIN Sys_Usuario AS U ON LA.idUsuario = U.idUsuario" + m_strCondicion +
" ORDER BY LA.IDLOGACCESO ";


            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds, "auditoria");


            DataTable data = Ds.Tables[0];
            return data;
        }
    }
}
