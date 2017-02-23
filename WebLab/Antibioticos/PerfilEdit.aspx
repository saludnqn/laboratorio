<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerfilEdit.aspx.cs" Inherits="WebLab.Antibioticos.PerfilEdit" MasterPageFile="~/Site1.Master" %>


<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>


<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    
 
  
   <link href="../App_Themes/default/style.css" rel="stylesheet" type="text/css" /> 


   
    <style type="text/css">
        .style1
        {
            border-style: none;
            font-size: 10pt;
            font-family: Calibri;
            background-color: #FFFFFF;
            color: #333333;
            font-weight: bold;
            width: 32px;
        }
        .style2
        {
            width: 32px;
        }
    </style>


   
    </asp:Content>




 
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          
<br />&nbsp;
<br />&nbsp;

  <table>
  <tr>
     	<td class="style1" >&nbsp;</td>
                                              
     	<td class="myLabelIzquierda" colspan="4" ><b  class="mytituloTabla">PERFIL DE ANTIBIOTICOS</b><hr /></td>
                                              
                                        </tr>
  <tr>
        <td class="style1">
            &nbsp;</td>
                                              
        <td class="myLabelIzquierda" colspan="4">
            Perfil:&nbsp;&nbsp;<asp:TextBox 
                ID="txtNombre" runat="server" MaxLength="50" Width="350px"></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtNombre" ErrorMessage="Nombre del Perfil" 
                ValidationGroup="0">*</asp:RequiredFieldValidator>
                                               </td>
                                              
                                        </tr>
  <tr>
        <td style="vertical-align: top" class="style2">
            &nbsp;</td>
                                              
        <td style="vertical-align: top">
        <p class="myLabelIzquierda">Antibiotico</p>
                                               <anthem:ListBox ID="lstAntibiotico" 
                runat="server" AutoCallBack="True" 
                                                   CssClass="myTexto" Height="300px" 
                Width="400px" SelectionMode="Multiple">
                                               </anthem:ListBox>
                                               </td>
                                              
        <td colspan="2">
                                    
                                                 <anthem:ImageButton ID="btnAgregarDiagnostico" runat="server" 
                                                     ImageUrl="~/App_Themes/default/images/añadir.jpg" 
                                                     onclick="btnAgregarDiagnostico_Click" /><br />
                                                     <p></p>
                                                 <anthem:ImageButton ID="btnSacarDiagnostico" runat="server" 
                                                     ImageUrl="~/App_Themes/default/images/sacar.jpg" 
                                                     onclick="btnSacarDiagnostico_Click" />

                                                     </td>                                   

                                                     <td>
                                    
                                             <p class="myLabelIzquierda">Antibioticos del Perfil&nbsp;&nbsp;
                                                 <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                                     ErrorMessage="Debe agregar antibioticos al perfil" 
                                                     onservervalidate="CustomValidator1_ServerValidate" ValidationGroup="0"></asp:CustomValidator>
                                                         </p>
                                                 <anthem:ListBox ID="lstAntibioticoFinal" runat="server" CssClass="myTexto" 
                                                     Height="300px" Width="400px" SelectionMode="Multiple">
                                                 </anthem:ListBox>
                                                 </td>

                                        </tr>
  <tr>
        <td class="style2">
            &nbsp;</td>
                                              
        <td colspan="4">
           <hr /></td>
                                              
                                        </tr>
  <tr>
        <td class="style2">
                                    &nbsp;</td>
                                              
        <td colspan="2">
                                    <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                        onclick="lnkRegresar_Click">Regresar</asp:LinkButton>
        </td>
                                              
        <td colspan="2" align="right">
                                                         <asp:Button ID="btnGuardar" runat="server" CssClass="myButton" 
                                                             onclick="btnGuardar_Click" Text="Guardar" ValidationGroup="0" />
                                                 </td>
                                              
                                        </tr>
  <tr>
        <td class="style2">
                                    &nbsp;</td>
                                              
        <td colspan="2">
                                    &nbsp;</td>
                                              
        <td colspan="2" align="right">
                                                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                             HeaderText="Debe completar los datos requeridos:" ShowMessageBox="True" 
                                                             ShowSummary="False" ValidationGroup="0" />
                                                 </td>
                                              
                                        </tr>
  <tr>
        <td class="style2">
                                    &nbsp;</td>
                                              
        <td colspan="2">
                                    &nbsp;</td>
                                              
        <td colspan="2" align="right">
                                                         &nbsp;</td>
                                              
                                        </tr>
                                        </table>
  
   </asp:Content>
