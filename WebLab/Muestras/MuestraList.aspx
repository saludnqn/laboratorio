<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MuestraList.aspx.cs" Inherits="WebLab.Muestras.MuestraList" MasterPageFile="~/Site1.Master" %>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
<br />   &nbsp;
         <div align="left" style="width:1000px">
  
       
	   
		<table width="600px" align="center" class="myTabla">
			<tr>
			
				<td ><b  class="mytituloTabla">LISTA DE TIPO DE MUESTRA</b>
                    </td>
			
			<td align="right"> <a href="../Help/Documentos/Tipo de Muestra.htm" target="_blank"  > <img style="border:none;"  title="Ayuda en linea" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
			</tr>
			<tr>
				<td colspan="2" >
                                     <hr class="hrTitulo" /></td>
			</tr>
			<tr>
				<td colspan="2">

	
       <div align="left" style="overflow:scroll;overflow-x:hidden;height:600px;">
   
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" DataKeyNames="idMuestra" 
            ForeColor="#333333" onrowcommand="gvLista_RowCommand1" 
            onrowdatabound="gvLista_RowDataBound" Font-Size="9pt" Width="100%" 
                        EmptyDataText="No hay tipos de muestras creadas" BorderColor="#3A93D2" 
                        BorderStyle="Solid" BorderWidth="3px" GridLines="Both">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="8pt" />
            <Columns>
                <asp:BoundField DataField="codigo" 
                    HeaderText="Codigo" >
                    <ItemStyle Width="10%" />
                </asp:BoundField>
             <asp:BoundField DataField="nombre" 
                    HeaderText="Descripción" >
                    <ItemStyle Width="80%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Modificar">
                            <ItemTemplate>
                            <asp:ImageButton ID="Modificar" runat="server" ImageUrl="~/App_Themes/default/images/editar.jpg"
                             CommandName="Modificar" />
                            </ItemTemplate>
                          
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                          
                        </asp:TemplateField>
                        <asp:TemplateField   HeaderText="Habilitado">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStatus" runat="server" 
                            AutoPostBack="true" OnCheckedChanged="chkStatus_OnCheckedChanged"
                            Checked='<%# Convert.ToBoolean(Eval("Habilitado")) %>'
                            />
                    </ItemTemplate>                    
                       <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:TemplateField>

              <%--   <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Eliminar" runat="server" ImageUrl="~/App_Themes/default/images/eliminar.jpg"
                             OnClientClick="return PreguntoEliminar();" CommandName="Eliminar" />
                            </ItemTemplate>
                          
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                          
                        </asp:TemplateField>--%>
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#3A93D2" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        </div>
                </td>
			</tr>
			<tr>
				<td colspan="2" ><hr /></td>
			</tr>
			<tr>
				<td align="right" colspan="2" >
                                    <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                                        Text="Agregar" Font-Size="10pt" CssClass="myButton" 
                                        ToolTip="Haga clic aquí para agregar un nuevo tipo de muestra" />
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