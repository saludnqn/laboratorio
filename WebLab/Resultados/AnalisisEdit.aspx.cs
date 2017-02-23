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
using NHibernate;
using NHibernate.Expression;
using System.Collections;

namespace WebLab.Resultados
{
    public partial class AnalisisEdit : System.Web.UI.Page
    {
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

        Protocolo oProtocolo = new Protocolo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetToken();
                Business.Data.Laboratorio.Protocolo oRegistro = new Business.Data.Laboratorio.Protocolo();
                oRegistro = (Business.Data.Laboratorio.Protocolo)oRegistro.Get(typeof(Business.Data.Laboratorio.Protocolo), int.Parse(Request["idProtocolo"].ToString()));

                lblProtocolo.Text = oRegistro.GetNumero() + " " + oRegistro.IdPaciente.Apellido + " " + oRegistro.IdPaciente.Nombre;

                    CargarListas(oRegistro);
                    MuestraDatos();

                
                
            }
        }

        private void MuestraDatos()
        {
            //Actualiza los datos de los objetos : alta o modificacion .
            Configuracion oC = new Configuracion(); oC = (Configuracion)oC.Get(typeof(Configuracion), "IdConfiguracion", 1);

            Business.Data.Laboratorio.Protocolo oRegistro = new Business.Data.Laboratorio.Protocolo();
            oRegistro = (Business.Data.Laboratorio.Protocolo)oRegistro.Get(typeof(Business.Data.Laboratorio.Protocolo), int.Parse(Request["idProtocolo"].ToString()));
          //  oRegistro.GrabarAuditoriaProtocolo("Consulta", int.Parse(Session["idUsuario"].ToString()));

            ddlMuestra.SelectedValue = oRegistro.IdMuestra.ToString();
         
            CargarItems(oRegistro);
         
         
            ///Agregar a la tabla las determinaciones para mostrarlas en el gridview                             
            //dtDeterminaciones = (System.Data.DataTable)(Session["Tabla1"]);
            DetalleProtocolo oDetalle = new DetalleProtocolo();
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(DetalleProtocolo));
            crit.Add(Expression.Eq("IdProtocolo", oRegistro));
            crit.AddOrder(Order.Asc("IdDetalleProtocolo"));

            IList items = crit.List();
            string pivot = "";
            string sDatos = "";
            foreach (DetalleProtocolo oDet in items)
            {
                if (pivot != oDet.IdItem.Nombre)
                {
                    if (sDatos == "")
                        sDatos = oDet.IdItem.Codigo + "#" + oDet.TrajoMuestra + "#" + oDet.ConResultado;
                    else
                        sDatos += ";" + oDet.IdItem.Codigo + "#" + oDet.TrajoMuestra + "#" + oDet.ConResultado;
                    //sDatos += "#" + oDet.IdItem.Codigo + "#" + oDet.IdItem.Nombre + "#" + oDet.TrajoMuestra + "@";                   
                    pivot = oDet.IdItem.Nombre;
                }

            }

            TxtDatosCargados.Value = sDatos;

            //TxtDatos.Value = sDatos;


        }
        //private void MostrarScreeningNeonatal()
        //{
        //    SolicitudScreening oRegistro = new SolicitudScreening();
        //    oRegistro = (SolicitudScreening)oRegistro.Get(typeof(SolicitudScreening), int.Parse(Request["idSolicitudScreening"].ToString()));

        //    txtNumeroOrigen.Text = oRegistro.NumeroOrigen.ToString();
        //    ddlEfector.SelectedValue = oRegistro.IdEfectorSolicitante.IdEfector.ToString();
        //    SelectedEfector();
        //    try
        //    {
        //        ddlEspecialista.SelectedValue = oRegistro.IdMedicoSolicitante.ToString();
        //    }
        //    catch
        //    { }

        //    string codigo = "1195"; ///sacar de alguna configuración           
        //    string sDatos =  codigo + "#Si#False";            
        //    TxtDatosCargados.Value = sDatos;

        //}





        protected void txtCodigoMuestra_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Muestra oMuestra = new Muestra();
                oMuestra = (Muestra)oMuestra.Get(typeof(Muestra), "Codigo", txtCodigoMuestra.Text, "Baja", false);
                if (oMuestra != null) ddlMuestra.SelectedValue = oMuestra.IdMuestra.ToString();
                ddlMuestra.UpdateAfterCallBack = true;
            }
            catch (Exception ex)
            {
                string exception = "";
                while (ex != null)
                {
                    exception = ex.Message + "<br>";

                }
            }
        }

        protected void ddlMuestra_SelectedIndexChanged(object sender, EventArgs e)
        {

            mostrarCodigoMuestra();

        }

        private void mostrarCodigoMuestra()
        {
            if (ddlMuestra.SelectedValue != "0")
            {
                Muestra oMuestra = new Muestra();
                oMuestra = (Muestra)oMuestra.Get(typeof(Muestra), int.Parse(ddlMuestra.SelectedValue));
                if (oMuestra != null) txtCodigoMuestra.Text = oMuestra.Codigo;
                txtCodigoMuestra.UpdateAfterCallBack = true;
            }
        }

        private void CargarListas(Protocolo oRegistro)
        {
            Utility oUtil = new Utility();
            Configuracion oC = new Configuracion(); oC = (Configuracion)oC.Get(typeof(Configuracion), "IdConfiguracion", 1);

           

         string   m_ssql = "SELECT I.idItem as idItem, I.nombre + ' - ' + I.codigo as nombre " +
                    " FROM Lab_item I  " +
                    " INNER JOIN Lab_area A ON A.idArea= I.idArea " +
                    " where A.baja=0 and I.baja=0 and  I.disponible=1 and A.idtipoServicio= " +oRegistro.IdTipoServicio.IdTipoServicio.ToString() + " AND (I.tipo= 'P') order by I.nombre ";
            oUtil.CargarCombo(ddlItem, m_ssql, "idItem", "nombre");


          
            ///Carga de determinaciones y rutinas dependen de la selección del tipo de servicio
            CargarItems(oRegistro);

            //CargarDiagnosticosFrecuentes();

            if (oRegistro.IdTipoServicio.IdTipoServicio == 3)
            {
                ////////////Carga de combos de Muestras
                pnlMuestra.Visible = true;
                m_ssql = "SELECT idMuestra, nombre + ' - ' + codigo as nombre FROM LAB_Muestra    order by nombre ";
                oUtil.CargarCombo(ddlMuestra, m_ssql, "idMuestra", "nombre");
                ////////////////7
            }
            m_ssql = null;
            oUtil = null;
        }



        private void CargarItems(Protocolo oRegistro)
        {
            Utility oUtil = new Utility();
            ///Carga del combo de determinaciones
            string m_ssql = "SELECT I.idItem as idItem, I.codigo as codigo, I.nombre as nombre, I.nombre + ' - ' + I.codigo as nombreLargo, " +
                           " I.disponible " +
                            " FROM Lab_item I  " +
                            " INNER JOIN Lab_area A ON A.idArea= I.idArea " +
                            " where A.baja=0 and I.baja=0  and A.idtipoServicio= " + oRegistro.IdTipoServicio.IdTipoServicio.ToString() + " AND (I.tipo= 'P') order by I.nombre ";
            NHibernate.Cfg.Configuration oConf = new NHibernate.Cfg.Configuration();
            String strconn = oConf.GetProperty("hibernate.connection.connection_string");
            SqlDataAdapter da = new SqlDataAdapter(m_ssql, strconn);
            DataSet ds = new DataSet();
            da.Fill(ds, "T");

            //gvLista.DataSource = ds.Tables["T"];
            //gvLista.DataBind();


            ddlItem.Items.Insert(0, new ListItem("", "0"));

            string sTareas = "";
            for (int i = 0; i < ds.Tables["T"].Rows.Count; i++)
            {
                sTareas += "#" + ds.Tables["T"].Rows[i][1].ToString() + "#" + ds.Tables["T"].Rows[i][2].ToString() + "#" + ds.Tables["T"].Rows[i][4].ToString() + "@";
            }
            txtTareas.Value = sTareas;

            //Carga de combo de rutinas
            m_ssql = "SELECT idRutina, nombre FROM Lab_Rutina where baja=0 and idTipoServicio= " + oRegistro.IdTipoServicio.IdTipoServicio.ToString() + " order by nombre ";
            oUtil.CargarCombo(ddlRutina, m_ssql, "idRutina", "nombre");
            ddlRutina.Items.Insert(0, new ListItem("Seleccione una rutina", "0"));

            ddlItem.UpdateAfterCallBack = true;
            ddlRutina.UpdateAfterCallBack = true;

            m_ssql = null;
            oUtil = null;
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            { ///Verifica si se trata de un alta o modificacion de protocolo               
                Business.Data.Laboratorio.Protocolo oRegistro = new Business.Data.Laboratorio.Protocolo();
                oRegistro = (Business.Data.Laboratorio.Protocolo)oRegistro.Get(typeof(Business.Data.Laboratorio.Protocolo), int.Parse(Request["idProtocolo"].ToString()));


                Guardar(oRegistro);
                Response.Redirect("AnalisisEdit.aspx?idProtocolo=" + oRegistro.IdProtocolo.ToString(), false);
            }
               

        }

    
     


      


     

        private void Guardar(Business.Data.Laboratorio.Protocolo oRegistro)
        {
            if (IsTokenValid())
            {
                TEST++;

                GuardarDetalle(oRegistro);
            }
           // oRegistro.GrabarAuditoriaProtocolo(Request["Operacion"].ToString(), int.Parse(Session["idUsuario"].ToString()));
        }

        //private void ActualizarSolicitudScreening(string p, Protocolo oProtocolo)
        //{
        //    SolicitudScreening oRegistro = new SolicitudScreening();
        //    oRegistro = (SolicitudScreening)oRegistro.Get(typeof(SolicitudScreening), int.Parse(p));
        //    oRegistro.IdProtocolo = oProtocolo.IdProtocolo;
        //    oRegistro.Save();
        //}

    

     


        private void GuardarDetalle(Business.Data.Laboratorio.Protocolo oRegistro)
        {
            int dias_espera = 0;
            string[] tabla = TxtDatos.Value.Split('@');
            ISession m_session = NHibernateHttpModule.CurrentSession;

            string recordar_practicas = "";

            for (int i = 0; i < tabla.Length - 1; i++)
            {
                string[] fila = tabla[i].Split('#');


                string codigo = fila[1].ToString();


                if (recordar_practicas == "")
                    recordar_practicas = codigo + "#Si#False";
                else
                    recordar_practicas += ";" + codigo + "#Si#False";

                if (codigo != "")
                {
                    Item oItem = new Item();
                    oItem = (Item)oItem.Get(typeof(Item), "Codigo", codigo, "Baja", false);

                    string trajomuestra = fila[3].ToString();

                    ICriteria crit = m_session.CreateCriteria(typeof(DetalleProtocolo));
                    crit.Add(Expression.Eq("IdProtocolo", oRegistro));
                    crit.Add(Expression.Eq("IdItem", oItem));
                    IList listadetalle = crit.List();
                    if (listadetalle.Count == 0)
                    { //// si no está lo agrego --- si ya está no hago nada


                        DetalleProtocolo oDetalle = new DetalleProtocolo();
                        //Item oItem = new Item();
                        oDetalle.IdProtocolo = oRegistro;
                        oDetalle.IdEfector = oRegistro.IdEfector;



                        oDetalle.IdItem = oItem; // (Item)oItem.Get(typeof(Item), "Codigo", codigo);

                        if (dias_espera < oDetalle.IdItem.Duracion) dias_espera = oDetalle.IdItem.Duracion;

                        /*CheckBox a = ((CheckBox)(row.Cells[0].FindControl("CheckBox1")));
                        if (a.Checked)
                            oDetalle.TrajoMuestra = "Si";
                        else*/

                        if (trajomuestra == "true")
                            oDetalle.TrajoMuestra = "No";
                        else
                            oDetalle.TrajoMuestra = "Si";


                        oDetalle.FechaResultado = DateTime.Parse("01/01/1900");
                        oDetalle.FechaValida = DateTime.Parse("01/01/1900");
                        oDetalle.FechaControl = DateTime.Parse("01/01/1900");
                        oDetalle.FechaImpresion = DateTime.Parse("01/01/1900");
                        oDetalle.FechaEnvio = DateTime.Parse("01/01/1900");
                        oDetalle.FechaObservacion = DateTime.Parse("01/01/1900");
                        oDetalle.FechaValidaObservacion = DateTime.Parse("01/01/1900");


                        GuardarDetallePractica(oDetalle);
                    }
                    else  //si ya esta actualizo si trajo muestra o no
                    {
                        foreach (DetalleProtocolo oDetalle in listadetalle)
                        {
                            if (trajomuestra == "true")
                                oDetalle.TrajoMuestra = "No";
                            else
                                oDetalle.TrajoMuestra = "Si";

                            oDetalle.Save();
                        }

                    }
                }
            }

         


            
                // Modificacion de protocolo en proceso: Elimina los detalles que no se ingresaron por pantalla         
                //  ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria critBorrado = m_session.CreateCriteria(typeof(DetalleProtocolo));
                critBorrado.Add(Expression.Eq("IdProtocolo", oRegistro));
                IList detalleaBorrar = critBorrado.List();
                if (detalleaBorrar.Count > 0)
                {
                    foreach (DetalleProtocolo oDetalle in detalleaBorrar)
                    {
                        bool noesta = true;
                        //oDetalle.Delete();                     
                        //string[] tablaAux = TxtDatos.Value.Split('@');
                        for (int i = 0; i < tabla.Length - 1; i++)
                        {
                            string[] fila = tabla[i].Split('#');
                            string codigo = fila[1].ToString();
                            if (codigo != "")
                            {
                                if (codigo == oDetalle.IdItem.Codigo) noesta = false;

                            }
                        }
                        if (noesta)
                        {
                            oDetalle.Delete();                            
                            oDetalle.GrabarAuditoriaDetalleProtocolo("Elimina", int.Parse(Session["idUsuario"].ToString()));
                        }
                    }
                }
            

            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);

            //if (oCon.TipoCalculoDiasRetiro == 0)

            if (oRegistro.IdOrigen.IdOrigen == 1) /// Solo calcula con Calendario si es Externo
                if (oCon.TipoCalculoDiasRetiro == 0)  //Calcula con los días de espera del analisis
                    oRegistro.FechaRetiro = oRegistro.CalcularCalendarioEntrega(oRegistro.Fecha.AddDays(dias_espera));
                else   // calcula con los días predeterminados de espera
                    oRegistro.FechaRetiro = oRegistro.CalcularCalendarioEntrega(oRegistro.Fecha.AddDays(oCon.DiasRetiro));
            else
                oRegistro.FechaRetiro = oRegistro.Fecha.AddDays(dias_espera);


            if( oRegistro.IdTipoServicio.IdTipoServicio==3) oRegistro.IdMuestra = int.Parse(ddlMuestra.SelectedValue);
            oRegistro.Save();


        }
        private void GuardarDetalle2(Business.Data.Laboratorio.Protocolo oRegistro)
        {
            ///Eliminar los detalles para volverlos a crear            
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(DetalleProtocolo));
            crit.Add(Expression.Eq("IdProtocolo", oRegistro));
            IList detalle = crit.List();
            if (detalle.Count > 0)
            {
                foreach (DetalleProtocolo oDetalle in detalle)
                {
                    oDetalle.Delete();
                }
            }


            int dias_espera = 0;
            string[] tabla = TxtDatos.Value.Split('@');

            for (int i = 0; i < tabla.Length - 1; i++)
            {
                string[] fila = tabla[i].Split('#');


                string codigo = fila[1].ToString();
                if (codigo != "")
                {
                    DetalleProtocolo oDetalle = new DetalleProtocolo();
                    Item oItem = new Item();
                    oDetalle.IdProtocolo = oRegistro;
                    oDetalle.IdEfector = oRegistro.IdEfector;

                    string trajomuestra = fila[3].ToString();

                    oDetalle.IdItem = (Item)oItem.Get(typeof(Item), "Codigo", codigo);

                    if (dias_espera < oDetalle.IdItem.Duracion) dias_espera = oDetalle.IdItem.Duracion;

                    /*CheckBox a = ((CheckBox)(row.Cells[0].FindControl("CheckBox1")));
                    if (a.Checked)
                        oDetalle.TrajoMuestra = "Si";
                    else*/

                    if (trajomuestra == "true")
                        oDetalle.TrajoMuestra = "No";
                    else
                        oDetalle.TrajoMuestra = "Si";


                    oDetalle.FechaResultado = DateTime.Parse("01/01/1900");
                    oDetalle.FechaValida = DateTime.Parse("01/01/1900");
                    oDetalle.FechaControl = DateTime.Parse("01/01/1900");
                    oDetalle.FechaImpresion = DateTime.Parse("01/01/1900");
                    oDetalle.FechaEnvio = DateTime.Parse("01/01/1900");
                    oDetalle.FechaObservacion = DateTime.Parse("01/01/1900");
                    oDetalle.FechaValidaObservacion = DateTime.Parse("01/01/1900");
                    GuardarDetallePractica(oDetalle);
                }
            }


            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
          //  DateTime fechaentrega;
            //if (oCon.TipoCalculoDiasRetiro == 0)

            if (oRegistro.IdOrigen.IdOrigen == 1) /// Solo calcula con Calendario si es Externo
                if (oCon.TipoCalculoDiasRetiro == 0)  //Calcula con los días de espera del analisis
                    oRegistro.FechaRetiro = oRegistro.CalcularCalendarioEntrega(oRegistro.Fecha.AddDays(dias_espera));
                else   // calcula con los días predeterminados de espera
                    oRegistro.FechaRetiro = oRegistro.CalcularCalendarioEntrega(oRegistro.Fecha.AddDays(oCon.DiasRetiro));
            else
                oRegistro.FechaRetiro = oRegistro.Fecha.AddDays(dias_espera);




            oRegistro.Save();


        }



        private void GuardarDetallePractica(DetalleProtocolo oDet)
        {
            if (oDet.IdItem.IdEfector.IdEfector != oDet.IdItem.IdEfectorDerivacion.IdEfector) //Si es un item derivable no busca hijos y guarda directamente.
            {
                oDet.IdSubItem = oDet.IdItem;
                oDet.Save();
            }
            else
            {
                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit = m_session.CreateCriteria(typeof(PracticaDeterminacion));
                crit.Add(Expression.Eq("IdItemPractica", oDet.IdItem));
                IList detalle = crit.List();
                if (detalle.Count > 0)
                {
                    int i = 1;
                    foreach (PracticaDeterminacion oSubitem in detalle)
                    {
                        if (oSubitem.IdItemDeterminacion != 0)
                        {
                            Item oSItem = new Item();
                            if (i == 1)
                            {
                                oDet.IdSubItem = (Item)oSItem.Get(typeof(Item), oSubitem.IdItemDeterminacion);
                                oDet.Save();
                            }
                            else
                            {
                                DetalleProtocolo oDetalle = new DetalleProtocolo();
                                oDetalle.IdProtocolo = oDet.IdProtocolo;
                                oDetalle.IdEfector = oDet.IdEfector;
                                oDetalle.IdItem = oDet.IdItem;
                                oDetalle.IdSubItem = (Item)oSItem.Get(typeof(Item), oSubitem.IdItemDeterminacion);
                                oDetalle.TrajoMuestra = oDet.TrajoMuestra;

                                oDetalle.FechaResultado = DateTime.Parse("01/01/1900");
                                oDetalle.FechaValida = DateTime.Parse("01/01/1900");
                                oDetalle.FechaControl = DateTime.Parse("01/01/1900");
                                oDetalle.FechaImpresion = DateTime.Parse("01/01/1900");
                                oDetalle.FechaEnvio = DateTime.Parse("01/01/1900");
                                oDetalle.FechaObservacion = DateTime.Parse("01/01/1900");
                                oDetalle.FechaValidaObservacion = DateTime.Parse("01/01/1900");

                                oDetalle.Save();
                            }
                            i = i + 1;
                        }//fin if
                    }//fin foreach
                }
                else
                {
                    oDet.IdSubItem = oDet.IdItem;
                    oDet.Save();
                }//fin   if (detalle.Count > 0)  
            }



        }



        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si el sexo es femenino se habilita la selecció de Embarazada
            // HabilitarEmbarazada();
        }



        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///////Con la selección del item se muestra el codigo
            if (ddlItem.SelectedValue != "0")
            {
                Item oItem = new Item();
                oItem = (Item)oItem.Get(typeof(Item), int.Parse(ddlItem.SelectedValue));
                txtCodigo.Text = oItem.Codigo;

            }
            else
                txtCodigo.Text = "";

            txtCodigo.UpdateAfterCallBack = true;


        }



        

        protected void txtCodigo_TextChanged1(object sender, EventArgs e)
        {

        }

        //protected void btnCancelar_Click(object sender, EventArgs e)
        //{
        //    if (Request["Operacion"].ToString() == "Modifica")
        //    {
        //        if (Request["DesdeUrgencia"] != null)
        //            Response.Redirect("../Urgencia/UrgenciaList.aspx");
        //        else
        //        {
        //            switch (Request["Desde"].ToString())
        //            {
        //                case "Default": Response.Redirect("Default.aspx?idServicio=" + Session["idServicio"].ToString(), false); break;
        //                case "ProtocoloList": Response.Redirect("ProtocoloList.aspx?idServicio=" + Session["idServicio"].ToString() + "&Tipo=Lista"); break;
        //                case "Control": Response.Redirect("ProtocoloList.aspx?idServicio=" + Session["idServicio"].ToString() + "&Tipo=Control"); break;
        //                case "Urgencia": Response.Redirect("../Urgencia/UrgenciaList.aspx", false); break;
        //            }

        //            //if (Request["Control"] != null)
        //            //    Response.Redirect("ProtocoloList.aspx?idServicio=" + Session["idServicio"].ToString() + "&Tipo=Control");
        //            //else
        //            //    Response.Redirect("ProtocoloList.aspx?idServicio="+ Session["idServicio"].ToString()+"&Tipo=Lista");
        //        }
        //    }
        //    else
        //    {
        //        if (Request["Operacion"].ToString() == "AltaTurno")
        //            Response.Redirect("../turnos/Turnolist.aspx", false);
        //        else
        //        {
        //            if (Session["idUrgencia"].ToString() != "0")
        //                Response.Redirect("Default.aspx?idServicio=1&idUrgencia=" + Session["idUrgencia"].ToString(), false);
        //            else
        //                Response.Redirect("Default.aspx?idServicio=" + Session["idServicio"].ToString() + "&idUrgencia=" + Session["idUrgencia"].ToString(), false);
        //        }
        //    }
        //}




        protected void btnAgregarRutina_Click(object sender, EventArgs e)
        {
            // if (ddlRutina.SelectedValue != "0")
            // AgregarRutina();           

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




        //protected void imgCrearSolicitante_Click(object sender, ImageClickEventArgs e)
        //{
        //    txtMatricula.Text = "";
        //    txtApellidoSolicitante.Text = "";
        //    txtNombreSolicitante.Text = "";
        //    Panel1.Visible = true;

        //}

       

    

       
       
        protected void cvAnalisis_ServerValidate(object source, ServerValidateEventArgs args)
        {


        }

        protected void cvValidacionInput_ServerValidate(object source, ServerValidateEventArgs args)
        {
            TxtDatosCargados.Value = TxtDatos.Value;

            string sDatos = "";

            string[] tabla = TxtDatos.Value.Split('@');

            for (int i = 0; i < tabla.Length - 1; i++)
            {
                string[] fila = tabla[i].Split('#');
                string codigo = fila[1].ToString();
                string muestra = fila[2].ToString();

                if (sDatos == "")
                    sDatos = codigo + "#" + muestra;
                else
                    sDatos += ";" + codigo + "#" + muestra;

            }

            TxtDatosCargados.Value = sDatos;

            if (!VerificarAnalisisContenidos())
            {
                TxtDatos.Value = "";
                args.IsValid = false;

                return;
            }
            else
            {
                if ((TxtDatos.Value == "") || (TxtDatos.Value == "1###on@"))
                {

                    args.IsValid = false;
                    this.cvValidacionInput.ErrorMessage = "Debe completar al menos un análisis";
                    return;
                }
                else args.IsValid = true;



            
            }
        }

        private bool VerificarAnalisisContenidos()
        {
            bool devolver = true;
            string[] tabla = TxtDatos.Value.Split('@');
            string listaCodigo = "";

            for (int i = 0; i < tabla.Length - 1; i++)
            {
                string[] fila = tabla[i].Split('#');
                string codigo = fila[1].ToString();
                if (listaCodigo == "")
                    listaCodigo = "'" + codigo + "'";
                else
                    listaCodigo += ",'" + codigo + "'";

                int i_idItemPractica = 0;
                if (codigo != "")
                {

                    Item oItem = new Item();
                    oItem = (Item)oItem.Get(typeof(Item), "Codigo", codigo, "Baja", false);


                    i_idItemPractica = oItem.IdItem;
                    for (int j = 0; j < tabla.Length - 1; j++)
                    {
                        string[] fila2 = tabla[j].Split('#');
                        string codigo2 = fila2[1].ToString();
                        if ((codigo2 != "") && (codigo != codigo2))
                        {
                            Item oItem2 = new Item();
                            oItem2 = (Item)oItem2.Get(typeof(Item), "Codigo", codigo2, "Baja", false);

                            PracticaDeterminacion oGrupo = new PracticaDeterminacion();
                            oGrupo = (PracticaDeterminacion)oGrupo.Get(typeof(PracticaDeterminacion), "IdItemPractica", oItem, "IdItemDeterminacion", oItem2.IdItem);
                            if (oGrupo != null)
                            {

                                this.cvValidacionInput.ErrorMessage = "Ha cargado análisis contenidos en otros. Verifique los códigos " + codigo + " y " + codigo2 + "!";
                                devolver = false; break;

                            }

                        }
                    }////for           
                }/// if codigo
                if (!devolver) break;
            }

            if ((devolver) && (listaCodigo != ""))
            { devolver = VerificarAnalisisComplejosContenidos(listaCodigo); }

            return devolver;

        }

        private bool VerificarAnalisisComplejosContenidos(string listaCodigo)
        { ///Este es un segundo nivel de validacion en donde los analisis contenidos no estan directamente sino en diagramas
            bool devolver = true;
            string m_ssql = "SELECT  PD.idItemDeterminacion, I.codigo" +
                            " FROM         LAB_PracticaDeterminacion AS PD " +
                            " INNER JOIN   LAB_Item AS I ON PD.idItemPractica = I.idItem " +
                            " WHERE     I.codigo IN (" + listaCodigo + ") AND (I.baja = 0)" +
                            " ORDER BY PD.idItemDeterminacion ";

            NHibernate.Cfg.Configuration oConf = new NHibernate.Cfg.Configuration();
            String strconn = oConf.GetProperty("hibernate.connection.connection_string");
            SqlDataAdapter da = new SqlDataAdapter(m_ssql, strconn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string itempivot = "";
            string codigopivot = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][0].ToString() == itempivot)
                {
                    devolver = false;
                    cvValidacionInput.ErrorMessage = "Ha cargado análisis contenidos en otros. Verifique los códigos " + codigopivot + " y " + ds.Tables[0].Rows[i][1].ToString() + "!";
                    break;
                }
                codigopivot = ds.Tables[0].Rows[i][1].ToString();
                itempivot = ds.Tables[0].Rows[i][0].ToString();
            }
            return devolver;

        }

        //private bool VerificarAnalisisComplejosContenidos()
        //{
        //    throw new NotImplementedException();
        //}

        //protected void btnGuardarImprimir_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    { ///Verifica si se trata de un alta o modificacion de protocolo               
        //        Business.Data.Laboratorio.Protocolo oRegistro = new Business.Data.Laboratorio.Protocolo();
        //        if (Request["Operacion"].ToString() == "Modifica") oRegistro = (Business.Data.Laboratorio.Protocolo)oRegistro.Get(typeof(Business.Data.Laboratorio.Protocolo), int.Parse(Request["idProtocolo"].ToString()));
        //        Guardar(oRegistro);

        //        if (Request["idTurno"] != null)
        //            ActualizarTurno(Request["idTurno"].ToString(), oRegistro);


        //        //if (oC.GeneraComprobanteProtocolo)
        //            Response.Redirect("ProtocoloMensaje.aspx?id=" + oRegistro.IdProtocolo.ToString()+"&Operacion="+Request["Operacion"].ToString(), false);
        //        //else
        //        //if (Request["Operacion"].ToString() == "Modifica")
        //        //    Response.Redirect("ProtocoloList.aspx", false);
        //        //else
        //        //{
        //        //    if (Request["Operacion"].ToString() == "AltaTurno")
        //        //        Response.Redirect("../turnos/Turnolist.aspx", false);
        //        //    else
        //        //        Response.Redirect("Default.aspx", false);
        //        //}
        //    }
        //}
       

        protected void lnkSiguiente_Click(object sender, EventArgs e)
        {
            Avanzar(1);
        }

        protected void lnkAnterior_Click(object sender, EventArgs e)
        {
            Avanzar(-1);
        }

        private void Avanzar(int avance)
        {

            int ProtocoloActual = int.Parse(Request["idProtocolo"].ToString());
            //Protocolo oProtocoloActual = new Protocolo();
            //oProtocoloActual = (Protocolo)oProtocoloActual.Get(typeof(Protocolo), ProtocoloActual);
            int ProtocoloNuevo = ProtocoloActual;

            if (Session["ListaProtocolo"] != null)
            {
                string[] lista = Session["ListaProtocolo"].ToString().Split(',');
                for (int i = 0; i < lista.Length; i++)
                {
                    if (ProtocoloActual == int.Parse(lista[i].ToString()))
                    {
                        if (avance == 1)
                        {
                            if (i < lista.Length - 1)
                            {
                                ProtocoloNuevo = int.Parse(lista[i + 1].ToString()); break;
                            }
                        }
                        else  //retrocede                        
                        {
                            if (i > 0)
                            {
                                ProtocoloNuevo = int.Parse(lista[i - 1].ToString()); break;
                            }
                        }


                    }
                }
            }
            //if (avance == 1)
            //{
            //    ProtocoloNuevo = ProtocoloActual+1;
            //}
            //else  //retrocede                        
            //    ProtocoloNuevo = ProtocoloActual - 1;



            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(Protocolo));
            crit.Add(Expression.Eq("IdProtocolo", ProtocoloNuevo));
            //crit.Add(Expression.Eq("IdSector", oProtocoloActual.IdSector));
            Protocolo oProtocolo = (Protocolo)crit.UniqueResult();

            string m_parametro = "";
            if (Request["DesdeUrgencia"] != null) m_parametro = "&DesdeUrgencia=1";

            if (oProtocolo != null)
            {
                //if (Request["Desde"].ToString() == "Control")
                Response.Redirect("ProtocoloEdit2.aspx?Desde=" + Request["Desde"].ToString() + "&idServicio=" + Session["idServicio"].ToString() + "&Operacion=Modifica&idProtocolo=" + ProtocoloNuevo + m_parametro);
                //else
                //    Response.Redirect("ProtocoloEdit2.aspx?Desde="+Request["Desde"].ToString()+"&idServicio=" + Session["idServicio"].ToString() + "&Operacion=Modifica&idProtocolo=" + ProtocoloNuevo + m_parametro);
            }
            else
                Response.Redirect("ProtocoloEdit2.aspx?Desde=" + Request["Desde"].ToString() + "&idServicio=" + Session["idServicio"].ToString() + "&Operacion=Modifica&idProtocolo=" + ProtocoloActual + m_parametro);

        }



        protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
                Response.Redirect("ProtocoloEdit2.aspx?Desde=" + Request["Desde"].ToString() + "&idServicio=" + Request["idServicio"].ToString() + "&Operacion=Modifica&idProtocolo=" + e.CommandArgument.ToString());


        }

      


    }
}

