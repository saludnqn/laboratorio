<%@ Page  Language="C#"  AutoEventWireup="true" CodeBehind="ResultadoEdit2.aspx.cs" Inherits="WebLab.Resultados.ResultadoEdit2" EnableEventValidation="true"  MasterPageFile="~/Site1.Master"%>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<%@ Register src="../Calidad/IncidenciaEdit.ascx" tagname="IncidenciaEdit" tagprefix="uc1" %>


<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript" src="../script/Mascara.js"></script>
    <script type="text/javascript" src="../script/ValidaFecha.js"></script>      
    <%--<script src="../script/jquery.min.js" type="text/javascript"></script>--%>
 <link href="../script/Resources/jquery-ui-1.8.20.css" rel="stylesheet" type="text/css" />   
 
                  <script src="jquery.min.js" type="text/javascript"></script>  
                  <script src="jquery-ui.min.js" type="text/javascript"></script> 

                   <script type="text/javascript">                     

             $(function() {
                 $("#tabContainer").tabs();
                        var currTab = $("#<%= HFCurrTabIndex.ClientID %>").val();
                        $("#tabContainer").tabs({ selected: currTab });
             });

                    function Enter(field, event) {
                        var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
                        if (keyCode == 13) {
                            var i;
                            for (i = 0; i < field.form.elements.length; i++)
                                if (field == field.form.elements[i])
                                    break;
                            i = (i + 1) % field.form.elements.length;
                            field.form.elements[i].focus();
                            return false;
                        }
                        else
                            return true;

                    }
             
             function enterToTab(pEvent) {////ie
                if (window.event.keyCode == 13  )
                {                           
                    window.event.keyCode = 9;
                }
            }            
    </script>   
    <style type="text/css">
        .myButtonGris {}
        .auto-style1 {
            width: 232px;
        }
        </style>
    </asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">               
    <br />  &nbsp;    
    
<div align="left" style="width: 1000px">
   
                 <table  width="1000px"                    
                     
                     style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal; color: #000000" 
                     cellpadding="1" cellspacing="1" align="center">
					
					
					<tr>
						<td   
                            
                            
                            style="font-family: Arial; font-size: 14px; font-weight: bold; vertical-align: top;" 
                            colspan="3">
                            <asp:Label ID="lblTitulo" runat="server" Text="CARGA DE RESULTADOS" 
                                ForeColor="#2B7EBD"></asp:Label>
                                <hr /></td>
					</tr>						
						
						<tr>
						 <td   align="center" style="vertical-align: top">
                            <table style="width:180px;">
                                <tr>
                                   <td align="left">
    <asp:HyperLink ID="hypRegresar" AccessKey="R" ToolTip="Alt+Shift+R" runat="server" CssClass="myLink">Regresar</asp:HyperLink>
    <br />  &nbsp;    
    <br />  &nbsp;
      <asp:Label CssClass="myLabelGris" ID="lblCantidadRegistros" runat="server" Text="Label"></asp:Label>
                                      </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                      
                                        <div  style="width:180px;height:350pt;overflow:scroll;overflow-x:hidden;border:1px solid #CCCCCC;"> 
                                            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
                                            DataKeyNames="idProtocolo" onrowcommand="gvLista_RowCommand" 
                                            onrowdatabound="gvLista_RowDataBound" CellPadding="3" 
                                            HorizontalAlign="Left" Font-Size="8pt" BorderColor="#3A93D2" BorderStyle="Solid" 
                                            BorderWidth="1px" GridLines="None" Font-Bold="False" ForeColor="#333333" Width="160px" >
                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Arial" 
                                            Font-Size="8pt" />

                                            <Columns>
                                            <asp:BoundField DataField="numero" HeaderText="Protocolo" />
                                            <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                            <asp:TemplateField>
                                            <ItemTemplate>
                                            <asp:ImageButton ID="Pdf" runat="server" ImageUrl="~/App_Themes/default/images/flecha.jpg" 
                                            CommandName="Pdf" />
                                            </ItemTemplate>

                                            <ItemStyle Width="20px" HorizontalAlign="Center" Height="20px" />

                                            </asp:TemplateField>

                                            <asp:BoundField DataField="estado" Visible="False" />

                                            </Columns>
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#3A93D2" Font-Bold="True" ForeColor="White" 
                                            Font-Names="Arial" Font-Size="8pt" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#333333" />
                                            </asp:GridView>
                                     </div>


                                        </td>
                                </tr>
                            </table>
                           
                        </td>				
					
					
						
						
						<td   align="left" style="vertical-align: top">
                           &nbsp;</td>
                            
						<td style="vertical-align: top"> 
                           
                            <table style="width:820px;">
                              <tr>
                              <td align="left">
                              	<asp:ImageButton ImageUrl="~/App_Themes/default/images/actualizar.gif"  ID="btnActualizar"  runat="server"  ToolTip="Ctrl+F4"
                                   onclick="btnActualizarPracticas_Click"
                        ></asp:ImageButton> 
                               
                      </td>
                              <td colspan="2" align="right">
                               
                                <asp:LinkButton AccessKey="<" ID="lnkAnterior" runat="server" CssClass="myLittleLink" 
                                onclick="lnkAnterior_Click">&lt;Anterior</asp:LinkButton> &nbsp;|&nbsp; <asp:LinkButton 
                                ID="lnkPosterior" runat="server" CssClass="myLittleLink" 
                                onclick="lnkPosterior_Click">Posterior&gt;</asp:LinkButton>
                              </td>
                              </tr>
							<tr>
						
						<td align="left"   colspan="3">  
						
						<table class="mytable1" >
      						
					
					
					<tr>
							<td class="myLabelIzquierdaFondo" width="150px">
                                DU:</td>
						<td class="auto-style1">
                            <asp:Label ID="lblDni" runat="server" Text="Label" Font-Bold="True" 
                                Font-Size="9pt"></asp:Label>
                        </td>
						<td class="myLabelIzquierdaFondo" width="100px">
                            Protocolo:</td>
						<td 
                            align="left"   width="150px"  >
                            <asp:Image ID="imgEstado" runat="server" 
                                ImageUrl="~/App_Themes/default/images/amarillo.gif" Width="12px" Height="12px" />
                              
                            <asp:Label ID="lblProtocolo" runat="server" Text="Label" Font-Bold="True" 
                                Font-Size="10pt" ></asp:Label>
                            </td>
                      	<td class="myLabelIzquierdaFondo">
                           Fecha</td>
                        <td     align="left"  width="130px" >   
                                     <asp:Label Font-Bold="True" 
                                Font-Size="9pt" ID="lblFecha" runat="server" Text="Label"></asp:Label>    </td>
					</tr>
						
					
					
					<tr>
							<td class="myLabelIzquierdaFondo">
                                Paciente:</td>
						<td class="auto-style1"  >
                            <asp:Label ID="lblPaciente" runat="server" Text="Label" Font-Bold="True" 
                                Font-Size="9pt"></asp:Label>
                            <asp:Label ID="lblCodigoPaciente" runat="server" Text="Label" Font-Bold="True" 
                                Font-Size="9pt" Visible="False"></asp:Label>
                        </td>
						<td class="myLabelIzquierdaFondo">
                            Nro. de Origen:</td>
						<td 
                            align="left"   >
                            <asp:Label ID="lblNumeroOrigen" runat="server" Text="Label"  CssClass="myLabel"></asp:Label>
                            </td>
                      	<td class="myLabelIzquierdaFondo">
                            F. Toma Muestra:</td>
                        <td   align="left">   
                                     <asp:Label Font-Bold="True" 
                                Font-Size="9pt" ID="lblFechaTomaMuestra" runat="server" Text="Label"></asp:Label>    </td>
					</tr>
						
					
					
					<tr>
							<td class="myLabelIzquierdaFondo">
                            Sexo:</td>
						<td class="auto-style1"  >
                            <asp:Label ID="lblSexo" runat="server" Text="Label" CssClass="myLabel" ></asp:Label>
                        </td>
						<td class="myLabelIzquierdaFondo">
                            Origen:
                            </td>
						<td 
                            align="left"   >
                            <asp:Label ID="lblOrigen" runat="server" Text="Label"  CssClass="myLabel"></asp:Label>
                        </td>
                      	<td class="myLabelIzquierdaFondo">
                            Servicio:</td>
                        <td   align="left">   
                                   <asp:Label ID="lblServicio" runat="server" CssClass="myLabel" 
                                       Text="Label"></asp:Label>
                                       
                                    </td>
					</tr>
						
					
					
					<tr>
							<td class="myLabelIzquierdaFondo">
                                Fecha Nac. - Edad:</td>
						<td class="auto-style1"  >
                            <asp:Label ID="lblFechaNacimiento" runat="server" Text="Label"  CssClass="myLabel"></asp:Label>
                        &nbsp;<asp:Label ID="lblEdad" runat="server" Text="Label"  CssClass="myLabel"></asp:Label>   
                        </td>
						<td class="myLabelIzquierdaFondo" >
                            Prioridad:
                            </td>
						<td   
                            align="left" >
                            <asp:Label ID="lblPrioridad" runat="server" Text="Label"  CssClass="myLabel"></asp:Label>
                        </td>
                    	<td class="myLabelIzquierdaFondo"> Usuario</td>
                        <td    align="left"> <asp:Label  ID="lblUsuario" runat="server" Text="Label"></asp:Label>                     
                       
                        </td>
					</tr>
						
					
					
					<tr>
							<td class="myLabelIzquierdaFondo">
                                Solicitante:</td>
						<td class="auto-style1">
                           
                            <asp:Label  ID="lblMedico" runat="server" CssClass="myLabel" Text="Label"></asp:Label>    
                        </td>
							<td class="myLabelIzquierdaFondo" >
                                Sector:
                        </td>
						<td   
                            align="left" >
                            <asp:Label ID="lblSector" runat="server" Text="Label"  CssClass="myLabel"></asp:Label>
                        </td>
                       	<td class="myLabelIzquierdaFondo">     
                               Fecha de Registro:</td>
                       <td   align="left">     
                                <asp:Label  ID="lblFechaRegistro" runat="server" Text="Label" CssClass="myLabel"></asp:Label>                
                                 <asp:Label  ID="lblHoraRegistro" runat="server" Text="Label" CssClass="myLabel"></asp:Label>    
                        </td>
					</tr>
						
							
						
					
					
					<tr>
					    <td class="myLabelIzquierdaFondo">Diagnósticos:<asp:ImageButton ImageUrl="~/App_Themes/default/images/add.png" Visible="false" ToolTip="Agregar o quitar Diagnósticos"  ID="imgDiagnostico"  runat="server"  OnClientClick="editDiagnostico(); return false;"  
                                   onclick="btnActualizarPracticas_Click"></asp:ImageButton> </td>                        
						<td class="myLabelIzquierda" colspan="5">
                            <asp:Label ID="lblDiagnostico" runat="server" Text="" Font-Bold="False" 
                                Font-Size="9pt" CssClass="myLabelRojo"></asp:Label>                                                                                                               
                                
                        </td>
                        
					</tr>
						
						<tr>
						<td class="myLabelIzquierdaFondo">Observaciones Internas:</td>
						<td colspan="5"  >
                            <asp:Panel ID="pnlObservaciones" runat="server">
                            <asp:Label ID="lblObservacion" runat="server" Text="Label"  CssClass="mytituloRojo"></asp:Label> 
                            </asp:Panel>
						   
                            </td>
					</tr>
					</table>
						</td>
						</tr>
				
					<tr>
						
						<td    colspan="3">  
						
						    
