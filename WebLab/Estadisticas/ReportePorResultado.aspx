<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportePorResultado.aspx.cs" Inherits="WebLab.Estadisticas.ReportePorResultado" MasterPageFile="~/Site1.Master" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
      
 <script src="Resources/jquery.min.js" type="text/javascript"></script>
    <link href="Resources/jquery-ui-1.8.20.css" rel="stylesheet" type="text/css" />
    <script src="Resources/jQuery-ui-1.8.18.min.js" type="text/javascript"></script>
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
    
  
    </style>
    
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
 
    <script language="Javascript" type="text/javascript" src="../FusionCharts/FusionCharts.js"></script>
    <br />   &nbsp;

    <table align="center" width="1000px">

<tr>
<td class="mytituloTabla" colspan="3"> 
    REPORTE ESTADISTICO POR RESULTADOS PREDEFINIDOS<asp:HiddenField ID="HFTipoMuestra" runat="server" />
</td>
<td class="mytituloTabla" align="right" style="width: 187px" colspan="2"> 
            <a href="../help/documentos/Estadísticas Por Resultado Predefinido.htm" title="Ayuda"  target="_blank" ><img style="border:0" src="../App_Themes/default/images/information.png" /> </a></td>
</tr>
<tr>
<td colspan="5"> <hr class="hrTitulo" /></td>
</tr>

      
       
<tr>
<td  class="myLabelIzquierda"  align="left"> 
  
 
    Análisis:</td>
<td align="left" colspan="4" class="auto-style1"> 
  
 
                            <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlArea_SelectedIndexChanged" CssClass="myList">
                            </asp:DropDownList>
                                        
 
                            <asp:DropDownList ID="ddlAnalisis" runat="server" CssClass="myList">
                                <asp:ListItem Selected="True">Chagas</asp:ListItem>
                                <asp:ListItem>HIV</asp:ListItem>
                                <asp:ListItem Value="Toxo">Toxoplasmosis</asp:ListItem>
                                <asp:ListItem>VDRL</asp:ListItem>
                            </asp:DropDownList>
                                        
                                            <asp:RangeValidator ID="rvAnalisis" runat="server" 
                                ControlToValidate="ddlAnalisis" ErrorMessage="RangeValidator" 
                                MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">Seleccione 
                            una practica</asp:RangeValidator>
                                        
                                            </td>
</tr>

      
       
<tr>
<td class="myLabelIzquierda" align="left"> 
  
 
    Fecha Desde:</td>
<td class="style3"  align="left"> 
  
 
  <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de inicio"  /></td>
<td class="myLabelIzquierda"  align="left" colspan="2"> 
  
 
    &nbsp;</td>
<td class="style4"  align="left"> 
  
 

                            &nbsp;</td>
</tr>

      
       
<tr>
<td class="myLabelIzquierda" align="left"> 
  
 
    Fecha Hasta:</td>
<td class="style3"  align="left"> 
  
 
    <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="4" class="myTexto" 
                                style="width: 70px" title="Ingrese la fecha de fin"  /><asp:CustomValidator ID="cvFechas" runat="server" 
                                ErrorMessage="Formato inválido de fechas " 
                                onservervalidate="cvFechas_ServerValidate" ValidationGroup="0">Formato inválido de fechas </asp:CustomValidator>
                                    </td>
<td class="myLabelIzquierda"  align="left" colspan="2"> 
  
 
    &nbsp;</td>
<td class="style4"  align="left"> 
  
 

                            &nbsp;</td>
</tr>

      
       
<tr>
<td class="myLabelIzquierda" align="left" colspan="5"> 
  
 
    <hr /></td>
</tr>

      
       
<tr>
<td class="myLabelIzquierda" align="left"> 
  
 
    Grupo Etáreo:</td>
<td class="myLabelIzquierda"  align="left" colspan="4"> 
  
 

                            <asp:DropDownList ID="ddlGrupoEtareo" runat="server" CssClass="myList">
                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">&lt; 1 año</asp:ListItem>
                                <asp:ListItem Value="2">1 año</asp:ListItem>
                                <asp:ListItem Value="3">2-4 años</asp:ListItem>
                                <asp:ListItem Value="4">5-9 años</asp:ListItem>
                                <asp:ListItem Value="5">10-14 años</asp:ListItem>
                                <asp:ListItem Value="6">15-24 años</asp:ListItem>
                                <asp:ListItem Value="7">25-34 años</asp:ListItem>
                                <asp:ListItem Value="8">25-44 años</asp:ListItem>                                <asp:ListItem Value="9">45-64 años</asp:ListItem>
                                
                                <asp:ListItem Value="10">&gt; 65 años</asp:ListItem>
                            </asp:DropDownList>
                                   
                                    </td>
