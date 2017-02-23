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
using Business.Data.AutoAnalizador;
using System.IO;
using NHibernate;
using Business;
using NHibernate.Expression;

namespace WebLab.AutoAnalizador
{
    public partial class EnvioMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.lblMensaje.Text = "Se han enviado " + Request["Cantidad"].ToString() + " muestras al equipo " + Request["Equipo"].ToString();
                if (Request["Equipo"].ToString() == "Metrolab")
                {
                    this.lblMensaje.Text = "Se han procesado " + Request["Cantidad"].ToString() + " protocolos.";
                    btnDescargarArchivo.Visible = true;
                }   
            }

        }

        private void GenerarArchivoFormatoMetrolabExportar()
        {
            string directorio = Server.MapPath(""); // @"C:\Archivos de Usuario\";

            if (Directory.Exists(directorio))
            {
                string archivo = directorio + "\\Muestras.ana"; /// probablemente tiene que ser extension .ana (renombrar)

                using (StreamWriter sw = new StreamWriter(archivo))
                {
                    ISession m_session = NHibernateHttpModule.CurrentSession;
                    ICriteria crit = m_session.CreateCriteria(typeof(ProtocoloEnvio));
                    crit.Add(Expression.Eq("Equipo", "Metrolab"));
                    IList detalle = crit.List();
                    if (detalle.Count > 0)
                    {
                        string linea = "";
                        foreach (ProtocoloEnvio oDetalle in detalle)
                        {                           
                            string[] datosPaciente = oDetalle.Paciente.Split((";").ToCharArray());

                            string dni = datosPaciente[0].ToString();
                            string nombre = datosPaciente[1].ToString().PadRight(30,' ');
                            if (nombre.Length > 30)   nombre = nombre.Substring(0, 30);
                            int cantidadPruebas= oDetalle.Iditem.Split((";").ToCharArray()).Length;                          
                            
                            //linea formato metrolab= numeroprotocolo (hasta 10c);N(1c);nombrePaciente(hasta 30c);documento(hasta 12c);edad(3c);sexo(M,F o blanco);cantidadPruebas (1 o 2c);pruebas
                            linea = oDetalle.NumeroProtocolo + ";N;" + nombre + ";" + dni + ";" + oDetalle.AnioNacimiento.PadLeft(3, '0') +";" + oDetalle.Sexo + ";" + cantidadPruebas.ToString() + ";" + oDetalle.Iditem;
                            sw.Write(linea);
                            sw.Write( "\r\n");//retorno de carro y avance de linea
                        }
                    }
                }              
                DescargarArchivo(archivo);


              
            }
        }

        private void DescargarArchivo(string archivo)
        {
         
            System.IO.FileInfo toDownload = new System.IO.FileInfo(archivo);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
            HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(archivo);
            HttpContext.Current.Response.End();
            ////////////////////////////////////////////////////////////////////////////
        }

        protected void lnkRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProtocoloBusqueda2.aspx?Equipo=" + Request["Equipo"].ToString(), false);
        }

        protected void btnDescargarArchivo_Click(object sender, EventArgs e)
        {
            GenerarArchivoFormatoMetrolabExportar();
        }
    }
}
