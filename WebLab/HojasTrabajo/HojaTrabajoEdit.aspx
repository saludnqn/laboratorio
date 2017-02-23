<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HojaTrabajoEdit.aspx.cs" Inherits="WebLab.HojasTrabajo.HojaTrabajoEdit" MasterPageFile="~/Site1.Master" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %> 

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<title>LABORATORIO</title>
<link href="../script/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
 <link href="../App_Themes/default/style.css" rel="stylesheet" type="text/css" />  
     <script src="../script/jquery.min.js" type="text/javascript"></script>  
                  <script src="../script/jquery-ui.min.js" type="text/javascript"></script> 
    <link rel="stylesheet" type="text/css" href ="../script/moverfilas/moverfilas.css" />
<script type="text/javascript" src="../script/moverfilas/codigo.js"></script>
<script type="text/javascript">
    $(function() {

                 $("#tabContainer").tabs();
                        var currTab = $("#<%= HFCurrTabIndex.ClientID %>").val();
                      
                        $("#tabContainer").tabs({ selected: currTab });
             });
</script>
  

    <style type="text/css">
        .style1
        {
            font-size: 10pt;
            font-family: Calibri;
            background-color: #FFFFFF;
            color: #333333;
            font-weight: bold;
            width: 189px;
        }
        .style2
        {
            width: 501px;
        }
    </style>
  

</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">       
<br />   &nbsp;
     <asp:HiddenField runat="server" ID="HFCurrTabIndex"   /> 

    <div align="left" style="width:1000px">
  
<table align="center" width="700px">
<tr>
					<td><b class="mytituloTabla">CONFIGURACION HOJA DE TRABAJO</b></td>
						
					<td align="right"> <a href="../Help/Documentos/Hoja de Trabajo.htm" target="_blank"  > 
                                <asp:RadioButtonList ID="rdbHojaTrabajo" runat="server" 
                                    ToolTip="Seleccione el tipo de hoja  de trabajo" 
                                    TabIndex="3" CssClass="myLabel" RepeatDirection="Horizontal" 
                            Visible="False">
                                    <asp:ListItem Selected="True" Value="0">Por Protocolos</asp:ListItem>
                                    <asp:ListItem Value="1">Por Análisis</asp:ListItem>
                                </asp:RadioButtonList>
                        <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
						
					</tr>
<tr>
						<td class="myLabelIzquierda" colspan="2" >
                                          <hr class="hrTitulo" /></td>
						
					</tr>
<tr>
						<td class="myLabelIzquierda" colspan="2" >
						  <div id="tabContainer" style="border: 0px solid #C0C0C0">
                                             <ul class="myLabel">
    <li><a href="#tab1">Hoja de Trabajo</a></li>       
    <li><a href="#tab2">Opciones de Impresión</a></li>
    <li><a href="#tab3">Detalle de la hoja</a></li>
