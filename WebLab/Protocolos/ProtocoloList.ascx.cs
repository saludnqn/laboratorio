using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Business;
using System.Drawing;

namespace WebLab.Protocolos
{
    public partial class ProtocoloList1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        public void CargarGrillaProtocolo(string s_idServicio)
        {
            HFidServicio.Value = s_idServicio;

            if (HFidServicio.Value == "3")//microbiologia
            {
            //    lblServicio.Text = "MICROBIOLOGIA";
                DataList1.HeaderStyle.BackColor = Color.Green;
                DataList1.HeaderStyle.ForeColor = Color.White;
            }
            //if (Session["idServicio"].ToString() == "1")
            //    lblServicio.Text = "LABORATORIO";
            if (HFidServicio.Value == "4")
            {
                //lblServicio.Text = "PESQUISA NEONATAL";

                DataList1.HeaderStyle.BackColor = Color.Maroon;
                DataList1.HeaderStyle.ForeColor = Color.White;
            }


            DataList1.DataSource = LeerDatosProtocolos();
            DataList1.DataBind();
        }

        private object LeerDatosProtocolos()
        {
            //if (Session["idServicio"] == null) Session["idServicio"] = 1;

            string str_condicion = " WHERE P.baja=0 and P.idTipoServicio=" + HFidServicio.Value;// Session["idServicio"].ToString();
            DateTime fecha1 = DateTime.Today;
            if (Request["urgencia"] != null)
            {
                str_condicion += " and P.idPrioridad=2 ";
            }

            string m_strSQL = " SELECT  TOP (10) dbo.NumeroProtocolo (idProtocolo) AS numero, Pa.apellido + ' ' + Pa.nombre AS paciente, U.username ,P.idProtocolo ,P.idTipoServicio," +
            " Pa.numeroDocumento, P.idPaciente as idPaciente, P.fechaRegistro , TP.nombre as muestra, '' as efector " +
            " FROM         dbo.LAB_Protocolo AS P " +
            " INNER JOIN   dbo.Sys_Paciente AS Pa ON Pa.idPaciente = P.idPaciente " +
            " INNER JOIN   dbo.Sys_Usuario AS U ON U.idUsuario = P.idUsuarioRegistro " +
            " LEFT JOIN  dbo.LAB_Muestra AS TP ON TP.idMuestra = P.idMuestra " +
            str_condicion +
            " order by P.idProtocolo desc ";


            if (HFidServicio.Value == "4")
            {

                m_strSQL = " SELECT   TOP (10) dbo.NumeroProtocolo (P.idProtocolo) AS numero, S.apellidoPaterno+ ' ' + Pa.nombre AS paciente, U.username ,P.idProtocolo ,P.idTipoServicio," +
                " Pa.numeroDocumento, P.idPaciente as idPaciente, P.fechaRegistro , S.idSolicitudScreeningOrigen as numerosolicitud, E.nombre as efector, '' as muestra " +
                " FROM        dbo.LAB_Protocolo AS P " +
                " INNER JOIN  dbo.Sys_Paciente AS Pa ON Pa.idPaciente = P.idPaciente " +
                " INNER JOIN  dbo.Sys_Usuario AS U ON U.idUsuario = P.idUsuarioRegistro " +
                " inner JOIN  dbo.LAB_SolicitudScreening  AS S ON S.idProtocolo = P.idProtocolo " +
                " inner JOIN  dbo.Sys_Efector  AS E ON E.idefector = P.idEfectorSolicitante " +
                " WHERE P.baja=0 and P.idTipoServicio=4"+
                " order by P.idProtocolo desc ";

            }
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);

            return Ds.Tables[0];

        }


        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //HyperLink oHplInfo = (HyperLink)e.Item.FindControl("hplProtocoloEdit");
            //if (oHplInfo != null)
            //{
            //    string idProtocolo = oHplInfo.NavigateUrl;
            //    oHplInfo.NavigateUrl = "ProtocoloEditPesquisa.aspx?idServicio=4&Desde=Derivacion&Operacion=Modifica&idProtocolo=" + idProtocolo;
            //}


            string s_idServicio = HFidServicio.Value;
            HyperLink oHplInfo = (HyperLink)e.Item.FindControl("hplProtocoloEdit");
            if (oHplInfo != null)
            {
                string s_pagina = "ProtocoloEdit2.aspx"; string s_desde = "Default";
                if (s_idServicio == "4")
                { s_pagina = "ProtocoloEditPesquisa.aspx"; s_desde = "Derivacion"; }

                string idProtocolo = oHplInfo.NavigateUrl;
                oHplInfo.NavigateUrl = s_pagina + "?idServicio=" + s_idServicio + "&Desde="+s_desde+"&Operacion=Modifica&idProtocolo=" + idProtocolo;
                HyperLink oHplNuevoLaboratorio = (HyperLink)e.Item.FindControl("lnkNuevoProtocolo");
                if (oHplNuevoLaboratorio != null)
                {
                    if (s_idServicio == "4") oHplNuevoLaboratorio.Visible = false;
                    else
                    {
                        oHplNuevoLaboratorio.Visible = true;
                        Label oIdPaciente = (Label)e.Item.FindControl("lblidPaciente");
                        oHplNuevoLaboratorio.NavigateUrl = s_pagina + "?idPaciente=" + oIdPaciente.Text + "&Operacion=Alta&idServicio=" + s_idServicio + "&Urgencia=0";
                    }
                }


                Label oMuestra = (Label)e.Item.FindControl("lblTipoMuestra");
                Label oEfector = (Label)e.Item.FindControl("lblEfector");

                if (HFidServicio.Value != "3")///laboratorio o pesquisa
                {

                    oMuestra.Visible = false;
                    HyperLink oHplBacteriologia = (HyperLink)e.Item.FindControl("lnkMicrobiologia");
                    if (oHplBacteriologia != null)
                    {
                        if (HFidServicio.Value == "4") { oHplBacteriologia.Visible = false; oEfector.Visible = true; }
                        else
                        {
                            oEfector.Visible = false;
                            oHplBacteriologia.Visible = true;
                            Label oIdPaciente = (Label)e.Item.FindControl("lblidPaciente");
                            oHplBacteriologia.NavigateUrl = s_pagina + "?idPaciente=" + oIdPaciente.Text + "&Operacion=Alta&idServicio=3&Urgencia=0";
                        }
                    }





                }
                else
              
                    oMuestra.Visible = true;
             
            }
        }
    }
}