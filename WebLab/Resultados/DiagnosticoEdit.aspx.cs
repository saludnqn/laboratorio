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
using Business.Data;

namespace WebLab.Resultados
{
    public partial class DiagnosticoEdit : System.Web.UI.Page
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
                //    CargarListas(oRegistro);
                    MuestraDatos(oRegistro);

                
                
            }
        }

        private void MuestraDatos(Business.Data.Laboratorio.Protocolo oRegistro)
        {
         
            ///Agregar a la tabla las determinaciones para mostrarlas en el gridview                             
            //dtDeterminaciones = (System.Data.DataTable)(Session["Tabla1"]);
            DetalleProtocolo oDetalle = new DetalleProtocolo();
            ISession m_session = NHibernateHttpModule.CurrentSession;

            ///Agregar a la tabla las diagnosticos para mostrarlas en el gridview                             
            //   dtDiagnosticos = (System.Data.DataTable)(Session["Tabla2"]);
            ProtocoloDiagnostico oDiagnostico = new ProtocoloDiagnostico();
            ICriteria crit2 = m_session.CreateCriteria(typeof(ProtocoloDiagnostico));
            crit2.Add(Expression.Eq("IdProtocolo", oRegistro));

            IList diagnosticos = crit2.List();

            foreach (ProtocoloDiagnostico oDiag in diagnosticos)
            {
                Cie10 oCie10 = new Cie10();
                oCie10 = (Cie10)oCie10.Get(typeof(Cie10), oDiag.IdDiagnostico);

                ListItem oDia = new ListItem();
                oDia.Text = oCie10.Codigo + " - " + oCie10.Nombre;
                oDia.Value = oCie10.Id.ToString();
                lstDiagnosticosFinal.Items.Add(oDia);
            }

        }
        protected void btnBusquedaDiagnostico_Click(object sender, EventArgs e)
        {

            lstDiagnosticos.Items.Clear();
            if (txtCodigoDiagnostico.Text != "")
            {
                Cie10 oDiagnostico = new Cie10();
                oDiagnostico = (Cie10)oDiagnostico.Get(typeof(Cie10), "Codigo", txtCodigoDiagnostico.Text);
                if (oDiagnostico != null)
                {
                    ListItem oDia = new ListItem();
                    oDia.Text = oDiagnostico.Codigo + " - " + oDiagnostico.Nombre;
                    oDia.Value = oDiagnostico.Id.ToString();
                    lstDiagnosticos.Items.Add(oDia);

                }
                else
                    lstDiagnosticos.Items.Clear();
            }

            if (txtNombreDiagnostico.Text != "")
            {
                lstDiagnosticos.Items.Clear();
                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit = m_session.CreateCriteria(typeof(Cie10));
                crit.Add(Expression.Sql(" Nombre like '%" + txtNombreDiagnostico.Text + "%' order by Nombre"));

                IList items = crit.List();

                foreach (Cie10 oDiagnostico in items)
                {
                    ListItem oDia = new ListItem();
                    oDia.Text = oDiagnostico.Codigo + " - " + oDiagnostico.Nombre;
                    oDia.Value = oDiagnostico.Id.ToString();
                    lstDiagnosticos.Items.Add(oDia);
                }


            }
            lstDiagnosticos.UpdateAfterCallBack = true;




        }

        private void CargarDiagnosticosFrecuentes()
        {
            Utility oUtil = new Utility();


            //  btnGuardarImprimir.Visible = oC.GeneraComprobanteProtocolo;
            lstDiagnosticos.Items.Clear();

            ///Carga de combos de tipos de servicios
            string m_ssql = "SELECT ID, Codigo + ' - ' + Nombre as nombre FROM Sys_CIE10 WHERE (ID IN (SELECT DISTINCT idDiagnostico FROM LAB_ProtocoloDiagnostico)) ORDER BY Nombre";
            oUtil.CargarListBox(lstDiagnosticos, m_ssql, "id", "nombre");
            lstDiagnosticos.UpdateAfterCallBack = true;


        }

        protected void btnBusquedaFrecuente_Click(object sender, EventArgs e)
        {
            CargarDiagnosticosFrecuentes();
        }







       
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
                          
        }

        private void GuardarDiagnosticos(Business.Data.Laboratorio.Protocolo oRegistro)
        {
            if (IsTokenValid())
            {
                TEST++;

                //   dtDiagnosticos = (System.Data.DataTable)(Session["Tabla2"]);
                ///Eliminar los detalles y volverlos a crear
                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit = m_session.CreateCriteria(typeof(ProtocoloDiagnostico));
                crit.Add(Expression.Eq("IdProtocolo", oRegistro));
                IList detalle = crit.List();
                if (detalle.Count > 0)
                {
                    foreach (ProtocoloDiagnostico oDetalle in detalle)
                    {
                        Cie10 oCie10 = new Cie10(oDetalle.IdDiagnostico);
                        string s_diag_1 = oCie10.Nombre;
                        oDetalle.Delete();
                        oRegistro.GrabarAuditoriaDetalleProtocolo("Elimina", int.Parse(Session["idUsuario"].ToString()), "Diagnóstico", s_diag_1);
                    }
                }


                ///Busca en la lista de diagnosticos buscados
                if (lstDiagnosticosFinal.Items.Count > 0)
                {
                    /////Crea nuevamente los detalles.
                    for (int i = 0; i < lstDiagnosticosFinal.Items.Count; i++)
                    {
                        ProtocoloDiagnostico oDetalle = new ProtocoloDiagnostico();
                        oDetalle.IdProtocolo = oRegistro;
                        oDetalle.IdEfector = oRegistro.IdEfector;
                        oDetalle.IdDiagnostico = int.Parse(lstDiagnosticosFinal.Items[i].Value);
                        string s_diag = lstDiagnosticosFinal.Items[i].Text;
                        oDetalle.Save();
                        oRegistro.GrabarAuditoriaDetalleProtocolo("Graba", int.Parse(Session["idUsuario"].ToString()), "Diagnóstico", s_diag);
                    }
                }

            }

        }


        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            { ///Verifica si se trata de un alta o modificacion de protocolo               
                Business.Data.Laboratorio.Protocolo oRegistro = new Business.Data.Laboratorio.Protocolo();
                oRegistro = (Business.Data.Laboratorio.Protocolo)oRegistro.Get(typeof(Business.Data.Laboratorio.Protocolo), int.Parse(Request["idProtocolo"].ToString()));

                GuardarDiagnosticos(oRegistro);
            }
        }



        protected void btnAgregarDiagnostico_Click(object sender, EventArgs e)
        {


            AgregarDiagnostico();
        }
        protected void btnAgregarDiagnostico_Click1(object sender, ImageClickEventArgs e)
        {
            AgregarDiagnostico();
        }

        protected void btnSacarDiagnostico_Click(object sender, ImageClickEventArgs e)
        {
            SacarDiagnostico();
        }

        private void AgregarDiagnostico()
        {
            if (lstDiagnosticos.SelectedValue != "")
            {
                lstDiagnosticosFinal.Items.Add(lstDiagnosticos.SelectedItem);
                lstDiagnosticosFinal.UpdateAfterCallBack = true;
            }
        }


        private void SacarDiagnostico()
        {
            if (lstDiagnosticosFinal.SelectedValue != "")
            {
                lstDiagnosticosFinal.Items.Remove(lstDiagnosticosFinal.SelectedItem);
                lstDiagnosticosFinal.UpdateAfterCallBack = true;
            }
        }




        protected void txtCodigo_TextChanged1(object sender, EventArgs e)
        {

        }

      


    }
}

