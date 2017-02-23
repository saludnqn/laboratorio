<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordEdit.aspx.cs" Inherits="WebLab.Usuarios.PasswordEdit" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

 <link href="../App_Themes/default/style.css" rel="stylesheet" type="text/css" />  
     

   
  
   
  
     

   
  
   
    </asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
 <br />   &nbsp;
        <div style="width:800px;" align="left">
        <table style="width:600px">
            <tr>
            
            <td colspan="2"  ><b  class="mytituloTabla">validacion<br />
                <br />
                CAMBIO DE CONTRASEÑA</b></td>
            </tr>
            <tr>
                <td  colspan="2">
                   <hr /></td>
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
                <td class="myLabelIzquierda">
                    Usuario:</td>
                <td class="style2"  >
                    <asp:Label ID="lblUsuario" runat="server" Text="Label" 
                        CssClass="myLabelIzquierda2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda">
                    Contraseña Anterior:</td>
                <td class="style2"  >
                    <asp:TextBox ID="txtPasswordActual" runat="server" Width="350px" CssClass="myTexto" 
                        MaxLength="50" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" 
                        ControlToValidate="txtPasswordActual" ErrorMessage="Usuario" 
                        ValidationGroup="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda">
                    Nueva
                    Contraseña:</td>
                <td class="style2"  >
                    <asp:TextBox ID="txtPasswordNueva" runat="server" Width="350px" TextMode="Password" 
                        CssClass="myTexto" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                        ControlToValidate="txtPasswordNueva" ErrorMessage="Contraseña" 
                        ValidationGroup="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda">
                    Confirmación Contraseña:</td>
                <td class="style2"  >
                    <asp:TextBox ID="txtPasswordNueva1" runat="server" Width="350px" TextMode="Password" 
                        CssClass="myTexto" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword0" runat="server" 
                        ControlToValidate="txtPasswordNueva1" ErrorMessage="Contraseña" 
                        ValidationGroup="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda" colspan="2">
                    <hr /></td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" 
                        onclick="btnGuardar_Click1" CssClass="myButton" ValidationGroup="0" />
                   
                </td>
            </tr>
            <tr>
                <td colspan="2">
                     <asp:CompareValidator ID="CompareValidator1" runat="server" 
                         ControlToCompare="txtPasswordNueva" ControlToValidate="txtPasswordNueva1" 
                         ErrorMessage="La nueva contraseña no coincide con la confirmacion de la contraseña" 
                         ValidationGroup="0"></asp:CompareValidator>
                     <br />
                     <asp:CustomValidator ID="CustomValidator1" runat="server" 
                         ControlToValidate="txtPasswordActual" 
                         ErrorMessage="La contraseña actual es incorrecta. Verifique." 
                         onservervalidate="CustomValidator1_ServerValidate" ValidationGroup="0"></asp:CustomValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        HeaderText="De completar datos requeridos:" 
                        ShowSummary="False" ValidationGroup="0" />
                   
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
    </div>
    
 </asp:Content>