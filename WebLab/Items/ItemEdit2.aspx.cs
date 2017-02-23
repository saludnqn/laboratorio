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
using Business;
using Business.Data;
using Business.Data.Laboratorio;
using NHibernate;
using NHibernate.Expression;
using System.Data.SqlClient;
using System.IO;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

namespace WebLab.Items
{
    public partial class ItemEdit2 : System.Web.UI.Page
    {

        CrystalReportSource oCr = new CrystalReportSource();        
          Configuracion oC = new Configuracion(); 
       //   Item oItem = new Item();
            
        protected void Page_PreInit(object sender, EventArgs e)
        {            
            oCr.CacheDuration = 0;
            oCr.EnableCaching = false;
            oC = (Configuracion)oC.Get(typeof(Configuracion), 1);
            
     

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               if (Session["idUsuario"] != null)
               {   
                    VerificaPermisos("Analisis");
                    CargarListas();             
                    if (Request["id"] != null)
                    { 
                        MostrarDatos();
                        MostrarDatosValoresReferencia();
                        MostrarDatosDiagrama();
                        MostrarDatosResultadosPredefinidos();
                        MostrarDatosRecomendaciones();                   
                    }
                }
                else
                 Response.Redirect("../FinSesion.aspx", false);
            }
        }


        ///************************************Inicio de Recomendaciones para el pacientes        ********************//
        private void MostrarDatosRecomendaciones()
        {
            ddlRecomendacion.SelectedValue = "0";
            gvListaRecomendacion.AutoGenerateColumns = false;
            gvListaRecomendacion.DataSource = LeerDatosRecomendacion();
            gvListaRecomendacion.DataBind();
        }

        private object LeerDatosRecomendacion()
        {
            string m_strSQL = " SELECT IR.idItemRecomendacion, R.descripcion as recomendacion " +
                              " FROM LAB_ItemRecomendacion IR " +
                              "INNER JOIN LAB_Recomendacion R ON R.idRecomendacion=IR.idRecomendacion " +
                              " WHERE IR.idItem=" + Request["id"].ToString();


            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);



