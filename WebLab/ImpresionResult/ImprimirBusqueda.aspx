<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimirBusqueda.aspx.cs" Inherits="WebLab.ImpresionResult.ImprimirBusqueda" MasterPageFile="~/Site1.Master" %>

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
  
  
   	 <script type="text/javascript" src="../script/Mascara.js"></script>
    <script type="text/javascript" src="../script/ValidaFecha.js"></script>      
   
</asp:Content>




<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          
<br />   &nbsp;
    
  <div align="left" style="width:1000px">
                 <table  width="750px" align="center"  class="myTabla"
                     
                     
                     
                     cellpadding="1" cellspacing="1" >
					<tr>
						<td colspan="5" align="right">
                           <asp:Image ID="imgUrgencia" Visible="false" ToolTip="URGENCIA" runat="server" ImageUrl="../App_Themes/default/images/urgencia.jpg" />
                            <%--<asp:Label Visible=False ID="lblUrgencia" runat="server" Text="URGENCIA" 
                                CssClass="mytituloRojo"></asp:Label>--%>    </td>
					</tr>
					<tr>
						<td colspan="5"><b class="mytituloTabla">IMPRESION DE RESULTADOS</b>      
                        <hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Servicio:</td>
						<td colspan="4">
                            <asp:DropDownList ID="ddlServicio" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlServicio_SelectedIndexChanged1">
                            </asp:DropDownList>
                                        
                                   <asp:Label ID="lblServicio" runat="server" CssClass="myLabelIzquierdaGde" 
                                       Text="Label"></asp:Label>
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Area:</td>
						<td colspan="4">
                            <anthem:DropDownList ID="ddlArea" runat="server" AutoCallBack="True" 
                                onselectedindexchanged="ddlArea_SelectedIndexChanged">
                            </anthem:DropDownList>
                                     
                                         <anthem:Image ID="imgAgregarArea" runat="server" Visible="false"
                                ImageUrl="~/App_Themes/default/images/add.png" />
                            <anthem:DropDownList ID="ddlArea2" runat="server" 
                                ToolTip="Seleccione el area" TabIndex="2" CssClass="myList" 
                               AutoPostBack="True" Visible="false" >
                            </anthem:DropDownList>   
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Fecha Desde:</td>
						<td>
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="2" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de inicio"  /><asp:CustomValidator ID="cvFechas" runat="server" 
                                ErrorMessage="Fechas de inicio y de fin" 
                                onservervalidate="cvFechas_ServerValidate" ValidationGroup="0">*</asp:CustomValidator>
                        </td>
							<td class="myLabelIzquierda" colspan="2">Fecha Hasta:</td>
						<td>
                    <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de fin"  /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Protocolo Desde:</td>
						<td>
                    <input id="txtProtocoloDesde" runat="server" type="text" maxlength="9"                        
                       tabindex="4" class="myTexto"           
                                onblur="valNumero(this)"                      style="width: 70px" 
                                title="Ingrese el numero de protocolo de inicio"  /><asp:CustomValidator ID="cvNumeroDesde" runat="server" 
                                ErrorMessage="Numero de Protocolo" 
                                onservervalidate="cvNumeros_ServerValidate" ValidationGroup="0" 
                                Font-Size="8pt">Sólo numeros</asp:CustomValidator>
                                    </td>
							<td class="myLabelIzquierda" colspan="2">Protocolo Hasta:</td>
						<td>
                    <input id="txtProtocoloHasta" runat="server" type="text" maxlength="9" 
                         tabindex="5" class="myTexto" onblur="valNumero(this)"
                                style="width: 70px" title="Ingrese el numero de protocolo de fin"  /><asp:CustomValidator ID="cvNumeroHasta" runat="server" 
                                ErrorMessage="Numero de Protocolo" 
                                onservervalidate="cvNumeroHasta_ServerValidate" ValidationGroup="0">Sólo numeros</asp:CustomValidator>
                                    </td>
					</tr>
						<tr>
						<td class="myLabelIzquierda" style="vertical-align: top">Lista de Protocolos:</td>
						<td colspan="4">
                            <asp:TextBox class="myTexto"    ID="txtRangoProtocolo" runat="server" 
                                Width="400px" TabIndex="6" 
                                ToolTip="Ingrese la lista de protocolos a buscar"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtRangoProtocolo" 
                                ErrorMessage="En la lista de protocolos escriba numeros separados por comas" 
                                ValidationExpression="^\d{1,10}(\,\d{1,10})*" ValidationGroup="0">*</asp:RegularExpressionValidator>
                            <br />
                            (Escriba los numeros de protocolos separados por comas)
                                
					</tr>
						<tr>
						<td class="myLabelIzquierda">Origen:</td>
						<td colspan="2">
                            <asp:DropDownList ID="ddlOrigen" runat="server" 
                                ToolTip="Seleccione el origen" TabIndex="7" CssClass="myList">
                            </asp:DropDownList>
                                        
					</tr>
					<tr>
						<td class="myLabelIzquierda">
                            <asp:Label ID="lblPrioridad" runat="server" Text="Prioridad:"></asp:Label>
                            </td>
                                        
					
					
						<td>
                            <asp:DropDownList ID="ddlPrioridad" runat="server" 
                                ToolTip="Seleccione la prioridad" TabIndex="9" CssClass="myList">
                            </asp:DropDownList>
                            </td>
                                        
					</tr>
					
						<tr>
						<td class="myLabelIzquierda"> Efector Solicitante:</td>
						<td colspan="4">
                            <asp:DropDownList ID="ddlEfector" runat="server" 
                                ToolTip="Seleccione el efector" TabIndex="8" CssClass="myList">
                            </asp:DropDownList></td>
                                        
					</tr>
						<tr>
						<td class="myLabelIzquierda" colspan="5">
                            <hr /></td>
                            </tr>
						<tr>
												<td class="myLabelIzquierda" style="height: 12px; vertical-align: top;">
                            Sector/Servicio:</td>
						<td style="height: 12px" colspan="3">
                                                           <asp:ListBox ID="lstSector" runat="server" 
                                CssClass="myTexto" Height="190px" 
                                                               SelectionMode="Multiple" TabIndex="7" 
                                ToolTip="Seleccione los sectores" Width="250px"></asp:ListBox>
                             </td>           
