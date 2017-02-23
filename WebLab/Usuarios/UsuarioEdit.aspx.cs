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
using Business.Data;
using Business.Data.Laboratorio;
using Business;

namespace WebLab.Usuarios
{
    public partial class UsuarioEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                VerificaPermisos("Usuarios");
                CargarListas();
                if (Request["id"] != null)
                    MostrarDatos();
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
        private void CargarListas()
        {
            Utility oUtil = new Utility();

            string m_ssql = @" SELECT idPerfil, nombre FROM Sys_Perfil WHERE (activo = 1) ORDER BY nombre ";

            oUtil.CargarCombo(ddlPerfil, m_ssql, "idPerfil", "nombre");
            ddlPerfil.Items.Insert(0, new ListItem("Seleccione un perfil", "0"));

        }

        private void MostrarDatos()
        {
            Usuario oRegistro = new Usuario();
            oRegistro = (Usuario)oRegistro.Get(typeof(Usuario), int.Parse(Request["id"]));
            txtApellido.Text = oRegistro.Apellido;
            txtNombre.Text = oRegistro.Nombre;
            txtFirmaValidacion.Text = oRegistro.FirmaValidacion;
            //txtMatricula.Text = oRegistro.Matricula;
            txtUsername.Text = oRegistro.Username;

           // txtPassword.Text = oRegistro.Password;
            chkActivo.Checked = oRegistro.Activo;
            ddlPerfil.SelectedValue = oRegistro.IdPerfil.IdPerfil.ToString();
        }




        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }


        private void Guardar(Usuario oRegistro)
        {            
            Configuracion oC = new Configuracion();
            oC = (Configuracion)oC.Get(typeof(Configuracion), "IdConfiguracion", 1);

            Perfil oPerfil = new Perfil();
            oPerfil = (Perfil)oC.Get(typeof(Perfil), int.Parse(ddlPerfil.SelectedValue));

            oRegistro.IdEfector = oC.IdEfector;
            oRegistro.IdPerfil =oPerfil;

            oRegistro.Apellido = txtApellido.Text;
            oRegistro.Nombre = txtNombre.Text;
            oRegistro.Legajo = "";
            oRegistro.FirmaValidacion = txtFirmaValidacion.Text;
             //oRegistro.Matricula=txtMatricula.Text;

            oRegistro.Username = txtUsername.Text;

            Utility oUtil = new Utility();
            string m_password = oUtil.Encrypt(txtPassword.Text);
            oRegistro.Password = m_password ;

            oRegistro.Activo = chkActivo.Checked;
            oRegistro.IdUsuarioActualizacion = int.Parse(Session["idUsuario"].ToString());
            oRegistro.FechaActualizacion = DateTime.Now;            

            oRegistro.Save();            
        }
      
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsuarioList.aspx");
        }

        protected void btnGuardar_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Usuario oRegistro = new Usuario();
                if (Request["id"] != null) oRegistro = (Usuario)oRegistro.Get(typeof(Usuario), int.Parse(Request["id"]));
                Guardar(oRegistro);

                if (Request["id"] != null)
                    Response.Redirect("UsuarioList.aspx", false);
                else
                    Response.Redirect("UsuarioEdit.aspx", false);
            }
        }
    }
}
