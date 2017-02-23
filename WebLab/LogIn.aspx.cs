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
using Business;
using System.Data.SqlClient;

namespace WebLab
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["idUsuario"] != null)
                {
                    
                    if (Request["Operacion"] == null)
                    //////////nuevo login
                    {                       
                        lblSubtitulo.Text = "";                        
                    }
                    else
                    ///////////////validacion
                    {
                       
                        lblSubtitulo.Text = "Usted está ingresando a validación de resultados. Ingrese su usuario y contraseña de firma electrónica.";
                    }     
                }
                else
                    Response.Redirect("FinSesion.aspx", false);

            }
            
        }

       
    }
}
