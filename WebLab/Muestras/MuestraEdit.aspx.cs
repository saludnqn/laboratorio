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
using Business;
using NHibernate;
using NHibernate.Expression;

namespace WebLab.Muestras
{
    public partial class MuestraEdit : System.Web.UI.Page
    {
        Utility oUtil = new Utility();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                VerificaPermisos("Tipo de Muestra");
                if (Request["id"] != null)
                    MostrarDatos();
            }
        }


        private void VerificaPermisos(string sObjeto)
        {
            if (Session["s_permiso"] != null)
            {
            //    Utility oUtil = new Utility();
                int i_permiso = oUtil.VerificaPermisos((ArrayList)Session["s_permiso"], sObjeto);
                switch (i_permiso)
                {
                    case 0: Response.Redirect("../AccesoDenegado.aspx", false); break;
                    case 1: btnGuardar.Visible = false; break;
                }
            }
            else Response.Redirect("../FinSesion.aspx", false);

        }
        private void MostrarDatos()
        {
            Muestra oRegistro = new Muestra();
            oRegistro = (Muestra)oRegistro.Get(typeof(Muestra), int.Parse(Request["id"].ToString()));
            txtCodigo.Text = oRegistro.Codigo;
            txtNombre.Text = oRegistro.Nombre;            
        }
       



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Muestra oRegistro = new Muestra();
                if (Request["id"] != null) oRegistro = (Muestra)oRegistro.Get(typeof(Muestra), int.Parse(Request["id"].ToString()));                
                Guardar(oRegistro);

                if (Request["id"] != null)
                    Response.Redirect("MuestraList.aspx", false);
                else
                    Response.Redirect("MuestraEdit.aspx", false);
            }
        }


        private void Guardar(Muestra oRegistro)
        {

           
            Usuario oUser = new Usuario();
            oUser = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));

            oRegistro.Codigo = txtCodigo.Text;

            oRegistro.Nombre = txtNombre.Text;
            oRegistro.IdEfector = oUser.IdEfector; // (Efector)oEfector.Get(typeof(Efector), "IdEfector", 5);            
            oRegistro.IdUsuarioRegistro = oUser;
            oRegistro.FechaRegistro = DateTime.Now;

            oRegistro.Save();
        }


        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MuestraList.aspx");
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Verifica que no exista un item con el codigo ingresado

            ISession m_session = NHibernateHttpModule.CurrentSession;

            ICriteria crit = m_session.CreateCriteria(typeof(Muestra));

            crit.Add(Expression.Eq("Codigo", txtCodigo.Text));
            crit.Add(Expression.Eq("Baja", false));


            IList lista = crit.List();
            if (lista.Count == 0)
                args.IsValid = true;
            else
            {

                foreach (Muestra oItem in lista)
                {
                    if (Request["id"] != null)
                    {
                        if (int.Parse(Request["id"].ToString()) == oItem.IdMuestra)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                }
            }
        }
    }
}
