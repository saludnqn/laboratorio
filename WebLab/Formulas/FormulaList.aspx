<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormulaList.aspx.cs" Inherits="WebLab.Formulas.FormulaList" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
  <br />   &nbsp;
    
  
       
	   
		<table width="60%" align="center" class="myTabla">
			<tr>
			
				<td ><b class="mytituloTabla">LISTA DE FORMULAS Y CONTROLES</b>
                    </td>
			
			<td align="right"> <a href="../Help/Documentos/Formulas y Controles.htm" target="_blank"  > <img style="border:none;"  title="Ayuda en linea" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
			</tr>
			<tr>
				<td colspan="2" >
                                    <hr class="hrTitulo" /></td>
			</tr>
			<tr>
				<td colspan="2">

	

   
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
            CellPadding="2" DataKeyNames="idFormula" 
            ForeColor="#333333" onrowcommand="gvLista_RowCommand1" 
            onrowdatabound="gvLista_RowDataBound" Font-Size="8pt" Width="100%" 
                        EmptyDataText="No hay fórmulas creadas" BorderColor="#3A93D2" 
                        BorderStyle="Solid" BorderWidth="3px" GridLines="None">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
             <asp:BoundField DataField="item" 
                    HeaderText="Analisis" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Expresión" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="50%" />
                </asp:BoundField>
                <asp:BoundField DataField="tipo" HeaderText="Tipo">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Modificar" runat="server" ImageUrl="~/App_Themes/default/images/editar.jpg"
                             CommandName="Modificar" />
                            </ItemTemplate>
                          
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                          
                        </asp:TemplateField>
                 <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Eliminar" runat="server" ImageUrl="~/App_Themes/default/images/eliminar.jpg"
                             OnClientClick="return PreguntoEliminar();" CommandName="Eliminar" />
                            </ItemTemplate>
                          
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                          
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
				<td colspan="2" >
                                    <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                                        Text="Agregar Formula" Font-Size="10pt" CssClass="myButton" 
                                        Width="120px" ToolTip="Haga clic aquí para agregar una formula" /> &nbsp; &nbsp;
                                    <asp:Button ID="btnAgregarControl" runat="server" onclick="btnAgregarControl_Click" 
                                        Text="Agregar Control" Font-Size="10pt" CssClass="myButton" 
                                        Width="120px" ToolTip="Haga clic aquí para agregar un control" />
                                </td>
			</tr>
			<tr>
				<td colspan="2" >
                                    &nbsp;</td>
			</tr>
			</table>

	

   
   
    
        
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
