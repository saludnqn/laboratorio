<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Derivados2.aspx.cs" Inherits="WebLab.Derivaciones.Derivados2" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
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
                 <table  width="550px" align="center" 
                     
                     
                   class="myTabla">
					<tr>
						<td colspan="2"><b class="mytituloTabla">
                            <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></b>    <hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Servicio:</td>
						<td>
                              <asp:DropDownList ID="ddlServicio" runat="server" 
                                ToolTip="Seleccione el servicio" TabIndex="1" CssClass="myList" 
                                onselectedindexchanged="ddlServicio_SelectedIndexChanged" 
                                  AutoPostBack="True">
                            </asp:DropDownList>
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Area:</td>
						<td>
                            <asp:dropdownlist ID="ddlArea" runat="server" 
                                ToolTip="Seleccione el area" TabIndex="2" CssClass="myList">
                            </asp:dropdownlist>
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Fecha Desde:<asp:RequiredFieldValidator ID="rfvFechaDesde" 
                                runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Fecha Desde" 
                                ValidationGroup="0">*</asp:RequiredFieldValidator>
                        </td>
						<td>
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de inicio"  /></td>
					</tr>
						<tr>
						<td class="myLabelIzquierda">Fecha Hasta:<asp:RequiredFieldValidator ID="rfvFechaHasta" 
                                runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Fecha Hasta" 
                                ValidationGroup="0">*</asp:RequiredFieldValidator>
                        </td>
						<td>
                    <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="4" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de fin"  /></tr>
						<tr>
						<td class="myLabelIzquierda">Origen:</td>
						<td>
                            <asp:DropDownList ID="ddlOrigen" runat="server" 
                                ToolTip="Seleccione el origen" TabIndex="5" CssClass="myList">
                            </asp:DropDownList>
                                        
					</tr>
						<tr>
						<td class="myLabelIzquierda">Prioridad:</td>
						<td>
                            <asp:DropDownList ID="ddlPrioridad" runat="server" 
                                ToolTip="Seleccione la prioridad" TabIndex="6" CssClass="myList">
                            </asp:DropDownList>
                                        
					</tr>
						<tr>
						<td class="myLabelIzquierda" style="vertical-align: top" colspan="2"><hr /></td>
						<tr>
						<td class="myLabelIzquierda" style="vertical-align: top">Efector a Derivar:<asp:RangeValidator 
                                ID="RangeValidator1" runat="server" ControlToValidate="ddlEfector" 
                                ErrorMessage="Efector" MaximumValue="99999" MinimumValue="1" Type="Integer" 
                                ValidationGroup="0">*</asp:RangeValidator>
                            </td>
						<td>
                            <anthem:DropDownList ID="ddlEfector" runat="server" 
                                ToolTip="Seleccione el efector" TabIndex="6" CssClass="myList" 
                                AutoCallBack="True" onselectedindexchanged="ddlEfector_SelectedIndexChanged">
                            </anthem:DropDownList>
                                        
					        </td>
                            </tr>
                        <tr>
						<td class="myLabelIzquierda" style="vertical-align: top">An�lisis:</td>
						<td>
                           <anthem:DropDownList ID="ddlItem" runat="server" CssClass="myList" >
                                            </anthem:DropDownList></td>
                            </tr>
						<tr>
						<td class="myLabelIzquierda">Estado:</td>
						<td>
                            <asp:RadioButtonList ID="rdbEstado" runat="server" RepeatDirection="Horizontal" 
                                TabIndex="12">
                                <asp:ListItem Value="0" Selected="True">Pendientes de enviar</asp:ListItem>
                                <asp:ListItem Value="1">Enviados</asp:ListItem>
                                <asp:ListItem Value="2">Marcado como No enviados</asp:ListItem>
                            </asp:RadioButtonList>
                                        
					</tr>
						<tr>
						<td   colspan="2"><hr /></td>
						<tr>
						<td   colspan="2" align="right">
                                           <asp:Button ID="btnBuscar" runat="server" CssClass="myButton" 
                                               onclick="btnBuscar_Click" Text="Buscar" ValidationGroup="0" />
                                           </td>
						<tr>
						<td   colspan="2">
                                           <asp:Panel ID="pnlHojaTrabajo" runat="server" Visible="False">
                                            <img alt="" src="../App_Themes/default/images/pdf.jpg"/>&nbsp;<asp:LinkButton 
                            ID="lnkPDF" runat="server" CssClass="myLittleLink" onclick="lnkPDF_Click" ValidationGroup="0" 
                                                   TabIndex="8" Visible="False">Visualizar 
                                               en formato pdf</asp:LinkButton><br />
                         <img alt="" src="../App_Themes/default/images/imprimir.jpg"/>&nbsp;<asp:LinkButton 
                            ID="lnkImprimir" runat="server" CssClass="myLittleLink" onclick="lnkImprimir_Click" 
                                               ValidationGroup="0" TabIndex="9" Visible="False">Imprimir</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           </asp:Panel>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                HeaderText="Debe completar los siguientes datos:" ShowMessageBox="True" 
                                                ValidationGroup="0" ShowSummary="False" />
                        </td>
						
					</tr>
					</table>						
 </div>
 </asp:Content>