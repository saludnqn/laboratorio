<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TurnosEdit2.aspx.cs" Inherits="WebLab.Turnos.TurnosEdit2" MasterPageFile="~/Site1.Master" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
 <script src="../script/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>  
   
       <link type="text/css"rel="stylesheet"      href="../App_Themes/default/style.css" />  
       <link href="../script/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" /> 
    <%--<script type="text/javascript"      src="../script/jquery.min.js"></script> --%>
      
      <script type="text/javascript">

          $(function () {
              $("#tabContainer").tabs();
              $("#tabContainer").tabs({ selected: 0 });
          });


          function enterToTab(pEvent) {///solo para internet explorer
              if (window.event) // IE
              {
                  if (window.event.keyCode == 13) {
                      if (pEvent.srcElement.id.indexOf('Codigo_') == 0) {
                          window.event.keyCode = 9;
                      }
                  }
              }

          }

          function Enter(field, event) {
              var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
              if (keyCode == 13) {
                  var i;
                  for (i = 0; i < field.form.elements.length; i++)
                      if (field == field.form.elements[i])
                          break;
                  i = (i + 1) % field.form.elements.length;
                  field.form.elements[i].focus();
                  return false;
              }
              else
                  return true;

          }

     
  </script> 
    
    <style type="text/css">
        .style1
        {
            width: 950px;
        }
        .style2
        {
            width: 8px;
        }
        .style3
        {
            width: 9px;
        }
        .style4
        {
            width: 260px;
        }
        .style7
        {
            width: 167px;
        }
        .style8
        {
            border-style: none;
            font-size: 10pt;
            font-family: Calibri;
            background-color: #FFFFFF;
            color: #333333;
            font-weight: bold;
            width: 167px;
        }
        .style9
        {
            border-style: none;
            font-size: 10pt;
            font-family: Calibri;
            background-color: #FFFFFF;
            color: #333333;
            font-weight: bold;
            width: 260px;
        }
        .style10
        {
            border-style: none;
            font-size: 10pt;
            font-family: Calibri;
            background-color: #FFFFFF;
            color: #333333;
            font-weight: bold;
            }
        .style11
        {
            width: 369px;
        }
    </style>
    
    </asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          
 <br /> &nbsp;
    <div align="left"  style="width:900px; ">
     <input id="hidToken" type="hidden" runat="server" />
    <table  width="900px" align="center"   cellpadding="1" cellspacing="1" 
        class="style1" >
					<tr>
						<td class="style2">&nbsp;</td>
						<td colspan="2"><b class="mytituloTabla">
                            <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></b></td>
						<td align="right" colspan="2"><b class="mytituloTabla">
                            <asp:Panel ID="pnlNuevo" runat="server"  style="color: #333333; font-weight: bold; font-size: 12px; vertical-align: top;">                                         
                                         
                            </asp:Panel></b></td>
						<td class="style3">&nbsp;</td>
					</tr>
					<tr>
						<td class="style2">&nbsp;</td>
						<td colspan="4"><hr /></td>
						<td class="style3">&nbsp;</td>
					</tr>
					<tr>
						<td class="style2"  >
                            &nbsp;</td>
						<td colspan="3"  >
                            <asp:Label ID="lblPaciente" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="10pt" ForeColor="Black" Text="26982063 - PINTOS BEATRIZ CAROLINA"></asp:Label>
                                        
                                            <asp:Label ID="lblIdPaciente" runat="server" Text="Label" 
                                Visible="False"></asp:Label>
                                        
                                            </td>
						<td align="right" class="style4" rowspan="3" style="vertical-align: top"  >
                            <b>
                            <%--   <td style="width: 80px; " class="tituloCelda">
                                Sin Muestra</td>      --%>
                            </b>
                                        
                            <asp:Label ID="lblAlerta" runat="server" CssClass="myLabelRojo" Text="Label" 
                                Visible="False"></asp:Label>
                                        
                                            </td>
						<td class="style3"  >
                            &nbsp;</td>
					</tr>
					
					<tr>
						<td class="style2"  >
                            &nbsp;</td>
						<td class="style10"   >
                         Fecha de Nacimiento:   </td>
						<td align="left" class="style11"  >
                            <asp:Label ID="lblFechaNacimiento" runat="server" 
                                                Text="Label" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="9pt" ForeColor="#333333" ></asp:Label> &nbsp;</td>
						<td class="style7"  >
                            &nbsp;</td>
					</tr>
					
					<tr>
						<td class="style2"  >
                            &nbsp;</td>
						<td class="style10"   >
                            Sexo:</td>
						<td align="left" class="style11"  >
                            <asp:Label ID="lblSexo" runat="server" 
                                                Text="Label" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="9pt" ForeColor="#333333" ></asp:Label></td>
						<td class="style7"  >
                            &nbsp;</td>
					</tr>
					
					<tr>
						<td class="style2"  >
                            &nbsp;</td>
						<td class="style10" colspan="4"   >
                        <hr /></td>
					</tr>
					
					<tr>
						<td class="style2"    >&nbsp;</td>
						<td class="style10"    >Tipo de Servicio:</td>
						<td class="style11"  >
                                        
                                            <asp:Label ID="lblTipoServicio" runat="server" Text="Label" Font-Bold="True" Font-Size="9pt" 
                                ></asp:Label>
                                        
                                            <asp:Label ID="lblIdTipoServicio" runat="server" Text="Label" 
                                Visible="False"></asp:Label>
                                        
                                            </td>
						<td class="style8"  >
                                        
                                            Fecha:</td>
						<td class="style9"  >
                                        
                                            <asp:Label ID="lblFecha" runat="server" Text="Label" Font-Bold="True" Font-Size="9pt" 
                                ></asp:Label>
                                       
                                            <asp:Label ID="lblHora" runat="server" Text="Label" Font-Bold="True" Font-Size="9pt" 
                                ></asp:Label>
                                        
                                            </td>
						<td class="style3"  >
                                        
                                            &nbsp;</td>
					</tr>
					
					<tr>
						<td class="style2"    >&nbsp;</td>
						<td class="style10"    >Servicio:</td>
						<td class="style11"  >
                                        
                                            <asp:DropDownList ID="ddlSectorServicio" runat="server" CssClass="myList" 
                                                TabIndex="13" Width="200px">
                                                
                                            </asp:DropDownList>
                                        
                                            </td>
						<td class="style8"  >
                                        
                                            Médico solicitante:</td>
						<td class="style9"  >
                                        
                                            <asp:DropDownList ID="ddlEspecialista" runat="server" CssClass="myList" 
                                                TabIndex="13" Width="200px">
                                                <asp:ListItem Value="1">No tiene</asp:ListItem>
                                            </asp:DropDownList>
                                        
                                            </td>
						<td class="style3"  >
                                        
                                            &nbsp;</td>
					</tr>
					<tr>
						<td class="style2"    >
                                            &nbsp;</td>
						<td   class="style10"    >
                                            Obra Social:</td>
						<td align="left" colspan="3">
                                            <asp:DropDownList ID="ddlObraSocial" runat="server" CssClass="myList" 
                                                TabIndex="13" Width="400px">
                                                <asp:ListItem Value="1">No tiene</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            </td>
						
						<td align="left" class="style3">
                                            &nbsp;</td>
						
					</tr>
					<tr>
						
						
						
						<td class="style2">
                                            &nbsp;</td>
						
						
						
						<td   colspan="4">
                                            <hr /></td>
						
						
						
						<td class="style3">
                                            &nbsp;</td>
						
						
						
					</tr>
					
					
					<tr>
						
						
						
						<td class="style2">
                                            &nbsp;</td>
						
						
						
						<td   colspan="4">

                        	<div id="tabContainer" style="width: 100%; z-index:200; position:relative;" >  
						       <ul style="font-size: small">
    <li ><a href="#tab1">Analisis</a></li>
    <li><a href="#tab2">Diagnósticos</a></li>
 