            return Ds.Tables[0];
        }

        protected void btnAgregarRecomendacion_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                AgregarRecomendacion();
                MostrarDatosRecomendaciones();
                
            }

        }

        private void AgregarRecomendacion()
        {
            ItemRecomendacion oRegistro = new ItemRecomendacion();
            Item oItem = new Item();
            Recomendacion oRec = new Recomendacion();

            oRegistro.IdItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["id"].ToString()));
            oRegistro.IdRecomendacion = (Recomendacion)oRec.Get(typeof(Recomendacion), int.Parse(ddlRecomendacion.SelectedValue));
            oRegistro.Save();


        }

        protected void gvListaRecomendacion_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if  (e.CommandName== "Eliminar")
            {
                    EliminarItemRecomendacion(e.CommandArgument);                                        
                    MostrarDatosRecomendaciones();                                      
             
            }
        }

        private void EliminarItemRecomendacion(object idItem)
        {
            ItemRecomendacion oRegistro = new ItemRecomendacion();
            oRegistro = (ItemRecomendacion)oRegistro.Get(typeof(ItemRecomendacion), int.Parse(idItem.ToString()));
            oRegistro.Delete();
        }

        protected void gvListaRecomendacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton CmdEliminar = (ImageButton)e.Row.Cells[1].Controls[1];
                CmdEliminar.CommandArgument = this.gvListaRecomendacion.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdEliminar.CommandName = "Eliminar";
                if (Permiso == 1)
                {
                    CmdEliminar.Visible = false;
                }
            }
        }

        ///**************************************Fin de REcomendaciones para el paciente********************************************//
        ///


        ///******************************************Inicio de Resultados Predefinidos***************************************************//
        private void MostrarDatosResultadosPredefinidos()
        {
            Item oItem = new Item();
            oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["id"].ToString()));
            //lblItem.Text = oItem.Codigo + " - " + oItem.Nombre;


            // dtDeterminaciones = (System.Data.DataTable)(Session["Tabla1"]);
            ResultadoItem oDetalle = new ResultadoItem();
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(ResultadoItem));
            crit.Add(Expression.Eq("IdItem", oItem));
            crit.Add(Expression.Eq("Baja", false));

            string sDatos = "";
            IList items = crit.List();

            foreach (ResultadoItem oDet in items)
            {
                if (sDatos == "")
                    sDatos = oDet.Resultado + "@";
                else
                    sDatos += oDet.Resultado + "@";

            }

            TxtDatosResultados.Value = sDatos;
           // LeerDatosRP();
            CargarListasRPDefecto();
            ddlResultadoPorDefecto.SelectedValue = oItem.IdResultadoPorDefecto.ToString();

        }


        private void CargarGrillaRP()
        {
            txtNombreRP.Text = "";
            //gvLista.AutoGenerateColumns = false;
            //gvLista.DataSource = LeerDatos();
            //gvLista.DataBind();
        }

        //private object LeerDatosRP()
        //{
        //    string m_strSQL = " SELECT idResultadoItem, resultado" +
        //                      " FROM LAB_ResultadoItem " +
        //                      " WHERE (baja = 0) and idItem=" + Request["id"].ToString() +
        //                      " ORDER BY idResultadoItem"; // SE PONE EL ORDEN EN QUE SE FUE AGREGANDO

        //    DataSet Ds = new DataSet();
        //    SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
        //    adapter.Fill(Ds);



        //    return Ds.Tables[0];
        //}

        //protected void btnAgregar_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    {
        //        Guardar();
        //       // CargarGrilla();
        //    }
        //}

        private void GuardarRP()
        {
            if (Request["id"] != null)
            {
                Item oItem = new Item(); oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["id"].ToString()));
                ///Borrar los existentes 
                ResultadoItem oDetalle = new ResultadoItem();
                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit = m_session.CreateCriteria(typeof(ResultadoItem));
                crit.Add(Expression.Eq("IdItem", oItem));
                //   string sDatos = "";
                IList items = crit.List();

                foreach (ResultadoItem oDet in items)
                {
                    oDet.Delete();
                }
                
                ///Guardar nuevamente
                string[] tabla = TxtDatosResultados.Value.Split('@');

                Usuario oUser = new Usuario();
                oUser = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));

                for (int i = 0; i < tabla.Length - 1; i++)
                {
                    ResultadoItem oRegistro = new ResultadoItem();

                    oRegistro.IdEfector = oC.IdEfector;
                    oRegistro.IdItem = oItem;
                    oRegistro.Resultado = tabla[i].ToString();

                    oRegistro.IdUsuarioRegistro = oUser;
                    oRegistro.FechaRegistro = DateTime.Now;

                    oRegistro.Save();
                }
            }
            ////            
        }

        protected void btnGuardarRP_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                GuardarRP();
                CargarListasRPDefecto();
               /// Response.Redirect("ItemEdit2.aspx", false);                
                
            }
        }

       
        private void CargarListasRPDefecto()
        {
            Utility oUtil = new Utility();
            string m_ssql = "select idResultadoItem, resultado  as nombre from Lab_ResultadoItem where baja=0 and idItem= " + Request["id"].ToString()  +" order by idResultadoItem";
            oUtil.CargarCombo(ddlResultadoPorDefecto, m_ssql, "idResultadoItem", "nombre");
            ddlResultadoPorDefecto.Items.Insert(0, new ListItem("               ", "0"));
            ddlResultadoPorDefecto.UpdateAfterCallBack = true;
        }
        //////////////////////////////*****************///Fin de Resultados Predefinidos*************************************//
        ///



        /// <summary>
        ///******************************************************* inicio de  Diagrama//////********************************//
        /// </summary>
        private void MostrarDatosDiagrama()
        {
            txtCodigoDiagrama.Text = "";
          //  ddlItemDiagrama.SelectedValue = "0";
            txtNombreDiagrama.Text = "";
            //  txtTitulo.Text = "";

            //gvListaDiagrama.AutoGenerateColumns = false;
            //gvListaDiagrama.DataSource = LeerDatosDiagrama();
            //gvListaDiagrama.DataBind();
            Item oItem = new Item();
            oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["id"].ToString()));
            //lblItem.Text = oItem.Codigo + " - " + oItem.Nombre;


            // dtDeterminaciones = (System.Data.DataTable)(Session["Tabla1"]);
            ResultadoItem oDetalle = new ResultadoItem();
            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(PracticaDeterminacion));
            crit.Add(Expression.Eq("IdItemPractica", oItem));
            crit.Add(Expression.Eq("Orden", 1));

            string sDatos = "";
            IList items = crit.List();

            foreach (PracticaDeterminacion oDet in items)
            {
                  Item oItemDeterminacion = new Item();
            oItemDeterminacion = (Item)oItemDeterminacion.Get(typeof(Item), oDet.IdItemDeterminacion);

                if (sDatos == "")
                    sDatos = oItemDeterminacion.Codigo + "#" +  oDet.Titulo + "@";
                else
                    sDatos += oItemDeterminacion.Codigo  + "#" +  oDet.Titulo + "@";

            }

            TxtDatosDiagrama.Value = sDatos;
            //LeerDatosRP();
            //CargarListasRPDefecto();
            //ddlResultadoPorDefecto.SelectedValue = oItem.IdResultadoPorDefecto.ToString();



        }


        private void GuardarDiagrama()
        {
            if (Request["id"] != null)
            {
                Item oItem = new Item(); oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["id"].ToString()));
                ///Borrar los existentes 
                EliminarDiagrama(oItem);


                ///Guardar nuevamente
                string[] tabla = TxtDatosDiagrama.Value.Split('@');


                for (int i = 0; i < tabla.Length - 1; i++)
                {
                    string[] item = tabla[i].ToString().Split('#');

                    AgregarItemDiagrama(item[0].ToString(), item[1].ToString());

                }
            }
            ////            
        }
        //private object LeerDatosDiagrama()
        //{
        //    string m_strSQL = " SELECT PD.idPracticaDeterminacion as idDiagrama, " +
        //                      " CASE WHEN PD.iditemdeterminacion = 0 THEN PD.titulo ELSE I.codigo + ' - ' + I.nombre END AS nombre," +
        //                      " PD.titulo as textoimprimir" +
        //                      " FROM LAB_PracticaDeterminacion AS PD " +
        //                      " left JOIN LAB_Item AS I ON PD.idItemDeterminacion = I.idItem" +
        //                      " WHERE PD.iditempractica=" + Request["id"].ToString() +
        //                      " ORDER BY PD.idPracticaDeterminacion"; // SE PONE EL ORDEN EN QUE SE FUE AGREGANDO

        //    DataSet Ds = new DataSet();
        //    SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
        //    adapter.Fill(Ds);



        //    return Ds.Tables[0];
        //}

       



        //protected void btnAgregarItemDiagrama_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    {
        //        AgregarItemDiagrama();
        //        MostrarDatosDiagrama();
                
        //    }
        //}

        private void AgregarItemDiagrama(string codigo, string titulo)
        {     
            //PracticaDeterminacion oRegistro = new PracticaDeterminacion();
            Item oItemPractica = new Item();            
            Usuario oUser = new Usuario();


            Configuracion oC = new Configuracion();
            oC = (Configuracion)oC.Get(typeof(Configuracion), "IdConfiguracion", 1);


            oItemPractica = (Item)oItemPractica.Get(typeof(Item), int.Parse(Request["id"].ToString()));



            //if (noExiste(oItemPractica, oC.IdEfector))
            //{
                Item oI = new Item();
                oI = (Item)oI.Get(typeof(Item), "Codigo" ,codigo,"Baja",false);

                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit = m_session.CreateCriteria(typeof(PracticaDeterminacion));

                crit.Add(Expression.Eq("IdItemPractica", oI));
                //crit.Add(Expression.Eq("IdEfector", oC.IdEfector));

                IList lista = crit.List();
                if (lista.Count > 0)
                {

                    if (titulo.Length > 99) titulo = titulo.Substring(0, 99);
                    PracticaDeterminacion oRegistro = new PracticaDeterminacion();
                    oRegistro.IdEfector = oC.IdEfector;
                    oRegistro.IdItemPractica = oItemPractica;

                    oRegistro.IdItemDeterminacion = oI.IdItem;
                    oRegistro.Titulo = titulo;
                    oRegistro.Orden = 1;
                    oRegistro.FormatoImpresion = "";
                    oRegistro.IdUsuarioRegistro = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
                    oRegistro.FechaRegistro = DateTime.Now;
                    oRegistro.Save();

                    foreach (PracticaDeterminacion oDet in lista)
                    {
                        

                        PracticaDeterminacion oR = new PracticaDeterminacion();
                        oR.IdEfector = oC.IdEfector;
                        oR.IdItemPractica = oItemPractica;
                        oR.IdItemDeterminacion = oDet.IdItemDeterminacion;
                        oR.Titulo = oDet.Titulo;
                         oR.Orden = 0;
                        oR.FormatoImpresion = "";
                        oR.IdUsuarioRegistro = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
                        oR.FechaRegistro = DateTime.Now;
                        oR.Save();
                    }
                }
                else
                {

                    PracticaDeterminacion oR = new PracticaDeterminacion();
                    oR.IdEfector = oC.IdEfector;
                    oR.IdItemPractica = oItemPractica;

                    oR.IdItemDeterminacion = oI.IdItem;
                    oR.Titulo = titulo;
                    oR.Orden = 1;
                    oR.FormatoImpresion = "";
                    oR.IdUsuarioRegistro = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
                    oR.FechaRegistro = DateTime.Now;
                    oR.Save();
                }

            }

        protected void btnGuardarDiagrama_Click(object sender, EventArgs e)
        {
                GuardarDiagrama();
                string m_parametroFiltro = "&Codigo=" + Request["Codigo"].ToString() + "&Nombre=" + Request["Nombre"].ToString() + "&Servicio=" + Request["Servicio"].ToString() +
        "&Area=" + Request["Area"].ToString() + "&Orden=" + Request["Orden"].ToString();                
                    Response.Redirect("ItemEdit2.aspx?id=" + Request["id"].ToString() + m_parametroFiltro, true);
      

        }


      

        protected void btnAgregarTitulo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //  AgregarItem("T");
                MostrarDatosDiagrama();
            }
        }

        

        private void MostrarInforme()
        {
            oCr.Report.FileName = "../Informes/Diagrama.rpt";
            oCr.ReportDocument.SetDataSource(GetDataSet());
            oCr.DataBind();
            oCr.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Diagrama.pdf");

            //MemoryStream oStream; // using System.IO
            //oStream = (MemoryStream)oCr.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment;filename=Diagrama.pdf");
            //Response.BinaryWrite(oStream.ToArray());
            //Response.End();

        }

        private DataTable GetDataSet()
        {

            string m_strSQL = " SELECT P.nombre AS practica, D.titulo AS nombre," +
                              " CASE WHEN I.idCategoria = 1 THEN 'Si' ELSE 'No' END AS esTitulo " +
                              " FROM LAB_PracticaDeterminacion AS D " +
                              " INNER JOIN LAB_Item AS P ON D.idItemPractica = P.idItem " +
                              " INNER JOIN lAB_iTEM AS i ON I.IDITEM= d.IDITEMDETERMINACION " +
                              " WHERE D.iditempractica=" + Request["id"].ToString() +
                              " ORDER BY D.idPracticaDeterminacion"; // SE PONE EL ORDEN EN QUE SE FUE AGREGANDO

            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);



            return Ds.Tables[0];

        }


      
        protected void ddlItemDiagrama_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemDiagrama.SelectedValue != "0")
            {
                Item oItem = new Item();
                oItem = (Item)oItem.Get(typeof(Item), int.Parse(ddlItemDiagrama.SelectedValue));
                if (oItem != null)
                {
                    //    ddlItem.SelectedValue = oItem.IdItem.ToString();
                    txtCodigoDiagrama.Text = oItem.Codigo;
                    txtNombreDiagrama.Text = oItem.Descripcion;
                }

                txtCodigoDiagrama.UpdateAfterCallBack = true;
                txtNombreDiagrama.UpdateAfterCallBack = true;
            }
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

   

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            MostrarInforme();
        }

        protected void lnkRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ItemList.aspx", false);
        }

        /// <summary>
        /// *************************************************find e Diagrama*****************************************************///
        /// </summary>
        /// 

        ///-******************************************Inicio Valores de REferencia **************************************************///

        private void MostrarDatosValoresReferencia()
        {
            Item oItem = new Item();
            oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["id"].ToString()));
            //lblItem.Text = oItem.Codigo + " - " + oItem.Nombre;
            lblDecimales.Text = oItem.FormatoDecimal.ToString();
            lblDecimales.Visible = false;
            HabilitarControlFormatoVR();
            CargarGrillaVR();
        }

        private void HabilitarControlFormatoVR()
        {
            string expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,2}";
            switch (lblDecimales.Text)
            {
                case "0": /// entero
                    {
                        expresionControlDecimales = "[-+]?\\d*";
                        revValorMinimo.Text = "Sólo se admite numero entero";
                        revValorMaximo.Text = "Sólo se admite numero entero";
                        revValorMinimo.ErrorMessage = "Valor mínimo sólo admite numero entero";
                        revValorMaximo.ErrorMessage = "Valor máximo sólo admite numero entero";
                    }
                    break;
                case "1":
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,1}";
                        revValorMinimo.Text = "Verifique formato #.#";
                        revValorMaximo.Text = "Verifique formato #.#";
                        revValorMinimo.ErrorMessage = "Valor mínimo sólo admite numero con formato #.#";
                        revValorMaximo.ErrorMessage = "Valor máximo sólo admite numero con formato #.#";
                    } break;
                case "2":
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,2}";
                        revValorMinimo.Text = "Verifique formato #.##";
                        revValorMaximo.Text = "Verifique formato #.##";
                        revValorMinimo.ErrorMessage = "Valor mínimo sólo admite numero con formato #.##";
                        revValorMaximo.ErrorMessage = "Valor máximo sólo admite numero con formato #.##";
                    } break;
                case "3":
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,3}";
                        revValorMinimo.Text = "Verifique formato #.###";
                        revValorMaximo.Text = "Verifique formato #.###";
                        revValorMinimo.ErrorMessage = "Valor mínimo sólo admite numero con formato #.###";
                        revValorMaximo.ErrorMessage = "Valor máximo sólo admite numero con formato #.###";
                    } break;
                case "4":
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,4}";
                        revValorMinimo.Text = "Verifique formato #.####";
                        revValorMaximo.Text = "Verifique formato #.####";
                        revValorMinimo.ErrorMessage = "Valor mínimo sólo admite numero con formato #.####";
                        revValorMaximo.ErrorMessage = "Valor máximo sólo admite numero con formato #.####";
                    } break;
            }

            revValorMinimo.ValidationExpression = expresionControlDecimales;
            revValorMaximo.ValidationExpression = expresionControlDecimales;
            revValorMinimo.UpdateAfterCallBack = true;
            revValorMaximo.UpdateAfterCallBack = true;
        }

        protected void rdbRango_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbRango.Items[0].Selected)
            {
                txtValorMinimo.Enabled = true;
                txtValorMaximo.Enabled = true;

            }
            if (rdbRango.Items[1].Selected)
            {
                txtValorMinimo.Enabled = true;
                txtValorMaximo.Enabled = false;
                //txtValorMaximo.BackColor = Color.Gainsboro;
                txtValorMaximo.Text = "";
            }
            if (rdbRango.Items[2].Selected)
            {
                txtValorMaximo.Enabled = true;
                txtValorMinimo.Enabled = false;
                txtValorMinimo.Text = "";
            }

            if (rdbRango.Items[3].Selected)
            {
                txtValorMinimo.Enabled = false;
                txtValorMaximo.Enabled = false;
                txtValorMinimo.Text = "";
                txtValorMaximo.Text = "";
            }
            else
                HabilitarControlFormato();


            txtValorMinimo.UpdateAfterCallBack = true;
            txtValorMaximo.UpdateAfterCallBack = true;


        }

        protected void cvValores_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (rdbRango.Items[0].Selected)  //Rango
            {
                cvValores.ErrorMessage = "Debe ingresar un valor minimo y un valor maximo";
                if ((txtValorMaximoVR.Text == "") && (txtValorMinimoVR.Text == ""))
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            if (rdbRango.Items[1].Selected) // Limite Inferior
            {
                cvValores.ErrorMessage = "Debe ingresar un valor minimo";
                if (txtValorMinimoVR.Text == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            if (rdbRango.Items[2].Selected) //Limite Superior
            {
                cvValores.ErrorMessage = "Debe ingresar un valor máximo";
                if (txtValorMaximoVR.Text == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }

            if (rdbRango.Items[3].Selected)///solo observaciones
            {
                cvValores.ErrorMessage = "Debe ingresar una observación";
                if (txtObservaciones.Text == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            
        }

        protected void gvListaVR_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName== "Eliminar")
            {
                    EliminarItemVR(e.CommandArgument);
                    CargarGrillaVR();             
            }
        }

        private void CargarGrillaVR()
        {
            gvListaVR.AutoGenerateColumns = false;
            gvListaVR.DataSource = LeerDatosVR();
            gvListaVR.DataBind();
        }

        private object LeerDatosVR()
        {
            string m_strSQL = " SELECT VR.idValorReferencia, VR.sexo, CONVERT(varchar(5), VR.edadDesde) + '-' + CONVERT(varchar(5), VR.edadHasta) AS edad, "+
                              " CASE VR.unidadEdad when 0 then 'Años' when 1 then 'Meses' when 2 then 'Dias' end as unidadEdad, M.nombre AS metodo,  " +
                              " CASE WHEN tipovalor = 0 OR tipovalor = 1 THEN  " +
                              " CASE I.formatoDecimal " +
                              " WHEN 0 THEN cast (CAST(vr.valorminimo AS int) as varchar) " +
                              " WHEN 1 THEN cast(CAST(vr.valorminimo AS decimal(18, 1)) as varchar)" +
                              " WHEN 2 THEN cast(CAST(vr.valorminimo AS decimal(18, 2)) as varchar) " +
                              " WHEN 3 THEN cast(CAST(vr.valorminimo AS decimal(18, 3)) as varchar) " +
                              " WHEN 4 THEN cast(CAST(vr.valorminimo AS decimal(18, 4)) as varchar) " +
                              " END ELSE ' - ' END AS minimo, " +
                              " CASE WHEN tipovalor = 0 OR  tipovalor = 2 THEN  " +
                              " CASE I.formatoDecimal " +
                              " WHEN 0 THEN cast (CAST(vr.valormaximo AS int) as varchar) " +
                              " WHEN 1 THEN cast(CAST(vr.valormaximo AS decimal(18, 1)) as varchar) " +
                              " WHEN 2 THEN cast(CAST(vr.valormaximo AS decimal(18, 2)) as varchar) " +
                              " WHEN 3 THEN cast(CAST(vr.valormaximo AS decimal(18, 3)) as varchar) " +
                              " WHEN 4 THEN cast(CAST(vr.valormaximo AS decimal(18, 4)) as varchar) " +
                              " END ELSE ' - ' END AS maximo, VR.observacion " +
                              " FROM dbo.LAB_ValorReferencia AS VR " +
                              " INNER JOIN  dbo.LAB_Item AS I ON VR.idItem = I.idItem " +
                              " LEFT OUTER JOIN dbo.LAB_Metodo AS M ON VR.idMetodo = M.idMetodo" +
                              " where VR.iditem=" + Request["id"].ToString();

            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);

            return Ds.Tables[0];
        }

        private void EliminarItemVR(object idItem)
        {
            ValorReferencia oRegistro = new ValorReferencia();
            oRegistro = (ValorReferencia)oRegistro.Get(typeof(ValorReferencia), int.Parse(idItem.ToString()));
            oRegistro.Delete();
        }

        protected void gvListaVR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton CmdEliminar = (ImageButton)e.Row.Cells[7].Controls[1];
                CmdEliminar.CommandArgument = this.gvListaVR.DataKeys[e.Row.RowIndex].Value.ToString();
                CmdEliminar.CommandName = "Eliminar";
                if (Permiso == 1)
                {
                    CmdEliminar.Visible = false;
                }
            }
        }

        private void GuardarVR()
        {
            ValorReferencia oRegistro = new ValorReferencia();
            Efector oEfector = new Efector();
            Usuario oUser = new Usuario();
            Item oItem = new Item();

            //Configuracion oC = new Configuracion();
            //oC = (Configuracion)oC.Get(typeof(Configuracion), "IdConfiguracion", 1);

            oRegistro.IdEfector = oC.IdEfector;
            oRegistro.IdItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["id"].ToString())); ;
            oRegistro.Sexo = ddlSexo.SelectedValue;
            oRegistro.TodasEdades = true;
            oRegistro.EdadDesde = int.Parse(txtEdadDesde.Value);
            oRegistro.EdadHasta = int.Parse(txtEdadHasta.Value);
            oRegistro.UnidadEdad = int.Parse(ddlUnidadEdad.SelectedValue);
            oRegistro.IdMetodo = int.Parse(ddlMetodo.SelectedValue);

            if (rdbRango.Items[0].Selected) oRegistro.TipoValor = 0;
            if (rdbRango.Items[1].Selected) oRegistro.TipoValor = 1;
            if (rdbRango.Items[2].Selected) oRegistro.TipoValor = 2;
            if (rdbRango.Items[3].Selected) oRegistro.TipoValor = 3;

            if (txtValorMinimoVR.Text != "") oRegistro.ValorMinimo = decimal.Parse(txtValorMinimoVR.Text, System.Globalization.CultureInfo.InvariantCulture);
            if (txtValorMaximoVR.Text != "") oRegistro.ValorMaximo = decimal.Parse(txtValorMaximoVR.Text, System.Globalization.CultureInfo.InvariantCulture);

            oRegistro.Observacion = this.txtObservaciones.Text;
            oRegistro.IdUsuarioRegistro = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
            oRegistro.FechaRegistro = DateTime.Now;

            oRegistro.Save();
        }


        /// <summary>
        /// //////////////////////////////////////////////////////////Fin de Valores de Referencia /*******************************************/
        /// </summary>

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
                    case 1:
                        {
                            btnGuardar.Visible = false;
                            btGuardarVR.Visible = false;
                           // btnAgregarItemDiagrama.Visible = false;
                            btnAgregarRecomendacion.Visible = false;
                            btnGuardarRP.Visible = false;
                            btnGuardarRPDefecto.Visible = false;
                            btnNuevo.Visible = false;
                        } break;
                }
            }
            else Response.Redirect("../FinSesion.aspx", false);
        }


        private void MostrarDatos()
        {
            Item oItem = new Item();
            oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["id"].ToString()));
            //Item oRegistro = new Item();
            //oRegistro = (Item)oRegistro.Get(typeof(Item), int.Parse(Request["id"].ToString()));
            lblItemVR.Text = oItem.Codigo + " - " + oItem.Nombre;
            lblItemDiagrama.Text = oItem.Codigo + " - " + oItem.Nombre;
            lblItemRP.Text = oItem.Codigo + " - " + oItem.Nombre;
            lblItemRecomendacion.Text = oItem.Codigo + " - " + oItem.Nombre;
            lblItemHiv.Text = oItem.Codigo + " - " + oItem.Nombre;

            txtCodigo.Text = oItem.Codigo.ToUpper();
            txtNombre.Text = oItem.Nombre;
            txtDescripcion.Text = oItem.Descripcion;
            ddlServicio.SelectedValue = oItem.IdArea.IdTipoServicio.IdTipoServicio.ToString();
            CargarArea(); ddlArea.SelectedValue = oItem.IdArea.IdArea.ToString();

            if (oItem.Tipo == "P") rdbTipo.Items[0].Selected = true;
            else rdbTipo.Items[1].Selected = true;

            //      txtRecomendaciones.Text=oItem.Recomendacion;
            if (oItem.IdEfectorDerivacion != oItem.IdEfector)
            {
                ddlDerivable.SelectedValue = "1";
                HabilitarDerivador();
                ddlEfector.SelectedValue = oItem.IdEfectorDerivacion.IdEfector.ToString();
            }
            else ddlDerivable.SelectedValue = "0";

            if (oItem.RequiereMuestra == "S")
            {
                rdbRequiereMuestra.Items[0].Selected = true;
                rdbRequiereMuestra.Items[1].Selected = false;
            }
            else
            {
                rdbRequiereMuestra.Items[0].Selected = false;
                rdbRequiereMuestra.Items[1].Selected = true;
            }

            ddlUnidadMedida.SelectedValue = oItem.IdUnidadMedida.ToString();


            if (oItem.IdCategoria == 0) rdbCategoria.Items[0].Selected = true;
            if (oItem.IdCategoria == 1) rdbCategoria.Items[1].Selected = true;

            HabilitarTipoResultados();
            ddlTipoResultado.SelectedValue = oItem.IdTipoResultado.ToString();
            HabilitarValorDefecto();
            ddlDecimales.SelectedValue = oItem.FormatoDecimal.ToString();
            ddlMultiplicador.SelectedValue = oItem.Multiplicador.ToString();
            HabilitarDecimales();

            if ((oItem.IdTipoResultado == 1) || (oItem.IdTipoResultado == 2))
                txtValorDefecto.Text = oItem.ResultadoDefecto;
            else
            {
                txtValorDefecto.Text = "";
                txtValorDefecto.Enabled = false;
            }

            if ((oItem.ValorMinimo != -1) && (oItem.ValorMaximo != -1))
            {
                string formato = Formato(ddlDecimales.SelectedValue);
                /*txtValorMinimo.Text = oItem.ValorMinimo.ToString(System.Globalization.CultureInfo.InvariantCulture);
                txtValorMaximo.Text = oItem.ValorMaximo.ToString(System.Globalization.CultureInfo.InvariantCulture);*/
                decimal v1 = decimal.Parse(oItem.ValorMinimo.ToString(formato));
                decimal v2 = decimal.Parse(oItem.ValorMaximo.ToString(formato));
                txtValorMinimo.Text = v1.ToString(System.Globalization.CultureInfo.InvariantCulture);
                txtValorMaximo.Text = v2.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            HabilitarControlFormato();

            txtDuracion.Value = oItem.Duracion.ToString();

            ddlItemReferencia.SelectedValue = oItem.IdItemReferencia.ToString(); MostrarCodigoItemReferencia();




            txtCodigoNomenclador.Text = oItem.CodigoNomenclador;
            MostrarItemNomenclador();

            if (oItem.IdTipoResultado == 0)  /// compuesta
            {
                pnlVR.Enabled = false;
                pnlDiagrama.Enabled = true;
            //    pnlVerDiagrama.Enabled = true;
                pnlPredefinidos.Enabled = false;
            }
            else
            {
                pnlVR.Enabled = true;
                pnlDiagrama.Enabled = false;
               // pnlVerDiagrama.Enabled = false;
                if ((oItem.IdTipoResultado == 3)||(oItem.IdTipoResultado == 4))  ///resultados predefinidos (selección simple o multiple)
                    pnlPredefinidos.Enabled = true;
                else
                    pnlPredefinidos.Enabled = false;
            }
            if (oItem.Tipo=="P") /// solo si el practica
            pnlRecomendacion.Enabled = true;
            else
                pnlRecomendacion.Enabled = false;



            ///codifica HIV: Solapa Mas Opciones
            chkCodificaHiv.Checked = oItem.CodificaHiv;
            txtLimite.Value = oItem.LimiteTurnosDia.ToString();
            chkEtiquetaAdicional.Checked = oItem.EtiquetaAdicional;
            ///
            /////screening neonatal
          //  chkIsScreening.Checked=oItem.IsScreeening ;


            if (oItem.Disponible) ddlDisponible.SelectedValue = "1";
            else ddlDisponible.SelectedValue = "0";

            pnlVR.UpdateAfterCallBack = true;
            pnlDiagrama.UpdateAfterCallBack = true;
         //   pnlVerDiagrama.UpdateAfterCallBack = true;
            pnlPredefinidos.UpdateAfterCallBack = true;
            pnlRecomendacion.UpdateAfterCallBack = true;
            txtValorDefecto.UpdateAfterCallBack = true;



            lnkRegresar.Visible = true;



        }

        private void HabilitarValorDefecto()
        {
            if (ddlTipoResultado.SelectedValue == "1")
            {
                txtValorDefecto.Enabled = true;
                txtValorDefecto.Width = Unit.Pixel(76);

            }
            if (ddlTipoResultado.SelectedValue == "2")
            {
                txtValorDefecto.Enabled = true;
                txtValorDefecto.Width = Unit.Pixel(250);
            }
            txtValorDefecto.UpdateAfterCallBack = true;

        }

        private string Formato(string p)
        {
            string aux = "";
            switch (p)
            {
                case "0": aux = "0"; break;
                case "1": aux = "0.0"; break;
                case "2": aux = "0.00"; break;
                case "3": aux = "0.000"; break;
                case "4": aux = "0.0000"; break;
            }
            return aux;
        }

        private void HabilitarTipoResultados()
        {
            if (rdbCategoria.Items[0].Selected)//Simple
            {
                ddlTipoResultado.Enabled = true;
                rvTipoResultado.Enabled = true;
                ddlDecimales.Enabled = true;
                txtValorMinimo.Enabled = true;
                txtValorMaximo.Enabled = true;
            }

            else //Compuesta
            {
                ddlTipoResultado.SelectedValue = "0";
                ddlTipoResultado.Enabled = false;
                ddlDecimales.Enabled = false;
                rvTipoResultado.Enabled = false;
                txtValorMinimo.Text = "";
                txtValorMaximo.Text = "";
                txtValorMinimo.Enabled = false;
                txtValorMaximo.Enabled = false;
            }
            ddlTipoResultado.UpdateAfterCallBack = true;
            ddlDecimales.UpdateAfterCallBack = true;
            rvTipoResultado.UpdateAfterCallBack = true;
            txtValorMinimo.UpdateAfterCallBack = true;
            txtValorMaximo.UpdateAfterCallBack = true;

        }


        private void CargarListas()
        {
            pnlVR.Enabled = false;
            pnlDiagrama.Enabled = false;
        //    pnlVerDiagrama.Enabled = false;
            pnlPredefinidos.Enabled = false;
            pnlRecomendacion.Enabled = false;

            pnlVR.UpdateAfterCallBack = true;
            pnlDiagrama.UpdateAfterCallBack = true;
         //   pnlVerDiagrama.UpdateAfterCallBack = true;
            pnlPredefinidos.UpdateAfterCallBack = true;
            pnlRecomendacion.UpdateAfterCallBack = true;

            Utility oUtil = new Utility();
            ///Carga de combos de Areas
            ///
            string m_ssql = "select idTipoServicio, nombre from Lab_TipoServicio WHERE (baja = 0)";
            oUtil.CargarCombo(ddlServicio, m_ssql, "idTipoServicio", "nombre");
            CargarArea();

            ///Carga de combos de Unidad de Medida
            m_ssql = "SELECT idUnidadMedida, nombre FROM LAB_UnidadMedida where baja=0 order by nombre";
            oUtil.CargarCombo(ddlUnidadMedida, m_ssql, "idUnidadMedida", "nombre");
            ddlUnidadMedida.Items.Insert(0, new ListItem("No Aplica", "0"));


            ///Carga de combos de Item de Referencia
            m_ssql = "SELECT I.idItem, I.nombre FROM LAB_Item I " +
                "INNER JOIN lab_AREA A ON I.idArea=A.idArea " +
                "where A.baja=0 and I.baja=0";
            oUtil.CargarCombo(ddlItemReferencia, m_ssql, "idItem", "nombre");
            ddlItemReferencia.Items.Insert(0, new ListItem("No Aplica", "0"));


            ///Carga de combos del Nomenclador
            m_ssql = "SELECT codigo, descrip FROM LAB_Nomenclador order by descrip";
            oUtil.CargarCombo(ddlItemNomenclador, m_ssql, "codigo", "descrip");
            ddlItemNomenclador.Items.Insert(0, new ListItem("No Aplica", "0"));

            //Utility oUtil = new Utility();
            /////Carga de combos de Areas
            m_ssql = "select idMetodo, nombre from Lab_metodo  where baja=0 order by nombre";
            oUtil.CargarCombo(ddlMetodo, m_ssql, "idMetodo", "nombre");
            ddlMetodo.Items.Insert(0, new ListItem("No Aplica", "0"));

            if (Request["id"] != null)
            {
                m_ssql =
                    " SELECT idItem, nombre FROM LAB_Item AS I " +
" WHERE     (baja = 0) AND (idItem <> " + Request["id"].ToString() + ") " +
" AND (idArea IN  (SELECT a.idArea   FROM LAB_Area AS a INNER JOIN LAB_Item AS I ON I.idArea = a.idArea" +
                            " WHERE      (I.idItem = " + Request["id"].ToString() + ")))ORDER BY nombre";
                
                
                //"select I.idItem, I.nombre from Lab_item I" +
                //    " INNER join LAB_Area A on A.idArea= I.idarea "+
                //    " where I.baja=0 and I.idItem<>" + Request["id"].ToString() + " order by I.nombre";
                oUtil.CargarCombo(ddlItemDiagrama, m_ssql, "idItem", "nombre");
                ddlItemDiagrama.Items.Insert(0, new ListItem("Seleccione Item a agregar", "0"));
            }


            m_ssql = "select idRecomendacion, nombre  from Lab_Recomendacion where baja = 0 order by nombre";
            oUtil.CargarCombo(ddlRecomendacion, m_ssql, "idRecomendacion", "nombre");
            ddlRecomendacion.Items.Insert(0, new ListItem("Seleccione Recomendacion", "0"));


            txtLimite.Value = "0";
       //     lnkRegresar.Visible = false;
            m_ssql = null;
            oUtil = null;
        }

        private void CargarArea()
        {
            Utility oUtil = new Utility();
            string m_ssql = "select idArea, nombre from Lab_Area where baja=0  and idTipoServicio=" + ddlServicio.SelectedValue + " order by nombre";
            oUtil.CargarCombo(ddlArea, m_ssql, "idArea", "nombre");
            ddlArea.Items.Insert(0, new ListItem("Seleccione Area", "0"));
            ddlArea.UpdateAfterCallBack = true;

        }
        protected void btnGuardarVR_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                GuardarVR();
                CargarGrillaVR();
            }
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }



        protected void ddlDerivable_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarDerivador();

        }

        private void HabilitarDerivador()
        {
            if (ddlDerivable.SelectedValue.ToString() == "1")//Si
                CargarEfector();
            else
            {
                ddlEfector.Enabled = false;
                rvEfector.Enabled = false;
            }
            ddlEfector.UpdateAfterCallBack = true;
            rvEfector.UpdateAfterCallBack = true;
        }

        private void CargarEfector()
        {
            Utility oUtil = new Utility();

            string m_ssql = "select E.idEfector, E.nombre from sys_efector E " +
                " INNER JOIN lab_Configuracion C on C.idEfector<>E.idEfector " +
                "order by E.nombre";
            oUtil.CargarCombo(ddlEfector, m_ssql, "idEfector", "nombre");
            ddlEfector.Items.Insert(0, new ListItem("Seleccione Efector", "0"));
            ddlEfector.Enabled = true;
            rvEfector.Enabled = true;

            m_ssql = null;
            oUtil = null;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Item oReg = new Item();
                if (Request["id"] != null)
                    oReg = (Item)oReg.Get(typeof(Item), int.Parse(Request["id"].ToString()));

                Guardar(oReg);

                string m_parametroFiltro = "&Codigo=" + Request["Codigo"].ToString() + "&Nombre=" + Request["Nombre"].ToString() + "&Servicio=" + Request["Servicio"].ToString() +
        "&Area=" + Request["Area"].ToString() + "&Orden=" + Request["Orden"].ToString();

                if (Request["id"] != null) //Modificacion
                {
                    // Response.Redirect ("javascript:history.go(-3);");
                 //   Response.Redirect("ItemList.aspx", false);
                    string popupScript = "<script language='JavaScript'> alert('Los datos se guardaron correctamente'); </script>";
                    Page.RegisterStartupScript("PopupScript", popupScript);
                    Response.Redirect("ItemEdit2.aspx?id=" + oReg.IdItem + m_parametroFiltro, false);
                }
                else //Nuevo
                {
                    //if (rdbCategoria.Items[1].Selected)//compuesta
                    //    Response.Redirect("ItemDiagramacion.aspx?id=" + oReg.IdItem, false);
                    //else
                    //    ///si es simple y con resultados predefinidos
                    //    if (ddlTipoResultado.SelectedValue == "3")
                    //        Response.Redirect("ItemResultado2.aspx?id=" + oReg.IdItem, false);
                    //    else
                    Response.Redirect("ItemEdit2.aspx?id=" + oReg.IdItem + m_parametroFiltro, false);
                }

            }
        }

        private void Guardar(Item oRegistro)
        {
            Area oArea = new Area();
            Efector oEfector = new Efector();
            Usuario oUser = new Usuario();

            //Configuracion oC = new Configuracion();
            //oC = (Configuracion)oC.Get(typeof(Configuracion), "IdConfiguracion", 1);

            oRegistro.IdEfector = oC.IdEfector;
            oRegistro.Codigo = txtCodigo.Text.ToUpper();
            oRegistro.Nombre = txtNombre.Text;
            oRegistro.Descripcion = txtDescripcion.Text;
            oRegistro.IdArea = (Area)oArea.Get(typeof(Area), int.Parse(ddlArea.SelectedValue));

            if (rdbTipo.Items[0].Selected) oRegistro.Tipo = "P";
            else oRegistro.Tipo = "D";


            if (ddlDerivable.SelectedValue == "1")
                oRegistro.IdEfectorDerivacion = (Efector)oEfector.Get(typeof(Efector), int.Parse(ddlEfector.SelectedValue));
            else
                oRegistro.IdEfectorDerivacion = oRegistro.IdEfector;

            oRegistro.RequiereMuestra = rdbRequiereMuestra.SelectedItem.Value;
            oRegistro.IdUnidadMedida = int.Parse(ddlUnidadMedida.SelectedValue);


            if (rdbCategoria.Items[0].Selected) //Simple
            { oRegistro.IdCategoria = 0;
           // EliminarDiagrama(oRegistro);
            }
            if (rdbCategoria.Items[1].Selected) oRegistro.IdCategoria = 1; //Compuesta

            oRegistro.IdTipoResultado = int.Parse(ddlTipoResultado.SelectedValue);
            oRegistro.FormatoDecimal = int.Parse(ddlDecimales.SelectedValue);
            if (txtValorMinimo.Text == "") txtValorMinimo.Text = "-1";
            if (txtValorMaximo.Text == "") txtValorMaximo.Text = "-1";
            oRegistro.ValorMinimo = decimal.Parse(txtValorMinimo.Text, System.Globalization.CultureInfo.InvariantCulture);
            oRegistro.ValorMaximo = decimal.Parse(txtValorMaximo.Text, System.Globalization.CultureInfo.InvariantCulture);

            oRegistro.Multiplicador =int.Parse( ddlMultiplicador.SelectedValue);
            oRegistro.ResultadoDefecto = txtValorDefecto.Text;
            oRegistro.LimiteTurnosDia = 0;
            //// si no es de tipo predefinido pone el idResultadoDefecto=0 dado que este campo se usa sólo para los predefinidos simples
            if (int.Parse(ddlTipoResultado.SelectedValue) != 3) oRegistro.IdResultadoPorDefecto = 0;


            oRegistro.Duracion = int.Parse(txtDuracion.Value.ToString());

            if (ddlItemReferencia.SelectedValue != "0") oRegistro.IdItemReferencia = int.Parse(ddlItemReferencia.SelectedValue);


            oRegistro.IdUsuarioRegistro = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
            oRegistro.FechaRegistro = DateTime.Now;

            oRegistro.CodigoNomenclador = txtCodigoNomenclador.Text;
            if (ddlDisponible.SelectedValue=="1")
            oRegistro.Disponible = true;
            else oRegistro.Disponible = false;


            oRegistro.Save();
            ///Si es simple y tenia diagrama se borra el mismo
            if    ( oRegistro.IdCategoria == 0)            EliminarDiagrama(oRegistro);
        }

        private void EliminarDiagrama(Item oRegistro)
        {
          

            ISession m_session = NHibernateHttpModule.CurrentSession;
            ICriteria crit = m_session.CreateCriteria(typeof(PracticaDeterminacion));
            crit.Add(Expression.Eq("IdItemPractica", oRegistro));            
            IList lista = crit.List();

            foreach (PracticaDeterminacion oItem in lista)
            {
                oItem.Delete();
            }
        }



        protected void cvCodigo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Verifica que no exista un item con el codigo ingresado

            ISession m_session = NHibernateHttpModule.CurrentSession;

            ICriteria crit = m_session.CreateCriteria(typeof(Item));

            crit.Add(Expression.Eq("Codigo", txtCodigo.Text));
            crit.Add(Expression.Eq("Baja", false));


            IList lista = crit.List();
            if (lista.Count == 0)
                args.IsValid = true;
            else
            {

                foreach (Item oItem in lista)
                {
                    if (Request["id"] != null)
                    {
                        if (int.Parse(Request["id"].ToString()) == oItem.IdItem)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                }
            }

        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "")
            {
                txtDescripcion.Text = txtNombre.Text;
                txtDescripcion.UpdateAfterCallBack = true;
            }
        }

        protected void txtCodigoDiagrama_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigoDiagrama.Text != "")
            {
                Item oItem = new Item();
                ISession m_session = NHibernateHttpModule.CurrentSession;

                ICriteria crit = m_session.CreateCriteria(typeof(Item));

                crit.Add(Expression.Eq("Codigo", txtCodigoDiagrama.Text));
                crit.Add(Expression.Eq("Baja", false));
                //   crit.Add(Expression.Eq("Tipo", "D"));

                oItem = (Item)crit.UniqueResult();
                if (oItem != null)
                {
                    try
                    {
                        ddlItemDiagrama.SelectedValue = oItem.IdItem.ToString();
                        txtNombreDiagrama.Text = oItem.Descripcion;
                        lblMensajeDiagrama.Text = "";
                    }
                    catch
                    {
                        lblMensajeDiagrama.Text = "El codigo ingresado no existe dentro del area";
                        ddlItemDiagrama.SelectedValue = "0";
                        txtNombreDiagrama.Text = "";
                    }
                }
                else
                {
                    lblMensajeDiagrama.Text = "El codigo ingresado no existe";
                    ddlItemDiagrama.SelectedValue = "0";
                    txtNombreDiagrama.Text = "";
                }

                ddlItemDiagrama.UpdateAfterCallBack = true;
                txtNombreDiagrama.UpdateAfterCallBack = true;
                lblMensajeDiagrama.UpdateAfterCallBack = true;
            }
        }

        protected void rdbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarTipoResultados();
        }

        protected void txtCodigo_TextChanged1(object sender, EventArgs e)
        {
            MostrarItemNomenclador();
        }

        private void MostrarItemReferencia()
        {
            ///Si encuentra el codigo ingresado muestra el item en el combo
            ///
            try
            {
                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit = m_session.CreateCriteria(typeof(Item));
                crit.Add(Expression.Eq("Codigo", txtCodigoReferencia.Text));
                crit.Add(Expression.Eq("Baja", false));
                //IList detalle = crit.List();

                Item oItem = (Item)crit.UniqueResult();
                if (oItem != null)
                {
                    ddlItemReferencia.SelectedValue = oItem.IdItem.ToString();
                    // lblValorNomenclador.Text = oItem.ValMod.ToString();
                    //lblMensaje.Visible = false;
                }
                else
                {
                    ddlItemReferencia.SelectedValue = "0";
                    //lblValorNomenclador.Text = "";
                    //if (txtCodigoNomenclador.Text != "")
                    //  lblMensaje.Visible = true;
                }
            }
            catch
            {

            }
            ddlItemReferencia.UpdateAfterCallBack = true;
            //lblValorNomenclador.UpdateAfterCallBack = true;
            //lblMensaje.UpdateAfterCallBack = true;
        }

        private void MostrarItemNomenclador()
        {
            ///Si encuentra el codigo ingresado muestra el item en el combo
            ///
            try
            {
                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit = m_session.CreateCriteria(typeof(Nomenclador));
                crit.Add(Expression.Eq("Codigo", txtCodigoNomenclador.Text));
                //IList detalle = crit.List();

                Nomenclador oItem = (Nomenclador)crit.UniqueResult();
                if (oItem != null)
                {
                    ddlItemNomenclador.SelectedValue = oItem.Codigo;
                    lblValorNomenclador.Text = oItem.ValMod.ToString();
                    lblFactorProduccion.Text = oItem.FactorProduccion.ToString();
                    lblMensaje.Visible = false;
                }
                else
                {
                    ddlItemNomenclador.SelectedValue = "0";
                    lblValorNomenclador.Text = "";
                    lblFactorProduccion.Text = "";
                    if (txtCodigoNomenclador.Text != "")
                        lblMensaje.Visible = true;
                }
            }
            catch
            {

            }
            ddlItemNomenclador.UpdateAfterCallBack = true;
            lblValorNomenclador.UpdateAfterCallBack = true;
            lblFactorProduccion.UpdateAfterCallBack = true;
            lblMensaje.UpdateAfterCallBack = true;
        }



        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        { ///////Con la selección del item se muestra el codigo
            if (ddlItemNomenclador.SelectedValue != "0")
            {
                Nomenclador oItem = new Nomenclador();
                oItem = (Nomenclador)oItem.Get(typeof(Nomenclador), ddlItemNomenclador.SelectedValue);
                txtCodigoNomenclador.Text = oItem.Codigo;
                lblValorNomenclador.Text = oItem.ValMod.ToString();
                lblFactorProduccion.Text = oItem.FactorProduccion.ToString();
            }
            else
            {
                txtCodigoNomenclador.Text = "";
                lblValorNomenclador.Text = "";
                lblFactorProduccion.Text = "";

            }
            txtCodigoNomenclador.UpdateAfterCallBack = true;
            lblValorNomenclador.UpdateAfterCallBack = true;
            lblFactorProduccion.UpdateAfterCallBack = true;

        }

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarArea();
        }

        protected void txtCodigoReferencia_TextChanged(object sender, EventArgs e)
        {
            MostrarItemReferencia();
        }

        protected void ddlItemReferencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarCodigoItemReferencia();
        }

        private void MostrarCodigoItemReferencia()
        {
            if (ddlItemReferencia.SelectedValue != "0")
            {
                Item oItem = new Item();
                oItem = (Item)oItem.Get(typeof(Item), int.Parse(ddlItemReferencia.SelectedValue));
                txtCodigoReferencia.Text = oItem.Codigo;
            }
            //lblValorNomenclador.Text = oItem.ValMod.ToString();
            txtCodigoReferencia.UpdateAfterCallBack = true;
            //lblValorNomenclador.UpdateAfterCallBack = true;
        }

        protected void ddlTipoResultado_SelectedIndexChanged(object sender, EventArgs e)
        {

            HabilitarDecimales();
        }

        private void HabilitarDecimales()
        {
            if (ddlTipoResultado.SelectedValue == "1")
            {
                ddlDecimales.Enabled = true;
                ddlMultiplicador.Enabled = true;
                txtValorMinimo.Enabled = true;
                txtValorMaximo.Enabled = true;
                txtValorDefecto.Width = Unit.Pixel(76);
              
            }
            else
            {
                ddlDecimales.Enabled = false;
                ddlMultiplicador.Enabled = false;
                txtValorMinimo.Text = "";
                txtValorMaximo.Text = "";
                txtValorMinimo.Enabled = false;
                txtValorMaximo.Enabled = false;
                txtValorDefecto.Width = Unit.Pixel(250);
                if (ddlTipoResultado.SelectedValue == "3")  //predefinidos
                {
                    txtValorDefecto.Enabled = false;
                }
               
            }

            ddlDecimales.UpdateAfterCallBack = true;
            ddlMultiplicador.UpdateAfterCallBack = true;
            txtValorMinimo.UpdateAfterCallBack = true;
            txtValorMaximo.UpdateAfterCallBack = true;
            txtValorDefecto.UpdateAfterCallBack = true;
         
        }

        protected void ddlDecimales_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarControlFormato();
        }

        private void HabilitarControlFormato()
        {
            string expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,2}";
            switch (ddlDecimales.SelectedValue)
            {
                case "0": /// entero
                    {
                        expresionControlDecimales = "[-+]?\\d*";
                    }
                    break;
                case "1":
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,1}";
                    } break;
                case "2":
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,2}";
                    } break;
                case "3":
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,3}";
                    } break;
                case "4":
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,4}";
                    } break;
            }

            revValorMinimo.ValidationExpression = expresionControlDecimales;
            revValorMaximo.ValidationExpression = expresionControlDecimales;
            
            revValorMinimo.UpdateAfterCallBack = true;
            revValorMaximo.UpdateAfterCallBack = true;
            if (ddlTipoResultado.SelectedValue == "1") //numerico
            {
                revValorDefecto.ValidationExpression = expresionControlDecimales;
                revValorDefecto.Enabled = true;
              
            }
            else
            {
                revValorDefecto.Enabled = false;

            }

            revValorDefecto.UpdateAfterCallBack = true;
        }



        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            ISession m_session = NHibernateHttpModule.CurrentSession;

            ICriteria crit = m_session.CreateCriteria(typeof(Item));

            crit.Add(Expression.Eq("Codigo", txtCodigo.Text));
            crit.Add(Expression.Eq("Baja", false));


            IList lista = crit.List();
            if (lista.Count != 0)
            {
                lblMensajeCodigo.Text = "El codigo " + txtCodigo.Text + " ya existe. Verifique.";
                lblMensajeCodigo.Visible = true;
                txtCodigo.Text = "";
            }
            else
                lblMensajeCodigo.Visible = false;

            txtCodigo.UpdateAfterCallBack = true;
            lblMensajeCodigo.UpdateAfterCallBack = true;
        }

        protected void lnkRegresar_Click1(object sender, EventArgs e)
        {
            string m_parametroFiltro = "?Codigo=" + Request["Codigo"].ToString() + "&Nombre=" + Request["Nombre"].ToString() + "&Servicio=" +Request["Servicio"].ToString() +
         "&Area=" + Request["Area"].ToString() + "&Orden=" + Request["Orden"].ToString();

            Response.Redirect("ItemList.aspx"+ m_parametroFiltro, false);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            string m_parametroFiltro = "?Codigo=" + Request["Codigo"].ToString() + "&Nombre=" + Request["Nombre"].ToString() + "&Servicio=" + Request["Servicio"].ToString() +
            "&Area=" + Request["Area"].ToString() + "&Orden=" + Request["Orden"].ToString();
            Response.Redirect("ItemEdit2.aspx" + m_parametroFiltro, false);

        }

        protected void btnGuardarRPDefecto_Click(object sender, EventArgs e)
        {
            Item oItem = new Item();
            oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["id"].ToString()));
            
            oItem.ResultadoDefecto = ddlResultadoPorDefecto.SelectedItem.Text;

            oItem.IdResultadoPorDefecto = int.Parse(ddlResultadoPorDefecto.SelectedValue);
            oItem.Save();

                lblMensajeRpD.Text = "El resultado por defecto ha sido guardado";
                lblMensajeRpD.Visible = true;
                lblMensajeRpD.UpdateAfterCallBack = true;

                
           
        }

        protected void btnGuardarHIV_Click(object sender, EventArgs e)
        {
            if (Request["id"] != null)
            {
                Item oItem = new Item();
                oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["id"].ToString()));

                oItem.CodificaHiv = chkCodificaHiv.Checked;
                oItem.LimiteTurnosDia = int.Parse(txtLimite.Value);
                oItem.EtiquetaAdicional = chkEtiquetaAdicional.Checked;
                //oItem.IsScreeening = chkIsScreening.Checked;
                oItem.Save();
            }
        }     
    }
}

