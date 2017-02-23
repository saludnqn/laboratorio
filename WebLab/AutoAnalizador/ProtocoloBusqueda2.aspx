<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProtocoloBusqueda2.aspx.cs" Inherits="WebLab.AutoAnalizador.ProtocoloBusqueda2" MasterPageFile="~/Site1.Master" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

    <link type="text/css"rel="stylesheet"      href="../script/jquery-ui-1.7.1.custom.css" />  

  <script type="text/javascript"      src="../script/jquery.min.js"></script> 
  <script type="text/javascript"      src="../script/jquery-ui.min.js"></script> 
    
      <script type="text/javascript"     src="../script/jquery.ui.datepicker-es.js"></script>   
      
      <script type="text/javascript"> 
      

	$(function() {
		$("#<%=txtFechaDesde.ClientID %>").datepicker({
			showOn: 'button',
			buttonImage: '../App_Themes/default/images/calend1.jpg',
			buttonImageOnly: true
		});
	});
	$(function() {
		$("#<%=txtFechaHasta.ClientID %>").datepicker({
			showOn: 'button',
			buttonImage: '../App_Themes/default/images/calend1.jpg',
			buttonImageOnly: true
		});
	}); 
     
  </script>    
  
   <%--	 <script type="text/javascript" src="../script/Mascara.js"></script>--%>
    <script type="text/javascript" src="../script/ValidaFecha.js"></script>      
   
   
   
</asp:Content>




<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          
    <br />   &nbsp;
    
