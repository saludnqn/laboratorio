<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgendaEdit.aspx.cs" Inherits="WebLab.Agendas.AgendaEdit" MasterPageFile="../Site1.Master" StyleSheetTheme="" Theme="" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>

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
  <div align="left" style="width:1000px">         

				 <table width="600px" align="center" class="myTabla">
					<tr>
						<td><b  class="mytituloTabla">AGENDA</b></td>
						<td align="right"> <a href="../Help/Documentos/Agenda.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
					<tr>
						<td colspan="2"><hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Tipo de Servicio:<span style="font-weight:bold"><asp:RangeValidator 
                                ID="rvTipoServicio" runat="server" 
                        ControlToValidate="cboTipoServicio" ErrorMessage="Tipo de Servicio" 
                        MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">*</asp:RangeValidator>
                    </span></td>
						<td>
                    <asp:DropDownList ID="cboTipoServicio" runat="server" CssClass="myList" TabIndex="1" 
                                ToolTip="Seleccione el servicio para el cual crear� la agenda" 
                                AutoPostBack="True" 
                                onselectedindexchanged="cboTipoServicio_SelectedIndexChanged">
                    </asp:DropDownList>
                            &nbsp;&nbsp;</td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Practica:<img src="../App_Themes/default/images/new.png" /></td>
						<td>
                            <asp:DropDownList ID="ddlItem" runat="server" 
                                CssClass="myList" 
                                Width="250px">
                            </asp:DropDownList>
                        &nbsp;<b 
                                style="font-family: Arial; font-size: 10px; font-style: italic">(opcional para turnos para practicas)</b></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Fecha Desde:<asp:RequiredFieldValidator 
                                ID="rfvFechaDesde" runat="server" 
                          ControlToValidate="txtFechaDesde" ErrorMessage="Fecha desde" ValidationGroup="0">*</asp:RequiredFieldValidator>
                                            </td>
						<td >
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                        style="width: 80px"  onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="2" class="myTexto" 
                                title="Ingrese la fecha de inicio de vigencia"  /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Fecha Hasta:<asp:RequiredFieldValidator 
                                ID="rfvFechaHasta" runat="server" 
                          ControlToValidate="txtFechaHasta" ErrorMessage="Fecha hasta" ValidationGroup="0">*</asp:RequiredFieldValidator>
                                            </td>
						<td >
                    <input id="txtFechaHasta" runat="server" type="text" maxlength="10" 
                        style="width: 80px"  onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                title="Ingrese la fecha de fin de vigencia"  /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Limite de Turnos por dia:<asp:RequiredFieldValidator 
                                ID="rfvLimite" runat="server" 
                          ControlToValidate="txtLimite" ErrorMessage="Limite de turnos" 
                                ValidationGroup="0">*</asp:RequiredFieldValidator>
                                            </td>
						<td style="font-family: Arial; font-size: 10px; font-style: italic">
                      <input id="txtLimite" runat="server" type="text" maxlength="3" 
                          style="width: 40px"  onblur="valNumero(this)" tabindex="4" class="myTexto" 
                                title="Ingrese el limite de turnos" />&nbsp; (colocar 0 para especificar sin limites de turnos)</td>
						</tr>
					<tr>
						<td class="myLabelIzquierda" >Dias de atencion:<asp:CustomValidator ID="cvDias" runat="server" 
                                ErrorMessage="Dias de atenci�n" onservervalidate="ValidaCheckBox" 
                                ValidationGroup="0">*</asp:CustomValidator>
                        </td>
						<td>
                            <anthem:RadioButtonList ID="rdbTipoDias" runat="server" AutoCallBack="True" 
                                onselectedindexchanged="rdbTipoDias_SelectedIndexChanged" 
                                RepeatDirection="Horizontal" TabIndex="5" 
                                ToolTip="Seleccione los dias de atenci�n">
                                <Items>
                                    <asp:ListItem Value="0">Todos los dias</asp:ListItem>
                                    <asp:ListItem Value="1">Dias habiles</asp:ListItem>
                                </Items>
                            </anthem:RadioButtonList>
                                        
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>
                            <anthem:CheckBoxList ID="cklDias" runat="server" RepeatColumns="5" 
                                RepeatDirection="Horizontal" TabIndex="6">
                                <Items>
                                    <asp:ListItem Value="1">Lunes</asp:ListItem>
                                    <asp:ListItem Value="2">Martes</asp:ListItem>
                                    <asp:ListItem Value="3">Miercoles</asp:ListItem>
                                    <asp:ListItem Value="4">Jueves</asp:ListItem>
                                    <asp:ListItem Value="5">Viernes</asp:ListItem>
                                    <asp:ListItem Value="6">Sabado</asp:ListItem>
                                    <asp:ListItem Value="0">Domingo</asp:ListItem>
                                </Items>
                            </anthem:CheckBoxList>
                                        
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Hora Desde:<span style="font-weight:bold"><asp:RequiredFieldValidator 
                         ID="rfvHoraDesde" runat="server" ControlToValidate="txtHoraDesde" 
                         ErrorMessage="Hora desde" ValidationGroup="0">*</asp:RequiredFieldValidator>
                     </span>
                                        
					    </td>
						<td style="font-family: Arial; font-size: 10px; font-style: italic">
                     <input id="txtHoraDesde" runat="server" type="text" maxlength="5" 
                        style="width: 50px"   onblur="valHora(this)"              
                        onkeyup="mascara(this,':',patron,true)" tabindex="7" class="myTexto" 
                                title="Ingrese la hora de inicio de turnos"  />&nbsp;  (ingrese en formato 00:00)</tr>
					<tr>
						<td class="myLabelIzquierda" >Hora Hasta:<asp:RequiredFieldValidator 
                         ID="rfvHoraHasta" runat="server" ControlToValidate="txtHoraHasta" 
                         ErrorMessage="Hora hasta" ValidationGroup="0">*</asp:RequiredFieldValidator>
                                        
					    </td>
						<td>
                     <input id="txtHoraHasta" runat="server" type="text" maxlength="5" 
                        style="width: 50px"   onblur="valHora(this)"              
                        onkeyup="mascara(this,':',patron,true)" tabindex="8" class="myTexto" 
                                title="Ingrese la hora de fin de turnos"  /></tr>
					<tr>
						<td class="myLabelIzquierda" >Horario de Turnos:</td>
						<td>
                            <anthem:RadioButtonList ID="rdbHorarioTurno" runat="server" AutoCallBack="True" 
                                onselectedindexchanged="rdbHorarioTurno_SelectedIndexChanged" TabIndex="9" 
                                ToolTip="Seleccione el horario de turnos">
                              
                                <Items>