</ul>


    <div id="tab1" class="tab_content" style="border: 1px solid #C0C0C0">
        <table style="width: 700px;" class="myLabelIzquierda">
            <tr>
               <td class="myLabelIzquierda" >
                                            Tipo de Servicio:</td>
						<td>
                           <anthem:DropDownList ID="ddlServicio" runat="server" 
                                ToolTip="Seleccione el servicio" TabIndex="1" CssClass="myList" 
                                AutoCallBack="True" onselectedindexchanged="ddlServicio_SelectedIndexChanged">
                            </anthem:DropDownList></td>
               
            </tr>
            <tr>
                <td class="myLabelIzquierda" >
                                            Area/Sector:</td>
						<td>
                            <anthem:DropDownList ID="ddlArea" runat="server" AutoCallBack="True" 
                                CssClass="myList" 
                                style="height: 22px" TabIndex="2" ToolTip="Seleccione el area" 
                                onselectedindexchanged="ddlArea_SelectedIndexChanged">
                            </anthem:DropDownList>
                            <asp:RangeValidator ID="rvArea" runat="server" ControlToValidate="ddlArea" 
                                ErrorMessage="Area" MaximumValue="999999" MinimumValue="1" Type="Integer" 
                                ValidationGroup="0">*</asp:RangeValidator>
                        </td>
               
            </tr>
            <tr>
               	<td class="myLabelIzquierda" >
                                            Codigo Hoja de Trabajo:</td>
						<td>
                                            <asp:TextBox ID="txtCodigoHT" runat="server" CssClass="myTexto" Width="264px" 
                                                MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCodigoHT" runat="server" 
                                                ControlToValidate="txtCodigoHT" ErrorMessage="Codigo Hoja de Trabajo" 
                                                ValidationGroup="0">*</asp:RequiredFieldValidator>
                                            </td>
               
            </tr>
            <tr>
               <td class="myLabelIzquierda" >
                                            Responsable:</td>
						<td>
                                            <asp:TextBox ID="txtResponsable" runat="server" CssClass="myTexto" 
                                                Width="264px" MaxLength="100"></asp:TextBox>
                                            </td>
                
            </tr>
           <%-- <tr>
                <td class="myLabelIzquierda" >
                                            Tipo de Hoja de Trabajo:</td>
						<td>
                                &nbsp;</td>
                
            </tr>--%>
            <tr>
                <td class="myLabelIzquierda" >
                                            ¿Incluir antecedentes en la HT?</td>
						<td class="myLabel">
                                            
                                            <asp:DropDownList ID="ddlImprimirAntecedente" runat="server" 
                                                CssClass="myLabelDerechaGde"                                                                                    >
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                                                &nbsp;
                                            (Incluye en la hoja de trabajo el ultimo resultado
                                            obtenido para el paciente.)</td>
                
            </tr>
        </table> 
    </div>
    <div id="tab2" class="tab_content" style="border: 1px solid #C0C0C0">
       <!--Content-->
        <table style="width: 700px" class="myLabelIzquierda">
            <tr>
               <td class="style1" >
                                            Orientación de la Hoja:</td>
						<td align="left" class="style2">
                                <asp:RadioButtonList ID="rdbFormatoHoja" runat="server" 
                                    ToolTip="Seleccione el formato de impresión de la hoja de trabajo" 
                                    TabIndex="3" CssClass="myLabelIzquierda" RepeatColumns="2" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">Horizontal</asp:ListItem>
                                    <asp:ListItem Value="1">Vertical</asp:ListItem>
                                </asp:RadioButtonList>
                                            </td>
               
            </tr>
            <tr>
                <td class="style1" >
                                            Formato Ancho Columnas:</td>
						<td class="style2">
                                <asp:DropDownList ID="ddlAnchoColumnas" runat="server" CssClass="myList">
                                    <asp:ListItem Selected="True" Value="0">Texto corto</asp:ListItem>
                                    <asp:ListItem Value="1">Texto mediano</asp:ListItem>
                                    <asp:ListItem Value="2">Texto grande</asp:ListItem>
                                    <asp:ListItem  Value="3">Texto corto c/Nro. Fila</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                
            </tr>
            
            <tr>
                <td class="style1" >
                                            Datos del Protocolo a Imprimir:</td>
						<td class="style2">
                                <asp:CheckBoxList ID="chkDatosProtocolo" runat="server" CssClass="myLabel" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Enabled="False" Selected="True">Numero</asp:ListItem>
                                    <asp:ListItem Value="1">Prioridad</asp:ListItem>
                                    <asp:ListItem Value="2">Origen</asp:ListItem>
                                    <asp:ListItem Value="3">Correlativo Anterior</asp:ListItem>
                                    <asp:ListItem Value="4">Médico Solicitante</asp:ListItem>
                                    
                                    
                                </asp:CheckBoxList>
                        </td>
                
            </tr>
            
            <tr>
                <td class="style1" >
                                            Datos del Paciente a Imprimir:<br />
                                                                </td>
						<td class="style2">
                                <asp:CheckBoxList ID="chkDatosPaciente" runat="server" CssClass="myLabel" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Apellido y Nombre</asp:ListItem>
                                    <asp:ListItem Value="2">Edad</asp:ListItem>
                                    <asp:ListItem Value="3">Sexo</asp:ListItem>
                                </asp:CheckBoxList>
                        </td>
                
            </tr>
            
            <tr>
                <td  >
                                            Cantidad de lineas adicionales:</td>
						<td class="myLabel">
                                <asp:TextBox ID="txtCantidadLineaAdicional" runat="server" Width="50px"></asp:TextBox>
                                <asp:RangeValidator ID="rvCantidadLineaAdicional" runat="server" 
                                    ControlToValidate="txtCantidadLineaAdicional" ErrorMessage="Dato Numerico" 
                                    MaximumValue="100" MinimumValue="0" Type="Integer" ValidationGroup="0"></asp:RangeValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Cantidad Linea Adicional" 
                                    ControlToValidate="txtCantidadLineaAdicional" ValidationGroup="0">*</asp:RequiredFieldValidator>                                
                                           </td>                
            </tr>                      
            <tr>
                <td class="myLabelIzquierda" colspan="2" >
                                          <hr /></td>                
            </tr>            
            <tr>
                <td class="style1">
                                            ¿Incluir fecha y hora de impresión?</td>
						<td class="style2">
                                            
                                            <asp:DropDownList ID="ddlImprimirFechaHora" runat="server" 
                                                
                                                CssClass="myLabelDerechaGde"                                                                                    >
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>
                                            </asp:DropDownList>
                                                                </td>
                
            </tr>
            
            <tr>
                <td class="myLabelIzquierda" colspan="2">
                                           <hr /></td>
                
            </tr>
            
          <tr>
          <td colspan="2">
          <table>
            <tr>
                <td class="myLabelIzquierda" style="width: 300px">
                                            Texto inferior izquierda</td>
						<td class="myLabelIzquierda" style="width: 300px">
                                            
                                            Texto inferior derecha</td>
                
            </tr>
            
            <tr>
                <td class="myLabelIzquierda" colspan="2">
                                            
                                            <asp:TextBox ID="txtInferiorIzquierda" runat="server" CssClass="myTexto" 
                                                MaxLength="50" Width="280px">Observaciones</asp:TextBox>
                                            
                                            &nbsp;&nbsp;
                                            
                                            <asp:TextBox ID="txtInferiorDerecha" runat="server" CssClass="myTexto" 
                                                MaxLength="50" Width="280px">Firma del responsable</asp:TextBox>
                </td>
                
            </tr>
          </table>
          </td>
          </tr>
            
           
            
        </table>
