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
using NHibernate;
using Business;

namespace WebLab
{
    public partial class ImpresoraEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                foreach (string MiImpresora in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    ddlImpresora.Items.Add(MiImpresora);                
                }
                MostrarImpresoras();
            }
        }

        private void MostrarImpresoras()
        {  
            ISession m_session = NHibernateHttpModule.CurrentSession;

            Impresora oRegistro = new Impresora();
            ICriteria crit2 = m_session.CreateCriteria(typeof(Impresora));
          
            IList listaImpresoras = crit2.List();

            foreach (Impresora oImpr in listaImpresoras)
            {
                ListItem oItem = new ListItem();
                oItem.Text = oImpr.Nombre;
                oItem.Value = oImpr.Nombre;
                lstImpresora.Items.Add(oItem);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!VerificarExisteImpresoras())
            {
                ListItem oImpresora = new ListItem();
                oImpresora.Text = ddlImpresora.SelectedValue;
                oImpresora.Value = ddlImpresora.SelectedValue;
                lstImpresora.Items.Add(oImpresora);
            }
        }

        private bool VerificarExisteImpresoras()
        {
            bool existe=false;
            if (lstImpresora.Items.Count > 0)
            {
                /////Crea nuevamente los detalles.
                for (int i = 0; i < lstImpresora.Items.Count; i++)
                {
                    if (ddlImpresora.SelectedValue == lstImpresora.Items[i].Value)
                    {
                        existe = true; break;
                    }
                }
            }
            return existe;
        }

        protected void btnGuardarImpresora_Click(object sender, EventArgs e)
        {
             if (lstImpresora.Items.Count > 0)
            {
                 ////borra las impresoras guardadas
                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit2 = m_session.CreateCriteria(typeof(Impresora));
                IList listaImpresoras = crit2.List();
                foreach (Impresora oImpr in listaImpresoras)
                {
                    oImpr.Delete();
                }

                /////Crea nuevamente los detalles.
                for (int i = 0; i < lstImpresora.Items.Count; i++)
                {
                     Impresora oRegistro = new Impresora();
                     oRegistro.Nombre = lstImpresora.Items[i].Value;
                     oRegistro.IdUsuarioRegistro = int.Parse(Session["idUsuario"].ToString());
                     oRegistro.FechaRegistro = DateTime.Now;
                     oRegistro.Save();
                }
            }
            
        }
    }
}
