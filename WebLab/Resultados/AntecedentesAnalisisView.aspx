<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AntecedentesAnalisisView.aspx.cs" Inherits="WebLab.Resultados.AntecedentesAnalisisView" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>       
      <link type="text/css"rel="stylesheet"      href="../App_Themes/default/style.css" />  
      <link type="text/css"rel="stylesheet"      href="../App_Themes/default/principal/style.css" />  
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
 
</head>

<body> 
  <div id="printableArea">    
    <form id="form1" runat="server">
    
  
   
                                               
         <div align="left" style="width:790px">


       <table  width="750px"  
                     
                     
                     style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal; color: #000000" 
                     cellpadding="1" cellspacing="1" align="left">

        <tr>
                     <td class="myLabelIzquierda">
                         <asp:Label  ID="lblPaciente" runat="server" Text="Label"></asp:Label>
                     </td>
                     <td align="center">  
                         <asp:ImageButton ID="imgPdf" runat="server" ImageUrl="~/App_Themes/default/images/pdf.jpg" onclick="imgPdf_Click" ToolTip="Exportar a Pdf" />
                     </td>
        </tr>
  
        <tr>
            <td colspan="2">
          
               <asp:Panel ID="pnlGrafico" runat="server">
         <hr />

   <div >
       <asp:Literal ID="FCLiteral" runat="server"></asp:Literal>
</div>
   </asp:Panel></td>
        </tr>
        <tr>
            <td colspan="2">
              <div align="left" style="border: 1px solid #999999; overflow: scroll; overflow-x:hidden; height: 85px; background-color: #F7F7F7;">
                <asp:GridView ID="gvHistorico" runat="server" AutoGenerateColumns="False"  DataKeyNames="idProtocolo" Width="100%"  EmptyDataText="No se encontraron datos para los filtros de búsqueda ingresados" Font-Size="8">
                    <Columns>
                        <asp:BoundField DataField="numero" HeaderText="Protocolo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="solicitante" HeaderText="Solicitante">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="resultadoNum" HeaderText="Resultado">
                            <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                        </asp:BoundField>
                         <%--<asp:BoundField DataField="resultadoCar" HeaderText="Resultado">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="unidad" HeaderText="U.Med">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ValorReferencia" HeaderText="V.Referencia">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="metodo" HeaderText="Metodo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="firmante" HeaderText="Validado por." />
                    </Columns>
                </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
       
    </table>
    </div>
                                               
    
  
    </form> 
     </div>
</body>
</html>
