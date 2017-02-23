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
using System.Data.SqlClient;
using Business;

namespace WebLab.Usuarios
{
    public partial class UsuarioList : System.Web.UI.Page
    {
        Utility oUtil = new Utility();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                VerificaPermisos("Usuarios");
                CargarGrilla();
            }
        }

        private int Permiso /*el permiso */
        {
            get { return ViewState["Permiso"] == null ? 0 : int.Parse(ViewState["Permiso"].ToString()); }
            set { ViewState["Permiso"] = value; }
        }

        private void VerificaPermisos(string sObjeto)
        {
            if (Session["s_permiso"] != null)
            {
                //   Utility oUtil = new Utility();
                Permiso = oUtil.VerificaPermisos((ArrayList)Session["s_permiso"], sObjeto);
                switch (Permiso)
                {
                    case 0: Response.Redirect("../AccesoDenegado.aspx", false); break;
                    case 1: btnAgregar.Visible = false; break;
                }
            }
            else Response.Redirect("../FinSesion.aspx", false);

        }

        private void CargarGrilla()
        {
            gvLista.AutoGenerateColumns = false;
            gvLista.DataSource = LeerDatos();
            gvLista.DataBind();
        }

        private object LeerDatos()
        {
            string m_strSQL = @"SELECT     U.idUsuario, U.apellido, U.nombre, U.username, P.nombre AS perfil
FROM         Sys_Usuario AS U INNER JOIN
                      Sys_Perfil AS P ON U.idPerfil = P.idPerfil order by username";
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);



            return Ds.Tables[0];
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsuarioEdit.aspx");
        }





        protected void gvLista_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
           

            if (e.CommandName == "Modificar")
            {
                string MyURL;

                MyURL = "UsuarioEdit.aspx?id=" + e.CommandArgument.ToString();
                Response.Redirect(MyURL);
            }
            // Response.Redirect("AreaEdit.aspx?id=" + e.CommandArgument);
          
        }


        //private void Eliminar(object p)
        //{
        //    Perfil oRegistro = new Perfil();
        //    oRegistro = (Perfil)oRegistro.Get(typeof(Perfil), int.Parse(p.ToString()));
        //    Usuario oUser = new Usuario();
        //    oRegistro.Baja = true;
        //    oRegistro.IdUsuarioRegistro = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
        //    oRegistro.FechaRegistro = DateTime.Now;
        //    oRegistro.Save();
        //}

        protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
             
                ImageButton CmdModificar = (ImageButton)e.Row.Cells[4].Controls[1];
                CmdModificar.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdModificar.CommandName = "Modificar";

                
                if (Permiso == 1)
                {                
                    CmdModificar.ToolTip = "Consultar";
                }
            }
        }
    }
}
