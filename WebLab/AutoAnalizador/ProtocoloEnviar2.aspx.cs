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
using System.Data.SqlClient;
using Business;
using Business.Data.Laboratorio;
using Business.Data.AutoAnalizador;
using System.Drawing;
using NHibernate;
using NHibernate.Expression;

namespace WebLab.AutoAnalizador
{
    public partial class ProtocoloEnviar2 : System.Web.UI.Page
    {
     //   int cantidadProtocolos = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                switch (Request["Equipo"].ToString())
                {
                    case "SysmexXS1000":
                        {//en produccion
                            VerificaPermisos("Sysmex XS1000i");
                            lblTituloEquipo.Text = "Sysmex XS1000i".ToUpper();
                        } break;
                    case "SysmexXT1800":
                        {
                            VerificaPermisos("Sysmex XT1800i");
                            lblTituloEquipo.Text = "Sysmex XT1800i".ToUpper();
                        } break;
                    case "Mindray":
                        {//en produccion
                            VerificaPermisos("Mindray BS-300");
                            lblTituloEquipo.Text = "Mindray BS-300".ToUpper();
                        } break;

                    case "Stago": 
                        {
                            VerificaPermisos("STA Compact");
                            lblTituloEquipo.Text = "STA Compact".ToUpper();
                        } break;
                    case "Metrolab":
                        {//en produccion
                            VerificaPermisos("Metrolab - Envío de datos");
                            lblTituloEquipo.Text = "METROLAB".ToUpper();
                        } break;
                    case "Miura":
                        {
                            VerificaPermisos("Miura");
                            lblTituloEquipo.Text = "MIURA QUIMICA".ToUpper();
                        } break;
                    case "CobasC311":
                        {
                            VerificaPermisos("CobasC311");
                            lblTituloEquipo.Text = ("Cobas C311 - Envío de datos");
                        } break;
                }
                CargarGrilla();          
                
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
                    case 1: btnEnviar.Visible = false; break;
                }
            }
            else Response.Redirect("../FinSesion.aspx", false);
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


        private void CargarGrilla()
        {
            gvLista.DataSource = LeerDatos("0");
            gvLista.DataBind();          
            lblCantidadProtocolos.Text =gvLista.Rows.Count.ToString() + " protocolos encontrados";          
            PintarReferencias();         
            MarcarSeleccionados(false);
        }            
      

        private void PintarReferencias()
        {
            foreach (GridViewRow row in gvLista.Rows)
            {       
                switch (row.Cells[8].Text)
                {

                    case "0": //sin enviar
                        {
                            System.Web.UI.WebControls.Image hlnk = new System.Web.UI.WebControls.Image();
                            hlnk.ImageUrl = "../App_Themes/default/images/amarillo.gif";
                            row.Cells[8].Controls.Add(hlnk);
                        }
                        break;
                    case "1": //enviado
                        {
                            System.Web.UI.WebControls.Image hlnk = new System.Web.UI.WebControls.Image();
                            hlnk.ImageUrl = "../App_Themes/default/images/verde.gif";
                            row.Cells[8].Controls.Add(hlnk);
                        }
                        break;
                    //case "2": //enviado
                    //    {
                    //        System.Web.UI.WebControls.Image hlnk = new System.Web.UI.WebControls.Image();
                    //        hlnk.ImageUrl = "../App_Themes/default/images/verde.gif";
                    //        row.Cells[6].Controls.Add(hlnk);
                    //    }
                    //    break;
                }

            }

        }
      
        private object LeerDatos(string s_idProtocolo)
        {
            string s_condicion= Request["Parametros"].ToString();
            if (s_idProtocolo != "0") s_condicion += " and P.idProtocolo=" + s_idProtocolo;
            string m_prefijo = "";
            if (Request["Prefijo"].ToString() != "Rutina")
                m_prefijo = Request["Prefijo"].ToString();

            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[LAB_GeneraProtocolosEnvio2]";


            cmd.Parameters.Add("@Equipo", SqlDbType.NVarChar);
            cmd.Parameters["@Equipo"].Value = Request["Equipo"].ToString();

            cmd.Parameters.Add("@Param", SqlDbType.NVarChar);
            cmd.Parameters["@Param"].Value = s_condicion;

            //cmd.Parameters.Add("@Cantidad", SqlDbType.NVarChar);
            //cmd.Parameters["@Cantidad"].Value = Request["Cantidad"].ToString();
          
            cmd.Parameters.Add("@TipoMuestra", SqlDbType.NVarChar);
            cmd.Parameters["@TipoMuestra"].Value = Request["tipoMuestra"].ToString();

            cmd.Parameters.Add("@Prefijo", SqlDbType.NVarChar);
            cmd.Parameters["@Prefijo"].Value =m_prefijo;

            cmd.Parameters.Add("@estado", SqlDbType.NVarChar);
            cmd.Parameters["@estado"].Value = Request["estado"].ToString();

            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Ds);
            return Ds.Tables[0];

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            EnviarDatos();
       
            //CargarGrilla();
        }

        private void EnviarDatos()
        {
            int IdMuestra=int.Parse( Request["IDMuestra"].ToString())-1;
            string m_Prefijo = Request["Prefijo"].ToString();
            string m_Equipo = Request["Equipo"].ToString();
             
           if (Request["LimpiaTemporal"].ToString()=="0") 
               LimpiarTablaTemporal();

            string pivot = "";
              
            string m_numeroMuestra = pivot;
            string m_numero = "";
              
            int cantidad = 1;
            foreach (GridViewRow row in gvLista.Rows)
            {            
                CheckBox a = ((CheckBox)(row.Cells[0].FindControl("CheckBox1")));
                if (a.Checked == true)
                {
                    string m_idProtocolo = gvLista.DataKeys[row.RowIndex].Value.ToString();
                    Protocolo oProtocolo = new Protocolo();
                    oProtocolo= (Protocolo)oProtocolo.Get(typeof(Protocolo),int.Parse( m_idProtocolo));
                        
                    m_numero = row.Cells[0].Text; 
                    GrabarDatosaEnviar(m_Equipo, oProtocolo, cantidad, m_numero);
                    cantidad += 1;                                                               
                }
            }
        
            Response.Redirect("EnvioMensaje.aspx?Cantidad=" + (cantidad-1).ToString() + "&Equipo=" + Request["Equipo"].ToString(), false);
        }

        private void GrabarDatosaEnviar(string m_Equipo,Protocolo oProtocolo,int IdMuestra, string numero)
        {
            string m_paciente = ""; string m_anioNacimiento = ""; string m_sexoPaciente = "";
            string m_listaItem = ""; string m_sectorSolicitante = ""; string m_Prefijo = "";
            string m_tipoMuestra = ""; bool marcarenviado = false;
            m_tipoMuestra = Request["tipoMuestra"].ToString();
      

            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(DetalleProtocolo));
            crit.Add(Expression.Eq("IdProtocolo", oProtocolo));
            //crit.Add(Expression.Eq("Enviado", 0));
            IList lista = crit.List();
           
            foreach (DetalleProtocolo oDetalle in lista)
            {
                    marcarenviado = false;
                    if (m_Equipo == "Mindray")
                    {
                        m_Prefijo = Request["Prefijo"].ToString(); 
                        MindrayItem oItemMindray = new MindrayItem();
                        oItemMindray = (MindrayItem)oItemMindray.Get(typeof(MindrayItem), "IdItem", oDetalle.IdSubItem.IdItem, "Habilitado", true);                      
                            
                        if (oItemMindray != null)
                        {
                            if (m_Prefijo.Trim() != "Rutina")
                            {
                                if (oItemMindray.Prefijo == m_Prefijo.Trim())
                                {
                                    marcarenviado = true;
                                    if (m_listaItem == "")
                                        m_listaItem = oItemMindray.IdMindray.ToString();
                                    else
                                        m_listaItem += "|" + oItemMindray.IdMindray.ToString();
                                }
                            }
                            else
                            {
                                if (oItemMindray.Prefijo == "")
                                {
                                    marcarenviado = true;
                                    if (m_listaItem == "")
                                        m_listaItem = oItemMindray.IdMindray.ToString();
                                    else
                                        m_listaItem += "|" + oItemMindray.IdMindray.ToString();
                                }
                            }
                        }                                          
                    }


                    if (m_Equipo == "SysmexXS1000")
                    {
                        SysmexItemXS1000 oItemSysmex = new SysmexItemXS1000();
                        oItemSysmex = (SysmexItemXS1000)oItemSysmex.Get(typeof(SysmexItemXS1000), "IdItem", oDetalle.IdSubItem.IdItem,"Habilitado",true);
                        if (oItemSysmex != null)
                        {
                            marcarenviado = true;
                            if (m_listaItem == "")
                                m_listaItem = oItemSysmex.IdSysmex.ToString();
                            else
                                m_listaItem += "|" + oItemSysmex.IdSysmex.ToString();
                            /////////////////////////////////////////////////////////
                            ///agrega los valores absolutos de la formula leucocitaria
                            switch (oItemSysmex.IdSysmex)
                            {
                                case "NEUT%": m_listaItem += "|NEUT#"; break;
                                case "LYMPH%": m_listaItem += "|LYMPH#"; break;
                                case "MONO%": m_listaItem += "|MONO#"; break;
                                case "EO%": m_listaItem += "|EO#"; break;
                                case "BASO%": m_listaItem += "|BASO#"; break;
                            }
                            /////////////////////////////////////////////////////////
                        }
                    }


                    if (m_Equipo == "SysmexXT1800")
                    {
                        SysmexItemxt1800 oItemSysmex = new SysmexItemxt1800();
                        oItemSysmex = (SysmexItemxt1800)oItemSysmex.Get(typeof(SysmexItemxt1800), "IdItem", oDetalle.IdSubItem.IdItem,"Habilitado",true);
                        if (oItemSysmex != null)
                        {
                            marcarenviado = true;
                            if (m_listaItem == "")
                                m_listaItem = oItemSysmex.IdSysmex;
                            else
                                m_listaItem += "|" + oItemSysmex.IdSysmex;                         
                        }
                    }

                    if (m_Equipo == "Metrolab")
                    {
                        m_Prefijo = Request["Prefijo"].ToString(); 
                        MetrolabItem oItemMetrolab = new MetrolabItem();
                        oItemMetrolab = (MetrolabItem)oItemMetrolab.Get(typeof(MetrolabItem), "IdItem", oDetalle.IdSubItem.IdItem,"Habilitado", true);
                        if (oItemMetrolab != null)
                        {
                            if (m_Prefijo.Trim() != "Rutina")
                            {
                                if (oItemMetrolab.Prefijo == m_Prefijo.Trim())
                                {
                                    marcarenviado = true;
                                    if (m_listaItem == "")
                                        m_listaItem = oItemMetrolab.IdMetrolab;
                                    else
                                        m_listaItem += ";" + oItemMetrolab.IdMetrolab;
                                }
                            }
                            else
                            {
                                if (oItemMetrolab.Prefijo == "")
                                {
                                    marcarenviado = true;
                                    if (m_listaItem == "")
                                        m_listaItem = oItemMetrolab.IdMetrolab;
                                    else
                                        m_listaItem += ";" + oItemMetrolab.IdMetrolab;
                                }
                            }
                        }
                    }
                    if (m_Equipo == "Miura")
                    {
                        MiuraItem oItemMiura = new MiuraItem();
                        oItemMiura = (MiuraItem)oItemMiura.Get(typeof(MiuraItem), "IdItem", oDetalle.IdSubItem.IdItem,"Habilitado",true);
                        if (oItemMiura != null)
                        {
                            if (m_Prefijo.Trim() != "Rutina")
                            {
                                if (oItemMiura.Prefijo == m_Prefijo.Trim())
                                {
                                    marcarenviado = true;
                                    if (m_listaItem == "")
                                        m_listaItem ="^" + oItemMiura.IdMiura + "^^"; 
                                    else
                                        m_listaItem += "|" + "^" + oItemMiura.IdMiura + "^^" ;
                                }
                            }
                            else
                            {
                                if (oItemMiura.Prefijo == "")
                                {
                                    marcarenviado = true;
                                    if (m_listaItem == "")
                                        m_listaItem = "^" + oItemMiura.IdMiura + "^^";
                                    else
                                        m_listaItem += "|" + "^" + oItemMiura.IdMiura + "^^";
                                }
                            }
                        }
                    }
                    if (marcarenviado)
                    {   ////////marca como enviado
                        oDetalle.Enviado = 1;
                        oDetalle.IdUsuarioEnvio = int.Parse(Session["idUsuario"].ToString());
                        oDetalle.FechaEnvio = DateTime.Now;
                        oDetalle.Save();
                        ///////////////
                    }
             }


            if (m_listaItem != "")
            {
                m_sexoPaciente = oProtocolo.Sexo; if (m_sexoPaciente == "I") m_sexoPaciente = "O";
                
                if (m_Equipo == "SysmexXT1800")
                {
                    if (m_sexoPaciente == "O") m_sexoPaciente = "U";
                    m_paciente = oProtocolo.IdPaciente.NumeroDocumento.ToString() + "-" + oProtocolo.IdPaciente.Apellido + "-" + oProtocolo.IdPaciente.Nombre;
                    m_anioNacimiento = oProtocolo.IdPaciente.FechaNacimiento.ToString("yyyyMMdd");
                    m_tipoMuestra = "Sangre";
                }

                if (m_Equipo == "SysmexXS1000")
                {
                    m_paciente = oProtocolo.IdPaciente.NumeroDocumento.ToString() + " - " + oProtocolo.IdPaciente.Apellido + "  " + oProtocolo.IdPaciente.Nombre;
                    m_anioNacimiento = oProtocolo.IdPaciente.FechaNacimiento.ToString("yyyyMMdd");
                    m_tipoMuestra = "Sangre";
                }
                
                if (m_Equipo == "Mindray")
                {                   
                    m_Prefijo = Request["Prefijo"].ToString(); 
                    if (m_Prefijo.Trim() != "Rutina") numero = numero + "-" + m_tipoMuestra.Substring(0, 1).ToUpper() + "-" + m_Prefijo.ToUpper();
                    else   numero = numero + "-" + m_tipoMuestra.Substring(0, 1).ToUpper();
                    m_paciente = oProtocolo.IdPaciente.Apellido + " " + oProtocolo.IdPaciente.Nombre;
                    m_anioNacimiento = oProtocolo.IdPaciente.FechaNacimiento.Year.ToString();
               
                }
                if (m_Equipo == "Metrolab")
                {
                    m_Prefijo = Request["Prefijo"].ToString();
                    if (m_Prefijo.Trim() != "Rutina") numero = numero + "-"+ m_Prefijo.ToUpper();                  

                    string numeroDocumento = "";
                    if (oProtocolo.IdPaciente.IdEstado==1)
                        numeroDocumento = oProtocolo.IdPaciente.NumeroDocumento.ToString();
                    m_paciente =numeroDocumento+";"+ oProtocolo.IdPaciente.Apellido + " " + oProtocolo.IdPaciente.Nombre;
                    if (oProtocolo.UnidadEdad == 0)

                        //string resultado = n.ToString.PadRight(9, '0')
                        m_anioNacimiento = oProtocolo.Edad.ToString();
                    else
                        m_anioNacimiento = "0";
                    m_sexoPaciente = oProtocolo.Sexo;
                }

                if (m_Equipo == "Miura")
                {
                    m_Prefijo = Request["Prefijo"].ToString();
                    if (m_Prefijo.Trim() != "Rutina") numero = numero + "-" + m_Prefijo.ToUpper();
                    if (oProtocolo.IdPaciente.IdEstado==1)
                        m_paciente = oProtocolo.IdPaciente.NumeroDocumento.ToString() + " - " + oProtocolo.IdPaciente.Apellido + "^" + oProtocolo.IdPaciente.Nombre;
                    else
                        m_paciente = "0 - " + oProtocolo.IdPaciente.Apellido + "^" + oProtocolo.IdPaciente.Nombre;

                    m_anioNacimiento = oProtocolo.IdPaciente.FechaNacimiento.ToString("yyyyMMdd");
                    m_sexoPaciente = oProtocolo.Sexo; if (m_sexoPaciente == "I") m_sexoPaciente = "U";
                }

              
                string m_urgente = "N";  if (oProtocolo.IdPrioridad.IdPrioridad == 2) m_urgente = "Y";
                m_sectorSolicitante = oProtocolo.IdSector.Nombre;


                //////INSERTAR LOS ANALISIS EN LA TABLA TEMPORAL LAB_TempProtocoloEnvio
                ProtocoloEnvio oRegistro = new ProtocoloEnvio();
                oRegistro.IdMuestra = IdMuestra;
                oRegistro.NumeroProtocolo = numero;
                oRegistro.Iditem = m_listaItem;
                oRegistro.Paciente = m_paciente;
                oRegistro.AnioNacimiento = m_anioNacimiento;
                oRegistro.Sexo = m_sexoPaciente;
                oRegistro.SectorSolicitante = m_sectorSolicitante;
                oRegistro.MedicoSolicitante = "";
                oRegistro.TipoMuestra = m_tipoMuestra;
                oRegistro.Urgente = m_urgente;
                oRegistro.Equipo = m_Equipo;
                oRegistro.Save();
                //////////////////////////////////////////// 
            }

        }

        private void LimpiarTablaTemporal()
        {
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(ProtocoloEnvio));            
            IList detalle = crit.List();
            if (detalle.Count > 0)
            {
                foreach (ProtocoloEnvio oDetalle in detalle)
                {                                                                         
                    if (oDetalle.Equipo==Request["Equipo"].ToString())
                    oDetalle.Delete();
                }

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla(); 
            
        }

        protected void lnkMarcar_Click(object sender, EventArgs e)
        {
            MarcarSeleccionados(true);
            PintarReferencias();
        }

        protected void lnkDesmarcar_Click(object sender, EventArgs e)
        {
            MarcarSeleccionados(false);
            PintarReferencias();
        }

        protected void ddlProtocolo_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            //gvLista.DataSource = LeerDatos(ddlProtocolo.SelectedValue);
            //gvLista.DataBind();                    

            //PintarReferencias();
            //lblCantidadProtocolos.Text = cantidadProtocolos.ToString() + " protocolos encontrados";
            //MarcarSeleccionados(true);
        }
        

        protected void lnkRegresar_Click(object sender, EventArgs e)
        {            
            Response.Redirect("ProtocoloBusqueda2.aspx?Equipo=" + Request["Equipo"].ToString(), false);
        }
    }
}
