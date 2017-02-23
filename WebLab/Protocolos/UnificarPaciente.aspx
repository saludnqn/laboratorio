<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnificarPaciente.aspx.cs" Inherits="WebLab.Protocolos.UnificarPaciente" MasterPageFile="~/Site1.Master" %>


<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../script/Mascara.js"></script>
    <script type="text/javascript" src="../script/ValidaFecha.js"></script>   
  
 

   
    </asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
 <br />   &nbsp;
    
    <div align="left" style="width: 1100px" >
&nbsp;&nbsp;

    <table  class="myTabla" width="1000" align="center">
        <tr>
            <td class="mytituloTabla" colspan="3">
                Unificación de Pacientes
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr /></td>
        </tr>
        <tr>
            <td style="vertical-align: top" width="400">
                <table style="width:100%;" class="tabs">
                    <tr>
                        <td colspan="2" class="mytituloGris">
                            <img alt="" src="../App_Themes/default/images/uno.jpg" /> Identificación del Paciente Principal </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="mytituloGris">
                            <hr /></td>
                    </tr>
                    <tr>
                        <td>
                            Documento Unico (DU):&nbsp;<input id="txtDni" type="text" runat="server"  class="myTexto"  
                                onblur="valNumeroSinPunto(this)" maxlength="8" style="width: 65px"/><asp:RequiredFieldValidator 
                                ID="rfvNumero" runat="server" ControlToValidate="txtDni" ErrorMessage="*" 
                                ValidationGroup="1">*</asp:RequiredFieldValidator>
&nbsp;
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" ValidationGroup="1" 
                                               CssClass="myButton" TabIndex="4" 
                                                ToolTip="Buscar Paciente" 
                                onclick="btnBuscar_Click" Width="50px" />
                                &nbsp;<asp:Button ID="btnBorrar" runat="server" Text="Borrar Selección" 
                                               CssClass="myButton" TabIndex="4" 
                                                ToolTip="Borrar Selección" Width="105px" 
                                onclick="btnBorrar_Click" />
                                </td>
                    </tr>
                    <tr>
                        <td>
                           <hr /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlPaciente" runat="server" Visible="False" CssClass="tab_container">
                         
                 
                    <table>
                        <tr>
                            <td>
                                DU:<asp:Label ID="lblidPaciente" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td class="myLabelIzquierdaGde">
                                <asp:Label ID="lblDU" runat="server" Text="Label"></asp:Label><asp:Label ID="lblHC" runat="server" Visible="false" Text="Label"></asp:Label>
                            </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    Nro. Historia Clinica:</td>
                                                <td class="myLabelIzquierdaGde">
                                                    
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    Apellido:</td>
                                                <td class="myLabelIzquierdaGde">
                                                    <asp:Label ID="lblApellido" runat="server" Text="Label"></asp:Label>
                                                </td>
                        </tr>
                                            <tr>
                                                <td >
                                                    Nombre:</td>
                                                <td class="myLabelIzquierdaGde">
                                                    <asp:Label ID="lblNombre" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Fecha de Nacimiento:</td>
                                                <td class="myLabelIzquierdaGde">
                    <asp:Label ID="lblFechaNacimiento" runat="server" Text="Label"></asp:Label><asp:Label ID="lblEdad" runat="server" Visible="false" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    Edad:</td>
                                                <td class="myLabelIzquierdaGde">
                                                    
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    Sexo:</td>
                                                <td class="myLabelIzquierdaGde">
                                                    <asp:Label ID="lblSexo" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                   </asp:Panel>
                            
                           </td>
                    </tr>
                    <tr>
                        <td>
        
                            <asp:Panel ID="pnlSinDatosPaciente" runat="server" Visible="false">
                            <div>
                                No se encontró paciente para el numero ingresado.</div>
                            </asp:Panel>
        
                           </td>
                    </tr>
                    </table>
            </td>
            <td width="20">
                &nbsp;
            </td>
            <td width="600" style="vertical-align: top">
                <table style="width:100%;">
                    <tr>
                        <td  class="mytituloGris">
                            <img alt="" src="../App_Themes/default/images/dos.jpg" /> Identificación de Protocolos a 
                            asignar al Paciente Principal                   
                            </td>
                             </tr>
                    <tr>
                        <td  class="mytituloGris">
                            <hr />
                            </td>
                            </tr>
                    <tr>
                        <td class="myLabelIzquierda">
                                            Buscar por:&nbsp; <asp:DropDownList ID="ddlFiltro" runat="server">
                                                <asp:ListItem Value="1">DU</asp:ListItem>
                                                <asp:ListItem Value="2">Apellido</asp:ListItem>
                                                <asp:ListItem Value="3">Nombre</asp:ListItem>
                                                <asp:ListItem Value="4">Nro.Protocolo</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtFiltro" runat="server" Width="200px"></asp:TextBox>
                                            &nbsp;<asp:RequiredFieldValidator ID="rfvFiltro" runat="server" 
                                                ControlToValidate="txtFiltro" ErrorMessage="*" ValidationGroup="0">*</asp:RequiredFieldValidator>