</tr>

      
       
<tr>
<td class="myLabelIzquierda" align="left"> 
  
 
    Sexo:</td>
<td  colspan="2" class="auto-style2"> 
  
 

                            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="myList">
                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">Femenino</asp:ListItem>
                                <asp:ListItem Value="2">Masculino</asp:ListItem>
                            </asp:DropDownList></td>
<td class="myLabelIzquierda"  align="left" colspan="2"> 
  
 

                           &nbsp;</td>
</tr>

      
       
<tr>
<td class="myLabelIzquierda" align="left"> 
  
 
    &nbsp;</td>
<td  colspan="2" class="auto-style2"> 
  
 

                            <asp:RadioButtonList ID="rdbPaciente" runat="server" 
                                RepeatDirection="Horizontal" CssClass="c">
                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">Solo Embarazadas</asp:ListItem>
                            </asp:RadioButtonList>
                                    </td>
<td class="myLabelIzquierda"  align="left" colspan="2"> 
  
 

                           &nbsp;</td>
</tr>

      
       
<tr>
<td class="myLabelIzquierda" align="left" colspan="5"> 
 <hr /></td>
</tr>

      
       
<tr>
<td class="myLabelIzquierda" align="left" style="vertical-align: top"> 
  
 
    Origen:</td>
<td align="left" colspan="2" class="auto-style2"> 
  
 

                            <div style="font-size: 9px; font-family: Verdana">
                                Seleccionar:
                                <asp:LinkButton ID="lnkMarcar" runat="server" CssClass="myLink" 
                                    onclick="lnkMarcar_Click" Font-Names="Verdana" Font-Size="8pt">Todas</asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="lnkDesmarcar" runat="server" CssClass="myLink" 
                                    onclick="lnkDesmarcar_Click" Font-Names="Verdana" Font-Size="8pt">Ninguna</asp:LinkButton>
                            </div>
  
 

                            <asp:CheckBoxList ID="ChckOrigen" runat="server" RepeatDirection="Vertical" 
                                RepeatColumns="5" CssClass="myList">
                            </asp:CheckBoxList>
    </td>
<td align="left" style="width: 187px" colspan="2"> 
  
 

                           <asp:Button ID="btnGenerar" runat="server" CssClass="myButton" 
                                onclick="btnGenerar_Click" Text="Generar Reporte" Width="131px" 
                                ValidationGroup="0" Height="35px" /></td>
</tr>

      
       
<tr>
<td align="left" colspan="5"> 
  
 
    <hr /></td>
</tr>

      
       
<tr>
<td colspan="5"> 
  
      <asp:Panel ID="pnlResultado" runat="server" Visible="False">
          <table style="width:100%;">
              <tr>
                  <td>
                      <asp:Label ID="lblAnalisis" runat="server" CssClass="mytituloGris" 
                          Font-Bold="True" Text="Label"></asp:Label>
                  </td>
                  <td align="right">
                     <asp:ImageButton ID="imgPdf" runat="server" 
            ImageUrl="~/App_Themes/default/images/pdf.jpg" 
            ToolTip="Exportar a Pdf" onclick="imgPdf_Click" />
