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
using Business;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data.SqlClient;
using Business.Data.Laboratorio;
using CrystalDecisions.Web;

namespace WebLab.Informes
{
    public partial class Informe : System.Web.UI.Page
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
                Inicializar();
                txtProtocoloDesde.Focus();                                                       
            }
        }



        private void Inicializar()
        {   
            txtFechaDesde.Value = DateTime.Now.ToShortDateString();
            txtFechaHasta.Value = DateTime.Now.ToShortDateString();
            CargarListas();

            switch (Request["Tipo"].ToString())
            {
                case "HojaTrabajo":
                    {
                        VerificaPermisos("Hoja de Trabajo");
                        lblTitulo.Text = "HOJA DE TRABAJO";
                        pnlEtiquetaCodigoBarras.Visible = false;
                        pnlHojaTrabajo.Visible = true;
                        pnlAnalisisFueraHT.Visible = false;
                        pnlHojaTrabajoResultado.Visible = true;
                        chkDesdeUltimoListado.Visible = true;
                        //pnlHojaTrabajo.Visible = true;
                        //btnBuscar.Visible = false;
                        CargarGrilla();

                    } break;
                case "AnalisisFueraHT":
                    {
                        VerificaPermisos("Analisis fuera de HT");
                        lblTitulo.Text = "ANALISIS FUERA DE HOJA DE TRABAJO";
                        pnlEtiquetaCodigoBarras.Visible =false;
                        pnlHojaTrabajo.Visible = false;
                        pnlHojaTrabajoResultado.Visible = false;
                        chkDesdeUltimoListado.Visible = false;
                        pnlAnalisisFueraHT.Visible = true;
                        CargarAnalisisFueraHT();
                        //pnlHojaTrabajo.Visible = false;
                     //   btnBuscar.Visible = true;
                    }
                    break;

                case "CodigoBarras":
                    {
                        VerificaPermisos("Etiqueta Codigo Barras");
                        lblTitulo.Text = "IMPRESION DE ETIQUETAS DE CODIGO DE BARRAS";
                        pnlEtiquetaCodigoBarras.Visible = true;
                        pnlHojaTrabajo.Visible = false;
                        chkDesdeUltimoListado.Visible = false;
                        pnlHojaTrabajoResultado.Visible = false;
                        pnlAnalisisFueraHT.Visible = false;                        
                        
                    }
                    break;
            }                        
          
        }

        private void VerificaPermisos(string sObjeto)
        {
            if (Session["s_permiso"] != null)
            {
                Utility oUtil = new Utility();
                int i_permiso = oUtil.VerificaPermisos((ArrayList)Session["s_permiso"], sObjeto);
                switch (i_permiso)
                {
                    case 0: Response.Redirect("../AccesoDenegado.aspx", false); break;
                    //case 1: btn .Visible = false; break;
                }
            }
            else Response.Redirect("../FinSesion.aspx", false);
        }


        private void CargarListas()
        {
            Utility oUtil = new Utility();
            ///Carga de combos de tipos de servicios
            string m_ssql = "select idTipoServicio, nombre from Lab_TipoServicio WHERE (baja = 0)";
            oUtil.CargarCombo(ddlServicio, m_ssql, "idTipoServicio", "nombre");
          

             ///Carga de combos de Origen
            m_ssql = "SELECT  idOrigen, nombre FROM LAB_Origen WHERE (baja = 0)";
            oUtil.CargarCombo(ddlOrigen, m_ssql, "idOrigen", "nombre");
            ddlOrigen.Items.Insert(0, new ListItem("-- Todos --", "0"));

            ///Carga de combos de Prioridad
            m_ssql = "SELECT idPrioridad, nombre FROM LAB_Prioridad WHERE (baja = 0)";
            oUtil.CargarCombo(ddlPrioridad, m_ssql, "idPrioridad", "nombre");
            ddlPrioridad.Items.Insert(0, new ListItem("-- Todos --", "0"));

            if (Request["Tipo"].ToString() == "HojaTrabajo") ddlPrioridad.SelectedValue = "1";//rutina

            ///Carga de Sectores
            m_ssql = "SELECT idSectorServicio, prefijo + ' - ' + nombre as nombre  FROM LAB_SectorServicio WHERE (baja = 0) order by nombre";            
            oUtil.CargarListBox(lstSector, m_ssql, "idSectorServicio", "nombre");

            for (int i = 0; i < lstSector.Items.Count; i++)
            {
                lstSector.Items[i].Selected =true;
            }
            //////////////////
            
            ///Carga de combos de areas
            CargarArea();

            ///Carga de Efectores solicitantes
            m_ssql = "SELECT idEfector, nombre FROM sys_Efector order by nombre ";
            oUtil.CargarCombo(ddlEfector, m_ssql, "idEfector", "nombre");
            ddlEfector.Items.Insert(0, new ListItem("-- Todos --", "0"));
            //ddlEfector.SelectedValue = oC.IdEfector.IdEfector.ToString();

            if (pnlAnalisisFueraHT.Visible)
            {
                pnlAnalisisFueraHT.Visible = true;
                CargarAnalisisFueraHT(); 
            }


            ///////////////Impresoras////////////////////////

            m_ssql = "SELECT idImpresora, nombre FROM LAB_Impresora ";
            oUtil.CargarCombo(ddlImpresora, m_ssql, "nombre", "nombre");
            if (Request["Tipo"].ToString() == "CodigoBarras")
            { if (Session["Etiquetadora"] != null) ddlImpresora.SelectedValue = Session["Etiquetadora"].ToString(); }
            else
                if (Session["Impresora"] != null) ddlImpresora.SelectedValue = Session["Impresora"].ToString();
           
            /////////////////////////////////////////////

            m_ssql = null;
            oUtil = null;
        }

        private void CargarAnalisisFueraHT()
        {
            Utility oUtil = new Utility();
            string m_condicion = " where 1=1 ";
            if (ddlArea.SelectedValue != "0") m_condicion += " and idArea= " + ddlArea.SelectedValue;
            if (ddlServicio.SelectedValue != "0") m_condicion += " and idTipoServicio= " + ddlServicio.SelectedValue;


            string m_ssql = "SELECT idItem,  nombre+ '  [' + codigo + ']' as nombre  FROM vta_LAB_AnalisisFueraHT " + m_condicion + " order by nombre ";
            oUtil.CargarListBox(lstAnalisis, m_ssql, "idItem", "nombre");   
        }


        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarArea();
            if (pnlHojaTrabajo.Visible)
            CargarGrilla();

            if (pnlAnalisisFueraHT.Visible)
                CargarAnalisisFueraHT();
            if (ddlServicio.SelectedValue == "3")
            { btnSeleccionarTipoMuestra.Visible = true;
            btnSeleccionarTipoMuestra.UpdateAfterCallBack = true;
            }
            else
            {
                btnSeleccionarTipoMuestra.Visible = false;
                lstMuestra.Visible = false;
                lstMuestra.UpdateAfterCallBack = true;
                btnSeleccionarTipoMuestra.UpdateAfterCallBack = true;
            }
            
            //ddlArea.UpdateAfterCallBack = true;
        }

        private void CargarTipoMuestra()
        {
            Utility oUtil = new Utility();

            string m_ssql = "SELECT idMuestra, nombre + ' - ' + codigo as nombre FROM LAB_Muestra   where baja=0  order by nombre ";
            
            oUtil.CargarListBox(lstMuestra, m_ssql, "idMuestra", "nombre");
            for (int i = 0; i < lstMuestra.Items.Count; i++)
            {
                lstMuestra.Items[i].Selected = true;
            }
            lstMuestra.Visible = true;
           

        }

        private void CargarArea()
        {
            Utility oUtil = new Utility();            
            
            string m_stroCondicion="";
            if (Request["Tipo"].ToString() == "CodigoBarras")   m_stroCondicion = " and imprimeCodigoBarra=1 ";

            string m_ssql = "select idArea, nombre from Lab_Area where baja=0  and idTipoServicio=" + ddlServicio.SelectedValue + m_stroCondicion +" order by nombre";            
            oUtil.CargarCombo(ddlArea, m_ssql, "idArea", "nombre");

            if (Request["Tipo"].ToString()=="CodigoBarras")  ddlArea.Items.Insert(0, new ListItem("--ETIQUETA GENERAL--", "0"));                    
            else  ddlArea.Items.Insert(0, new ListItem("Todas", "0"));                    
            
            m_ssql = null;
            oUtil = null;
           
        }

        private void MostrarInforme(string tipo, DataTable dt)
        {
            
            //Aca se deberá consultar los parametros para mostrar una hoja de trabajo u otra
            //this.CrystalReportSource1.Report.FileName = "HTrabajo2.rpt";

            try
            {

                Configuracion oCon = new Configuracion();              oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
                 
                ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
                encabezado1.Value = oCon.EncabezadoLinea1;

                ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
                encabezado2.Value = oCon.EncabezadoLinea2;

                ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
                encabezado3.Value = oCon.EncabezadoLinea3;

             //   string informe = "";
                string m_reporte = "Reporte.pdf";
                switch (Request["Tipo"].ToString())
                {
                    case "HojaTrabajo":
                        {
                            /*            if (oCon.TipoHojaTrabajo == 0) informe = "HTrabajo.rpt";
                                        if (oCon.TipoHojaTrabajo == 1) informe = "HTrabajo2.rpt"; 
                             * */

                            Business.Data.Laboratorio.Area oArea = new Business.Data.Laboratorio.Area();
                            oArea = (Business.Data.Laboratorio.Area)oArea.Get(typeof(Business.Data.Laboratorio.Area), int.Parse(ddlArea.SelectedValue));

                            HojaTrabajo oRegistro = new HojaTrabajo();
                            oRegistro = (HojaTrabajo)oRegistro.Get(typeof(HojaTrabajo), "IdArea", oArea);
                            if (oRegistro.Formato == 0)
                            {
                                switch (oRegistro.FormatoAncho)
                                {
                                    case 0: oCr.Report.FileName = "../iNFORMES/HojasdeTRabajo/HTrabajoProtocolo1.rpt"; break;
                                    case 1: oCr.Report.FileName = "../iNFORMES/HojasdeTRabajo/HTrabajoProtocolo2.rpt"; break;
                                    case 2: oCr.Report.FileName = "../iNFORMES/HojasdeTRabajo/HTrabajoProtocolo3.rpt"; break;
                                    case 3: oCr.Report.FileName = "../iNFORMES/HojasdeTRabajo/HTrabajoProtocolo4.rpt"; break;
                                } 
                                m_reporte = oRegistro.Codigo+ ".pdf";
                            }
                            if (oRegistro.Formato == 1)
                            {
                                switch (oRegistro.FormatoAncho)
                                {
                                    case 0: oCr.Report.FileName = "../iNFORMES/HojasdeTrabajo/HTrabajoAnalisis1.rpt"; break;
                                    case 1: oCr.Report.FileName = "../iNFORMES/HojasdeTrabajo/HTrabajoAnalisis2.rpt"; break;
                                    case 2: oCr.Report.FileName = "../iNFORMES/HojasdeTrabajo/HTrabajoAnalisis3.rpt"; break;
                                }               
                                m_reporte = oRegistro.Codigo+ ".pdf";
                            }

                            
                            oCr.ReportDocument.SetDataSource(dt);
                        } break;
                    case "MuestrasFaltantes": lblTitulo.Text = "MUESTRAS FALTANTES"; break;
                }




                //            this.CrystalReportSource1.ReportDocument.SetDataSource(GetDataSet_HojaTrabajo());

                oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
                oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
                oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
                oCr.DataBind();

                if (tipo == "Imprimir")//imprimir
                {
                    try
                    {
                        Session["Impresora"] = ddlImpresora.SelectedValue;
                        oCr.ReportDocument.PrintOptions.PrinterName = ddlImpresora.SelectedValue;
                        oCr.ReportDocument.PrintToPrinter(1, true, 1, 100);
                    }
                    catch (Exception ex)
                    {
                        string exception = "";
                        //while (ex != null)
                        //{
                        //    exception = ex.Message + "<br>";

                        //}
                        string popupScript = "<script language='JavaScript'> alert('No se pudo imprimir en la impresora " + Session["Impresora"].ToString() + ". Si el problema persiste consulte con soporte técnico." + exception + "'); </script>";
                        Page.RegisterStartupScript("PopupScript", popupScript);
                    }
                }
                else
                {
                    oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, m_reporte);
                    //MemoryStream oStream; // using System.IO
                    //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    //Response.Clear();
                    //Response.Buffer = true;
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("Content-Disposition", "attachment;filename=" + m_reporte);

                    //Response.BinaryWrite(oStream.ToArray());
                    //Response.End();
                }
            }
            catch
            { }

        }

        
        protected void gvLista_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Pdf")
            {
                if (Page.IsValid)
                    VistaPreeliminar(e.CommandArgument, "PDF");
            }


            if (e.CommandName == "Imprimir")
            {
                if (Page.IsValid)
                    VistaPreeliminar(e.CommandArgument, "Impresora");
            }
        }

     

        
        private void MarcarSeleccionados(bool p)
        {
            foreach (GridViewRow row in gvLista.Rows)
            {
                CheckBox a = ((CheckBox)(row.Cells[0].FindControl("CheckBox1")));
                if (a.Checked == !p)
                    ((CheckBox)(row.Cells[0].FindControl("CheckBox1"))).Checked = p;
            }

        }
        private void VistaPreeliminar(object p, string accion)
        {

                DataTable dt = new DataTable();
                dt = GetDataSet_HojaTrabajo(p);


                if (dt.Rows.Count == 0)
                {
                    if (accion == "PDF")
                    {
                        string popupScript = "<script language='JavaScript'> alert('No se encontraron datos para la hoja de trabajo seleccionada'); </script>";
                        Page.RegisterStartupScript("PopupScript", popupScript);
                    }
                    
                }
                else
                {
                    CrystalReportSource oCr = new CrystalReportSource();

                    oCr.Report.FileName = "";
                        oCr.CacheDuration = 0;
                        oCr.EnableCaching = false;


                    HojaTrabajo oRegistro = new HojaTrabajo();
                    oRegistro = (HojaTrabajo)oRegistro.Get(typeof(HojaTrabajo), int.Parse(p.ToString()));

                    Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

                    ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
                    encabezado1.Value = oCon.EncabezadoLinea1;

                    ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
                    encabezado2.Value = oCon.EncabezadoLinea2;

                    ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
                    encabezado3.Value = oCon.EncabezadoLinea3;

                    ParameterDiscreteValue imprimirFechaHora = new ParameterDiscreteValue();
                    imprimirFechaHora.Value = oRegistro.ImprimirFechaHora;

                    string m_reporte = "";
                    string nombre_reporte = "";
                    if (oRegistro.Formato == 0) ///unico formato
                    {
                        m_reporte = oRegistro.Codigo;
                        switch (oRegistro.FormatoAncho)
                        {
                            case 0: { 
                                if (oCon.TipoNumeracionProtocolo==2)
                                    nombre_reporte = "../iNFORMES/HojasdeTRabajo/HTrabajoProtocoloLetra1"; 
                                else
                                    nombre_reporte = "../iNFORMES/HojasdeTRabajo/HTrabajoProtocolo1"; 
                                //oCr.Report.FileName = "../iNFORMES/HojasdeTRabajo/HTrabajoProtocolo1.rpt"; 
                            }
                                break;
                            case 1:
                                {
                                    if (oCon.TipoNumeracionProtocolo == 2)
                                       nombre_reporte = "../iNFORMES/HojasdeTRabajo/HTrabajoProtocoloLetra2";
                                    else
                                        nombre_reporte= "../iNFORMES/HojasdeTRabajo/HTrabajoProtocolo2";
                                } break;
                            case 2:{
                                if (oCon.TipoNumeracionProtocolo == 2)
                                    nombre_reporte= "../iNFORMES/HojasdeTRabajo/HTrabajoProtocoloLetra3";
                                else
                                nombre_reporte=  "../iNFORMES/HojasdeTRabajo/HTrabajoProtocolo3"; }
                                break;
                            case 3://texto corto con numero de fila
                                {
                                    if (oCon.TipoNumeracionProtocolo == 2)
                                      nombre_reporte= "../iNFORMES/HojasdeTRabajo/HTrabajoProtocoloLetra3";
                                    else
                                        nombre_reporte=  "../iNFORMES/HojasdeTRabajo/HTrabajoProtocolo4";
                                } break;
                        }
                    }
                        if (ddlServicio.SelectedValue=="3")  //microbiolgoia
                            nombre_reporte= nombre_reporte+ "Microbiologia";

                        if (ddlServicio.SelectedValue == "4")  //microbiolgoia
                        {
                            nombre_reporte = nombre_reporte + "Pesquisa";
                            if (txtFechaDesde.Value == txtFechaHasta.Value)
                                encabezado3.Value = txtFechaDesde.Value;
                            else
                                encabezado3.Value = txtFechaDesde.Value + " - " + txtFechaHasta.Value;
                        }

                        if (!oRegistro.TipoHoja) // 0: Horizontal
                            nombre_reporte = nombre_reporte + "Horizontal";
                        


                        nombre_reporte= nombre_reporte+ ".rpt";

                    oCr.Report.FileName = nombre_reporte;
                    //if (oRegistro.Formato == 1)
                    //{
                    //    m_reporte = oRegistro.Codigo;

                    //    switch (oRegistro.FormatoAncho)
                    //    {
                    //        case 0: oCr.Report.FileName = "../iNFORMES/HojasdeTrabajo/HTrabajoAnalisis1.rpt"; break;
                    //        case 1: oCr.Report.FileName = "../iNFORMES/HojasdeTrabajo/HTrabajoAnalisis2.rpt"; break;
                    //        case 2: oCr.Report.FileName = "../iNFORMES/HojasdeTrabajo/HTrabajoAnalisis3.rpt"; break;
                    //    }

                    //}

                    oCr.ReportDocument.PrintOptions.PaperSize = PaperSize.PaperA4;
                    oCr.ReportDocument.SetDataSource(dt);
                    //if (!oRegistro.TipoHoja) // 0: Horizontal
                    //    oCr.ReportDocument.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    //else //1: vertical
                    //    oCr.ReportDocument.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                    oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
                    oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
                    oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
                    oCr.ReportDocument.ParameterFields[3].CurrentValues.Add(imprimirFechaHora);
                    oCr.DataBind();


                    if (accion == "PDF")
                    {
                    oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, m_reporte + ".pdf");

                    //MemoryStream oStream; // using System.IO
                    //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    ////Response.CacheControl = "Private";
                    //Response.Buffer = true;
                    //Response.ClearContent();
                    //Response.ClearHeaders();
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("Content-Disposition", "attachment;filename=" + m_reporte + ".pdf");
                    //Response.BinaryWrite(oStream.ToArray());
                    //Response.End();
                }
                    else
                    {
                        try
                        {
                            Session["Impresora"] = ddlImpresora.SelectedValue.ToString();
                            oCr.ReportDocument.PrintOptions.PrinterName = ddlImpresora.SelectedValue; // ConfigurationSettings.AppSettings["Impresora"]; 
                            oCr.ReportDocument.PrintToPrinter(1, false, 0, 0);
                        }
                        catch (Exception ex)
                        {
                            string exception = "";
                            //while (ex != null)
                            //{
                                exception = ex.Message + "<br>";

                            //}
                            string popupScript = "<script language='JavaScript'> alert('No se pudo imprimir en la impresora " + Session["Impresora"].ToString() + ". Si el problema persiste consulte con soporte técnico." + exception + "'); </script>";
                            Page.RegisterStartupScript("PopupScript", popupScript);
                        }
                    }
                }





        }

     


        private DataTable GetDataSet_HojaTrabajo(object p)
        {

            string s_store = "LAB_GeneraHT";
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            // Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
            if (rdbHojaConResultados.SelectedValue == "0")
               s_store = "LAB_GeneraHT";
            else                           
               s_store = "LAB_GeneraHTconResultados";
              
            
            if (ddlServicio.SelectedValue=="3") s_store = s_store + "Microbiologia";
            if (ddlServicio.SelectedValue=="4") s_store = s_store + "Pesquisa";

            cmd.CommandText= s_store;
            /////Parametros de fechas           
            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            cmd.Parameters.Add("@fechaDesde", SqlDbType.NVarChar);
            cmd.Parameters["@fechaDesde"].Value = fecha1.ToString("yyyyMMdd");
            cmd.Parameters.Add("@fechaHasta", SqlDbType.NVarChar);
            cmd.Parameters["@fechaHasta"].Value = fecha2.ToString("yyyyMMdd");
            /////


            ///////Parametro hoja de trabajo
            cmd.Parameters.Add("@idHojaTrabajo", SqlDbType.NVarChar); 
            cmd.Parameters["@idHojaTrabajo"].Value = p.ToString();        

            ///////Parametro @idEfectorSolicitante
            cmd.Parameters.Add("@idEfectorSolicitante", SqlDbType.NVarChar); 
            cmd.Parameters["@idEfectorSolicitante"].Value = ddlEfector.SelectedValue;

            ///////Parametro @@idOrigen
            cmd.Parameters.Add("@idOrigen", SqlDbType.NVarChar); 
            cmd.Parameters["@idOrigen"].Value = ddlOrigen.SelectedValue;

            ///////Parametro @@@idPrioridad
            cmd.Parameters.Add("@idPrioridad", SqlDbType.NVarChar); 
            cmd.Parameters["@idPrioridad"].Value = ddlPrioridad.SelectedValue;
            
            
            ///////Parametro @@@@idSector        

            cmd.Parameters.Add("@idSector", SqlDbType.NVarChar);
            cmd.Parameters["@idSector"].Value = getListaSectores(true);  // getListaSectores();// ddlSectorServicio.SelectedValue;


            ///////Parametro @@@@idSector
            cmd.Parameters.Add("@estado", SqlDbType.NVarChar);
            cmd.Parameters["@estado"].Value = rdbEstadoAnalisis.SelectedValue;

            ///////Parametro @@@@numeroDesde
            cmd.Parameters.Add("@numeroDesde", SqlDbType.NVarChar);
            cmd.Parameters["@numeroDesde"].Value = txtProtocoloDesde.Value;


            ///////Parametro @@@@numeroDesde
            cmd.Parameters.Add("@numeroHasta", SqlDbType.NVarChar);
            cmd.Parameters["@numeroHasta"].Value = txtProtocoloHasta.Value;


            cmd.Parameters.Add("@desdeUltimoNumero", SqlDbType.Int);
            if (chkDesdeUltimoListado.Checked)
                cmd.Parameters["@desdeUltimoNumero"].Value = 1;
            else
                cmd.Parameters["@desdeUltimoNumero"].Value = 0;



            if (ddlServicio.SelectedValue == "3")
            {
                cmd.Parameters.Add("@tipoMuestra", SqlDbType.NVarChar);
                cmd.Parameters["@tipoMuestra"].Value = getListaMuestra();
            }

            cmd.Connection = conn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(Ds);
            return Ds.Tables[0];

        }

        protected void btnSeleccionarTipoMuestra_Click(object sender, EventArgs e)
        {
            if (lstMuestra.Visible) { lstMuestra.Visible = false; btnSeleccionarTipoMuestra.Text = "Elegir Tipo de muestra"; }
            else
            {
                btnSeleccionarTipoMuestra.Text = "Ocultar Tipo de Muestra";
                CargarTipoMuestra();
            }
            btnSeleccionarTipoMuestra.UpdateAfterCallBack = true;
            lstMuestra.UpdateAfterCallBack = true;
        }

        private string getListaMuestra()
        {
            string m_lista = "";
            for (int i = 0; i < lstMuestra.Items.Count; i++)
            {
                if (lstMuestra.Items[i].Selected)
                {
                    if (m_lista == "")
                        m_lista = lstMuestra.Items[i].Value;
                    else
                        m_lista += "," + lstMuestra.Items[i].Value;
                }
            }
            return m_lista;
        }

        //private object getListaSectores()
        //{
        //    string m_lista = "";
        //    for (int i = 0; i <lstSector.Items.Count; i++)
        //    {
        //        if (lstSector.Items[i].Selected)
        //        {
        //            if (m_lista == "")
        //                m_lista = lstSector.Items[i].Value;
        //            else
        //                m_lista += "," + lstSector.Items[i].Value;
        //        }
        //    }
        //    return m_lista;
        //}

        private string getListaSectores(bool filtro)
        {
            string m_lista = "";
            bool todasseleccionadas = true;
            for (int i = 0; i < lstSector.Items.Count; i++)
            {
                if (lstSector.Items[i].Selected)
                {
                    if (m_lista == "")
                        m_lista = lstSector.Items[i].Value;
                    else
                        m_lista += "," + lstSector.Items[i].Value;
                }
                else todasseleccionadas = false;

            }
            if ((filtro) && (todasseleccionadas)) m_lista = "";
            return m_lista;
        }

        private DataTable GetDataSet_AnalisisFueraHT()
        {
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.CommandText = "LAB_AnalisisFueraHT";


            /////Parametros de fechas           
            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            cmd.Parameters.Add("@fechaDesde", SqlDbType.NVarChar);
            cmd.Parameters["@fechaDesde"].Value = fecha1.ToString("yyyyMMdd");
            cmd.Parameters.Add("@fechaHasta", SqlDbType.NVarChar);
            cmd.Parameters["@fechaHasta"].Value = fecha2.ToString("yyyyMMdd");
            /////


            ///////Parametro hoja de trabajo



            ///////Parametro @idEfectorSolicitante
            cmd.Parameters.Add("@idEfectorSolicitante", SqlDbType.NVarChar);
            cmd.Parameters["@idEfectorSolicitante"].Value = ddlEfector.SelectedValue;

            ///////Parametro @@idOrigen
            cmd.Parameters.Add("@idOrigen", SqlDbType.NVarChar);
            cmd.Parameters["@idOrigen"].Value = ddlOrigen.SelectedValue;

            ///////Parametro @@@idPrioridad
            cmd.Parameters.Add("@idPrioridad", SqlDbType.NVarChar);
            cmd.Parameters["@idPrioridad"].Value = ddlPrioridad.SelectedValue;


            ///////Parametro @@@@idSector
            cmd.Parameters.Add("@idSector", SqlDbType.NVarChar);
            cmd.Parameters["@idSector"].Value = getListaSectores(true); // ddlSectorServicio.SelectedValue;


            ///////Parametro @@@@idSector
            cmd.Parameters.Add("@listaItem", SqlDbType.NVarChar);
            cmd.Parameters["@listaItem"].Value = getListaAnalisis();


            ///////Parametro @@@@numeroDesde
            cmd.Parameters.Add("@numeroDesde", SqlDbType.NVarChar);
            cmd.Parameters["@numeroDesde"].Value = txtProtocoloDesde.Value;


            ///////Parametro @@@@numeroDesde
            cmd.Parameters.Add("@numeroHasta", SqlDbType.NVarChar);
            cmd.Parameters["@numeroHasta"].Value = txtProtocoloHasta.Value;


            ///////Parametro @@@@numeroDesde
            cmd.Parameters.Add("@estado", SqlDbType.NVarChar);
            cmd.Parameters["@estado"].Value = rdbEstadoAnalisis.SelectedValue;

            cmd.Connection = conn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(Ds);
            return Ds.Tables[0];
        }

        private object getListaAnalisis()
        {
            string m_lista = "";
            for (int i = 0; i < lstAnalisis.Items.Count; i++)
            {
                if (lstAnalisis.Items[i].Selected)
                {
                    if (m_lista == "")
                        m_lista = lstAnalisis.Items[i].Value;
                    else
                        m_lista += "," + lstAnalisis.Items[i].Value;
                }

            }
            return m_lista;
        }


        private void VistaPreeliminar_AnalisisFueraHT( string accion)
        {

            DataTable dt = new DataTable();
            dt = GetDataSet_AnalisisFueraHT();

            if (dt.Rows.Count == 0)
            {               
                    string popupScript = "<script language='JavaScript'> alert('No se encontraron datos para los filtros de busqueda seleccionados'); </script>";
                    Page.RegisterStartupScript("PopupScript", popupScript);               
            }
            else
            {               

                Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

                ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
                encabezado1.Value = oCon.EncabezadoLinea1;

                ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
                encabezado2.Value = oCon.EncabezadoLinea2;

                ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
                encabezado3.Value = oCon.EncabezadoLinea3;

                oCr.Report.FileName = "../iNFORMES/AnalisisFueraHT.rpt";                
                oCr.ReportDocument.SetDataSource(dt);
                
              
                oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
                oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
                oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
                oCr.DataBind();


                if (accion == "PDF")
                {
                    oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "AnalisisFueraHT.pdf");
                    //MemoryStream oStream; // using System.IO
                    //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    ////Response.CacheControl = "Private";
                    //Response.Buffer = true;
                    //Response.ClearContent();
                    //Response.ClearHeaders();
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("Content-Disposition", "attachment;filename=AnalisisFueraHT.pdf");
                    //Response.BinaryWrite(oStream.ToArray());
                    //Response.End();
                }
                else
                {
                    try
                    {
                        Session["Impresora"] = ddlImpresora.SelectedValue;
                        oCr.ReportDocument.PrintOptions.PrinterName = ddlImpresora.SelectedValue;
                        oCr.ReportDocument.PrintToPrinter(1, false, 0, 0);
                    }
                    catch (Exception ex)
                    {
                        string exception = "";
                        //while (ex != null)
                        //{
                            exception = ex.Message + "<br>";

                        //}
                        string popupScript = "<script language='JavaScript'> alert('No se pudo imprimir en la impresora " + Session["Impresora"].ToString() + ". Si el problema persiste consulte con soporte técnico." + exception + "'); </script>";
                        Page.RegisterStartupScript("PopupScript", popupScript);
                    }
                }
            }





        }
        protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {               
                ImageButton CmdPdf = (ImageButton)e.Row.Cells[4].Controls[1];
                CmdPdf.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdPdf.CommandName = "Pdf";
                CmdPdf.ToolTip = "Vista Preeliminar";

                ImageButton CmdAdicional = (ImageButton)e.Row.Cells[5].Controls[1];
                CmdAdicional.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdAdicional.CommandName = "Imprimir";
                CmdAdicional.ToolTip = "Imprimir";   
            }
        }

        private void CargarGrilla()
        {
          //  gvLista.AutoGenerateColumns = false;
         
            gvLista.DataSource = LeerDatos();
            gvLista.DataBind();
            MarcarSeleccionados(false);
        }

        private object LeerDatos()
        {
            string m_condicion = "  ";
            if (ddlArea.SelectedValue != "0") m_condicion += " and HT.idArea= " + ddlArea.SelectedValue;
            if (ddlServicio.SelectedValue != "0") m_condicion += " and A.idTipoServicio= " + ddlServicio.SelectedValue;

            string m_strSQL = " SELECT HT.idHojaTrabajo AS idHojaTrabajo, A.nombre AS area, TS.nombre AS servicio, HT.codigo AS codigo" +
                              " FROM  LAB_HojaTrabajo AS HT INNER JOIN" +
                              " LAB_Area AS A ON HT.idArea = A.idArea INNER JOIN " +
                              " LAB_TipoServicio AS TS ON A.idTipoServicio = TS.idTipoServicio" +
                              " WHERE     (HT.baja = 0) AND (A.baja = 0) " + m_condicion +
                              " order by A.nombre";
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);

            return Ds.Tables[0];
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CargarGrilla();
               
            }

        }

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
         
        }

        
        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            
            foreach (GridViewRow row in gvLista.Rows)
            {

                CheckBox a = ((CheckBox)(row.Cells[0].FindControl("CheckBox1")));
                if (a.Checked == true)
                {
                    VistaPreeliminar(gvLista.DataKeys[row.RowIndex].Value.ToString(), "Impresora");                    
                }
            }
            

         
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtFechaDesde.Value == "")
                args.IsValid = false;
            else                            
            if (txtFechaHasta.Value == "") args.IsValid = false;
            else args.IsValid = true;

        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pnlHojaTrabajo.Visible)
            CargarGrilla();
            if (pnlAnalisisFueraHT.Visible)
            CargarAnalisisFueraHT(); 
          //  gvLista.UpdateAfterCallBack = true;
        }

        protected void lnkMarcar_Click(object sender, EventArgs e)
        {
            MarcarSeleccionados(true);
        }

        protected void lnkDesmarcar_Click(object sender, EventArgs e)
        {
            MarcarSeleccionados(false);
        }

        protected void lnkImprimirAnalisisFueraHT_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                VistaPreeliminar_AnalisisFueraHT ("Impresora");    
            }
        }

        protected void lnkPDFAnalisisFueraHT_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                VistaPreeliminar_AnalisisFueraHT("PDF");
            }
        }

        protected void lnkDesmarcarAnalisis_Click(object sender, EventArgs e)
        {
            MarcarSeleccionadosAnalisis(false);
        }

        private void MarcarSeleccionadosAnalisis(bool p)
        {
            for (int i = 0; i < lstAnalisis.Items.Count; i++)
            {
                lstAnalisis.Items[i].Selected = p;
            }
                
                
        }

        protected void lnkMarcarAnalisis_Click(object sender, EventArgs e)
        {
            MarcarSeleccionadosAnalisis(true);
        }

        protected void btnImprimirEtiqueta_Click(object sender, EventArgs e)
        {
            GenerarEtiquetas();
        }


        private void GenerarEtiquetas()
        {

            TipoServicio oTipoServicio= new TipoServicio();
            oTipoServicio = (TipoServicio)oTipoServicio.Get(typeof(TipoServicio), int.Parse(ddlServicio.SelectedValue));                           

            DataTable dt = new DataTable();
            dt = GetDataSet_Etiquetas();

            if (dt.Rows.Count == 0)
            {
                string popupScript = "<script language='JavaScript'> alert('No se encontraron datos para los filtros de busqueda seleccionados'); </script>";
                Page.RegisterStartupScript("PopupScript", popupScript);
            }
            else
            {
                ConfiguracionCodigoBarra oConBarra = new ConfiguracionCodigoBarra(); oConBarra = (ConfiguracionCodigoBarra)oConBarra.Get(typeof(ConfiguracionCodigoBarra), "IdTipoServicio", oTipoServicio);
                string sFuenteBarCode = oConBarra.Fuente;
                bool imprimeProtocoloFecha = oConBarra.ProtocoloFecha;
                bool imprimeProtocoloOrigen = oConBarra.ProtocoloOrigen;
                bool imprimeProtocoloSector = oConBarra.ProtocoloSector;
                bool imprimeProtocoloNumeroOrigen=oConBarra.ProtocoloNumeroOrigen;
                bool imprimePacienteNumeroDocumento= oConBarra.PacienteNumeroDocumento;
                bool imprimePacienteApellido = oConBarra.PacienteApellido;
                bool imprimePacienteSexo = oConBarra.PacienteSexo;
                bool imprimePacienteEdad = oConBarra.PacienteEdad;


                foreach (DataRow item in dt.Rows)
                {                  

                    //DataRow reg = tabla.NewRow();
                    int reg_idProtocolo =int.Parse( item[0].ToString());
                    Business.Data.Laboratorio.Protocolo oProt = new Business.Data.Laboratorio.Protocolo();
                    oProt = (Business.Data.Laboratorio.Protocolo)oProt.Get(typeof(Business.Data.Laboratorio.Protocolo), reg_idProtocolo);
                    //reg_idArea"] = item[1].ToString();
                    string reg_numero = item[2].ToString();
                    string reg_area = item[3].ToString();
                    string reg_Fecha = item[4].ToString().Substring(0,10);
                    string reg_Origen = item[5].ToString();
                    string reg_Sector= item[6].ToString();
                    string reg_NumeroOrigen = item[7].ToString();
                    string reg_NumeroDocumento = oProt.IdPaciente.NumeroDocumento.ToString(); //item[8].ToString();
                    string reg_codificaHIV = item[9].ToString().ToUpper(); //.Substring(0,32-reg_NumeroOrigen.Length);
                    string reg_sexo = item[10].ToString();
                    string reg_edad = item[11].ToString();
                    //tabla.Rows.Add(reg);
                    //tabla.AcceptChanges();

                    string reg_apellido="";
                    if (reg_codificaHIV == "FALSE")
                        reg_apellido = oProt.IdPaciente.Apellido + " " + oProt.IdPaciente.Nombre;//  .Substring(0,20); SUBSTRING(Pac.apellido + ' ' + Pac.nombre, 0, 20) ELSE upper(P.sexo + substring(Pac.nombre, 1, 2) 
                    else
                        reg_apellido = reg_sexo + oProt.IdPaciente.Nombre.Substring(0, 2) + oProt.IdPaciente.Apellido.Substring(0, 2) + oProt.IdPaciente.FechaNacimiento.ToShortDateString().Replace("/", "");

                    if  (!imprimeProtocoloFecha) reg_Fecha = "          ";
                    if (!imprimeProtocoloOrigen) reg_Origen = "          ";
                    if (!imprimeProtocoloSector) reg_Sector = "   ";
                    if (!imprimeProtocoloNumeroOrigen) reg_NumeroOrigen = "     ";
                    if (!imprimePacienteNumeroDocumento) reg_NumeroDocumento = "        ";
                    if (!imprimePacienteApellido) reg_apellido = "";
                    if (!imprimePacienteSexo) reg_sexo = " ";
                    if (!imprimePacienteEdad) reg_edad = "   ";
                    //ParameterDiscreteValue fuenteCodigoBarras = new ParameterDiscreteValue(); fuenteCodigoBarras.Value = oConBarra.Fuente;
                    
                    Business.Etiqueta ticket = new Business.Etiqueta();
                    if (reg_Origen.Length > 11) reg_Origen = reg_Origen.Substring(0, 10);

                    ticket.AddHeaderLine(reg_apellido.ToUpper());
                    ticket.AddSubHeaderLine(reg_sexo + " " +reg_edad + " "+ reg_NumeroDocumento + " " + reg_Fecha);
                    ticket.AddSubHeaderLine(reg_Origen + "  " + reg_NumeroOrigen);// reg_Sector);
                    ticket.AddSubHeaderLineNegrita(reg_area);
                    //ticket.AddSubHeaderLine(reg_area);

                    // falta pasar por parametro la fuente de codigo de barras
                    ticket.AddCodigoBarras( reg_numero,sFuenteBarCode);
                    ticket.AddFooterLine(reg_numero );
                    
                    
                    Session["Etiquetadora"] = ddlImpresora.SelectedValue;
                    //oCr.ReportDocument.PrintOptions.PrinterName = Session["Etiquetadora"].ToString();// ConfigurationSettings.AppSettings["Impresora"]; 
                    try
                    {
                        ticket.PrintTicket(ddlImpresora.SelectedValue, oConBarra.Fuente);
                    }
                    catch (Exception ex)
                    {
                        string exception = "";
                        //while (ex != null)
                        //{
                            exception = ex.Message + "<br>";

                        //}
                        string popupScript = "<script language='JavaScript'> alert('No se pudo imprimir en la impresora " + Session["Etiquetadora"].ToString() + ". Si el problema persiste consulte con soporte técnico." + exception + "'); </script>";
                        Page.RegisterStartupScript("PopupScript", popupScript);
                        break;
                    }
                    
                }
            }
        }


        private DataTable GetDataSet_Etiquetas()
        {

            string m_strSQL = "";
            ///////////////establecer los filtros.
            string s_condicion = "";
            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            s_condicion = " idTipoServicio=" + ddlServicio.SelectedValue;
            s_condicion += " AND Fecha>='" + fecha1.ToString("yyyyMMdd") + "' AND Fecha<='" + fecha2.ToString("yyyyMMdd") + "'";

            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

            //if (txtProtocoloDesde.Value != "") s_condicion += " And numero>=" +(txtProtocoloDesde.Value);
            //if (txtProtocoloHasta.Value != "") s_condicion += " AND numero<=" +(txtProtocoloHasta.Value);

            if (txtProtocoloDesde.Value != "")
            {
		        switch (oCon.TipoNumeracionProtocolo )
                {
                    case 0:	s_condicion += " and numero>=" +(txtProtocoloDesde.Value); break;
                    case 1:	s_condicion += " and numeroDiario>=" +(txtProtocoloDesde.Value); break;
                    case 2:	s_condicion += " and numeroSector>=" +(txtProtocoloDesde.Value); break;
                    case 3:	s_condicion += " and numeroTipoServicio>=" +(txtProtocoloDesde.Value); break;
                }
            }


            if (txtProtocoloHasta.Value != "")
            { 
                switch (oCon.TipoNumeracionProtocolo )
                {
                    case 0 : s_condicion += " and numero<="  +(txtProtocoloHasta.Value);break;
                    case 1 : s_condicion += " and numeroDiario<="  +(txtProtocoloHasta.Value);break;
                    case 2 : s_condicion += " and numeroSector<="  +(txtProtocoloHasta.Value); break;
                    case 3 : s_condicion += " and numeroTipoServicio<=" + (txtProtocoloHasta.Value); break;
                }
            }

            
            if (ddlEfector.SelectedValue != "0") s_condicion += " AND idEfectorSolicitante=" + ddlEfector.SelectedValue;
            if (ddlOrigen.SelectedValue != "0") s_condicion += " AND idOrigen=" + ddlOrigen.SelectedValue;
            if (ddlPrioridad.SelectedValue != "0") s_condicion += " AND idPrioridad=" + ddlPrioridad.SelectedValue;

            if (rdbEstadoAnalisis.SelectedValue == "1") s_condicion += " AND conResultado=0 ";

            string s_sectores = getListaSectores(true);
            if (s_sectores!="")   s_condicion += " AND idSector in (" + getListaSectores(true) + ")";

            if (ddlServicio.SelectedValue == "3")
            {
                string listaM = getListaMuestra();
                if (listaM!="") s_condicion += " AND idMuestra in (" + listaM + ")";
            }
            
            ////////////////////////

            if (ddlArea.SelectedValue != "0")
            {
                //s_condicion += " AND idDestinoEtiqueta=" + ddlArea.SelectedValue;
                
                s_condicion += " AND idArea=" + ddlArea.SelectedValue;

                m_strSQL = @" SELECT distinct [idProtocolo] ,[idArea] ,[numeroP],[area] ,[Fecha],[Origen] ,[Sector],[NumeroOrigen],[NumeroDocumento],[apellido],[Sexo],[edad]
                FROM vta_LAB_GeneraCodigoBarras
                WHERE   " + s_condicion;
            }
            if (ddlArea.SelectedValue=="0")
            {
                m_strSQL += @"   SELECT [idProtocolo],[idArea],[numeroP],[area],[Fecha],[Origen],[Sector],[NumeroOrigen],[NumeroDocumento]      ,[apellido]      ,[Sexo]      ,[edad]
                FROM vta_LAB_GeneraCodigoBarrasGeneral                    
                WHERE   " + s_condicion;
            }

            m_strSQL += " ORDER BY area";

            
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds, "resultado");

            
            DataTable data = Ds.Tables[0];
            return data;
        
        }

        protected void cvNumeros_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Utility oUtil = new Utility();

            if (txtProtocoloDesde.Value != "")
            {
                if (oUtil.EsEntero(txtProtocoloDesde.Value))
                {
                    if (txtProtocoloHasta.Value != "")
                    {
                        if (oUtil.EsEntero(txtProtocoloHasta.Value)) args.IsValid = true; else args.IsValid = false;
                    }
                }
                else args.IsValid = false;
            }
            else
            {
                if (txtProtocoloHasta.Value != "")
                {
                    if (oUtil.EsEntero(txtProtocoloHasta.Value)) args.IsValid = true; else args.IsValid = false;
                }
                else args.IsValid = true;
            }

            
        }
      

        }
    }

