<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralFiltro.aspx.cs" Inherits="WebLab.Estadisticas.GeneralFiltro" MasterPageFile="~/Site1.Master" %>
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
                 <table  width="800px" align="center" 
                     
                  
                    
                     cellpadding="1" cellspacing="1" class="myTabla" >
					<tr>
						<td style="width: 203px"><b class="mytituloTabla">
                            <asp:Label ID="lblTitulo" runat="server" Text="REPORTES ESTADISTICOS"></asp:Label></b></td>
						<td align="right" style="width: 490px"><a href="../Help/Documentos/Estadisticas Generales.htm" title="Ayuda" target="_blank"><img style="border:0" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
					<tr>
						<td colspan="2" align="right"><hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" colspan="2">
                    <anthem:RadioButtonList ID="rdbTipoInforme" runat="server" AutoCallBack="True" 
                                onselectedindexchanged="rdbTipoInforme_SelectedIndexChanged" 
                                CssClass="mytituloGris" RepeatColumns="3">
                             
                                <Items>
<asp:ListItem Value="0">Conteo por Areas</asp:ListItem>
<asp:ListItem Value="1">Conteo por Análisis</asp:ListItem>
<asp:ListItem Value="4">Conteo por Derivaciones</asp:ListItem>
                                    <asp:ListItem Value="6">Conteo por Diagnósticos</asp:ListItem>
<asp:ListItem Value="3">Conteo por Efector Solicitante</asp:ListItem>
<asp:ListItem Value="2">Conteo por Médico Solicitante</asp:ListItem>
                                    <asp:ListItem Value="8">Conteo por Sector Solicitante</asp:ListItem>
                                    <asp:ListItem Value="7">Protocolos por Dia</asp:ListItem>
                                    <asp:ListItem Value="9">Ranking por día de la semana</asp:ListItem>
<asp:ListItem Value="5">Totalizado Resumido</asp:ListItem>
</Items>
                            </anthem:RadioButtonList>
                                        
                                            <anthem:RequiredFieldValidator 
                                ID="rfvTipoInforme" runat="server" ControlToValidate="rdbTipoInforme" 
                                ErrorMessage="Tipo de reporte" ValidationGroup="0">*</anthem:RequiredFieldValidator>
                                </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" colspan="2">
                            <anthem:Label ID="lblDescripcion" runat="server" CssClass="alert" 
                                Visible="False" Height="100%"></anthem:Label>
                                        
                                </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" colspan="2"><hr /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" style="width: 203px">Fecha Desde:</td>
						<td style="width: 490px">
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de inicio"  />&nbsp;&nbsp;&nbsp;
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" style="width: 203px">Fecha Hasta:</td>
						<td style="width: 490px">
                            <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="4" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de fin"  /><asp:CustomValidator ID="CustomValidator1" runat="server" 
                                ErrorMessage="Debe ingresar un rango de fechas valido" 
                                onservervalidate="CustomValidator1_ServerValidate" ValidationGroup="0">Debe 
                            ingresar un rango de fechas valido</asp:CustomValidator>
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" style="width: 203px">Servicio:</td>
						<td style="width: 490px">
                            <anthem:DropDownList ID="ddlServicio" runat="server" 
                                ToolTip="Seleccione el servicio" TabIndex="1" CssClass="myList" 
                                AutoCallBack="True" onselectedindexchanged="ddlServicio_SelectedIndexChanged">
                            </anthem:DropDownList>
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" style="width: 203px">Area:</td>
						<td style="width: 490px">
                            <anthem:DropDownList ID="ddlArea" runat="server" 
                                ToolTip="Seleccione el area" TabIndex="1" CssClass="myList">
                            </anthem:DropDownList>
                                        
                                            </td>
					</tr>
						<tr>
						<td class="myLabelIzquierda" style="width: 203px">Efector Solicitante:</td>
						<td style="width: 490px">
                            <anthem:DropDownList ID="ddlEfector" runat="server" 
                                ToolTip="Seleccione el area" TabIndex="1" CssClass="myList">
                            </anthem:DropDownList>
                                        
                        </tr>
						<tr>
						<td class="myLabelIzquierda" style="width: 203px">Medico Solicitante:</td>
						<td style="width: 490px">
                            <anthem:DropDownList ID="ddlEspecialista" runat="server" 
                                ToolTip="Seleccione el area" TabIndex="1" CssClass="myList">
                            </anthem:DropDownList>
                                        
                        </tr>
						<tr>
						<td class="myLabelIzquierda" style="width: 203px">Agrupación:</td>
						<td style="width: 490px">
                            <asp:RadioButtonList ID="rdbAgrupacion" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">Por Origen</asp:ListItem>
                                <asp:ListItem Value="1">Por Prioridad</asp:ListItem>
                            </asp:RadioButtonList>
                        </tr>
                            <%-- <img alt="" src="../App_Themes/default/images/pdf.jpg"/>&nbsp;<asp:LinkButton 
                            ID="lnkPdf" runat="server" CssClass="myLittleLink" onclick="lnkPDF_Click" ValidationGroup="0">Visualizar 
                        en formato pdf</asp:LinkButton><br>      <img alt="" src="../App_Themes/default/images/imprimir.jpg"/>&nbsp;<asp:LinkButton 
                            ID="lnkImprimir" runat="server" CssClass="myLittleLink" onclick="lnkImprimir_Click" 
                                               ValidationGroup="0">Imprimir</asp:LinkButton>
                                             <br />
                                             <img alt="" src="../App_Themes/default/images/excelPeq.gif"/>&nbsp;     
                                               <asp:LinkButton ID="lnkExcel" runat="server" CssClass="myLittleLink" 
                                                   onclick="lnkExcel_Click" ValidationGroup="0">Visualizar en formato Excel</asp:LinkButton>    --%>
						<tr>
						<td class="myLabelIzquierda" style="width: 203px">Generar Gráfico:</td>
						<td style="width: 490px">
                    <anthem:RadioButtonList ID="rdbGrafico" runat="server" 
                                onselectedindexchanged="rdbTipoInforme_SelectedIndexChanged" 
                                RepeatDirection="Horizontal">
                             
                                <Items>
<asp:ListItem Value="0" Selected="True">Si</asp:ListItem>
<asp:ListItem Value="1">No</asp:ListItem>
</Items>
                            </anthem:RadioButtonList>
                                        
                        </tr>
                            <tr>
						<td class="myLabelIzquierda" style="width: 203px">Protocolos:</td>
						<td style="width: 490px">
                            <asp:RadioButtonList ID="rdbEstado" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="-1" Selected="True">Todos</asp:ListItem>
                                <asp:ListItem Value="0">No Procesados</asp:ListItem>
                                <asp:ListItem Value="1">En Proceso</asp:ListItem>
                                <asp:ListItem Value="2">Terminados</asp:ListItem>
                            </asp:RadioButtonList>
                        </tr>
                            <tr>
						<td class="myLabelIzquierda" colspan="2"><hr /></td>
						<tr>
						<td colspan="2" align="right">
                           <asp:Button ID="btnGenerar" runat="server" CssClass="myButton" 
                                onclick="btnGenerar_Click" Text="Generar Reporte" Width="131px" 
                                ValidationGroup="0" />&nbsp;&nbsp;
                                
                                </td>
						<tr>
						<td   colspan="2">
                                        
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" 
                                                ValidationGroup="0" ShowSummary="False" />
                        </td>
						
					</tr>
					<tr>
						<td   colspan="2">
                            &nbsp;</td>
						
					</tr>
					</table>						
					
					</div>
 
 </asp:Content>
