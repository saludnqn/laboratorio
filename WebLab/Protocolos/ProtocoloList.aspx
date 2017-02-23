<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProtocoloList.aspx.cs" Inherits="WebLab.Protocolos.ProtocoloList" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>



<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>



<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

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
  
 

   
    <style type="text/css">
        .style4
        {
            width: 16%;
        }
        .style5
        {
            width: 101px;
        }
        .style8
        {
            width: 101px;
            height: 28px;
        }
        .style9
        {
            height: 28px;
        }
        .style10
        {
            width: 16%;
            height: 28px;
        }
    </style>
  
 

   
    </asp:Content>
 
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          
    <br />   &nbsp;  <div align="left" style="width: 1200px" >


				 <table  width="1150px" align="center"                      
                     
                   
                     class="myTabla" >
				
					<tr>
					<td colspan="5">
					
					<table  width="1150px" align="center"                      
                     
                   
                     cellpadding="1" cellspacing="1" class="myTabla">
					<tr>
						<td colspan="5">
						<b class="mytituloTabla">
                            <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>
                            </b>      
                        <hr class="hrTitulo" /></td>
					</tr>
					<tr >
						<td class="myLabelIzquierda" >Servicio:</td>
						<td>
                            <asp:DropDownList ID="ddlServicio" runat="server" 
                                ToolTip="Seleccione el servicio" TabIndex="1" CssClass="myList" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddlServicio_SelectedIndexChanged1">
                            </asp:DropDownList>
                                        
                                            </td>
						<td class="myLabelIzquierda" colspan="2">
                            <asp:Label ID="lblNumeroTarjeta" runat="server" Text="Numero Tarjeta:"></asp:Label>
                        </td>
						<td>
                    <input id="txtNumeroTarjeta" runat="server" type="text" maxlength="9" 
                        tabindex="6" class="myTexto"  onblur="valNumero(this)"
                                style="width: 70px"  /><asp:CustomValidator ID="cvNumeroTarjeta" runat="server" 
                                ErrorMessage="Numero de Tarjeta" 
                                onservervalidate="cvNumeroTarjeta_ServerValidate" ValidationGroup="0">Sólo numeros (sin puntos ni espacios)</asp:CustomValidator>
                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Fecha Desde:</td>
						<td>
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px"  /></td>
						<td class="myLabelIzquierda" colspan="2" >
                            Fecha Hasta:</td>
						<td>
                    <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                        style="width: 70px"  onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="4" class="myTexto"  />
                         </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Sector/Servicio:</td>
						<td>
                                        <asp:DropDownList ID="ddlSectorServicio" runat="server" TabIndex="2" 
                                            ToolTip="Seleccione el sector">
                                        </asp:DropDownList>
                                        
                                            </td>
						<td class="myLabelIzquierda" colspan="2" >
                                            Nro. de Origen:</td>
						<td>
                            <asp:TextBox ID="txtNroOrigen" runat="server" CssClass="myTexto"></asp:TextBox>
                                     </td>
                            </tr>
                            
						<tr>
						<td class="myLabelIzquierda" >Protocolo Desde:</td>
						<td>
                    <input id="txtProtocoloDesde" runat="server" type="text" maxlength="9" 
                          tabindex="5" class="myTexto"  onblur="valNumero(this)"
                                style="width: 70px"  /><asp:CustomValidator ID="cvNumeroDesde" runat="server" 
                                ErrorMessage="Numero de Protocolo" 
                                onservervalidate="cvNumeros_ServerValidate" ValidationGroup="0" 
                                >Sólo numeros (sin puntos ni espacios)</asp:CustomValidator>
                            </td>
						<td class="myLabelIzquierda" colspan="2" >
                                            Protocolo Hasta:</td>
						<td>
                    <input id="txtProtocoloHasta" runat="server" type="text" maxlength="9" 
                        tabindex="6" class="myTexto"  onblur="valNumero(this)"
                                style="width: 70px"  /><asp:CustomValidator ID="cvNumeroHasta" runat="server" 
                                ErrorMessage="Numero de Protocolo" 
                                onservervalidate="cvNumeroHasta_ServerValidate" ValidationGroup="0">Sólo numeros (sin puntos ni espacios)</asp:CustomValidator>
                            </td>
						</tr>
						
						
						<tr>
						<td class="myLabelIzquierda" >Origen:</td>
						<td>
                            <asp:DropDownList ID="ddlOrigen" runat="server" 
                                ToolTip="Seleccione el origen" TabIndex="7" CssClass="myList">
                            </asp:DropDownList>
                                        
					
						<td class="myLabelIzquierda"  >
                                            Prioridad:</td>                                            
						<td colspan="2">
                            <asp:DropDownList ID="ddlPrioridad" runat="server" 
                                ToolTip="Seleccione la prioridad" TabIndex="8" CssClass="myList">
                            </asp:DropDownList></td>
                                        
					    </tr>
						<tr>
					<td class="myLabelIzquierda" >
                                            Efector Solicitante:</td>
						<td>
                            <asp:DropDownList ID="ddlEfector" runat="server" 
                                ToolTip="Seleccione el efector" TabIndex="9" CssClass="myList">
                            </asp:DropDownList>
                                        
                        </td>
						
						<td class="myLabelIzquierda" colspan="2" >
                                            Médico Solicitante:</td>
						
						<td>
                            <asp:DropDownList ID="ddlEspecialista" runat="server" 
                                ToolTip="Seleccione el especialista" TabIndex="10" CssClass="myList">
                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                            </asp:DropDownList>
                                        
                            </td>
						
					</tr>
						<tr>
					<td class="myLabelIzquierda">DNI Paciente:</td>
						<td  align="left">
                             <input id="txtDni" type="text" runat="server"  class="myTexto"  
                                onblur="valNumero(this)" tabindex="11"/><asp:CompareValidator ID="cvDni" 
                                 runat="server" ControlToValidate="txtDni" 
                                ErrorMessage="Debe ingresar solo numeros" Operator="DataTypeCheck" 
                                Type="Integer" ValidationGroup="0">Debe ingresar solo numeros</asp:CompareValidator>
                                </td>
						
						<td class="myLabelIzquierda" colspan="2" >
                                            Estado:</td>
						
						<td>
                                            <asp:DropDownList ID="ddlEstado" runat="server" 
                                CssClass="myList" TabIndex="12">
                                                <asp:ListItem Selected="True" Value="-1">Todos los activos</asp:ListItem>
                                                <asp:ListItem Value="0">No Procesado</asp:ListItem>
                                                <asp:ListItem Value="1">En Proceso</asp:ListItem>
                                                <asp:ListItem Value="2">Terminado</asp:ListItem>
                                                <asp:ListItem Value="3">Eliminados</asp:ListItem>
                                            </asp:DropDownList>
                                            	<anthem:CheckBox ID="chkWhonet" runat="server" CssClass="myLabelIzquierda" 
                                Text="Incluir sólo Informados Whonet" Visible="False"  Enabled="false"
                                ToolTip="Agrega el protocolo en el listado para Whonet." />
                    
                                        
                            </td>
						
					</tr>
						<tr>
					<td class="myLabelIzquierda">Apellido/s:</td>
						<td  align="left">
                                            <asp:TextBox ID="txtApellido" runat="server" CssClass="myTexto" TabIndex="13" 
                                                Width="250px"></asp:TextBox>
                            </td>
						
						<td class="myLabelIzquierda" colspan="2" >
                                            Obra Social:</td>
						
						<td>
                                           <asp:DropDownList ID="ddlObraSocial" runat="server" CssClass="myList" 
                                                TabIndex="15" Width="400px" >
                                               
                                            </asp:DropDownList>
                                                </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" >
                                            Nombres/s:</td>
						<td align="left" colspan="2">
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="myTexto" TabIndex="14" 
                                                Width="300px"></asp:TextBox>
                        </td>
						
						<td align="right" colspan="2">
                        
                           
                                            <asp:CheckBox ID="chkRecordarFiltro" runat="server" CssClass="myLabelIzquierda" 
                                Text="Recordar filtros" Font-Italic="True" Visible="False" ForeColor="#993300" />
                        
                           
                        </td>
						
					</tr>
					</table>
					</td>
					</tr>
					<tr>
						<td   colspan="5">
                                            <hr /></td>
						
					</tr>
					<tr>
						<td   colspan="5"> 
                        <asp:CustomValidator ID="cvFechas" runat="server"  ErrorMessage="Fechas de inicio y de fin" onservervalidate="cvFechas_ServerValidate" ValidationGroup="0">Debe ingresar fechas de inicio y fin</asp:CustomValidator>
						
                        <asp:Panel ID="pnlControl" runat="server">
                                           
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td align=left>
                                                              
                                                            </td>
                                                            <td align=right>
                                                                
                                                                 &nbsp;&nbsp;&nbsp;
                                                                 <asp:Button ID="btnBuscarControl" runat="server" CssClass="myButton" 
                                                                     onclick="btnBuscarControl_Click" TabIndex="15" Text="Buscar" 
                                                                     ValidationGroup="0" Width="77px" />
                                                                 </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <hr /></td>
                                                        </tr>
                                                    </table>
                                                
                                            </asp:Panel>
                                            <asp:Panel ID="pnlListadoOrdenado" CssClass="myLabelIzquierda" runat="server">
                                                     

                                                    <table style="width:100%; vertical-align: top;">
                                                        <tr>
                                                            <td align="left" style="vertical-align: top" class="style8">
                                                                Area: &nbsp; &nbsp;<%--  <div  style="width:230px;height:60pt;overflow:scroll;overflow-x:hidden;border:1px solid #CCCCCC;"> --%><%--<asp:CheckBoxList ID="chkMuestra" runat="server">
                                                                </asp:CheckBoxList>--%>&nbsp;&nbsp;</td>
                                                            <td align="left"   style="vertical-align: top" class="style9">
                                                                <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlArea_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left"   style="vertical-align: top" class="style9">
                                                                <anthem:LinkButton CssClass="myLabelIzquierda"  ID="btnSeleccionarTipoMuestra" runat="server"  Visible="false"
                                                                     >Tipos de muestras:</anthem:LinkButton>
                                                            </td>
                                                            <td align="left" rowspan="2" style="vertical-align: top">
                                                                <anthem:ListBox ID="lstMuestra" runat="server" Height="150px" Width="250px" 
                                                                    SelectionMode="Multiple" Visible="False">
                                                                </anthem:ListBox>
                                                            </td>
                                                            <td style="vertical-align: top" align="right" class="style10">
                                                                <asp:Button ID="btnBuscarExportar" runat="server" CssClass="myButton" 
                                                                    onclick="btnBuscarExportar_Click" TabIndex="15" Text="Buscar" 
                                                                    ValidationGroup="0" Width="77px" />
                                                            </td>
                                                        </tr>
                                                        
                                                        
                                                        <tr>
                                                            <td align="left" class="style5" style="vertical-align: top">
                                                                Practica:</td>
                                                            <td align="left"   style="vertical-align: top">
                                                                <asp:DropDownList ID="ddlItem" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left"   style="vertical-align: top">
                                                            <%--    <asp:DropDownList ID="ddlMuestra" runat="server" Visible="false">
                                                                </asp:DropDownList>--%>
                                                            </td>
                                                            <td align="right" class="style4" style="vertical-align: top">
                                                                &nbsp;</td>
                                                        </tr>
                                                        
                                                        
                                                        <tr>
                                                            <td align="left" style="vertical-align: top" colspan="5">
                                                                <asp:Panel ID="pnlImpresion" runat="server" Visible="false">
                                                                <hr />
                                                                  <table style="width:100%; vertical-align: top;">
                                                                        <tr>
                                                                            <td align="left" class="myLabelIzquierda" style="vertical-align: top">
                                                                                <asp:RadioButtonList ID="rdbTipoListaProtocolo" runat="server" 
                                                                                    CssClass="myLabel" RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Selected="True" Value="0">Formato Reducido (Nombre)</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Formato Reducido (Codigo)</asp:ListItem>
                                                                                    <%-- <asp:ListItem Value="1">Formato Extendido</asp:ListItem>--%>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                            <td align="right">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                           <td align="left" style="vertical-align: top" colspan="2">
                                                             <div align="left" style="border:1px solid #C0C0C0; width:100%; background-color: #EFEFEF;" >
 <table width="100%" align="center">
 	<tr>
					<td class="myLabelIzquierda" align="left" style="width: 140px; background-color: #EFEFEF;">
                                        Impresora del sistema: </td>
						<td align="left">
                                        <asp:DropDownList ID="ddlImpresora" runat="server" CssClass="myList" >
                                        </asp:DropDownList>
                            </td>
						
                                        <td align="right">
                                                  <img alt="" src="../App_Themes/default/images/excelPeq.gif"/>
                                                                <asp:LinkButton ID="lnkExcel" runat="server" CssClass="myLittleLink" 
                                                                    onclick="lnkExcel_Click" ValidationGroup="0">Exportar a Excel</asp:LinkButton>
                                            &nbsp;
                                                  <img alt="" src="../App_Themes/default/images/pdf.jpg" />
                                                                <asp:LinkButton ID="lnkPDF" runat="server" CssClass="myLittleLink" 
                                                                    onclick="lnkPDF_Click" ValidationGroup="0">Exportar a Pdf</asp:LinkButton>
                                                                <img alt="" src="../App_Themes/default/images/imprimir.jpg" />
                                                                &nbsp;
                                                                <asp:LinkButton ID="lnkImprimir" runat="server" CssClass="myLittleLink" 
                                                                    onclick="lnkImprimir_Click" TabIndex="11" 
                                                      ValidationGroup="0">Imprimir</asp:LinkButton></td>
						
                                        </tr>
                                        </table>
                                        </div>
                                        </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        
                                                        
                                                        <tr>
                                                            <td align="left" colspan="5">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </table>

                                                
                                            </asp:Panel>
                        </td>
						
					</tr>
					<tr>
						<td   colspan="5">
                                            <asp:Panel ID="pnlLista" runat="server">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td class="myLabelLitlle">
                                                                        Referencias:
                                                                    </td>
                                                                    <td class="myLabelLitlle" width="80px">
                                                                        <img src="../App_Themes/default/images/rojo.gif" /> No Procesado</td>
                                                                    <td class="myLabelLitlle" width="80px">
                                                                        <img src="../App_Themes/default/images/amarillo.gif" /> En Proceso</td>
                                                                    <td class="myLabelLitlle" width="70px">
                                                                        <img src="../App_Themes/default/images/verde.gif" /> Terminado</td>
                                                                    <td class="myLabelLitlle" width="60px">
                                                                        <img src="../App_Themes/default/images/impreso.jpg" /> Impreso</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="btnBuscar" runat="server" CssClass="myButton" 
                                                                onclick="btnBuscar_Click" TabIndex="15" Text="Buscar" ValidationGroup="0" 
                                                                Width="77px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;<asp:Label ID="CantidadRegistros" runat="server"  
                                                              forecolor="Blue" />
                                                            &nbsp;
                                                            <asp:Label ID="CurrentPageLabel" runat="server" forecolor="Blue" />
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                 
                                                            <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                                                                AutoGenerateColumns="False" BorderColor="#3A93D2" BorderStyle="Solid" 
                                                                BorderWidth="1px" CellPadding="2" DataKeyNames="idProtocolo" 
                                                                EmptyDataText="No se encontraron protocolos para los parametros de busqueda ingresados" 
                                                                Font-Size="9pt" ForeColor="#666666" GridLines="Horizontal" 
                                                                onpageindexchanging="gvLista_PageIndexChanging" 
                                                                onrowcommand="gvLista_RowCommand" onrowdatabound="gvLista_RowDataBound" 
                                                                PageSize="20" Width="100%">
                                                                <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" 
                                                                    ForeColor="#333333" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="estado" />
                                                                    <asp:BoundField DataField="impreso" />
                                                                    <asp:BoundField DataField="numero" HeaderText="Nro.">
                                                                        <ItemStyle Width="5%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha">
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="fechaRetiro" HeaderText="Entrega" />
                                                                    <asp:BoundField DataField="dni" HeaderText="DNI">
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="paciente" HeaderText="Apellidos y Nombres">
                                                                        <ItemStyle Width="30%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="edad" HeaderText="Edad">
                                                                        <ItemStyle Width="5%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sexo" HeaderText="Sexo">
                                                                        <ItemStyle Width="5%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="origen" HeaderText="Origen">
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="prioridad" HeaderText="Prioridad">
                                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="username" HeaderText="Usuario">
                                                                        <ItemStyle Width="5%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="fechaRegistro" HeaderText="Fecha Act.">
                                                                        <ItemStyle Font-Size="7pt" Width="10%" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Editar" runat="server" 
                                                                                ImageUrl="~/App_Themes/default/images/editar.jpg" ommandName="Editar" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Height="20px" HorizontalAlign="Center" Width="40px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton Visible="false" ID="Imprimir" runat="server" CommandName="Imprimir" 
                                                                                ImageUrl="~/App_Themes/default/images/imprimir.jpg" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Height="20px" HorizontalAlign="Center" Width="40px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Pdf" runat="server" CommandName="Pdf" 
                                                                                ImageUrl="~/App_Themes/default/images/pdf.jpg" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Height="20px" HorizontalAlign="Center" Width="40px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Eliminar" runat="server" CommandName="Eliminar" 
                                                                                ImageUrl="~/App_Themes/default/images/eliminar.jpg" 
                                                                                OnClientClick="return PreguntoEliminar();" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Height="20px" HorizontalAlign="Center" Width="40px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerSettings Mode="NumericFirstLast" Position="Top" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#E6E6E6" ForeColor="Black" HorizontalAlign="Right" />
                                                                <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="#3A93D2" Font-Bold="False" Font-Names="Arial" 
                                                                    Font-Size="8pt" ForeColor="White" />
                                                                <EditRowStyle BackColor="#999999" />
                                                            </asp:GridView>
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                        </td>
						
					</tr>
					<tr>
						<td  align="left" colspan="3">
                            &nbsp;</td>
						
						<td   align="right" colspan="2">
                                            &nbsp;</td>
						
					</tr>
					</table>
						
<br />		
</div>
<script src="../script/Resources/jquery.min.js" type="text/javascript"></script>
 <link href="../script/Resources/jquery-ui-1.8.20.css" rel="stylesheet" type="text/css" />   
    <script src="../script/Resources/jQuery-ui-1.8.18.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

function VerificaLargo (source, arguments)
    {                
    var Observacion = arguments.Value.toString().length;
 //   alert(Observacion);
    if (Observacion>4000 )
        arguments.IsValid=false;    
    else   
        arguments.IsValid=true;    //Si llego hasta aqui entonces la validación fue exitosa        
}


    
    function PreguntoEliminar()
    {
    if (confirm('¿Está seguro de anular el protocolo?'))
    return true;
    else
    return false;
}



function muestraSelect() {
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

    $('<iframe src="../Muestras/MuestraSelect.aspx" />').dialog({
        title: 'Tipo de Muestras',
        autoOpen: true,
        width: 790,
        height: 420,
        modal: true,
        resizable: false,
        autoResize: true,
        overlay: {
            opacity: 0.5,
            background: "black"
        }
    }).width(800);
}
    </script>

   <%-- </form>--%>
   
 
    </table>
   
 
 </asp:Content>
 