</div>
<div id="tab3" class="tab_content" style="border: 1px solid #C0C0C0">
    <table style="width: 700px" class="myLabelIzquierda">
<tr>
						<td class="myLabelIzquierda" >
                                            Analisis:</td>
						<td>
                            <anthem:TextBox ID="txtCodigo" runat="server" CssClass="myTexto" 
                               style="text-transform:uppercase"   ontextchanged="txtCodigo_TextChanged" Width="88px" AutoCallBack="True" 
                                TabIndex="4" Enabled="False"></anthem:TextBox>
                            <anthem:DropDownList ID="ddlItem" runat="server" CssClass="myList" 
                                onselectedindexchanged="ddlItem_SelectedIndexChanged" AutoCallBack="True" 
                                TabIndex="5" Enabled="False">
                            </anthem:DropDownList>
                                        
                        </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" >
                                            Texto a Imprimir:</td>
						<td>
                            <anthem:TextBox ID="txtNombre" runat="server" CssClass="myTexto" Width="196px" AutoCallBack="True" 
                                 TabIndex="6" Enabled="False" MaxLength="50"></anthem:TextBox>
                                        
  <input id="btnAgregar" type="button" value="Agregar a Lista"  onclick="CrearFila()" class="myButtonGris" style="width: 120px" /></td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" >
                                            &nbsp;</td>
						<td>
                            <asp:CustomValidator ID="cvAnalisis" runat="server" 
                                ErrorMessage="Debe completar al menos un Analisis" 
                                onservervalidate="cvAnalisis_ServerValidate" ValidationGroup="0" 
                                Font-Size="8pt">Debe completar al menos un Analisis</asp:CustomValidator>
                                        <anthem:Label ID="lblMensaje" runat="server" ForeColor="#FF3300">&nbsp;&nbsp;
                            </anthem:Label>
                        </td>
						
					</tr>
					<tr>
						<td  colspan="2" >
                                            <table id="Table1"  
        style="font-size:.9em; margin-left:1%; background-color: #DFE7F7;" cellpadding="0" cellspacing="0" 
        align="center">
