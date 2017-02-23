<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerfilEdit.aspx.cs" Inherits="WebLab.Usuarios.PerfilEdit" MasterPageFile="~/Site1.Master" %>




<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

 <link href="../App_Themes/default/style.css" rel="stylesheet" type="text/css" />  
     

   
  
   
    </asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <br />   &nbsp;
      <div style="width:1000px;" align="left">
        <table style="width:500px;">
            <tr>
                   <td colspan="2"  ><b  class="mytituloTabla">PERFIL DE USUARIO</b>
                    </td>
                    <td align="right"> <a href="../Help/Documentos/Perfil de Usuario.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                   <hr /></td>
            </tr>
            
            <tr>
                <td class="myLabelIzquierda"  style="vertical-align: top; width: 82px;">
                    Nombre Perfil:</td>
                <td class="style10" colspan="2">
                    <asp:TextBox ID="txtNombre" runat="server" Width="350px" CssClass="myTexto"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td class="myLabelIzquierda" style="width: 82px">
                    Activo:</td>
                <td class="style10">
                    <asp:CheckBox ID="chkActivo" runat="server" Checked="True" />
                </td>
                <td class="style10">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8" colspan="3">
                    <hr /></td>
            </tr>
            <tr>
                <td class="style8" style="width: 82px">
                  <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="PerfilList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                    </td>
                <td align="right" colspan="2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" 
                        onclick="btnGuardar_Click1" CssClass="myButton" />
                   
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
    </div>
    
 </asp:Content>