﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RutinaList.aspx.cs" Inherits="WebLab.Items.ItemRutinasList" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
   	<br />   &nbsp;    <div align="left" style="width:1000px">
       
	   
		<table width="600px" align="center" class="myTabla">
			<tr>
			
				<td ><b class="mytituloTabla">LISTA DE RUTINAS</b>
                    </td>
			
			<td align="right"> <a href="../Help/Documentos/Rutinas.htm" target="_blank"  > <img style="border:none;" alt="Ayuda"  title="Ayuda en linea" src="../App_Themes/default/images/information.png" /></a></td>
			</tr>
			<tr>
				<td colspan="2" >
                                    <hr class="hrTitulo" /></td>
			</tr>
			<tr>
				<td colspan="2">

	

   
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" DataKeyNames="idRutina" 
            ForeColor="#333333" onrowcommand="gvLista_RowCommand1" 
            onrowdatabound="gvLista_RowDataBound" Font-Size="8pt" Width="100%" 
                        EmptyDataText="No hay rutinas creadas" BorderColor="#3A93D2" 
                        BorderStyle="Solid" BorderWidth="3px" GridLines="None">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="8pt" />
            <Columns>
             <asp:BoundField DataField="servicio" 
                    HeaderText="Servicio" >
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Rutina" >
                    <ItemStyle Width="80%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Modificar" runat="server" ImageUrl="~/App_Themes/default/images/editar.jpg"
                             CommandName="Modificar" />
                            </ItemTemplate>
                          
                            <ItemStyle HorizontalAlign="Center" Width="20px" Height="18px" />
                          
                        </asp:TemplateField>
                 <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Eliminar" runat="server" ImageUrl="~/App_Themes/default/images/eliminar.jpg"
                             OnClientClick="return PreguntoEliminar();" CommandName="Eliminar" />
                            </ItemTemplate>
                          
                            <ItemStyle HorizontalAlign="Center" Width="20px" Height="18px" />
                          
                        </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#3A93D2" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
                </td>
			</tr>
			<tr>
				<td colspan="2" ><hr /></td>
			</tr>
			<tr>
				<td align="right" colspan="2" >
                                    <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                                        Text="Agregar" Font-Size="10pt" CssClass="myButton" 
                                        ToolTip="Haga clic aquí para agregar una nueva rutina" />
                                </td>
			</tr>
			<tr>
				<td colspan="2" >
                                    &nbsp;</td>
			</tr>
			</table>

	

   
  </div>
    
        
    <script type="text/javascript" language="javascript">
    
    function PreguntoEliminar()
    {
    if (confirm('¿Está seguro de eliminar el registro?'))
    return true;
    else
    return false;
    }
    </script>
</asp:Content>