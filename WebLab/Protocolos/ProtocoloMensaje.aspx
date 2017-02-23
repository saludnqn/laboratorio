<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProtocoloMensaje.aspx.cs" Inherits="WebLab.Protocolos.ProtocoloMensaje" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">         

    <table  width="940px" align="center">
    <tr>
    <td colspan="2" style="font-weight: bold">
      <img alt="" src="../App_Themes/default/images/tres.jpg" />&nbsp;Previsualización del 
        Protocolo Generado</td>
    </tr>
    <tr>
    <td colspan="2">
       <hr /></td>
    </tr>
    <tr>
    <td>
                         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="true" />
                         </td>
    <td style="vertical-align: top">
        <asp:Panel ID="Panel1" runat="server">
      
        <img alt="" src="../App_Themes/default/images/imprimir.jpg"/>&nbsp;<asp:LinkButton 
                            ID="lnkImprimir" runat="server" CssClass="myLittleLink" onclick="lnkImprimir_Click">Imprimir</asp:LinkButton>
        
         
          <br />
                         <img alt="" src="../App_Themes/default/images/pdf.jpg"/>&nbsp;<asp:LinkButton 
                            ID="lnkPDF" runat="server" CssClass="myLittleLink" onclick="lnkPDF_Click">Visualizar
                            en pdf</asp:LinkButton>
         </asp:Panel>
       
        </td>    
    </tr>
    <tr>
    <td colspan="2">
        <hr /></td>
    </tr>
    <tr>
    <td>
        <asp:LinkButton 
                            ID="lnkRegresar" runat="server" CssClass="myLink" 
                onclick="lnkRegresar_Click">Regresar</asp:LinkButton>
        
         
                         </td>
    <td>
        &nbsp;</td>    
    </tr>
    </table>
   

 </asp:Content>