&nbsp;
        &nbsp;
        <asp:ImageButton ID="imgExcel" runat="server" 
            ImageUrl="~/App_Themes/default/images/excelPeq.gif" onclick="imgExcel_Click" 
            ToolTip="Exportar a Excel" /></td>
              </tr>
              <tr>
                  <td colspan="2">
                      <asp:GridView ID="gvEstadistica" runat="server" AutoGenerateColumns="False" 
                          DataKeyNames="idItem" Font-Bold="True" Font-Size="8pt" 
                          onrowcommand="gvEstadistica_RowCommand" 
                          onrowdatabound="gvEstadistica_RowDataBound" ShowFooter="True" Width="1000px"
                            BorderColor="#666666" BorderStyle="Double" BorderWidth="1px" 
                          CellPadding="1" Font-Names="Verdana">
                             <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                          <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                             <RowStyle BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px" />
                          <Columns>
                              <asp:BoundField DataField="RESULTADO" HeaderText="RESULTADO" >
                                  <ItemStyle Width="20%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="CANTIDAD" HeaderText="CANTIDAD DE CASOS" >
                                  <ItemStyle Width="10%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="<1" HeaderText="Menos 1 año" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="1" HeaderText="1 año" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="2 a 4" HeaderText="2-4 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="5 a 9" HeaderText="5-9 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="10 a 14" HeaderText="10-14 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="15 a 24" HeaderText="15-24 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="25 a 34" HeaderText="25-34 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="35 a 44" HeaderText="35-44 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="45 a 64" HeaderText="45-64 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="65 y +" HeaderText="Mas 65 años" >
                                  <FooterStyle BackColor="#CC3300" />
                                  <HeaderStyle BackColor="#CC3300" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="M" HeaderText="Masc." >
                                  <FooterStyle BackColor="#336699" />
                                  <HeaderStyle BackColor="#336699" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="F" HeaderText="Fem." >
                                  <FooterStyle BackColor="#336699" />
                                  <HeaderStyle BackColor="#336699" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="I" HeaderText="Indet." >
                                  <FooterStyle BackColor="#336699" />
                                  <HeaderStyle BackColor="#336699" />
                                  <ItemStyle Width="5%" />
                              </asp:BoundField>
                              <asp:TemplateField HeaderText="Pacientes">
                                  <ItemTemplate>
                                      <asp:ImageButton ID="PacientesPDF" runat="server" CommandName="PacientesPDF" 
                                          ImageUrl="~/App_Themes/default/images/pdf.jpg" />
                                  </ItemTemplate>
                                  <ItemStyle Height="20px" HorizontalAlign="Center" Width="5%" />
                              </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Pacientes">
                                  <ItemTemplate>
                                      <asp:ImageButton ID="PacientesEXCEL" runat="server" CommandName="PacientesEXCEL" 
                                          ImageUrl="~/App_Themes/default/images/excelPeq.gif" />
                                  </ItemTemplate>
                                  <ItemStyle Height="20px" HorizontalAlign="Center" Width="5%" />
                              </asp:TemplateField>
                          </Columns>
                      </asp:GridView>
                      <br />
                        <asp:ImageButton ToolTip="Ver grafico de tortas" ID="btnVerGraficoTipoMuestra"  runat="server"  ImageUrl="~/App_Themes/default/images/ico_torta.png"  OnClientClick="verGrafico('torta'); return false;"                       />
                                 &nbsp;&nbsp;<asp:ImageButton ToolTip="Ver grafico de barras" ID="btnVerGraficoTipoMuestra2"  runat="server"  ImageUrl="~/App_Themes/default/images/ico_barra.png"  OnClientClick="verGrafico('barra'); return false;"                       />
                
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
</table> 
   <script src="../script/Resources/jquery.min.js" type="text/javascript"></script>
 <link href="../script/Resources/jquery-ui-1.8.20.css" rel="stylesheet" type="text/css" />   
    <script src="../script/Resources/jQuery-ui-1.8.18.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    var valores = $("#<%= HFTipoMuestra.ClientID %>").val();
    

    function verGrafico(tipoGrafico) {
        var dom = document.domain;
        var domArray = dom.split('.');
        for (var i = domArray.length - 1; i >= 0; i--) {
            try {
                var dom = '';
                for (var j = domArray.length - 1; j >= i; j--) {
                    dom = (j == domArray.length - 1) ? (domArray[j]) : domArray[j] + '.' + dom;
                }
                document.domain = dom;
                break;
            } catch (E) {
            }
        }


        var $this = $(this);
        $('<iframe src="Grafico.aspx?valores=' + valores + '&tipo=3&tipoGrafico=' + tipoGrafico + '" />').dialog({
            title: 'Gráfico Estadístico',
            autoOpen: true,
            width: 900,
            height: 500,
            modal: true,
            resizable: false,
            autoResize: true,
            overlay: {
                opacity: 0.5,
                background: "black"
            }
        }).width(900);
    }


    
    </script>

    </asp:Content>