<tr>
<td style="width:40px;">&nbsp</td><td width="60px" align="center" >Codigo</td>
    <td width="257px" align="center" >Analisis</td><td style="width:256px;" 
        align="center">Texto a imprimir</td><td width="20px" >&nbsp</td>
</tr>


</table>
<table summary="Tabla editable para sumar filas y columnas" id="tabla"  
        style="font-size:.9em; margin-left:1%; " cellpadding="0" cellspacing="0" 
        align="center">

<tbody id="cuerpo">

</tbody>
</table></td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" >
                                            &nbsp;</td>
						<td>
                            &nbsp;</td>
						
					</tr>
</table>
</div>
</div>

                                            
                                            </td>
						
					</tr>
<tr>
						<td class="myLabelIzquierda" colspan="2" >
                                            <hr /></td>
						
					</tr>

					<tr>
						<td align="left" >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="HTList.aspx" CausesValidation="False" 
                                                ToolTip="Regresa a la pagina anterior" 
                                onclick="lnkRegresar_Click">Regresar</asp:LinkButton><anthem:TextBox ID="txtNombreAnalisis" runat="server" CssClass="myTexto" 
                                BorderColor="White" ForeColor="White" 
                               ></anthem:TextBox>     
                        </td>
						
						<td align="right" >
    <asp:Button ID="btnGuardar" runat="server" CssClass="myButton" Text="Guardar" 
        onclick="btnGuardar_Click" ValidationGroup="0" />  
                        </td>
						
					</tr>
					<tr>
						<td align="right" colspan="2" >
                            <asp:ValidationSummary ID="vsHojaTrabajo" runat="server" 
                                HeaderText="Debe completar los siguientes datos requeridos:" 
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="0" />
                        </td>
						
					</tr>
</table>

  &nbsp;
        <br />


 <input type="hidden" runat="server" name="TxtDatos" id="TxtDatos" value="" />  </div>
  <script language="javascript" type="text/javascript">
