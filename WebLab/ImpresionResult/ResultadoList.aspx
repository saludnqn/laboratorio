<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultadoList.aspx.cs" Inherits="WebLab.ImpresionResult.ProtocoloList" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>



<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>



<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript" src="../script/Mascara.js"></script>
    <script type="text/javascript" src="../script/ValidaFecha.js"></script>   
  
 

   
</asp:Content>
 
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          

<br />   &nbsp;
    
				 <table  width="1000px" align="center" 
                     
                     
                     
                     cellpadding="1" cellspacing="1" >
					
					<tr>
						<td   colspan="2"><b class="mytituloTabla">LISTA DE PROTOCOLOS</b>
                                            </td>
						
						<td   colspan="2" align="right">
                                   <asp:Label ID="lblServicio" runat="server" CssClass="myLabelIzquierdaGde" 
                                       Text="Label"></asp:Label>
                                            </td>
						
					</tr>
					
					<tr>
						<td   colspan="4"><hr /></td>
						
					</tr>
					<tr>
						<td   style="color: #333333; font-size: 12px">
                                       </td>
						<td align="right" style="color: #333333; font-size: 12px" colspan="3">
                                           <table style="font-size: 9px; width: 250px;">
                                                
                        <tr>
                        <td class="myLabelLitlle"  style="vertical-align: top" ><img src="../App_Themes/default/images/rojo.gif" />&nbsp;No Procesado</td>
                        <td  class="myLabelLitlle" style="vertical-align: top">&nbsp;</td>
                        <td class="myLabelLitlle"  style="vertical-align: top"><img src="../App_Themes/default/images/amarillo.gif"   />&nbsp;En Proceso</td>
                          <td class="myLabelLitlle" style="vertical-align: top">&nbsp;</td>
                          <td class="myLabelLitlle"  style="vertical-align: top" ><img src="../App_Themes/default/images/verde.gif" />&nbsp;Terminado</td>
                        </tr>
                        
                        </table></td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda">
                                            Acciones:    &nbsp;<asp:LinkButton 
                            ID="lnkPDF" runat="server" CssClass="myLittleLink" onclick="lnkPDF_Click" ValidationGroup="0">Visualizar 
                        en formato pdf</asp:LinkButton>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton 
                            ID="lnkMarcarImpresos" runat="server" CssClass="myLittleLink" onclick="lnkMarcarImpresos_Click" 
                                                   ValidationGroup="0">Marcar como Impresos</asp:LinkButton> </td>
						<td align="right" style="color: #333333; font-size: 12px" colspan="2">
                                            <asp:CheckBox ID="chkAnalisisResultados" runat="server" 
                                                CssClass="myLabel" Text="Imprimir sólo análisis con resultados" 
                                                Checked="True" />
                        </td>
						
						<td align="right" style="color: #333333; font-size: 12px">
                                            <asp:CheckBox ID="chkAnalisisValidados" runat="server" 
                                                CssClass="myLabel" Text="Imprimir sólo análisis validados" 
                                                Checked="True" />
                        </td>
						
					</tr>
					
					
						<tr>
						<td colspan="4">
						<table width="100%" align="center" style="border: 1px solid #C0C0C0">
 	<tr>
						<td class="myLabelIzquierda" align="left" style="width: 140px" >
                                        Impresora del sistema: </td>
						<td  align="left">
                                        <asp:DropDownList ID="ddlImpresora" runat="server" CssClass="myLabel" >
                                        </asp:DropDownList>
                            </td>
                            <td align="right">
                            <asp:LinkButton ID="lnkImprimir" runat="server" CssClass="myLittleLink" onclick="lnkImprimir_Click"  ValidationGroup="0">Imprimir</asp:LinkButton>
                            </td>
						
                                        </tr>
                                        </table>
                                        </td>
					</tr>	
					
					
					<tr>
						<td colspan="4">
						<hr />
                                        </td>
					</tr>
						<tr>
						<td   style="color: #333333; font-size: 12px" colspan="2">
                                              <div class="mylabelizquierda"> Seleccionar:                                           <asp:LinkButton 
                            ID="lnkMarcar" runat="server" CssClass="myLink" onclick="lnkMarcar_Click" 
                                                   ValidationGroup="0">Todas</asp:LinkButton>&nbsp;
                                            <asp:LinkButton 
                            ID="lnkDesmarcar" runat="server" CssClass="myLink" onclick="lnkDesmarcar_Click" 
                                                   ValidationGroup="0">Ninguna</asp:LinkButton></div></td>
						
						<td   style="color: #333333; font-size: 12px" colspan="2" align="right">
                                           <asp:Label ID="CantidadRegistros" runat="server"  
                                                              forecolor="Blue" Font-Size="8pt" />
                                                            </td>
						
					</tr>
					<tr>
						<td   colspan="4">
						<div  style="width:100%;height:450pt;overflow:scroll;;overflow-x:hidden;border:1px solid #CCCCCC; background-color: #F3F3F3;"> 
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" DataKeyNames="idProtocolo" Font-Size="9pt" 
                                                Width="98%" CellPadding="1" 
                                ForeColor="#666666" PageSize="40" 
                                
                                
                                
                                EmptyDataText="No se encontraron protocolos para los parametros de busqueda ingresados" 
                                onrowdatabound="gvLista_RowDataBound" BorderColor="#3A93D2" 
                                BorderStyle="Solid" BorderWidth="3px" GridLines="Horizontal" 
                                AllowPaging="False" onpageindexchanging="gvLista_PageIndexChanging">
            <RowStyle BackColor="White" ForeColor="#333333" Font-Names="Arial" 
                Font-Size="8pt" />
            <Columns>
                <asp:TemplateField HeaderText="Sel." >
                                                        <ItemTemplate>
                                                         <asp:CheckBox ID="CheckBox1" runat="server" EnableViewState="true" />
                                                     </ItemTemplate>
                                                     <ItemStyle Width="5%" 
                                                            HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                <asp:BoundField DataField="impreso" />
          <asp:BoundField DataField="numero" 
                    HeaderText="Protocolo" >
                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fecha" HeaderText="Fecha" >
                    <ItemStyle Width="8%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="dni" HeaderText="DNI" >
                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="paciente" HeaderText="Apellidos y Nombres">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="origen" HeaderText="Origen">
                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prioridad" HeaderText="Prioridad">
                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sector" HeaderText="Sector">
                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="estado" HeaderText=" " >
            

                 
                
                        
         
                        
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            

                 
                
                        
         
                        
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#3A93D2" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#3A93D2" Font-Bold="False" ForeColor="White" 
                Font-Names="Arial" Font-Size="8pt" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        </div>
                        </td>
						
					</tr>
				
					<tr>
						<td colspan="4">
						<hr />
                                        </td>
					</tr>
				
					<tr>
						<td align="left">       
                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                onclick="lnkRegresar_Click">Regresar</asp:LinkButton>
                          </td>
						<td align="right" colspan="3">       
                              &nbsp;</td>
					</tr>
					</table>
						
<br />		



   
 
    </table>
   
 
 </asp:Content>