</ul>
                                            		      
                            <div id="tab1" class="tab_content">
					  
					  <table width="100%">
					<tr>
						
						<td style="vertical-align: top">
						
						   <table cellpadding="1" cellspacing="0" >
                            <tr >
                                <td style="width: 10px; height: 13px">
                                </td>
                                <td style="width: 50px;" class="tituloCelda">
                                    Codigo</td>
                                <td style="width: 350px;"  class="tituloCelda">
                                    Descripcion</td>
                            <%--   <td style="width: 80px; " class="tituloCelda">
                                Sin Muestra</td>      --%>         
                              
                    </tr>
                </table>
                  <div  onkeydown="enterToTab(event)"  style="width:470px;height:180pt;overflow:scroll;border:1px solid #CCCCCC;"> 
                
                    <table  class="mytablaIngreso" border="0" id="tabla" cellpadding="0" cellspacing="0" >
	  	                <tbody id="Datos" >
		                    
		                </tbody>
	                </table>
	                
                    <input type="hidden" runat="server" name="TxtCantidadFilas" id="TxtCantidadFilas" value="0" />
                </div>
                
                
                </td>
						
						<td   colspan="3" style="vertical-align: top">
							
					  <fieldset id="Fieldset3" title="Analisis" style="width:100%; text-align:left; ">
                                       <legend class="myLabelIzquierda">Analisis</legend>

						<table align="left" width="100%">
						<tr>
						<td class="myLabelIzquierda"  >Rutina:</td>
						</td>
						<td>        
                            &nbsp;</td>
						<td>   
                            &nbsp;</td>
						</tr>
						<tr>
						<td class="myLabelIzquierda"  colspan="3">             
                            <anthem:DropDownList ID="ddlRutina" runat="server" AutoCallBack="True" 
                                               
                                                TextDuringCallBack="Cargando ..."
                                CssClass="myList" TabIndex="20" 
                                onselectedindexchanged="ddlRutina_SelectedIndexChanged" ToolTip="Rutina">
                                            </anthem:DropDownList>   
                         </td>
						</tr>
						<tr>
						<td class="myLabelIzquierda" colspan="3">   <input id="Button2" type="button" value="Agregar Rutina" 
                                onclick="AgregarDeLaListaRutina();"
                                class="myButtonGris" title="Agrega la rutina seleccionada a la lista" style="width: 120px" />    &nbsp;</td>
						</tr>
                        	<tr>
						<td class="myLabelIzquierda"   ></td>
						<td>        
                            &nbsp;</td>
						<td>   
                            &nbsp;</td>
						</tr>
						<tr>
						<td class="myLabelIzquierda"   >     Busqueda por Nombre:</td>
						<td>        
                            &nbsp;</td>
						<td>   
                            &nbsp;</td>
						</tr>
					
						
                       
                
                  
                </div>
                </td>
						</tr>
						<tr>
						<td class="myLabelIzquierda" colspan="3"   >             
                            <anthem:dropdownlist ID="ddlItem" runat="server" AutoCallBack="True" 
                                                onselectedindexchanged="ddlItem_SelectedIndexChanged" 
                                                TextDuringCallBack="Cargando ..." 
                                CssClass="myList" TabIndex="20" ToolTip="Analisis">
                                            </anthem:dropdownlist>
                           <input id="Button1" type="button" value="Agregar Analisis" 
                                onclick="AgregarDeLaLista();" style="width: 120px"
                                class="myButtonGris" title="Agrega el analisis seleccionado a la lista" /></td>
						</tr>
					
						
                       
                
                  
                		</table>
						
                             </td>
						
						<td style="vertical-align: top">
						
						    &nbsp;</td>   <input type="hidden" runat="server" name="TxtDatos" id="TxtDatos" value="" />                                
                <input id="txtTareas" name="txtTareas" runat="server" type="hidden"  />
						
					</tr>
					
				
					
				
					
					</table>
				</div>
                                              	
                            <div id="tab2" class="tab_content">
					<table width="100%">
					<tr>
						
						<td style="vertical-align: top" align="right">
						
						    &nbsp;</td>
						
						<td   colspan="3" style="vertical-align: top">
						
						       <fieldset id="Fieldset2"  
                                >
                                <legend class="myLabelIzquierda">Diagnósticos Presuntivos - Codificación CIE 10</legend>
                                     <table align="left" width="100%">
                                         <tr>
                                           <td class="myLabelIzquierda" colspan="3"  >
                                               Codigo: &nbsp; &nbsp;
                                                 <anthem:TextBox ID="txtCodigoDiagnostico" runat="server" AutoCallBack="True" 
                                                     CssClass="myTexto" ontextchanged="txtCodigoDiagnostico_TextChanged"></anthem:TextBox>
                                             </td>
                                         </tr>
                                         <tr>
                                           <td class="myLabelIzquierda" colspan="3"  >
                                               Nombre: &nbsp;<anthem:TextBox ID="txtNombreDiagnostico" runat="server" AutoCallBack="True" 
                                                     CssClass="myTexto" ontextchanged="txtNombreDiagnostico_TextChanged" 
                                                   Width="268px"></anthem:TextBox>
                                             </td>
                                         </tr>
                                             <tr>
                                           <td class="myLabelIzquierda" colspan="3"  >
                                              <anthem:Button ID="btnBusquedaDiagnostico" CssClass="myButtonGris" runat="server" Text="Buscar" 
                                                   onclick="btnBusquedaDiagnostico_Click" />
                              <anthem:Button ID="btnBusquedaFrecuente" CssClass="myButtonRojo" runat="server" Text="Ver Frecuentes" 
                                                   onclick="btnBusquedaFrecuente_Click" /></td>
                                         </tr>
                                         <tr>
                                           <td class="myLabelIzquierda" colspan="3"  >
                                               <hr /></td>
                                         </tr>

                                         <tr>
                                           <td class="myLabelIzquierda"  >
                                               Diagnosticos encontrados</td>
                                             <td>
                                                 &nbsp;</td>                                         
                                             <td  class="myLabelIzquierda" >
                                                 Diagnosticos del Paciente</td>                                     
                                         </tr>
                                         <tr>
                                           <td class="myLabelIzquierda"  >
                                               <anthem:ListBox ID="lstDiagnosticos" runat="server" AutoCallBack="True" 
                                                   CssClass="myTexto" Height="150px" Width="400px">
                                               </anthem:ListBox>
                                             </td>
                                             <td>
                                                 <anthem:ImageButton ID="btnAgregarDiagnostico" runat="server" 
                                                     ImageUrl="~/App_Themes/default/images/añadir.jpg" 
                                                     onclick="btnAgregarDiagnostico_Click1" 
                                                     ToolTip="Agregar a la lista de Diagnosticos del Paciente" /><br />
                                                     <p></p>
                                                 <anthem:ImageButton ID="btnSacarDiagnostico" runat="server" 
                                                     ImageUrl="~/App_Themes/default/images/sacar.jpg" 
                                                     onclick="btnSacarDiagnostico_Click"
                                                        ToolTip="Quitar diagnóstico de la lista de Diagnosticos del Paciente"
                                                      />
                                             </td>                                         
                                             <td class="myLabelIzquierda">
                                                 <anthem:ListBox ID="lstDiagnosticosFinal" runat="server" CssClass="myTexto" 
                                                     Height="150px" Width="400px" SelectionMode="Multiple" 
                                                     ToolTip="Sacar de la lista de Diagnosticos del Paciente">
                                                 </anthem:ListBox>
                                             </td>                                         
                                         </tr>
                                      
                                         </table>
                                 </fieldset>
           
              </td>
						
						<td style="vertical-align: top">
						
						    &nbsp;</td>
						
					</tr>
					
				
					</table>
					   </div>
					  </div>
                                            
                                            </td>
						
						
						
						<td class="style3">
                                            &nbsp;</td>
						
						
						
					</tr>
					
					
				
					
					<tr>
						
						
						
						<td class="style2">
                                            &nbsp;</td>
						
						
						
						<td   colspan="4">
				            <asp:CheckBox ID="chkImprimir" runat="server" CssClass="myLabel" 
                                Text="Imprimir comprobante" />
                                   
                                            </td>
						
						
						
						<td class="style3">
                                            &nbsp;</td>
						
						
						
					</tr>
					
					
				
					
					<tr>
						
						
						
						<td class="style2">
                                            &nbsp;</td>
						
						
						
						<td   colspan="2">
                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="0" 
                                                onclick="btnGuardar_Click" CssClass="myButton" TabIndex="24" 
                                                ToolTip="Guarda los datos cargados para el turno" />  
                                            
                                            <asp:CustomValidator ID="cvValidaPracticas" runat="server" 
                                                ErrorMessage="CustomValidator" 
                                                onservervalidate="cvValidaPracticas_ServerValidate" ValidationGroup="0"></asp:CustomValidator>
                                            
                                            </td>
						
						
						
						<td   colspan="2" align="right">
				<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                                onclick="btnCancelar_Click" CssClass="myButton" TabIndex="24" 
                                                CausesValidation="False" /> 
                                            
                                            </td>
						
						
						
						<td class="style3">
                                            &nbsp;</td>
						
						
						
					</tr>
					
					
				
					
					<tr>
						
						
						
						<td class="style2">
                                            &nbsp;</td>
						
						
						
						<td   colspan="4">
				            &nbsp;</td>
						
						
						
						<td class="style3">
                                            &nbsp;</td>
						
						
						
					</tr>
					
					<tr>
						
						
						
						<td class="style2">
                                            &nbsp;</td>
						
						
						
						<td   colspan="4">
				           <asp:LinkButton CssClass="myLink" Visible="false" ID="lnkReimprimirComprobante" onclick="lnkReimprimirComprobante_Click" runat="server">Reimprimir Comprobante</asp:LinkButton></td>
						
						
						
						<td class="style3">
                                            &nbsp;</td>
						
						
						
					</tr>
				
					
					<tr>
						
						
						
						<td class="style2">
                                            &nbsp;</td>
						
						
						
						<td   colspan="4">
						
						         
				             <asp:Panel ID="pnlImpresora" runat="server" style="border:1px solid #C0C0C0; width:100%; background-color: #EFEFEF;">
                           
                           
 <table width="720px" align="center">
 	<tr>
						<td class="myLabelIzquierda" align="left" style="width: 140px; background-color: #EFEFEF;">
                                        Impresora del sistema: </td>
						<td  align="left">
                                        <asp:DropDownList ID="ddlImpresora" runat="server" CssClass="myList" >
                                        </asp:DropDownList>
                            </td>
						
                                        </tr>
                                        </table>
														
 </asp:Panel></td>
						
						
						
						<td class="style3">
                                            &nbsp;</td>
						
						
						
					</tr>
					
					
				
					
					<tr>
						
						
						
						<td class="style2">
                                            &nbsp;</td>
						
						
						
						<td   colspan="4">
				            &nbsp;</td>
						
						
						
						<td class="style3">
                                            &nbsp;</td>
						
						
						
					</tr>
					
					
				
					
					</table>
					
					
				
				
				<div style="width: 95%" align="left">  
                    <anthem:TextBox 
                                ID="txtCodigo"   runat="server" BorderColor="White" ForeColor="White" 
                                BackColor="White" BorderStyle="Solid" BorderWidth="0px"></anthem:TextBox>
                                                 <anthem:TextBox 
                                ID="txtCodigosRutina"  runat="server" BorderColor="White" 
                                ForeColor="White" BackColor="White" BorderStyle="Solid" BorderWidth="0px"></anthem:TextBox>
				</div>
					
	</div>			
				
					
					<script language="javascript" type="text/javascript">
  var contadorfilas = 0;