</tr>
						
					
					
						<tr>
						<td class="myLabelIzquierda" colspan="5" ><hr /></td>
												
					
					
						<tr>
						<td class="myLabelIzquierda" >Imprimir:</td>
						<td colspan="4" class="myLabel" >
                            <asp:RadioButtonList ID="rdbOpcion" runat="server" 
                                RepeatDirection="Horizontal" Font-Size="9pt" CssClass="myLabel" 
                                TabIndex="10">
                                <asp:ListItem Value="0" Selected="True">Pendientes de Imprimir</asp:ListItem>
                                <asp:ListItem Value="1">Impresos</asp:ListItem>
                                <asp:ListItem Value="2">Todos</asp:ListItem>
                            </asp:RadioButtonList>
                                        
					</tr>
						
					
					
						<tr>
						<td class="myLabelIzquierda" >Estado:</td>
						<td colspan="4" class="myLabel" >
                            <asp:RadioButtonList ID="rdbEstado" runat="server" 
                                RepeatDirection="Horizontal" Font-Size="9pt" CssClass="myLabel" 
                                TabIndex="11">
                                <asp:ListItem Value="0" Enabled="False">No Procesados</asp:ListItem>
                                <asp:ListItem Value="1">En Proceso</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">Terminados</asp:ListItem>
                                <asp:ListItem Value="3">Todos</asp:ListItem>
                            </asp:RadioButtonList>
                                        
					</tr>
						
					
					
						<tr>
						<td class="myLabelIzquierda" colspan="5" >
                           <hr /></td>
																		
					
					
						<tr>
						<td class="myLabelIzquierda" >
                                            <asp:CheckBox ID="chkRecordarFiltro" runat="server" CssClass="myLabelIzquierda2" 
                                Text="Recordar filtros" Font-Italic="True" Checked="True" />
                            </td>
																		
					
					
						<td class="myLabelIzquierda" align="right" colspan="4" >
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                                                ValidationGroup="0" CssClass="myButton" TabIndex="12" 
                                                Width="100px" onclick="btnBuscar_Click" 
                                                ToolTip="Haga clic aquí para buscar o presione ENTER" />
                            </td>
																		
					
					
						<tr>
						<td   colspan="5">
                                            <hr /></td>
						
					</tr>
					<tr>
						<td   colspan="5">
                            <div class="myLabelIzquierda2" >&nbsp; Para optimizar el proceso 
                            de busqueda utilice los filtros de busqueda disponibles. &nbsp;&nbsp;&nbsp;
                                                   <asp:LinkButton ID="lnkLimpiar" runat="server" CssClass="myLinkRojo" 
                                                onclick="lnkLimpiar_Click">Limpiar Filtros</asp:LinkButton>
                            </div></td>
						
					</tr>
					<tr>
						<td   align="right" colspan="5">
                                            &nbsp;</td>
						
					</tr>
					<tr>
						<td   colspan="5">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                HeaderText="Debe completar los siguientes datos" ShowMessageBox="True" 
                                                ValidationGroup="0" ShowSummary="False" />
                        </td>
						
					</tr>
					<tr>
						<td   colspan="5">
                            &nbsp;</td>
						
					</tr>
					</table>						
 </div>
 </asp:Content>