<asp:HiddenField runat="server" ID="HFCurrTabIndex" /> 

						    
<asp:HiddenField runat="server" ID="HFIdItem" /> 
<asp:HiddenField runat="server" ID="HFIdGermen" /> 
<asp:HiddenField runat="server" ID="HFIdMetodo" /> 
<asp:HiddenField runat="server" ID="HFIdProtocolo" /> 
<asp:HiddenField runat="server" ID="HFNumeroAislamiento" /> 
                            <asp:HiddenField runat="server" ID="HFOperacion" /> 

                           
                            <div id="tabContainer">  
                             <asp:Panel ID="pnlResultados" runat="server" > 
                             <ul>
    <li><a href="#tab1"><b>Análisis</b></a></li>  
    <li id="tituloCalidad" runat="server"><a  href="#tab6">Incidencia<img alt="tiene incidencias" runat="server" id="inci" visible="false" style="border:none;" src="~/App_Themes/default/images/red_pin.gif" /></a></li>
    <li id="tituloMicroOrganismo" runat="server"><a href="#tab5"><b>Aislamientos&nbsp;<img alt="tiene antibiogramas" runat="server" id="aisl" visible="false" style="border:none;" src="~/App_Themes/default/images/red_pin.gif" /></b></a></li>          
    <li id="tituloAntibiograma" runat="server"><a href="#tab4"><b>Antibiogramas&nbsp;<img alt="tiene antibiogramas" runat="server" id="anti" visible="false" style="border:none;" src="~/App_Themes/default/images/red_pin.gif" /></b></a></li>          
    <li id="tituloObservaciones" runat="server"><a href="#tab2" >Observaciones</a></li>
      <li id="tituloAntecedente" runat="server"><a href="#tab3" id="tabAntecedente" runat="server">Antecedentes y más...</a></li>
    
                                                
    
  
</ul>                          


  
                            </asp:Panel>
                            
                            <asp:Panel ID="pnlHC" runat="server" > 
                      <%--     <ul>
    <li><a href="#tab1">Resultados</a></li>    
  <%--    <li><a href="#tab2">Diagnósticos</a></li>      
  
