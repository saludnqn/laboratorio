<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgendaList.aspx.cs" Inherits="WebLab.Agendas.AgendaList" MasterPageFile="~/Site1.Master" %>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <title>LABORATORIO</title>    
    
</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
 	<br />   &nbsp;  <div align="left" style="width:1000px">
  
       
	   
		<table width="600px" align="center" class="myTabla" style="width: 600px">
			<tr>
			
				<td colspan="2" ><b  class="mytituloTabla">LISTA DE AGENDAS</b>
                    </td>
			
		<td colspan="2" align="right"> <a href="../Help/Documentos/Agenda.htm" target="_blank"  > <img style="border:none;"  title="Ayuda en linea" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
			</tr>
			<tr>
				<td colspan="4" >
                                <hr class="hrTitulo" /></td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" style="width: 100px" >
                                    Tipo de servicio:</td>
				<td colspan="2" >
                    <asp:DropDownList ID="ddlTipoServicio" runat="server" CssClass="myList" 
                        ToolTip="Seleccione el servicio" AutoPostBack="True" 
                        onselectedindexchanged="ddlTipoServicio_SelectedIndexChanged">
                    </asp:DropDownList>
                            </td>
				<td align="right" style="width: 134px" >
                                    <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                                        Text="Agregar" Font-Size="10pt" CssClass="myButton" />
                </td>
			</tr>
			<tr>
				<td colspan="4" align="right"   >
                                   <hr /></td>
			</tr>
			<tr>
				<td style="width: 100px"   >
                                    &nbsp;</td>
				<td colspan="3" >
                                    &nbsp;</td>
			</tr>
			<tr>
				<td colspan="4">

	

   
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" DataKeyNames="idAgenda" 
            ForeColor="#333333" onrowcommand="gvLista_RowCommand1" 
            onrowdatabound="gvLista_RowDataBound" Font-Size="8pt" Width="100%" 
                        EmptyDataText="No hay agendas creadas" BorderColor="#3A93D2" 
                        BorderStyle="Solid" BorderWidth="3px" GridLines="Horizontal">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="8pt" />
            <Columns>
             <asp:BoundField DataField="nombre"  HeaderText="Tipo de Servicio" >
                    <ItemStyle Width="25%" HorizontalAlign="left"/>
                </asp:BoundField>
            <asp:BoundField DataField="item"  HeaderText="Practica" >
                    <ItemStyle Width="25%" HorizontalAlign="left"/>
                </asp:BoundField>
                <asp:BoundField DataField="fechadesde" HeaderText="Fecha Desde" >
                    <ItemStyle Width="20%" HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="fechahasta" HeaderText="Fecha Hasta">
                    <ItemStyle Width="20%" HorizontalAlign="left" />
                </asp:BoundField>
                  <asp:TemplateField>
                    <ItemTemplate>
                    <asp:ImageButton ID="Editar" runat="server" ImageUrl="~/App_Themes/default/images/editar.jpg" 
                    ommandName="Editar" />
                    </ItemTemplate>                          
                    <ItemStyle Width="20px" HorizontalAlign="Center" Height="20px" />                          
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                    <asp:ImageButton ID="Eliminar" runat="server" ImageUrl="~/App_Themes/default/images/eliminar.jpg"
                        OnClientClick="return PreguntoEliminar();" CommandName="Eliminar" />
                    </ItemTemplate>                          
                    <ItemStyle HorizontalAlign="Center" Width="20px" Height="20px" />                          
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
				<td colspan="4" ><hr /></td>
			</tr>
			<tr>
				<td colspan="4" >
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

