<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultadoBusqueda.aspx.cs" Inherits="WebLab.Resultados.ResultadoBusqueda" MasterPageFile="~/Site1.Master" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

    <link type="text/css"rel="stylesheet"      href="../App_Themes/default/style.css" />  
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
    
    <div align="left" style="width: 800px" >
&nbsp;&nbsp;

                 <table  width="700px" align="center" 
                     
                     
                 
                     cellpadding="1" cellspacing="1" class="myTabla">
					<tr>
						<td colspan="3">
                           
                            <b class="mytituloTabla"> <asp:Label ID="lblTitulo" runat="server" Text="CARGA DE RESULTADOS"></asp:Label></b></td>
						<td colspan="3" align="right">
                           
                            <asp:Image ID="imgUrgencia" ToolTip="URGENCIA" runat="server" ImageUrl="../App_Themes/default/images/urgencia.jpg" />    </td>
					</tr>
					<tr>
						<td colspan="6">
                           
                        <hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >
                           
                                                        Servicio:</td>
						<td colspan="5">
                                   <asp:Label ID="lblServicio" runat="server" CssClass="myLabelIzquierdaGde" 
                                       Text="Label"></asp:Label>
                            <anthem:DropDownList ID="ddlServicio" runat="server" 
                                ToolTip="Seleccione el servicio" TabIndex="1" CssClass="myList" 
                                AutoCallBack="True" onselectedindexchanged="ddlServicio_SelectedIndexChanged" 
                                       Height="16px">
                            </anthem:DropDownList>
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >
                           
                          <asp:Label ID="lblFormaCarga" runat="server" 
                                Text="Forma de Carga:" ></asp:Label></td>
						<td colspan="5">
                                   <anthem:RadioButtonList ID="rdbCargaResultados" runat="server" 
                                       AutoPostBack="True" 
                                       onselectedindexchanged="rdbCargaResultados_SelectedIndexChanged" 
                                       RepeatDirection="Horizontal">
                                       <Items>
                                           <asp:ListItem Value="0">Por Lista de Protocolos</asp:ListItem>
                                           <asp:ListItem Value="1">Por Hoja de Trabajo</asp:ListItem>
                                           <asp:ListItem Value="2">Por Analisis</asp:ListItem>
                                       </Items>
                                   </anthem:RadioButtonList>
                                        
                                       <%--  <asp:RequiredFieldValidator ID="rfvFechaDesde" 
                                runat="server" ControlToValidate="txtFecha" ErrorMessage="Fecha Desde" 
                                ValidationGroup="0">*</asp:RequiredFieldValidator>--%>
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Area:<anthem:RangeValidator ID="rvArea" runat="server" 
                                ControlToValidate="ddlArea" ErrorMessage="Area" 
                                MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">*</anthem:RangeValidator>
                                        
                                            </td>
						<td colspan="5">
                            <anthem:DropDownList ID="ddlArea" runat="server" 
                                ToolTip="Seleccione el area" TabIndex="2" CssClass="myList" 
                               AutoPostBack="True"  onselectedindexchanged="ddlArea_SelectedIndexChanged">
                            </anthem:DropDownList>
                                        
                            <anthem:DropDownList ID="ddlHojaTrabajo" runat="server" 
                                ToolTip="Seleccione la hoja de trabajo" TabIndex="2" CssClass="myList">
                            </anthem:DropDownList>
                                        
                            <anthem:RangeValidator ID="rvHojaTrabajo" runat="server" 
                                ControlToValidate="ddlHojaTrabajo" ErrorMessage="Hoja Trabajo" 
                                MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">*</anthem:RangeValidator>
                                        
                                            <anthem:Image ID="imgAgregarArea" runat="server" 
                                ImageUrl="~/App_Themes/default/images/add.png" />
                            <anthem:DropDownList ID="ddlArea2" runat="server" 
                                ToolTip="Seleccione el area" TabIndex="2" CssClass="myList" 
                               AutoPostBack="True" Visible="false" >
                            </anthem:DropDownList>
                                        
                                            </td>
					</tr>
					<tr>
						<td  class="myLabelIzquierda" style="vertical-align: top">
                            <asp:Label ID="lblAnalisis" runat="server" Text="Análisis:"></asp:Label>
                        </td>
						<td colspan="5"  >
                            <anthem:TextBox ID="txtCodigo" runat="server" CssClass="myTexto" 
                               style="text-transform:uppercase"   ontextchanged="txtCodigo_TextChanged" Width="88px" AutoCallBack="True" 
                                TabIndex="4" Enabled="False"></anthem:TextBox>
                            <anthem:DropDownList ID="ddlAnalisis" runat="server" Width="400px" 
                                ToolTip="Seleccione el analisis" TabIndex="1" CssClass="myList" 
                                Enabled="False" onselectedindexchanged="ddlAnalisis_SelectedIndexChanged" 
                                AutoCallBack="True">  </anthem:DropDownList>
                               
                            <anthem:RangeValidator ID="rvAnalisis" runat="server" 
                                ControlToValidate="ddlAnalisis" ErrorMessage="Analisis" 
                                MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">*</anthem:RangeValidator>
                                        
                        </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" colspan="6"> 
                                 <anthem:Label ID="lblMensaje" runat="server" ForeColor="#FF3300"></anthem:Label> <hr /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Fecha Desde:</td>
						<td>
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de inicio de busqueda"  />
                              <%--  <asp:RequiredFieldValidator ID="rfvFechaDesde" 
                                runat="server" ControlToValidate="txtFecha" ErrorMessage="Fecha Desde" 
                                ValidationGroup="0">*</asp:RequiredFieldValidator>--%>
                            <asp:CustomValidator ID="cvFechas" runat="server" 
                                ErrorMessage="Fechas de inicio y de fin" 
                                onservervalidate="cvFechas_ServerValidate" ValidationGroup="0">*</asp:CustomValidator>
                        &nbsp;&nbsp;
                        </td>
						<td class="myLabelIzquierda" colspan="3">
                            Fecha Hasta:</td>
						<td>
                             <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="4" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de fin de busqueda"  /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Prot. Desde:</td>
						<td>
                    <input id="txtProtocoloDesde" runat="server" type="text" maxlength="9"                        
                       tabindex="5" class="myTexto"        
                                onblur="javascript:valNumero(this); copiarNumero();"                        
                                title="Ingrese el numero de protocolo de inicio"  /><asp:CustomValidator ID="cvNumeroDesde" runat="server" 
                                ErrorMessage="Numero de Protocolo" 
                                onservervalidate="cvNumeros_ServerValidate" ValidationGroup="0" 
                                Font-Size="8pt">Sólo numeros</asp:CustomValidator>
                        </td>
						<td class="myLabelIzquierda" colspan="3">
                            Prot. Hasta:</td>
						<td>
                    <input id="txtProtocoloHasta" runat="server" type="text" maxlength="9" 
                         tabindex="6" class="myTexto"  onblur="valNumero(this)"
                                title="Ingrese el numero de protocolo de fin"  /><asp:CustomValidator ID="cvNumeroHasta" runat="server" 
                                ErrorMessage="Numero de Protocolo" 
                                onservervalidate="cvNumeroHasta_ServerValidate" ValidationGroup="0">Sólo numeros</asp:CustomValidator>
                        </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Nro. de Origen:</td>
						<td colspan="5">
                            <asp:TextBox ID="txtNroOrigen" runat="server" CssClass="myTexto" TabIndex="7" 
                                ToolTip="Ingrese numero de origen"></asp:TextBox>
                        </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Origen:</td>
						<td>
                            <asp:DropDownList ID="ddlOrigen" runat="server" 
                                ToolTip="Seleccione el origen" TabIndex="8" CssClass="myList">
                            </asp:DropDownList>
                            
                        </td>
						<td class="myLabelIzquierda" colspan="3">
                            <asp:Label ID="lblPrioridad" runat="server" Text="Prioridad:"></asp:Label>
                            </td>
						<td>
                                        
                            <anthem:DropDownList ID="ddlPrioridad" runat="server" 
                                ToolTip="Seleccione la prioridad" TabIndex="9" CssClass="myList">
                            </anthem:DropDownList>
                                        
                        </td>
					</tr>
					
						<tr>
						<td class="myLabelIzquierda"> Efector Solicitante:</td>
						<td colspan="5">
                            <asp:DropDownList ID="ddlEfector" runat="server" 
                                ToolTip="Seleccione el efector" TabIndex="10" CssClass="myList">
                            </asp:DropDownList></td>
                                        
					</tr>
						
					
					
						
						
					
					
						<tr>
						<td class="myLabelIzquierda"> DNI Paciente:</td>
						<td colspan="5">
                    <input id="txtDNI" runat="server" type="text" maxlength="10"                        
                       tabindex="11" class="myTexto"        
                                onblur="valNumero(this)"                        
                                title="Ingrese el DNI del paciente"  /><asp:CompareValidator 
                                ID="cvtxtDNI" runat="server" ControlToValidate="txtDNI" 
                                ErrorMessage="Debe ingresar solo numeros" Operator="DataTypeCheck" 
                                Type="Integer" ValueToCompare="0">Sólo números</asp:CompareValidator>
                            </td>
                                        
					</tr>
						
					
					
						
						
					
					
						<tr>
						<td class="myLabelIzquierda" colspan="6">
                            <hr /></td>
												
					</tr>
					
												
						
					
					
						<tr>
						<td class="myLabelIzquierda" style="height: 12px; vertical-align: top;">
                            Sector/Servicio:</td>
						<td style="height: 12px" colspan="5">
                                                           <asp:ListBox ID="lstSector" runat="server" 
                                CssClass="myTexto" Height="160px" 
                                                               SelectionMode="Multiple" TabIndex="12" 
                                ToolTip="Seleccione los sectores" Width="350px"></asp:ListBox>
                             </td>           
					</tr>
						
					
					
																		
						
					
					
						<tr>
						<td class="myLabelIzquierda" >
                            <asp:Label ID="lblEstado" runat="server" Text="Estado Protocolo:"></asp:Label>
                            </td>
						<td style="height: 12px" colspan="5">
                            <asp:RadioButtonList ID="rdbEstado" runat="server" RepeatDirection="Horizontal" 
                                TabIndex="13" ToolTip="Seleccione el estado de los protocolos a buscar">
                                <asp:ListItem Value="0" Selected="True">No procesados y en Proceso</asp:ListItem>
                                <asp:ListItem Value="1">Solo Validados</asp:ListItem>
                                <asp:ListItem Value="2">Todos</asp:ListItem>
                            </asp:RadioButtonList>
                             </td>           
					</tr>
						
					
					
																		
						
					
					
						<tr>
						<td   colspan="6">
                                            <hr /></td>
						
					</tr>
					<tr>
						<td   colspan="4">
                               <div class="myLabelIzquierda2" >Para optimizar el proceso 
                            de busqueda utilice los filtros disponibles. </div>
                                         
                                            <asp:CheckBox ID="chkRecordarFiltro" runat="server" CssClass="myLabelIzquierda2" 
                                Text="Recordar filtros" Checked="True" Font-Italic="True" TabIndex="14" />  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;  <asp:LinkButton ID="lnkLimpiar" runat="server" CssClass="myLinkRojo" 
                                                onclick="lnkLimpiar_Click">Limpiar Filtros</asp:LinkButton>
                                                  
                                         
                               <br />
                                         
                                         
                        </td>
						
						<td   colspan="2" align="right">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar Protocolos" 
                                                ValidationGroup="0" CssClass="myButton" TabIndex="14" 
                                                Width="132px" onclick="btnBuscar_Click" 
                                                ToolTip="Haga clic aqui para buscar los protocolos" />
                                         
                        </td>
						
					</tr>
					<tr>
						<td   colspan="6">
                           
                           <hr /></td>
						
					</tr>
					<tr>
						<td   colspan="6">       
                        
                                   <div class="mytable1" style="background-color: #F7F7F7">
                                   <asp:Label ID="lblUsuarioValida" runat="server" CssClass=myLabelIzquierdaGde BackColor="#F7F7F7"></asp:Label>
                                   <br />
                            <asp:HyperLink ID="hplCambiarContrasenia" CssClass="myLittleLink" runat="server">Cambiar Contraseña</asp:HyperLink>
                            </div>                                    
                           
                        </td>
						
					</tr>
					<tr>
						<td   colspan="6">
                                            <anthem:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                HeaderText="Debe completar los siguientes datos" ShowMessageBox="True" 
                                                ValidationGroup="0" ShowSummary="False" />
                        </td>
						
					</tr>
					<tr>
						<td   colspan="6">
                            &nbsp;</td>
						
					</tr>
					</table>						

                     <script type="text/javascript">


  

                         function copiarNumero() {                             
                             var numerito = document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtProtocoloDesde").ClientID %>').value;                             
                         //    document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtProtocoloHasta").ClientID %>').value = numerito;

                         }
  </script>    
  
 </div>
 </asp:Content>