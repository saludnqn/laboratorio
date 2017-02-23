<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebLab.Turnos.Default" MasterPageFile="~/Site1.Master" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="head">

    <title>LABORATORIO</title> 
<script type="text/javascript" src="../script/Mascara.js"></script>
  
      <script type="text/javascript" src="../script/ValidaFecha.js"></script> 
  
 <script src="../Protocolos/Resources/jquery.min.js" type="text/javascript"></script>
    <link href="../Protocolos/Resources/jquery-ui-1.8.20.css" rel="stylesheet" type="text/css" />
    <script src="../Protocolos/Resources/jQuery-ui-1.8.18.min.js" type="text/javascript"></script>

    <script type="text/javascript">    
	        $(function() {
		        $("#<%=txtFechaNac.ClientID %>").datepicker({
			        showOn: 'button',
			        buttonImage: '../App_Themes/default/images/calend1.jpg',
			        buttonImageOnly: true
		        });
	        });

    function seleccionarPaciente() {
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
        $('<iframe src="http://intranet.hospitalneuquen.org.ar/scripts/historias/historias.dll/alta?inside=1" style="-moz-box-sizing: border-box; -webkit-box-sizing: border-box; box-sizing: border-box" />').dialog({
            title: 'Nuevo Paciente',
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

    
    function closeDialog(success, historia) {
        var hfPaciente = $("#<%= hfPaciente.ClientID %>").val(historia.codigo);
        <%= GetPostBackEventReference(btnSeleccionarPaciente) %>
    }
 
</script>
</asp:Content>


<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">     
     <br /> &nbsp;
    <div align="left"  style="width:850px; ">
    
<table  width="600px" align="center" class="myTabla">
					<tr>
						<td colspan="3" ><B class="mytituloTabla">NUEVO TURNO</B><hr /></td>
					</tr>
					<tr>
						<td   colspan="3" style="color: #333333; font-weight: bold; font-size: 12px; vertical-align: top;">Búsqueda e Identificación del Paciente</td>
						
					</tr>
					<tr>
						<td   colspan="3">
                                            <hr /></td>
						
					</tr>
					<tr>
							<td class="myLabelIzquierda"  >
                                            DNI/LE/LC:</td>
						<td align="left" colspan="2">
                           <input id="txtDni" type="text" runat="server"  class="myTexto"  
                                onblur="valNumero(this)" maxlength="8" tabindex="0"/>
                            <asp:CompareValidator ID="cvDni" runat="server" ControlToValidate="txtDni" 
                                ErrorMessage="Debe ingresar solo numeros" Operator="DataTypeCheck" 
                                Type="Integer" ValidationGroup="0"></asp:CompareValidator>
                        </td>
						
					</tr>
					<tr>
							<td class="myLabelIzquierda"  >
                                            Apellido/s:</td>
						<td align="left" colspan="2">
                                            <asp:TextBox ID="txtApellido" runat="server" CssClass="myTexto" TabIndex="1" 
                                                ValidationGroup="1" Width="150px"></asp:TextBox>
                        </td>
						
					</tr>
					<tr>
							<td class="myLabelIzquierda"  >
                                            Nombres/s:</td>
						<td align="left" colspan="2">
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="myTexto" TabIndex="2" 
                                                Width="216px"></asp:TextBox>
                        </td>
						
					</tr>

                    
					<tr>
						<td class="myLabelIzquierda"  >
                                            Fecha de Nac.:</td>
						<td align="left" colspan="2">
                                            <input id="txtFechaNac" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px"  /></td>
						
					</tr>
				
					
					<tr>
						<td class="myLabelIzquierda"  >
                                            Sexo:</td>
						<td align="left" colspan="2">
                                            <asp:DropDownList ID="ddlSexo" runat="server" TabIndex="4">
                                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                                                <asp:ListItem Value="2">Femenino</asp:ListItem>
                                                <asp:ListItem Value="3">Masculino</asp:ListItem>
                                                <asp:ListItem Value="1">Indeterminado</asp:ListItem>
                                            </asp:DropDownList>
                        </td>
						
					</tr>
					<tr>
						<td   colspan="3">
                                            <hr /></td>
						
					</tr>
					<tr>
						<td  >
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" ValidationGroup="0" 
                                               CssClass="myButton" TabIndex="5" onclick="btnBuscar_Click" />
                        </td>
						<td align="left">
                                            <asp:CustomValidator ID="cvDatosEntrada" runat="server" 
                                                ErrorMessage="Debe ingresar al menos un parametro de busqueda" 
                                                onservervalidate="cvDatosEntrada_ServerValidate" ValidationGroup="0"></asp:CustomValidator>
                        </td>
						
						<td align="right">
                                           <asp:HyperLink ID="HyperLink1" runat="server" 
                                                
                                                
                                               NavigateUrl="//10.1.200.131/principal/Paciente/PacienteEdit.aspx?id=0&llamada=LaboTurno" 
                                               Font-Bold="True" Font-Size="9pt" Font-Underline="True" ForeColor="#006666"  Visible="false"
                                               ToolTip="Crear nuevo paciente en el SGH">Nuevo Paciente</asp:HyperLink>
                                               
                                                 <asp:Button ToolTip="Crear un nuevo paciente " CssClass="myButton" 
                                               ID="btnSeleccionarPaciente" Width="150px" runat="server" Text="Nuevo Paciente" 
                                               OnClientClick="seleccionarPaciente(); return false;" 
                                               onclick="btnSeleccionarPaciente_Click" TabIndex="100" 
                                               UseSubmitBehavior="False" />
        <br />
        <asp:HiddenField ID="hfPaciente" runat="server" />
                                               </td>
						
					</tr>
					<tr>
						<td   colspan="3">
                                            &nbsp;</td>
						
					</tr>
					<tr>
						<td   colspan="3" style="vertical-align: top">
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" DataKeyNames="idPaciente" Font-Size="9pt" 
                                                Width="100%" CellPadding="3" 
                                ForeColor="#333333" PageSize="15" 
                                
                                
                                EmptyDataText="No se encontraron pacientes para los parametros de busqueda ingresados" 
                                onrowcommand="gvLista_RowCommand" onrowdatabound="gvLista_RowDataBound" 
                                TabIndex="5" BorderColor="#3A93D2" BorderStyle="Solid" BorderWidth="3px" 
                                GridLines="None" onpageindexchanging="gvLista_PageIndexChanging" 
                                Font-Bold="True">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Arial" 
                Font-Size="8pt" />
            <Columns>
                <asp:TemplateField>
                            <ItemTemplate>
                            <asp:ImageButton ID="Editar" runat="server" ImageUrl="~/App_Themes/default/images/editar.jpg" 
                            ommandName="Editar" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="18px" HorizontalAlign="Center" Height="18px" />
                          
                        </asp:TemplateField>
                <asp:BoundField DataField="dni" HeaderText="DNI" >
                    <ItemStyle Width="15%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="paciente" HeaderText="Apellidos y Nombres">
                    <ItemStyle Width="65%" />
                </asp:BoundField>
                 <asp:BoundField DataField="sexo" HeaderText="Sexo">
                     <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="fechaNacimiento" HeaderText="Fecha Nacimiento">
                     <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:BoundField>
                 <asp:TemplateField>
                            <ItemTemplate>
                            <asp:ImageButton ID="Turno" runat="server" ImageUrl="~/App_Themes/default/images/flecha.jpg" 
                            ommandName="Turno" />
                            </ItemTemplate>
                          
                               <ItemStyle Width="18px" HorizontalAlign="Center" Height="18px" />
                          
                        </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#3A93D2" Font-Bold="True" ForeColor="White" 
                Font-Names="Arial" Font-Size="8pt" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
                        </td>
						
					</tr>
					</table>
                    </div>
     </asp:Content>