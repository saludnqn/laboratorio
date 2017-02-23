using System;
using System.Collections;


using System.Data;

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Business;
using System.Data.SqlClient;
using System.IO;
using CrystalDecisions.Shared;
using Business.Data.Laboratorio;
using CrystalDecisions.Web;
using System.Text;

namespace WebLab.Protocolos
{
    public partial class ProtocoloList : System.Web.UI.Page
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
              
            }
        }


        private void Inicializar()
        {
            txtProtocoloDesde.Focus();
            txtFechaDesde.Value = DateTime.Now.ToShortDateString();
            txtFechaHasta.Value = DateTime.Now.ToShortDateString();
            CargarListas();
            switch (Request["Tipo"].ToString())
            {
                case "Lista":
                    {
                        PreventingDoubleSubmit(btnBuscar);
                        chkRecordarFiltro.Visible = true;
                        VerificaPermisos("Lista de Protocolos");       
                        lblTitulo.Text = "LISTA DE PROTOCOLOS";
                        pnlLista.Visible = true;
                        pnlListadoOrdenado.Visible = false;
                        pnlImpresion.Visible = false;
                        pnlControl.Visible = false;
                        IniciarValores();
                        CargarGrilla();
                        
                    } break;
                case "ListadoOrdenado":
                    {
                        VerificaPermisos("Listado Ordenado");
                        lblTitulo.Text = "LISTADO ORDENADO";
                        pnlLista.Visible = false;
                        pnlListadoOrdenado.Visible = true;
                        pnlImpresion.Visible = true;
                        pnlControl.Visible = false;
                        btnBuscarExportar.Visible = false;
                        Configuracion oC = new Configuracion();  oC = (Configuracion)oC.Get(typeof(Configuracion), "IdConfiguracion", 1);                        
                        rdbTipoListaProtocolo.SelectedValue = oC.TipoListaProtocolo.ToString();
                        PreventingDoubleSubmit(btnBuscar);
                    }
                    break;
                case "Control":
                    {
                        VerificaPermisos("Control de Protocolos");
                        lblTitulo.Text = "CONTROL DE PROTOCOLOS CARGADOS";
                        pnlLista.Visible = false;
                        pnlListadoOrdenado.Visible = false;
                        pnlImpresion.Visible = false;
                        pnlControl.Visible = true;
                        PreventingDoubleSubmit(btnBuscarControl);
                    }
                    break;
                case "Exportacion":
                    {
                        
                        chkWhonet.Visible = true;
                        VerificaPermisos("Exportacion de datos");
                        lblTitulo.Text = "EXPORTACION DE DATOS";
                        pnlLista.Visible = false;
                        pnlListadoOrdenado.Visible = true;
                        pnlImpresion.Visible = false;
                        pnlControl.Visible = false;
                        btnBuscarExportar.Visible = true;
                        PreventingDoubleSubmit(btnBuscarExportar);
                    }
                    break;
            }

        }
        private int Permiso /*el permiso */
        {
            get { return ViewState["Permiso"] == null ? 0 : int.Parse(ViewState["Permiso"].ToString()); }
            set { ViewState["Permiso"] = value; }
        }

        private void VerificaPermisos(string sObjeto)
        {
            if (Session["idUsuario"] != null)
            {
                if (Session["s_permiso"] != null)
                {
                Utility oUtil = new Utility();
                Permiso = oUtil.VerificaPermisos((ArrayList)Session["s_permiso"], sObjeto);
                switch (Permiso)
                {
                    case 0: Response.Redirect("../AccesoDenegado.aspx", false); break;
                  
                   }
                }
                else Response.Redirect("../FinSesion.aspx", false);
            }
            else Response.Redirect("../FinSesion.aspx", false);
        }
        private void PreventingDoubleSubmit(Button button)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("if (typeof(Page_ClientValidate) == ' ') { ");
            sb.Append("var oldPage_IsValid = Page_IsValid; var oldPage_BlockSubmit = Page_BlockSubmit;");
            sb.Append("if (Page_ClientValidate('" + button.ValidationGroup + "') == false) {");
            sb.Append(" Page_IsValid = oldPage_IsValid; Page_BlockSubmit = oldPage_BlockSubmit; return false; }} ");
            sb.Append("this.value = 'Procesando...';");
            sb.Append("this.disabled = true;");
            sb.Append(ClientScript.GetPostBackEventReference(button, null) + ";");
            sb.Append("return true;");

            string submit_Button_onclick_js = sb.ToString();
            button.Attributes.Add("onclick", submit_Button_onclick_js);
        }

        private void CargarListas()
        {
            Utility oUtil = new Utility();

            if (Request["idServicio"] != null) Session["idServicio"] = Request["idServicio"].ToString();

            string m_ssql = "select idTipoServicio, nombre from Lab_TipoServicio where baja=0 ";
            if (Request["Tipo"].ToString() == "Lista")
                m_ssql += " and idTipoServicio= " + Request["idServicio"].ToString();

            oUtil.CargarCombo(ddlServicio, m_ssql, "idTipoServicio", "nombre");


            if (Request["Tipo"].ToString() == "Exportacion") ddlEstado.SelectedValue = "2";

            if ((Request["Tipo"].ToString() == "ListadoOrdenado") ||
                (Request["Tipo"].ToString() == "Exportacion"))
            {

                m_ssql = "SELECT idMuestra, nombre + ' - ' + codigo as nombre FROM LAB_Muestra   where baja=0  order by nombre ";
                //oUtil.CargarCombo(ddlMuestra, m_ssql, "idMuestra", "nombre");
                //ddlMuestra.Items.Insert(0, new ListItem("--Todas--", "0"));
                oUtil.CargarListBox(lstMuestra, m_ssql, "idMuestra", "nombre");
                for (int i = 0; i < lstMuestra.Items.Count; i++)
                {
                    lstMuestra.Items[i].Selected = true;
                }

                //oUtil.CargarCheckBox(chkMuestra, m_ssql, "idMuestra", "nombre");

                ddlItem.Items.Insert(0, new ListItem("--Todos--", "0"));
                if (Request["Tipo"].ToString() == "ListadoOrdenado") ddlServicio.Items.Insert(0, new ListItem("-- Todos --", "0"));
            }

            m_ssql = "SELECT idArea, nombre FROM LAB_Area where baja=0  order by nombre ";
            oUtil.CargarCombo(ddlArea, m_ssql, "idArea", "nombre");
            ddlArea.Items.Insert(0, new ListItem("--Todas--", "0"));

            ddlItem.Items.Insert(0, new ListItem("--Todas--", "0"));



            //////////////////////////Carga de combos de ObraSocial//////////////////////////////////////////
            m_ssql = "SELECT idObraSocial,  nombre AS nombre FROM Sys_ObraSocial order by idObraSocial ";
            oUtil.CargarCombo(ddlObraSocial, m_ssql, "idObraSocial", "nombre");
            ddlObraSocial.Items.Insert(0, new ListItem("--Todos--", "0"));
            //////////////////////////////////////////////////////////////////////////////////////////////////

            ///Carga de combos de Origen
            m_ssql = "SELECT  idOrigen, nombre FROM LAB_Origen WHERE (baja = 0)";
            oUtil.CargarCombo(ddlOrigen, m_ssql, "idOrigen", "nombre");
            ddlOrigen.Items.Insert(0, new ListItem("-- Todos --", "0"));

            ///Carga de combos de Prioridad
            m_ssql = "SELECT idPrioridad, nombre FROM LAB_Prioridad WHERE     (baja = 0)";
            oUtil.CargarCombo(ddlPrioridad, m_ssql, "idPrioridad", "nombre");
            ddlPrioridad.Items.Insert(0, new ListItem("-- Todos --", "0"));


            ///Carga de Sectores
            m_ssql = "SELECT idSectorServicio,  nombre  + ' - ' + prefijo as nombre FROM LAB_SectorServicio WHERE (baja = 0) order by nombre";
            oUtil.CargarCombo(ddlSectorServicio, m_ssql, "idSectorServicio", "nombre");
            ddlSectorServicio.Items.Insert(0, new ListItem("-- Todos --", "0"));

            ///Carga de combos de Efector
            m_ssql = "SELECT idEfector, nombre FROM sys_Efector order by nombre ";
            oUtil.CargarCombo(ddlEfector, m_ssql, "idEfector", "nombre");
            ddlEfector.Items.Insert(0, new ListItem("-- Todos --", "0"));

            ///Carga de combos de Medicos Solicitantes
            m_ssql = "SELECT idProfesional, apellido + ' ' + nombre AS nombre FROM Sys_Profesional ORDER BY apellido, nombre ";
            oUtil.CargarCombo(ddlEspecialista, m_ssql, "idProfesional", "nombre");
            ddlEspecialista.Items.Insert(0, new ListItem("-- Todos --", "0"));


            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
            oCon = (Configuracion)oCon.Get(typeof(Configuracion), "IdConfiguracion", 1);

            gvLista.PageSize = oCon.CantidadProtocolosPorPagina;


            if (Request["idServicio"].ToString() != "1")//microbiologia o pesquisa
            {
                //////El rango de fechas no es a fecha actual, sino los ultimos 30 dias
                txtFechaDesde.Value = DateTime.Now.AddDays(-10).ToShortDateString();
                txtFechaHasta.Value = DateTime.Now.ToShortDateString();

                //// La prioridad siempre es a rutina
                ddlPrioridad.SelectedValue = "1"; //rutina
                ddlPrioridad.Enabled = false;
                lblNumeroTarjeta.Visible = true;
                txtNumeroTarjeta.Visible = true;
                cvNumeroTarjeta.Enabled = true;
                txtNumeroTarjeta.Focus();
            }else
            {
                cvNumeroTarjeta.Enabled = false;
                lblNumeroTarjeta.Visible = false;
                txtNumeroTarjeta.Visible = false;
            }
            ///////////////Impresoras////////////////////////

            if (Request["Tipo"].ToString() == "ListadoOrdenado")
            {
                m_ssql = "SELECT idImpresora, nombre FROM LAB_Impresora ";
                oUtil.CargarCombo(ddlImpresora, m_ssql, "nombre", "nombre");
                if (Session["Impresora"] != null) ddlImpresora.SelectedValue = Session["Impresora"].ToString();
            }
            ///////////////////////////////////////

            //
            m_ssql = null;
            oUtil = null;
        }

        private void AlmacenarSesion()
        {
            string s_valores = "txtFechaDesde:" + txtFechaDesde.Value;
            s_valores += ";txtFechaHasta:" + txtFechaHasta.Value;
            s_valores += ";ddlSectorServicio:" + ddlSectorServicio.SelectedValue;
            s_valores += ";txtProtocoloDesde:" + txtProtocoloDesde.Value;
            s_valores += ";txtProtocoloHasta:" + txtProtocoloHasta.Value;
            s_valores += ";ddlOrigen:" + ddlOrigen.SelectedValue;           
            s_valores += ";ddlPrioridad:" + ddlPrioridad.SelectedValue;
            s_valores += ";ddlEfector:" + ddlEfector.SelectedValue;
            s_valores += ";ddlEspecialista:" + ddlEspecialista.SelectedValue;
            s_valores += ";txtDni:" +txtDni.Value;
            s_valores += ";txtApellido:" +txtApellido.Text;
            s_valores += ";txtNombre:" + txtNombre.Text;
            s_valores += ";ddlEstado:" + ddlEstado.SelectedValue;
            s_valores += ";txtNroOrigen:" + txtNroOrigen.Text;
            
            Session["FiltroProtocolo"] = s_valores;
        }


        private void IniciarValores()
        {
            if (Session["FiltroProtocolo"] != null)
            {
                string[] arr = Session["FiltroProtocolo"].ToString().Split((";").ToCharArray());
                foreach (string item in arr)
                {
                    string[] s_control = item.Split((":").ToCharArray());
                    switch (s_control[0].ToString())
                    {                       
                        case "txtFechaDesde": txtFechaDesde.Value = s_control[1].ToString(); break;
                        case "txtFechaHasta": txtFechaHasta.Value = s_control[1].ToString(); break;
                        case "ddlSectorServicio": ddlSectorServicio.SelectedValue = s_control[1].ToString(); break;


                        case "txtProtocoloDesde": txtProtocoloDesde.Value = s_control[1].ToString(); break;
                        case "txtProtocoloHasta": txtProtocoloHasta.Value = s_control[1].ToString(); break;

                        case "txtNroOrigen": txtNroOrigen.Text = s_control[1].ToString(); break;

                        case "ddlOrigen": ddlOrigen.SelectedValue = s_control[1].ToString(); break;

                        case "ddlPrioridad": ddlPrioridad.SelectedValue = s_control[1].ToString(); break;
                        case "ddlEfector": ddlEfector.SelectedValue = s_control[1].ToString(); break;
                        case "ddlEspecialista": ddlEspecialista.SelectedValue = s_control[1].ToString(); break;


                        case "txtDni": txtDni.Value = s_control[1].ToString(); break;
                        case "txtApellido": txtApellido.Text = s_control[1].ToString(); break;
                        case "txtNombre": txtNombre.Text = s_control[1].ToString(); break;
                        case "ddlEstado": ddlEstado.SelectedValue = s_control[1].ToString(); break;
                      

                    }
                }
            }

        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (chkRecordarFiltro.Checked) AlmacenarSesion();
                CargarGrilla();
                CurrentPageLabel.Text = " ";
            }
            else
                PintarReferencias();
            
        }
        private void CargarGrilla()
        {          
            gvLista.DataSource = LeerDatos(0);           
            gvLista.DataBind();
            PintarReferencias();                      
        }

        private DataTable LeerDatos(int tipo)
        {
            string str_condicion = " 1=1 ";
            
            DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
            DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);

            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
            switch (oCon.TipoNumeracionProtocolo)
            {
                case 0: // busqueda con autonumerico
                    {
                        if (txtProtocoloDesde.Value != "") str_condicion += " AND P.numero >= " + int.Parse(txtProtocoloDesde.Value);
                        if (txtProtocoloHasta.Value != "") str_condicion += " AND P.numero <= " + int.Parse(txtProtocoloHasta.Value);
                    } break;

                case 1: //busqueda con numero diario
                    {
                        if (txtProtocoloDesde.Value != "") str_condicion += " AND P.numeroDiario >= " + int.Parse(txtProtocoloDesde.Value);
                        if (txtProtocoloHasta.Value != "") str_condicion += " AND P.numeroDiario <= " + int.Parse(txtProtocoloHasta.Value);
                    }
                    break;
                case 2: //busqueda con numero de grupo
                    {
                        if (txtProtocoloDesde.Value != "") str_condicion += " AND P.numeroSector >= " + int.Parse(txtProtocoloDesde.Value);
                        if (txtProtocoloHasta.Value != "") str_condicion += " AND P.numeroSector <= " + int.Parse(txtProtocoloHasta.Value);
                    }
                    break;
                case 3:
                    {
                        if (txtProtocoloDesde.Value != "") str_condicion += " And P.numeroTipoServicio>=" + int.Parse(txtProtocoloDesde.Value);
                        if (txtProtocoloHasta.Value != "") str_condicion += " AND  P.numeroTipoServicio<=" + int.Parse(txtProtocoloHasta.Value);
                    } break;
            }
            if (txtNumeroTarjeta.Value != "") str_condicion += " And S.numeroTarjeta=" + int.Parse(txtNumeroTarjeta.Value);
            if ((Request["Tipo"].ToString() == "ListadoOrdenado") || (Request["Tipo"].ToString() == "Exportacion"))
            {
                if (lstMuestra.Visible)
                {
                    string listaM = getListaMuestra();
                    if (listaM != "") str_condicion += " AND P.idMuestra  in  (" + listaM + ")"; // ddlMuestra.SelectedValue;
                }
                

                //if (ddlMuestra.SelectedValue != "0") str_condicion += " AND P.idMuestra = " + ddlMuestra.SelectedValue;
            }

            if (ddlObraSocial.SelectedValue != "0") str_condicion += " AND P.idObraSocial = " + ddlObraSocial.SelectedValue;
            if (ddlServicio.SelectedValue != "0") str_condicion += " AND P.idTipoServicio = " + ddlServicio.SelectedValue;
            if (ddlSectorServicio.SelectedValue != "0") str_condicion += " AND P.idSector = " + ddlSectorServicio.SelectedValue;
            if (txtFechaDesde.Value != "") str_condicion += " AND P.fecha>= '" + fecha1.ToString("yyyyMMdd") + "'";
            if (txtFechaHasta.Value != "") str_condicion += " AND P.fecha<= '" + fecha2.ToString("yyyyMMdd") + "'";

            if (txtNroOrigen.Text != "") str_condicion += " And P.numeroOrigen='" + txtNroOrigen.Text + "'";
          
            if (ddlOrigen.SelectedValue != "0") str_condicion += " AND P.idOrigen = " + ddlOrigen.SelectedValue;
            if (ddlPrioridad.SelectedValue != "0") str_condicion += " AND P.idPrioridad = " + ddlPrioridad.SelectedValue;
            if (ddlEfector.SelectedValue != "0") str_condicion += " AND P.idEfectorSolicitante = " + ddlEfector.SelectedValue;
            if (ddlEspecialista.SelectedValue != "0") str_condicion += " AND P.idEspecialistaSolicitante = " + ddlEspecialista.SelectedValue;
            if (txtDni.Value != "") str_condicion += " AND Pa.numeroDocumento = '" + txtDni.Value + "'";
            if (txtApellido.Text != "") str_condicion += " AND Pa.apellido like '%" + txtApellido.Text.TrimEnd() + "%'";
            if (txtNombre.Text != "") str_condicion += " AND Pa.nombre like '%" + txtNombre.Text.TrimEnd() + "%'";
            if (ddlEstado.SelectedValue == "-1")
            {
                str_condicion += " AND P.baja=0";
            }
            else
            {
                if (ddlEstado.SelectedValue != "3")
                    str_condicion += " AND P.estado=" + ddlEstado.SelectedValue;
                else
                    str_condicion += " AND P.baja=1";
            }
            //if (rdbPaciente.SelectedValue == "1")
            //{
            //    str_condicion += " AND ((P.sexo='F' and P.edad<64) OR (P.sexo='M' and P.edad<19)) ";
            //}         

            /////////////

            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.CommandText = "[LAB_ListaProtocolo]";

            cmd.Parameters.Add("@FiltroBusqueda", SqlDbType.NVarChar);
            cmd.Parameters["@FiltroBusqueda"].Value = str_condicion;

            cmd.Parameters.Add("@TipoLista", SqlDbType.Int);
            cmd.Parameters["@TipoLista"].Value = tipo;

            cmd.Parameters.Add("@idArea", SqlDbType.Int);
            cmd.Parameters["@idArea"].Value = ddlArea.SelectedValue;

            cmd.Parameters.Add("@idItem", SqlDbType.Int);
            cmd.Parameters["@idItem"].Value = ddlItem.SelectedValue;                    
            
            cmd.Connection = conn;


            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(Ds);
            //////////


            //(count(*)/@RegistrosPorPagina)+1 

            CantidadRegistros.Text = Ds.Tables[0].Rows.Count.ToString() + " registros encontrados";

            //   Session["ListaProtocolo"] = listaProtocolos(Ds.Tables[0]);

            return Ds.Tables[0];
        }

        private string getListaMuestra()
        {
            string m_lista = ""; bool todasseleccionadas = true;
            for (int i = 0; i < lstMuestra.Items.Count; i++)
            {
                if (lstMuestra.Items[i].Selected)
                {
                    if (m_lista == "")
                        m_lista = lstMuestra.Items[i].Value;
                    else
                        m_lista += "," + lstMuestra.Items[i].Value;
                }
                else todasseleccionadas = false;
            }
            
            if  (todasseleccionadas) m_lista = "";
           
            return m_lista;
        }


        protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            switch (e.CommandName)
            {
                case "Modificar":
                    {
                        Business.Data.Laboratorio.Protocolo oProtocolo = new Business.Data.Laboratorio.Protocolo();
                        oProtocolo = (Business.Data.Laboratorio.Protocolo)oProtocolo.Get(typeof(Business.Data.Laboratorio.Protocolo), int.Parse(e.CommandArgument.ToString()));
                        if (Request["idServicio"].ToString()=="4")
                        Response.Redirect("ProtocoloEditPesquisa.aspx?idServicio="+ Request["idServicio"].ToString()+"&Desde=ProtocoloList&Operacion=Modifica&idProtocolo=" + oProtocolo.IdProtocolo);
                        else
                            Response.Redirect("ProtocoloEdit2.aspx?idServicio=" + Request["idServicio"].ToString() + "&Desde=ProtocoloList&Operacion=Modifica&idProtocolo=" + oProtocolo.IdProtocolo);
                        break;
                    }
                case "Imprimir":
                    {
                        Imprimir(e.CommandArgument, "I");
                        PintarReferencias();
                        break;
                    }
                case "Pdf":
                    {

                        Imprimir(e.CommandArgument, "P");
                        CargarGrilla();// PintarReferencias();
                        break;
                    }
                case "Anular":
                    {
                        Anular(e.CommandArgument);
                       // PintarReferencias();
                        break;
                    }
            }           
        }

        private void Imprimir(object p, string p_2)
        {            
        
            Business.Data.Laboratorio.Protocolo oProt = new Business.Data.Laboratorio.Protocolo();
            oProt = (Business.Data.Laboratorio.Protocolo)oProt.Get(typeof(Business.Data.Laboratorio.Protocolo),int.Parse( p.ToString()));

            oProt.GrabarAuditoriaProtocolo("Genera PDF Comprobante", int.Parse(Session["idUsuario"].ToString()));       

            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
            oCon = (Configuracion)oCon.Get(typeof(Configuracion), "IdConfiguracion", 1);

            ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
            encabezado1.Value = oCon.EncabezadoLinea1;

            ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
            encabezado2.Value = oCon.EncabezadoLinea2;

            ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
            encabezado3.Value = oCon.EncabezadoLinea3;


            ParameterDiscreteValue tipoNumeracion = new ParameterDiscreteValue();
            tipoNumeracion.Value = oCon.TipoNumeracionProtocolo;


            

            oCr.Report.FileName = "../Informes/Protocolo.rpt";
            oCr.ReportDocument.SetDataSource(oProt.GetDataSet("Protocolo", "", oProt.IdTipoServicio.IdTipoServicio)); 
            
            oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
            oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
            oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
            oCr.ReportDocument.ParameterFields[3].CurrentValues.Add(tipoNumeracion);

            oCr.DataBind();
            if (p_2 == "I")
                 oCr.ReportDocument.PrintToPrinter(1, false, 0,0);
            else
            {

                MemoryStream oStream; // using System.IO
                oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=Protocolo.pdf");

                Response.BinaryWrite(oStream.ToArray());
                Response.End();
            }

        }

        
        private void Anular(object p)
        {
            Business.Data.Laboratorio.Protocolo oRegistro = new Business.Data.Laboratorio.Protocolo();
            oRegistro = (Business.Data.Laboratorio.Protocolo)oRegistro.Get(typeof(Business.Data.Laboratorio.Protocolo), int.Parse(p.ToString()));

            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
            oCon = (Configuracion)oCon.Get(typeof(Configuracion), "IdConfiguracion", 1);

            if (oRegistro.Estado == 2) 
            {
                if (oCon.EliminarProtocoloTerminado)
                {
                    oRegistro.Baja = true;
                    oRegistro.Save();
                    oRegistro.GrabarAuditoriaProtocolo("Elimina Protocolo",  int.Parse(Session["idUsuario"].ToString()));       
                  
                }
                else
                {
                    string popupScript = "<script language='JavaScript'> alert('No es posible eliminar un protocolo terminado.')</script>";
                    Page.RegisterClientScriptBlock("PopupScript", popupScript);
                }
            }
            else
            {
                oRegistro.Baja = true;
                oRegistro.Save();

                oRegistro.GrabarAuditoriaProtocolo("Elimina Protocolo", int.Parse(Session["idUsuario"].ToString()));  
            }

            
            CargarGrilla();
            CurrentPageLabel.Text = " ";
            
        }

        protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             if (e.Row.Cells.Count > 1)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton CmdModificar = (ImageButton)e.Row.Cells[13].Controls[1];
                    CmdModificar.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                    CmdModificar.CommandName = "Modificar";
                    CmdModificar.ToolTip = "Modificar";


                    ImageButton CmdImprimir = (ImageButton)e.Row.Cells[14].Controls[1];
                    CmdImprimir.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                    CmdImprimir.CommandName = "Imprimir";
                    CmdImprimir.ToolTip = "Imprimir";


                    ImageButton CmdPdf = (ImageButton)e.Row.Cells[15].Controls[1];
                    CmdPdf.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                    CmdPdf.CommandName = "Pdf";
                    CmdPdf.ToolTip = "Enviar a PDF";

                    ImageButton CmdEliminar = (ImageButton)e.Row.Cells[16].Controls[1];
                    CmdEliminar.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                    CmdEliminar.CommandName = "Anular";
                    CmdEliminar.ToolTip = "Anular";

           

                    if (Permiso == 1)
                    {
                        CmdEliminar.Visible = false;
                        CmdModificar.ToolTip = "Consultar";
                    }

                              Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
            oCon = (Configuracion)oCon.Get(typeof(Configuracion), "IdConfiguracion", 1);


            if (oCon.EliminarProtocoloTerminado) CmdEliminar.Visible = true;
            else
                CmdEliminar.Visible = false;
           

                }

                    //Configuracion oCon = new Configuracion();              oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
                    ////     CmdImprimir.Visible = oCon.GeneraComprobanteTurno;
                    //e.Row.Cells[13].Visible = oCon.GeneraComprobanteProtocolo;
                    //e.Row.Cells[14].Visible = oCon.GeneraComprobanteProtocolo;
                   

              

                

            }  
        }

        private void PintarReferencias()
        {
          

            
            foreach (GridViewRow row in gvLista.Rows)
            {                
                    switch (row.Cells[0].Text)
                    {
                        case "0": ///Abierto
                            {
                                Image hlnk = new Image();
                                hlnk.ImageUrl = "~/App_Themes/default/images/rojo.gif";
                                row.Cells[0].Controls.Add(hlnk);
                            }
                            break;
                        case "1": //en proceso
                            {
                                Image hlnk = new Image();
                                hlnk.ImageUrl = "~/App_Themes/default/images/amarillo.gif";
                               row.Cells[0].Controls.Add(hlnk);
                            }
                            break;
                        case "2": //terminado
                            {
                                Image hlnk = new Image();
                                hlnk.ImageUrl = "~/App_Themes/default/images/verde.gif";
                                row.Cells[0].Controls.Add(hlnk);
                            }
                            break;
                    }

                    switch (row.Cells[1].Text)
                    {
                        case "True":
                            {
                                Image hlnk = new Image();
                                hlnk.ImageUrl = "~/App_Themes/default/images/impreso.jpg";
                                hlnk.ToolTip = "Protocolo Impreso";
                               row.Cells[1].Controls.Add(hlnk);
                            }
                            break;
                        case "False":
                            {
                                Image hlnk = new Image();
                                hlnk.ImageUrl = "~/App_Themes/default/images/transparente.jpg";
                               row.Cells[1].Controls.Add(hlnk);
                            }
                            break;

                    }                

            }
        
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
           
        }


        private string listaProtocolos(DataTable dt)
        {           
            string m_lista = "";         
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (m_lista == "")
                    m_lista += dt.Rows[i][0].ToString();
                else
                    m_lista += "," + dt.Rows[i][0].ToString();
            }           
            return m_lista;

        }

        private void ImprimirListado(string tipo)
        {
            //Business.Data.Laboratorio.Protocolo oProt = new Business.Data.Laboratorio.Protocolo();
            //oProt = (Business.Data.Laboratorio.Protocolo)oProt.Get(typeof(Business.Data.Laboratorio.Protocolo), int.Parse(p.ToString()));

            Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
            oCon = (Configuracion)oCon.Get(typeof(Configuracion), "IdConfiguracion", 1);

            ParameterDiscreteValue encabezado1 = new ParameterDiscreteValue();
            encabezado1.Value = oCon.EncabezadoLinea1;

            ParameterDiscreteValue encabezado2 = new ParameterDiscreteValue();
            encabezado2.Value = oCon.EncabezadoLinea2;

            ParameterDiscreteValue encabezado3 = new ParameterDiscreteValue();
            encabezado3.Value = oCon.EncabezadoLinea3;

            ParameterDiscreteValue tipoNumeracion = new ParameterDiscreteValue();
            tipoNumeracion.Value = oCon.TipoNumeracionProtocolo;

            ParameterDiscreteValue fechaDesde = new ParameterDiscreteValue();
            fechaDesde.Value = txtFechaDesde.Value;

            ParameterDiscreteValue fechaHasta = new ParameterDiscreteValue();
            fechaHasta.Value =txtFechaHasta.Value;


            string m_reporte = "../Informes/ListaProtocolo.rpt";
            int tiporeporte = 0;
            switch (rdbTipoListaProtocolo.SelectedValue)
            {
                case "0": //Reducida por Nombre
                { 
                    tiporeporte = 2; m_reporte = "../Informes/ListaProtocoloReducido.rpt";     
                }
                    break;
                case "1":  //Normal Extendidos            
                {
                    tiporeporte = 1; m_reporte = "../Informes/ListaProtocolo.rpt";                 
                }
                    break;
                case "2": //Reducida por Codigo+Nombre
                { tiporeporte = 3; m_reporte = "../Informes/ListaProtocoloReducido.rpt";
                } break;
            }

            oCr.Report.FileName = m_reporte;
            oCr.ReportDocument.SetDataSource(LeerDatos(tiporeporte));
            oCr.ReportDocument.ParameterFields[0].CurrentValues.Add(encabezado1);
            oCr.ReportDocument.ParameterFields[1].CurrentValues.Add(encabezado2);
            oCr.ReportDocument.ParameterFields[2].CurrentValues.Add(encabezado3);
            oCr.ReportDocument.ParameterFields[3].CurrentValues.Add(tipoNumeracion);
            oCr.ReportDocument.ParameterFields[4].CurrentValues.Add(fechaDesde);
            oCr.ReportDocument.ParameterFields[5].CurrentValues.Add(fechaHasta);
            oCr.DataBind();
            if (tipo == "I")
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
                    //    exception = ex.Message + "<br>";

                    //}
                    string popupScript = "<script language='JavaScript'> alert('No se pudo imprimir en la impresora " + Session["Impresora"].ToString() + ". Si el problema persiste consulte con soporte técnico." + exception + "'); </script>";
                    Page.RegisterStartupScript("PopupScript", popupScript);
                }
            }
            else
            {
                oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Listado_Ordenado");

                //MemoryStream oStream; // using System.IO
                //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("Content-Disposition", "attachment;filename=Listado_Ordenado.pdf");

                //Response.BinaryWrite(oStream.ToArray());
                //Response.End();
            }
        }

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            ImprimirListado("PDF");
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {


            gvLista.PageIndex = e.NewPageIndex;

            int currentPage = gvLista.PageIndex + 1;
            CurrentPageLabel.Text = "Página " + currentPage.ToString() + " de " + gvLista.PageCount.ToString();            
            CargarGrilla();
        }

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            ImprimirListado("I");
        }

        protected void btnBuscarControl_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                Session["ListaProtocolo"] = listaProtocolos(LeerDatos(5));
                if (Session["ListaProtocolo"] != null)
                {
                    if (Session["ListaProtocolo"].ToString() != "")
                    {
                        string[] array = Session["ListaProtocolo"].ToString().Split(',');
                        string primerProtocolo = array[0].ToString().Trim();
                        if (ddlServicio.SelectedValue=="4") ///pesquisa
                            Response.Redirect("ProtocoloEditPesquisa.aspx?idServicio=" + ddlServicio.SelectedValue + "&Desde=Control&Operacion=Modifica&idProtocolo=" + primerProtocolo);
                        else
                        Response.Redirect("ProtocoloEdit2.aspx?idServicio=" + ddlServicio.SelectedValue + "&Desde=Control&Operacion=Modifica&idProtocolo=" + primerProtocolo);
                    }
                    else
                    {
                        string popupScript = "<script language='JavaScript'> alert('No se encontraron datos para los filtros de busqueda ingresados')</script>";
                        Page.RegisterClientScriptBlock("PopupScript", popupScript);
                    }

                }
                else
                {
                    string popupScript = "<script language='JavaScript'> alert('No se encontraron datos para los filtros de busqueda ingresados')</script>";
                    Page.RegisterClientScriptBlock("PopupScript", popupScript);
                }
            }
        }

        protected void cvFechas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                DateTime fecha1 = DateTime.Parse(txtFechaDesde.Value);
                DateTime fecha2 = DateTime.Parse(txtFechaHasta.Value);
            }
            catch
            {
                args.IsValid = false;
                cvFechas.ErrorMessage = "Fechas inválidas";
            }
            if (txtFechaDesde.Value == "")
                args.IsValid = false;
            else
                if (txtFechaHasta.Value == "") args.IsValid = false;
                else args.IsValid = true;

        }

        protected void rdbPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void CargarTipoMuestra()
        {
            Utility oUtil = new Utility();
            if ((Request["Tipo"].ToString() == "ListadoOrdenado")
                || (Request["Tipo"].ToString() == "Exportacion"))
            {
              //  ddlServicio.Items.Insert(0, new ListItem("-- Todos --", "0"));
              //string  m_ssql = "SELECT idMuestra, nombre + ' - ' + codigo as nombre FROM LAB_Muestra   where baja=0  order by nombre ";
              //  oUtil.CargarCombo(ddlMuestra, m_ssql, "idMuestra", "nombre");
              //  ddlMuestra.Items.Insert(0, new ListItem("--Todas--", "0"));

               string m_ssql = "SELECT idArea, nombre FROM LAB_Area where baja=0 and idtipoServicio=" + ddlServicio.SelectedValue + " order by nombre ";
                oUtil.CargarCombo(ddlArea, m_ssql, "idArea", "nombre");
                ddlArea.Items.Insert(0, new ListItem("--Todas--", "0"));

            }


        }

        protected void ddlServicio_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlServicio.SelectedValue == "3")
            {
                btnSeleccionarTipoMuestra.Visible = true;
                
                
                    lstMuestra.Visible = true;

                    chkWhonet.Enabled = true;
                
            }
            else
            {
                chkWhonet.Enabled = false;
                btnSeleccionarTipoMuestra.Visible = false;
                lstMuestra.Visible = false;

            }
                //CargarTipoMuestra();
            chkWhonet.UpdateAfterCallBack = true;
            btnSeleccionarTipoMuestra.UpdateAfterCallBack = true;
            lstMuestra.UpdateAfterCallBack = true;

            CargarTipoMuestra();
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlArea.SelectedValue != "0") CargarPracticas();
        }

        private void CargarPracticas()
        {
            Utility oUtil = new Utility();
            if ((Request["Tipo"].ToString() == "ListadoOrdenado")||
                (Request["Tipo"].ToString() == "Exportacion"))
            {
                //  ddlServicio.Items.Insert(0, new ListItem("-- Todos --", "0"));
                //string  m_ssql = "SELECT idMuestra, nombre + ' - ' + codigo as nombre FROM LAB_Muestra   where baja=0  order by nombre ";
                //  oUtil.CargarCombo(ddlMuestra, m_ssql, "idMuestra", "nombre");
                //  ddlMuestra.Items.Insert(0, new ListItem("--Todas--", "0"));

                string m_ssql = @"select idItem, LAB_Item.nombre from LAB_Item 
where idArea="+ddlArea.SelectedValue+" and LAB_Item.baja=0 and tipo='P' and idEfectorDerivacion=LAB_Item.idEfector order by LAB_Item.nombre ";

                oUtil.CargarCombo(ddlItem, m_ssql, "idItem", "nombre");
                ddlItem.Items.Insert(0, new ListItem("--Todos--", "0"));
            }
        }

        protected void btnBuscarExportar_Click(object sender, EventArgs e)
        {
            Session["ListaProtocolo"] = listaProtocolos(LeerDatos(3));
            if (Session["ListaProtocolo"] != null)
            {
                if (Session["ListaProtocolo"].ToString() != "")
                {                    
                    Response.Redirect("ProtocoloExport.aspx?idServicio=" +ddlServicio.SelectedValue+"&Whonet="+ chkWhonet.Checked);
                }
                else
                {
                    string popupScript = "<script language='JavaScript'> alert('No se encontraron datos para los filtros de busqueda ingresados')</script>";
                    Page.RegisterClientScriptBlock("PopupScript", popupScript);
                }
            }
            else
            {
                string popupScript = "<script language='JavaScript'> alert('No se encontraron datos para los filtros de busqueda ingresados')</script>";
                Page.RegisterClientScriptBlock("PopupScript", popupScript);
            }
        }

        protected void btnSeleccionarTipoMuestra_Click(object sender, EventArgs e)
        {
            lstMuestra.Visible = true;
            lstMuestra.UpdateAfterCallBack = true;

        }

        protected void cvNumeros_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Utility oUtil = new Utility();
            if (txtProtocoloDesde.Value != "") { if (oUtil.EsEntero(txtProtocoloDesde.Value)) args.IsValid = true; else args.IsValid = false; }
            else
                args.IsValid = true;            
        }

        protected void cvNumeroTarjeta_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Utility oUtil = new Utility();
            if (txtNumeroTarjeta.Value != "") { if (oUtil.EsEntero(txtNumeroTarjeta.Value)) args.IsValid = true; else args.IsValid = false; }
            else
                args.IsValid = true;      
        }

        
        protected void cvNumeroHasta_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Utility oUtil = new Utility();
            if (txtProtocoloHasta.Value != "") { if (oUtil.EsEntero(txtProtocoloHasta.Value)) args.IsValid = true; else args.IsValid = false; }
            else
                args.IsValid = true;      
        }

        protected void lnkExcel_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
                dataTableAExcel(LeerDatos(6), "Protocolos");
        }
    

    private void dataTableAExcel(DataTable tabla, string nombreArchivo)
    {
        if (tabla.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            HtmlForm form = new HtmlForm();
            GridView dg = new GridView();
            dg.EnableViewState = false;
            dg.DataSource = tabla;
            dg.DataBind();
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(dg);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nombreArchivo + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
    }

}
}