</ul>                       --%>           
                            </asp:Panel>
						 

        <div onkeydown="enterToTab(event)"  id="tab1" >   
        
        <table width="850px">
        	<tr>						
						<td 
                            colspan="2"> 
                            <asp:HyperLink ID="hplPesquisa" Visible="false" CssClass="myLink2" runat="server">Ver Tarjeta</asp:HyperLink>
                            
                            <asp:Image ID="imgPesquisa" Visible="false" runat="server" ImageUrl="~/App_Themes/default/images/pendiente.png" />                                       				
                            <asp:Label CssClass="myLabelIzquierdaGde" ID="lblMuestra" runat="server" ></asp:Label>            
                            <asp:CustomValidator ID="cvValidaControles" runat="server"  ErrorMessage="CustomValidator" Font-Size="9pt" onservervalidate="cvValidaControles_ServerValidate"   ValidationGroup="0" Font-Bold="True"></asp:CustomValidator> &nbsp; &nbsp;
                            <asp:Button ID="btnAceptarValorFueraLimite" Width="220px" CssClass="myButtonRojo"  onclick="btnAceptarValorFueraLimite_Click" runat="server" Text="Aceptar valor fuera de límite" Visible="false" />  
                            <asp:Label ID="lblIdValorFueraLimite" Visible="false" runat="server" Text="0"></asp:Label>
                            <asp:Label ID="lblIdValorFueraLimite1" Visible="false" runat="server" Text="0"></asp:Label>                           
                        </td>
						
						<td align="right" > 
                       
                            
                       <asp:Button ID="btnActualizarPracticasCarga" Visible="false" ToolTip="" runat="server" Text="Editar Practicas"  Width="150px"   
                       OnClientClick="ProtocoloEditCarga(); return false;" />
                       
                       <INPUT TYPE="button" accesskey="m"  title="Alt+Shift+M"  runat="server" NAME="marcar" id="lnkMarcar" VALUE="Marcar todos" onClick="seleccionar_todo()" style="font-size: 11px; color: #333333; text-decoration: underline; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" class="myLabelIzquierda">
                       <INPUT TYPE="button" accesskey="z" title="Alt+Shift+Z" runat="server" NAME="marcar" id="lnkDesmarcar"  VALUE="Desmarcar todos" onClick="desmarcar_todo()" style="font-size: 11px; color: #333333; text-decoration: underline; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" class="myLabelIzquierda">
						     
                                      </td>
						
					</tr>
        	<tr>
						
						<td  colspan="3" style="vertical-align: top">   
                        

						     <asp:Panel ID="Panel1" runat="server" >                                                                                                                                      						 
                                               <asp:Table ID="tContenido"   enableviewstate="true"
                                                   Runat="server" CellPadding="0" CellSpacing="0" CssClass="mytable1" 
                                                   Width="98%" ></asp:Table>                                                                                                                                     
                                           </asp:Panel></td>
						
					</tr>
        	<tr>
						
						<td class="myLabel" style="vertical-align: top" colspan="3"> 
                         </td>
						
					</tr>
        	<tr>
						
						<td class="myLabel" style="vertical-align: top" colspan="3"> 
                  
                 
                                                                    </td>
						
					</tr>
        	<tr>
						
						<td  style="vertical-align: top" colspan="3"> 
                            <%--<b class="myLabelRojo">Referencias:</b> R: Resistente&nbsp;&nbsp;I:Intermedio&nbsp;&nbsp;S:Sensible&nbsp;&nbsp;<br />SD: Sensibilidad Disminuida&nbsp;&nbsp;AS: Apto para Sinergia&nbsp;&nbsp;<br />SR: Sin Reactivo--%></td>
						
					</tr>
        	<tr>
						
						
						<td  colspan="2" align="left"> 
						
                            <asp:Label ID="lblObservacionResultado" runat="server" Font-Names="Courier New" 
                                Font-Size="9pt" ForeColor="Black" Visible="False"></asp:Label>
                            
                                                                    </td>
                                                                    </tr>
                                                                    
                                                                    
                                                                    
                                                                    <tr>
                                                                      <td colspan="3"  align="right" style="vertical-align: top">
                                                                 
                                                                    <asp:Button ID="btnAplicarFormula" runat="server" CssClass="myButtonRojo" 
                                Text="F(x) Calcular Fórmulas"  AccessKey="F" ToolTip="Alt+Shift+F"
                                onclick="btnAplicarFormula_Click" Visible="False" Width="180px" TabIndex="600" /> <br />
                            <asp:CheckBox ID="chkFormula" runat="server" Checked="True" CssClass="myLabel" 
                                Text="Calcular fórmulas al guardar" Visible="False" 
                                ToolTip="Recalcula y sobreescribe formulas al guardar" TabIndex="600" />
                            <asp:Label ID="lblFormula" runat="server" Font-Bold="True" ForeColor="Red" Text="F(x)" 
                                Visible="False"></asp:Label>
                    </td>
                        
                    
                                                                  
                                                                    
                     
                                
                              
                        
					</tr>
					
			
        		
					
        	<tr>
						
						<td class="myLabel" style="vertical-align: top" colspan="3"> 
                           <hr /></td>
						
					</tr>
        	<tr>
						
						<td colspan="2" align="left"> 
                            
                            <asp:CheckBox ID="chkCerrarSinResultados" runat="server" CssClass="myLabelIzquierda" 
                                Text="Terminar protocolo" Visible="False" 
                                ToolTip="Da por terminado el protocolo con analisis sin resultados" />
                        
						<asp:CheckBox ID="chkWhonet" runat="server" CssClass="myLabelIzquierda" 
                                Text="Informa Whonet" Visible="False" 
                                ToolTip="Agrega el protocolo en el listado para Whonet." />
                        
						<asp:RadioButtonList ID="rdbImprimir" RepeatDirection="Horizontal" runat="server" CssClass="myLabel" 
                                Font-Names="Arial" Font-Size="8pt">
                                <asp:ListItem Selected="True" Value="0">Imprimir sólo seleccionados</asp:ListItem>
                                <asp:ListItem Value="1">Imprimir todos validados</asp:ListItem>
                            </asp:RadioButtonList>
						</td>
						<td align="right" style="vertical-align: top" colspan="1"> 
                           <asp:Button ID="btnValidarImprimir" AccessKey="I" runat="server" CssClass="myButton" ToolTip="Alt+Shift+I:Validar e Imprimir en impresora seleccionada" Text="Validar + Imprimir" 
                                onclick="btnValidarImprimir_Click" ValidationGroup="0" Visible="False" 
                                Width="160px" /> 
                                                  &nbsp;&nbsp;
                          <asp:Button ID="btnGuardar" AccessKey="s" Width="100px" runat="server" CssClass="myButton" Text="Guardar" 
                                onclick="btnGuardar_Click" ValidationGroup="0" TabIndex="600" ToolTip="Alt+Shift+S: Guarda resultados" />  
                            
                            </td>
						
					</tr>
						<tr>
						
						<td class="myLabel" style="vertical-align: top" colspan="3"> 
                           <hr /></td>
						
					</tr>
        	<tr>
						
						<td align="left" > 
                            <asp:Panel ID="pnlReferencia" runat="server" Width="250px">
                              <table style="border: 1px solid #3A93D2; font-size: 10px" width="100%">
                                                
                        <tr>
                        <td><b>Referencias:</b></td>
                            <td>
                               Dentro de VR</td>
                            <td style="color: #FF0000">
                                Fuera de VR</td>
                        </tr>
                        
                        </table>
                            </asp:Panel> 
                            </td>
                            <td align="right" colspan="2" > 
                                 <asp:Button AccessKey="D" ID="btnDesValidar" Visible="false" runat="server" CssClass="myButtonGris" Text="Desvalidar"   OnClientClick="return PreguntoConfirmar();"
                                onclick="btnDesValidar_Click" ValidationGroup="0" TabIndex="600"  ToolTip="Alt+Shift+D:Desvalida lo validado por el usuario actual"/>
                          &nbsp;                                                    
                            
                            <asp:Button ID="btnValidarPendienteImprimir" AccessKey="L" runat="server" ToolTip="Alt+Shift+L:Validar pendiente de validar e Imprimir en impresora seleccionada" CssClass="myButtonGris" Text="Validar pendiente + I " 
                                onclick="btnValidarPendienteImprimir_Click" Visible="false" ValidationGroup="0" Width="170px" TabIndex="600" />  &nbsp;
                                   <asp:Button ID="btnValidarPendiente"  AccessKey="P" runat="server" CssClass="myButtonGris" Text="Validar pendiente"  ToolTip="Alt+Shift+P:Validar solo lo pendiente"
                                onclick="btnValidarPendiente_Click" Visible="false" ValidationGroup="0" Width="150px" TabIndex="600" />  
                           
                               
                            </td>
						
						
						
					</tr>
        	</table>
                                          
                                           </div>
                                               
                                             
          <div id="tab5" >
            <asp:Panel  ID="pnlMicroOrganismo" runat="server" Height="500px">    

               <fieldset style="background-color: #FFF9E1">
           <asp:DropDownList CssClass="myList" ID="ddlPracticaAislamiento"  Width="350px" runat="server"> </asp:DropDownList> 
                   <asp:RangeValidator ID="rvPracticaAislamiento" ControlToValidate="ddlPracticaAislamiento" MinimumValue="1" MaximumValue="999999" ValidationGroup="AIS" runat="server" ErrorMessage="*"></asp:RangeValidator>
               </fieldset>
          <table width="850">           
                <tr>
                <td style="vertical-align: top" height="100%" width="400px">
                
                 
                </td>
                </tr>

                 
                <tr>
                <td style="vertical-align: top">
              Microorganismo: &nbsp; &nbsp; <anthem:TextBox ID="txtCodigoMicroorganismo" runat="server" CssClass="myTexto"                               
                        ontextchanged="txtCodigoMicroorganismo_TextChanged" Width="60px" AutoCallBack="True"  TabIndex="1" 
                        ToolTip="Ingrese el codigo de microorganismo"></anthem:TextBox>

                    <anthem:DropDownList CssClass="myList" Width="400px" ID="ddlAislamiento" runat="server"></anthem:DropDownList>
                    
                       <asp:RangeValidator ID="rvAislamiento" ControlToValidate="ddlAislamiento" MinimumValue="1" MaximumValue="999999" ValidationGroup="AIS" runat="server" ErrorMessage="*" Type="Integer"></asp:RangeValidator>
                         <asp:Button CssClass="myButtonGris" ID="btnAgregarGermen" ValidationGroup="AIS"  runat="server" Text="Agregar" onclick="btnAgregarGermen_Click"/>
                                                                    </td>
                </tr>
                
               
                <tr>
                <td>
                   </td>
                </tr>
                
                <tr>
                <td align="right">
                   <asp:LinkButton ID="lnkMarcarAislamiento" runat="server" CssClass="myLittleLink" onclick="lnkMarcarAislamiento_Click" >Marcar todas</asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="lnkDesMarcarAislamiento" runat="server" CssClass="myLittleLink" onclick="lnkDesMarcarAislamiento_Click" >Desmarcar</asp:LinkButton></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvAislamientos" runat="server" AutoGenerateColumns="False" CellPadding="1" DataKeyNames="idProtocoloGermen" Font-Names="Arial" Font-Size="9pt" ForeColor="#333333" onrowcommand="gvAislamientos_RowCommand" onrowdatabound="gvAislamientos_RowDataBound1" Width="100%">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="item" HeaderText="" />
                                <asp:BoundField DataField="numeroAislamiento" HeaderText="Nro.Cepa" />
                                <asp:BoundField DataField="germen" HeaderText="Aislamiento" />
                                <asp:TemplateField HeaderText="ATB">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAtb" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "atb") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observaciones">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtObservacionesAislamiento" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "observaciones") %>' Width="400px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valida">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkValida" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="" />
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Eliminar" runat="server" CommandName="Eliminar" ImageUrl="~/App_Themes/default/images/eliminar.jpg" OnClientClick="return PreguntoEliminar();" />
                                    </ItemTemplate>
                                    <ItemStyle Height="18px" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                        </asp:GridView>
                        <hr />
                        <asp:Button ID="btnGuardarAislamientos" runat="server" CssClass="myButton" onclick="btnGuardarAislamiento_Click" Text="Guardar" />
                    </td>
                </tr>
           </table>
           </asp:Panel>
               <asp:Panel ID="pnlMicroorganismoHC" runat="server" Visible="False" >
                   <asp:GridView ID="gvAislamientosHC" runat="server" CellPadding="4" 
                        EmptyDataText="No se encontraron antibiogramas para el protocolo" 
                        ForeColor="Black" 
                        Font-Names="Arial" Font-Size="9pt" 
                       ShowFooter="false" BorderColor="#999999" BorderStyle="Solid" Font-Bold="True" 
                                UseAccessibleHeader="False" Width="100%" OnRowDataBound="gvAislamientosHC_RowDataBound" DataKeyNames="idProtocoloGermen">
                       <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                       <RowStyle BackColor="White" ForeColor="Black" Font-Bold="False" />
                       <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                       <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                       <HeaderStyle BackColor="#DADADA" Font-Bold="True" ForeColor="Black" />
                       <EditRowStyle BackColor="#999999" />
                       <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                   </asp:GridView>
              </asp:Panel>
          
          </div>    
            
       <div id="tab4" > 
                <asp:Panel ID="pnlAntibiograma" Height="600px" runat="server" >   
                  <fieldset style="background-color: #FFF9E1">
                &nbsp;&nbsp;<asp:DropDownList CssClass="myList" ID="ddlPracticaAtb" Width="250px" runat="server" AutoPostBack="true"  onselectedindexchanged="ddlPracticaAtb_SelectedIndexChanged"> </asp:DropDownList>  
                </fieldset>
               <br />
           <table width="850">           
                <tr>
                <td style="vertical-align: top" height="100%"  rowspan="2" width="400px">
                
                <fieldset>
                 
                <div>      <b class="myLabelIzquierda">NUEVO ANTIBIOGRAMA<b/>  &nbsp;<br />                             
                 &nbsp;<br />                             
             
                <asp:DropDownList CssClass="myList"  ID="ddlGermen" runat="server" Width="250px">  </asp:DropDownList>
               &nbsp;<asp:RangeValidator ID="rvGermen" runat="server" ControlToValidate="ddlGermen" 
                        ErrorMessage="*" MaximumValue="9999999" MinimumValue="1" Type="Integer" 
                        ValidationGroup="A"></asp:RangeValidator>

                  <asp:RadioButtonList CssClass="myLabelIzquierda"   onselectedindexchanged="rdbMetodologiaAntibiograma_SelectedIndexChanged"   Width="180px" ID="rdbMetodologiaAntibiograma" RepeatDirection="Horizontal"  runat="server">
                     <asp:ListItem Selected="True" Value="0">Disco</asp:ListItem>
                                <asp:ListItem Value="1">CIM</asp:ListItem>
                                <asp:ListItem Value="2">Etest</asp:ListItem>
                    </asp:RadioButtonList>
           
                   <asp:DropDownList CssClass="myList"  onselectedindexchanged="ddlPerfilAntibiotico_SelectedIndexChanged" AutoPostBack="true"   ID="ddlPerfilAntibiotico" runat="server" Width="250px">  </asp:DropDownList>
                   <hr />
