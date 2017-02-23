<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PeticionList.aspx.cs" Inherits="WebLab.PeticionElectronica.PeticionList" MasterPageFile="~/Site1.Master" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

    <link type="text/css"rel="stylesheet"      href="../script/jquery-ui-1.7.1.custom.css" />  

  <script type="text/javascript"      src="../script/jquery.min.js"></script> 
  <script type="text/javascript"      src="../script/jquery-ui.min.js"></script> 
    
   	 <script type="text/javascript" src="../script/Mascara.js"></script>
    <script type="text/javascript" src="../script/ValidaFecha.js"></script>   
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
     <br />   &nbsp;  <div align="left" style="width: 1100px" >

      <table  width="1000px" align="center" cellpadding="1" cellspacing="1"  >
        <tr>
            <td class="mytituloTabla">
						LISTA DE PETICIONES<hr class="hrTitulo" /></td>
        </tr>
        <tr>
            <td align="left">
              <table  width="1000px"  cellpadding="1" cellspacing="1" >
					<tr>
						<td  class="myLabelIzquierda" >Fecha Desde:</td>
						<td  >
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="0" class="myTexto" 
                                style="width: 70px"  /></td>
						<td class="myLabelIzquierda" >
                            Fecha Hasta:</td>
						<td  >
                            <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                        style="width: 70px"  onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="1" class="myTexto"  /></td>
						<td class="myLabelIzquierda">
                                            Nro. de Petición:</td>
						<td  >
                            <asp:TextBox ID="txtNro" runat="server" onblur="valNumeroSinPunto(this)" CssClass="myTexto" MaxLength="9" TabIndex="2" Width="80px"></asp:TextBox>
                           
                         </td>
					</tr>
					<tr>
						<td  class="myLabelIzquierda" >Origen:</td>
						<td  >
                            <asp:DropDownList ID="ddlOrigen" runat="server" 
                                ToolTip="Seleccione el origen" TabIndex="3" CssClass="myList">
                            </asp:DropDownList>
                                        
                                            </td>
						<td class="myLabelIzquierda" >
                                            Sector/Servicio:</td>
						<td  >
                                        <asp:DropDownList ID="ddlSectorServicio" runat="server" TabIndex="4" Width="250px" 
                                            ToolTip="Seleccione el sector">
                                        </asp:DropDownList>
                                        
                                     </td>
						<td class="myLabelIzquierda">
                                            Estado:</td>
						<td  >
                                            <asp:DropDownList ID="ddlEstado" runat="server" 
                                CssClass="myList" TabIndex="5" ToolTip="Seleccione estado de la petición">
                                                <asp:ListItem Selected="True" Value="-1">Todos</asp:ListItem>
                                                <asp:ListItem Value="0">Pendientes</asp:ListItem>
                                                <asp:ListItem Value="1">Procesados</asp:ListItem>
                                                  <asp:ListItem Value="2">Eliminadas</asp:ListItem>
                                            </asp:DropDownList>
                                        
                                     </td>
                            </tr>
                            
						
					<tr>
						<td  class="myLabelIzquierda" >DNI:</td>
						<td  >
                             <input id="txtDni" type="text" runat="server"  class="myTexto"  
                                onblur="valNumero(this)" tabindex="6"/></td>
						<td class="myLabelIzquierda" >
                                            Apellido/s:</td>
						<td  >
                                        <asp:TextBox ID="txtApellido" runat="server" CssClass="myTexto" TabIndex="7" 
                                                Width="200px"></asp:TextBox>
                                        
                                     </td>
						<td class="myLabelIzquierda">
                                            Nombres/s:</td>
						<td  >
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="myTexto" TabIndex="8" 
                                                Width="200px"></asp:TextBox>
                                        
                                     </td>
                            </tr>
                            
						
					<tr>
						<td  class="myLabelIzquierda" colspan="5" > 
                            <asp:CustomValidator ID="cvNumero" runat="server" 
                                ErrorMessage="Numero de Peticion" 
                                onservervalidate="cvNumeros_ServerValidate" ValidationGroup="0" 
                                >Numero de Peticion: Sólo numeros (sin puntos ni espacios)</asp:CustomValidator>
                            <asp:CompareValidator ID="cvDni" 
                                 runat="server" ControlToValidate="txtDni" 
                                ErrorMessage="Debe ingresar solo numeros" Operator="DataTypeCheck" 
                                Type="Integer" ValidationGroup="0">DNI: Debe ingresar solo numeros</asp:CompareValidator>
                        </td>
						
						<td align="right"><asp:Button ID="btnBuscar" runat="server" CssClass="myButton" 
                                                           TabIndex="9" Text="Buscar" ValidationGroup="0" 
                                                                Width="120px" onclick="btnBuscar_Click" />
                                                        </td>
						
					</tr>
					</table></td>
        </tr>

          <tr>
            <td>
			<hr /></td>
        </tr>
        <tr>
            <td>
           
                 <div align="left" style="border: 1px solid #999999; overflow: scroll; overflow-x:hidden; height: 500px; background-color: #F7F7F7;">
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
                    CellPadding="2" DataKeyNames="idPeticion" ForeColor="#333333" EmptyDataText="No hay peticiones generadas para los filtros de busqueda ingresados." 
                    Width="100%" onrowcommand="gvLista_RowCommand"  onrowdatabound="gvLista_RowDataBound" BorderColor="#3A93D2" 
                    BorderStyle="Solid" BorderWidth="2px" GridLines="Horizontal" Font-Names="Arial" 
                    Font-Size="8pt" EnableModelValidation="True" TabIndex="10" >
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField />
                            <asp:BoundField DataField="idPeticion" HeaderText="Peticion" >
                            <ItemStyle Width="10%"  CssClass="myLabelIzquierda" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundField>
                          <asp:BoundField DataField="fecha" HeaderText="Fecha" >
                            <ItemStyle Width="5%" HorizontalAlign="Left"  VerticalAlign="Top"/>
                        </asp:BoundField>
                         <asp:BoundField DataField="origen" HeaderText="Origen" >                           
                        <ItemStyle Width="10%" VerticalAlign="Top"/>                           
                        </asp:BoundField>
                        <asp:BoundField DataField="sector" HeaderText="Sector" >
                        <ItemStyle Width="10%" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="numeroDocumento" HeaderText="DNI" >
                            <ItemStyle Width="10%" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="apellido" HeaderText="Apellidos" >
                            <ItemStyle Font-Bold="true" Width="10%" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombres" >
                            <ItemStyle  Font-Bold="true" Width="10%" VerticalAlign="Top" />
                        </asp:BoundField>
                          
                          <asp:BoundField  HeaderText="Detalle" >
                       <ItemStyle Width="25%" CssClass="myLittleLink2" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField Visible="false" DataField="observaciones" HeaderText="Observaciones" >
                            <ItemStyle Width="10%" VerticalAlign="Top" />
                        </asp:BoundField>
                            <asp:BoundField DataField="solicitante" HeaderText="Solicitante" >
                            <ItemStyle Width="10%" VerticalAlign="Top" />
                        </asp:BoundField>

                             <asp:BoundField DataField="usuario" HeaderText="Usuario" >
                            <ItemStyle Width="5%" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado" >
                             <ItemStyle Width="5%" CssClass="myLabelIzquierda" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundField>
                          <asp:BoundField DataField="Protocolo" HeaderText="Protocolo" >
                        <ItemStyle Width="5%" CssClass="myLabelIzquierda" HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:BoundField>
                        <%--<asp:TemplateField>
                            <ItemTemplate>
                            <asp:ImageButton ID="Editar" runat="server" ImageUrl="~/App_Themes/default/images/editar.jpg" 
                            ommandName="Editar" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="20px" HorizontalAlign="Center" Height="20px" VerticalAlign="Top" />
                          
                        </asp:TemplateField>--%>
                
                        
          
                           <asp:TemplateField>
                            <ItemTemplate>
                            <asp:ImageButton  ID="Eliminar" runat="server" ImageUrl="~/App_Themes/default/images/eliminar.jpg" 
                             OnClientClick="return PreguntoEliminar();" CommandName="Eliminar" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="20px" HorizontalAlign="Center" Height="20px" VerticalAlign="Top"/>
                          
                        </asp:TemplateField>
                        
                            <asp:TemplateField>
                           <ItemTemplate>
                            <asp:ImageButton ID="Protocolo" runat="server" ImageUrl="~/App_Themes/default/images/flecha.jpg" 
                              CommandName="Peticion" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="20px" HorizontalAlign="Center" Height="20px" VerticalAlign="Top" />
                          
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#3A93D2" Font-Bold="True" ForeColor="White" 
                        HorizontalAlign="Left" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
               </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table> 
    <script language="javascript" type="text/javascript">

                 function PreguntoEliminar() {
                     if (confirm('¿Está seguro de eliminar el pedido?'))
                         return true;
                     else
                         return false;
                 }
    </script>
  
    </div>
</asp:Content>