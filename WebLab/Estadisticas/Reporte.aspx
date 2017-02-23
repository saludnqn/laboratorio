<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporte.aspx.cs" Inherits="WebLab.Estadisticas.Reporte" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
 <script language="Javascript" type="text/javascript" src="../FusionCharts/FusionCharts.js"></script> 
  <script type="text/javascript">


                                                                                                           function printDiv(divName) {
                                                                                                               var printContents = document.getElementById(divName).innerHTML;
                                                                                                               var originalContents = document.body.innerHTML;
                                                                                                               document.body.innerHTML = printContents;
                                                                                                               window.print();
                                                                                                               document.body.innerHTML = originalContents;
                                                                                                           }
     
  </script>  
 <br />   &nbsp;
    

 <div align="left" style="width:1000px">
    <table align="center" width="800px">
<tr>
<td class="mytituloGris" colspan="2"> 
    <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>
    <br />
</td>
</tr>
<tr>
<td colspan="2"><hr /></td>
</tr>
<tr>
<td align="left"> 
  
 
    <asp:Label ID="lblFiltro" runat="server" CssClass="myLabelIzquierda" 
        Text="Label"></asp:Label>
  
 
    </td>
<td align="right"> 
         <asp:ImageButton ID="imgPdf" runat="server" 
            ImageUrl="~/App_Themes/default/images/pdf.jpg" onclick="imgPdf_Click" 
            ToolTip="Exportar a Pdf" />
&nbsp;
        <%--<asp:ImageButton ID="imgImprimir" runat="server" 
            ImageUrl="~/App_Themes/default/images/imprimir.jpg" onclick="imgImprimir_Click" 
            ToolTip="Imprimir reporte" Visible="False" />--%>
        &nbsp;
        <asp:ImageButton ID="imgExcel" runat="server" 
            ImageUrl="~/App_Themes/default/images/excelPeq.gif" onclick="imgExcel_Click" 
            ToolTip="Exportar a Excel" />
        &nbsp;</td>
</tr>

      
       
<tr>
<td colspan="2">   
<asp:Panel ID="pnlGrafico" runat="server">
   <div style="border: 1px solid #C0C0C0">
       <asp:Literal ID="FCLiteral" runat="server"></asp:Literal>
     

  
       <asp:Literal  ID="FCLiteral0" runat="server"></asp:Literal> </div>
    </asp:Panel>
      </td>
</tr> 
<tr>
<td colspan="2"> 
  
      </td>
</tr>
<tr>
<td colspan="2"> 
    <div id="printableArea"  style="border: 1px solid #C0C0C0">
       <asp:GridView ID="gvEstadistica" runat="server" CellPadding="2" 
           ForeColor="Black" ShowFooter="True" Width="100%" Font-Size="10pt" 
           onrowdatabound="gvEstadistica_RowDataBound">
           <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
           <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
           <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
           <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
           <EditRowStyle BackColor="#999999" />
           <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
       </asp:GridView>
        
  </div>
  <br />
  <img src="../App_Themes/default/images/imprimir.jpg" onclick="printDiv('printableArea')" /> 
  
   
    </td>
</tr>
<tr>
<td colspan="2"><hr /></td>
</tr>
<tr>
<td colspan="2">        
       <asp:LinkButton 
                            ID="lnkRegresar" runat="server" CssClass="myLink"  
           ValidationGroup="0" onclick="lnkRegresar_Click">Regresar</asp:LinkButton>
                          </td>
</tr>
</table> 
   </div>
    </asp:Content>