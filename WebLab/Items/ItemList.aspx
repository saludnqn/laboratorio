<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemList.aspx.cs" Inherits="WebLab.Items.ItemList" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>


<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  

  <br />   &nbsp;
    
 
  <table  width="1000px" align="center" class="myTabla">
					<tr>
						<td colspan="4"><b class="mytituloTabla">LISTA DE ANALISIS</b></td>
						<td align="right" colspan="2">
                             <a href="../Help/Documentos/ANALISIS.htm" target="_blank"  > <img style="border:none;" alt="Ayuda"  title="Ayuda en linea" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
					<tr>
						<td colspan="6"><hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Codigo:</td>
						<td>
                            <asp:TextBox ID="txtCodigo" runat="server" MaxLength="100" 
                                Width="86px" style="text-transform :uppercase"  
                                ToolTip="Ingrese el codigo del análisis" TabIndex="3" 
                                CssClass="myTexto" />
                                            </td>
						<td class="myLabelIzquierda"  colspan="3">
                                                        Nombre corto :</td>
						<td>
                            <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" 
                                Width="236px" style="text-transform :uppercase"  
                                ToolTip="Ingrese el nombre del analisis" TabIndex="3" 
                                CssClass="myTexto" />
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Servicio:</td>
						<td>
                            <anthem:DropDownList ID="ddlServicio" runat="server" 
                                ToolTip="Seleccione el servicio" TabIndex="4" CssClass="myList" 
                                onselectedindexchanged="ddlServicio_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </anthem:DropDownList>
                                        
                                            </td>
						<td class="myLabelIzquierda"  colspan="3">
                                                        Area:</td>
						<td>
                                        
                            <anthem:DropDownList ID="ddlArea" runat="server" 
                                ToolTip="Seleccione el area" TabIndex="5" CssClass="myList" 
                                AutoPostBack="True" onselectedindexchanged="ddlArea_SelectedIndexChanged1">
                            </anthem:DropDownList>
                                        
                                            </td>
					</tr>
						<tr>
						<td class="myLabelIzquierda" >
                                            Ordenar por:</td>
						<td colspan="3">
                            <asp:DropDownList ID="ddlTipo" runat="server" 
                                ToolTip="Seleccione el orden de la lista" TabIndex="6" CssClass="myList" 
                                AutoPostBack="True" onselectedindexchanged="ddlTipo_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">Codigo</asp:ListItem>
                                <asp:ListItem Value="1">Nombre</asp:ListItem>
                                <asp:ListItem Value="2">Area</asp:ListItem>
                            </asp:DropDownList>
                                        
                        </td>
						<td colspan="2" align="right">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                                                onclick="btnBuscar_Click" CssClass="myButton" 
                                                ToolTip="Haga clic aquí para buscar o presione ENTER" />
                                        
                            </td>
						
					</tr>
					<tr>
						<td colspan="6">
                                         <hr /></td>
						
					</tr>
					
					</table>
					
					
					
					
					<table align="center" width="1000px">
					
					<tr>
						<td  style="vertical-align: top; " >
						
                <asp:label id="CurrentPageLabel"
                  forecolor="Blue"
                  runat="server"/>
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" DataKeyNames="idItem" onrowcommand="gvLista_RowCommand1" 
            onrowdatabound="gvLista_RowDataBound" Font-Size="9pt" Width="850px" CellPadding="1" 
                                ForeColor="#666666" onselectedindexchanged="gvLista_SelectedIndexChanged" 
                                AllowPaging="True" onpageindexchanging="gvLista_PageIndexChanging" 
                                onselectedindexchanging="gvLista_SelectedIndexChanging" 
                                BorderColor="#3A93D2" BorderStyle="Solid" BorderWidth="1px" 
                                GridLines="Horizontal" PageSize="25" 
                                EmptyDataText="No se encontraron analisis para los filtros de busqueda ingresados" 
                                ToolTip="Lista de analisis" ondatabound="gvLista_DataBound">
            <RowStyle BackColor="White" ForeColor="#333333" Font-Names="Arial" 
                Font-Size="8pt" />
            <Columns>
                <asp:BoundField DataField="codigoNomenclador" HeaderText="Nomenclador">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="15%" />
                </asp:BoundField>
          <asp:BoundField DataField="codigo" 
                    HeaderText="Codigo" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="40%" />
                </asp:BoundField>
                <asp:BoundField DataField="area" HeaderText="Area">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="tipo" HeaderText="Tipo">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="disponible">
                    <ItemStyle Width="5px" />
                </asp:BoundField>
               <asp:TemplateField>
                            <ItemTemplate>
                            <asp:ImageButton ID="Editar" runat="server" ImageUrl="~/App_Themes/default/images/editar.jpg" 
                            CommandName="Editar" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="20px" HorizontalAlign="Center" Height="20px" />
                          
                        </asp:TemplateField>
               <%--    <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="VR" runat="server" ImageUrl="~/App_Themes/default/images/valorefe.jpg" 
                              CommandName="VR" />
                            </ItemTemplate>
                          
                       <ItemStyle Width="20px" HorizontalAlign="Center" Height="20px" />
                  
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Diagrama" runat="server" ImageUrl="~/App_Themes/default/images/diagrama.jpg" 
                              CommandName="Diagrama" />
                            </ItemTemplate>
                          
                       <ItemStyle Width="20px" HorizontalAlign="Center" Height="20px" />
                  
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Resultados" runat="server" ImageUrl="~/App_Themes/default/images/lista.jpg" 
                           CommandName="Resultados" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="20px" Height="20px" HorizontalAlign="Center" />
                          
                        </asp:TemplateField>     
                        
                               <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Recomendacion" runat="server" ImageUrl="~/App_Themes/default/images/buscar.jpg" 
                           CommandName="Recomendacion" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="20px" Height="20px" HorizontalAlign="Center" />
                          
                        </asp:TemplateField>  --%>                                         
                           <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Eliminar" runat="server" ImageUrl="~/App_Themes/default/images/eliminar.jpg" 
                             OnClientClick="return PreguntoEliminar();" CommandName="Eliminar" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="20px" Height="20px" HorizontalAlign="Center" />
                          
                        </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="20" Position="Top" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#E6E6E6" ForeColor="Black" HorizontalAlign="Right" 
                BorderColor="White" VerticalAlign="Top" />
                
               
      
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#3A93D2" Font-Bold="False" ForeColor="White" 
                Font-Names="Arial" Font-Size="8pt" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
                        </td>
						<td style="vertical-align: top;"   align="right" >
						 
                                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" 
                                                onclick="btnNuevo_Click" CssClass="myButton" 
                                                ToolTip="Haga clic aquí para agregar un nuevo analisis" />
                                        <br />
                                        <br />
                     <img alt="" src="../App_Themes/default/images/pdf.jpg"/>&nbsp;<asp:LinkButton 
                            ID="lnkPDF" runat="server" CssClass="myLittleLink" onclick="lnkPDF_Click"> 
                                            Descargar Nomenclador</asp:LinkButton>  <br />
                                <img alt="" src="../App_Themes/default/images/pdf.jpg"/>&nbsp;<asp:LinkButton 
                            ID="lnkPdfReducido" runat="server" CssClass="myLittleLink" 
                                onclick="lnkPdfReducido_Click" >
                            Analisis del Laboratorio</asp:LinkButton></td>
					</tr>
					<tr>
						<td colspan="2">
                                            </td>
						
					</tr>
					</table>
<%--    </form>
    --%>
  
    
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
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <title>LABORATORIO</title>    
    
    <style type="text/css">
        .style1
        {}
    </style>
    
</asp:Content>
