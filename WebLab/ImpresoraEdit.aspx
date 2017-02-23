<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImpresoraEdit.aspx.cs" Inherits="WebLab.ImpresoraEdit" MasterPageFile="~/Site1.Master" %>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

<title>LABORATORIO</title> 

   


</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
       
     <div>
     <table width="400" style="width: 400px" >
     <tr>
     <td colspan="2"  >Impresoras</td>
     </tr>
     <tr>
     <td class="style2" colspan="2"><hr /></td>
     </tr>
     <tr>
     <td class="style2" style="width: 134px">Impresora del Sistema:</td>
     <td style="width: 256px">
         <asp:DropDownList ID="ddlImpresora" runat="server">
         </asp:DropDownList>
         <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
             onclick="btnAgregar_Click" />
 </td>
         <tr>
     <td class="style2" colspan="2"><hr />
         <asp:ListBox ID="lstImpresora" runat="server" Width="350px"></asp:ListBox>
             </td>
     
     </tr>
     </tr>
         <tr>
     <td class="style2" align="right" colspan="2"><hr />
         <asp:Button ID="btnGuardar" runat="server" onclick="btnGuardar_Click" 
             Text="Guardar" />
             </td>
     
     </tr>
         <tr>
     <td class="style2" style="width: 134px">&nbsp;</td>
     <td style="width: 256px">
         &nbsp;</td>
     
     </tr>
     </table>
         
              </div>                                                                                        
    </asp:Content>