<asp:ListItem Selected="True" Value="0">Dar turnos a horario de inicio</asp:ListItem>
<asp:ListItem Value="1">Dar turnos segun frecuencia</asp:ListItem>
</Items>
                            </anthem:RadioButtonList>
                        </tr>
					<tr>
						<td class="myLabelIzquierda" >Frecuencia (min):<anthem:RequiredFieldValidator ID="rfvFrecuencia" 
                                runat="server" ControlToValidate="txtFrecuenciaTurno" Enabled="False" 
                                ErrorMessage="Frecuencia" ValidationGroup="0">*</anthem:RequiredFieldValidator>
                        </td>
						<td>
                      <input id="txtFrecuenciaTurno" runat="server" type="text" maxlength="3" 
                          style="width: 40px"  onblur="valNumero(this)" tabindex="10" class="myTexto" 
                                title="Ingrese la frecuencia de turnos" /></tr>
					<tr>
						<td colspan="2">
                                            <hr /></td>
						
					</tr>
					<tr>
						<td>
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="AgendaList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
						<td align="right">
                            <asp:Button ID="btnGuardar" runat="server" onclick="btnGuardar_Click" 
            Text="Guardar" TabIndex="11" CssClass="myButton" ValidationGroup="0" 
                                ToolTip="Haga clic aqu� para guardar los datos" />
                        </td>
						
					</tr>
					<tr>
						<td colspan="2">
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                ControlToCompare="txtFechaDesde" ControlToValidate="txtFechaHasta" 
                                                ErrorMessage="La fecha hasta tiene que se mayor a la fecha desde" 
                                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="0"></asp:CompareValidator>
                        </td>
						
					</tr>
					</table>
			
	
                 <asp:ValidationSummary ID="vs" runat="server" 
                     HeaderText="Debe completar los datos marcados como requeridos:" 
                     ShowMessageBox="True" ShowSummary="False" ValidationGroup="0" />
</div>
</asp:Content>

   	     
        
        
	

