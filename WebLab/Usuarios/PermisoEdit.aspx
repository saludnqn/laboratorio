﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermisoEdit.aspx.cs" Inherits="WebLab.Usuarios.PermisoEdit" MasterPageFile="~/Site1.Master" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

 <link href="../App_Themes/default/style.css" rel="stylesheet" type="text/css" />          
  
   
    </asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
<br />   &nbsp;
        <div style="width:1000px;" align="left">
        <table style="width:800px;">
            <tr>
               <td>&nbsp;</td>
               <td colspan="2"><b  class="mytituloTabla">PERMISOS</b>
                    </td>
            </tr>
            <tr>
                <td  >
                    &nbsp;</td>
                <td   colspan="2">
                    <hr /></td>
            </tr>
            <tr>
                <td class="myLabelGris" style="width: 94px">
                    &nbsp;</td>
                <td class="myLabelIzquierda" style="width: 94px">
                    Perfil de Usuario:</td>
                <td class="mytituloGris" style="width: 696px">
                    <asp:Label ID="lblPerfil" runat="server" Text="Label"></asp:Label>
                   
                </td>
            </tr>
            <tr>
                <td    style="vertical-align: top">
                    </td>
                <td    style="vertical-align: top" colspan="2">
                   <hr /></td>
            </tr>
            <tr>
                <td    style="vertical-align: top">
                 
                    &nbsp;</td>
                <td    style="vertical-align: top" colspan="2">
                 
                    <table width="100%">
                    <tr>
                <td    style="vertical-align: top" rowspan="4">
                    &nbsp;</td>
                <td   style="width: 26px">
                    &nbsp;</td>
                <td   style="vertical-align: top">
    <asp:Menu ID="mnuPrincipal" runat="server" 
         BackColor="#990000" 
        DynamicHorizontalOffset="2" Font-Names="Arial" Font-Size="10pt" 
        ForeColor="White" StaticSubMenuIndent="10px" 
     Font-Bold="True"  StaticDisplayLevels="3" DynamicMenuStyle-CssClass="menu" 
                        onmenuitemclick="mnuPrincipal_MenuItemClick" BorderColor="#990000" 
                        BorderStyle="Solid" BorderWidth="1px" Orientation="Horizontal">
        <StaticSelectedStyle BackColor="#1C5E55" />
        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <DynamicHoverStyle BackColor="#666666" ForeColor="White" />

<DynamicMenuStyle BackColor="#E3EAEB" BorderColor="#CCCCCC" BorderStyle="Solid" 
         BorderWidth="1px"></DynamicMenuStyle>
        <DynamicSelectedStyle BackColor="#1C5E55" />
     <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" 
         ForeColor="#333333" />
        <StaticHoverStyle BackColor="#666666" ForeColor="White" />

    
    

    
    </asp:Menu>
                   
                </td>
            </tr>
                    <tr>
                <td   style="width: 26px">
                    &nbsp;</td>
                <td class="myLabelIzquierda" style="vertical-align: top">
                    <asp:Label ID="lblModulo" runat="server"></asp:Label>
                   
                </td>
            </tr>
                    <tr>
                <td   style="width: 26px">
                    &nbsp;</td>
                <td   style="vertical-align: top">
                    <hr /></td>
            </tr>
                    <tr>
                <td   style="width: 26px">
                    &nbsp;</td>
                <td   style="vertical-align: top">
                    <asp:GridView ID="gvPermisoMenu" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="idMenu" CellPadding="4" CssClass="myLabelIzquierda" 
                        ForeColor="#333333" GridLines="Vertical" 
                        onrowdatabound="gvPermisoMenu_RowDataBound" Width="600px">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField HeaderText="Menu" DataField="objeto" >
                                <ItemStyle Width="25%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Permiso">

                   <EditItemTemplate>

                      

                   </EditItemTemplate>

                   <ItemTemplate>
                       <asp:RadioButtonList ID="chkPermiso" runat="server" RepeatDirection="Horizontal">                     
                        <asp:ListItem Value="2">Modifica/Graba</asp:ListItem> 
                        <asp:ListItem Value="1">Solo Consulta</asp:ListItem>
                        <asp:ListItem Value="0">No accede</asp:ListItem>
                       </asp:RadioButtonList>
                        

                   </ItemTemplate>

                                <ItemStyle Width="75%" />

               </asp:TemplateField>
                            <asp:BoundField DataField="permiso"  >
                                <ItemStyle ForeColor="White" Width="0%" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#999999" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
                        <tr>
                            <td   style="vertical-align: top">
                                &nbsp;</td>
                            <td   style="width: 26px">
                                &nbsp;</td>
                            <td   style="vertical-align: top">
                                <hr /></td>
                        </tr>
                        <tr>
                            <td   style="vertical-align: top">
                                &nbsp;</td>
                            <td   style="width: 26px">
                                &nbsp;</td>
                            <td   style="vertical-align: top">
                                <asp:Button ID="btnGuardarPermisos" runat="server" Height="26px" 
                                    Text="Grabar" onclick="btnGuardarPermisos_Click" CssClass="myButton" />
                            </td>
                        </tr>
                    </table>
                   
                </td>
            </tr>
            
            </table>
        <br />
        <br />
        <br />
    </div>
    
 </asp:Content>