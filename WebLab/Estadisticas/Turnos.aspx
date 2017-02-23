<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Turnos.aspx.cs" Inherits="WebLab.Estadisticas.Turnos" MasterPageFile="~/Site1.Master" %>

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
<script language="Javascript" type="text/javascript" src="../FusionCharts/FusionCharts.js"></script>
<br />   &nbsp;
     <div align="left" style="width:1000px">
                 <table  width="600px" align="center" 
                     
                  
                    
                     cellpadding="1" cellspacing="1" class="myTabla">
					<tr>
						<td colspan="2"><b class="mytituloTabla">
                            reporte estadistico de turnos</b>    <hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Fecha Desde:</td>
						<td>
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de inicio"  />&nbsp;&nbsp;&nbsp;
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">Fecha Hasta:</td>
						<td>
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
						<td class="myLabelIzquierda">Servicio:</td>
						<td>
                            <asp:DropDownList ID="ddlServicio" runat="server">
                            </asp:DropDownList>
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">&nbsp;</td>
						<td align="right">
                           <asp:Button ID="btnGenerar" runat="server" CssClass="myButton" 
                                onclick="btnGenerar_Click" Text="Ver Reporte" Width="131px" 
                                ValidationGroup="0" />
                                        
                                            </td>
					</tr>
                            <tr>
						<td class="myLabelIzquierda" colspan="2"><hr /></td>
						<tr>
						<td   colspan="2" style="vertical-align: top">
                            <asp:Panel ID="pnlSinDatos" runat="server" Visible="False">
                                <asp:Label ID="Label1" runat="server" 
                                    Text="No se encontraron datos para los filtros de busqueda ingresados" 
                                    CssClass="myLabel"></asp:Label>
                            </asp:Panel>
                        </td>
						
					</tr>
					<tr>
						<td   colspan="2" style="vertical-align: top">
                             <asp:Panel ID="pnlDatos" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                                Visible="False">
                                 <table style="width:100%;">
                                     <tr>
                                         <td style="vertical-align: top">
                                             <asp:GridView ID="gvLista" runat="server" CellPadding="4" ForeColor="#333333" 
                                                 Width="150px" GridLines="None">
                                                 <RowStyle BackColor="#EFF3FB" />
                                                 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                 <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                 <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                 <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                 <EditRowStyle BackColor="#2461BF" />
                                                 <AlternatingRowStyle BackColor="White" />
                                             </asp:GridView>
                                         </td>
                                         <td>
                                             <asp:Literal ID="FCLiteral" runat="server"></asp:Literal>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td colspan="2">
                                             <asp:ImageButton ID="imgPdf" runat="server" 
                                                 ImageUrl="~/App_Themes/default/images/pdf.jpg" onclick="imgPdf_Click" 
                                                 ToolTip="Exportar a Pdf" />
                                             &nbsp;&nbsp;&nbsp;
                                             <asp:ImageButton ID="imgExcel" runat="server" 
                                                 ImageUrl="~/App_Themes/default/images/excelPeq.gif" onclick="imgExcel_Click" 
                                                 ToolTip="Exportar a Excel" />
                                         </td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                        </td>
						
					</tr>
					<tr>
						<td   colspan="2">
                            &nbsp;</td>
						
					</tr>
					</table>						
					
					</div>
 
 </asp:Content>