&nbsp;<asp:Button ID="btnBuscar0" 
                                runat="server" Text="Buscar" ValidationGroup="0" 
                                               CssClass="myButton" TabIndex="4" 
                                                ToolTip="Buscar Protocolos" onclick="btnBuscar0_Click" Width="60px" />
                        &nbsp; <asp:Button ID="btnBorrar0" runat="server" Text="Borrar Selección" 
                                               CssClass="myButton" TabIndex="4" 
                                                ToolTip="Borrar Selección" Width="105px" onclick="btnBorrar0_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                                         <hr /></td>
                    </tr>
                    <tr>
                        <td class="myLabelIzquierda">
                        
                            <asp:Panel ID="pnlProtocolos" runat="server" Visible="False">
                            
                                <div>
                                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="idProtocolo" Font-Size="9pt" 
                                                Width="100%" CellPadding="1" 
                                ForeColor="#666666" PageSize="50" 
                                
                                
                                
                                
                    EmptyDataText="No se encontraron protocolos para los parametros de busqueda ingresados" BorderColor="#3A93D2" 
                                BorderStyle="Solid" BorderWidth="3px" GridLines="Horizontal">
            <RowStyle BackColor="White" ForeColor="#333333" Font-Names="Arial" 
                Font-Size="8pt" />
            <Columns>
                <asp:TemplateField HeaderText="Sel." >
                                                        <ItemTemplate>
                                                         <asp:CheckBox ID="CheckBox1" runat="server" EnableViewState="true" />
                                                     </ItemTemplate>
                                                     <ItemStyle Width="5%" 
                                                            HorizontalAlign="Center" />
                                                    </asp:TemplateField>
          <asp:BoundField DataField="numero" 
                    HeaderText="Protocolo" >
                    <ItemStyle Width="5%" HorizontalAlign="Center"  />
                </asp:BoundField>
                <asp:BoundField DataField="dni" HeaderText="DNI" >
                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="paciente" HeaderText="Apellidos y Nombres">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
            

                 
                
                        
         
                        
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#3A93D2" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#3A93D2" Font-Bold="False" ForeColor="White" 
                Font-Names="Arial" Font-Size="8pt" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <br />
           <asp:Button ID="btnMover" runat="server"  OnClientClick="return Pregunto();"
                    Text="Asignar Protocolos al Paciente Principal" ValidationGroup="0" 
                                               CssClass="myButtonRojo" TabIndex="4" Width="250px" 
                                        onclick="btnMover_Click" />
                                </div>
                            </asp:Panel>
        
                            <asp:Panel ID="pnlSinDatos" runat="server" Visible="false">
                            <div class="myLabelRojo">
                            No se encontraron datos para los filtros determinados.
                            </div>
                            </asp:Panel>
        
                        </td>
                    </tr>
                    <tr>
                        <td>
                                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                                         
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
        
 <script language="javascript" type="text/javascript">




     function Pregunto() {
         if (confirm('¿Está seguro de asignar los protocolos seleccionados al paciente principal?'))
             return true;
         else
             return false;
     }
    </script>

</div>   
			
 </asp:Content>