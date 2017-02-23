﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data.SqlClient;
using System.Data;
using Business.Data.Laboratorio;
using Business.Data;
using NHibernate;
using System.Collections;
using NHibernate.Expression;
using System.Drawing;

namespace WebLab.Resultados
{
    public partial class ATBEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


            if (!Page.IsPostBack)
            {
                string s_iditem = Request["idItem"].ToString();
                string s_idProtocolo = Request["idProtocolo"].ToString();
                string s_idGermen = Request["idGermen"].ToString();
                string s_idMetodo= Request["idMetodo"].ToString();
                string s_numeroAislamiento = Request["numeroAislamiento"].ToString();
                switch (int.Parse(s_idMetodo))
                {
                    case 0: lblMetodo.Text = "Disco"; break;
                    case 1: lblMetodo.Text = "CIM"; break;
                    case 2: lblMetodo.Text = "eTest"; break;
                }
                Protocolo oProtocolo = new Protocolo();
                oProtocolo = (Protocolo)oProtocolo.Get(typeof(Protocolo), int.Parse(s_idProtocolo));
                lblProtocolo.Text = oProtocolo.GetNumero();

                Germen oGermen = new Germen();
                oGermen = (Germen)oGermen.Get(typeof(Germen), int.Parse(s_idGermen));

                Item oItem = new Item();
                oItem = (Item)oItem.Get(typeof(Item), int.Parse(s_iditem));

                lblGermen.Text = s_numeroAislamiento + " " + oGermen.Nombre;
                lblPractica.Text = oItem.Nombre;

               

                CargarPerfilAntibiotico();
                CargarListaAntibiotico();
                ActualizarVistaAntibiograma();

            }
          
        }
        private void CargarPerfilAntibiotico()
        {
            Utility oUtil = new Utility();
            ///Carga los perfiles de  Antibioticos
            string m_ssql = @" SELECT DISTINCT PA.idPerfilAntibiotico, PA.nombre
FROM         LAB_PerfilAntibiotico AS PA INNER JOIN
                      LAB_DetallePerfilAntibiotico AS DPA ON PA.idPerfilAntibiotico = DPA.idPerfilAntibiotico INNER JOIN
                      LAB_Antibiotico AS A ON DPA.idAntibiotico = A.idAntibiotico
WHERE     (PA.baja = 0)
ORDER BY PA.nombre";
            oUtil.CargarCombo(ddlPerfilAntibiotico, m_ssql, "idPerfilAntibiotico", "nombre");
            //ddlPerfilAntibiotico.Items.Insert(0, new ListItem("--SELECCIONE PERFIL ANTIBIOTICOS--", "0"));
            ddlPerfilAntibiotico.Items.Insert(0, new ListItem("--TODOS LOS ANTIBIOTICOS--", "0"));
            //////////////////////////////                              
        }
        private void ActualizarVistaAntibiograma()
        {
          //  CargarListaAntibiogramas();

            string s_iditem = Request["idItem"].ToString();
            string s_idProtocolo = Request["idProtocolo"].ToString();
            string s_idGermen = Request["idGermen"].ToString();
            string s_idMetodo = Request["idMetodo"].ToString();
            string s_numeroAislamiento = Request["numeroAislamiento"].ToString();
            ///////////////////////////////////////////////////////////////////////////////////////////
            //int cantidadAntibiogramas=ddlAntibiograma.Items.Count - 1;
            //lblCantidadAntibiograma.Text = " *" + cantidadAntibiogramas.ToString();

            string m_ssql = @" SELECT  ATB.idAntibiograma, A.descripcion, ATB.resultado, ATB.valor, ATB.idMetodologia
FROM         LAB_Antibiograma AS ATB INNER JOIN
                      LAB_Antibiotico AS A ON ATB.idAntibiotico = A.idAntibiotico
WHERE ATB.numeroAislamiento="+ s_numeroAislamiento +" and  ATB.idMetodologia=" + s_idMetodo + " AND  (ATB.idProtocolo = " + s_idProtocolo + ") AND (ATB.idItem = " + s_iditem + ") AND (ATB.idGermen = " + s_idGermen + ") order by A.descripcion";
            

            DataSet Ds1 = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_ssql, conn);
            adapter.Fill(Ds1);

            gvAntiobiograma.DataSource = Ds1.Tables[0];
            gvAntiobiograma.DataBind();

     




        }
        protected void ddlPerfilAntibiotico_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListaAntibiotico();
           
        }


        private void CargarListaAntibiotico()
        {
            Utility oUtil = new Utility();
            ///Carga los antibioticos para la solapa Antibiograma

            string m_ssql = "";
            if (ddlPerfilAntibiotico.SelectedValue == "0") ///Todos los antibioticos            
                m_ssql = " SELECT idAntibiotico , descripcion FROM LAB_Antibiotico where baja=0 order by descripcion ";
            
            else            
                m_ssql = @" SELECT DISTINCT A.idAntibiotico, A.descripcion
FROM         LAB_PerfilAntibiotico AS PA INNER JOIN
                      LAB_DetallePerfilAntibiotico AS DPA ON PA.idPerfilAntibiotico = DPA.idPerfilAntibiotico INNER JOIN
                      LAB_Antibiotico AS A ON DPA.idAntibiotico = A.idAntibiotico
WHERE     (PA.idPerfilAntibiotico = " + ddlPerfilAntibiotico.SelectedValue + ")  ORDER BY A.descripcion";
            

            oUtil.CargarCombo(ddlAntibiotico, m_ssql, "idAntibiotico", "descripcion");
            ddlAntibiotico.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }
        protected void btnGuardarAntibiograma_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
               
                    GuardarAntibiograma();
                    ddlPerfilAntibiotico.SelectedValue = "0";
                    ddlAntibiotico.SelectedValue = "0";
                    ddlResultado.SelectedValue = "0";
                    txtValor.Text = "";
                    ActualizarVistaAntibiograma();
               
            }
        }

       

        private void GuardarAntibiograma()
        {
            string s_iditem = Request["idItem"].ToString();
            string s_idProtocolo = Request["idProtocolo"].ToString();
            string s_idGermen = Request["idGermen"].ToString();
            string s_idMetodo = Request["idMetodo"].ToString();
            string s_numeroAislamiento = Request["numeroAislamiento"].ToString();

            Protocolo oProtocolo = new Protocolo();
            oProtocolo = (Protocolo)oProtocolo.Get(typeof(Protocolo), int.Parse(s_idProtocolo));

            Germen oGermen = new Germen();
            oGermen = (Germen)oGermen.Get(typeof(Germen), int.Parse(s_idGermen));

            Usuario oUser = new Usuario();
            if  ( (Request["Operacion"].ToString() == "Valida") &&(Session["idUsuarioValida"] != null))
                oUser = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuarioValida"].ToString()));
            else
                oUser = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));

         

            Antibiograma oRegistro = new Antibiograma();
            oRegistro.IdProtocolo = oProtocolo;
            oRegistro.IdEfector = oProtocolo.IdEfector;
            oRegistro.IdGermen = oGermen;
            oRegistro.NumeroAislamiento = int.Parse( s_numeroAislamiento);
            oRegistro.IdItem = int.Parse(s_iditem);

            Antibiotico oAntibiotico = new Antibiotico();
            oAntibiotico = (Antibiotico)oAntibiotico.Get(typeof(Antibiotico), int.Parse(ddlAntibiotico.SelectedValue));
            oRegistro.IdAntibiotico = oAntibiotico;


            oRegistro.IdMetodologia = int.Parse(s_idMetodo);
            oRegistro.Resultado = ddlResultado.SelectedValue;                     
            
            oRegistro.Valor = txtValor.Text;

            oRegistro.IdUsuarioRegistro = oUser.IdUsuario;
            oRegistro.FechaRegistro = DateTime.Now;
            oRegistro.FechaValida = DateTime.Parse("01/01/1900");
            if (Request["Operacion"].ToString() == "Valida")
            {
                oRegistro.IdUsuarioValida = oUser.IdUsuario;
                oRegistro.FechaValida = DateTime.Now;
            }
            oRegistro.Save();                                

            oProtocolo.GrabarAuditoriaDetalleProtocolo("Alta", oUser.IdUsuario, "ATB " + oRegistro.NumeroAislamiento.ToString()+ " " +  oRegistro.IdGermen.Nombre +  " ("+lblMetodo.Text +") - " + oRegistro.IdAntibiotico.Descripcion, oRegistro.Resultado + " " + oRegistro.Valor);


               
        }

        protected void ddlPerfilAntibiotico_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CargarListaAntibiotico();
            ddlAntibiotico.UpdateAfterCallBack = true;
        }

        protected void gvAntiobiograma_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName=="Eliminar")
            {EliminarAntibiotico(e.CommandArgument.ToString());
                ActualizarVistaAntibiograma();
            }
            
        }

        private void EliminarAntibiotico(string anti)
        {
            Antibiograma oRegistro = new Antibiograma();
            oRegistro = (Antibiograma)oRegistro.Get(typeof(Antibiograma), int.Parse(anti));


            if (oRegistro != null)
            {
                oRegistro.Delete();
                oRegistro.IdProtocolo.GrabarAuditoriaDetalleProtocolo("Elimina", int.Parse(Session["idUsuario"].ToString()), "ATB: " + oRegistro.IdGermen.Nombre + "(" + lblMetodo.Text + ") - " + oRegistro.IdAntibiotico.Descripcion, oRegistro.Resultado + " " + oRegistro.Valor);
            }
           
            
        }

       
      

        protected void gvAntiobiograma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton CmdModificar = (ImageButton)e.Row.Cells[4].Controls[1];
                CmdModificar.CommandArgument = this.gvAntiobiograma.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdModificar.CommandName = "Eliminar";
                CmdModificar.ToolTip = "Eliminar";

                // si esta validado no se puede eliminar
                Antibiograma oRegistro = new Antibiograma();
                oRegistro = (Antibiograma)oRegistro.Get(typeof(Antibiograma), int.Parse(this.gvAntiobiograma.DataKeys[e.Row.RowIndex].Value.ToString()));
                if ((oRegistro.IdUsuarioValida > 0) && (Request["Operacion"] == "Carga"))
                    CmdModificar.Visible = false;
                /////
                Label lbl = (e.Row.Cells[3].FindControl("lblEstado") as Label);
                if (oRegistro.IdUsuarioRegistro == 0) //enviado por el equipo
                {
                    //lblPersona.Text = "AUTOMÁTICO: " + oDetalle.FechaResultado.ToShortDateString() + " - " + oDetalle.FechaResultado.ToShortTimeString();
                    lbl.Text = "AUTOMATICO " + oRegistro.FechaRegistro.ToShortDateString() + " - " + oRegistro.FechaRegistro.ToShortTimeString();
                    lbl.ForeColor = Color.Red;
                }
                else
                {
                    Usuario oUser = new Usuario();

                    oUser = (Usuario)oUser.Get(typeof(Usuario), oRegistro.IdUsuarioRegistro);
                    lbl.Text = "Carg.: " + oUser.Apellido + " " + oUser.Nombre;
                    lbl.ForeColor = Color.Black;

                }
                if (oRegistro.IdUsuarioValida > 0) //enviado por el equipo
                {
                    Usuario oUser = new Usuario();

                    oUser = (Usuario)oUser.Get(typeof(Usuario), oRegistro.IdUsuarioValida);
                    lbl.Text = "Val.: " + oUser.Apellido + " " + oUser.Nombre;
                    lbl.ForeColor = Color.Blue;
                }
                lbl.Font.Size = FontUnit.Point(7);
                lbl.Font.Italic = true;

                  
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
                      //////////////////
          string s_iditem = Request["idItem"].ToString();
            string s_idProtocolo = Request["idProtocolo"].ToString();
            string s_idGermen = Request["idGermen"].ToString();

            string s_idMetodo = Request["idMetodo"].ToString();

            Protocolo oProtocolo = new Protocolo();
            oProtocolo = (Protocolo)oProtocolo.Get(typeof(Protocolo), int.Parse(s_idProtocolo));

            Germen oGermen = new Germen();
            oGermen = (Germen)oGermen.Get(typeof(Germen), int.Parse(s_idGermen));


            Antibiotico oAntibiotico = new Antibiotico();
            oAntibiotico = (Antibiotico)oAntibiotico.Get(typeof(Antibiotico), int.Parse(ddlAntibiotico.SelectedValue));

            ////////////////


            Antibiograma oATB = new Antibiograma();
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(Antibiograma));

         
            crit.Add(Expression.Eq("IdProtocolo", oProtocolo));
            crit.Add(Expression.Eq("IdMetodologia", int.Parse(s_idMetodo)));
            crit.Add(Expression.Eq("IdGermen", oGermen));
            crit.Add(Expression.Eq("IdItem", int.Parse(s_iditem)));

            crit.Add(Expression.Eq("IdAntibiotico", oAntibiotico));
              IList lista = crit.List();
            if (lista.Count > 0)
                 args.IsValid=false;
            else
                  args.IsValid=true;


              
        }

       
    }
}