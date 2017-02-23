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
using Business.Data.Laboratorio;
using NHibernate;
using NHibernate.Expression;
using System.Drawing;
using MathParser;

using Business.Data;

namespace WebLab.Resultados
{
    public partial class ResultadoHTEdit : System.Web.UI.Page
    {
       
                                

        protected void Page_PreInit(object sender, EventArgs e)
        {
           
             if (Session["idUsuario"] != null)
                {
                ArmarEncabezado();

                }
             else
                 Response.Redirect("../FinSesion.aspx", false);
         

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    Inicializar();
                   
                }
                else
                    Response.Redirect("../FinSesion.aspx", false);
            }

        }

        private void Inicializar()
        {
            HojaTrabajo oRegistro = new HojaTrabajo();
            oRegistro = (HojaTrabajo)oRegistro.Get(typeof(HojaTrabajo), int.Parse(Request["idHojaTrabajo"].ToString()));
            lblArea.Text = oRegistro.IdArea.Nombre + " - " + oRegistro.Codigo; chkFormula.Visible = false;
            Usuario oUser = new Usuario();
            oUser = (Usuario)oUser.Get(typeof(Usuario), int.Parse(Session["idUsuario"].ToString()));
            //lnkControl.Text = "Marcar hoja como controlada por " + oUser.Nombre + " " + oUser.Apellido;
            switch  (Request["Operacion"].ToString())            
            {
                case "Control":{
                lblTitulo.Text = "CONTROL DE HOJA DE TRABAJO";
                pnlControl.Visible = true; }break;
                case "Carga":
                    {
                        lblTitulo.Text = "CARGA DE RESULTADOS";
                        pnlControl.Visible = false;
                    } break;
                case "Valida":
                    {
                        lblTitulo.Text = "VALIDACION DE RESULTADOS";
                        lblTitulo.CssClass = "mytituloRojo2";
                        btnGuardar.Text = "Validar y Salir";
                        btnGuardarParcial.Text = "Validar";
                        btnValidarPendiente.Visible = true;
                        btnValidarPendienteySalir.Visible = true;
                        pnlControl.Visible = true;
                    } break;
            }
        }

        private void ArmarEncabezado()
        {
            
           // Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
           
             HojaTrabajo oRegistro = new HojaTrabajo();
             oRegistro = (HojaTrabajo)oRegistro.Get(typeof(HojaTrabajo),int.Parse( Request["idHojaTrabajo"].ToString()));

             
            string m_strSQL  =@" SELECT CHT.textoImprimir,CHT.idItem FROM dbo.LAB_DetalleHojaTrabajo AS CHT 
            where CHT.idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() +" order by CHT.idDETALLEHojaTrabajo";
                        
             
                 DataSet Ds = new DataSet();
                 SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
                 SqlDataAdapter adapter = new SqlDataAdapter();
                 adapter.SelectCommand = new SqlCommand(m_strSQL, conn);
                 adapter.Fill(Ds);
     
                 //tContenido0.Controls.Add(objRow0);
                 if (Ds.Tables[0].Rows.Count > 0)
                 {

                     TableRow objRow = new TableRow();                   
                     for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                     {
                         string m_iditem = Ds.Tables[0].Rows[i].ItemArray[1].ToString();
                         Item oItem = new Item(); oItem = (Item)oItem.Get(typeof(Item), int.Parse(m_iditem));                               
                          
                         string s_tipoREsultado= oItem.IdTipoResultado.ToString();
                         string s_nombre =oItem.Nombre;
                        // maximoAnchoTabla += 1;
                         TableCell objCell = new TableCell();
                         Label lbl0 = new Label();
                         switch (s_tipoREsultado)
                         {
                             case "1":
                                 { lbl0.Width = Unit.Pixel(80); objCell.Width = Unit.Pixel(80); } break;
                             case "2":
                                 { lbl0.Width = Unit.Pixel(100); objCell.Width = Unit.Pixel(100); } break;
                             default:
                                 { lbl0.Width = Unit.Pixel(150); objCell.Width = Unit.Pixel(150); } break;
                         }
                         //objCell.Text = Ds.Tables[0].Rows[i].ItemArray[0].ToString().Substring(0, CalcularLargo(Ds.Tables[0].Rows[i].ItemArray[0].ToString().Length));
                         lbl0.Text = Ds.Tables[0].Rows[i].ItemArray[0].ToString().Substring(0, CalcularLargo(Ds.Tables[0].Rows[i].ItemArray[0].ToString().Length));
                         lbl0.Font.Size = FontUnit.Point(8);
                         lbl0.Font.Bold = true;
                         lbl0.ToolTip = s_nombre;
                         //dt.Columns.Add(nombre_Columna);
                         Master.FindControl("ContentPlaceHolder1").FindControl("PanelEncabezado").Controls.Add(lbl0);
                         objCell.Controls.Add(lbl0);
                         objCell.BackColor = Color.WhiteSmoke;
                             
                         objCell.HorizontalAlign = HorizontalAlign.Center;
                         objCell.Height = Unit.Pixel(25);
                         
                        
                        
                         objRow.Cells.Add(objCell);
                         tEncabezado.Controls.Add(objRow);
                         //tContenido.Controls.Add(objRow);
                        
                     }
                     Master.FindControl("ContentPlaceHolder1").FindControl("PanelEncabezado").Controls.Add(tEncabezado);
                     ArmarTabla(oRegistro);
                   
                     
                 }

                 else
                 {
                     PanelEncabezado.Visible = false;
                     tEncabezado.Visible = false;
                     PanelPrimeraColumna.Visible=false;
                     tContenido0.Visible = false;
                     Panel1.Visible = false;
                     tContenido.Visible = false;
                     lblMensaje.Text = "No se encontraron protocolos para los filtros ingresados";
                     lblMensaje.Visible = true;
                     btnGuardar.Visible = false;
                     chkFormula.Visible = false;
                 }

             
        }

        private int CalcularLargo(int p)
        {
            if (p >20) return 20;
            else return p;
        }

        private void ArmarTabla(HojaTrabajo oRegistro)
        {
            //try
            //{
          
                ///Llena la tabla con los datos
                DataSet Ds = new DataSet();
                SqlConnection conn = (SqlConnection)NHibernateHttpModule.CurrentSession.Connection;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                string s_idHoja = Request["idHojaTrabajo"].ToString();
                Configuracion oCon = new Configuracion(); oCon = (Configuracion)oCon.Get(typeof(Configuracion), 1);
                           

                if (oRegistro != null)
                {
                   
            //        int tipo_HojaTrabajo = oRegistro.Formato;


                    //int columna_Control = 0;
                    //if (tipo_HojaTrabajo == 0)// formato prtocolo:filas - determinacion:columna
                    //{
                    //    cmd.CommandText = "LAB_TablaCruzada";
                    //    //columna_Control = 4;
                    //}
                    //else
                    //{/// sin uso este store 
                    //    cmd.CommandText = "LAB_TablaCruzada2";
                    //    //columna_Control = 5;
                    //}
                    cmd.CommandText = "LAB_TablaCruzada";
                    cmd.Parameters.Add("@ListaProtocolos", SqlDbType.NVarChar);
                    cmd.Parameters["@ListaProtocolos"].Value = Session["Parametros"].ToString();
                    cmd.Parameters.Add("@IdHojaTrabajo", SqlDbType.NVarChar);
                    cmd.Parameters["@IdHojaTrabajo"].Value = oRegistro.IdHojaTrabajo.ToString(); // Request["idHojaTrabajo"].ToString();


                    cmd.Connection = conn;


                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(Ds);

                    //GridView1.DataSource = Ds;
                    //GridView1.DataBind();
                   //string validado = "0";
                    bool controlado = false;
                    for (int j = 0; j < Ds.Tables[0].Rows.Count; j++)
                    {

                        string m_nroprotocolo = "";
                        TableRow objRow = new TableRow();
                        //objRow.Height = Unit.Pixel(50);
                        for (int i = 0; i < Ds.Tables[0].Columns.Count; i++)
                        {
                            TableCell objCell = new TableCell();

                            if (i == 0) ///numero del protocolo o nombre del analisis segun sea el caso
                            {
                                TableRow objRow0 = new TableRow();
                              //  objRow0.Height = Unit.Pixel(40);
                                m_nroprotocolo = Ds.Tables[0].Rows[j].ItemArray[i].ToString();

                                Label lbl0 = new Label();
                                lbl0.Text = m_nroprotocolo;
                                lbl0.ID = m_nroprotocolo;
                                lbl0.ToolTip = "Haga clic aquí para ver más información del protocolo";
                                lbl0.Font.Size = FontUnit.Point(8);
                                lbl0.Font.Bold = true;
                                string s_idProtocolo = Ds.Tables[0].Rows[j].ItemArray[1].ToString();
                                
                                lbl0.Attributes.Add("onClick", "javascript: protocoloView (" + s_idProtocolo + "); return false");
                                objCell.Height = Unit.Pixel(40);
                                objCell.BackColor = Color.WhiteSmoke;        
                        


                              
                                if (true)// formato prtocolo:filas - determinacion:columna
                                {
                                    if (Request["Operacion"] != "Carga")
                                         {
                                    controlado = estaControladoProtocolo(Ds.Tables[0].Rows[j].ItemArray[1].ToString(), s_idHoja, Request["Operacion"].ToString());
                                    if (controlado)
                                    {///agrega tilde
                                        //Label oLblEstado = new Label();
                                        if (Request["Operacion"] == "Control")
                                        {
                                            objCell.BackColor = Color.LightGreen;
                                            //lbl0.ForeColor = Color.Green;
                                            lbl0.Font.Bold = true;
                                            lbl0.ToolTip = "Controlado";
                                            //oLblEstado.Text = "C";

                                        //oLblEstado.ForeColor = Color.Green;
                                        }
                                        if (Request["Operacion"] == "Valida")
                                        {
                                            objCell.BackColor = Color.LightBlue;
                                            //lbl0.ForeColor = Color.Blue;
                                            lbl0.Font.Bold = true;
                                            lbl0.ToolTip = "Validado";
                                            //oLblEstado.Text = "V";

                                            //oLblEstado.ForeColor = Color.Blue;
                                        }
                                       

                                    }
                                 

                                    //PanelPrimeraColumna.Width = Unit.Pixel(105);
                                    //tContenido0.Width = Unit.Pixel(105);
                                         }
                                   
                                }
  
                                Master.FindControl("ContentPlaceHolder1").FindControl("PanelPrimeraColumna").Controls.Add(lbl0);
                                objCell.Controls.Add(lbl0);

                                ////////////////////
                                if ((Request["idServicio"].ToString() == "4") && (Request["Operacion"].ToString() == "Valida")) //Pesquisa
                                {
                                    Protocolo oP = new Protocolo();
                                    oP = (Protocolo)oP.Get(typeof(Protocolo), int.Parse(s_idProtocolo));
                                    SolicitudScreening oSolicitud = new SolicitudScreening();
                                    oSolicitud = (Business.Data.Laboratorio.SolicitudScreening)oSolicitud.Get(typeof(Business.Data.Laboratorio.SolicitudScreening), "IdProtocolo", oP);
                                    if (oSolicitud != null)
                                    {
                                        int i_alarmas = oSolicitud.GetCantidadAlarmas();
                                        ImageButton imgPesquisa = new ImageButton();
                                        //imgPesquisa.TabIndex = short.Parse("500");                                    
                                        imgPesquisa.ImageUrl = "~/App_Themes/default/images/pendiente.png";
                                        imgPesquisa.ToolTip = "Tiene " + i_alarmas.ToString() + " alarmas";
                                        imgPesquisa.Attributes.Add("onClick", "javascript: protocoloView (" + s_idProtocolo + "); return false");
                                        if (i_alarmas > 0) imgPesquisa.Visible = true; else imgPesquisa.Visible = false; 
                                        Master.FindControl("ContentPlaceHolder1").FindControl("PanelPrimeraColumna").Controls.Add(imgPesquisa);
                                        objCell.Controls.Add(imgPesquisa);       
                                    }
                                }

                                //////////////////

                              
                                                       

                                objRow0.Cells.Add(objCell);
                              

                                if (Request["control"] != null)
                                {
                                    ////FUNCION DE CONTROL
                                    TableCell objCellControl = new TableCell();
                                    //objCellControl.Width=                                    Unit.Pixel(60);

                                    CheckBox chk1 = new CheckBox();
                                    chk1.ID = "chkc" + Ds.Tables[0].Rows[j].ItemArray[1].ToString();
                                    Master.FindControl("ContentPlaceHolder1").FindControl("PanelPrimeraColumna").Controls.Add(chk1);
                                    objCellControl.BackColor = Color.Beige;
                                    objCellControl.Controls.Add(chk1);
                                    objRow0.Cells.Add(objCellControl);


                                }
                                tContenido0.Controls.Add(objRow0);                                                                                   
                                Master.FindControl("ContentPlaceHolder1").FindControl("PanelPrimeraColumna").Controls.Add(tContenido0);
                            }

                            if (i >= 3)
                            {
                               
                                string m_iditem = Ds.Tables[0].Columns[i].ToString();
                                string m_idprotocolo = Ds.Tables[0].Rows[j].ItemArray[0].ToString();
                                //if (tipo_HojaTrabajo == 0)// formato prtocolo:filas - determinacion:columna
                                //{
                                    m_iditem = Ds.Tables[0].Columns[i].ToString();
                                    m_idprotocolo = Ds.Tables[0].Rows[j].ItemArray[1].ToString();
                                    //m_idprotocolo = Ds.Tables[0].Rows[j].ItemArray[0].ToString();
                                //}
                                //else
                                //{
                                //    m_iditem = Ds.Tables[0].Rows[j].ItemArray[1].ToString();
                                //    m_idprotocolo = Ds.Tables[0].Columns[i].ToString();                               
                                //}

                                string m_valoritem = Ds.Tables[0].Rows[j].ItemArray[i].ToString();
                                string m_idControl = m_idprotocolo + "_" + m_iditem;

                                Item oItem = new Item();
                                oItem = (Item)oItem.Get(typeof(Item), int.Parse(m_iditem));
                                Label oLblXXX = new Label();
                                oLblXXX.Text = "";
                                if (oItem.IdTipoResultado == 1)
                                { objCell.Width = Unit.Pixel(80); oLblXXX.Width = Unit.Pixel(80); }
                                if (oItem.IdTipoResultado == 2)
                                { objCell.Width = Unit.Pixel(100);  oLblXXX.Width= Unit.Pixel(100);}
                                if (oItem.IdTipoResultado > 2)
                                { objCell.Width = Unit.Pixel(150); oLblXXX.Width = Unit.Pixel(150); }

                                objCell.Controls.Add(oLblXXX);
                             
                                if (m_valoritem != "")
                                {
                                   
                                    string nombrePractica =  oItem.Nombre;
                                    Protocolo oP = new Protocolo();
                                    oP = (Protocolo)oP.Get(typeof(Protocolo), int.Parse(m_idprotocolo));
                                    DetalleProtocolo oDet = new DetalleProtocolo();
                                    oDet = (DetalleProtocolo)oDet.Get(typeof(DetalleProtocolo), "IdSubItem", oItem, "IdProtocolo", oP);
                                    //objCell.Height = Unit.Pixel(40);
                                  
                                    ///Antes de mostrar el control verifica  si está derivado                    
                                    if (oItem.IdEfectorDerivacion != oItem.IdEfector) //es derivado
                                    {
                                        Label lblDerivacion = new Label();
                                        lblDerivacion.Text = "Derivado";// +oItem.IdEfectorDerivacion.Nombre; /// Ds.Tables[0].Rows[i].ItemArray[1].ToString();
                                        lblDerivacion.Font.Italic = true;
                                        lblDerivacion.Font.Size = FontUnit.Point(8);
                                        lblDerivacion.ForeColor = Color.Red;
                                        objCell.Controls.Add(lblDerivacion);
                                        //objCell.Controls.Add(lblDerivacion);
                                    }
                                    else
                                    {//No es derivado
                                        
                                        switch (oItem.IdTipoResultado)
                                        {
                                            case 1: ///numerico
                                                {
                                                    Utility oUtil = new Utility();
                                                    TextBox txt1 = new TextBox();
                                                    txt1.ID = m_idControl;

                                                    decimal aux = decimal.Parse( m_valoritem);
                                                    string formato = oUtil.Formato(oItem.FormatoDecimal.ToString());
                                                    decimal x = decimal.Parse(aux.ToString(formato));                                                                                                                                                            

                                                    //decimal x = decimal.Parse(m_valoritem);
                                                    txt1.Text = x.ToString(System.Globalization.CultureInfo.InvariantCulture);                                                    
                                                    txt1.Width = Unit.Pixel(70);
                                                    txt1.CssClass = "myTexto";
                                                 
                                                    txt1.Attributes.Add("onkeypress", "javascript:return Enter(this, event)");
                                        

                                                  
                                                    if (oDet.ConResultado ==false)//no tiene resultado
                                                    {
                                                        if (oItem.ResultadoDefecto != "")
                                                            txt1.Text = oItem.ResultadoDefecto;
                                                        else
                                                            txt1.Text = "";
                                                    }

                                                    if (oDet.IdUsuarioValida > 0) // validado
                                                        txt1.BackColor = Color.LightBlue;
                                                    else
                                                        if (oDet.IdUsuarioControl > 0) // controlado
                                                            txt1.BackColor = Color.LightGreen;
                                                    
                                                    if (Request["Operacion"] == "Control") // control
                                                        {
                                                    if (oDet.IdUsuarioValida > 0)
                                                        txt1.Enabled = false;
                                                        }
                                                    if (Request["Operacion"] == "Carga") // control
                                                    {
                                                        if (oDet.IdUsuarioControl > 0)
                                                            txt1.Enabled = false;
                                                        else
                                                        {
                                                            if (oDet.IdUsuarioValida > 0)
                                                                txt1.Enabled = false;
                                                        }
                                                    }
                                                    //else
                                                    //{
                                                    //    if ((Request["control"] == null) && (oDet.IdUsuarioControl > 0)) // carga
                                                    //    {
                                                    //        txt1.Enabled = false;
                                                    //    }
                                                    //}

                                                    ///Se agrega un validador de solo numeros
                                                    RegularExpressionValidator oValidaNumero = new RegularExpressionValidator();
                                                    oValidaNumero.ValidationExpression = ExpresionFormato(oItem.FormatoDecimal);
                                                    oValidaNumero.ControlToValidate = txt1.ID;
                                                    oValidaNumero.Width = Unit.Pixel(1);
                                                    oValidaNumero.Text = "*";
                                                    oValidaNumero.ValidationGroup = "0";
                                                    txt1.ToolTip = txt1.Text;

                                                    Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").Controls.Add(txt1);
                                                    objCell.Controls.Add(txt1);

                                                    Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").Controls.Add(oValidaNumero);
                                                    //objCell.Controls.AddAt(0,oValidaNumero);
                                                    objCell.Controls.Add(oValidaNumero);

                                                    objCell.Width = Unit.Pixel(80);
                                                }
                                                break;
                                            case 2: //texto
                                                {                                                                                                        
                                                    TextBox txt1 = new TextBox();
                                                    txt1.ID = m_idControl;
                                                    txt1.CssClass = "myTexto";                                                                                                            
                                                    txt1.Width = Unit.Pixel(90);
                                                    txt1.TextMode = TextBoxMode.MultiLine;
                                                    txt1.Rows = 1;
                                                    txt1.MaxLength = 200;
                                                   
                                                
                                                    if (oDet != null)
                                                    {
                                                        if (oDet.ConResultado == false)
                                                        {
                                                            if (oItem.ResultadoDefecto != "")
                                                                txt1.Text = oItem.ResultadoDefecto;
                                                            else
                                                                txt1.Text = "";
                                                        }
                                                        else
                                                        {
                                                            string resNum = oDet.ResultadoNum.ToString();
                                                            if ((oDet.ResultadoCar == "") && (oDet.Enviado == 2) && (oDet.IdUsuarioValida==0) && (oDet.IdUsuarioResultado==0)) // automatico
                                                            {
                                                                if (resNum != "") txt1.Text = resNum.Substring(0, resNum.Length - 2).Replace(",", ".");                                                                
                                                            }
                                                            else
                                                            txt1.Text = oDet.ResultadoCar;
                                                        }
                                                    }

                                                    if (oDet.IdUsuarioValida > 0) // validado
                                                        txt1.BackColor = Color.LightBlue;
                                                    else
                                                        if (oDet.IdUsuarioControl > 0) // controlado
                                                            txt1.BackColor = Color.LightGreen;

                                                    if (Request["Operacion"] == "Control") // control
                                                    {
                                                        if (oDet.IdUsuarioValida > 0)
                                                            txt1.Enabled = false;
                                                    }
                                                    if (Request["Operacion"] == "Carga") // control
                                                    {
                                                        if (oDet.IdUsuarioControl > 0)
                                                            txt1.Enabled = false;
                                                        else
                                                        {
                                                            if (oDet.IdUsuarioValida > 0)
                                                                txt1.Enabled = false;
                                                        }
                                                    }

                                                    txt1.ToolTip = txt1.Text;
                                                    objCell.Controls.Add(txt1);
                                                    objCell.Width = Unit.Pixel(100);

                                                } // fin case 2
                                                break;
                                            case 3: //Lista predefinida
                                                {
                                                   
                                                    //Verifica si la determinacion tiene una lista predeterminada de resultados
                                                    ISession m_session = NHibernateHttpModule.CurrentSession;
                                                    ICriteria crit = m_session.CreateCriteria(typeof(ResultadoItem));
                                                    crit.Add(Expression.Eq("IdItem", oItem));
                                                    crit.Add(Expression.Eq("Baja", false));


                                                    ///Si tiene resultados predeterminados muestra un combo
                                                    IList resultados = crit.List();
                                                    if (resultados.Count > 0)
                                                    {
                                                        DropDownList ddl1 = new DropDownList();
                                                        //ddl1.Font.Size = FontUnit.Point(7);
                                                        ListItem ItemSeleccion = new ListItem();
                                                        ItemSeleccion.Value = "0";
                                                        ItemSeleccion.Text = "";
                                                        
                                                        ddl1.ToolTip = nombrePractica;
                                                        ddl1.Items.Add(ItemSeleccion);
                                                        foreach (ResultadoItem oResultado in resultados)
                                                        {
                                                            ListItem Item = new ListItem();
                                                            Item.Value = oResultado.IdResultadoItem.ToString();
                                                            Item.Text = oResultado.Resultado;
                                                                                                                    
                                                            ddl1.ID = m_idControl;
                                                            ddl1.Items.Add(Item);
                                                            //ddl1.SelectedItem.Text = Ds.Tables[0].Rows[i].ItemArray[4].ToString();
                                                            ddl1.SelectedIndexChanged += new EventHandler(ddl1_SelectedIndexChanged);

                                                        }
                                                      

                                                        if (oDet != null)
                                                        {
                                                            if (oDet.ConResultado == false) // sin resultado
                                                            {
                                                                if (oItem.ResultadoDefecto != "")
                                                                    ddl1.SelectedValue = oItem.IdResultadoPorDefecto.ToString();
                                                                else
                                                                    ddl1.SelectedValue= "0";
                                                            }
                                                            else

                                                                ddl1.SelectedItem.Text = oDet.ResultadoCar;
                                                        }

                                                        if (oDet.IdUsuarioValida > 0) // validado
                                                            ddl1.BackColor = Color.LightBlue;
                                                        else
                                                            if (oDet.IdUsuarioControl > 0) // controlado
                                                                ddl1.BackColor = Color.LightGreen;

                                                        if (Request["Operacion"] == "Control") // control
                                                        {
                                                            if (oDet.IdUsuarioValida > 0)
                                                                ddl1.Enabled = false;
                                                        }
                                                        if (Request["Operacion"] == "Carga") // control
                                                        {
                                                            if (oDet.IdUsuarioControl > 0)
                                                                ddl1.Enabled = false;
                                                            else
                                                            {
                                                                if (oDet.IdUsuarioValida > 0)
                                                                    ddl1.Enabled = false;
                                                            }
                                                        }

                                                        ddl1.Width = Unit.Pixel(150);
                                                        objCell.Controls.Add(ddl1);
                                                        objCell.Width = Unit.Pixel(150);
                                                    }
                                                }
                                                break;
                                            case 4: //Lista predefinida con seleccion multiple (sin seleccion multiple...jiji)
                                                {
                                                    
                                                    //Verifica si la determinacion tiene una lista predeterminada de resultados
                                                    ISession m_session = NHibernateHttpModule.CurrentSession;
                                                    ICriteria crit = m_session.CreateCriteria(typeof(ResultadoItem));
                                                    crit.Add(Expression.Eq("IdItem", oItem));
                                                    crit.Add(Expression.Eq("Baja", false));


                                                    ///Si tiene resultados predeterminados muestra un combo
                                                    IList resultados = crit.List();
                                                    if (resultados.Count > 0)
                                                    {
                                                        DropDownList ddl1 = new DropDownList();
                                                        //ddl1.Font.Size = FontUnit.Point(7);
                                                        ListItem ItemSeleccion = new ListItem();
                                                        ItemSeleccion.Value = "0";
                                                        ItemSeleccion.Text = "";

                                                        ddl1.ToolTip = nombrePractica;
                                                        ddl1.Items.Add(ItemSeleccion);
                                                        foreach (ResultadoItem oResultado in resultados)
                                                        {
                                                            ListItem Item = new ListItem();
                                                            Item.Value = oResultado.IdResultadoItem.ToString();
                                                            Item.Text = oResultado.Resultado;

                                                            ddl1.ID = m_idControl;
                                                            
                                                            ddl1.Items.Add(Item);
                                                            //ddl1.SelectedItem.Text = Ds.Tables[0].Rows[i].ItemArray[4].ToString();
                                                            ddl1.SelectedIndexChanged += new EventHandler(ddl1_SelectedIndexChanged);

                                                        }


                                                        if (oDet != null)
                                                        {
                                                            if (oDet.ConResultado == false) // sin resultado
                                                            {
                                                                if (oItem.ResultadoDefecto != "")
                                                                    ddl1.SelectedValue = oItem.IdResultadoPorDefecto.ToString();
                                                                else
                                                                    ddl1.SelectedValue = "0";
                                                            }
                                                            else

                                                                ddl1.SelectedItem.Text = oDet.ResultadoCar;
                                                        }

                                                        if (oDet.IdUsuarioValida > 0) // validado
                                                            ddl1.BackColor = Color.LightBlue;
                                                        else
                                                            if (oDet.IdUsuarioControl > 0) // controlado
                                                                ddl1.BackColor = Color.LightGreen;

                                                        if (Request["Operacion"] == "Control") // control
                                                        {
                                                            if (oDet.IdUsuarioValida > 0)
                                                                ddl1.Enabled = false;
                                                        }
                                                        if (Request["Operacion"] == "Carga") // control
                                                        {
                                                            if (oDet.IdUsuarioControl > 0)
                                                                ddl1.Enabled = false;
                                                            else
                                                            {
                                                                if (oDet.IdUsuarioValida > 0)
                                                                    ddl1.Enabled = false;
                                                            }
                                                        }

                                                        ddl1.Width= Unit.Pixel(150);
                                                        objCell.Controls.Add(ddl1);
                                                        objCell.Width = Unit.Pixel(150);
                                                    }
                                                }
                                                break;
                                           
                                        }
                                    }

                                }

                                objCell.Height = Unit.Pixel(40);                                
                                objRow.Cells.Add(objCell);
                            }
                        }
                        tContenido.Controls.Add(objRow);                      
                    }
                    Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").Controls.Add(tContenido);                    
                }
            
        }

   

        private string ExpresionFormato(int p)
        {
            string expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,2}";
            switch (p)
            {
                case 0:
                    {
                        expresionControlDecimales = "[-+]?\\d*";                      
                    }
                    break;
                case 1:
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,1}";
                        
                    } break;
                case 2:
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,2}";
                      
                    } break;
                case 3:
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,3}";
                      
                    } break;
                case 4:
                    {
                        expresionControlDecimales = "[-+]?\\d*\\.?\\,?\\d{0,4}";
                      
                    } break;
            }
            return expresionControlDecimales;
        }

     


        void ddl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl1 = (DropDownList)sender;
        }

    
     

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Guardar(true);
                ////Guardar y Salir
                if (Request["control"] != null) // muestra la misma pantalla con los valores controlados y la marca de control.
                    if (Request["Operacion"].ToString()=="Control")
                    Response.Redirect("../ControlResultados/ControlPlanilla.aspx?tipo=ht&idArea=" + Request["idArea"].ToString() + "&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() + "&control=1", false);
                    else
                        Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Valida&modo=Normal", false);
                else /// redirecciona a los filtros de busqueda nuevamente.
                    Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Carga&modo=Normal", false);
                //    Response.Redirect("ResultadoHTEdit.aspx?Parametros=" + Request["Parametros"] + "&idArea=" + Request["idArea"].ToString() + "&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() , false);
                
            }
        }


        protected void btnValidarPendienteySalir_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Guardar(false);
                ////Guardar y Salir
                if (Request["control"] != null) // muestra la misma pantalla con los valores controlados y la marca de control.
                    if (Request["Operacion"].ToString() == "Control")
                        Response.Redirect("../ControlResultados/ControlPlanilla.aspx?tipo=ht&idArea=" + Request["idArea"].ToString() + "&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() + "&control=1", false);
                    else
                        Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Valida&modo=Normal", false);
                else /// redirecciona a los filtros de busqueda nuevamente.
                    Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Carga&modo=Normal", false);
                //    Response.Redirect("ResultadoHTEdit.aspx?Parametros=" + Request["Parametros"] + "&idArea=" + Request["idArea"].ToString() + "&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() , false);

            }
        }


        private void Guardar( bool todo)
        {
            //string m_id = "";

            //Label lbl;
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
                                                                        if (txt.Enabled) GuardarResultado(txt.ID, txt.Text, todo);
                                                                        
                                                                    }

                                                                    if (control5 is DropDownList)
                                                                    {
                                                                        ddl = (DropDownList)control5;
                                                                        if (ddl.Enabled) GuardarResultado(ddl.ID, ddl.SelectedItem.Text, todo);
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

        private void GuardarResultado(string m_idControl, string valorItem, bool todo)
        {
         //   bool b_guardar = true;
            string aux = m_idControl;
            string[] arr = aux.Split(("_").ToCharArray());

            string id_Protocolo = arr[0].ToString();
            string m_idItem = arr[1].ToString();

            //Usuario oUser = new Usuario();
            Protocolo oProtocolo = new Protocolo();
            oProtocolo = (Protocolo)oProtocolo.Get(typeof(Protocolo), int.Parse(id_Protocolo));
            //oProtocolo.CalcularFormulas();
     //       oProtocolo.CalcularFormulas(Request["Operacion"].ToString(), int.Parse(Session["idUsuario"].ToString()));

            //if (oProtocolo.Estado == 0)
            //{
            //    if (oProtocolo.EnProceso())
            //    {
            //        oProtocolo.Estado = 1;//en proceso
            //       // oProtocolo.ActualizarResultados(Request["Operacion"].ToString(), int.Parse(Session["idUsuario"].ToString()));
            //    }
            //}                              


            if (valorItem != "") 
            {

                string s_unidadMedida = ""; //string s_metodo = "";
                Item oItem = new Item();
                oItem = (Item)oItem.Get(typeof(Item), int.Parse(m_idItem));
                if (oItem.IdUnidadMedida>0)
                {
                    UnidadMedida  oUnidad = new UnidadMedida();
                    oUnidad= (UnidadMedida)oUnidad.Get(typeof(UnidadMedida),oItem.IdUnidadMedida);
                    if (oUnidad!=null)  s_unidadMedida= oUnidad.Nombre;
                }

                

                int tiporesultado = oItem.IdTipoResultado;
                


                ISession m_session = NHibernateHttpModule.CurrentSession;
                ICriteria crit = m_session.CreateCriteria(typeof(DetalleProtocolo));
                crit.Add(Expression.Eq("IdSubItem", oItem));
                crit.Add(Expression.Eq("IdProtocolo", oProtocolo));
                if (!todo) crit.Add(Expression.Eq("IdUsuarioValida", 0));
                //crit.Add(Expression.Eq("IdEfector", oProtocolo.IdEfector));

                IList detalle = crit.List();
                if (detalle.Count > 0)
                {
                    foreach (DetalleProtocolo oDetalle in detalle)
                    {
                        string valorReferencia = oDetalle.CalcularValoresReferencia();
                        oDetalle.ValorReferencia = valorReferencia;
                        switch (tiporesultado)
                        {
                            case 1:// numerico 
                                if (valorItem != "")
                                {
                                    oDetalle.UnidadMedida = s_unidadMedida;
                                    oDetalle.ResultadoNum = decimal.Parse(valorItem, System.Globalization.CultureInfo.InvariantCulture);
                                    oDetalle.FormatoValida = oItem.FormatoDecimal;
                                    oDetalle.ConResultado = true;
                                }
                                else
                                {
                                    oDetalle.ResultadoNum = 0;
                                    oDetalle.ConResultado = false;
                                }
                                break;
                            default:
                                {
                                    oDetalle.UnidadMedida = s_unidadMedida;
                                    oDetalle.ResultadoCar = valorItem;
                                    oDetalle.ConResultado = true;
                                } break;
                            
                        }

                        if (Request["control"] ==null) //no es control
                        {
                            if (oDetalle.ConResultado)
                            {
                                oDetalle.IdUsuarioResultado = int.Parse(Session["idUsuario"].ToString());
                                oDetalle.FechaResultado = DateTime.Now;                                
                            }
                            
                            oDetalle.Save();
                            if (oDetalle.ConResultado) oDetalle.GrabarAuditoriaDetalleProtocolo("Alta", int.Parse(Session["idUsuario"].ToString()));
                           
                        }
                        if (Request["control"] != null) // es control
                        {

                            HojaTrabajo oRegistro = new HojaTrabajo();
                            oRegistro = (HojaTrabajo)oRegistro.Get(typeof(HojaTrabajo), int.Parse(Request["idHojaTrabajo"].ToString()));
                            int tipo_HojaTrabajo = oRegistro.Formato;
                            string s_pivot = "";
                            if (tipo_HojaTrabajo == 0)  //por protocolo
                            { s_pivot = id_Protocolo; }
                            else
                            { s_pivot = m_idItem; }

                            if (oDetalle.ConResultado)
                            {
                                if (estaTildado(s_pivot))
                                {
                                    if (!oDetalle.ConResultado)
                                    {
                                        oDetalle.ConResultado = true;
                                        oDetalle.IdUsuarioResultado = int.Parse(Session["idUsuario"].ToString());
                                        oDetalle.FechaResultado = DateTime.Now;
                                        oDetalle.Save();
                                    }
                                    if (Request["Operacion"].ToString() == "Valida")   //Validacion
                                    {
                                        if (oDetalle.ConResultado)
                                        {
                                            oDetalle.IdUsuarioValida = int.Parse(Session["idUsuarioValida"].ToString());
                                            oDetalle.FechaValida = DateTime.Now;

      

                                            oDetalle.Save();
                                            if (oDetalle.ConResultado) oDetalle.GrabarAuditoriaDetalleProtocolo(Request["Operacion"].ToString(), int.Parse(Session["idUsuarioValida"].ToString()));
                                        }
                                    }
                                    if (Request["Operacion"].ToString() == "Control")   //Control
                                    {
                                        oDetalle.IdUsuarioControl = int.Parse(Session["idUsuario"].ToString());
                                        oDetalle.FechaControl = DateTime.Now;
                                        oDetalle.Save();
                                        oDetalle.GrabarAuditoriaDetalleProtocolo("Control", int.Parse(Session["idUsuario"].ToString()));
                                    }
                                }
                            }
                        }                                                                                                          
                    }
                
                if (Request["Operacion"].ToString() != "Valida")
                {
                    if (oProtocolo.Estado == 0)
                    {
                        if (oProtocolo.EnProceso())
                        {
                            oProtocolo.Estado = 1;//en proceso
                            // oProtocolo.ActualizarResultados(Request["Operacion"].ToString(), int.Parse(Session["idUsuario"].ToString()));
                        }
                    }
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
        protected void lnkMarcarControl_Click(object sender, EventArgs e)
        {
          //  Marcar(true);
        }

        protected void lnkDesmarcarControl_Click(object sender, EventArgs e)
        {
          //  Marcar(false );
        }  
        private bool estaTildado(string m_id)
        {
            string nombre_control =  "chkc" + m_id;            
            
            Control control1 = Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").FindControl(nombre_control);
            CheckBox chk = (CheckBox)control1;
            if (chk != null)
            {
                if (chk.Checked) return true;
                else return false;
            }
            else
                return false;
        }

  

        protected void cvValidaControles_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ValidaControles())
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void lnkRegresar_Click(object sender, EventArgs e)
        {
            if (Request["control"]!=null)

                  if (Request["Operacion"].ToString()=="Control")
                    Response.Redirect("../ControlResultados/ControlPlanilla.aspx?tipo=ht&idArea=" + Request["idArea"].ToString() + "&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() + "&control=1", false);
                    else
                        Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Valida&modo=Normal", false);
                 //Response.Redirect("../ControlResultados/ControlPlanilla.aspx?tipo=ht", false); 
            else

                Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Carga&modo=Normal", false);
        }

        private bool ValidaControles()
        {
            bool valida = true;
        //    string m_id = "";

          //  Label lbl;
            TextBox txt;
         //   DropDownList ddl;


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
                                                                          

                                                                            valida = ValidarValor(txt.ID, txt.Text);

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
            string aux = m_idControl;
            string[] arr = aux.Split(("_").ToCharArray());

            string id_Protocolo = arr[0].ToString();
            string m_idItem = arr[1].ToString();


            bool control = true;

            Item oItem= new Item();
            oItem= (Item)oItem.Get(typeof(Item), int.Parse( m_idItem));

            if ((oItem.Multiplicador > 1) && (valorItem != ""))
            {
               valorItem= AplicarMultiplicador(m_idControl, oItem);
            }

            if (!oItem.VerificaValoresMinimosMaximos(valorItem)) 
            { cvValidaControles.ErrorMessage = "Error de validación en " + oItem.Nombre; control = false; return control; }

          
            return control;
        }

        private string AplicarMultiplicador(string m_idControl,   Item oItem)
        {
            string aux = m_idControl;
            string[] arr = aux.Split(("_").ToCharArray());

            string id_Protocolo = arr[0].ToString();
        //    string m_idItem = arr[1].ToString();
            string valorItem = "0";

            Protocolo oProtocolo = new Protocolo();
            oProtocolo = (Protocolo)oProtocolo.Get(typeof(Protocolo), int.Parse(id_Protocolo));


            ISession m_session = NHibernateHttpModule.CurrentSession;

            ICriteria crit2 = m_session.CreateCriteria(typeof(DetalleProtocolo));
            crit2.Add(Expression.Eq("IdSubItem", oItem));
            crit2.Add(Expression.Eq("IdProtocolo", oProtocolo));
//            crit2.Add(Expression.Eq("IdEfector", oProtocolo.IdEfector));

            IList detalleProtocolo = crit2.List();
            if (detalleProtocolo.Count > 0)
            {
                foreach (DetalleProtocolo oDetalle in detalleProtocolo)
                {
                    decimal valorActual = Math.Round(oDetalle.ResultadoNum, oDetalle.IdSubItem.FormatoDecimal);
                    valorItem =valorActual.ToString(System.Globalization.CultureInfo.InvariantCulture) ;
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
            }
            return valorItem;            
             
        }

        private bool VerificaValoresMinimosMaximos(Item oItem)
        {
            bool devolver = true;
            try
            {
                if (oItem.IdTipoResultado == 1) //solo para los numericos
                {
                    decimal m_minimo = oItem.ValorMinimo;
                    decimal m_maximo = oItem.ValorMaximo;
                    if ((m_minimo != -1) && (m_maximo != -1))  //si tiene valores minimos y maximos predefinidos
                    {

                        Control control1 = Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").FindControl(oItem.IdItem.ToString());
                        TextBox txt = txt = (TextBox)control1;
                        decimal valor = decimal.Parse(txt.Text);
                        if (valor < m_minimo)
                            devolver = false;
                        if (valor > m_maximo)
                            devolver = false;
                    }
                }
            }
            catch { devolver = false; }

            return devolver;
        }

       

       


        private bool estaControladoProtocolo(string m_idprotocolo, string m_idhoja, string operacion)
        {
            bool controlado = true;
         

            Protocolo oProtocolo = new Protocolo();
            oProtocolo = (Protocolo)oProtocolo.Get(typeof(Protocolo), int.Parse(m_idprotocolo));

            HojaTrabajo oHojaTrabajo = new HojaTrabajo();
            oHojaTrabajo = (HojaTrabajo)oHojaTrabajo.Get(typeof(HojaTrabajo), int.Parse(m_idhoja));

            ISession m_session = NHibernateHttpModule.CurrentSession;



            ICriteria crit = m_session.CreateCriteria(typeof(DetalleHojaTrabajo));
            crit.Add(Expression.Eq("IdHojaTrabajo", oHojaTrabajo));

            IList detalle = crit.List();
            if (detalle.Count > 0)
            {
                foreach (DetalleHojaTrabajo oDetalle in detalle)
                {

                    /////////////
                    ICriteria crit2 = m_session.CreateCriteria(typeof(DetalleProtocolo));
                    crit2.Add(Expression.Eq("IdSubItem", oDetalle.IdItem));
                    crit2.Add(Expression.Eq("IdProtocolo", oProtocolo));
                    crit2.Add(Expression.Eq("IdEfector", oProtocolo.IdEfector));

                    IList detalleProtocolo = crit2.List();
                    if (detalleProtocolo.Count > 0)
                    {
                        foreach (DetalleProtocolo oDetalleProtocolo in detalleProtocolo)
                        {
                            if (operacion=="Control"){
                                if (oDetalleProtocolo.IdUsuarioControl == 0)
                                {
                                    controlado = false;
                                    break;
                                }
                            }
                            if (operacion == "Valida")
                            {
                                if (oDetalleProtocolo.IdUsuarioValida == 0)
                                {
                                    controlado = false;
                                    break;
                                }
                            }
                            

                        }
                    }
                    /////
                    if (!controlado) break;

                }
            }




            return controlado;
        }


        protected void btnValidarPendiente_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Guardar(false);
                ////Guarda y se queda en la pantalla
                if (Request["control"] != null) // muestra la misma pantalla con los valores controlados y la marca de control.
                    Response.Redirect("ResultadoHTEdit.aspx?idArea=" + Request["idArea"].ToString() + "&Operacion=" + Request["Operacion"].ToString() + "&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() + "&control=1&idServicio=" + Request["idServicio"].ToString(), false);
                else /// redirecciona a los filtros de busqueda nuevamente.
                    //    Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Carga&modo=Normal", false);
                    Response.Redirect("ResultadoHTEdit.aspx?idArea=" + Request["idArea"].ToString() + "&Operacion=Carga&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() + "&idServicio=" + Request["idServicio"].ToString(), false);


                //Guardar();
                //////Guardar y Salir
                //if (Request["control"] != null) // muestra la misma pantalla con los valores controlados y la marca de control.
                //    if (Request["Operacion"].ToString() == "Control")
                //        Response.Redirect("../ControlResultados/ControlPlanilla.aspx?tipo=ht&idArea=" + Request["idArea"].ToString() + "&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() + "&control=1", false);
                //    else
                //        Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Valida&modo=Normal", false);
                //else /// redirecciona a los filtros de busqueda nuevamente.
                //    Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Carga&modo=Normal", false);
            }

        }

        protected void btnGuardarParcial_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Guardar(true);            
                ////Guarda y se queda en la pantalla
                if (Request["control"] != null) // muestra la misma pantalla con los valores controlados y la marca de control.
                    Response.Redirect("ResultadoHTEdit.aspx?idArea=" + Request["idArea"].ToString()  +"&Operacion=" + Request["Operacion"].ToString() + "&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() + "&control=1&idServicio=" + Request["idServicio"].ToString(), false);
                else /// redirecciona a los filtros de busqueda nuevamente.
                    //    Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Carga&modo=Normal", false);
                    Response.Redirect("ResultadoHTEdit.aspx?idArea=" + Request["idArea"].ToString()  + "&Operacion=Carga&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString()+"&idServicio=" + Request["idServicio"].ToString(), false);


                //Guardar();
                //////Guardar y Salir
                //if (Request["control"] != null) // muestra la misma pantalla con los valores controlados y la marca de control.
                //    if (Request["Operacion"].ToString() == "Control")
                //        Response.Redirect("../ControlResultados/ControlPlanilla.aspx?tipo=ht&idArea=" + Request["idArea"].ToString() + "&idHojaTrabajo=" + Request["idHojaTrabajo"].ToString() + "&control=1", false);
                //    else
                //        Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Valida&modo=Normal", false);
                //else /// redirecciona a los filtros de busqueda nuevamente.
                //    Response.Redirect("ResultadoBusqueda.aspx?idServicio=" + Request["idServicio"].ToString() + "&Operacion=Carga&modo=Normal", false);
            }

        }

    }

    



      
}
