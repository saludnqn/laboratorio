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
using NHibernate;
using Business;
using NHibernate.Expression;
using System.Drawing;
using System.Data.SqlClient;
using Business.Data;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

namespace WebLab
{
    public partial class ParametrosEdit : System.Web.UI.Page
    {
        private enum TabIndex
        {  
            DEFAULT = 0,    //General
            ONE = 1,        //Protocolos
            TWO = 2,        //Turnos
            THREE = 3,      //Calendario
            CUARTO = 4,     //Carga/Validación Resultados
            QUINTO = 5,     //Laboratorio General
            SIX=6 ,         //Microbiología
            SEVEN=7         //Impresoras  
        }
        private void SetSelectedTab(TabIndex tabIndex)
        {
            HFCurrTabIndex.Value = ((int)tabIndex).ToString();
        }
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
                VerificaPermisos("Parametros SI");                              
                CargarListas();
                MostrarDatos();           
            }

        }
        protected void btnGuardarImpresora_Click(object sender, EventArgs e)
        {
            GuardarImpresoras();
            SetSelectedTab(TabIndex.SEVEN);

        }

        private void GuardarImpresoras()
        {
            if (lstImpresora.Items.Count > 0)
            {
                ////borra las impresoras guardadas
                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit2 = m_session.CreateCriteria(typeof(Impresora));
                IList listaImpresoras = crit2.List();
                foreach (Impresora oImpr in listaImpresoras)
                {
                    oImpr.Delete();
                }

                /////Crea nuevamente los detalles.
                for (int i = 0; i < lstImpresora.Items.Count; i++)
                {
                    Impresora oRegistro = new Impresora();
                    oRegistro.Nombre = lstImpresora.Items[i].Value;
                    oRegistro.IdUsuarioRegistro = int.Parse(Session["idUsuario"].ToString());
                    oRegistro.FechaRegistro = DateTime.Now;
                    oRegistro.Save();
                }
            }
        }
        protected void btnAgregarImpresora_Click(object sender, EventArgs e)
        {
            if (!VerificarExisteImpresoras())
            {
                ListItem oImpresora = new ListItem();
                oImpresora.Text = ddlImpresora.SelectedValue;
                oImpresora.Value = ddlImpresora.SelectedValue;
                lstImpresora.Items.Add(oImpresora);
            }
            lstImpresora.UpdateAfterCallBack = true;
            SetSelectedTab(TabIndex.SEVEN);
        }

        protected void btnSacarImpresora_Click(object sender, EventArgs e)
        {
            if (lstImpresora.SelectedValue != "")
            {
                lstImpresora.Items.Remove(lstImpresora.SelectedItem);               
            }


            lstImpresora.UpdateAfterCallBack = true;
            SetSelectedTab(TabIndex.SEVEN);
        }
        private bool VerificarExisteImpresoras()
        {
            bool existe = false;
            if (lstImpresora.Items.Count > 0)
            {
                /////Crea nuevamente los detalles.
                for (int i = 0; i < lstImpresora.Items.Count; i++)
                {
                    if (ddlImpresora.SelectedValue == lstImpresora.Items[i].Value)
                    {
                        existe = true; break;
                    }
                }
            }
            return existe;
        }

        private void MostrarImpresoras()
        {
            ISession m_session = NHibernateHttpModule.CurrentSession;

            Impresora oRegistro = new Impresora();
            ICriteria crit2 = m_session.CreateCriteria(typeof(Impresora));

            IList listaImpresoras = crit2.List();

            foreach (Impresora oImpr in listaImpresoras)
            {
                ListItem oItem = new ListItem();
                oItem.Text = oImpr.Nombre;
                oItem.Value = oImpr.Nombre;
                lstImpresora.Items.Add(oItem);
            }
        }

        private void VerificaPermisos(string sObjeto)
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("FinSesion.aspx", false); 
            }
            else
            {
                if (Session["s_permiso"] != null)
                {
                    Utility oUtil = new Utility();
                    int i_permiso = oUtil.VerificaPermisos((ArrayList)Session["s_permiso"], sObjeto);
                    switch (i_permiso)
                    {
                        case 0: Response.Redirect("AccesoDenegado.aspx", false); break;
                        case 1: btnGuardar.Visible = false; break;
                    }
                }
                else Response.Redirect("FinSesion.aspx", false);
            }
        }


        private void CargarListas()
        {
            Utility oUtil = new Utility();          

            ///Carga de Sectores
            string m_ssql = " SELECT idSectorServicio,  nombre  + ' - ' + prefijo as nombre FROM LAB_SectorServicio WHERE (baja = 0) order by nombre";
            oUtil.CargarCombo(ddlSectorUrgencia, m_ssql, "idSectorServicio", "nombre");
            ddlSectorUrgencia.Items.Insert(0, new ListItem("", "0"));

            ///Carga de combos de Origen
            m_ssql = "SELECT  idOrigen, nombre FROM LAB_Origen WHERE (baja = 0)";
            oUtil.CargarCombo(ddlOrigenUrgencia, m_ssql, "idOrigen", "nombre");
            ddlOrigenUrgencia.Items.Insert(0, new ListItem("", "0"));

            foreach (string MiImpresora in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                ddlImpresora.Items.Add(MiImpresora);
            }

            m_ssql = null;
            oUtil = null;
        }


        private void MostrarDatosCodigoBarrasLaboratorio()
        {
            TipoServicio oTipo = new TipoServicio();
            oTipo = (TipoServicio)oTipo.Get(typeof(TipoServicio), 1);

            ConfiguracionCodigoBarra oRegistro = new ConfiguracionCodigoBarra();
            oRegistro = (ConfiguracionCodigoBarra)oRegistro.Get(typeof(ConfiguracionCodigoBarra), "IdTipoServicio", oTipo);

            if (oRegistro == null)
            {
                chkImprimeCodigoBarrasLaboratorio.Checked = false;
                pnlLaboratorio.Enabled = false;
            }
            else
            {
                chkImprimeCodigoBarrasLaboratorio.Checked = oRegistro.Habilitado;
                pnlLaboratorio.Enabled = chkImprimeCodigoBarrasLaboratorio.Checked;
                ddlFuente.SelectedValue = oRegistro.Fuente;
                chkProtocolo.Items[1].Selected = oRegistro.ProtocoloFecha;
                chkProtocolo.Items[2].Selected = oRegistro.ProtocoloOrigen;

                chkProtocolo.Items[3].Selected = oRegistro.ProtocoloSector;
                chkProtocolo.Items[4].Selected = oRegistro.ProtocoloNumeroOrigen;

                chkPaciente.Items[0].Selected = oRegistro.PacienteApellido;
                chkPaciente.Items[1].Selected = oRegistro.PacienteSexo;
                chkPaciente.Items[2].Selected = oRegistro.PacienteEdad;
                chkPaciente.Items[3].Selected = oRegistro.PacienteNumeroDocumento;



            }
        }

        private void MostrarDatosCodigoBarrasMicrobiologia()
        {
            TipoServicio oTipo = new TipoServicio();
            oTipo = (TipoServicio)oTipo.Get(typeof(TipoServicio), 3);

            ConfiguracionCodigoBarra oRegistro = new ConfiguracionCodigoBarra();
            oRegistro = (ConfiguracionCodigoBarra)oRegistro.Get(typeof(ConfiguracionCodigoBarra), "IdTipoServicio", oTipo);

            if (oRegistro == null)
            {
                chkImprimeCodigoBarrasMicrobiologia.Checked = false;
                pnlMicrobiologia.Enabled = false;
            }
            else
            {
                chkImprimeCodigoBarrasMicrobiologia.Checked = oRegistro.Habilitado;
                pnlMicrobiologia.Enabled = chkImprimeCodigoBarrasMicrobiologia.Checked;
                rdbFuente2.SelectedValue = oRegistro.Fuente;
                chkProtocolo2.Items[1].Selected = oRegistro.ProtocoloFecha;
                chkProtocolo2.Items[2].Selected = oRegistro.ProtocoloOrigen;

                chkProtocolo2.Items[3].Selected = oRegistro.ProtocoloSector;
                chkProtocolo2.Items[4].Selected = oRegistro.ProtocoloNumeroOrigen;

                chkPaciente2.Items[0].Selected = oRegistro.PacienteApellido;
                chkPaciente2.Items[1].Selected = oRegistro.PacienteSexo;
                chkPaciente2.Items[2].Selected = oRegistro.PacienteEdad;
                chkPaciente2.Items[3].Selected = oRegistro.PacienteNumeroDocumento;



            }
        }


        private void MostrarDatosCodigoBarrasPesquisa()
        {
            TipoServicio oTipo = new TipoServicio();
            oTipo = (TipoServicio)oTipo.Get(typeof(TipoServicio), 4);

            ConfiguracionCodigoBarra oRegistro = new ConfiguracionCodigoBarra();
            oRegistro = (ConfiguracionCodigoBarra)oRegistro.Get(typeof(ConfiguracionCodigoBarra), "IdTipoServicio", oTipo);

            if (oRegistro == null)
            {
                chkImprimeCodigoBarrasPesquisa.Checked = false;

                pnlPesquisa.Enabled = false;
            }
            else
            {
                chkImprimeCodigoBarrasPesquisa.Checked = oRegistro.Habilitado;
                pnlPesquisa.Enabled = chkImprimeCodigoBarrasPesquisa.Checked;
                rdbFuente3.SelectedValue = oRegistro.Fuente;
                chkProtocolo3.Items[1].Selected = oRegistro.ProtocoloFecha;
                chkProtocolo3.Items[2].Selected = oRegistro.ProtocoloOrigen;

                chkProtocolo3.Items[3].Selected = oRegistro.ProtocoloSector;
                chkProtocolo3.Items[4].Selected = oRegistro.ProtocoloNumeroOrigen;

                chkPaciente3.Items[0].Selected = oRegistro.PacienteApellido;
                chkPaciente3.Items[1].Selected = oRegistro.PacienteSexo;
                chkPaciente3.Items[2].Selected = oRegistro.PacienteEdad;
                chkPaciente3.Items[3].Selected = oRegistro.PacienteNumeroDocumento;



            }
        }
        private void MostrarDatos()
        {
            
            //Usuario oUser = new Usuario();
            //oUser=(Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
            
            Configuracion oC = new Configuracion();
            oC = (Configuracion)oC.Get(typeof(Configuracion),1); // "IdEfector", oUser.IdEfector);

           

            if (!SiNoHayProtocolosCargados()) { lblMensajeNumeracion.Visible = true; rdbTipoNumeracionProtocolo.Enabled = false; }
            lblZona.Text += oC.IdEfector.IdZona.Nombre;
            lblEfector.Text += oC.IdEfector.Nombre;

            if (oC.PeticionElectronica) ddlPeticionElectronica.SelectedValue = "1";
            else ddlPeticionElectronica.SelectedValue = "0";

           
            
            /////////Grupo referido al Comprobante para el paciente Protocolo//////////////
            if (!oC.GeneraComprobanteProtocolo)  ddlProtocoloComprobante.SelectedValue = "0";
            else ddlProtocoloComprobante.SelectedValue = "1";

            if (!oC.GeneraComprobanteProtocoloMicrobiologia) ddlProtocoloComprobanteMicrobiologia.SelectedValue = "0";
            else ddlProtocoloComprobanteMicrobiologia.SelectedValue = "1";

            txtTextoAdicionalComprobante.Text=oC.TextoAdicionalComprobanteProtocolo ;
            txtTextoAdicionalComprobanteMicrobiologia.Text = oC.TextoAdicionalComprobanteProtocoloMicrobiologia;

            /////////////////////fin/////////////////////////

            ////Accesos directos de la pantalla principal

            chkAccesoPrincipal.Items[0].Selected =oC.PrincipalTurno ;
            chkAccesoPrincipal.Items[1].Selected = oC.PrincipalRecepcion;
            chkAccesoPrincipal.Items[2].Selected = oC.PrincipalImpresionHT;
            chkAccesoPrincipal.Items[3].Selected = oC.PrincipalCargaResultados;
            chkAccesoPrincipal.Items[4].Selected = oC.PrincipalValidacion;
            chkAccesoPrincipal.Items[5].Selected = oC.PrincipalImpresionResultados;
            chkAccesoPrincipal.Items[6].Selected = oC.PrincipalUrgencias;
            chkAccesoPrincipal.Items[7].Selected = oC.PrincipalResultados;


           

            rdbTipoNumeracionProtocolo.Items[oC.TipoNumeracionProtocolo].Selected = true;

          

                ////dias de entrega            
            if (oC.TipoCalculoDiasRetiro == 0)
                rdbDiasEspera.Items[0].Selected = true;
            else
                rdbDiasEspera.Items[1].Selected = true;


            txtDiasEntrega.Value = oC.DiasRetiro.ToString();
            
            HabilitarValidadorDias();


            CalendarioEntrega oItem = new CalendarioEntrega();
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(CalendarioEntrega));
            crit.Add(Expression.Eq("IdEfector", oC.IdEfector));

            IList items = crit.List();
            foreach (CalendarioEntrega oDia in items)
            {
                int i = oDia.Dia;
                cklDias.Items[i - 1].Selected = true;              
            }

            /////////////////////////////////////////////////

            /////Recordar el ultimo origen cargado ///////
            if (oC.RecordarOrigenProtocolo) ddlRecordarOrigenProtocolo.SelectedValue = "1" ;
            else ddlRecordarOrigenProtocolo.SelectedValue = "0";
            //////////////////////////////////////////////

            /////Recordar el ultimo sector cargado ///////
            if (oC.RecordarSectorProtocolo) ddlRecordarSectorProtocolo.SelectedValue = "1";
            else ddlRecordarSectorProtocolo.SelectedValue = "0";
            //////////////////////////////////////////////


            ///Tamaño maximo de las paginas de la lista de protocolos            
            ddlPaginadoProtocolo.SelectedValue = oC.CantidadProtocolosPorPagina.ToString();
            ///////////

            /////modificar el protocolo terminado ///////
            if ( oC.ModificarProtocoloTerminado) ddlModificaProtocoloTerminado.SelectedValue = "1";
            else ddlModificaProtocoloTerminado.SelectedValue = "0";
            //////////////////////////////////////////////

            /////eliminar el protocolo terminado ///////
            if (oC.EliminarProtocoloTerminado) ddlEliminaProtocoloTerminado.SelectedValue = "1";
            else ddlEliminaProtocoloTerminado.SelectedValue = "0";
            //////

            ////Modulo Urgencia
            ddlOrigenUrgencia.SelectedValue = oC.IdOrigenUrgencia.ToString();
            ddlSectorUrgencia.SelectedValue = oC.IdSectorUrgencia.ToString();
            //////////

            /////////////Grupo referido al Turno/////////////
            if (!oC.Turno)
                ddlTurno.SelectedValue = "0";
            else
                ddlTurno.SelectedValue = "1";

            if (!oC.GeneraComprobanteTurno)
                ddlTurnoComprobante.SelectedValue = "0";
            else
                ddlTurnoComprobante.SelectedValue = "1";

            if (!oC.SmsCancelaTurno) ddlSmsCancelaTurno.SelectedValue = "0";
            else ddlSmsCancelaTurno.SelectedValue = "1";
            ////////////////////////////////////////////////

            /////Formato de la Lista de Protocolos ///////
            rdbTipoListaProtocolo.SelectedValue = oC.TipoListaProtocolo.ToString();
        
            //////////////////////////////////////////////

            //////////Formato de la Hoja de Trabajo///////////
            //if (oC.TipoHojaTrabajo == 0)
            //{
            //    rdbHojaTrabajo.Items[0].Selected = true;
            //    rdbHojaTrabajo.Items[1].Selected = false;
            //}
            //else
            //{
            //    rdbHojaTrabajo.Items[0].Selected = false;
            //    rdbHojaTrabajo.Items[1].Selected = true;
            //}
            ////////////////////////////////////////////////


            //////////Formato de la Carga de Resultados//////
            switch (oC.TipoCargaResultado)
            {
                case 0:
            {
                rdbCargaResultados.Items[0].Selected = true;
                rdbCargaResultados.Items[1].Selected = false;
                rdbCargaResultados.Items[2].Selected = false;
            }
                    break;
                case 1:
            {
                rdbCargaResultados.Items[0].Selected = false;
                rdbCargaResultados.Items[1].Selected = true;
                rdbCargaResultados.Items[2].Selected = false;
            }
            break;
                case 2:
            {
                rdbCargaResultados.Items[0].Selected = false;
                rdbCargaResultados.Items[1].Selected = false;
                rdbCargaResultados.Items[2].Selected = true;
            }
            break;
        }

            if (!oC.OrdenCargaResultado)
            rdbOrdenCargaResultados.SelectedValue = "0";
            else
                rdbOrdenCargaResultados.SelectedValue = "1";
            /////////////////////////////////////////////////

            ///////Tipo de impresion de resultado///

            if (oC.TipoImpresionResultado)
            {
                rdbTipoImpresionResultado.Items[0].Selected = true;
                rdbTipoImpresionResultado.Items[1].Selected = false;
            }
            else
            {
                rdbTipoImpresionResultado.Items[0].Selected = false;
                rdbTipoImpresionResultado.Items[1].Selected = true;
            }

            if (oC.TipoImpresionResultadoMicrobiologia)
            {
                rdbTipoImpresionResultadoMicrobiologia.Items[0].Selected = true;
                rdbTipoImpresionResultadoMicrobiologia.Items[1].Selected = false;
            }
            else
            {
                rdbTipoImpresionResultadoMicrobiologia.Items[0].Selected = false;
                rdbTipoImpresionResultadoMicrobiologia.Items[1].Selected = true;
            }

            ddlTipoHojaImpresionResultados.SelectedValue = oC.TipoHojaImpresionResultado;
            ddlTipoHojaImpresionResultadosMicrobiologia.SelectedValue = oC.TipoHojaImpresionResultadoMicrobiologia;
            //////////////////////////////////////////

            ///Aplicar formula por defecto
            if (oC.AplicarFormulaDefecto)ddlAplicaFormula.SelectedValue = "1";
            else ddlAplicaFormula.SelectedValue = "0";



            ////Datos a imprimir del Protocolo///////////////

            chkDatosProtocoloImprimir.Items[0].Selected = oC.ResultadoNumeroRegistro;
             chkDatosProtocoloImprimir.Items[1].Selected=oC.ResultadoFechaEntrega;
             chkDatosProtocoloImprimir.Items[2].Selected = oC.ResultadoSector;
             chkDatosProtocoloImprimir.Items[3].Selected = oC.ResultadoSolicitante ;
             chkDatosProtocoloImprimir.Items[4].Selected = oC.ResultadoOrigen;
             chkDatosProtocoloImprimir.Items[5].Selected = oC.ResultadoPrioridad;

             chkDatosProtocoloImprimirMicrobiologia.Items[0].Selected = oC.ResultadoNumeroRegistroMicrobiologia;
             chkDatosProtocoloImprimirMicrobiologia.Items[1].Selected = oC.ResultadoFechaEntregaMicrobiologia;
             chkDatosProtocoloImprimirMicrobiologia.Items[2].Selected = oC.ResultadoSectorMicrobiologia;
             chkDatosProtocoloImprimirMicrobiologia.Items[3].Selected = oC.ResultadoSolicitanteMicrobiologia;
             chkDatosProtocoloImprimirMicrobiologia.Items[4].Selected = oC.ResultadoOrigenMicrobiologia;
             chkDatosProtocoloImprimirMicrobiologia.Items[5].Selected = oC.ResultadoPrioridadMicrobiologia;
            /////////////////////////////////////////////////


            ////Datos a imprimir del Paciente///////////////

            chkDatosPacienteImprimir.Items[3].Selected = oC.ResultadoEdad; ///edad
            chkDatosPacienteImprimir.Items[4].Selected = oC.ResultadoFNacimiento; ///f.nacimiento
            chkDatosPacienteImprimir.Items[5].Selected = oC.ResultadoSexo; ///sexo
            chkDatosPacienteImprimir.Items[2].Selected = oC.ResultadoHC; ///hc
            chkDatosPacienteImprimir.Items[1].Selected = oC.ResultadoDNI; ///dni
            chkDatosPacienteImprimir.Items[6].Selected = oC.ResultadoDomicilio; ///domicilio

            chkDatosPacienteImprimirMicrobiologia.Items[3].Selected = oC.ResultadoEdadMicrobiologia;
            chkDatosPacienteImprimirMicrobiologia.Items[4].Selected = oC.ResultadoFNacimientoMicrobiologia;
            chkDatosPacienteImprimirMicrobiologia.Items[5].Selected = oC.ResultadoSexoMicrobiologia;
            chkDatosPacienteImprimirMicrobiologia.Items[2].Selected = oC.ResultadoHCMicrobiologia;
            chkDatosPacienteImprimirMicrobiologia.Items[1].Selected = oC.ResultadoDNIMicrobiologia;
            chkDatosPacienteImprimirMicrobiologia.Items[6].Selected = oC.ResultadoDomicilioMicrobiologia;
            
            /////////////////////////////////////////////////


            ////////firma electronica///////////
            ddlImprimePieResultados.SelectedValue = oC.FirmaElectronicaLaboratorio.ToString();
            ddlImprimePieResultadosMicrobiologia.SelectedValue = oC.FirmaElectronicaMicrobiologia.ToString();
            //if (oC.ResultadoImprimePie)ddlImprimePieResultados.SelectedValue = "1";
            //else ddlImprimePieResultados.SelectedValue = "0";

            ////////////////////////////////////////////////
            /////////Formato de Impresión///////////////////
            txtEncabezado1.Text = oC.EncabezadoLinea1;
            txtEncabezado2.Text = oC.EncabezadoLinea2;
            txtEncabezado3.Text = oC.EncabezadoLinea3;

            txtEncabezado1Microbiologia.Text = oC.EncabezadoLinea1Microbiologia;
            txtEncabezado2Microbiologia.Text = oC.EncabezadoLinea2Microbiologia;
            txtEncabezado3Microbiologia.Text = oC.EncabezadoLinea3Microbiologia;

            ////////////////////////////////////////////////

            ddlTipoHojaImpresionResultados.SelectedValue = oC.TipoHojaImpresionResultado;

            if (oC.RutaLogo != "")
            {
                Image1.Visible = true;
                Image1.ImageUrl = "~/Logo/" + oC.RutaLogo;
                
            }



            if (!oC.AutenticaValidacion)
                ddlAutenticaValidacion.SelectedValue = "0";
            else
                ddlAutenticaValidacion.SelectedValue = "1";


            ddlFechaOrden.SelectedValue = oC.ValorDefectoFechaOrden.ToString();
            ddlFechaTomaMuestra.SelectedValue = oC.ValorDefectoFechaTomaMuestra.ToString();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            MostrarDatosCodigoBarrasLaboratorio(); MostrarDatosCodigoBarrasMicrobiologia(); MostrarDatosCodigoBarrasPesquisa(); MostrarImpresoras();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            oC = null;
        }

        private bool SiNoHayProtocolosCargados()
        {
            bool dev=true;
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(Protocolo));            
            crit.Add(Expression.Sql(" IdProtocolo in (Select top 1 IdProtocolo From LAb_Protocolo where Baja=0)"));
            IList lista = crit.List();
            if (lista.Count > 0)
            {
                dev = false;
            }
            return dev;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            Response.Redirect("ParametrosMsje.aspx", false);
        }

        private void Guardar()
        {


            Usuario oUser = new Usuario();
            oUser = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));

            Configuracion oC = new Configuracion();
            oC = (Configuracion)oC.Get(typeof(Configuracion), "IdEfector", oUser.IdEfector);

            //oC.NombreImpresora=ddlImpresora.SelectedValue  ;

            ////Accesos directos de la pantalla principal

            oC.PrincipalTurno=chkAccesoPrincipal.Items[0].Selected  ;
            oC.PrincipalRecepcion=chkAccesoPrincipal.Items[1].Selected  ;
            oC.PrincipalImpresionHT=chkAccesoPrincipal.Items[2].Selected;
            oC.PrincipalCargaResultados=chkAccesoPrincipal.Items[3].Selected;
            oC.PrincipalValidacion=chkAccesoPrincipal.Items[4].Selected  ;
            oC.PrincipalImpresionResultados=chkAccesoPrincipal.Items[5].Selected;
             oC.PrincipalUrgencias=chkAccesoPrincipal.Items[6].Selected ;
             oC.PrincipalResultados=chkAccesoPrincipal.Items[7].Selected ;

            //////////////////////////////


            
            /////////Grupo referido al comprobante para el paciente//////////////
            if (ddlProtocoloComprobante.SelectedValue == "0") oC.GeneraComprobanteProtocolo = false;
            else  oC.GeneraComprobanteProtocolo = true;

            if (ddlProtocoloComprobanteMicrobiologia.SelectedValue == "0") oC.GeneraComprobanteProtocoloMicrobiologia = false;
            else  oC.GeneraComprobanteProtocoloMicrobiologia = true;

            oC.TextoAdicionalComprobanteProtocolo = txtTextoAdicionalComprobante.Text;
            oC.TextoAdicionalComprobanteProtocoloMicrobiologia = txtTextoAdicionalComprobanteMicrobiologia.Text;
            
            ///////////////fin/////////////////

            if (ddlPeticionElectronica.SelectedValue == "1") oC.PeticionElectronica = true;
            else oC.PeticionElectronica = false;



            if (rdbTipoNumeracionProtocolo.Items[0].Selected) oC.TipoNumeracionProtocolo=0;
            if (rdbTipoNumeracionProtocolo.Items[1].Selected) oC.TipoNumeracionProtocolo = 1;
            if (rdbTipoNumeracionProtocolo.Items[2].Selected) oC.TipoNumeracionProtocolo = 2;
            if (rdbTipoNumeracionProtocolo.Items[3].Selected) oC.TipoNumeracionProtocolo = 3;

            ///////utilizaNumeroEliminado ///////
            //if (ddlUtilizarNumeroEliminado.SelectedValue == "0") oC.UtilizaNumeroEliminado = false;
            //else oC.UtilizaNumeroEliminado = true;
            ////////////////////////////////////////////////


            ////dias de entrega
            if (rdbDiasEspera.Items[0].Selected)  //Calcula segun duración de analisis
            {
                oC.TipoCalculoDiasRetiro = 0;
                oC.DiasRetiro = 0;
            }
            else  //valor predeterminado
            {
                oC.TipoCalculoDiasRetiro = 1;
                oC.DiasRetiro = int.Parse(txtDiasEntrega.Value);
            }


            ///Calendario de Entregas            


            ///Primero borra lo que hay
            CalendarioEntrega oItem = new CalendarioEntrega();
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(CalendarioEntrega));
            crit.Add(Expression.Eq("IdEfector", oC.IdEfector));

            IList items = crit.List();
            foreach (CalendarioEntrega oDia in items)
            {
                oDia.Delete();
            }

            ///
            ///Escribe los nuevos datos
            for (int i = 0; i < cklDias.Items.Count; i++)
            {
                if (cklDias.Items[i].Selected)
                {
                    CalendarioEntrega oDia = new CalendarioEntrega();
                    oDia.IdEfector = oC.IdEfector;
                    oDia.Dia = i + 1;
                    oDia.Save();
                }
            }
            /////////////////////////////////////////////////

            /////Recordar el ultimo origen cargado ///////
            if (ddlRecordarOrigenProtocolo.SelectedValue=="0") oC.RecordarOrigenProtocolo=false;
            else oC.RecordarOrigenProtocolo = true;
            //////////////////////////////////////////////

            /////Recordar el ultimo sector cargado ///////
            if (ddlRecordarSectorProtocolo.SelectedValue == "0") oC.RecordarSectorProtocolo = false;
            else oC.RecordarSectorProtocolo = true;
            //////

            ///Tamaño maximo de las paginas de la lista de protocolos            
            oC.CantidadProtocolosPorPagina =int.Parse( ddlPaginadoProtocolo.SelectedValue);

            ///////////

            /////modificar el protocolo terminado ///////
            if (ddlModificaProtocoloTerminado.SelectedValue == "0") oC.ModificarProtocoloTerminado = false;
            else oC.ModificarProtocoloTerminado = true;
            //////////////////////////////////////////////

            /////eliminar el protocolo terminado ///////
            if (ddlEliminaProtocoloTerminado.SelectedValue == "0") oC.EliminarProtocoloTerminado = false;
            else oC.EliminarProtocoloTerminado= true;
            //////

            ////Modulo Urgencia
            oC.IdOrigenUrgencia=int.Parse( ddlOrigenUrgencia.SelectedValue);
            oC.IdSectorUrgencia = int.Parse( ddlSectorUrgencia.SelectedValue);
            //////////


            /////////////Grupo referido al Turno/////////////
            if (ddlTurno.SelectedValue == "0") oC.Turno = false;
            else oC.Turno = true;


            if (ddlTurnoComprobante.SelectedValue == "0") oC.GeneraComprobanteTurno = false;
            else  oC.GeneraComprobanteTurno = true;

            if (ddlSmsCancelaTurno.SelectedValue == "0") oC.SmsCancelaTurno = false;
            else
                oC.SmsCancelaTurno = true;
            ////////////////////////////////////////////////

            /////Formato de la Lista de Protocolos ///////
            oC.TipoListaProtocolo=int.Parse( rdbTipoListaProtocolo.SelectedValue);
            
 
            //////////////////////////////////////////////


            ////////Formato de la Hoja de Trabajo///////////
            //if (rdbHojaTrabajo.Items[0].Selected)  oC.TipoHojaTrabajo = 0;
            //else    oC.TipoHojaTrabajo = 1;
            
            ////////////////////////////////////////////////


         

            oC.TipoCargaResultado =int.Parse( rdbCargaResultados.SelectedValue);
            if (rdbOrdenCargaResultados.SelectedValue == "0")
                oC.OrdenCargaResultado = false;
            else
                oC.OrdenCargaResultado = true;
            /////////////////////////////////////////////////

            ///////Tipo de impresion de resultado///

            oC.TipoImpresionResultado = rdbTipoImpresionResultado.Items[0].Selected;
            oC.TipoHojaImpresionResultado = ddlTipoHojaImpresionResultados.SelectedValue.Trim();

            oC.TipoImpresionResultadoMicrobiologia = rdbTipoImpresionResultadoMicrobiologia.Items[0].Selected;
            oC.TipoHojaImpresionResultadoMicrobiologia = ddlTipoHojaImpresionResultadosMicrobiologia.SelectedValue.Trim();
         //   oC.tipo
            //////////////////////////////////////////


            ///Aplicar formula por defecto ///
            
            if (ddlAplicaFormula.SelectedValue == "0") oC.AplicarFormulaDefecto = false;
            else oC.AplicarFormulaDefecto = true;


            ////Datos a imprimir del Protocolo///////////////

            oC.ResultadoNumeroRegistro =chkDatosProtocoloImprimir.Items[0].Selected; 
            oC.ResultadoFechaEntrega =chkDatosProtocoloImprimir.Items[1].Selected; 
            oC.ResultadoSector=chkDatosProtocoloImprimir.Items[2].Selected; 
            oC.ResultadoSolicitante=chkDatosProtocoloImprimir.Items[3].Selected;
            oC.ResultadoOrigen=chkDatosProtocoloImprimir.Items[4].Selected; 
            oC.ResultadoPrioridad =chkDatosProtocoloImprimir.Items[5].Selected;


            oC.ResultadoNumeroRegistroMicrobiologia = chkDatosProtocoloImprimirMicrobiologia.Items[0].Selected;
            oC.ResultadoFechaEntregaMicrobiologia = chkDatosProtocoloImprimirMicrobiologia.Items[1].Selected;
            oC.ResultadoSectorMicrobiologia = chkDatosProtocoloImprimirMicrobiologia.Items[2].Selected;
            oC.ResultadoSolicitanteMicrobiologia = chkDatosProtocoloImprimirMicrobiologia.Items[3].Selected;
            oC.ResultadoOrigenMicrobiologia = chkDatosProtocoloImprimirMicrobiologia.Items[4].Selected;
            oC.ResultadoPrioridadMicrobiologia = chkDatosProtocoloImprimirMicrobiologia.Items[5].Selected;
            /////////////////////////////////////////////////


            ////Datos a imprimir del Paciente///////////////

            oC.ResultadoEdad = chkDatosPacienteImprimir.Items[3].Selected; ///edad
            oC.ResultadoFNacimiento = chkDatosPacienteImprimir.Items[4].Selected; ///f.nacimiento
            oC.ResultadoSexo = chkDatosPacienteImprimir.Items[5].Selected; ///sexo
            oC.ResultadoHC = chkDatosPacienteImprimir.Items[2].Selected; ///hc
            oC.ResultadoDNI = chkDatosPacienteImprimir.Items[1].Selected; ///dni
            oC.ResultadoDomicilio = chkDatosPacienteImprimir.Items[6].Selected; ///domicilio
                                                                                ///

            oC.ResultadoEdadMicrobiologia = chkDatosPacienteImprimirMicrobiologia.Items[3].Selected;
            oC.ResultadoFNacimientoMicrobiologia = chkDatosPacienteImprimirMicrobiologia.Items[4].Selected;
            oC.ResultadoSexoMicrobiologia = chkDatosPacienteImprimirMicrobiologia.Items[5].Selected;
            oC.ResultadoHCMicrobiologia = chkDatosPacienteImprimirMicrobiologia.Items[2].Selected;
            oC.ResultadoDNIMicrobiologia = chkDatosPacienteImprimirMicrobiologia.Items[1].Selected;
            oC.ResultadoDomicilioMicrobiologia = chkDatosPacienteImprimirMicrobiologia.Items[6].Selected;
            /////////////////////////////////////////////////

            ////////Imprime firma electronica///////////
            oC.FirmaElectronicaLaboratorio=int.Parse( ddlImprimePieResultados.SelectedValue);
            oC.FirmaElectronicaMicrobiologia =int.Parse( ddlImprimePieResultadosMicrobiologia.SelectedValue);
            //if (ddlImprimePieResultados.SelectedValue == "0")
            //    oC.ResultadoImprimePie = false;
            //else
            //    oC.ResultadoImprimePie = true;

            ////////////////////////////////////////////////

            /////////Formato de Impresión///////////////////
            oC.EncabezadoLinea1 = txtEncabezado1.Text;
            oC.EncabezadoLinea2 = txtEncabezado2.Text;
            oC.EncabezadoLinea3 = txtEncabezado3.Text;

            oC.EncabezadoLinea1Microbiologia = txtEncabezado1Microbiologia.Text;
            oC.EncabezadoLinea2Microbiologia = txtEncabezado2Microbiologia.Text;
            oC.EncabezadoLinea3Microbiologia = txtEncabezado3Microbiologia.Text;
            ////////////////////////////////////////
            if (chkBorrarImagen.Checked)
                oC.RutaLogo = "";
            else
                if (fupLogo.FileName != "")    oC.RutaLogo = "logo." + fupLogo.PostedFile.FileName.Split('.')[1];                               

            if (fupLogo.FileName != "")
            {
                //arch.PostedFile.SaveAs("nuevo_nombre." + arch.PostedFile.FileName.Split('.')[1]);
               
                fupLogo.PostedFile.SaveAs(Server.MapPath("~/Logo/logo."+fupLogo.PostedFile.FileName.Split('.')[1]));
                //this.fupLogo.SaveAs(Server.MapPath("~/Logo/" + oC.RutaLogo));                                   
            }
            if (ddlAutenticaValidacion.SelectedValue == "0")
                oC.AutenticaValidacion = false;
            else
                oC.AutenticaValidacion = true;


            ////

            oC.ValorDefectoFechaOrden = int.Parse(ddlFechaOrden.SelectedValue);
            oC.ValorDefectoFechaTomaMuestra = int.Parse(ddlFechaTomaMuestra.SelectedValue);

            oC.Save();
            GuardarCodigoBarrasMicrobiologia(); GuardarCodigoBarrasLaboratorio(); GuardarCodigoBarrasPesquisa();

        }


        private void GuardarCodigoBarrasPesquisa()
        {
            TipoServicio oTipo = new TipoServicio();
            oTipo = (TipoServicio)oTipo.Get(typeof(TipoServicio), 4);

            ConfiguracionCodigoBarra oRegistro = new ConfiguracionCodigoBarra();
            oRegistro = (ConfiguracionCodigoBarra)oRegistro.Get(typeof(ConfiguracionCodigoBarra), "IdTipoServicio", oTipo);

            if (oRegistro == null)
            {
                ConfiguracionCodigoBarra oRegistroNew = new ConfiguracionCodigoBarra();
                GuardarCDsPesquisa(oRegistroNew, oTipo);
            }
            else
                GuardarCDsPesquisa(oRegistro, oTipo);




        }

        private void GuardarCodigoBarrasMicrobiologia()
        {
           TipoServicio oTipo = new TipoServicio();
                oTipo = (TipoServicio)oTipo.Get(typeof(TipoServicio), 3);

                ConfiguracionCodigoBarra oRegistro = new ConfiguracionCodigoBarra();
                oRegistro = (ConfiguracionCodigoBarra)oRegistro.Get(typeof(ConfiguracionCodigoBarra), "IdTipoServicio", oTipo);

                if (oRegistro == null)
                {
                    ConfiguracionCodigoBarra oRegistroNew = new ConfiguracionCodigoBarra();
                    GuardarCDsMicrobiologia(oRegistroNew, oTipo);
                }
                else
                GuardarCDsMicrobiologia(oRegistro,oTipo);
           
          


        }


        private void GuardarCDsPesquisa(ConfiguracionCodigoBarra oRegistro, TipoServicio oTipo)
        {

            oRegistro.Habilitado =chkImprimeCodigoBarrasPesquisa.Checked;

            oRegistro.IdTipoServicio = oTipo;

            oRegistro.Fuente = rdbFuente3.SelectedValue;
            oRegistro.ProtocoloFecha = chkProtocolo3.Items[1].Selected;
            oRegistro.ProtocoloOrigen = chkProtocolo3.Items[2].Selected;
            oRegistro.ProtocoloSector = chkProtocolo3.Items[3].Selected;
            oRegistro.ProtocoloNumeroOrigen = chkProtocolo3.Items[4].Selected;

            oRegistro.PacienteApellido = chkPaciente3.Items[0].Selected;
            oRegistro.PacienteSexo = chkPaciente3.Items[1].Selected;
            oRegistro.PacienteEdad = chkPaciente3.Items[2].Selected;
            oRegistro.PacienteNumeroDocumento = chkPaciente3.Items[3].Selected;


            oRegistro.Save();
        }
        private void GuardarCDsMicrobiologia(ConfiguracionCodigoBarra oRegistro, TipoServicio oTipo)
        {
           
            oRegistro.Habilitado = chkImprimeCodigoBarrasMicrobiologia.Checked;

            oRegistro.IdTipoServicio = oTipo;
            oRegistro.Fuente = rdbFuente2.SelectedValue;
            oRegistro.ProtocoloFecha = chkProtocolo2.Items[1].Selected;
            oRegistro.ProtocoloOrigen = chkProtocolo2.Items[2].Selected;
            oRegistro.ProtocoloSector = chkProtocolo2.Items[3].Selected;
            oRegistro.ProtocoloNumeroOrigen = chkProtocolo2.Items[4].Selected;

            oRegistro.PacienteApellido = chkPaciente2.Items[0].Selected;
            oRegistro.PacienteSexo = chkPaciente2.Items[1].Selected;
            oRegistro.PacienteEdad = chkPaciente2.Items[2].Selected;
            oRegistro.PacienteNumeroDocumento = chkPaciente2.Items[3].Selected;


            oRegistro.Save();
        }

        private void GuardarCodigoBarrasLaboratorio()
        {
            TipoServicio oTipo = new TipoServicio();
            oTipo = (TipoServicio)oTipo.Get(typeof(TipoServicio), 1);

            ConfiguracionCodigoBarra oRegistro = new ConfiguracionCodigoBarra();
            oRegistro = (ConfiguracionCodigoBarra)oRegistro.Get(typeof(ConfiguracionCodigoBarra), "IdTipoServicio", oTipo);

            if (oRegistro == null)
            {
                ConfiguracionCodigoBarra oRegistroNew = new ConfiguracionCodigoBarra();
                GuardarCDLaboratorio(oRegistroNew, oTipo);
            }
            else
                GuardarCDLaboratorio(oRegistro, oTipo);
           


        }

        private void GuardarCDLaboratorio(ConfiguracionCodigoBarra oRegistro, TipoServicio oTipo)
        {
            oRegistro.Habilitado = chkImprimeCodigoBarrasLaboratorio.Checked;
            oRegistro.IdTipoServicio = oTipo;
            oRegistro.Fuente = ddlFuente.SelectedValue;
            oRegistro.ProtocoloFecha = chkProtocolo.Items[1].Selected;
            oRegistro.ProtocoloOrigen = chkProtocolo.Items[2].Selected;
            oRegistro.ProtocoloSector = chkProtocolo.Items[3].Selected;
            oRegistro.ProtocoloNumeroOrigen = chkProtocolo.Items[4].Selected;

            oRegistro.PacienteApellido = chkPaciente.Items[0].Selected;
            oRegistro.PacienteSexo = chkPaciente.Items[1].Selected;
            oRegistro.PacienteEdad = chkPaciente.Items[2].Selected;
            oRegistro.PacienteNumeroDocumento = chkPaciente.Items[3].Selected;

            oRegistro.Save();
        }

        protected void chkImprimeCodigoBarrasLaboratorio_CheckedChanged(object sender, EventArgs e)
        {

            pnlLaboratorio.Enabled = chkImprimeCodigoBarrasLaboratorio.Checked;
            pnlLaboratorio.UpdateAfterCallBack = true;
            SetSelectedTab(TabIndex.DEFAULT);
        }


        protected void chkImprimeCodigoBarrasPesquisa_CheckedChanged(object sender, EventArgs e)
        {
            pnlPesquisa.Enabled = chkImprimeCodigoBarrasPesquisa.Checked;
            pnlPesquisa.UpdateAfterCallBack = true;
            SetSelectedTab(TabIndex.DEFAULT);
        }
        protected void chkImprimeCodigoBarrasMicrobiologia_CheckedChanged(object sender, EventArgs e)
        {

            pnlMicrobiologia.Enabled = chkImprimeCodigoBarrasMicrobiologia.Checked;
            pnlMicrobiologia.UpdateAfterCallBack = true;
            SetSelectedTab(TabIndex.ONE);
        }
        protected void rdbDiasEspera_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarValidadorDias();
        }

        private void HabilitarValidadorDias()
        {
            if (rdbDiasEspera.Items[1].Selected)
                rfvDiasEspera.Enabled = true;
            else
                rfvDiasEspera.Enabled = false;
            rfvDiasEspera.UpdateAfterCallBack = true;
        }

        protected void rdbTipoDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbTipoDias.Items[0].Selected)//Todos los días
                for (int i = 0; i < 7; i++)
                {
                    cklDias.Items[i].Selected = true;
                }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    cklDias.Items[i].Selected = true;
                }
                for (int i = 5; i < 7; i++)
                {
                    cklDias.Items[i].Selected = false;
                }
            }
            cklDias.UpdateAfterCallBack = true;

        }

        protected void fupLogo_DataBinding(object sender, EventArgs e)
        {
          
            
            
        }

        protected void ddlRecordarOrigenProtocolo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rdbTipoNumeracionProtocolo_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        protected void btnAgregarSector_Click(object sender, EventArgs e)
        {
     
        }

        //protected void lnkImpresionPrueba_Click(object sender, EventArgs e)
        //{
        //    ImpresiondePrueba();
        //}

        //private void ImpresiondePrueba()
        //{
        //    ParameterDiscreteValue impresora = new ParameterDiscreteValue();
        //    impresora.Value = ddlImpresora.SelectedValue;

        //    oCr.Report.FileName = "iNFORMES/PaginaPrueba.rpt";
        //    oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(impresora);

        //    oCr.ReportDocument.PrintOptions.PrinterName = ddlImpresora.SelectedValue;
        //    oCr.ReportDocument.PrintToPrinter(1, false, 0,0);                
        //}
        
    }
}