<div  style="width:330px;height:250pt;overflow:scroll;overflow-x:hidden;border:1px solid #CCCCCC;"> 
                    <asp:GridView ID="gvAntiobiograma" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="idAntibiotico"                         CellPadding="1"  Width="310px"                          >
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="White" ForeColor="#333333" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" />
        <Columns>

<asp:BoundField HeaderText="Antibiótico" DataField="descripcion"  />
<asp:TemplateField HeaderText="Inter.">
    <ItemTemplate>

        <asp:DropDownList ID="ddlAntibiotico" runat="server" Font-Size="10">
        <asp:ListItem Value="" ></asp:ListItem>
         <asp:ListItem Value="Resistente">R</asp:ListItem>
         <asp:ListItem Value="Intermedio">I</asp:ListItem>
         <asp:ListItem Value="Sensible">S</asp:ListItem>
         <asp:ListItem Value="Sensibilidad Disminuida">SD</asp:ListItem>
         <asp:ListItem Value="Apto para Sinergia">AS</asp:ListItem>
         <asp:ListItem Value="Sin Reactivo">SR</asp:ListItem>
        </asp:DropDownList>
    </ItemTemplate>

</asp:TemplateField>
<asp:TemplateField HeaderText="Valor">
    <ItemTemplate >
        <asp:TextBox CssClass="myTexto" ID="txtHalo" Width="60px"  runat="server"></asp:TextBox>
    </ItemTemplate>
