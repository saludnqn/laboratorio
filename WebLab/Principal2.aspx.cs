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
using Business.Data.Laboratorio;
using Business;
using System.Data.SqlClient;

namespace WebLab
{
    public partial class Principal2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
             if (!Page.IsPostBack)
            {
             
                if (Session["s_permiso"] == null)Response.Redirect("FinSesion.aspx");
                
                Business.Data.Laboratorio.Protocolo oP = new Business.Data.Laboratorio.Protocolo();
                Configuracion oCon = new Configuracion();oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

                switch (oCon.TipoNumeracionProtocolo )
                {
                    case 0:  ///autonumerico
                    lblProximoProtocolo1.Text= oP.GenerarNumero().ToString(); break;
                    case 1:  //numero diario
                        lblProximoProtocolo1.Text = oP.GenerarNumeroDiario(DateTime.Now.ToString("yyyyMMdd")).ToString(); break;
                    case 2: // numero por sector
                        {                            
                            lblProximoProtocolo1.Visible = false;
                            lnkUltimoNumeroSector.Visible = true;
                        } break;

                    case 3: // numero por Tipo de Servicio
                        {
                            lblProximoProtocolo1.Visible = false;
                            lnkUltimoNumeroSector.Visible = true;
                        } break;



                }
                lblProtocoloPendiente.Text = oP.GetPendientesImprimir().ToString();
                lblResultadoPendiente.Text = oP.GetResultadosPendientes().ToString();

                lblValidaPendiente.Text = oP.GetPendientesValidar().ToString();
                pnlRecepcion.Visible = false;

                if (!oCon.Turno)
                {
                    pnlTurno.Visible = false;
                    pnlTurnoRecepcion.Visible = false;
                }
                else
                {
                    if (VerificaPermisos("Pacientes con turno") == 0)
                        pnlTurnoRecepcion.Visible = false;
                    else
                        pnlTurnoRecepcion.Visible = oCon.PrincipalRecepcion;
                    if (VerificaPermisos("Asignacion de turnos") == 0)
                        pnlTurno.Visible = false;
                    else
                        pnlTurno.Visible = oCon.PrincipalTurno;
                }


                if  (VerificaPermisos("Pacientes sin turno") == 0) 
                    pnlRecepcion.Visible = false;
                else
                    pnlRecepcion.Visible = oCon.PrincipalRecepcion;

                //    else
                //    {
                //        if (VerificaPermisos("Pacientes con turno") == 0)
                //            pnlTurnoRecepcion.Visible = false;
                //        else
                //            pnlTurnoRecepcion.Visible = oCon.PrincipalRecepcion;

                //        if (VerificaPermisos("Pacientes sin turno") == 0) 
                //            pnlProtocolo.Visible = false;
                //        else
                //            pnlProtocolo.Visible = oCon.PrincipalRecepcion;

                    
                //    }
                


                 //Si no tiene permisos directamente no se muestra
                 //Si tiene permisos y si está habilitado el acceso directo se muestra
                if (VerificaPermisos("Hoja de Trabajo") == 0)
                    pnlHojaTrabajo.Visible = false;
                else
                    pnlHojaTrabajo.Visible = oCon.PrincipalImpresionHT;


                if (VerificaPermisos("Carga") == 0) pnlCargaResultado.Visible = false;
                else pnlCargaResultado.Visible = oCon.PrincipalCargaResultados;

                if (VerificaPermisos("Validacion") == 0)   pnlValidacion.Visible = false;
                else pnlValidacion.Visible = oCon.PrincipalValidacion;

                if (VerificaPermisos("Impresión") == 0)                    pnlImpresion.Visible = false;
                else pnlImpresion.Visible = oCon.PrincipalImpresionResultados;

          
                 ///Para que el usuario acceda al modulo urgencias deberá tener permisos a la carga de protocolo,
                 ///carga de resultados y validacion.
                if ((VerificaPermisos("Pacientes sin turno") == 0)&& (VerificaPermisos("Carga") == 0)&&(VerificaPermisos("Validacion") == 0)) ///Urgencias
                    pnlUrgencia.Visible = false;
                else
                    pnlUrgencia.Visible = oCon.PrincipalUrgencias;

                 ///Agregar tambien el acceso a evolucion por analisis e historial (solapa antecedentes de resultados)
                if (VerificaPermisos("Historial de Resultados") == 0) pnlResultados.Visible = false;
                else pnlResultados.Visible = oCon.PrincipalResultados;


                if (VerificaPermisos("Mensajes Internos") != 0)
                    MostrarMensajes();
                else
                    mensajeria.Visible = false;
                //if (VerificaPermisos("Historial Por Analisis") == 0) pnlResultadoAnalisis.Visible = false;
                //else pnlResultadoAnalisis.Visible = oCon.PrincipalResultados;   

                if (VerificaPermisos("Peticion Electronica") != 0)
                    MostrarPeticionesPendientes();
                else
                    dlstPeticiones.Visible = false;

            }

        }

        private void MostrarPeticionesPendientes()
        {
            dlstPeticiones.Visible = false;
            dlstPeticiones.DataSource = LeerDatosPeticiones();
            dlstPeticiones.DataBind();
            if (dlstPeticiones.Items.Count > 0) dlstPeticiones.Visible = true;
        }

        private object LeerDatosPeticiones()
        {
            string m_strSQL = @" SELECT P.idPeticion, P.fechaRegistro ,Pac.apellido+ ' ' +  Pac.nombre as paciente FROM  dbo.LAB_Peticion  as P 
 inner join sys_paciente as Pac on P.idPaciente= Pac.idPaciente 
 where P.idProtocolo=0 ";
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);

            return Ds.Tables[0];


        }



        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            HyperLink oHplInfo = (HyperLink)e.Item.FindControl("hplMensajeEdit");
            if (oHplInfo != null)
            {
                string s_idMensaje = oHplInfo.NavigateUrl;
                oHplInfo.NavigateUrl = "MensajeEdit.aspx?idMensaje=" + s_idMensaje + "&Operacion=Eliminar";

                MensajeInterno oMensaje = new MensajeInterno();
                oMensaje = (MensajeInterno)oMensaje.Get(typeof(MensajeInterno), int.Parse(s_idMensaje));
                if (oMensaje.IdUsuarioRegistro.ToString() == Session["idUsuario"].ToString())
                    oHplInfo.Visible = true;
                else
                    oHplInfo.Visible = false;
            }
        }
        protected void dlstPeticiones_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            HyperLink oHplInfo = (HyperLink)e.Item.FindControl("hplPeticionEdit");
            if (oHplInfo != null)
            {
                string s_idPeticion = oHplInfo.NavigateUrl;
                Business.Data.Laboratorio.Peticion oRegistro = new Business.Data.Laboratorio.Peticion();
                oRegistro = (Business.Data.Laboratorio.Peticion)oRegistro.Get(typeof(Business.Data.Laboratorio.Peticion), int.Parse(s_idPeticion));

                oHplInfo.NavigateUrl = "Protocolos/ProtocoloEdit2.aspx?idPaciente=" + oRegistro.IdPaciente.IdPaciente.ToString() + "&Operacion=AltaPeticion&idServicio=" + oRegistro.IdTipoServicio.IdTipoServicio.ToString() + "&idPeticion=" + oRegistro.IdPeticion.ToString();

            }
        }

        private void MostrarMensajes()
        {
            BorrarMensajes();
            mensajeria.Visible = true;
            CargarGrilla();
        }

        private void BorrarMensajes()
        {
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter da = new SqlDataAdapter();
            string m_ssql = @" Delete from  LAB_Mensaje where fechaHoraRegistro <'" + DateTime.Now.AddDays(-5).ToString("yyyyMMdd") + "'"; 
            SqlCommand cmdUpdate = new SqlCommand(m_ssql, conn);
            da.InsertCommand = cmdUpdate;
            da.InsertCommand.ExecuteNonQuery();
                                                          
        }
        
        private void CargarGrilla()
        {
            DataList1.DataSource = LeerDatos(); DataList1.DataBind();
        }

        private object LeerDatos()
        {
            string m_strSQL = " SELECT     TOP (10) idMensaje, fechaHoraRegistro, mensaje, destinatario, remitente " +
            " FROM         dbo.LAB_Mensaje ORDER BY IDMENSAJE DESC";
            DataSet Ds = new DataSet();
                
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);                
            return Ds.Tables[0];     
            
           
        }

        private int VerificaPermisos(string sObjeto)
        {
            int i_permiso = 0;
            
                Utility oUtil = new Utility();
                 i_permiso = oUtil.VerificaPermisos((ArrayList)Session["s_permiso"], sObjeto);
                return i_permiso;    
          
        }

        protected void lnkUltimoNumeroSector_Click(object sender, EventArgs e)
        {
            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
            Response.Redirect("ProximoNumeroList.aspx?tipo="+ oCon.TipoNumeracionProtocolo.ToString(), false);
        }

        protected void imgAgregarMensaje_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MensajeEdit.aspx?Operacion=Alta", false);
           
        }
        

     
      
    }
}