<div align="left" style="width:850px">
                 <table  width="800px" align="center" 
                     
                     
                 
                     cellpadding="1" cellspacing="1" class="style1">
				
					<tr>
						<td align="right" class="style2">
                           
                            &nbsp;</td>
						<td align="right"  >
                           
                            &nbsp;</td>
						<td align="right" colspan="7">
                           
                            &nbsp;</td>
					</tr>
					<tr>
						<td class="style2" rowspan="3" style="vertical-align: top">
                           
                            <asp:Image ID="imgEquipo" Visible="false" runat="server" />
                        </td>
						<td   rowspan="3" style="vertical-align: top">
                           
                            &nbsp;  &nbsp;  </td>
						<td colspan="7" class="mytituloTabla">
                           
                            ENVIO DE PROTOCOLOS AL EQUIPO
                            
 </td>
					</tr>
					<tr>
						<td colspan="7" class="mytituloTabla">
                           
                         <hr /></td>
					</tr>
					<tr>
						<td colspan="3" class="mytituloGris">
                                                  <asp:Label ID="lblTituloEquipo" CssClass="mytituloRojo2" runat="server" Text="Label"></asp:Label>
                        </td>
						<td colspan="4" class="myLink" align="right">
                           
                            </td>
					</tr>
					
					<tr>
						<td class="style2">&nbsp;</td>
						<td  >&nbsp;</td>
						<td class="myLabelIzquierda" colspan="7">
                              Modo manual de envio de informacion de las muestras al equipo (sin codigo de barras) <hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="style2">&nbsp;</td>
						<td  >&nbsp;</td>
						<td class="myLabelIzquierda">Fecha Desde:</td>
						<td class="style11">
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de inicio de busqueda"  />
                              <%--	<tr>
						<td class="myLabelIzquierda" >
                            Cantidad de Protocolos a buscar:</td>
						<td style="height: 12px" colspan="4">
                    <input id="txtCantidad" runat="server" type="text" maxlength="3" 
                         tabindex="5" class="myTexto"  onblur="valNumero(this)"
                                style="width: 70px" title="Ingrese la cantidad de protocolos a buscar"  /><asp:CompareValidator 
                                    ID="cvCantidadProtocolo" runat="server" ControlToValidate="txtCantidad" 
                                ErrorMessage="Debe ingresar sólo números" Operator="DataTypeCheck" 
                                Type="Integer" ValueToCompare="0" ValidationGroup="0">Debe ingresar sólo números</asp:CompareValidator>
                             </td>           
					</tr>
						--%>&nbsp;&nbsp;
                        </td>
						<td class="myLabelIzquierda" colspan="4">
                            Fecha Hasta:</td>
						<td class="style6">
                             <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de fin de busqueda"  /></td>
					</tr>
					<tr>
						<td class="style2">&nbsp;</td>
						<td  >&nbsp;</td>
						<td class="myLabelIzquierda">Protocolo Desde:</td>
						<td class="style11">
                    <input id="txtProtocoloDesde" runat="server" type="text" maxlength="10"                        
                       tabindex="4" class="myTexto"        
                                onblur="valNumero(this)"                          style="width: 70px" 
                                title="Ingrese el numero de protocolo de inicio"  /><asp:CompareValidator ID="cvProtocoloDesde" runat="server" ControlToValidate="txtProtocoloDesde" 
                                ErrorMessage="Debe ingresar solo numeros" Operator="DataTypeCheck" 
                                Type="Integer" ValueToCompare="0">Debe ingresar solo numeros</asp:CompareValidator>
                        </td>
						<td class="myLabelIzquierda" colspan="4">
                            Protocolo Hasta:</td>
						<td class="style6">
                    <input id="txtProtocoloHasta" runat="server" type="text" maxlength="10" 
                         tabindex="5" class="myTexto"  onblur="valNumero(this)"
                                style="width: 70px" title="Ingrese el numero de protocolo de fin"  /><asp:CompareValidator ID="cvProtocoloHasta" runat="server" ControlToValidate="txtProtocoloHasta" 
                                ErrorMessage="Debe ingresar solo numeros" Operator="DataTypeCheck" 
                                Type="Integer" ValueToCompare="0">Debe ingresar solo numeros</asp:CompareValidator>
                        </td>
					</tr>
					<tr>
						<td class="style2">&nbsp;</td>
						<td  >&nbsp;</td>
						<td class="myLabelIzquierda">Origen:</td>
						<td class="style11">
                            <asp:DropDownList ID="ddlOrigen" runat="server" 
                                ToolTip="Seleccione el origen" TabIndex="6" CssClass="myList">
                            </asp:DropDownList>
                            
                        </td>
						<td class="myLabelIzquierda" colspan="4">
                            <asp:Label ID="lblPrioridad" runat="server" Text="Prioridad:"></asp:Label>
                            </td>
						<td class="style6">
                                        
                            <anthem:DropDownList ID="ddlPrioridad" runat="server" 
                                ToolTip="Seleccione la hoja de trabajo" TabIndex="2" CssClass="myList">
                            </anthem:DropDownList>
                                        
                        </td>
					</tr>
					
						<tr>
						<td class="style2"> &nbsp;</td>
						<td  > &nbsp;</td>
						<td class="myLabelIzquierda"> Efector Solicitante:</td>
						<td colspan="6">
                            <asp:DropDownList ID="ddlEfector" runat="server" 
                                ToolTip="Seleccione el efector" TabIndex="7" CssClass="myList">
                            </asp:DropDownList></td>
                                        
					</tr>
						
					
					
						
						
					
					
						<tr>
						<td class="style2">
                            &nbsp;</td>
												
					
					
												
						
					
					
						<td  >
                            &nbsp;</td>
												
					
					
												
						
					
					
						<td class="myLabelIzquierda" colspan="7">
                            <hr /></td>
												
					
					
												
						
					
					
						<tr>
						<td class="style7" style="vertical-align: top;">
                            &nbsp;</td>
						<td class="style8" style="vertical-align: top;">
                            &nbsp;</td>
						<td class="myLabelIzquierda" style="vertical-align: top;">
                            Sector/Servicio:</td>
						<td style="height: 12px" colspan="6">
                                                           <asp:ListBox ID="lstSector" runat="server" 
                                CssClass="myTexto" Height="190px" 
                                                               SelectionMode="Multiple" TabIndex="7" 
                                ToolTip="Seleccione los sectores" Width="350px"></asp:ListBox>
                             </td>           
					</tr>
						
					
					
																		
						
					
					
						<tr>
						<td class="style2" >
                            &nbsp;</td>
						<td   >
                            &nbsp;</td>
						<td class="myLabelIzquierda" colspan="7" >
                           <hr /></td>
					</tr>
						
					
					
																		
						
					
					
					
						
					
					
																		
						<tr>
						<td class="style2" >
                            &nbsp;</td>
						<td   >
                            &nbsp;</td>
						<td class="myLabelIzquierda" >
                            <asp:Label ID="lblEstado" runat="server" Text="Estado Protocolo:"></asp:Label>
                            </td>
						<td style="height: 12px" colspan="3" class="myLabelIzquierda">
                            <asp:RadioButtonList ID="rdbEstado" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected="True">Pendientes de Enviar</asp:ListItem>
                                <asp:ListItem Value="1">Enviados</asp:ListItem>
                                <asp:ListItem Value="2">Todos</asp:ListItem>
                            </asp:RadioButtonList>
                             </td>           
						<td style="height: 12px" colspan="3" class="myLabelIzquierda">
                            &nbsp;</td>           
					</tr>
						
					
					
																		
						
					
					
					
						
					
					
																		
						<div id="pnlMindray" runat="server">
						<tr>
						<td class="style2" >
                            &nbsp;</td>
						<td   >
                            &nbsp;</td>
						<td class="myLabelIzquierda" >
                            Tipo de Muestra:</td>
						<td style="height: 12px" colspan="6">
                                <asp:DropDownList ID="ddlTipoMuestra" runat="server">
                                    <asp:ListItem Selected="True">Suero</asp:ListItem>
                                    <asp:ListItem>Orina</asp:ListItem>
                                    <asp:ListItem>Plasma</asp:ListItem>
                                    <asp:ListItem>Otros</asp:ListItem>
                                </asp:DropDownList>
                             </td>           
					</tr>
						<tr>
						<td class="style2" >
                            &nbsp;</td>
						<td   >
                            &nbsp;</td>
						<td class="myLabelIzquierda" >
                            Prefijo a Filtrar:</td>
						<td style="height: 12px" colspan="6">
                                <asp:DropDownList ID="ddlPrefijo" runat="server">
                                </asp:DropDownList>
                             </td>           
					</tr>
						
					
					
																		
						
					
					
					<%--	<tr>
						<td class="myLabelIzquierda" >
                            Cantidad de Protocolos a buscar:</td>
						<td style="height: 12px" colspan="4">
                    <input id="txtCantidad" runat="server" type="text" maxlength="3" 
                         tabindex="5" class="myTexto"  onblur="valNumero(this)"
                                style="width: 70px" title="Ingrese la cantidad de protocolos a buscar"  /><asp:CompareValidator 
                                    ID="cvCantidadProtocolo" runat="server" ControlToValidate="txtCantidad" 
                                ErrorMessage="Debe ingresar sólo números" Operator="DataTypeCheck" 
                                Type="Integer" ValueToCompare="0" ValidationGroup="0">Debe ingresar sólo números</asp:CompareValidator>
                             </td>           
					</tr>
						--%>
					
					
																		
						
					
					
						<tr>
						<td class="style2" >
                            &nbsp;</td>
						<td   >
                            &nbsp;</td>
						<td class="myLabelIzquierda" >
                            Numerar las 
                            <br />
                            muestras desde:</td>
						<td style="height: 12px" colspan="6">
                    <input id="txtIDMuestra" runat="server" type="text" maxlength="3" 
                         tabindex="5" class="myTexto"  onblur="valNumero(this)"
                                style="width: 70px" 
                                title="Ingrese el numero de inicio de ID de muestra" value="1"  /><asp:CompareValidator 
                                    ID="cvCantidadProtocolo0" runat="server" ControlToValidate="txtIDMuestra" 
                                ErrorMessage="Debe ingresar sólo números" Operator="DataTypeCheck" 
                                Type="Integer" ValueToCompare="0" ValidationGroup="0">Debe ingresar sólo números</asp:CompareValidator>
                             </td>           
					</tr>
						
					</div>
					
																		
						
					
					
					
						<tr>
						<td class="style2" >
                            &nbsp;</td>
						<td   >
                            &nbsp;</td>
						<td class="myLabelIzquierda" colspan="7" >
                            <asp:CheckBox ID="chkLimpiarTemporal" Checked="true" runat="server" Text="Anexar a la lista del envío anterior" ToolTip="No sobreescribe la lista de protocolos generada en el envío anterior" />
                            </td>
					</tr>
						
																		
						
					
					
						<tr>
						<td class="style2">
                                            &nbsp;</td>
						
						<td  >
                                            &nbsp;</td>
						
						<td   colspan="7">
                                            <hr /></td>
						
					</tr>
					<tr>
						<td class="style2">
                              
                                                  
                                         
                               &nbsp;</td>
						
						<td  >
                              
                                                  
                                         
                               &nbsp;</td>
						
						<td   colspan="5">
                              
                                                  
                                         
                               <asp:CustomValidator ID="cvFechas" runat="server" 
                                ErrorMessage="Fechas de inicio y de fin" 
                                onservervalidate="cvFechas_ServerValidate" ValidationGroup="0" 
                                   Font-Size="10pt">*</asp:CustomValidator>
                                                  
                                         
                               <br />
                                         
                                         
                        </td>
						
						<td   colspan="2" align="right">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar Protocolos" 
                                                ValidationGroup="0" CssClass="myButton" TabIndex="9" 
                                                Width="132px" onclick="btnBuscar_Click" />
                                         
                        </td>
						
					</tr>
					<tr>
						<td class="style2">
                           
                            &nbsp;</td>
						
						<td  >
                           
                            &nbsp;</td>
						
						<td   colspan="7">
                           
                           <hr /></td>
						
					</tr>
					
						<tr>
						<td align="left" class="style2">
                           
                            &nbsp;</td>
						<td align="left"  >
                           
                            &nbsp;</td>
						<td align="left" colspan="3">
                           
                         </td>
						<td align="right" colspan="4">
                           
                            &nbsp;</td>
					</tr>
					<tr>
						<td class="style2">
                                            &nbsp;</td>
						
						<td  >
                                            &nbsp;</td>
						
						<td   colspan="7">
                                            <anthem:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                HeaderText="Debe completar los siguientes datos" ShowMessageBox="True" 
                                                ValidationGroup="0" ShowSummary="False" />
                        </td>
						
					</tr>
					<tr>
						<td class="style2">
                            &nbsp;</td>
						
						<td  >
                            &nbsp;</td>
						
						<td   colspan="7">
                            &nbsp;</td>
						
					</tr>
					</table>						
 </div>
 </asp:Content>
