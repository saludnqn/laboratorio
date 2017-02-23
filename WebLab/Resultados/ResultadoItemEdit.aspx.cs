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
using System.Data.SqlClient;
using Business.Data.Laboratorio;
using NHibernate;
using NHibernate.Expression;
using System.Drawing;

namespace WebLab.Resultados
{
    public partial class ResultadoItemEdit : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["idUsuario"] != null)
            {
                if (Request["idItem"] != null)
                {
                    LlenarTabla(Request["idItem"].ToString());
                }
            }
            else Response.Redirect("../FinSesion.aspx", false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                  if (Session["idUsuario"] != null) Inicializar();                
                  else Response.Redirect("../FinSesion.aspx", false);
            }

        }

        private void Inicializar()
        {
            hypRegresar.NavigateUrl = "ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Carga&modo=" + Request["modo"].ToString();
            if (Request["idItem"] != null)
            {
                Item oItem = new Item();
                oItem = (Item)oItem.Get(typeof(Item), int.Parse(Request["idItem"].ToString()));
                lblItem.Text = oItem.Codigo + "  -  " + oItem.Nombre;
                //if (oItem.IdTipoResultado == 4)
                //    lblMensaje.Text = "Para ampliar la selección de carga de resultados acceder por Lista de Protocolos";
                //else lblMensaje.Text = "";
            }

            switch (Request["Operacion"].ToString())
            {
                case "Carga":
                    {                                                
                        hypRegresar.NavigateUrl = "ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=" + Request["Operacion"].ToString() + "&modo=" + Request["modo"].ToString();
                        lblTitulo.Text = "CARGA DE RESULTADOS";
                        lnkMarcar.Visible = false;
                        lnkDesmarcar.Visible = false;                                                
                    }
                    break;             

                case "Valida":
                    {                        
                        hypRegresar.NavigateUrl = "ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=" + Request["Operacion"].ToString() + "&modo=" + Request["modo"].ToString();
                        lblTitulo.Text = "VALIDACION DE RESULTADOS";
                        lblTitulo.CssClass = "mytituloRojo2";
                        btnGuardar.Text = "Validar";
                        btnValidarPendiente.Visible = true;
                        lnkMarcar.Visible = true;
                        lnkDesmarcar.Visible = true;                                                
                    }
                    break;
               
            }
        }

        protected void lnkMarcar_Click(object sender, EventArgs e)
        {

         //   Marcar(true);

        }

        

        protected void lnkDesmarcar_Click(object sender, EventArgs e)
        {
          //  Marcar(false);
        }

        private void LlenarTabla(string p)
        {
            Item oItem = new Item();
            oItem = (Item)oItem.Get(typeof(Item), int.Parse(p));

            
            bool hiv= oItem.CodificaHiv;

            DataTable dt = getDataSet(oItem.IdItem.ToString());
            if (dt.Rows.Count > 0)
            {
                //lblCantidadRegistros.Text = dt.Rows.Count.ToString() + " protocolos encontrados ";

                TableRow objFila_TITULO = new TableRow();
                TableCell objCellProtocolo_TITULO = new TableCell();
                TableCell objCellFecha_TITULO = new TableCell();
                TableCell objCellPaciente_TITULO = new TableCell();
                TableCell objCellResultado_TITULO = new TableCell();
                TableCell objCellResultadoAnterior_TITULO = new TableCell();

                TableCell objCellReferencia_TITULO = new TableCell();
                TableCell objCellPersona_TITULO = new TableCell(); TableCell objCellValida_TITULO = new TableCell();

                TableCell objCellObservaciones_TITULO = new TableCell();

                Label lblTituloProtocolo = new Label();
                lblTituloProtocolo.Text = "PROTOCOLO";
                objCellProtocolo_TITULO.Controls.Add(lblTituloProtocolo);


                Label lblTituloFecha = new Label();
                lblTituloFecha.Text = "FECHA";
                objCellFecha_TITULO.Controls.Add(lblTituloFecha);

                Label lblTituloPaciente = new Label();
                lblTituloPaciente.Text = "PACIENTE";
                objCellPaciente_TITULO.Controls.Add(lblTituloPaciente);

                Label lblTituloResultado = new Label();
                lblTituloResultado.Text = "RESULTADO";
                objCellResultado_TITULO.Controls.Add(lblTituloResultado);                              
                
                Label lblTituloObservacionesResultado = new Label();
                lblTituloObservacionesResultado.Text = "OBS.";
                objCellObservaciones_TITULO.Controls.Add(lblTituloObservacionesResultado);

                Label lblResultadoAnterior = new Label();
                lblResultadoAnterior.Text = "R.ANTER.";
                objCellResultadoAnterior_TITULO.Controls.Add(lblResultadoAnterior);

                Label lblTituloReferencia = new Label();
                lblTituloReferencia.Text = "REFERENCIA|METODO";
                objCellReferencia_TITULO.Controls.Add(lblTituloReferencia);

                Label lblCargadoPor = new Label();
                lblCargadoPor.Text = "ESTADO";
                objCellPersona_TITULO.Controls.Add(lblCargadoPor);


                Label lblValida = new Label();

                if (Request["Operacion"].ToString() == "Valida")
                        lblValida.Text = "VAL";
                
                objCellValida_TITULO.Controls.Add(lblValida);
                objFila_TITULO.Cells.Add(objCellProtocolo_TITULO);
                objFila_TITULO.Cells.Add(objCellFecha_TITULO);
                objFila_TITULO.Cells.Add(objCellPaciente_TITULO);
                objFila_TITULO.Cells.Add(objCellResultado_TITULO);
                
                if (Request["Operacion"].ToString() == "Valida") objFila_TITULO.Cells.Add(objCellResultadoAnterior_TITULO);
                objFila_TITULO.Cells.Add(objCellReferencia_TITULO);
                objFila_TITULO.Cells.Add(objCellPersona_TITULO);
                

                if (Request["Operacion"].ToString() == "Valida") 
                {                
                    objFila_TITULO.Cells.Add(objCellValida_TITULO);
                }

                objFila_TITULO.Cells.Add(objCellObservaciones_TITULO);
                objFila_TITULO.CssClass = "myLabelIzquierda";
                objFila_TITULO.BackColor = Color.Gainsboro; //.DarkBlue;// "#F2F2FF";

                Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").Controls.Add(tContenido);

                tContenido.Controls.Add(objFila_TITULO);//.Rows.Add(objRow);    

                
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                
                    //decimal m_minimoReferencia = -1;
                    //decimal m_maximoReferencia = -1;
                    string s_valorReferencia = dt.Rows[i].ItemArray[9].ToString();
                    //string s_paciente = dt.Rows[i].ItemArray[15].ToString() + " - " + dt.Rows[i].ItemArray[0].ToString().ToUpper();                    
                    

                    //string s_fecha = dt.Rows[i].ItemArray[1].ToString();
                    
                    string s_idDetalleProtocolo = dt.Rows[i].ItemArray[3].ToString();
                    DetalleProtocolo oDetalle = new DetalleProtocolo();
                    oDetalle = (DetalleProtocolo)oDetalle.Get(typeof(DetalleProtocolo),int.Parse( s_idDetalleProtocolo));

                    string s_idProtocolo = oDetalle.IdProtocolo.ToString();
                    string s_fecha = oDetalle.IdProtocolo.Fecha.ToShortDateString();
                   // dbo.NumeroProtocolo(P.idProtocolo) + case when P.numeroOrigen<>'' then '-' +P.numeroOrigen else '' end
                    
                    string s_numero = oDetalle.IdProtocolo.GetNumero() ; // dt.Rows[i].ItemArray[2].ToString();
                    string s_numeroOrigen=oDetalle.IdProtocolo.NumeroOrigen;
                    if ( s_numeroOrigen!="")   s_numero = s_numero+ "-"+ s_numeroOrigen;


                    string numerodocumento ="";
                    if (oDetalle.IdProtocolo.IdPaciente.IdEstado==2)  numerodocumento="(Temporal)";
                    else numerodocumento = oDetalle.IdProtocolo.IdPaciente.NumeroDocumento.ToString();
                    

                     string apellido = oDetalle.IdProtocolo.IdPaciente.Apellido.ToUpper();
                    string nombre = oDetalle.IdProtocolo.IdPaciente.Nombre.ToUpper();
                    string s_paciente = "";

                    if (hiv) //datosPaciente = " upper(P.sexo+substring(Pac.nombre,1,2)+SUBSTRING(Pac.apellido, 1,2)+REPLACE ( CONVERT(varchar(10), Pac.fechaNacimiento,103),'/','')) ";
                        s_paciente = oDetalle.IdProtocolo.getCodificaHiv(""); // oDetalle.IdProtocolo.IdPaciente.getSexo().Substring(0, 1) + nombre.Substring(0, 2) + apellido.Substring(0, 2) + oDetalle.IdProtocolo.IdPaciente.FechaNacimiento.ToShortDateString().Replace("/", "");

                    else
                        if (oDetalle.IdProtocolo.IdTipoServicio.IdTipoServicio == 4)
                            s_paciente= oDetalle.IdProtocolo.getDatosParentesco();
                        else
                        s_paciente = numerodocumento.ToUpper() + " - " + apellido.ToUpper() + " " + nombre.ToUpper();


                    string s_embarazada = oDetalle.IdProtocolo.GetDiagnostico();
                    string m_formato0 = dt.Rows[i].ItemArray[4].ToString();
                    string m_formato1 = dt.Rows[i].ItemArray[5].ToString();
                    string m_formato2 = dt.Rows[i].ItemArray[6].ToString();
                    string m_formato3 = dt.Rows[i].ItemArray[7].ToString();
                    string m_formato4 = dt.Rows[i].ItemArray[8].ToString();
                    int estado= 0;
                    
                    if (oDetalle.IdUsuarioValida>0)
                        estado=2;

                    if (oDetalle.IdUsuarioControl>0)
                        estado=2;
                    //if (dt.Rows[i].ItemArray[9].ToString() != "") m_minimoReferencia = decimal.Parse(dt.Rows[i].ItemArray[9].ToString().Replace(".", ","));
                    //if (dt.Rows[i].ItemArray[10].ToString() != "") m_maximoReferencia = decimal.Parse(dt.Rows[i].ItemArray[10].ToString().Replace(".", ","));
                    string m_observacionReferencia = dt.Rows[i].ItemArray[16].ToString();

                    string m_tipoValorReferencia = dt.Rows[i].ItemArray[12].ToString();
                    string m_metodo = dt.Rows[i].ItemArray[11].ToString();

                    //string s_referencia = dt.Rows[i].ItemArray[10].ToString();
                    string s_resultadoCar = dt.Rows[i].ItemArray[13].ToString();
                    string s_conResultado = dt.Rows[i].ItemArray[14].ToString();

                    if (s_conResultado == "False") { s_conResultado = "0"; }
                    else { s_conResultado = "1"; }


                    string s_Validado = "0";
                    string s_usuario = "";

                    if (dt.Rows[i].ItemArray[18].ToString() != "")//  usuario valida                    
                    {
                        s_usuario = "Val.: " + dt.Rows[i].ItemArray[18].ToString();
                        s_Validado = "2";
                    }
                    else
                    {
                        if (dt.Rows[i].ItemArray[19].ToString() != "")//  usuario control                    
                        {
                            s_usuario = "Ctrl: " + dt.Rows[i].ItemArray[19].ToString();
                            s_Validado = "1";
                        }
                        else
                        {
                            if (dt.Rows[i].ItemArray[17].ToString() != "")
                                s_usuario = "Carg.: " + dt.Rows[i].ItemArray[17].ToString(); //usuario de carga
                            else
                                s_usuario = "";
                        }
                    }


                    TableRow objFila = new TableRow();
                    TableCell objCellProtocolo = new TableCell();
                    TableCell objCellFecha = new TableCell();
                    TableCell objCellPaciente = new TableCell();
                    TableCell objCellResultado = new TableCell();
                    TableCell objCellReferencia = new TableCell();
                    TableCell objCellValida = new TableCell();
                    TableCell objCellPersona = new TableCell();
                    TableCell objCellObservaciones = new TableCell();
                    TableCell objCellResultadoAnterior = new TableCell();

                    Label olblProtocolo = new Label();
                    olblProtocolo.Font.Name = "Arial";
                    olblProtocolo.Font.Size = FontUnit.Point(8);
                    olblProtocolo.Text = s_numero;
                    olblProtocolo.Font.Bold = true;
                    //olblProtocolo.ToolTip = "Haga clic aqui para ver mas información del protocolo";
                    //olblProtocolo.Attributes.Add("onClick", "javascript: protocoloView (" + s_idProtocolo + "); return false");
                    objCellProtocolo.BackColor = Color.Beige;
                    objCellProtocolo.Controls.Add(olblProtocolo);

                    Label olblFecha = new Label();                  
                    olblFecha.Text = s_fecha;
                    olblFecha.Font.Name = "Arial";
                    olblFecha.Font.Size = FontUnit.Point(8);
                    objCellFecha.Controls.Add(olblFecha);


                    Label olblPaciente = new Label();
                    olblPaciente.Font.Name = "Arial";
                    olblPaciente.Font.Size = FontUnit.Point(8);
                    olblPaciente.Text = s_paciente;

                    objCellPaciente.Controls.Add(olblPaciente);


                  
                    if (s_embarazada != "")
                    {
                        Label olblEmbarazo = new Label();
                        olblEmbarazo.Font.Name = "Arial";
                        olblEmbarazo.Font.Size = FontUnit.Point(7);
                        olblEmbarazo.ForeColor = Color.Red;
                        olblEmbarazo.Text = "&nbsp;" + s_embarazada;
                        objCellPaciente.Controls.Add(olblEmbarazo);
                    }

                    if (Request["Operacion"].ToString() == "Valida") /// Solo en la validacion
                    {
                        ImageButton btnAddDiagnostico = new ImageButton();
                        btnAddDiagnostico.TabIndex = short.Parse("500");
                        //btnAddDetalle.AutoUpdateAfterCallBack = true;
                        btnAddDiagnostico.ID = "d" + s_idDetalleProtocolo;
                        btnAddDiagnostico.ToolTip = "Agregar/quitar Diagnostico del paciente.";
                        btnAddDiagnostico.ImageUrl = "~/App_Themes/default/images/add.png";
                        //btnObservacionDetalle2.Attributes.Add("onClick", "javascript: ObservacionEdit (" + oDetalle.IdDetalleProtocolo.ToString() + "," + oDetalle.IdProtocolo.IdTipoServicio.IdTipoServicio.ToString() + ",'" + Request["Operacion"].ToString() + "'); return false");
                        btnAddDiagnostico.Attributes.Add("onClick", "javascript: editDiagnostico (" + oDetalle.IdProtocolo.IdProtocolo.ToString() + "); return false");
                        //objCellPaciente.Controls.Add(btnAddDiagnostico);
                        objCellProtocolo.Controls.Add(btnAddDiagnostico);
                    }
                    decimal x = 0;



                    switch (oItem.IdTipoResultado)
                    {//tipoResultado

                        case 4://Lista predefinida de resultados con seleccion multiple ( sin seleccion muktiple...jeje).
                            {
                                ///   Verifica si la determinacion tiene una lista predeterminada de resultados
                                //ISession m_session = NHibernateHttpModule.CurrentSession;
                                //ICriteria crit = m_session.CreateCriteria(typeof(ResultadoItem));
                                //crit.Add(Expression.Eq("IdItem", oItem));
                                //crit.Add(Expression.Eq("Baja", false));

                                /////Si tiene resultados predeterminados muestra un combo
                                //IList resultados = crit.List();
                                //if (resultados.Count > 0)
                                //{
                                //    DropDownList ddl1 = new DropDownList();
                                //    ddl1.ID = s_idDetalleProtocolo;

                                //    ListItem ItemSeleccion = new ListItem();
                                //    ItemSeleccion.Value = "0";
                                //    ItemSeleccion.Text = "";
                                //    ddl1.Items.Add(ItemSeleccion);


                                //    foreach (ResultadoItem oResultado in resultados)
                                //    {
                                //        ListItem Item = new ListItem();
                                //        Item.Value = oResultado.IdResultadoItem.ToString();
                                //        Item.Text = oResultado.Resultado;
                                //        ddl1.Items.Add(Item);
                                //    }
                                //    if (s_conResultado == "0")// sin resultado
                                //        ddl1.SelectedValue = oItem.IdResultadoPorDefecto.ToString();
                                //    else
                                //        ddl1.SelectedItem.Text = s_resultadoCar;

                                    
                                    //ddl1.SelectedIndexChanged += new EventHandler(ddl1_SelectedIndexChanged);
                                    //objCellResultado.Controls.Add(ddl1);
                                //}


                                TextBox txt1 = new TextBox();
                                txt1.ID = s_idDetalleProtocolo;
                                txt1.TabIndex = short.Parse(i + 1.ToString());
                                txt1.Text = s_resultadoCar;
                                txt1.TextMode = TextBoxMode.MultiLine;
                                txt1.Width = Unit.Percentage(95);
                                txt1.Rows = 2;
                                txt1.MaxLength = 200;
                                txt1.ToolTip = s_resultadoCar;
                                //txt1.CssClass = "myTexto";

                                ImageButton btnAddDetalle = new ImageButton();
                                btnAddDetalle.TabIndex = short.Parse("500");
                                //btnAddDetalle.AutoUpdateAfterCallBack = true;
                                btnAddDetalle.ID = "b" + s_idDetalleProtocolo;
                                btnAddDetalle.ToolTip = "Desplegar opciones";
                                btnAddDetalle.ImageUrl = "~/App_Themes/default/images/add.png";
                                //btnObservacionDetalle2.Attributes.Add("onClick", "javascript: ObservacionEdit (" + oDetalle.IdDetalleProtocolo.ToString() + "," + oDetalle.IdProtocolo.IdTipoServicio.IdTipoServicio.ToString() + ",'" + Request["Operacion"].ToString() + "'); return false");
                                btnAddDetalle.Attributes.Add("onClick", "javascript: PredefinidoSelect (" + oDetalle.IdDetalleProtocolo.ToString() + ",'" + Request["Operacion"].ToString() + "'); return false");
                                //btnAddDetalle.Click += new ImageClickEventHandler(btnAddDetalle_Click);

                                if (s_Validado != "0")
                                {
                                    txt1.BackColor = Color.LightBlue;
                                    if (Request["Operacion"].ToString() == "Carga")
                                    {
                                        txt1.Enabled = false;// btnAddDetalle.Enabled = false; 
                                    }
                                }



                                objCellResultado.Controls.Add(txt1);
                                objCellResultado.Controls.Add(btnAddDetalle);

                                //////Otra forma de observacion
                                //ImageButton btnObservacionDetalle2 = new ImageButton();
                                //btnObservacionDetalle2.TabIndex = short.Parse("500");

                                ////btnObservacionDetalle2.ID = "Obs2|" + oDetalle.IdDetalleProtocolo.ToString() + "|" + m_estadoObservacion.ToString();//  m_idItem.ToString();

                                //if (oDetalle.Observaciones != "")//tiene observaciones
                                //{

                                //    if (oDetalle.IdUsuarioValidaObservacion == 0)
                                //        btnObservacionDetalle2.ImageUrl = "~/App_Themes/default/images/obs_cargado.png";
                                //    else
                                //        btnObservacionDetalle2.ImageUrl = "~/App_Themes/default/images/obs_validado.png";
                                //}
                                //else
                                //{

                                //    btnObservacionDetalle2.ImageUrl = "~/App_Themes/default/images/obs_normal.png";
                                //}

                                //btnObservacionDetalle2.AlternateText = oDetalle.Observaciones;
                                ////  btnObservacionDetalle2.ToolTip = "Observaciones para " + lbl1.Text.Replace("&nbsp;", "");
                                //btnObservacionDetalle2.Attributes.Add("onClick", "javascript: ObservacionEdit (" + oDetalle.IdDetalleProtocolo.ToString() + "," + oDetalle.IdProtocolo.IdTipoServicio.IdTipoServicio.ToString() + ",'" + Request["Operacion"].ToString() + "'); return false");

                                //objCellObservaciones.Controls.Add(btnObservacionDetalle2);

                                ////////////////////                                        

                            } //fin case 4

                            break;

                        case 3://Lista predefinida de resultados
                            {
                                ///   Verifica si la determinacion tiene una lista predeterminada de resultados
                                ISession m_session = NHibernateHttpModule.CurrentSession;
                                ICriteria crit = m_session.CreateCriteria(typeof(ResultadoItem));
                                crit.Add(Expression.Eq("IdItem", oItem));
                                crit.Add(Expression.Eq("Baja", false));

                                ///Si tiene resultados predeterminados muestra un combo
                                IList resultados = crit.List();
                                if (resultados.Count > 0)
                                {
                                    DropDownList ddl1 = new DropDownList();
                                    ddl1.ID = s_idDetalleProtocolo;

                                    ListItem ItemSeleccion = new ListItem();
                                    ItemSeleccion.Value = "0";
                                    ItemSeleccion.Text = "";
                                    ddl1.Items.Add(ItemSeleccion);

                             
                                    foreach (ResultadoItem oResultado in resultados)
                                    {
                                        ListItem Item = new ListItem();
                                        Item.Value = oResultado.IdResultadoItem.ToString();
                                        Item.Text = oResultado.Resultado;
                                        ddl1.Items.Add(Item);
                                    }
                                    if (s_conResultado == "0")// sin resultado
                                        ddl1.SelectedValue = oItem.IdResultadoPorDefecto.ToString();
                                    else
                                        ddl1.SelectedItem.Text = s_resultadoCar;

                                    if (s_Validado != "0")
                                    {
                                        ddl1.BackColor = Color.LightBlue;
                                        if (Request["Operacion"].ToString() == "Carga")
                                        { ddl1.Enabled = false; }
                                    }



                                    ddl1.SelectedIndexChanged += new EventHandler(ddl1_SelectedIndexChanged);
                                    objCellResultado.Controls.Add(ddl1);
                                }

                                /////////////////Resultado Anterior
                                if (Request["Operacion"].ToString() == "Valida")
                                {
                                    string resultadoAnterior = oDetalle.BuscarResultadoAnterior(oDetalle.IdSubItem, oDetalle.IdItem, true);
                                    if (resultadoAnterior != "")
                                    {
                                        Label olblResultadoAnterior = new Label();
                                        olblResultadoAnterior.Font.Size = FontUnit.Point(8);
                                        //olblResultadoAnterior.Font.Bold = true;
                                        olblResultadoAnterior.ForeColor = Color.Green;
                                        olblResultadoAnterior.Width = Unit.Pixel(20);
                                        olblResultadoAnterior.Text = resultadoAnterior;
                                        olblResultadoAnterior.ToolTip = "Haga clic aquí para ver más datos.";
                                        olblResultadoAnterior.Attributes.Add("onClick", "javascript: AntecedenteAnalisisView (" + oDetalle.IdSubItem.IdItem.ToString() + "," + oDetalle.IdProtocolo.IdPaciente.IdPaciente.ToString() + ",790,200); return false");
                                        
                                        objCellResultadoAnterior.Controls.Add(olblResultadoAnterior);

                                    }
                                }
                                //////////////////////
                                ////Otra forma de observacion
                                ImageButton btnObservacionDetalle2 = new ImageButton();
                                btnObservacionDetalle2.TabIndex = short.Parse("500");

                                btnObservacionDetalle2.ID = "Obs2|" + oDetalle.IdDetalleProtocolo.ToString(); // +"|" + m_estadoObservacion.ToString();//  m_idItem.ToString();

                                if (oDetalle.Observaciones != "")//tiene observaciones
                                {

                                    if (oDetalle.IdUsuarioValidaObservacion == 0)
                                        btnObservacionDetalle2.ImageUrl = "~/App_Themes/default/images/obs_cargado.png";
                                    else
                                        btnObservacionDetalle2.ImageUrl = "~/App_Themes/default/images/obs_validado.png";
                                }
                                else
                                {

                                    btnObservacionDetalle2.ImageUrl = "~/App_Themes/default/images/obs_normal.png";
                                }

                                btnObservacionDetalle2.AlternateText = oDetalle.Observaciones;
                                //  btnObservacionDetalle2.ToolTip = "Observaciones para " + lbl1.Text.Replace("&nbsp;", "");
                                btnObservacionDetalle2.Attributes.Add("onClick", "javascript: ObservacionEdit (" + oDetalle.IdDetalleProtocolo.ToString() + "," + oDetalle.IdProtocolo.IdTipoServicio.IdTipoServicio.ToString() + ",'" + Request["Operacion"].ToString() + "'); return false");

                                objCellObservaciones.Controls.Add(btnObservacionDetalle2);

                                ////////////////////                                        

                            } //fin case 3
                            break;

                        case 1: //numerico
                            {
                                string expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,2}";
                                switch (oItem.FormatoDecimal.ToString())
                                {
                                    case "0":
                                        {
                                            expresionControlDecimales = "[-+]?\\d*";
                                            x = decimal.Parse(m_formato0);
                                        }
                                        break;
                                    case "1":
                                        {
                                            expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,1}";
                                            x = decimal.Parse(m_formato1);
                                        } break;
                                    case "2":
                                        {
                                            expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,2}";
                                            x = decimal.Parse(m_formato2);
                                        } break;
                                    case "3":
                                        {
                                            expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,3}";
                                            x = decimal.Parse(m_formato3);
                                        } break;
                                    case "4":
                                        {
                                            expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,4}";
                                            x = decimal.Parse(m_formato4);
                                        } break;
                                }

                                TextBox txt1 = new TextBox();
                                txt1.ID = s_idDetalleProtocolo;
                                if (s_conResultado == "0")// sin resultado
                                    txt1.Text = oItem.ResultadoDefecto;
                                else
                                    txt1.Text = x.ToString(System.Globalization.CultureInfo.InvariantCulture);

                                //    txt1.Text = val.Replace(".", "");
                                txt1.Width = Unit.Pixel(60);
                                txt1.CssClass = "myTexto";
                                txt1.Attributes.Add("onkeypress", "javascript:return Enter(this, event)");

                                if (s_Validado != "0")
                                {
                                    txt1.BackColor = Color.LightBlue;
                                    if (Request["Operacion"].ToString() == "Carga")
                                    { txt1.Enabled = false; }
                                }



                                objCellResultado.Controls.Add(txt1);

                                RegularExpressionValidator oValidaNumero = new RegularExpressionValidator();
                                oValidaNumero.ValidationExpression = expresionControlDecimales;
                                oValidaNumero.ControlToValidate = txt1.ID;
                                oValidaNumero.Text = "Formato incorrecto";
                                oValidaNumero.ValidationGroup = "0";

                                objCellResultado.Controls.Add(oValidaNumero);

                                /////////////////Resultado Anterior
                                if (Request["Operacion"].ToString() == "Valida")
                                {
                                    string resultadoAnterior = oDetalle.BuscarResultadoAnterior(oDetalle.IdSubItem, oDetalle.IdItem,true);
                                    if (resultadoAnterior != "")
                                    {
                                        Label olblResultadoAnterior = new Label();
                                        olblResultadoAnterior.Font.Size = FontUnit.Point(8);
                                        //olblResultadoAnterior.Font.Bold = true;
                                        olblResultadoAnterior.ForeColor = Color.Green;
                                        olblResultadoAnterior.Width = Unit.Pixel(20);
                                        olblResultadoAnterior.Text = resultadoAnterior;
                                        olblResultadoAnterior.ToolTip = "Haga clic aquí para ver gráfico de evolución.";

                                        olblResultadoAnterior.Attributes.Add("onClick", "javascript: AntecedenteAnalisisView (" + oDetalle.IdSubItem.IdItem.ToString() + "," + oDetalle.IdProtocolo.IdPaciente.IdPaciente.ToString() + ",790,420); return false");                                                                                                              
                                     
                                        objCellResultadoAnterior.Controls.Add(olblResultadoAnterior);
                                   
                                     
                                    }
                                }
                                //////////////////////


                                ////Otra forma de observacion
                                ImageButton btnObservacionDetalle2 = new ImageButton();
                                btnObservacionDetalle2.TabIndex = short.Parse("500");

                                btnObservacionDetalle2.ID = "Obs2|" + oDetalle.IdDetalleProtocolo.ToString();// +"|" + m_estadoObservacion.ToString();//  m_idItem.ToString();

                                if (oDetalle.Observaciones != "")//tiene observaciones
                                {

                                    if (oDetalle.IdUsuarioValidaObservacion == 0)
                                        btnObservacionDetalle2.ImageUrl = "~/App_Themes/default/images/obs_cargado.png";
                                    else
                                        btnObservacionDetalle2.ImageUrl = "~/App_Themes/default/images/obs_validado.png";
                                }
                                else
                                {

                                    btnObservacionDetalle2.ImageUrl = "~/App_Themes/default/images/obs_normal.png";
                                }

                                btnObservacionDetalle2.AlternateText = oDetalle.Observaciones;
                              //  btnObservacionDetalle2.ToolTip = "Observaciones para " + lbl1.Text.Replace("&nbsp;", "");
                                btnObservacionDetalle2.Attributes.Add("onClick", "javascript: ObservacionEdit (" + oDetalle.IdDetalleProtocolo.ToString() + "," + oDetalle.IdProtocolo.IdTipoServicio.IdTipoServicio.ToString() + ",'" + Request["Operacion"].ToString() + "'); return false");

                                objCellObservaciones.Controls.Add(btnObservacionDetalle2);

                                ////////////////////                                        

                                

                            }
                            break;
                        case 2: //texto
                            {
                                TextBox txt1 = new TextBox();
                                txt1.ID = s_idDetalleProtocolo;
                                if (s_conResultado == "0")// sin resultado
                                    txt1.Text = oItem.ResultadoDefecto;
                                else
                                {
                                    if (s_resultadoCar == "")
                                    {
                                        string resNum = oDetalle.ResultadoNum.ToString();
                                        if ((s_resultadoCar == "") && (oDetalle.Enviado == 2) && (oDetalle.IdUsuarioValida == 0) && (oDetalle.IdUsuarioResultado == 0)) // automatico
                                        {
                                            if (resNum != "") txt1.Text = resNum.Substring(0, resNum.Length - 2).Replace(",", ".");
                                        }
                                        else
                                            if (oDetalle.Enviado == 2) { if (oDetalle.Observaciones != "") txt1.Text = oDetalle.Observaciones; }
                                    }
                                    else
                                        txt1.Text = s_resultadoCar;
                                }
                                txt1.TextMode = TextBoxMode.MultiLine;
                                txt1.Width = Unit.Percentage(80);
                                txt1.Rows = 1;
                                txt1.MaxLength = 200;
                                txt1.CssClass = "myTexto";


                                if (s_Validado != "0")
                                {
                                    txt1.BackColor = Color.LightBlue;
                                    if (Request["Operacion"].ToString() == "Carga")
                                    { txt1.Enabled = false; }
                                }

                                Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").Controls.Add(txt1);

                                objCellResultado.Controls.Add(txt1);


                                /////////////////Resultado Anterior
                                if (Request["Operacion"].ToString() == "Valida")
                                {
                                    string resultadoAnterior = oDetalle.BuscarResultadoAnterior(oDetalle.IdSubItem, oDetalle.IdItem, true);
                                    if (resultadoAnterior != "")
                                    {
                                        Label olblResultadoAnterior = new Label();
                                        olblResultadoAnterior.Font.Size = FontUnit.Point(8);
                                        //olblResultadoAnterior.Font.Bold = true;
                                        olblResultadoAnterior.ForeColor = Color.Green;
                                        olblResultadoAnterior.Width = Unit.Pixel(20);
                                        olblResultadoAnterior.Text = resultadoAnterior;
                                        olblResultadoAnterior.ToolTip = "Haga clic aquí para ver más datos.";
                                        olblResultadoAnterior.Attributes.Add("onClick", "javascript: AntecedenteAnalisisView (" + oDetalle.IdSubItem.IdItem.ToString() + "," + oDetalle.IdProtocolo.IdPaciente.IdPaciente.ToString() + ",790,200); return false");
                                   
                                        objCellResultadoAnterior.Controls.Add(olblResultadoAnterior);

                                    }
                                }
                                //////////////////////

                            } // fin case 1
                            break;

                    }//fin swicth




                    Label lblValoresReferencia = new Label();

                    lblValoresReferencia.ID = "VR" + s_idDetalleProtocolo.ToString();
                    lblValoresReferencia.Font.Name = "Arial";
                    lblValoresReferencia.Font.Size = FontUnit.Point(8);
                    lblValoresReferencia.Font.Italic = true;
                    
                    
                    if (s_valorReferencia != "")
                    {
                        lblValoresReferencia.Text = s_valorReferencia;
                        if (m_metodo != "")
                            lblValoresReferencia.Text += " | " + m_metodo;
                    }
                    else
                        lblValoresReferencia.Text = oDetalle.CalcularValoresReferencia();

                 

                    objCellReferencia.Controls.Add(lblValoresReferencia);


                    Label lblPersona = new Label();
                    lblPersona.Text = s_usuario;
                    lblPersona.Font.Size = FontUnit.Point(7);
                    lblPersona.Font.Italic = true;
                    
                    if (s_Validado == "2") lblPersona.ForeColor = Color.Blue; //VALIDADO
                    if (s_Validado == "1") lblPersona.ForeColor = Color.Green; ///CONTROLADO


                    if (Request["Operacion"].ToString() == "Valida") 
                    {
                        CheckBox chk1 = new CheckBox();
                        chk1.ID = "chk" + s_idDetalleProtocolo;
                        if ((estado == 2) && (Request["Operacion"].ToString() == "Carga")) //si esta validado y entro a controlar no puedo modificar
                        {
                            chk1.Visible = false;

                        }
                        objCellValida.Controls.Add(chk1);
                    }



                    objCellPersona.Controls.Add(lblPersona);
                   

                    ///Definir los anchos de las columnas
                    objCellProtocolo.Width = Unit.Percentage(10);
                    objCellFecha.Width = Unit.Percentage(5);
                    objCellPaciente.Width = Unit.Percentage(35);
                    objCellResultado.Width = Unit.Percentage(20);

                    objCellReferencia.Width = Unit.Percentage(20);
                    objCellPersona.Width = Unit.Percentage(10);



                    ///////////////////////
                    ///agrega a la fila cada una de las celdas

                    objFila.Cells.Add(objCellProtocolo);
                    objFila.Cells.Add(objCellFecha);
                    objFila.Cells.Add(objCellPaciente);
                    objFila.Cells.Add(objCellResultado);
                   
                    if (Request["Operacion"].ToString() == "Valida")  objFila.Cells.Add(objCellResultadoAnterior);                
                    objFila.Cells.Add(objCellReferencia);
                    //objFila.Cells.Add(objCellValoresReferencia);
                    objFila.Cells.Add(objCellPersona); if (Request["Operacion"].ToString() == "Valida") objFila.Cells.Add(objCellValida);
                    //objFila.Cells.Add(objCellValida);

                    objFila.Cells.Add(objCellObservaciones);

                    //////

                    Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").Controls.Add(tContenido);

                    //'añadimos la fila a la tabla
                    if (objFila != null)
                        tContenido.Controls.Add(objFila);//.Rows.Add(objRow);                                
                }
            }
           

        }


        protected void btnActualizarPracticas_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResultadoItemEdit.aspx?idServicio=" + Request["idServicio"].ToString() + "&idItem=" + Request["idItem"].ToString() + "&modo=" + Request["modo"].ToString() + "&Operacion=" + Request["Operacion"].ToString(), false);
            //Avanzar(0);
            //SetSelectedTab(TabIndex.DEFAULT);
            ////ActualizarVistaAntibiograma(ddlPracticaAtb.SelectedValue);



        }
        private DataTable getDataSet( string idItem)
        {
            string s_listaProtocolos = Session["Parametros"].ToString();
                              
            string m_strSQL = " SELECT '' as paciente, '' as fecha , " +
                              " DP.idProtocolo AS numero , DP.idDetalleProtocolo, " +
                              " DP.formato0, DP.formato1, DP.formato2, DP.formato3, DP.formato4, " +
                              " DP.ValorReferencia, '' as MaximoReferencia, DP.Metodo as metodo, '' as tipoValorReferencia, DP.resultadoCar, " +
                              " DP.conResultado, '' as numeroDocumento,  '' as observacionReferencia,  DP.userCarga, DP.userValida , DP.userControl " +                              
                              " FROM vta_LAB_Resultados AS DP " +
                              " WHERE  iditem= " + idItem + " and idProtocolo in (" + s_listaProtocolos + ")"+
                              " ORDER BY DP.iddetalleprotocolo" ;
                
            DataSet Ds = new DataSet();
            SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
            adapter.Fill(Ds);
            return Ds.Tables[0];
        }

        void ddl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl1 = (DropDownList)sender;
        }

        protected void cvValidaControles_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ValidaControles())
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        private bool ValidaControles()
        {
            bool valida = true;
         //   string m_id = "";

       //     Label lbl;
            TextBox txt;
        //    DropDownList ddl;


            if (Page.Master != null)
            {
                foreach (Control control in Page.Master.Controls)
                {
                    if (control is HtmlForm)
                    {
                        foreach (Control controlform in control.Controls)
                        {
                            if (controlform is ContentPlaceHolder)
                            {
                                foreach (Control control1 in controlform.Controls)
                                {
                                    if (control1 is Panel)
                                        foreach (Control control2 in control1.Controls)
                                        {
                                            if (control2 is Table)
                                                foreach (Control control3 in control2.Controls)
                                                {

                                                    if (control3 is TableRow)
                                                        foreach (Control control4 in control3.Controls)
                                                        {

                                                            if (control4 is TableCell)
                                                                foreach (Control control5 in control4.Controls)
                                                                {
                                                                    if (control5 is TextBox)
                                                                    {
                                                                        txt = (TextBox)control5;
                                                                        if (txt.Enabled)
                                                                        {
                                                                            if (Request["Operacion"].ToString() == "Valida")
                                                                            {
                                                                                if (estaTildado(txt.ID))
                                                                                {
                                                                                    if (txt.ID.Substring(0, 3) != "OBS")
                                                                                        valida = ValidarValor(txt.ID, txt.Text);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (txt.ID.Substring(0, 3) != "OBS") valida = ValidarValor(txt.ID, txt.Text);
                                                                            }
                                                                            if (!valida) { return false; }
                                                                        }
                                                                    }

                                                                  
                                                                }
                                                        }
                                                }
                                        }
                                }
                            }
                        }
                    }
                }
            }
            return valida;


        }

        private bool ValidarValor(string m_idControl, string valorItem)
        {
           bool control = true;
          
            

            DetalleProtocolo oDetalle = new DetalleProtocolo();
            oDetalle = (DetalleProtocolo)oDetalle.Get(typeof(DetalleProtocolo), int.Parse(m_idControl));

            if ((oDetalle.IdSubItem.Multiplicador > 1) && (valorItem != ""))
            {
              //  valorItem = AplicarMultiplicador(m_idControl, oItem);

                decimal valorActual = Math.Round(oDetalle.ResultadoNum, oDetalle.IdSubItem.FormatoDecimal);
                valorItem = valorActual.ToString(System.Globalization.CultureInfo.InvariantCulture);
                Control control1 = Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").FindControl(m_idControl);
                TextBox txt = txt = (TextBox)control1;
                if (txt != null)
                {
                    if (txt.Text != valorActual.ToString(System.Globalization.CultureInfo.InvariantCulture))  // si no tiene resultados 
                    {
                        decimal resultadoNumerico = decimal.Parse(txt.Text, System.Globalization.CultureInfo.InvariantCulture) * oDetalle.IdSubItem.Multiplicador;
                        txt.Text = resultadoNumerico.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        valorItem = txt.Text;
                    }
                }
            }


            if (oDetalle.AnalizarLimites(lblIdValorFueraLimite1.Text))
            {
                if (!oDetalle.IdSubItem.VerificaValoresMinimosMaximos(valorItem))
                {
                    cvValidaControles.ErrorMessage = "Error de valor fuera de límite en protocolo " + oDetalle.IdProtocolo.GetNumero();
                    lblIdValorFueraLimite.Text = oDetalle.IdDetalleProtocolo.ToString();
                    btnAceptarValorFueraLimite.Visible = true;
                    control = false; return control;
                }
            }

            return control;
        }

        protected void btnAceptarValorFueraLimite_Click(object sender, EventArgs e)
        {
            lblIdValorFueraLimite1.Text += "," + lblIdValorFueraLimite.Text;

        }



        protected void btnValidarPendiente_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Guardar(false);
                Response.Redirect("ResultadoItemEdit.aspx?idServicio=" + Request["idServicio"].ToString() + "&idItem=" + Request["idItem"].ToString() + "&modo=" + Request["modo"].ToString() + "&Operacion=" + Request["Operacion"].ToString(), false);
            }  
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Guardar(true);
                Response.Redirect("ResultadoItemEdit.aspx?idServicio=" + Request["idServicio"].ToString() + "&idItem=" + Request["idItem"].ToString() + "&modo=" + Request["modo"].ToString() + "&Operacion=" + Request["Operacion"].ToString(), false);
            }  
        }

        private void Guardar(bool todo)
        {


         //   string m_id = "";

            TextBox txt;
            DropDownList ddl;

            if (Page.Master != null)
            {
                foreach (Control control in Page.Master.Controls)
                {
                    if (control is HtmlForm)
                    {
                        foreach (Control controlform in control.Controls)
                        {
                            if (controlform is ContentPlaceHolder)
                            {
                                foreach (Control control1 in controlform.Controls)
                                {
                                    if (control1 is Panel)
                                        foreach (Control control2 in control1.Controls)
                                        {
                                            if (control2 is Table)
                                                foreach (Control control3 in control2.Controls)
                                                {

                                                    if (control3 is TableRow)
                                                        foreach (Control control4 in control3.Controls)
                                                        {

                                                            if (control4 is TableCell)
                                                                foreach (Control control5 in control4.Controls)
                                                                {

                                                                    if (control5 is TextBox)
                                                                    {
                                                                        txt = (TextBox)control5;
                                                                        if (txt.Enabled)
                                                                        {

                                                                            if (Request["Operacion"].ToString() == "Valida")
                                                                            {
                                                                                if (estaTildado(txt.ID))
                                                                                {
                                                                                    if (txt.ID.Substring(0, 3) != "OBS") GuardarResultado(txt.ID, txt.Text, todo);
                                                                                }
                                                                            }
                                                                            else{
                                                                                if (txt.ID.Substring(0, 3) != "OBS") GuardarResultado(txt.ID, txt.Text,todo);
                                                                            }
                                                                        }
                                                                    }

                                                                    if (control5 is DropDownList)
                                                                    {
                                                                        ddl = (DropDownList)control5;
                                                                        if (ddl.Enabled)
                                                                        {
                                                                            if (Request["Operacion"].ToString() == "Valida")
                                                                            {
                                                                                if (estaTildado(ddl.ID))
                                                                                {
                                                                                    GuardarResultado(ddl.ID, ddl.SelectedItem.Text, todo);
                                                                                }
                                                                            }
                                                                            else
                                                                                GuardarResultado(ddl.ID, ddl.SelectedItem.Text,todo);
                                                                        }
                                                                    }
                                                                }
                                                        }
                                                }
                                        }
                                }
                            }
                        }
                    }
                }
            }

         

          
        }

        private bool estaTildado(string m_idItem)
        {

            string nombre_control = "chk" + m_idItem;
            Control control1 = Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").FindControl(nombre_control);
            CheckBox chk = (CheckBox)control1;
            if (chk != null)
            {
                if (chk.Checked)
                {
                    if (Session["tildados"] == "")
                        Session["tildados"] = m_idItem;
                    else
                        Session["tildados"] += "," + m_idItem;
                    return true;
                }
                else return false;
            }
            else
                return false;
        }



        private void GuardarResultado(string m_idDetalleProtocolo, string valorItem, bool todo)
        {
            DetalleProtocolo oDetalle = new DetalleProtocolo();
            if (!todo)
                oDetalle = (DetalleProtocolo)oDetalle.Get(typeof(DetalleProtocolo), "IdDetalleProtocolo",int.Parse(m_idDetalleProtocolo), "IdUsuarioValida",0);// crit.Add(Expression.Eq("IdUsuarioValida", 0));
            else
                oDetalle = (DetalleProtocolo)oDetalle.Get(typeof(DetalleProtocolo), int.Parse(m_idDetalleProtocolo));

            if (oDetalle!=null)
            {
            
                int tiporesultado = oDetalle.IdSubItem.IdTipoResultado;
                switch (tiporesultado)
                        {
                            case 1:// numerico         
                                if (valorItem != "")
                                {
                                    oDetalle.ResultadoNum = decimal.Parse(valorItem, System.Globalization.CultureInfo.InvariantCulture);
                                    oDetalle.FormatoValida = oDetalle.IdSubItem.FormatoDecimal;
                                    oDetalle.ConResultado = true;
                                }
                                else
                                {
                                    oDetalle.ResultadoNum = 0;
                                    oDetalle.ConResultado = false;
                                }
                                break;
                            default:
                                if (valorItem != "")
                                {
                                    oDetalle.ResultadoCar = valorItem;
                                    oDetalle.ConResultado = true;
                                }else
                                {
                                    oDetalle.ResultadoCar = "";
                                    oDetalle.ConResultado = false;
                                }
                                break;
                        }

                string m_metodo = "";
                string m_valorReferencia = "";
                string nombre_control = "VR" + oDetalle.IdDetalleProtocolo.ToString();
                Control control1 = Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").FindControl(nombre_control);
                Label valorRef = (Label)control1;


                if (valorRef != null)
                {
                    string[] arr = valorRef.Text.Split(("|").ToCharArray());
                    switch (arr.Length)
                    {
                        case 1: m_valorReferencia = arr[0].Trim().ToString(); break;
                        case 2:
                            {
                                m_valorReferencia = arr[0].Trim().ToString();
                                m_metodo = arr[1].Trim().ToString();
                            } break;
                    }
                    oDetalle.Metodo = m_metodo;
                    oDetalle.ValorReferencia = m_valorReferencia;
                }

                string s_unidadMedida = "";
                int i_unidadMedida = oDetalle.IdSubItem.IdUnidadMedida;
                if (i_unidadMedida > 0)
                {
                    UnidadMedida oUnidad = new UnidadMedida();
                    oUnidad = (UnidadMedida)oUnidad.Get(typeof(UnidadMedida), i_unidadMedida);
                    s_unidadMedida = oUnidad.Nombre;
                }

                oDetalle.UnidadMedida = s_unidadMedida;
                oDetalle.Metodo = m_metodo;
                oDetalle.ValorReferencia = m_valorReferencia;

                if (oDetalle.ConResultado)
                {
                    oDetalle.IdUsuarioResultado = int.Parse(Session["idUsuario"].ToString());
                    oDetalle.FechaResultado = DateTime.Now;
                }

                if ((Request["Operacion"].ToString() == "Valida") && (oDetalle.ConResultado))  //Validacion
                {
                     oDetalle.IdUsuarioValida = int.Parse(Session["idUsuarioValida"].ToString());
                     oDetalle.FechaValida = DateTime.Now;                    
                }
                

                oDetalle.Save();


                if (oDetalle.ConResultado) oDetalle.GrabarAuditoriaDetalleProtocolo(Request["Operacion"].ToString(), int.Parse(Session["idUsuario"].ToString()));

                Protocolo oProtocolo = new Protocolo();
                oProtocolo = oDetalle.IdProtocolo;

                if (Request["Operacion"].ToString() != "Valida")
                {
                    if (oProtocolo.Estado == 0)                    
                        oProtocolo.Estado = 1;                        //oProtocolo.Save();                    
                }
                else //Validacion
                {
                    if (oProtocolo.ValidadoTotal(Request["Operacion"].ToString(), int.Parse(Session["idUsuario"].ToString())))
                        oProtocolo.Estado = 2;  //validado total (cerrado);                    
                    else
                        oProtocolo.Estado = 1;
                }
                oProtocolo.Save();
               
            }



        }
    }
}