InicioPagina();
//function VerificaLargo (source, arguments)
//    {                
//    var Observacion = arguments.Value.toString().length;
// //   alert(Observacion);
//    if (Observacion>4000 )
//        arguments.IsValid=false;    
//    else   
//        arguments.IsValid=true;    //Si llego hasta aqui entonces la validación fue exitosa        
//}



        
      
        
        document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtCantidadFilas").ClientID %>').value  = contadorfilas;
        
        function InicioPagina()
        {
 
            if (   document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatos").ClientID %>').value == "")
            {
                CrearFila(true);
         
            }
            else
            {
                CargarDetalles();OrdenarDatos();
                //alert(contadorfilas);
            }
        }
        
        function NuevaFila()
        {
            Grilla = document.getElementById('Datos');
         
             
            fila = document.createElement('tr');
            fila.id = 'cod_'+contadorfilas;
            fila.name='cod_'+contadorfilas;
        	
            celda1 = document.createElement('td');
            oNroFila = document.createElement('input');
            oNroFila.type = 'text';
            oNroFila.readOnly=true;
            
            oNroFila.runat = 'server';
            oNroFila.name = 'NroFila_'+contadorfilas;
            oNroFila.id = 'NroFila_'+contadorfilas;
            //oNroFila.onfocus= function() {PasarFoco(this)}
            oNroFila.className = 'fila';
            celda1.appendChild(oNroFila);
            fila.appendChild(celda1);
        			
            celda2 = document.createElement('td');		
            oCodigo = document.createElement('input');
            oCodigo.type = 'text';
            oCodigo.runat = 'server';
            oCodigo.name = 'Codigo_'+contadorfilas;
            oCodigo.id = 'Codigo_'+contadorfilas;
            oCodigo.className = 'codigo';

            oCodigo.onblur = function () { CargarTarea(this); };
            oCodigo.setAttribute("onkeypress", "javascript:return Enter(this, event)"); 
            //oCodigo.onchange = function () {CargarDatos()};
            celda2.appendChild(oCodigo);
    	    fila.appendChild(celda2);
        	
    	    celda3 = document.createElement('td');		
            oTarea = document.createElement('input');
            oTarea.type = 'text';
            oTarea.readOnly=true;
            oTarea.runat = 'server';
            oTarea.name = 'Tarea_'+contadorfilas;
            oTarea.id = 'Tarea_'+contadorfilas;
            oTarea.className = 'descripcion';
            oTarea.onchange = function () {CargarDatos()};
            celda3.appendChild(oTarea);
    	    fila.appendChild(celda3);
        	
//    	 
        	
        	        	
            celda6 = document.createElement('td');
            oBoton = document.createElement('input');
            oBoton.className='boton';
            oBoton.type = 'button';
            oBoton.value= 'X';
            oBoton.onclick = function () {borrarfila(this)};
            celda6.appendChild(oBoton);
            fila.appendChild(celda6);
        	
            Grilla.appendChild(fila);
            contadorfilas = contadorfilas + 1;
        }
  
       function CrearFila(validar)
        {
            var valFila = contadorfilas - 1;
	        if (UltimaFilaCompleta(valFila, validar))
	        {
	   
	            NuevaFila();
    	       
    	        valFila = contadorfilas - 1;
    	        document.getElementById('NroFila_' + valFila).value = contadorfilas;
    	        
	            if (contadorfilas > 1)
	            {
	                var filaAnt = contadorfilas - 2;
//	             
	            }
    	        
	            document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtCantidadFilas").ClientID %>').value = contadorfilas;	           	            
	            document.getElementById('Codigo_' + valFila).focus();
	        }
        }
        
        
        function UltimaFilaCompleta(fila, validar)
        {
            if ((fila >= 0) && (validar))
            { 
                var cod = document.getElementById('Codigo_' + fila);
                if (cod.value == '') 
                {
          
                    return false;
                }

            }
            
            return true;
        }
        
        function CargarDatos()
        {
            var str = '';
            
	        for (var i=0; i<contadorfilas; i++)
	        {
	        
	            var nroFila = document.getElementById('NroFila_' + i);
	            var cod = document.getElementById('Codigo_' + i);
	            var tarea = document.getElementById('Tarea_' + i);
	          
		        
		        if (cod.value!='')
		            str = str + nroFila.value + '#' + cod.value + '#' + tarea.value +  '@';
//		        
	        }
	 
	         document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatos").ClientID %>').value = str;
	        
	        
        }
        
        function PasarFoco(Fila)
        {
            var fila = Fila.id.substr(8);
         
            document.getElementById('Codigo_' + fila).focus();
        }
        
        function CargarTarea(codigo)
        {
            var nroFila = codigo.name.replace('Codigo_', '');
            var tarea = document.getElementById('Tarea_' + nroFila);
            
           var lista = document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtTareas").ClientID %>').value ;
            
//         
            
            if (codigo.value == '')
            {
                tarea.value = '';
            }
            else
            {
               if (verificarRepetidos(codigo,tarea))	            
                {     
                    var i = lista.indexOf('#' + codigo.value + '#',0);
                    
                    if (i < 0)
                    {
                        codigo.value = '';
                        tarea.value = '';
                        alert('El codigo de tarea no existe.');
                        document.getElementById('Codigo_' + nroFila).focus();
                    }
                    else
                    {
                        var j = lista.indexOf('@',i);
                        i = lista.indexOf('#',i+1) + 1;
                        tarea.value = lista.substring(i,j);
                       CargarDatos();
                         CrearFila(true);       
                    }
                }
            }
        }
        
        function borrarfila(obj)
        {
            if(contadorfilas > 1)
            {
	            var delRow = obj.parentNode.parentNode;
	            var tbl = delRow.parentNode.parentNode;
	            var rIndex = delRow.sectionRowIndex;
	            Grilla = document.getElementById('Datos'); 
	            Grilla.parentNode.deleteRow(rIndex);
	            
	            OrdenarDatos();
	            
	            contadorfilas = contadorfilas - 1;
            }
            else
            {
           
	            var cod = document.getElementById('Codigo_0').value = '';
	            var tarea = document.getElementById('Tarea_0').value = '';
	    
	            	            
	            CargarDatos();
            }
        }
        
        function OrdenarDatos()
        {
            var pos = 0;
            var str = '';
            
	        for (var i=0; i<contadorfilas; i++)
	        {
	            var nroFila = document.getElementById('NroFila_' + i);
	            
	            if (nroFila != null)
	            {
	                nroFila.name = 'NroFila_' + pos;
	                nroFila.id = 'NroFila_' + pos;
	                nroFila.value = pos + 1;
	                var cod = document.getElementById('Codigo_' + i);
	                cod.name = 'Codigo_' + pos;
	                cod.id = 'Codigo_' + pos;
	                var tarea = document.getElementById('Tarea_' + i);
	                tarea.name = 'Tarea_' + pos;
	                tarea.id = 'Tarea_' + pos;
	            
	                	                
	                pos = pos + 1;
	                
	             
	                str = str + nroFila.value + '#' + cod.value + '#' + tarea.value +  '@';
	            }   
	        }
	        
	        
	         document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatos").ClientID %>').value = str;
	    
        }
          function verificarRepetidos(objCodigo, objTarea)
        {
          ///Verifica si ya fue cargado en el txtDatos
          var devolver=true;
          var codigo=objCodigo.value;
          if (objTarea.value=='')
          {
          var listaExistente =document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtDatos").ClientID %>').value ;
            var cantidad=1;
                var sTabla = listaExistente.split('@');                                    
	            for (var i=0; i<(sTabla.length-1); i++)
	            {
	                var sFila = sTabla[i].split('#');	                
	                if  (sFila[1]!="")
	                {
	                    if (codigo== sFila[1])	                    
	                    cantidad+=1;	                        	                     
	                }	                
	            }

	            if (cantidad>1)
	                {
	                 objCodigo.value = '';
                            objTarea.value = '';
                            alert('El código '+ codigo +' ya fue cargado. No se admiten analisis repetidos.');	
                            objCodigo.focus();	                    
	                       devolver=false;       
	                }
	          else
	               devolver=true;
	             ///Fin Verifica si ya fue cargado en el txtDatos
	             }
	             
	             else
	               devolver=true;
	         return devolver;
        }
        
        function AgregarDeLaLista()
        {    
            var elvalor= document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtCodigo").ClientID %>').value;
            if (elvalor!='')
            {
                var con= contadorfilas-1;                   
	            if (UltimaFilaCompleta(con, true))	     
	            {
	            NuevaFila();
	            }       
                document.getElementById( 'Codigo_'+con).value=elvalor;          
                CargarTarea( document.getElementById( 'Codigo_'+con)); 
//                CargarDatos();
//                NuevaFila();
                OrdenarDatos();
            }
        }
        
        
        function AgregarDeLaListaRutina()
        {      
            var elvalor= document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtCodigosRutina").ClientID %>').value;
            //alert(elvalor);
            if (elvalor!='')
            {
                var sTabla = elvalor.split(';');                                    
	            for (var i=0; i<(sTabla.length); i++)
	            {
	                var valorCodigo = sTabla[i];	         
	                var con= contadorfilas-1;
	                if (UltimaFilaCompleta(con, true))	    
	                {
                            NuevaFila();
	                }	         	           
	                document.getElementById( 'Codigo_'+con).value=valorCodigo;          
                    CargarTarea( document.getElementById( 'Codigo_'+con)); 
//                    CargarDatos();
//                    NuevaFila();
	            }
	            OrdenarDatos();	                
            }                    
        }
        
        
        function CargarDetalles()
        {
            var str = '';
            
           
            var detalles =document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatos").ClientID %>').value 
          
            
          
            var sTabla = detalles.split('@');
                                    
	        for (var i=0; i<(sTabla.length-1); i++)
	        {
	            var sFila = sTabla[i].split('#');
	            if  (sFila[1]!="")
	            {
	            CrearFila(false);
	            var nroFila = document.getElementById('NroFila_' + i);
	            nroFila.value = sFila[0];
	            var cod = document.getElementById('Codigo_' + i);
	            cod.value = sFila[1];
	            var tarea = document.getElementById('Tarea_' + i);
	            tarea.value = sFila[2];
	       
		        str = str + nroFila.value + '#' + cod.value + '#' + tarea.value +  '@';
		        }
	        }
	        
	        
	         document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatos").ClientID %>').value = str;
	        
	        
        }
        
//        function validar(e) 
//        { 
//            tecla = (document.all) ? e.keyCode : e.which; 
//            if (tecla==8) return true; //Tecla de retroceso (para poder borrar) 
//            //if (tecla==44) return true; //Tecla , (coma)  
//            //if (tecla==46) return true; //Tecla . (punto)}
//            if (tecla==47) return true; //Tecla / (barra)}
//            //patron =/[A-Za-z]/; // Solo acepta letras 
//            patron = /\d/; //Solo acepta números 
//            //patron = /\w/; Acepta números y letras 
//            //patron = /\D/; No acepta números 
//            te = String.fromCharCode(tecla);
//            return patron.test(te);
//        }
        
//        function FormatoFecha(control,e)
//        {
//            tecla = (document.all) ? e.keyCode : e.which; 
//            if (tecla!=8)//Tecla de retroceso (para poder borrar) 
//            {
//                if ((document.getElementById(control).value.length == 2) ||
//                        (document.getElementById(control).value.length == 5))
//                {
//                    document.getElementById(control).value = document.getElementById(control).value + "/";
//                }
//                if (document.getElementById(control).value.length > 10) 
//                {
//                    document.getElementById(control).value = document.getElementById(control).value.substr(0,10);
//                }
//            }
//        }
//        
//        function LargoFecha(control)
//        {
//            valFecha(document.getElementById(control));
//            var fecha = document.getElementById(control).value.split('/');
//            if (fecha[2].length == 2)
//            {
//                if (fecha[2] < 50)
//                {
//                    fecha[2] = "20" + fecha[2];
//                }
//                else
//                {
//                    fecha[2] = "19" + fecha[2];
//                }
//            }
//            document.getElementById(control).value = fecha[0] + '/' + fecha[1] + '/' + fecha[2];
//        }
        
function Button1_onclick() {

}

    </script>
</asp:Content>