CargarDetalle();
 // var contadorfilas = 0; 
   var tab;
            var filas;	




  function NoExiste( cod)
  {
    var devolver= true;
    var codigoNuevo= cod;
        if (codigoNuevo=='')
        {
          devolver= false; 
        }
        else
        {
             	         	    
    	    tab = document.getElementById('cuerpo');
            filas = tab.getElementsByTagName('tr');
            for (i=0; ele = filas[i]; i++)
            {  
                var codigo=ele.getElementsByTagName('input')[0].value;        
 		        if (codigo==codigoNuevo)
 		        {
 		        alert('El item ya existe en la lista');
		          devolver= false; break;
		        }
            }     	     	
         }     
            return devolver;
  }
  
  function CrearFila()
  {
       
        var codigoNuevo=  document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtCodigo").ClientID %>').value;
	            var nombreNuevo= document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtNombreAnalisis").ClientID %>').value;
var textoNuevo=	           document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtNombre").ClientID %>').value ;
        NuevaFila(codigoNuevo,nombreNuevo,textoNuevo);
       
  }
  
  function NuevaFila(cod,nom,tex)
        {
   if (NoExiste(cod))
        { 
      //  alert('entra');
            Grilla = document.getElementById('cuerpo');
         
         
             
            fila = document.createElement('tr');
           // fila.id = 'cod_'+contadorfilas;
            //fila.name='cod_'+contadorfilas;
            
        
            celdaflecha1= document.createElement('td');    
            celdaflecha1.className= "orden";
                ////crea la primera flecha
                  oFlecha1= document.createElement('img');
                  oFlecha1.name= "pru";
                  oFlecha1.src="../script/moverfilas/arriba.gif";
                  oFlecha1.alt="subir fila";
                ////crea la segunda flecha
                  oFlecha2= document.createElement('img');            
                  oFlecha2.src="../script/moverfilas/abajo.gif";
                  oFlecha2.alt="bajas fila";                                  
            
            celdaflecha1.appendChild(oFlecha1);
            celdaflecha2= document.createElement('td');    
            celdaflecha2.className= "orden";
            celdaflecha2.appendChild(oFlecha2);
            fila.appendChild(celdaflecha1);
            fila.appendChild(celdaflecha2);
            
            ///////////////////////////////////        	
            celdaCodigo = document.createElement('td');   
                ///celda para el codigo
                oCodigo = document.createElement('input');
                oCodigo.type = 'text';                        
                oCodigo.runat = 'server';
                //oCodigo.name = 'Codigo_'+contadorfilas;
                //oCodigo.id = 'Codigo_'+contadorfilas;
                oCodigo.readOnly=true;
                oCodigo.className = 'codigoAnalisis';
                celdaCodigo.className = 'codigoAnalisis';
                oCodigo.value= cod;
            
            celdaCodigo.appendChild(oCodigo);
            fila.appendChild(celdaCodigo);
        			
            ///////////////////////////////////
                     celdaAnalisis = document.createElement('td');                         
                ///celda para el codigo
                oAnalisis = document.createElement('input');
                oAnalisis.type = 'text';                        
                oAnalisis.runat = 'server';
                //oAnalisis.name = 'Analisis_'+contadorfilas;
                //oAnalisis.id = 'Analisis_'+contadorfilas;
                 oAnalisis.className = 'nombreAnalisis';
                  celdaAnalisis.className = 'nombreAnalisis';
                oAnalisis.readOnly=true;                 
             //   alert(   document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtNombreAnalisis").ClientID %>').value );
                oAnalisis.value=   nom;

            
            celdaAnalisis.appendChild(oAnalisis);
            fila.appendChild(celdaAnalisis);
           
           /////////////////////////////////////              
                     celdaTexto = document.createElement('td');                                   
                ///celda para el codigo
                oTexto = document.createElement('input');
                oTexto.type = 'text';                        
                oTexto.runat = 'server';
                oTexto.className = 'nombreAnalisis';
                 celdaTexto.className = 'nombreAnalisis';
                //oTexto.name = 'Texto_'+contadorfilas;
                //oTexto.id = 'Texto_'+contadorfilas;
                oTexto.readOnly=true;
                oTexto.value= tex;
            
            celdaTexto.appendChild(oTexto);
            fila.appendChild(celdaTexto);
            
            ///////////////////////////////////
            
                 celda6 = document.createElement('td');  
                  celda6.className= "orden";
                  celda6.width="60px";
          oBoton= document.createElement('img');
              //    oBoton.name= "pru";
                  oBoton.src="../script/moverfilas/eliminar.gif";
                  oBoton.alt="eliminar fila";
            oBoton.onclick = function () {borrarfila(this,'cuerpo')};
            celda6.appendChild(oBoton);
            fila.appendChild(celda6);
        	
            Grilla.appendChild(fila);
       //     contadorfilas = contadorfilas + 1;
          //  alert(contadorfilas);
            iniciarTabla('cuerpo');
            CargarDatos();
            }
           
        }
        
        
        
        
        
        
        
        function CargarDatos()
        {
            var str = '';            
            var tab;
            var filas;	   	     
    	    
    	    tab = document.getElementById('cuerpo');
            filas = tab.getElementsByTagName('tr');
            for (i=0; ele = filas[i]; i++)
            {  
                var codigo=ele.getElementsByTagName('input')[0].value;
                var texto=ele.getElementsByTagName('input')[2].value;   
 		        if (codigo!='')
		         str = str + codigo + '#' + texto  + '@';
            }     	     	     
	         document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatos").ClientID %>').value = str;	   	        
        }
        
        function CargarDetalle()
{ 
	var datos= document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatos").ClientID %>').value ;	   	        
	
    if (datos!="")
    {      
            var sTab = datos.split('@');
                                    
	        for (var i=0; i<(sTab.length-1); i++)
	        {//alert(sTab[i]);
	            var sFi = sTab[i].split('#');
	            //alert(sFi[0]);
	            if  (sFi[0]!="")
	            {
	            
	         
	            NuevaFila(sFi[0],sFi[1],sFi[2]);	            
		       
		      
		        }
	        }
	        }

}
       
</script> 
 
 </asp:Content>


