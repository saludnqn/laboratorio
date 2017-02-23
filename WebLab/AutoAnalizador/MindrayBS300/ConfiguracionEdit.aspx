<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfiguracionEdit.aspx.cs" Inherits="WebLab.AutoAnalizador.MindrayBS300.ConfiguracionEdit" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">



   
</asp:Content>
 
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          
  
	<br />   &nbsp;
    	<div align="left" style="width: 750px">
		
<table align="center" width="700" >
<tr>
						<td class="myLink" colspan="2" >
                            </td>
						
					</tr>
<tr>
						<td class="mytituloTabla" colspan="2" >
                                            <img src="../../App_Themes/default/images/mindray.jpg" />          </td>
						
					</tr>
<tr>
						<td class="mytituloGris" colspan="2" >
                                  Configuración LIS - Equipo Mindray BS-300<hr /></td>
						
					</tr>
<tr>
						<td class="myLabelIzquierda" style="width: 115px" >
                                            Area:</td>
						<td style="width: 475px">
                            <anthem:DropDownList ID="ddlArea" runat="server" CssClass="myList" 
                                AutoCallBack="True" onselectedindexchanged="ddlArea_SelectedIndexChanged"></anthem:DropDownList>
                                        
                        </td>
						
					</tr>
<tr>
						<td class="myLabelIzquierda" style="width: 115px" >
                                            Análisis del LIS:<asp:RangeValidator ID="RangeValidator1" runat="server" 
                                ControlToValidate="ddlItem" ErrorMessage="*" MaximumValue="999999" 
                                MinimumValue="1" Type="Integer" ValidationGroup="0"></asp:RangeValidator>
                                        
                        </td>
						<td style="width: 475px">
                                        
                            <anthem:DropDownList ID="ddlItem" runat="server" CssClass="myList" 
                                
                               >
                            </anthem:DropDownList>
                                        
                        </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" style="width: 115px" >
                                            Orden en Equipo:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                runat="server" ControlToValidate="txtOrden" ErrorMessage="*" 
                                ValidationGroup="0"></asp:RequiredFieldValidator>
                                        
                                                </td>
						<td style="width: 475px">
                            <asp:TextBox ID="txtOrden" runat="server" CssClass="myTexto" Width="60px" 
                                MaxLength="50"></asp:TextBox>
                                        
                                                </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" style="vertical-align: top; width: 115px;" >
                                            Tipo de Muestra:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="rdbMuestra" ErrorMessage="*" ValidationGroup="0"></asp:RequiredFieldValidator>
                        </td>
						<td style="width: 475px">
                            <asp:RadioButtonList ID="rdbMuestra" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Suero</asp:ListItem>
                                <asp:ListItem>Plasma</asp:ListItem>
                                <asp:ListItem>Orina</asp:ListItem>
                                <asp:ListItem>Otros</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" style="vertical-align: top; width: 115px;" >
                                            Prefijo especial:</td>
						<td class="myLabelIzquierda"  style="width: 475px">
                            <asp:TextBox ID="txtPrefijo" runat="server" CssClass="myTexto" Width="60px" 
                                MaxLength="10"></asp:TextBox>
                                        <br />
                                                &nbsp;(Sólo para casos como Glucemia Post ingesta, Glucemia Post Almuerzo, etc.)</td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" style="width: 115px" >
                                            &nbsp;</td>
						<td style="width: 475px">
                                              <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                                  onclick="btnGuardar_Click2" CssClass="myButton" ValidationGroup="0" />
                        </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" colspan="2" >
                                              <asp:Label ID="lblMensajeValidacion" runat="server" 
                                                  ForeColor="#FF3300"></asp:Label>
                        </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" colspan="2" >
                                           <hr /></td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" colspan="2" >
					
						<div style="border: 1px solid #999999; height: 600px; width:610px; overflow: scroll;">
                                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" Width="590px" 
                                    DataKeyNames="idMindrayItem" onrowcommand="gvLista_RowCommand" 
                                    onrowdatabound="gvLista_RowDataBound" 
                                    onselectedindexchanged="gvLista_SelectedIndexChanged" 
                                    EmptyDataText="No se configuraron prácticas para el equipo.">
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="idMindray" HeaderText="Orden en Equipo" />
                                        <asp:BoundField DataField="tipoMuestra" HeaderText="Tipo de Muestra" />
                                        <asp:BoundField DataField="prefijo" HeaderText="Prefijo Especial" />
                                       
                                         <asp:TemplateField HeaderText="Habilitado">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStatus" runat="server" 
                            AutoPostBack="true" OnCheckedChanged="chkStatus_OnCheckedChanged"
                            Checked='<%# Convert.ToBoolean(Eval("Habilitado")) %>'
                            />
                    </ItemTemplate>                    
                </asp:TemplateField>
                                                    
                                                    
                                        <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Eliminar" runat="server" CommandName="Eliminar" 
                                                        ImageUrl="../../App_Themes/default/images/eliminar.jpg" 
                                                        OnClientClick="return PreguntoEliminar();" />
                                                </ItemTemplate>
                                                <ItemStyle Height="20px" HorizontalAlign="Center" Width="20px" />
                                            </asp:TemplateField>
                                    </Columns>
                                    
                                    <HeaderStyle BackColor="#999999" />
                                </asp:GridView>
					</div>
					
					    </td>
						
					</tr>
					</table>
		
		</div>				      
                            
                      
 <script language="javascript" type="text/javascript">

    
    function PreguntoEliminar()
    {
    if (confirm('¿Está seguro de eliminar?'))
    return true;
    else
    return false;
    }
    </script>
 </asp:Content>
