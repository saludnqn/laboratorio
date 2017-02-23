﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RutinaEdit.aspx.cs" Inherits="WebLab.Items.ItemGrupo" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <title>LABORATORIO</title>    
    
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <br />   &nbsp;
        <div align="left" style="width:1000px">
     <table  width="600px" align="center" class="myTabla">
					<tr>
						<td><b class="mytituloTabla">RUTINA</b></td>
					
						<td align="right"> <a href="../Help/Documentos/Rutinas.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
					
					</tr>
					<tr>
						<td   colspan="2" >    <hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Tipo de Servicio:</td>
						<td align="left" >
                            <anthem:DropDownList ID="ddlServicio" runat="server" AutoCallBack="True" 
                                                onselectedindexchanged="ddlServicio_SelectedIndexChanged" 
                                CssClass="myList" TabIndex="1" ToolTip="Seleccione el servicio">
                                            </anthem:DropDownList>
                            <asp:RangeValidator ID="rvTipoServicio" runat="server" 
                                ControlToValidate="ddlServicio" ErrorMessage="Tipo de Servicio" 
                                MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">*</asp:RangeValidator>
                        </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Nombre de Rutina:</td>
						<td align="left" >
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="myTexto" Width="90%" 
                                TabIndex="2" ToolTip="Ingrese el nombre de la rutina"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                                ControlToValidate="txtNombre" ErrorMessage="Nombre" ValidationGroup="0">*</asp:RequiredFieldValidator>
                        </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" >&nbsp;</td>
						<td align="left" >
                            <asp:CheckBox ID="chkPeticionElectronica" runat="server" 
                                Text="Solo para peticion electronica" />
                        </td>
						
					</tr>
					<tr>
						<td colspan="2">&nbsp;</td>
						
					</tr>
					<tr>
						<td    style="vertical-align: top" colspan="2">
						
						<fieldset id="Fieldset1" title="Determinaciones" style="width:95%; text-align:left; ">
						<legend>Analisis</legend>

						<table align="left" width="100%">
						<tr>
						<td class="myLabelIzquierda" >	   Codigo:				</td>
						<td>		  
                            <anthem:TextBox 
                                ID="txtCodigo" runat="server" AutoCallBack="True" 
                                ontextchanged="txtCodigo_TextChanged1" Width="95px" 
                                CssClass="myTexto" TabIndex="3" Enabled="False" 
                                ToolTip="Codigo del analisis"></anthem:TextBox>  				</td>
						</tr>
						<tr>
						<td class="myLabelIzquierda" >     Nombre:
						</td>
						<td>   
                            <anthem:DropDownList ID="ddlItem" runat="server" AutoCallBack="True" 
                                                onselectedindexchanged="ddlItem_SelectedIndexChanged" 
                                                TextDuringCallBack="Cargando ..." 
                                CssClass="myList" TabIndex="5" Enabled="False" 
                                ToolTip="Seleccione el analisis">
                                            </anthem:DropDownList></td>
						</tr>
						<tr>
						<td colspan="2">   
                            <anthem:Button ID="btnAgregar" runat="server" 
                                                onclick="btnAgregar_Click" Text="Agregar" Width="70px" 
                                CssClass="myButton" TabIndex="22" /></td>
						</tr>
						<tr>
						<td colspan="2">   
						
                                <asp:CustomValidator ID="cvListaDeterminaciones" runat="server" 
                                    ErrorMessage="Determinaciones" ValidationGroup="0" 
                                        onservervalidate="cvListaDeterminaciones_ServerValidate1">Debe 
                                ingresar al menos un analisis</asp:CustomValidator>
						
                             </td>
						</tr>
						<tr>
						<td colspan="2">   
						
                                <anthem:GridView ID="gvLista" runat="server" 
                                onrowdatabound="gvLista_RowDataBound1" AutoGenerateColumns="False" 
                                CellPadding="0" ForeColor="#333333" 
                                onrowcommand="gvLista_RowCommand" Width="100%" 
                                EmptyDataText="Agregue los analisis correspondientes" 
                                BorderColor="#3A93D2" BorderStyle="Solid" BorderWidth="2px" Font-Size="8pt" 
                                GridLines="Horizontal">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#3A93D2" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Analisis" >
                                        <ItemStyle Width="95%" />
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Eliminar" runat="server" ImageUrl="~/App_Themes/default/images/eliminar.jpg"
                            CommandName="Eliminar" />
                            </ItemTemplate>
                          
                            <ItemStyle HorizontalAlign="Center" Width="20px" Height="18px" />
                          
                        </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <RowStyle BackColor="#EFF3FB" />
                            </anthem:GridView>
                            
                                </td>
						</tr>
						</table>
                             </td>
						</tr>
					<tr>
						<td   colspan="2">
                                            <hr /></td>
						
					</tr>
					<tr>
						<td align="left">
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="RutinaList.aspx">Regresar</asp:LinkButton>
                                        
                        </td>
						
						<td align="right">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                HeaderText="Debe completar los datos marcados como requeridos:" 
                                                ShowMessageBox="True" ValidationGroup="0" ShowSummary="False" />
                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="0" 
                                                onclick="btnGuardar_Click" CssClass="myButton" TabIndex="24" />
                                        
                        </td>
						
					</tr>
					</table>
  </div>
 </asp:Content>