</asp:TemplateField>

</Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="#333333" />
                    <EditRowStyle BackColor="#999999" />
                   

</asp:GridView>
</div>


<div>
<hr />                    
<%--<b class="myLabelRojo">Referencias:</b> R: Resistente&nbsp;&nbsp;I:Intermedio&nbsp;&nbsp;S:Sensible&nbsp;&nbsp;<br />SD: Sensibilidad Disminuida&nbsp;&nbsp;AS: Apto para Sinergia&nbsp;&nbsp;<br />SR: Sin Reactivo--%>



</div>
  <hr />
<asp:Button ID="btnAgregarAntibiograma" runat="server" Text="Guardar" 
                        onclick="btnAgregarAntibiograma_Click"  CssClass="myButton"
                        ValidationGroup="A" />
                       
                    &nbsp;<asp:Button ID="btnAgregarAntibiogramaValidado" runat="server"  CssClass="myButton" onclick="btnAgregarAntibiogramaValidado_Click" Text="Guardar y Validar" ValidationGroup="A" Visible="False" Width="150px" />
                       
                    </div>
                    </b></b>
                    </fieldset>&nbsp;
                </td>
                <td style="vertical-align: top">&nbsp;</td>
                <td style="vertical-align: top" height="100%" rowspan="2">
                
                
               
                <fieldset>
                                     <b class="myLabelIzquierda">  ANTIBIOGRAMAS ASOCIADOS</b>
                                      <hr />
                                        <table>
                  <tr>
                  <td class="myLabelIzquierda">ATB:</td>
                  <td> <asp:DropDownList AutoPostBack="true" Width="100px" onselectedindexchanged="ddlMetodoAntibiograma_SelectedIndexChanged" ID="ddlMetodoAntibiograma" runat="server"> 
         <asp:ListItem Selected="True" Value="0">Disco</asp:ListItem>
         <asp:ListItem Value="1">CIM</asp:ListItem>
         <asp:ListItem Value="2">Etest</asp:ListItem>         
        </asp:DropDownList>
                        
                        <asp:DropDownList Width="250px" ID="ddlAntibiograma" runat="server" AutoPostBack="true"  onselectedindexchanged="ddlAntibiograma_SelectedIndexChanged" >
                        </asp:DropDownList>
                        <asp:RangeValidator ID="rvAntibiograma" runat="server" CssClass="myList" 
                            ControlToValidate="ddlAntibiograma" ErrorMessage="*" MaximumValue="9999999" 
                            MinimumValue="1" Type="Integer" Enabled="false" ValidationGroup="EA"></asp:RangeValidator></td>
                  </tr>

                  <tr>
                  <td class="myLabelIzquierda">&nbsp;</td>
                  <td><asp:Button ID="btnEliminarAntibiograma" ToolTip="Eliminar ATB" Enabled="false" runat="server" CssClass="myButtonGris" Width="81px"  
                          OnClientClick="return PreguntoEliminar();"      onclick="btnEliminarAntibiograma_Click" Text="Eliminar" ValidationGroup="EA" />

                    <asp:Button ID="btnEditarAntibiograma" ToolTip="Modificar ATB" Enabled="false" runat="server" Text="Modificar" CssClass="myButtonGris" Width="79px"   onclick="btnEditarAntibiograma_Click" 
                       OnClientClick="editarATB(); return false;"                       />
                       

                        <%--<asp:ImageButton ID="ImageButton1" ToolTip="Actualizar vista de antibiogramas" runat="server" ImageUrl="~/App_Themes/default/images/actualizar.gif"  onclick="btnActualizarATB_Click"/>--%>
                  <%-- <asp:Button ID="btnActualizarATB" runat="server" Visible="true"  onclick="btnActualizarATB_Click" Text="Actualizar" CssClass="myButtonGris" Width="120px"/>--%>
                      <asp:Button ID="btnValidarAntibiograma" runat="server" CssClass="myButtonGris" Visible="false" onclick="btnEditarAntibiograma_Click" OnClientClick="validarATB(); return false;" Text="Modificar/Validar" ToolTip="Validar ATB" Width="132px" />
                      <asp:Button ID="btnActualizarAntibiograma" runat="server" CssClass="myButtonGris" onclick="btnActualizarAntibiograma_Click" Text="Actualizar" ToolTip="Actualizar ATB" Width="82px" />
                  </td>
                  </tr>
                  </table>
                            
                       
                
                   
                   
                  <hr />                                                   
                  <div  style="width:500px;height:255pt;overflow:scroll;border:1px solid #CCCCCC;"> 
                    <asp:GridView ID="gvAntiBiograma2" runat="server" CellPadding="2" 
                        EmptyDataText="No se encontraron antibiogramas para el protocolo" 
                        ForeColor="#333333" CssClass="myLabelIzquierda" 
                        Font-Names="Arial" Font-Size="9pt" OnRowDataBound="gvAntiBiograma2_RowDataBound">
                        <FooterStyle  BackColor="White" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="#333333" Height="40px" />
                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
                    </div>
                    <br />
                  <hr />
                
   <asp:Button ID="btnValidarTodosAntibiogramas" runat="server" CssClass="myButton"   OnClientClick="return PreguntoValidar();"  onclick="btnValidarTodosAntibiogramas_Click" Text="Validar todo" ToolTip="Validar todos los ATB" Visible="false" Width="125px" />
                    &nbsp;<asp:Button ID="btnValidarTodosAntibiogramasPendientes" runat="server"  CssClass="myButton" OnClientClick="return PreguntoValidar();"  onclick="btnValidarTodosAntibiogramasPendientes_Click" Text="Validar sólo pendientes" ToolTip="Validar todos los ATB (sólo pendiente)" Visible="false" Width="195px" />               
                    </fieldset>
                                                              
                                                                    </td>
                </tr>
             
                
                  <tr>
                        <td></td>
                </tr>
           </table>
           </asp:Panel>               <asp:Panel ID="pnlAntibiogramaHC" runat="server" >    
                  <asp:GridView ID="gvAntiBiogramaHC" runat="server" CellPadding="4" 
                        EmptyDataText="No se encontraron antibiogramas para el protocolo" 
                        ForeColor="Black" 
                        Font-Names="Arial" Font-Size="9pt" 
                       ShowFooter="false" BorderColor="#999999" BorderStyle="Solid" Font-Bold="True" 
                                UseAccessibleHeader="False" Width="100%" OnRowDataBound="gvAntiBiogramaHC_RowDataBound">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="White" ForeColor="Black" Font-Bold="False" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#DADADA" Font-Bold="True" ForeColor="Black" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                    </asp:GridView>
                </asp:Panel>
           
       </div>
          
         
            <div  id="tab2">   
          
              <asp:Panel ID="pnlObservacionProtocolo" Height="500px" runat="server" >  
                <table style="width: 850px;">
                    <tr>
                        <td class="myLabelIzquierda">

                            Observaciones codificadas: &nbsp;
						    <anthem:DropDownList CssClass="myList" ID="ddlObsCodificadaGeneral" runat="server" Width="350px">
                               </anthem:DropDownList>
                               <anthem:Button  CssClass="myButtonGrisLittle" OnClick="btnAgregarObsCodificadaGral_Click"  ID="btnAgregar"  Width="100px" runat="server" Text="Agregar" />
                               <br />
                           <anthem:TextBox CssClass="myTexto" ID="txtObservacion" runat="server" TextMode="MultiLine" 
                              Width="95%" MaxLength="4000" Height="400px"></anthem:TextBox>  
                              

                        </td>
                    </tr>
                    <tr>
                        <td><hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <anthem:Button  CssClass="myButtonGrisLittle" OnClick="btnBorrarObsCodificadaGral_Click" ID="btnBorrarObservaciones" runat="server" Text="Borrar"  Width="100px" /> &nbsp;
                        
                            <asp:Button  CssClass="myButtonGrisLittle" OnClick="btnGuardarObsCodificadaGral_Click" ID="btnGuardarObservaciones" runat="server" Text="Guardar"  Width="100px" />  &nbsp;
                        
                          <%--  <anthem:Button  ID="btnValidarObservaciones" runat="server" Text="Guardar y Validar" />--%>  &nbsp;
                        </td>
                    </tr>
                    
                </table>
                
                </asp:Panel>
                            
                                                                    <br />         
                                               
                                               </div>
         
                                               <div  id="tab3" >

                                               <asp:Panel runat="server" Height="400px" id="pnlAntecedentes" >


                                                
                          <table width="800">
                             <tr>
                                                           <td>
                                                               &nbsp;
                                                           </td>
                                                            <td>
                                                               &nbsp;
                                                           </td>
                                                            <td>
                                                               &nbsp;
                                                           </td>
                                                       </tr>

                                                       <tr>
                                                           <td class="myLabelIzquierda">
                                                           Editar Prácticas
                                                           </td>
                                                           <td>
                                                               &nbsp;
                                                           </td>
                                                           <td class="myLabelIzquierda" align="right">
                                                            

                                                           </td>
                                                       </tr>
                                                       <tr>
                                                          <td class="myLabel">
                                                            <br />
                                                              Modifique desde acá las practicas solicitadas; tenga en cuenta que los cambios que realice quedaran en el registro de auditoria del protocolo.
                                                              <br /> &nbsp;  <br /> &nbsp;

                       
                                                               <asp:Button ID="btnActualizarPracticas" ToolTip="" runat="server" Text="Editar Practicas"  Width="150px" 
                       OnClientClick="ProtocoloEdit(); return false;" />
                       <hr />
                                                           </td>
                                                            <td>
                                                               &nbsp;
                                                           </td>
                                                            <td>
                                                               &nbsp;
                                                           </td>
                                                       </tr>
                                                       <tr>
                                                           <td class="myLabelIzquierda" colspan="3">
                                                             Antecedentes   <br /> &nbsp;  
                                                             </td>
                                                       </tr>
                                                       <tr>
                                                           <td class="myLabel">
                                                               <asp:Label ID="lblAntecedentes" Visible="false"   runat="server" Text=""></asp:Label>                                                                                                                          
                                                                  <asp:Button ID="btnAntecedente" ToolTip="Antecedentes del Pacientes" runat="server" Text="Ver Antecedentes" Width="150px" 
                       OnClientClick="AntecedenteView(); return false;" />
                         <br /> &nbsp;  <br /> &nbsp;
                                                               Para ver un histograma de resultados numéricos y más detalle de los antecedentes acceder a <b>Historial por Analisis</b>.
                                                           </td>
                                                           <td>
                                                               &nbsp;</td>
                                                           <td class="myLabelIzquierda" align="right">
                                                          

                                                           </td>
                                                       </tr>
                                                       <tr>
                                                           <td class="myLabelIzquierda" colspan="3">
                                                             <hr />
                                                           </td>
                                                       </tr>
                                                          <tr>
                                                           <td>
                                                               &nbsp;
                                                           </td>
                                                            <td>
                                                               &nbsp;
                                                           </td>
                                                            <td>
                                                               &nbsp;
                                                           </td>
                                                       </tr>
                                                       
                                                       <tr>
                                                           <td class="myLabelIzquierda">
                                                           Auditoría
                                                           </td>
                                                           <td>
                                                               &nbsp;
                                                           </td>
                                                           <td class="myLabelIzquierda" align="right">
                                                            

                                                           </td>
                                                       </tr>
                                                       <tr>
                                                          <td class="myLabel">
                                                            <br />
                                                              Desde acá puede ver la trazabilidad (los movimientos realizados por lo usuarios) del protocolo.
                                                              <br /> &nbsp;  <br /> &nbsp;
                                                               <asp:Button ID="Button4" ToolTip="Auditoría del Protocolo" runat="server" Text="Ver Auditoria"  Width="150px" 
                       OnClientClick="AuditoriaView(); return false;" />
                       
                                                           </td>
                                                            <td>
                                                               &nbsp;
                                                           </td>
                                                            <td>
                                                               &nbsp;
                                                           </td>
                                                       </tr>
                                                   </table>
                                                <%--   <table width="850">
                                                       <tr>
                                                           <td class="myLabelIzquierdaGde">
                                                               &nbsp;
                                                               Análisis:&nbsp;&nbsp;
                                                               
                                                               <asp:DropDownList ID="ddlItem" runat="server">
                                                               </asp:DropDownList>
                                                              
                                                                <asp:Button ID="btnVerAntecendente" runat="server"  CssClass="myButtonGrisLittle"
                                                                   onclick="btnVerAntecendente_Click" Text="Ver Antecedentes" Width="120px" />
                                                           </td>
                                                       </tr>
                                                       <tr>
                                                           <td>
                                                               &nbsp;
                                                               &nbsp;
                                                               &nbsp;
                                                               <asp:GridView ID="gvAntecedente" runat="server" 
                                                                   CellPadding="1" EmptyDataText="No se encontraron antecedentes." Font-Size="9pt" 
                                                                   Width="100%" CssClass="mytable2">
                                                                   <HeaderStyle BackColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                                                                       ForeColor="#333333" BorderColor="#666666" />
                                                               </asp:GridView>
                                                           </td>
                                                       </tr>
                                                       <tr>
                                                           <td>
                                                               &nbsp;
                                                           </td>
                                                       </tr>
                                                   </table>--%>
                                               </asp:Panel> 
                                               
                                               </div>
                                               
                         <div id="tab6" class="tab_content" style="height: 500px">
                                    
                                    <uc1:IncidenciaEdit ID="IncidenciaEdit1" runat="server" />
                             <asp:Button ID="btnGuardarIncidencia" runat="server" Text="Guardar" onclick="btnGuardarIncidencia_Click" CssClass="myButtonGris" Width="100px" />
                             <asp:Button ID="btnEliminarIncidencia" runat="server" Text="Borrar" onclick="btnEliminarIncidencia_Click" CssClass="myButtonGris" Width="100px"/>
                                    
                                </div>                       
                                               
