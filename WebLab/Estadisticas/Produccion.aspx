<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produccion.aspx.cs" Inherits="WebLab.Estadisticas.Produccion1" MasterPageFile="~/Site1.Master" %>

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
    <style type="text/css">





    </style>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<br />&nbsp;&nbsp;<br />
         
                 <table  width="800px" align="center" cellpadding="1" cellspacing="1" class="myTabla" >
					<tr>
						<td colspan="3">
						<%--<a href="../Help/Documentos/Estadisticas Generales.htm" title="Ayuda" target="_blank"><img style="border:0" src="../App_Themes/default/images/information.png" /></a>--%>
						</td>
					</tr>
					<tr>
						<td colspan="3"><b class="mytituloTabla">
                            <asp:Label ID="lblTitulo" runat="server" Text="ESTADISTICAS DE PRODUCCION"></asp:Label></b></td>
					</tr>
		
					<tr>
						<td class="myLabelIzquierda" colspan="3"><hr /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Fecha Desde:</td>
						<td class="myLabelIzquierda" >&nbsp;</td>
						<td >
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de inicio"  />&nbsp;&nbsp;&nbsp;
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Fecha Hasta:</td>
						<td class="myLabelIzquierda" >&nbsp;</td>
						<td >
                            <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="4" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de fin"  /><asp:CustomValidator ID="CustomValidator1" runat="server" 
                                ErrorMessage="Debe ingresar un rango de fechas valido" 
                                onservervalidate="CustomValidator1_ServerValidate" ValidationGroup="0">Debe 
                            ingresar un rango de fechas valido</asp:CustomValidator>
                                        <hr />
                                            </td>
					</tr>
				
					<tr>
						<td class="myLabelIzquierda" style="vertical-align: top" >Sector/Servicio:</td>
						<td class="myLabelIzquierda" style="vertical-align: top" >&nbsp;</td>
						<td>
                                                           <asp:ListBox ID="lstSector" runat="server" 
                                CssClass="myTexto" Height="190px" 
                                                               SelectionMode="Multiple" TabIndex="11" 
                                ToolTip="Seleccione los sectores" Width="350px"></asp:ListBox>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                               ControlToValidate="lstSector" ErrorMessage="Sector/Servicio" 
                                                               ValidationGroup="0">*</asp:RequiredFieldValidator>
                                <hr />
                                            </td>
					</tr>
				
					<tr>
						<td class="myLabelIzquierda" style="vertical-align: top" >Origen a incluir:
                            
                        </td>
						<td class="myLabelIzquierda" style="vertical-align: top" >&nbsp;&nbsp;</td>
						<td align="left">
                            <div class="mylabelizquierda">
                                Seleccionar:
                                <asp:LinkButton ID="lnkMarcar" runat="server" CssClass="myLink" 
                                    onclick="lnkMarcar_Click">Todas</asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="lnkDesmarcar" runat="server" CssClass="myLink" 
                                    onclick="lnkDesmarcar_Click">Ninguna</asp:LinkButton>
                            </div>
                            <asp:CheckBoxList ID="ChckOrigen" runat="server" RepeatDirection="Vertical" 
                                RepeatColumns="5">
                            </asp:CheckBoxList>
                                        <asp:CustomValidator ID="cvOrigen" runat="server" 
                                ErrorMessage="Debe seleccionar al menos un origen" 
                                onservervalidate="cvOrigen_ServerValidate" ValidationGroup="0">Debe seleccionar al menos un origen</asp:CustomValidator>
                                        <hr />
                                            </td>
					</tr>
				
					<tr>
						<td class="myLabelIzquierda" style="vertical-align: top;">Areas a incluir:
                              </td>
						<td class="myLabelIzquierda" style="vertical-align: top;">&nbsp;</td>
						<td align="left" >
                            <div class="mylabelizquierda">
                                Seleccionar:
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="myLink" 
                                    onclick="LinkButton1_Click">Todas</asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="myLink" 
                                    onclick="LinkButton2_Click">Ninguna</asp:LinkButton>
                            </div>
                            <asp:CheckBoxList ID="ChckArea" RepeatDirection="Vertical" runat="server" 
                                RepeatColumns="4">
                            </asp:CheckBoxList>
                                        
                                        <asp:CustomValidator ID="cvOrigen0" runat="server" 
                                ErrorMessage="Debe seleccionar al menos un area" 
                                onservervalidate="cvOrigen0_ServerValidate" ValidationGroup="0">Debe seleccionar al menos un area</asp:CustomValidator>
                                        
                                            </td>
					</tr>
				
                            <tr>
						<td class="myLabelIzquierda" colspan="3"><hr /></td>
                        </tr>
						<tr>
						<td align="left">
                            &nbsp;&nbsp;<asp:ImageButton ID="imgExcel" runat="server" 
                                ImageUrl="~/App_Themes/default/images/excelPeq.gif" onclick="imgExcel_Click" 
                                ToolTip="Exportar a Excel" ValidationGroup="0" />
                                
                                </td>
						<td align="left">
                            &nbsp;</td>
						<td align="right">
                           <asp:Button ID="btnGenerar" runat="server" CssClass="myButton" 
                                onclick="btnGenerar_Click" Text="Generar Reporte" Width="131px" 
                                ValidationGroup="0" />
                                
                                </td>
                                </tr>
						<tr>
						<td   colspan="3">
                                        
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" 
                                                ValidationGroup="0" ShowSummary="False" HeaderText="Debe seleccionar:" />
                                            <br />
                               <div class="myLabelRojo">           *Las prácticas procesadas son las validadas.</div></td>
						
					</tr>
					
						<tr>
						<td   colspan="3" align="left">
                                        
                                            &nbsp;</td>
						
					</tr>
					
					</table>
                    <br />&nbsp;&nbsp;<br />						
 
 </asp:Content>