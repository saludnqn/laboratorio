<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportePorResultadoNum.aspx.cs" Inherits="WebLab.Estadisticas.ReportePorResultadoNum" MasterPageFile="~/Site1.Master" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
<link type="text/css"rel="stylesheet"      href="../App_Themes/default/style.css" />  
    <link type="text/css"rel="stylesheet"      href="../script/jquery-ui-1.7.1.custom.css" />  



  <script type="text/javascript"      src="../script/jquery.min.js"></script> 
  <script type="text/javascript"      src="../script/jquery-ui.min.js"></script> 
    
      <script type="text/javascript"     src="../script/jquery.ui.datepicker-es.js"></script>   
      
      <script type="text/javascript"> 
     

	$(function() {
		$("#<%=txtFechaDesde.ClientID %>").datepicker({
			showOn: 'button',
			buttonImage: '../App_Themes/default/images/calend1.jpg',
			buttonImageOnly: true
		});
	});

	$(function() {
		$("#<%=txtFechaHasta.ClientID %>").datepicker({
			showOn: 'button',
			buttonImage: '../App_Themes/default/images/calend1.jpg',
			buttonImageOnly: true
		});
	});
 
     
  </script>  
  
  
   	 <script type="text/javascript" src="../script/Mascara.js"></script>
    <script type="text/javascript" src="../script/ValidaFecha.js"></script>   
    
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
 
    <script language="Javascript" type="text/javascript" src="../FusionCharts/FusionCharts.js"></script>


    <br />
    <table align="center" width="1000px">

<tr>
<td class="mytituloTabla" colspan="5"> 
    REPORTE ESTADISTICO POR RESULTADOS NUMERICOS<br />
</td>
<td   align="right"> 
      <a href="../Help/Documentos/Estadísticas Por Resultado Numericos.htm" title="Ayuda" target="_blank"><img style="border:0" src="../App_Themes/default/images/information.png" /></a></td>
</tr>
<tr>
<td colspan="6"> <hr class="hrTitulo" /></td>
</tr>

      
       
<tr>
<td    align="left" class="myLabelIzquierda"> 
  
 
    Análisis:&nbsp; </td>
<td align="left"  colspan="4"> 
  
 
                            <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlArea_SelectedIndexChanged">
                            </asp:DropDownList>
                                        
 
                            <asp:DropDownList ID="ddlAnalisis" runat="server">
                                <asp:ListItem Selected="True">Chagas</asp:ListItem>
                                <asp:ListItem>HIV</asp:ListItem>
                                <asp:ListItem Value="Toxo">Toxoplasmosis</asp:ListItem>
                                <asp:ListItem>VDRL</asp:ListItem>
                            </asp:DropDownList>
                                        
                                            <asp:RangeValidator ID="rvAnalisis" runat="server" 
                                ControlToValidate="ddlAnalisis" ErrorMessage="RangeValidator" 
                                MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">Seleccione análisis</asp:RangeValidator>
                                        
                                            </td>
</tr>

      
       
<tr>
<td   align="left" class="myLabelIzquierda"> 
  
 
    Fecha Desde:</td>
<td align="left"   colspan="5"> 
  
 
                         <table><tr><td> 
  
 
  <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de inicio"  /></td><td class="myLabelIzquierda">
                                 &nbsp;</td><td class="myLabelIzquierda">Fecha Hasta:</td><td><input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="4" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de fin"  /><asp:CustomValidator ID="cvFechas" runat="server" 
                                ErrorMessage="Fechas de inicio y de fin" 
                                onservervalidate="cvFechas_ServerValidate" ValidationGroup="0">Formato inválido de fecha</asp:CustomValidator>
                                        
                                            </td></tr></table></td>
</tr>

      
       
<tr>
<td   align="left" class="myLabelIzquierda"> 
  
 
    Valor Desde:</td>
