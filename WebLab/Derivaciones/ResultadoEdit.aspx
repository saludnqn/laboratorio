﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultadoEdit.aspx.cs" Inherits="WebLab.Derivaciones.ResultadoEdit" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">               
<br />   &nbsp;
    
  <div align="left" style="width:1000px">
                 <table  width="1000px"                                            
                     style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal; color: #000000" 
                     cellpadding="1" cellspacing="1" align="center">										
					<tr>
						<td>
                            <asp:Label ID="lblTitulo" CssClass="mytituloTabla" runat="server" Text="CARGA DE RESULTADOS DERIVADOS" ></asp:Label></td>
						<td align="right">
                                                   &nbsp;</td>
					</tr>																
					<tr>
						<td class="myLabel" style="vertical-align: top" colspan="4">
                                            <hr class="hrTitulo" /></td>
						
						
					</tr>																
					<tr>
						<td class="myLabelIzquierda" style="vertical-align: top">
                                            Practica:&nbsp;
                                            <asp:DropDownList ID="ddlItem" runat="server" 
                                               
                                                CssClass="myLabel">
                                            </asp:DropDownList>
                        </td>
						
					<td class="myLabelIzquierda" style="vertical-align: top">
                                            Efector a derivar:
                                            <asp:DropDownList ID="ddlEfector" runat="server" 
                                              
                                                CssClass="myLabel">
                                            </asp:DropDownList>
                        </td>
						
						<td class="myLabelIzquierda">
                                            Estado:
                                            <asp:DropDownList ID="ddlEstado" runat="server"                                                 
                                                CssClass="myLabel" >
                                                <asp:ListItem Selected="True" Value="0">Pendiente</asp:ListItem>
                                                <asp:ListItem Value="1">Con Resultado</asp:ListItem>
                                                <asp:ListItem Value="-1">Todos</asp:ListItem>
                                            </asp:DropDownList></td>
						
						
						<td class="myLabel">
                                            <asp:Button ID="Button1" runat="server" CssClass="myButton" onclick="Button1_Click" Text="Buscar" />
                        </td>
						
						
					</tr>																
					<tr>
						<td colspan="4">
                            <hr /></td>
					</tr>																											
                   <tr>																											
						<td  colspan="4" style="vertical-align: top">                           												        
   
						                     
						                           <asp:Panel ID="Panel1" runat="server" style="overflow: scroll; height: 600px;" Width="1100px">                                                                                                                
                                               <asp:Table ID="tContenido" 
                                            
                   
                                                   Runat="server"  CssClass="mytable1" 
                                                   Width="990px" ></asp:Table></asp:Panel>
                                           
                                         </td>
						
                                          


				</tr>		
					<tr>
						
						<td style="vertical-align: top" align="left" colspan="4">        
                           <hr /></td>
						
					</tr>
					<tr>
						
						<td style="vertical-align: top" align="left">        
    <asp:HyperLink ID="hypRegresar" runat="server" CssClass="myLink">Regresar</asp:HyperLink>
                                         </td>
						
						<td colspan="3" style="vertical-align: top" align="right">        
                            <asp:Button ID="btnGuardar" runat="server" CssClass="myButton" Text="Guardar" 
                                onclick="btnGuardar_Click" ValidationGroup="0" />
                                         </td>
						
					</tr>
					</table>
						
									
 </div>
</asp:Content>