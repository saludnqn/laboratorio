<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioEdit.aspx.cs" Inherits="WebLab.Usuarios.UsuarioEdit" MasterPageFile="~/Site1.Master" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

 <link href="../App_Themes/default/style.css" rel="stylesheet" type="text/css" />  
     

   
  
   
    </asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
  <br />   &nbsp;
        <div style="width:800px;" align="left">
        <table style="width:600px">
            <tr>
            
            <td  ><b  class="mytituloTabla">USUARIO</b>
                    </td>
                <td align="right"> <a href="../Help/Documentos/Usuarios.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
            </tr>
            <tr>
                <td class="style1" colspan="2">
                    <hr /></td>
            </tr>
            
            <tr>
                <td class="myLabelIzquierda"  style="vertical-align: top; width: 93px;">
                    Apellido:</td>
                <td style="width: 497px"  >
                    <asp:TextBox ID="txtApellido" runat="server" Width="350px" CssClass="myTexto" 
                        MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvApellido" runat="server" 
                        ControlToValidate="txtApellido" ErrorMessage="Apellido" 
                        ValidationGroup="0">*</asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda" style="width: 93px">
                    Nombres:</td>
                <td style="width: 497px"  >
                    <asp:TextBox ID="txtNombre" runat="server" Width="350px" CssClass="myTexto" 
                        MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                        ControlToValidate="txtNombre" ErrorMessage="Nombres" ValidationGroup="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda" style="width: 93px; vertical-align: top;">
                    Firma Validación:</td>
                <td style="width: 497px"  >
                    <asp:TextBox ID="txtFirmaValidacion" runat="server" Width="350px" CssClass="myTexto" 
                        MaxLength="50" Rows="3" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
           <%-- <tr>
                <td class="myLabelIzquierda" style="width: 70px">
                    Matricula:</td>
                <td  >
                    <asp:TextBox ID="txtMatricula" runat="server" Width="150px" CssClass="myTexto" 
                        MaxLength="50"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td class="myLabelIzquierda" colspan="2">
                    <hr /></td>
            </tr>
            <tr>
                <td class="myLabelIzquierda" style="width: 93px">
                    Usuario:</td>
                <td style="width: 497px"  >
                    <asp:TextBox ID="txtUsername" runat="server" Width="350px" CssClass="myTexto" 
                        MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" 
                        ControlToValidate="txtUsername" ErrorMessage="Usuario" ValidationGroup="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda" style="width: 93px">
                    Contraseña:</td>
                <td style="width: 497px"  >
                    <asp:TextBox ID="txtPassword" runat="server" Width="350px" TextMode="Password" 
                        CssClass="myTexto" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                        ControlToValidate="txtPassword" ErrorMessage="Contraseña" 
                        ValidationGroup="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda" style="width: 93px">
                    Perfil:</td>
                <td style="width: 497px"  >
                    <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="myTexto">
                    </asp:DropDownList>
                    <asp:RangeValidator ID="rvPerfil" runat="server" ControlToValidate="ddlPerfil" 
                        ErrorMessage="Perfil" MaximumValue="999999" MinimumValue="1" Type="Integer" 
                        ValidationGroup="0">*</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda" style="width: 93px">
                    Activo:</td>
                <td style="width: 497px"  >
                    <asp:CheckBox ID="chkActivo" runat="server" Checked="True" />
                </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda" colspan="2">
                    <hr /></td>
            </tr>
            <tr>
                <td>
                     <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="UsuarioList.aspx" CausesValidation="False">Regresar</asp:LinkButton></td>
                <td align="right" style="width: 497px">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" 
                        onclick="btnGuardar_Click1" CssClass="myButton" ValidationGroup="0" />
                   
                </td>
            </tr>
            <tr>
                <td>
                     &nbsp;</td>
                <td align="right" style="width: 497px">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        HeaderText="De completar datos requeridos:" ShowMessageBox="True" 
                        ShowSummary="False" ValidationGroup="0" />
                   
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
    </div>
    
 </asp:Content>