<td  align="left" colspan="2"> 
  
 
   <table><tr><td> 
  
 

                            <asp:TextBox ID="txtValorDesde" CssClass="myTexto" runat="server" Width="80px"></asp:TextBox>
                           
                           
                            </td><td class="myLabelIzquierda">&nbsp;</td><td class="myLabelIzquierda">Valor Hasta:</td><td> 
  
 

                            <asp:TextBox ID="txtValorHasta" CssClass="myTexto" runat="server" Width="80px"></asp:TextBox>
                           
                           
                            </td></tr></table></td>
<td   align="left"> 
  
 
                            &nbsp;</td>
<td    align="left"> 
  
 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtValorDesde" 
                                ErrorMessage="Debe ingresar solo valores numericos" 
                                ValidationExpression="^[0-9]{1,5}(\.[0-9]{0,2})?$" ValidationGroup="0"></asp:RegularExpressionValidator>
  
 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="txtValorHasta" 
                                ErrorMessage="Debe ingresar solo valores numericos" 
                                ValidationExpression="^[0-9]{1,5}(\.[0-9]{0,2})?$" ValidationGroup="0"></asp:RegularExpressionValidator>
                                    </td>
<td align="left"  > 
  
 

                            &nbsp;</td>
</tr>

      
       
<tr>
<td class="myLabelGris" align="left" colspan="5"> 
  
 
    Si desea consultar la cantidad de casos independientemente del resultado 
    obtenido no ingrese Valor Desde y Valor Hasta.</td>
<td align="left"  > 
  
 

                           &nbsp;</td>
</tr>

      
       
<tr>
<td   align="left" colspan="6"> 
  
 <hr /></td>
</tr>

      
       
<tr>
<td  align="left" class="myLabelIzquierda"> 
  
 
    Grupo Etáreo:</td>
<td align="left"   colspan="4"> 
  
 

                            <asp:DropDownList ID="ddlGrupoEtareo" runat="server">
                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">&lt;6 meses</asp:ListItem>
                                <asp:ListItem Value="2">6-11 meses</asp:ListItem>
                                <asp:ListItem Value="3">12-23 meses</asp:ListItem>
                                <asp:ListItem Value="4">2-4 años</asp:ListItem>
                                <asp:ListItem Value="5">5-9 años</asp:ListItem>
                                <asp:ListItem Value="6">10-14 años</asp:ListItem>
                                <asp:ListItem Value="7">15-19 años</asp:ListItem>
                                <asp:ListItem Value="8">20-24 años</asp:ListItem>
                                <asp:ListItem Value="9">25-34 años</asp:ListItem>
                                <asp:ListItem Value="10">35-44 años</asp:ListItem>
                                <asp:ListItem Value="11">45-54 años</asp:ListItem>
                                <asp:ListItem Value="12">55-64 años</asp:ListItem>
                                <asp:ListItem Value="13">65-74 años</asp:ListItem>
                                <asp:ListItem Value="14">&gt;= 75 años</asp:ListItem>
                            </asp:DropDownList>
    </td>
<td align="left"  > 
  
 

                           &nbsp;</td>
</tr>

      
       
<tr>
<td  align="left" class="myLabelIzquierda"> 
  
 
    Sexo:</td>
<td align="left"  > 
  
 

                            <asp:DropDownList ID="ddlSexo" runat="server">
                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">Femenino</asp:ListItem>
                                <asp:ListItem Value="2">Masculino</asp:ListItem>
                            </asp:DropDownList>   
    </td>
<td align="left"   colspan="3"> 
  
 

                           <asp:RadioButtonList ID="rdbPaciente" runat="server" 
                                RepeatDirection="Horizontal" CssClass="myLabelIzquierda">
                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">Solo Embarazadas</asp:ListItem>
                            </asp:RadioButtonList>
    </td>
<td align="left"  > 
  
 

                           &nbsp;</td>
</tr>

      
       
<tr>
<td   align="left"> 
  
 
    &nbsp;</td>
