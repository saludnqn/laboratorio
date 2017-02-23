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
using Business.Data;
using Business;
using Business.Data.Laboratorio;
using NHibernate;
using NHibernate.Expression;
using CrystalDecisions.Shared;
using System.IO;
using System.Data.SqlClient;
using CrystalDecisions.Web;

namespace WebLab.Turnos
{
    public partial class TurnosEdit2 : System.Web.UI.Page
    {
        public CrystalReportSource oCr = new CrystalReportSource();
        Configuracion oC = new Configuracion();

        private Random
      random = new Random();

        private static int
            TEST = 0;

        private bool IsTokenValid()
        {
            bool result = double.Parse(hidToken.Value) == ((double)Session["NextToken"]);
            SetToken();
            return result;
        }

        private void SetToken()
        {
            double next = random.Next();
            hidToken.Value = next + "";
            Session["NextToken"] = next;
        }
 
        protected void Page_PreInit(object sender, EventArgs e)
        {
            oCr.Report.FileName = "";
            oCr.CacheDuration = 0;
            oCr.EnableCaching = false;
            oC = (Configuracion)oC.Get(typeof(Configuracion), "IdConfiguracion", 1);
         
        }
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetToken();
               VerificaPermisos("Asignacion de turnos");               
               CargarListas();