</div>
</td>
						
					</tr>
					<tr>
						
						<td   colspan="3" align="right">
                             <asp:ImageButton ID="imgImprimir" runat="server" 
                                ImageUrl="~/App_Themes/default/images/imprimir.jpg" 
                                   onclick="imgImprimir_Click" /> &nbsp; 
						       <asp:ImageButton ID="imgPdf" runat="server" 
                                ImageUrl="~/App_Themes/default/images/pdf.jpg" onclick="imgPdf_Click" /> </td>
						
					</tr>
					
					<tr>
						
						<td   colspan="3" align="right">
                            <asp:Panel ID="pnlImpresora" runat="server" style="border:1px solid #C0C0C0; width:100%; background-color: #EFEFEF;">                                                      
 <table width="720px" align="center">
 	<tr>
						<td class="myLabelIzquierda" align="left" style="width: 140px; background-color: #EFEFEF;">
                                        Impresora del sistema: </td>
						<td  align="left">
                                        <asp:DropDownList ID="ddlImpresora" runat="server" CssClass="myList" >
                                        </asp:DropDownList>
                            </td>
						
                                        </tr>
                                        </table>
														
 </asp:Panel>
                         </td>
						
					</tr>
					        </table>
                            </td>
						
					</tr>
											
					
					
					
						
						</table>						
 </div>

 <script src="../script/Resources/jquery.min.js" type="text/javascript"></script>
 <link href="../script/Resources/jquery-ui-1.8.20.css" rel="stylesheet" type="text/css" />   
    <script src="../script/Resources/jQuery-ui-1.8.18.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

  
    function seleccionar_todo() {//Funcion que permite seleccionar todos los checkbox     
        form = document.forms[0];
        
        for (i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == "checkbox") 
            {
                if (form.elements[i].name.indexOf("TreeView2") == -1)
                if (form.elements[i].name.indexOf("chkCerrarSinResultados")==-1)   
                 if (form.elements[i].name.indexOf("F")==-1) 
                    form.elements[i].checked = 1;                                       
            }
        }
    }

    function desmarcar_todo() {//Funcion que permite seleccionar todos los checkbox
        form = document.forms[0];
        for (i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == "checkbox") {
                if (form.elements[i].name.indexOf("TreeView2") == -1)
                if (form.elements[i].name.indexOf("chkCerrarSinResultados") == -1)
                    if (form.elements[i].name.indexOf("F")==-1) 
                        form.elements[i].checked = 0;
            }
        }
    } 


    var iditem = $("#<%= HFIdItem.ClientID %>").val();
    var idProtocolo = $("#<%= HFIdProtocolo.ClientID %>").val();
    var idGermen = $("#<%= HFIdGermen.ClientID %>").val();

    var idMetodo = $("#<%= HFIdMetodo.ClientID %>").val();
    var numeroAislamiento = $("#<%= HFNumeroAislamiento.ClientID %>").val();
    var operacion = $("#<%= HFOperacion.ClientID %>").val();
    
 
    function editarATB() {
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
        $('<iframe src="ATBEdit.aspx?Operacion='+operacion+'&idItem=' + iditem + '&idMetodo='+idMetodo+'&numeroAislamiento='+ numeroAislamiento +'&idGermen='+ idGermen +'&idProtocolo='+idProtocolo+ '" />').dialog({
            title: 'Modificar ATB',
            autoOpen: true,
            width: 450,
            height: 500,
            modal: false,
            resizable: false,
            autoResize: true,
         <%--   open: function (event, ui) { jQuery('.ui-dialog-titlebar-close').hide();},
            buttons: {
             'Cerrar': function () { <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnEditarAntibiograma))%>; }               
            },--%>
            overlay: {
                opacity:0.5,
                background: "black"
            }
        }).width(480);
    }


    function validarATB() {
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
        $('<iframe src="ATBValida.aspx?idItem=' + iditem + '&idMetodo=' + idMetodo + '&numeroAislamiento=' + numeroAislamiento + '&idGermen=' + idGermen + '&idProtocolo=' + idProtocolo + '" />').dialog({
            title: 'Validar ATB',
            autoOpen: true,
            width: 650,
            height: 560,
            modal: true,
            resizable: false,
            autoResize: false,
            //open: function (event, ui) { jQuery('.ui-dialog-titlebar-close').hide(); },
          <%--  buttons: {
                'Cerrar': function () { <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnEditarAntibiograma))%>; }
            },--%>
            overlay: {
                opacity: 0.5,
                background: "black"
            }
        }).width(680);
}


    function AntecedenteView() {
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
        $('<iframe src="AntecedentesView2.aspx?idProtocolo=' + idProtocolo + '" />').dialog({
            title: 'Antecedentes del Paciente',
            autoOpen: true,
            width:750,
            height: 490,
            modal: true,
            resizable: false,
            autoResize: true,
            overlay: {
                opacity: 0.5,
                background: "black"
            }
        }).width(770);
    }

      function ObservacionEdit(idDetalle,idTipoServicio,operacion) {
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
        $('<iframe src="ObservacionesEdit.aspx?idDetalleProtocolo=' + idDetalle + '&idTipoServicio='+idTipoServicio+'&Operacion='+operacion+'" />').dialog({
            title: 'Observaciones',
            autoOpen: true,
            width: 500,
            height: 440,
            modal: true,
            resizable: false,
            autoResize: true,
              open: function (event, ui) { jQuery('.ui-dialog-titlebar-close').hide();},
            buttons: {
             'Cerrar': function () { <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnActualizar))%>; }               
            },
            overlay: {
                opacity:0.5,
                background: "black"
            }
        }).width(510);
    }

    function PredefinidoSelect(idDetalle, operacion) {
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
        $('<iframe src="PredefinidoSelect.aspx?idDetalleProtocolo=' + idDetalle +'&Operacion='+operacion+'" />').dialog({        
            title: 'Resultados',
            autoOpen: true,
            width: 500,
            height: 440,
            modal: true,
            resizable: false,
            autoResize: true,
              open: function (event, ui) { jQuery('.ui-dialog-titlebar-close').hide();},
            buttons: {
             'Cerrar': function () { <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnActualizar))%>; }               
            },
            overlay: {
                opacity:0.5,
                background: "black"
            }
        }).width(510);
    }

    function AuditoriaView() {
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
        $('<iframe src="AuditoriaView.aspx?idProtocolo=' + idProtocolo + '" />').dialog({
            title: 'Auditoria del Protocolo',
            autoOpen: true,
            width: 800,
            height: 520,
            modal: true,
            resizable: false,
            autoResize: true,
            overlay: {
                opacity: 0.5,
                background: "black"
            }
        }).width(900);
    }
 
 
 

    function ProtocoloEdit() {
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
        $('<iframe src="AnalisisEdit.aspx?idProtocolo=' + idProtocolo + '" />').dialog({
            title: 'Practicas Pedidas',
            autoOpen: true,
            width: 800,
            height: 450,
            modal: true,
            resizable: false,
            autoResize: true,
            open: function (event, ui) { jQuery('.ui-dialog-titlebar-close').hide();},
            buttons: {
             'Cerrar': function () { <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnActualizar))%>; }               
            },
            overlay: {
                opacity:0.5,
                background: "black"
            }
        }).width(900);
    }
 
 
 function ProtocoloEditCarga() {
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
        $('<iframe src="AnalisisEdit.aspx?idProtocolo=' + idProtocolo + '" />').dialog({
            title: 'Practicas Pedidas',
            autoOpen: true,
            width: 800,
            height: 450,
            modal: true,
            resizable: false,
            autoResize: true,
              open: function (event, ui) { jQuery('.ui-dialog-titlebar-close').hide();},
            buttons: {
             'Cerrar': function () { <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnActualizar))%>; }               
            },
            overlay: {
                opacity:0.5,
                background: "black"
            }
        }).width(900);
    }
 
 
 
 
 
 function editDiagnostico() {
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
        $('<iframe src="DiagnosticoEdit.aspx?idProtocolo=' + idProtocolo + '" />').dialog({
            title: 'Diagnostico del Paciente',
            autoOpen: true,
            width: 750,
            height: 475,
            modal: true,
            resizable: false,
            autoResize: true,
              open: function (event, ui) { jQuery('.ui-dialog-titlebar-close').hide();},
            buttons: {
             'Cerrar': function () { <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnActualizar))%>; }               
            },
            overlay: {
                opacity:0.5,
                background: "black"
            }
        }).width(765);
    }
 
   
    function PreguntoEliminar()
    {
    if (confirm('¿Está seguro de borrar?'))
    return true;
    else
    return false;
    }
      function PreguntoConfirmar()
    {
    if (confirm('¿Está seguro de desvalidar lo seleccionado?'))
    return true;
    else
    return false;
    }
      function PreguntoValidar() {
          if (confirm('¿Está seguro de validar todos los ATB?'))
              return true;
          else
              return false;
      }


    function AntecedenteAnalisisView(idAnalisis, idPaciente, ancho,alto) {
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

        $('<iframe src="AntecedentesAnalisisView.aspx?idAnalisis=' +  idAnalisis+ '&idPaciente='+idPaciente+'" />').dialog({
            title: 'Evolucion',
            autoOpen: true,
            width:ancho,
            height: alto,
            modal: true,
            resizable: false,
            autoResize: true,
            overlay: {
                opacity: 0.5,
                background: "black"
            }
        }).width(800);
    }



     function PesquisaNeonatalView(idProtocolo, ancho,alto) {
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

        $('<iframe src="PesquisaNeonatalView.aspx?idProtocolo=' +  idProtocolo+ '" />').dialog({
            title: 'Tarjeta Pesquisa Neonatal',
            autoOpen: true,
            width:ancho,
            height: alto,
            modal: true,
            resizable: false,
            autoResize: true,
            overlay: {
                opacity: 0.5,
                background: "black"
            }
        }).width(670);
    }

    </script>



</asp:Content>
