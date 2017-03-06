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
using Business.Data;
using System.Data.SqlClient;
using Business;
using System.Security.Principal;


namespace WebLab
{
    public partial class Default : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ConfigurationManager.AppSettings["tipoAutenticacion"].ToString() == "SSO")  IdentificarSSO();
                else IdentificarSGH();                
            }          
        }

        private void IdentificarSGH()
        {
            int idUsuarioLogueado = 2; // Request["idUsuario"].ToString();
            
            if (Request["idUsuario"] != null) idUsuarioLogueado =int.Parse( Request["idUsuario"].ToString());
            CrearLogAcceso(idUsuarioLogueado);
            CrearPermisos(idUsuarioLogueado);
            Session["idUsuario"] = idUsuarioLogueado;
            Response.Redirect("Principal.aspx", false);
            

            //Response.Redirect("Estadisticas/Pesquisa/Reporte.aspx", false);                      
         //   Response.Redirect("PeticionElectronica/PeticionEdit.aspx?idPaciente=1379450", false);
        }

        private void IdentificarSSO()
        {
            ///////Simula Log In//////
            //string sessionId = "admin";
            //HttpContext.Current.User = new GenericPrincipal(new Salud.Security.SSO.SSOIdentity(new HttpCookie("SSO_AUTH_COOKIE", sessionId)), null);
            ////////////////////////////            
            int idUsuarioLogueado = IdentificarUsuarioSSO();
            if (idUsuarioLogueado == 0)
                Salud.Security.SSO.SSOHelper.RedirectToSSOPage("Login.aspx", Request.Url.ToString());
            else
            {
                Usuario oUser = new Usuario();
                oUser = (Usuario)oUser.Get(typeof(Usuario), "Username", Salud.Security.SSO.SSOHelper.CurrentIdentity.Username);
                if (oUser != null)
                {
                    idUsuarioLogueado = oUser.IdUsuario;

                string s_perfil = oUser.IdPerfil.Nombre;

                //object perfil = Salud.Security.SSO.SSOHelper.CurrentIdentity.GetSetting(s_perfil);
                //if (perfil == null) Salud.Security.SSO.SSOHelper.RedirectToErrorPage(403,0,"El usuario no tiene permisos de acceso al sistema de laboratorio. Comuniquese con el Administrador del Sistema.");
                //else
                //{    
                CrearLogAcceso(idUsuarioLogueado);
                CrearPermisos(idUsuarioLogueado);
                Session["idUsuario"] = idUsuarioLogueado;
                Response.Redirect("Principal.aspx", false);
                }
                else
                    Response.Redirect("AccesoDenegado.htm");
            }
                
        }

        private int IdentificarUsuarioSSO()
        {
            Salud.Security.SSO.SSOHelper.Authenticate();
            if (Salud.Security.SSO.SSOHelper.CurrentIdentity == null)
            {
                // 1.1. No lo está. Debe redirigir al sitio de SSO
                // Redirigir ...
                return 0;
                //pnlSinUsuario.Visible = true;
            }
            else
            {
                return Salud.Security.SSO.SSOHelper.CurrentIdentity.Id;

            }
        }
        //private int IdentificarUsuario()
        //{
        //    if (Salud.Security.SSO.SSOHelper.CurrentIdentity == null)
        //    {
        //        // 1.1. No lo está. Debe redirigir al sitio de SSO
        //        // Redirigir ...
        //        Salud.Security.SSO.SSOHelper.RedirectToSSOPage("Login.aspx", Request.Url.ToString());
        //        //pnlSinUsuario.Visible = true;
        //    }
        //    else
        //    {
        //        // 1.2 El usuario está loggeado. Obtiene todos los datos
        //        pnlUsuario.Visible = true;
        //        lblID.Text = Salud.Security.SSO.SSOHelper.CurrentIdentity.Id.ToString();
        //        lblNombre.Text = Salud.Security.SSO.SSOHelper.CurrentIdentity.Fullname;

        //        // Verifica el perfil
        //        object perfil = Salud.Security.SSO.SSOHelper.CurrentIdentity.GetSetting("Perfil_Laboratorio");
        //        lblPerfil.Text = (perfil == null) ? "Sin perfil asignado" : perfil.ToString();

        //        // Para simular un cambio de perfil, quitar el comentario de la siguiente línea
        //        //Salud.Security.SSO.SSOHelper.CurrentIdentity.SetSetting("Perfil_Laboratorio", "Bioquímico");
        //    }
        //}

        private void CrearLogAcceso(int idUsuarioLogueado)
        {
            LogAcceso RegistroAcceso = new LogAcceso();
            RegistroAcceso.IdUsuario = idUsuarioLogueado;
            RegistroAcceso.Fecha = DateTime.Now;
            RegistroAcceso.Save();
        }

        private void CrearPermisos(int p)
        {
            string m_strSQL = " SELECT  M.objeto, P.permiso " +
                              " FROM Sys_Perfil " +
                              " INNER JOIN   Sys_Usuario AS U ON Sys_Perfil.idPerfil = U.idPerfil " +
                              " INNER JOIN   Sys_Permiso AS P ON Sys_Perfil.idPerfil = P.idPerfil " +
                              " INNER JOIN   Sys_Menu AS M ON P.idMenu = M.idMenu " +
                              " WHERE (M.habilitado = 1)  AND (M.idModulo = 2) and  (U.activo=1 )  AND (U.idUsuario =" + p.ToString() + ")";

            using (SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection)
            {
                DataSet Ds = new DataSet();
                //SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
                adapter.Fill(Ds);

                DataTable dtPermisos = Ds.Tables[0];
                ArrayList l_permisos;
                l_permisos = new ArrayList();
                foreach (DataRow dr in dtPermisos.Rows)
                {
                    l_permisos.Add(dr.ItemArray[0].ToString() + ":" + dr.ItemArray[1].ToString());
                    Session["s_permiso"] = l_permisos;       
                }
                conn.Close();
            }
            
        }

      
    }
}