               chkImprimir.Visible= oC.GeneraComprobanteTurno;
               
                
                if (Request["Modifica"].ToString() == "1")
                {
                    lblTitulo.Text = "ACTUALIZACION TURNO";
                    pnlNuevo.Visible = false;
                    chkImprimir.Visible = false;
                    lnkReimprimirComprobante.Visible = true;
                    MuestraDatos();
                   
                }
                else
                {
                  //  CargarListas();
                    lblTitulo.Text = "NUEVO TURNO";
                    DateTime m_fecha =DateTime.Parse(Session["Turno_Fecha"].ToString());
                    string m_tipoServicio = Session["Turno_IdTipoServicio"].ToString();
                    string m_hora = Session["Turno_Hora"].ToString();
                    string m_iditem = Session["idItem"].ToString();

                    lblFecha.Text = m_fecha.ToShortDateString();
                    lblHora.Text = m_hora;
                    lblIdTipoServicio.Text = m_tipoServicio;
                    
                    TipoServicio oServicio = new TipoServicio();
                    oServicio = (TipoServicio)oServicio.Get(typeof(TipoServicio), int.Parse(m_tipoServicio));


                    if (m_iditem != "0")
                    {
                        Item oItem = new Item();
                        oItem = (Item)oItem.Get(typeof(Item), int.Parse(m_iditem));                      
                        string sDatos = "";                      
                        sDatos += "#" +oItem.Codigo+ "#" + oItem.Nombre + "@";
                        TxtDatos.Value = sDatos;
                    }

                    lblTipoServicio.Text = oServicio.Nombre;
                 
                    MostrarPaciente();
                 
                }
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
                    case 1: btnGuardar.Visible = false; break;
                }
            }
            else Response.Redirect("../FinSesion.aspx", false);

        }




        private void MuestraDatos()
        {
            //Actualiza los datos de los objetos : alta o modificacion .

            Turno oRegistro = new Turno();
            oRegistro = (Turno)oRegistro.Get(typeof(Turno), int.Parse(Request["idTurno"].ToString()));

            lblFecha.Text = oRegistro.Fecha.ToShortDateString();
            lblHora.Text = oRegistro.Hora;
            lblTipoServicio.Text = oRegistro.IdTipoServicio.Nombre;
            lblIdTipoServicio.Text = oRegistro.IdTipoServicio.IdTipoServicio.ToString();            
            
            ///Datos del Paciente
            lblIdPaciente.Text = oRegistro.IdPaciente.IdPaciente.ToString();
            if (oRegistro.IdPaciente.IdEstado != 2)
                lblPaciente.Text = oRegistro.IdPaciente.NumeroDocumento.ToString() + " - " + oRegistro.IdPaciente.Apellido + " " + oRegistro.IdPaciente.Nombre;
            else
                lblPaciente.Text = "(Sin DU Temporal) - " + oRegistro.IdPaciente.Apellido + " " + oRegistro.IdPaciente.Nombre;
          //  lblPaciente.Text = oRegistro.IdPaciente.NumeroDocumento.ToString() + " - " + oRegistro.IdPaciente.Apellido + " " + oRegistro.IdPaciente.Nombre;            
            ddlObraSocial.SelectedValue = oRegistro.IdObraSocial.IdObraSocial.ToString();
            ddlSectorServicio.SelectedValue = oRegistro.IdSector.ToString();
            ddlEspecialista.SelectedValue = oRegistro.IdEspecialistaSolicitante.ToString();
            lblFechaNacimiento.Text = oRegistro.IdPaciente.FechaNacimiento.ToShortDateString();
            switch (oRegistro.IdPaciente.IdSexo)
            {
                case 1: lblSexo.Text = "Indeterminado"; break;
                case 2: lblSexo.Text = "Femenino"; break;
                case 3: lblSexo.Text = "Masculino"; break;
            }
            

            ///Agregar a la tabla las determinaciones para mostrarlas en el gridview                             
        //    dtDeterminaciones = (System.Data.DataTable)(Session["Tabla1"]);
            DetalleProtocolo oDetalle = new DetalleProtocolo();
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(TurnoItem));
            crit.Add(Expression.Eq("IdTurno", oRegistro));

            IList items = crit.List();
            string pivot = "";
            string sDatos = "";
            foreach (TurnoItem oDet in items)
            {
                if (pivot != oDet.IdItem.Nombre)
                {
                    sDatos += "#" + oDet.IdItem.Codigo + "#" + oDet.IdItem.Nombre  + "@";
                    pivot = oDet.IdItem.Nombre;
                }                                
            }
            TxtDatos.Value = sDatos;
            

            ///Agregar a la tabla las diagnosticos para mostrarlas en el gridview                             
          
            TurnoDiagnostico oDiagnostico = new TurnoDiagnostico();            
            ICriteria crit2 = m_session.CreateCriteria(typeof(TurnoDiagnostico));
            crit2.Add(Expression.Eq("IdTurno", oRegistro));

            IList diagnosticos = crit2.List();

            foreach (TurnoDiagnostico oDiag in diagnosticos)
            {
                Cie10 oC = new Cie10();
                oC = (Cie10)oC.Get(typeof(Cie10), oDiag.IdDiagnostico);

                
                ListItem oDia = new ListItem();
                oDia.Text = oC.Codigo + " - " + oC.Nombre;
                oDia.Value = oC.Id.ToString();
                lstDiagnosticosFinal.Items.Add(oDia);                 


            }
        
            //////////////////////////////////////////////            
        }

        protected void btnBusquedaDiagnostico_Click(object sender, EventArgs e)
        {
            BuscarDiagnostico();



        }

        private void BuscarDiagnostico()
        {

            lstDiagnosticos.Items.Clear();
            if (txtCodigoDiagnostico.Text != "")
            {
                Cie10 oDiagnostico = new Cie10();
                oDiagnostico = (Cie10)oDiagnostico.Get(typeof(Cie10), "Codigo", txtCodigoDiagnostico.Text);
                if (oDiagnostico != null)
                {
                    ListItem oDia = new ListItem();
                    oDia.Text = oDiagnostico.Codigo + " - " + oDiagnostico.Nombre;
                    oDia.Value = oDiagnostico.Id.ToString();
                    lstDiagnosticos.Items.Add(oDia);

                }
                else
                    lstDiagnosticos.Items.Clear();
            }

            if (txtNombreDiagnostico.Text != "")
            {
                lstDiagnosticos.Items.Clear();
                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit = m_session.CreateCriteria(typeof(Cie10));
                crit.Add(Expression.Sql(" Nombre like '%" + txtNombreDiagnostico.Text + "%' order by Nombre"));

                IList items = crit.List();

                foreach (Cie10 oDiagnostico in items)
                {
                    ListItem oDia = new ListItem();
                    oDia.Text = oDiagnostico.Codigo + " - " + oDiagnostico.Nombre;
                    oDia.Value = oDiagnostico.Id.ToString();
                    lstDiagnosticos.Items.Add(oDia);
                }


            }
            lstDiagnosticos.UpdateAfterCallBack = true;

        }

        protected void btnBusquedaFrecuente_Click(object sender, EventArgs e)
        {
            CargarDiagnosticosFrecuentes();
        }
        private void CargarDiagnosticosFrecuentes()
        {
            Utility oUtil = new Utility();


            //  btnGuardarImprimir.Visible = oC.GeneraComprobanteProtocolo;
            lstDiagnosticos.Items.Clear();

            ///Carga de combos de tipos de servicios
            string m_ssql = "SELECT ID, Codigo + ' - ' + Nombre as nombre FROM Sys_CIE10 WHERE (ID IN (SELECT DISTINCT idDiagnostico FROM LAB_ProtocoloDiagnostico)) ORDER BY Nombre";
            oUtil.CargarListBox(lstDiagnosticos, m_ssql, "id", "nombre");
            lstDiagnosticos.UpdateAfterCallBack = true;


        }

        private void CargarListas()
        {
            Utility oUtil = new Utility();
            string m_idtiposervicio="1";///por defecto el servicio es Laboratorio
            
            if  (Session["Turno_IdTipoServicio"]!=null)  m_idtiposervicio= Session["Turno_IdTipoServicio"].ToString();


            ///Carga de combos de ObraSocial
            string m_ssql = "SELECT idObraSocial, nombre FROM   Sys_ObraSocial order by idObraSocial ";
            oUtil.CargarCombo(ddlObraSocial, m_ssql, "idObraSocial", "nombre");


            //Carga del combo de diagnosticos
            //m_ssql = "SELECT  ID, Nombre FROM Sys_CIE10 ORDER BY Nombre";
            //oUtil.CargarCombo(ddlDiagnostico, m_ssql, "id", "nombre");
            //ddlDiagnostico.Items.Insert(0, new ListItem("", "0"));

            m_ssql = "SELECT  idSectorServicio,  prefijo + ' - ' + nombre   as nombre FROM LAB_SectorServicio WHERE (baja = 0) order by nombre";
            oUtil.CargarCombo(ddlSectorServicio, m_ssql, "idSectorServicio", "nombre");
            ddlSectorServicio.Items.Insert(0, new ListItem("-SIN IDENTIFICAR-", "0"));


            m_ssql = "SELECT idProfesional, apellido + ' ' + nombre AS nombre FROM Sys_Profesional   ORDER BY apellido, nombre ";
            oUtil.CargarCombo(ddlEspecialista, m_ssql, "idProfesional", "nombre");
            ddlEspecialista.Items.Insert(0, new ListItem("No identificado", "0"));

            //Carga del combo de determinaciones
            m_ssql = "SELECT I.idItem as idItem, I.codigo as codigo, I.nombre as nombre, I.nombre + ' - ' + I.codigo as nombreLargo FROM Lab_item I  " +
                    " INNER JOIN Lab_area A ON A.idArea= I.idArea " +
                    " where A.baja=0 and I.baja=0 and I.disponible =1 and A.idtipoServicio= " + m_idtiposervicio + " AND (I.tipo= 'P') order by I.nombre ";
            NHibernate.Cfg.Configuration oConf = new NHibernate.Cfg.Configuration();
            String strconn = oConf.GetProperty("hibernate.connection.connection_string");
            SqlDataAdapter da = new SqlDataAdapter(m_ssql, strconn);
            DataSet ds = new DataSet();
            da.Fill(ds, "T");
             ddlItem.DataTextField = "nombreLargo";
             ddlItem.DataValueField = "idItem";
             ddlItem.DataSource = ds.Tables["T"];
             ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("", "0"));

            string sTareas = "";        

            for (int i = 0; i < ds.Tables["T"].Rows.Count; i++)
            {
                sTareas += "#" + ds.Tables["T"].Rows[i][1].ToString() + "#" + ds.Tables["T"].Rows[i][2].ToString() + "@";
            }
            txtTareas.Value = sTareas;


            //Carga de combo de rutinas
            m_ssql = "SELECT idRutina, nombre FROM Lab_Rutina where baja=0 and idTipoServicio= " + m_idtiposervicio + " order by nombre ";
            oUtil.CargarCombo(ddlRutina, m_ssql, "idRutina", "nombre");
            ddlRutina.Items.Insert(0, new ListItem("Seleccione una rutina", "0"));

            ddlItem.UpdateAfterCallBack = true;
            ddlRutina.UpdateAfterCallBack = true;

            ///////////////Impresoras////////////////////////
            m_ssql = "SELECT idImpresora, nombre FROM LAB_Impresora ";
            oUtil.CargarCombo(ddlImpresora, m_ssql, "nombre", "nombre");
            if (Session["Impresora"] != null) ddlImpresora.SelectedValue = Session["Impresora"].ToString();

            ///////////////Fin de Impresoras///////////////////

            m_ssql = null;
            oUtil = null;
        }



    


        private void MostrarPaciente()
        {
            ///Muestro los datos del paciente 
            Paciente oPaciente = new Paciente();
            oPaciente = (Paciente)oPaciente.Get(typeof(Paciente), int.Parse(Request["idPaciente"].ToString()));
            if (oPaciente.IdEstado!=2)                            
                lblPaciente.Text = oPaciente.NumeroDocumento.ToString() + " - " + oPaciente.Apellido + " " + oPaciente.Nombre;
            else
                lblPaciente.Text = "(Sin DU Temporal) - " + oPaciente.Apellido + " " + oPaciente.Nombre;

            lblIdPaciente.Text = oPaciente.IdPaciente.ToString();
            ddlObraSocial.SelectedValue = oPaciente.IdObraSocial.ToString();
            lblFechaNacimiento.Text = oPaciente.FechaNacimiento.ToShortDateString();
              switch (oPaciente.IdSexo)
              {
                  case 1: lblSexo.Text = "Indeterminado"; break;
                  case 2: lblSexo.Text = "Femenino"; break;
                  case 3: lblSexo.Text = "Masculino"; break;
              }
            

            ////Busca turnos con inasistencia
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(Turno));
            crit.Add(Expression.Eq("IdPaciente", oPaciente));
            crit.Add(Expression.Eq("IdProtocolo", 0));
            crit.Add(Expression.Le("Fecha", DateTime.Now));

            IList detalle = crit.List();
            if (detalle.Count > 0)
            {                
                lblAlerta.Visible = true;
                lblAlerta.Text = "El paciente tiene " + detalle.Count.ToString() + " inasistencia en los turnos solicitados";
            } else lblAlerta.Visible = false;
            ///// fin de busqueda

        }

       

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///////Con la selección del item se muestra el codigo
            if (ddlItem.SelectedValue != "0")
            {
                Item oItem = new Item();
                oItem = (Item)oItem.Get(typeof(Item), int.Parse(ddlItem.SelectedValue));
                txtCodigo.Text = oItem.Codigo;
                txtCodigo.UpdateAfterCallBack = true;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
          //  AgregarDeterminaciones();
        }

       

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //if (Request["Modifica"].ToString() == "1")
                Response.Redirect("TurnoList.aspx?", false);
            /*else
                Response.Redirect("Default.aspx", false);*/
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            { ///Verifica si se trata de un alta o modificacion de protocolo               
                Turno oRegistro = new Turno();
                if (Request["Modifica"].ToString() == "1") oRegistro = (Turno)oRegistro.Get(typeof(Turno), int.Parse(Request["idTurno"].ToString()));
                Guardar(oRegistro);
                if (chkImprimir.Checked) Imprimir(oRegistro);

                Response.Redirect("TurnoList.aspx", false);
            }
        }

     
        private void Guardar(Turno oRegistro)
        {
            if (IsTokenValid())
            {
                TEST++;
                //Actualiza los datos de los objetos : alta o modificacion .       
                Usuario oUser = new Usuario();
                TipoServicio oServicio = new TipoServicio();
                Paciente oPaciente = new Paciente();
                ObraSocial oObra = new ObraSocial();


                oRegistro.IdEfector = oC.IdEfector;
                oRegistro.IdTipoServicio = (TipoServicio)oServicio.Get(typeof(TipoServicio), int.Parse(lblIdTipoServicio.Text));
                oRegistro.Fecha = DateTime.Parse(lblFecha.Text);
                oRegistro.Hora = lblHora.Text;

                ///Desde aca guarda los datos del paciente en Turno
                oRegistro.IdPaciente = (Paciente)oPaciente.Get(typeof(Paciente), int.Parse(lblIdPaciente.Text));
                oRegistro.IdObraSocial = (ObraSocial)oObra.Get(typeof(ObraSocial), int.Parse(ddlObraSocial.SelectedValue));
                oRegistro.IdSector = int.Parse(ddlSectorServicio.SelectedValue);
                oRegistro.IdEspecialistaSolicitante = int.Parse(ddlEspecialista.SelectedValue);
                oRegistro.IdUsuarioRegistro = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
                oRegistro.FechaRegistro = DateTime.Now;
                if (Session["idItem"] != null) oRegistro.IdItem = int.Parse(Session["idItem"].ToString());
                else      oRegistro.IdItem = 0;
                oRegistro.Save();               

                GuardarDiagnosticos(oRegistro);
                GuardarDetalle(oRegistro);

                //if (chkImprimir.Checked) Imprimir(oRegistro);

                //Response.Redirect("TurnoList.aspx", false);
            }
            ///else es doble clic
        }



        private void GuardarDiagnosticos(Turno oRegistro)
        {
         //   dtDiagnosticos = (System.Data.DataTable)(Session["Tabla2"]);


            ///Eliminar los detalles y volverlos a crear
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(TurnoDiagnostico));
            crit.Add(Expression.Eq("IdTurno", oRegistro));
            IList detalle = crit.List();
            if (detalle.Count > 0)
            {
                foreach (TurnoDiagnostico oDetalle in detalle)
                {
                    oDetalle.Delete();
                }

            }

            /////Crea nuevamente los detalles.
            for (int i = 0; i < lstDiagnosticosFinal.Items.Count; i++)
            {
                TurnoDiagnostico oDetalle = new TurnoDiagnostico();
                oDetalle.IdTurno = oRegistro;
                oDetalle.IdEfector = oRegistro.IdEfector;
                oDetalle.IdDiagnostico = int.Parse(lstDiagnosticosFinal.Items[i].Value);
                oDetalle.Save();

               
            }
        }
        protected void lnkReimprimirComprobante_Click(object sender, EventArgs e)
        {
            Turno oRegistro = new Turno();
            oRegistro = (Turno)oRegistro.Get(typeof(Turno), int.Parse(Request["idTurno"].ToString()));

            ////Imprimir Comprobante para el paciente
            Imprimir(oRegistro);



        }

        private void Imprimir(Turno oRegistro)
        {

            ///////////////
            Business.Reporte ticket = new Business.Reporte();
            
            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
            
            DataTable dt = oRegistro.GetDataSet();
            string fechaHora = dt.Rows[0][1].ToString() + " " + dt.Rows[0][2].ToString();
            string analisis = dt.Rows[0][9].ToString();
            string recomendaciones = dt.Rows[0][10].ToString();
            string paciente = oRegistro.IdPaciente.Apellido + " " + oRegistro.IdPaciente.Nombre;
            string dni = "";

            if (oRegistro.IdPaciente.IdEstado != 2)
                dni = "DU:" + oRegistro.IdPaciente.NumeroDocumento.ToString() + " - " + oRegistro.IdPaciente.Apellido + " " + oRegistro.IdPaciente.Nombre;
            else
                dni = "DU:" + "(Sin DU Temporal) - " + oRegistro.IdPaciente.Apellido + " " + oRegistro.IdPaciente.Nombre;

            //if (oRegistro.IdPaciente.IdEstado >= 0)
            //    dni = "DU:" + oRegistro.IdPaciente.NumeroDocumento.ToString();
            //if (s_hiv != "False") paciente = oRegistro.IdPaciente.getSexo().sub oProt.Sexo + oProt.IdPaciente.Nombre.Substring(1, 2) + oProt.IdPaciente.Apellido.Substring(1, 2) + oProt.IdPaciente.FechaNacimiento.ToShortDateString().Replace("/", "");
            //else paciente = oProt.IdPaciente.Apellido.ToUpper() + " " + oProt.IdPaciente.Nombre.ToUpper();

            ticket.AddHeaderLine("LABORATORIO " + oCon.EncabezadoLinea1);
            ticket.AddSubHeaderLine(" ");
            //ticket.AddSubHeaderLine("___________________________________________________________________________________");
            ticket.AddSubHeaderLine("TURNO " + fechaHora + " hs");
            ticket.AddSubHeaderLine(" ");
            ticket.AddSubHeaderLine(paciente.ToUpper() + "            " + dni + "      Fecha de Nacimiento:" + oRegistro.IdPaciente.FechaNacimiento.ToShortDateString() + "  Sexo:" + oRegistro.IdPaciente.getSexo().Substring(0, 1));

            ticket.AddSubHeaderLine("___________________________________________________________________________________________");
            if (analisis.Trim() != "")
            {                
                int largo= analisis.Length;
                int cantidadFilas=largo / 90;
                if (cantidadFilas >= 0)
                {
                    ticket.AddSubHeaderLine("PRACTICAS SOLICITADAS");
                    for (int i = 1; i <= cantidadFilas; i++)
                    {            
                        int l = i * 90; 
                        analisis=analisis.Insert(l, "&");                        

                    }
                    string[] tabla =analisis.Split('&');        

                    /////Crea nuevamente los detalles.
                    for (int i = 0; i <= tabla.Length - 1; i++)
                    {
                        ticket.AddSubHeaderLine("     " + tabla[i].ToUpper());
                    }
            
                    
                    ticket.AddSubHeaderLine("___________________________________________________________________________________________");
                }
            }
            if (recomendaciones.Trim() != "")
            {
                int largo = recomendaciones.Length;
                int cantidadFilas = largo /110;
                if (cantidadFilas >= 0)
                {                    
                    for (int i = 1; i <= cantidadFilas; i++)
                    {
                        int l = i * 110;
                        recomendaciones = recomendaciones.Insert(l, "&");
                    }
                    string[] tabla = recomendaciones.Split('&');
                                        
                    for (int i = 0; i <= tabla.Length - 1; i++)
                    {
                        if (tabla[i].Trim() != "")
                        {
                            if (i == 0)
                            {
                                ticket.AddSubHeaderLine("INDICACIONES PARA EL PACIENTE");
                                ticket.AddSubHeaderLine(" ");
                                ticket.AddSubHeaderLine("     " + tabla[i]);
                            }
                            else
                                ticket.AddSubHeaderLine("     " + tabla[i]);
                        }
                    }
                    ticket.AddSubHeaderLine("___________________________________________________________________________________________");
                }
            }

            Session["Impresora"] = ddlImpresora.SelectedValue;
            ticket.PrintTicket(ddlImpresora.SelectedValue, "");
            /////fin de impresion de archivos
        }


        //private void Imprimir(Turno oRegistro)
        //{
        //    //Aca se deberá consultar los parametros para mostrar una hoja de trabajo u otra
        //    // CrystalReportSource1.Report.FileName = "HTrabajo2.rpt";
        //    string informe = "../Informes/Turno.rpt";
          

        //    ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
        //    encabezado1.Value = oC.EncabezadoLinea1;

        //    ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
        //    encabezado2.Value = oC.EncabezadoLinea2;

        //    ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
        //    encabezado3.Value = oC.EncabezadoLinea3;

        //    Session["Impresora"] = ddlImpresora.SelectedValue;
        //    oCr.ReportDocument.PrintOptions.PrinterName = ddlImpresora.SelectedValue;

        //    oCr.Report.FileName = informe;
        //    oCr.ReportDocument.SetDataSource(oRegistro.GetDataSet());
        //    oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
        //    oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
        //    oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
        //    oCr.DataBind();

          
        //    oCr.ReportDocument.PrintToPrinter(1, false, 0, 0); 
           
          
        //}
      

        private void GuardarDetalle(Turno oRegistro)
        {
          //  Bind();
            //dtDeterminaciones = (System.Data.DataTable)(Session["Tabla1"]);

            ///Eliminar los detalles para volverlos a crear            
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(TurnoItem));
            crit.Add(Expression.Eq("IdTurno", oRegistro));
            IList detalle = crit.List();
            if (detalle.Count > 0)
            {
                foreach (TurnoItem oDetalle in detalle)
                {
                    oDetalle.Delete();
                }
            }


            string[] tabla = TxtDatos.Value.Split('@');
        

            /////Crea nuevamente los detalles.
            for (int i = 0; i < tabla.Length - 1; i++)
            {
                string[] fila = tabla[i].Split('#');
                string codigo = fila[1].ToString();
                if (codigo != "")  
                {  
                    Item oItem = new Item();
                    oItem= (Item)oItem.Get(typeof(Item), "Codigo", codigo,"Baja",false);
                   
                    //ISession m_session = NHibernateHttpModule.CurrentSession;
                    ICriteria critRec = m_session.CreateCriteria(typeof(ItemRecomendacion));
                    critRec.Add(Expression.Eq("IdItem", oItem));
                    IList listaRecomendacion = critRec.List();
                    if (listaRecomendacion.Count > 0)
                    {
                        foreach (ItemRecomendacion oRec in listaRecomendacion)
                        {  
                            TurnoItem oDetalle = new TurnoItem();                  
                            oDetalle.IdTurno = oRegistro;
                            oDetalle.IdEfector = oRegistro.IdEfector;
                            oDetalle.IdItem = oItem;
                            ///buscar recomdaciones asociadas al item                    
                            oDetalle.Recomendacion = oRec.IdRecomendacion.Descripcion;
                            oDetalle.Save();
                            
                        }
                    }
                    else /// si no hay ninguna recomendacion se guarda vacia
                    {

                         TurnoItem oDetalle = new TurnoItem();                  
                            oDetalle.IdTurno = oRegistro;
                            oDetalle.IdEfector = oRegistro.IdEfector;
                            oDetalle.IdItem = oItem;
                            ///buscar recomdaciones asociadas al item                    
                            oDetalle.Recomendacion ="";
                            oDetalle.Save();
                    }


                }
            }
          //  Response.Redirect("TurnoList.aspx", false);
        }

      


        protected void btnAgregarRutina_Click(object sender, EventArgs e)
        {
            if (ddlRutina.SelectedValue!="0")
            AgregarRutina();
        }

        private void AgregarRutina()
        {
            Rutina oRutina = new Rutina();
            oRutina = (Rutina)oRutina.Get(typeof(Rutina), int.Parse(ddlRutina.SelectedValue));

            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(DetalleRutina));
            crit.Add(Expression.Eq("IdRutina", oRutina));
            IList detalle = crit.List();
            if (detalle.Count > 0)
            {
                string codigos = "";
                foreach (DetalleRutina oDetalle in detalle)
                {

                    if (codigos == "")
                        codigos = oDetalle.IdItem.Codigo;
                    else
                        codigos += ";" + oDetalle.IdItem.Codigo;                 

                    //ddlRutina.SelectedValue = "0";
                    //ddlRutina.UpdateAfterCallBack = true;
                }
                txtCodigosRutina.Text = codigos;
                txtCodigosRutina.UpdateAfterCallBack = true;
            }

        }

        protected void ddlRutina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRutina.SelectedValue != "0")
                AgregarRutina();
        }

        protected void btnAgregarDiagnostico_Click(object sender, EventArgs e)
        {
            AgregarDiagnostico();
        }

        private void AgregarDiagnostico()
        {
            if (lstDiagnosticos.SelectedValue != "")
            {
                lstDiagnosticosFinal.Items.Add(lstDiagnosticos.SelectedItem);
                lstDiagnosticosFinal.UpdateAfterCallBack = true;
            }                  
        }

      

        protected void btnAgregarDiagnostico_Click1(object sender, ImageClickEventArgs e)
        {
            AgregarDiagnostico();
        }

        protected void btnSacarDiagnostico_Click(object sender, ImageClickEventArgs e)
        {
            SacarDiagnostico();
        }

        private void SacarDiagnostico()
        {
            if (lstDiagnosticosFinal.SelectedValue != "")
            {
                lstDiagnosticosFinal.Items.Remove(lstDiagnosticosFinal.SelectedItem);
                lstDiagnosticosFinal.UpdateAfterCallBack = true;
            }
        }
        protected void txtCodigoDiagnostico_TextChanged(object sender, EventArgs e)
        {
            BuscarDiagnostico();       
        }

        protected void txtNombreDiagnostico_TextChanged(object sender, EventArgs e)
        {
            BuscarDiagnostico();       
        }

        protected void cvValidaPracticas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            string practicallagalimiteTurnos= VerificarLimiteTurnosPorPracticas();
            if (practicallagalimiteTurnos == "")
                args.IsValid = true;
            else
                cvValidaPracticas.Text = "Ha superado el límite de turnos diarios para " + practicallagalimiteTurnos+ " o no se ha habilitado la dación de turnos para esta práctica. Verifique con el Administrador." ;
        }

        private string VerificarLimiteTurnosPorPracticas()
        {
            string practica="";
            string[] tabla = TxtDatos.Value.Split('@');


            /////Crea nuevamente los detalles.
            for (int i = 0; i < tabla.Length - 1; i++)
            {
                string[] fila = tabla[i].Split('#');
                string codigo = fila[1].ToString();
                if (codigo != "")
                {
                    Item oItem = new Item();
                    oItem = (Item)oItem.Get(typeof(Item), "Codigo", codigo, "Baja", false);
                    if (oItem!=null)
                    {
                        if (oItem.LimiteTurnosDia < 0) ///-1 no se puede dar turnos.
                        { practica = oItem.Nombre; break; }

                        if (oItem.LimiteTurnosDia > 0)
                        {
                            int cantidadDados = 0;
                            ISession m_session = NHibernateHttpModule.CurrentSession;
                            ICriteria crit = m_session.CreateCriteria(typeof(Turno));
                            crit.Add(Expression.Eq("Fecha",  DateTime.Parse(lblFecha.Text)));
                            crit.Add(Expression.Eq("Baja", false));

                            IList detalle = crit.List();
                            foreach (Turno oTurno in detalle)
                            {
                              //  ISession m_session = NHibernateHttpModule.CurrentSession;
                                ICriteria crit2 = m_session.CreateCriteria(typeof(TurnoItem));
                                crit2.Add(Expression.Eq("IdTurno", oTurno));
                                crit2.Add(Expression.Eq("IdItem", oItem));
                                

                                IList detalle2 = crit2.List();
                                cantidadDados += detalle2.Count;                                                                   

                            }
                            if (cantidadDados >= oItem.LimiteTurnosDia) { practica =oItem.Nombre; break; }
                        }                      
                    }                      
                }/// fin codigo
            }/// fin foreach
            return practica;

        }
    }
}
