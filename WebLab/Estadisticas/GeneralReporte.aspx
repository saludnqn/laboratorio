<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralReporte.aspx.cs" Inherits="WebLab.Estadisticas.GeneralReporte" MasterPageFile="~/Site1.Master" EnableEventValidation="false" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 



    <table align="center" width="1100px">
<tr>
<td colspan="2" class="mytituloTabla"> 
    VISUALIZACION DEL REPORTE<br />
</td>
</tr>
<tr>
<td colspan="2"><hr /></td>
</tr>
<tr>
<td> 
  
   
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="True" Height="50px" 
        Width="350px"  />
  
   
   </td>
<td style="vertical-align: top"> 
  
   
      <img alt="" src="../App_Themes/default/images/pdf.jpg"/>
       <asp:LinkButton 
                            ID="lnkPdf" runat="server" CssClass="myLittleLink"  
           ValidationGroup="0" onclick="lnkPdf_Click">Visualizar 
                        en formato pdf</asp:LinkButton>
                        <br />
                             <img alt="" src="../App_Themes/default/images/imprimir.jpg"/>&nbsp;<asp:LinkButton 
                            ID="lnkImprimir" runat="server" CssClass="myLittleLink" 
                                               ValidationGroup="0" 
          onclick="lnkImprimir_Click">Imprimir</asp:LinkButton>
                        <br />                       
                                             <img alt="" src="../App_Themes/default/images/excelPeq.gif"/>&nbsp;<asp:LinkButton ID="lnkExcel" 
           runat="server" CssClass="myLittleLink" 
                                                   ValidationGroup="0" onclick="lnkExcel_Click1" 
           >Visualizar en formato Excel</asp:LinkButton>  </td>
</tr>
<tr>
<td colspan="2"><hr /></td>
</tr>
<tr>
<td>        
       <asp:LinkButton 
                            ID="lnkRegresar" runat="server" CssClass="myLink"  
           ValidationGroup="0" onclick="lnkRegresar_Click">Regresar</asp:LinkButton>
                          </td>
<td>        
                            &nbsp;</td>
</tr>
</table> 
   
    </asp:Content>