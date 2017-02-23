﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditoriaView.aspx.cs" Inherits="WebLab.Resultados.AuditoriaView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  
     
     <link type="text/css"rel="stylesheet"      href="../App_Themes/default/style.css" />  
      <link type="text/css"rel="stylesheet"      href="../App_Themes/default/principal/style.css" />  
         
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
    <form id="form1" runat="server">
    <div id="printableArea">
                                               
        <table width="800">
            <tr>
                <td class="myLabelIzquierdaGde">                                                               
                 
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                 <br /> &nbsp;  
                </td>
                <td class="myLabelIzquierda" align="center">                                                               
                 
                    <asp:ImageButton ID="imgPdf" runat="server" 
                        ImageUrl="~/App_Themes/default/images/pdf.jpg" onclick="imgPdf_Click" 
                        ToolTip="Descargar Reporte en Pdf" />
&nbsp;Descargar Pdf</td>
            </tr>
            <tr>
                <td colspan="2">
                                   <div  style="width:95%;height:300pt;overflow:scroll;overflow-x:hidden;border:1px solid #CCCCCC;">                            
                    <asp:GridView ID="gvLista" runat="server" 
                        CellPadding="1" Font-Size="10pt" 
                        Width="98%" CssClass="mytable1" AutoGenerateColumns="False" 
                        EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha y Hora" >                           
                            <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="username" HeaderText="Usuario" >
                            <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="accion" HeaderText="Acción" >
                            <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="analisis" HeaderText="Análisis" >
                            <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor" HeaderText="Valor" >
                            <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valoranterior" HeaderText="Valor ant." >
                            <ItemStyle Width="100px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                            ForeColor="#333333" BorderColor="#666666" />
                    </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;&nbsp;
                   
                </td>
            </tr>
        </table>
                                               
    
    </div>
    </form>
</body>
</html>
