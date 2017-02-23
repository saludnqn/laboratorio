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
using Business;
using System.Data.SqlClient;
using Business.Data.Laboratorio;
using Business.Data;

namespace WebLab.Agendas
{
    public partial class AgendaList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
              
                    VerificaPermisos("Agenda");
                    
                    CargarListas();
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
                Utility oUtil = new Utility();
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
            string m_condicion = "";
            if (ddlTipoServicio.SelectedValue != "0") m_condicion += " and A.idTipoServicio=" + ddlTipoServicio.SelectedValue;

            string m_strSQL = " select top 20 A.idAgenda, T.nombre, I.nombre as item, convert(varchar(10),A.fechaDesde,103) as fechaDesde," +
                              " convert(varchar(10),A.fechaHasta,103) as fechaHasta " +
                              " from Lab_TipoServicio T " +
                              " INNER JOIN lAB_aGENDA A on A.idTipoServicio= T.idTipoServicio " +
                              " LEFT JOIN lab_item I on A.iditem=i.iditem" +
                              " where A.baja=0 " + m_condicion +
                              " order by A.idAgenda desc ";

            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);

             
             
            return Ds.Tables[0];
        }



        private void CargarListas()
        {
            Utility oUtil = new Utility();   ///Carga de combos de Areas         
            string m_ssql = "select idTipoServicio, nombre from Lab_TipoServicio WHERE idtipoServicio<4 and (baja = 0)";
            oUtil.CargarCombo(ddlTipoServicio, m_ssql, "idTipoServicio", "nombre");
            ddlTipoServicio.Items.Insert(0, new ListItem("Todos", "0"));


            m_ssql = null;
            oUtil = null;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgendaEdit.aspx", false);
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                ImageButton CmdModificar = (ImageButton)e.Row.Cells[4].Controls[1];
                CmdModificar.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdModificar.CommandName = "Modificar";
                CmdModificar.ToolTip="Modifica";

                 ImageButton CmdEliminar = (ImageButton)e.Row.Cells[5].Controls[1];
                    CmdEliminar.CommandArgument = this.gvLista.DataKeys[e.Row.RowIndex].Value.ToString();
                    if (Permiso == 1)
                    {
                        CmdEliminar.Visible = false;
                        CmdModificar.ToolTip = "Consultar";
                    }

                    CmdEliminar.CommandName = "Eliminar";
                    CmdEliminar.ToolTip = "Eliminar";
                
            }  
        }

        protected void gvLista_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Modificar")
                Response.Redirect("AgendaEdit.aspx?id=" + e.CommandArgument);
            if (e.CommandName == "Eliminar")
            {
                if (Permiso == 2)
                {

                    Eliminar(e.CommandArgument);
                    CargarGrilla();
                }
            }
        }


        private void Eliminar(object p)
        {
            Agenda oRegistro = new Agenda();
            oRegistro = (Agenda)oRegistro.Get(typeof(Agenda), int.Parse(p.ToString()));
            Usuario oUser = new Usuario();
            oRegistro.Baja = true;
           oRegistro.IdUsuarioRegistro = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
            oRegistro.FechaRegistro = DateTime.Now;
            oRegistro.Save();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void ddlTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

    }
}
