<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="WebLab.Login" MasterPageFile="~/Site1.Master" %>

<%@ Register src="login.ascx" tagname="login" tagprefix="uc1" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

  
   
   
</asp:Content>




<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          

<div align="center" style="width:100%">

<br />   &nbsp;<br />   &nbsp;<br />
<br />
    <table style="border: 1px solid #C0C0C0; width: 250px;">
        <tr>
            
         
          <td class="myLabelIzquierda">
              <asp:Label ID="lblSubtitulo" runat="server" Text="Label"></asp:Label>   
            </td>
           
        </tr>
        <tr>
            <td align="center">
            
                
                <uc1:login ID="login1" runat="server" />
            
                
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
           
        </tr>
                
    </table>

<br />
    
</div>

</asp:Content>