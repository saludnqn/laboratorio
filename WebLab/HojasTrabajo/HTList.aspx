<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HTList.aspx.cs" Inherits="WebLab.HojasTrabajo.HTList" MasterPageFile="~/Site1.Master" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>



<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
  <br />   &nbsp;
         <div align="left" style="width:1000px">
  
       
	   
		<table width="800px" align="center" class="myTabla">
			<tr>
			
				<td colspan="3" ><b  class="mytituloTabla">LISTA DE HOJAS DE TRABAJO</b></td>
			
				<td  colspan="3" align="right"> <a href="../Help/Documentos/Hoja de Trabajo.htm" target="_blank"  > <img style="border:none;"  title="Ayuda en linea" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
			</tr>
			<tr>
				<td colspan="6" >
                                   <hr class="hrTitulo" /></td>
			</tr>
			<tr>
		<td class="myLabelIzquierda" >
	

   
                    Tipo de Servicio:</td>
				<td>

	

   
                            <asp:DropDownList ID="ddlServicio" runat="server" 
                                ToolTip="Seleccione el servicio" TabIndex="1" CssClass="myList"                              
                                onselectedindexchanged="ddlServicio_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                                        
                </td>
		<td class="myLabelIzquierda" >

	

   
                    Area:</td>
				<td>

	

   
                            <asp:DropDownList ID="ddlArea" runat="server" 
                                ToolTip="Seleccione el area" TabIndex="2" CssClass="myList" 
                        onselectedindexchanged="ddlArea_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                                        
                </td>
				<td>

	

   
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" ValidationGroup="0" 
                                                onclick="btnBuscar_Click" CssClass="myButton" TabIndex="24" Width="77px" />
                </td>
				<td>

	

   
                    &nbsp;</td>
			</tr>
			<tr>
				<td colspan="6">

	

   
                    &nbsp;</td>
			</tr>
			<tr>
				<td colspan="6">

	

   
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" DataKeyNames="idHojaTrabajo" 
            ForeColor="#333333" onrowcommand="gvLista_RowCommand1" 
            onrowdatabound="gvLista_RowDataBound" Font-Size="8pt" Width="100%" 
                        EmptyDataText="No se encontraron registros para los filtros de busqueda ingresados" BorderColor="#3A93D2" 
                        BorderStyle="Solid" BorderWidth="3px" GridLines="Horizontal">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="8pt" />
            <Columns>
             <asp:BoundField DataField="servicio" 
                    HeaderText="Tipo de Servicio" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="area" HeaderText="Area">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="codigo" HeaderText="Codigo">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Modificar" runat="server" ImageUrl="~/App_Themes/default/images/editar.jpg"
                             CommandName="Modificar" />
                            </ItemTemplate>
                          
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                          
                        </asp:TemplateField>
                           <asp:TemplateField>
                            <ItemTemplate>
                            <asp:ImageButton ID="Pdf" runat="server" ImageUrl="~/App_Themes/default/images/pdf.jpg" 
                              CommandName="Pdf" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="20px" HorizontalAlign="Center" Height="20px" />
                          
                        </asp:TemplateField>
                        
                        
                 <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Eliminar" runat="server" ImageUrl="~/App_Themes/default/images/eliminar.jpg"
                             OnClientClick="return PreguntoEliminar();" CommandName="Eliminar" />
                            </ItemTemplate>
                          
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                          
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
				<td colspan="6" ><hr /></td>
			</tr>
			<tr>
				<td align="right" colspan="6" >
                                    <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                                        Text="Agregar" Font-Size="10pt" CssClass="myButton" 
                                        ToolTip="Haga clic aquí para agregar un nuevo método" />
                                </td>
			</tr>
			<tr>
				<td colspan="6" >
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