<td align="left"  > 
  
 

                           <asp:Button ID="btnGenerar" runat="server" CssClass="myButton" 
                                onclick="btnGenerar_Click" Text="Generar Reporte" Width="131px" 
                                ValidationGroup="0" />
    </td>
<td align="left"  > 
  
 

                           &nbsp;</td>
<td align="left"  > 
  
 

                           &nbsp;</td>
<td align="left"  > 
  
 

                           &nbsp;</td>
<td align="left"  > 
  
 

                           &nbsp;</td>
</tr>

      
       
<tr>
<td align="left" colspan="6"> 
  
 
    <hr /></td>
</tr>

      
       
<tr>
<td colspan="6"> 
  
      <asp:Panel ID="pnlResultado" runat="server" Visible="False">
          <table style="width:100%;">
              <tr>
                  <td>
                      <asp:Label CssClass="mytituloGris" ID="lblAnalisis" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                  </td>
                  <td align="right">
                     <asp:ImageButton ID="imgPdf" runat="server" 
            ImageUrl="~/App_Themes/default/images/pdf.jpg" 
            ToolTip="Exportar a Pdf" onclick="imgPdf_Click" style="height: 20px" />
&nbsp;
        &nbsp;
        <asp:ImageButton ID="imgExcel" runat="server" 
            ImageUrl="~/App_Themes/default/images/excelPeq.gif" onclick="imgExcel_Click" 
            ToolTip="Exportar a Excel" /></td>
              </tr>
              <tr>
                  <td colspan="2">
                      <asp:GridView ID="gvEstadistica" runat="server" Font-Bold="True" Font-Size="10pt" 
                          onrowcommand="gvEstadistica_RowCommand" 
                          onrowdatabound="gvEstadistica_RowDataBound" ShowFooter="True" 
                          BorderColor="#666666" BorderStyle="Double" BorderWidth="1px" 
                          AutoGenerateColumns="False">
                          <Columns>
                              <asp:BoundField DataField="Sexo" HeaderText="Sexo" >
                              <ItemStyle Width="10%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" >
                              <ItemStyle Width="5%" />
                              </asp:BoundField>
                            <asp:BoundField DataField="<6 meses" HeaderText="Menos de 6 meses" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="6 a 11 meses" HeaderText="6-11 meses" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="12 a 23 meses" HeaderText="12-23 meses" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="2 a 4 años" HeaderText="2-4 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="5 a 9 años" HeaderText="5-9 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="10 a 14 años" HeaderText="10-14 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="15 a 19 años" HeaderText="15-19 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="20 a 24 años" HeaderText="20-24 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="25 a 34 años" HeaderText="25-34 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="35 a 44 años" HeaderText="35-44 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="45 a 54 años" HeaderText="45-54 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="55 a 64 años" HeaderText="55-64 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="65 a 74 años" HeaderText="65-74 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="75 y +" HeaderText="75 años ó +" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                                  <asp:TemplateField HeaderText="Pacientes">
                                  <ItemTemplate>
                                      <asp:ImageButton ID="PacientesPDF" runat="server" CommandName="PacientesPDF" 
                                          ImageUrl="../App_Themes/default/images/pdf.jpg" />
                                  </ItemTemplate>
                                  <ItemStyle Height="20px" HorizontalAlign="Center" Width="5%" />
                              </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Pacientes">
                                  <ItemTemplate>
                                      <asp:ImageButton ID="PacientesEXCEL" runat="server" CommandName="PacientesEXCEL" 
                                          ImageUrl="../App_Themes/default/images/excelPeq.gif" />
                                  </ItemTemplate>
                                  <ItemStyle Height="20px" HorizontalAlign="Center" Width="5%" />
                              </asp:TemplateField>
                          </Columns>
                          <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                          <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                      </asp:GridView>
                  </td>
              </tr>
              <tr>
                  <td colspan="2">
                    <hr /></td>
              </tr>
          </table>
      </asp:Panel>
  
      </td>
</tr>
<tr>
<td colspan="6">&nbsp;</td>
</tr>
</table> 
   
    </asp:Content>