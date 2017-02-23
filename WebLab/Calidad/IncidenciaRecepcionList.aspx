<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncidenciaRecepcionList.aspx.cs" Inherits="WebLab.Calidad.IncidenciaRecepcionList" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

    <link type="text/css"rel="stylesheet"      href="../script/jquery-ui-1.7.1.custom.css" />  

     <script language="Javascript" type="text/javascript" src="../FusionCharts/FusionCharts.js"></script>

  <script type="text/javascript"      src="../script/jquery.min.js"></script> 
  <script type="text/javascript"      src="../script/jquery-ui.min.js"></script> 
    
      <script type="text/javascript"     src="../script/jquery.ui.datepicker-es.js"></script>   
      
      <script type="text/javascript">


          $(function () {
              $("#<%=txtFechaDesde.ClientID %>").datepicker({
                  showOn: 'button',
                  buttonImage: '../App_Themes/default/images/calend1.jpg',
                  buttonImageOnly: true
              });
          });

          $(function () {
              $("#<%=txtFechaHasta.ClientID %>").datepicker({
                  showOn: 'button',
                  buttonImage: '../App_Themes/default/images/calend1.jpg',
                  buttonImageOnly: true
              });
          });
 
     
  </script>  
  
  
   	 <script type="text/javascript" src="../script/Mascara.js"></script>
    <script type="text/javascript" src="../script/ValidaFecha.js"></script>   
  
</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  

  <br />   &nbsp;
    
 
  <table  width="800px" align="center" class="myTabla">
					<tr>
						<td colspan="5"><b class="mytituloTabla">LISTA DE INCIDENCIAS REGISTRADAS</b></td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan="6"><hr class="hrTitulo" /></td>
					</tr>
					
				<tr>
<td class="myLabelIzquierda" align="left"> 
  
 
    Efector Origen:</td>
<td class="style3"  align="left" colspan="4"> 
  
 
                                                <asp:DropDownList ID="ddlEfectorOrigen" runat="server"  CssClass="myList" TabIndex="4" ToolTip="Seleccione el efector">
                                                </asp:DropDownList>
                                                </td>
<td class="style3"  align="left"> 
  
    &nbsp;</td>
</tr>
					
				<tr>
<td class="myLabelIzquierda" align="left"> 
  
 
    &nbsp;Desde:</td>
<td class="style3"  align="left"> 
  
 
  <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="0" class="myTexto" 
                                style="width: 70px" 
        title="Ingrese la fecha de inicio"  /></td>
<td class="myLabelIzquierda"  align="left"> 
  
 
    &nbsp;Hasta:</td>
<td class="style3"  align="left"> 
  
 

                            <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="1" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de fin"  /></td>
<td class="style3"  align="left"> 
  
 <asp:CustomValidator ID="cvFechas" runat="server"  ErrorMessage="Fechas de inicio y de fin" onservervalidate="cvFechas_ServerValidate" ValidationGroup="0">Debe ingresar fechas de inicio y fin</asp:CustomValidator>

    <asp:Button ID="btnBuscar" runat="server" CssClass="myButton" 
        onclick="btnBuscar_Click" Text="Buscar" Width="100px" TabIndex="2" 
        ValidationGroup="0" />
        </td>
<td class="style3"  align="left"> 
  
    <asp:Button ID="btnNuevo" runat="server" CssClass="myButtonRojo" 
        onclick="btnNuevo_Click" Text="Nueva" 
        ToolTip="Haga clic aquí para agregar una nueva incidencia" Width="100px" />
        </td>
</tr>
					<tr>
						<td colspan="6">
                                         <hr /></td>
						
					</tr>
					
					
					<tr>
						<td colspan="6">
                    
                    
                    
					
					<table  width="800px">
					
					<tr>
						<td  style="vertical-align: top; " >
						
                <asp:label id="CurrentPageLabel"
                  forecolor="Blue"
                  runat="server"/>
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="idIndicenciaRecepcion" onrowcommand="gvLista_RowCommand1" 
            onrowdatabound="gvLista_RowDataBound" Font-Size="9pt" Width="800px" CellPadding="1" 
                                ForeColor="#666666" onselectedindexchanged="gvLista_SelectedIndexChanged" 
                                AllowPaging="True" onpageindexchanging="gvLista_PageIndexChanging" 
                                onselectedindexchanging="gvLista_SelectedIndexChanging" 
                                BorderColor="#3A93D2" BorderStyle="Solid" BorderWidth="1px" 
                                GridLines="Horizontal" PageSize="25" 
                                
                                EmptyDataText="No se encontraron incidencias para los filtros de busqueda ingresados" 
                                ondatabound="gvLista_DataBound">
            <RowStyle BackColor="White" ForeColor="#333333" Font-Names="Arial" 
                Font-Size="8pt" />
            <Columns>
                <asp:BoundField DataField="idIndicenciaRecepcion" HeaderText="Numero">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="5%" Font-Bold="True" />
                </asp:BoundField>
          
                <asp:BoundField DataField="Efector" HeaderText="Origen">
                    <ItemStyle Width="40%" />
                </asp:BoundField>
          
                <asp:BoundField DataField="fecha" HeaderText="Fecha" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="usuario" HeaderText="Usuario">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                
               <asp:TemplateField>
                            <ItemTemplate>
                            <asp:ImageButton ID="Editar" runat="server" ImageUrl="~/App_Themes/default/images/editar.jpg" 
                            CommandName="Editar" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="5%" HorizontalAlign="Center" Height="20px" />
                          
                        </asp:TemplateField>
               
                           <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Eliminar" runat="server" ImageUrl="~/App_Themes/default/images/eliminar.jpg" 
                             OnClientClick="return PreguntoEliminar();" CommandName="Eliminar" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="5%" Height="20px" HorizontalAlign="Center" />
                          
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
						 
                                            
                                        <br />
                                        <br />
                        </td>
					</tr>
					<tr>
						<td colspan="2">
                                            </td>
						
					</tr>
					</table>

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
