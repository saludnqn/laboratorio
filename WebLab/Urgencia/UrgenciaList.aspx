<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UrgenciaList.aspx.cs" Inherits="WebLab.Urgencia.UrgenciaList" MasterPageFile="~/Site1.Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajx" %>
<%@ Register Src="~/PeticionList.ascx" TagPrefix="uc1" TagName="PeticionList" %>


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
  
 

   
    </asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          
<br />   &nbsp;
    
  <ajx:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
    EnableScriptLocalization="true">
  </ajx:toolkitscriptmanager>
	 <table  width="1600px" align="left" cellpadding="1" cellspacing="1" class="myTabla" >
 <tr>
     <td>
				 <table  width="1000px" align="left" cellpadding="1" cellspacing="1" class="myTabla" >
					<tr>
						<td colspan="7" class="mytituloRojo2" style="vertical-align: bottom"><img src="../App_Themes/default/images/urgencia.jpg" />&nbsp;&nbsp;CONTROL DE URGENCIAS    <hr class="hrTitulo" /></td>
						<td class="mytituloRojo2" style="vertical-align: bottom">&nbsp;</td>
						<td class="mytituloRojo2" style="vertical-align: bottom">&nbsp;</td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Fecha Desde:&nbsp;
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="0" class="myTexto" 
                                style="width: 70px"  /></td>
						<td class="myLabelIzquierda">
                            Fecha Hasta:&nbsp;
                    <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                        style="width: 70px; vertical-align: middle;"  onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="1" class="myTexto"  /></td>
						<td class="myLabelIzquierda">
                                            Estado:</td>
						<td>
                                            <asp:DropDownList ID="ddlEstado" runat="server" 
                                CssClass="myList" TabIndex="2" AutoPostBack="True" 
                                                onselectedindexchanged="ddlEstado_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="-1">Todos</asp:ListItem>
                                                <asp:ListItem Value="0">No Procesado</asp:ListItem>
                                                <asp:ListItem Value="1">En Proceso</asp:ListItem>
                                                <asp:ListItem Value="2">Terminado</asp:ListItem>
                                            </asp:DropDownList>
                                        
                            </td>
						<td class="myLabelIzquierda">
                                            Número:</td>
						<td>
                                            <asp:TextBox ID="txtNumero" runat="server" CssClass="myTexto" MaxLength="9" 
                                                TabIndex="3" ToolTip="Numero de protocolo" Width="100px"></asp:TextBox>
                                        
                            </td>
						<td align="right">
                                                            <asp:Button ID="btnBuscar" runat="server" CssClass="myButton" 
                                                                onclick="btnBuscar_Click" TabIndex="4" Text="Actualizar" ValidationGroup="0" 
                                                                Width="100px" />
                                        
                            </td>
						<td align="right">
                                                            &nbsp;</td>
						<td align="right" rowspan="3" style="vertical-align: top">
                            	
        
                                        
                            </td>
					</tr>
						<tr>
						<td   colspan="7">
                                           <hr /></td>
						
						<td>
                                           &nbsp;</td>
						
					</tr>
					<tr>
						<td   colspan="7">
                                           
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <table class="mytable2" >
                                                                <tr >
                                                                    <td class="myLabelLitlle" width="50px" align=center>
                                                                        TOTAL: <asp:Label ID="CantidadRegistros" runat="server" Font-Bold="True"  /></td>
                                                                    <td class="myLabelLitlle" width="100px" align=center>
                                                                        <img src="../App_Themes/default/images/rojo.gif" /> No Procesado: <asp:Label ID="lblNoProcesado" runat="server" Font-Bold="True" /></td>
                                                                    <td class="myLabelLitlle" width="100px" align=center>
                                                                        <img src="../App_Themes/default/images/amarillo.gif" /> En Proceso: <asp:Label ID="lblEnProceso" runat="server" Font-Bold="True"  /></td>
                                                                    <td class="myLabelLitlle" width="100px" align=center>
                                                                        <img src="../App_Themes/default/images/verde.gif" /> Terminado: <asp:Label ID="lblTerminado" runat="server" Font-Bold="True"  /></td>
                                                                    <td class="myLabelLitlle" width="100px" align=center>
                                                                        <img src="../App_Themes/default/images/impreso.jpg" /> Impreso:  <asp:Label ID="lblImpreso" runat="server" Font-Bold="True" /></td>
                                                                </tr>
                                                                
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           <hr /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><div class="myLabel">Ultimos 25 protocolos de urgencia<asp:CustomValidator ID="cvFechas" runat="server" 
                                ErrorMessage="Fechas de inicio y de fin" 
                                onservervalidate="cvFechas_ServerValidate" ValidationGroup="0">formato inválido</asp:CustomValidator>
                                        
                                            <asp:CustomValidator ID="cvNumeroDesde" runat="server" 
                                ErrorMessage="Numero de Protocolo" 
                                onservervalidate="cvNumeros_ServerValidate" ValidationGroup="0" 
                                >Sólo numeros</asp:CustomValidator>
                                        
                                                            </div>

                                                        <div align="left" style="border: 1px solid #999999; overflow: scroll; overflow-x:hidden; height: 500px; background-color: #F7F7F7;">
                                                            <asp:GridView ID="gvLista" runat="server" CssClass="myTabla"
                                                                AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idProtocolo" 
                                                                EmptyDataText="No se encontraron protocolos urgentes para los parametros de busqueda ingresados" 
                                                                Font-Size="9pt" ForeColor="#333333" GridLines="Horizontal" 
                                                                onrowcommand="gvLista_RowCommand" onrowdatabound="gvLista_RowDataBound" 
                                                                PageSize="20" Width="100%">
                                                                <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" 
                                                                    ForeColor="#333333" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="estado" >
                                                                        <ItemStyle Width="20px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="impreso" >
                                                                        <ItemStyle Width="20px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="numero" HeaderText="Nro.">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle Width="5%" CssClass="myLabelIzquierda"/>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="myLabelIzquierda"/>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="dni" HeaderText="DNI">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="myLabelIzquierda" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="paciente" HeaderText="Apellidos y Nombres">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="30%" CssClass="myLabelIzquierda" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="edad" HeaderText="Edad">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" CssClass="myLabelIzquierda"/>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sexo" HeaderText="Sexo">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" CssClass="myLabelIzquierda"/>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="item" HeaderText="Detalle">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="30%" HorizontalAlign="Center" CssClass="myLittleLink2" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="username" HeaderText="Usuario">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="fechaRegistro" HeaderText="Fecha Act.">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Center"  />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Editar" runat="server" 
                                                                                ImageUrl="~/App_Themes/default/images/editar.jpg" ommandName="Editar" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Height="20px" HorizontalAlign="Center" Width="20px" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                             <asp:ImageButton ID="Eliminar" runat="server" CommandName="Eliminar" 
                                                                                ImageUrl="~/App_Themes/default/images/eliminar.jpg" 
                                                                                OnClientClick="return PreguntoEliminar();" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Height="20px" HorizontalAlign="Center" Width="20px" />
                                                                    </asp:TemplateField>
                                                                    
                                                                     <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Resultado" runat="server" 
                                                                                ImageUrl="~/App_Themes/default/images/diagrama.jpg" CommandName="Resultado" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Height="20px" HorizontalAlign="Center" Width="20px" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Valida" runat="server" 
                                                                                ImageUrl="~/App_Themes/default/images/valida.jpg" CommandName="Validacion" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Height="20px" HorizontalAlign="Center" Width="20px" />
                                                                    </asp:TemplateField>

                                                                    
                                                                </Columns>
                                                                <PagerSettings Position="Top" />
                                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Arial" 
                                                                    Font-Size="8pt" ForeColor="#333333" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                          
                        </td>
						
						<td>
                                           
                                                &nbsp;</td>
						
					</tr>
					</table>
		</td>
     <td style="vertical-align:top;"> <table  width="600px" align="center" cellpadding="1" cellspacing="1" class="myTabla" >
					<tr>
						<td><uc1:PeticionList runat="server" ID="PeticionList" /></td>
                        </tr></table></td>

 </tr>			
         </table>
<br />		


<script language="javascript" type="text/javascript">



    
    function PreguntoEliminar()
    {
    if (confirm('¿Está seguro de anular el protocolo?'))
    return true;
    else
    return false;
    }
    </script>


   
 
 </asp:Content>