<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProtocoloEdit2.aspx.cs" Inherits="WebLab.Protocolos.ProtocoloEdit2" MasterPageFile="~/Site1.Master" %>

<%@ Register Src="~/Services/ObrasSociales.ascx" TagName="OSociales" TagPrefix="uc1" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>


<%@ Register src="../Calidad/IncidenciaEdit.ascx" tagname="IncidenciaEdit" tagprefix="uc1" %>


<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href='<%= ResolveUrl("~/script/jquery-ui.css") %>'  />
<script src='<%= ResolveUrl("~/script/jquery-1.9.1.js") %>' ></script>
<script src='<%= ResolveUrl("~/script/jquery-ui.js") %>' ></script>
    <script  src='<%= ResolveUrl("~/script/jquery.ui.datepicker-es.js") %>'  ></script>   

      <script type="text/javascript" src='<%= ResolveUrl("~/Services/js/jquery-1.5.1.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Services/js/jquery-ui-1.8.9.custom.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Services/js/json2.js") %>'></script>
    <link href='<%= ResolveUrl("~/Services/css/redmond/jquery.ui.all.css") %>' rel="stylesheet" type="text/css" />
    

      
   	 
     <script type="text/javascript" src="../script/ValidaFecha.js"></script>                

     
     
     <script type="text/javascript">                     
            $(function() {
                 $("#tabContainer").tabs();                        
                $("#tabContainer").tabs({ selected: 0 });
             });                         
          
            
         $(function() {
		        $("#<%=txtFecha.ClientID %>").datepicker({
		            showOn: "both",
		            buttonImage: '../App_Themes/default/images/calend1.jpg',
		            buttonImageOnly: true
		        });
	        });
	 
	        $(function() {
		        $("#<%=txtFechaOrden.ClientID %>").datepicker({
		            showOn: "both",
			        buttonImage: '../App_Themes/default/images/calend1.jpg',
			        buttonImageOnly: true
		        });
	        });
              
           $(function() {
		        $("#<%=txtFechaTomaMuestra.ClientID %>").datepicker({
		            showOn: "both",
			        buttonImage: '../App_Themes/default/images/calend1.jpg',
			        buttonImageOnly: true
		        });
	        });
          

            function enterToTab(pEvent) 
            {///solo para internet explorer
                if (window.event) // IE
                {
                    if (window.event.keyCode == 13) {
                        if (pEvent.srcElement.id.indexOf('Codigo_') == 0) {
                            window.event.keyCode = 9;
                        }
                    }
                }
               
             }

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

  </script>  
  
   <link href="../App_Themes/default/style.css" rel="stylesheet" type="text/css" /> 


   
    </asp:Content>




 
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          
    <table>
  <tr>
   <td>
  &nbsp;
  
  </td>
  <td  style="vertical-align: top">
      <asp:Panel ID="pnlLista" runat="server" 
          style="width:118px;height:450pt;overflow:scroll;border:1px solid #CCCCCC;" 
          Visible="False">
      
  
  			 
    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="idProtocolo" onrowcommand="gvLista_RowCommand" 
        onrowdatabound="gvLista_RowDataBound" CellPadding="4" 
        HorizontalAlign="Left" Font-Size="8pt" BorderColor="#3A93D2" BorderStyle="Solid" 
                                BorderWidth="1px" GridLines="None" Font-Bold="False" ForeColor="#333333" 
                                Width="100px" Visible="False" 
                                        >
                                         <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                         <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Arial" 
                Font-Size="8pt" />
       
        <Columns>
            <asp:BoundField DataField="numero" HeaderText="Protocolo" />
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
    
   </asp:Panel>
  
  
  </td>
  <td>
  &nbsp;
  
  </td>
  <td>
  <div style="margin:1%; width:1100px; height:760px;border:1px solid #CCCCCC;"> 
                 <table  width="1100px" align="center">
					<tr>
						<td style="vertical-align: top" rowspan="6"  > 
						
                                     </td>
						<td colspan="2"  >
						
						    <%--<asp:DropDownList ID="ddlObraSocial" runat="server" CssClass="myList" 
                                                TabIndex="15" Width="400px" >
                                                <asp:ListItem Value="1">No tiene</asp:ListItem>
                                            </asp:DropDownList>--%>
                            <table style="width:100%;">
                                <tr>
                                    <td class="myLabelIzquierda" colspan="10">
                                          
                             
                                                                                    
                                      
						    
                                <div>
                                <table style="width:100%;" >
                                <tr>
                                    
                                <td style="vertical-align: top" align="left" >   
                                    <asp:Panel ID="pnlNumero"  runat="server" width="80px" Visible="false">
                                    <asp:Label ID="lblTitulo" runat="server" CssClass="mytituloRelleno"></asp:Label>     <br />   <br />
                                                    <asp:Label ID="lblServicio" runat="server" ></asp:Label>     
                                        </asp:Panel>
                                </td>
                                <td style="vertical-align: top" align="left">   
                            
                                        
                             
                                          <asp:Panel ID="pnlPaciente" runat="server" Width="700px">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPaciente" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                            Font-Size="12pt" ForeColor="#333333" Text="zzzzzzz"></asp:Label>
                                                        
                                                    </td>
                                                
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFechaNacimiento" runat="server" Text="Label"></asp:Label>
                                                        <label>
                                                        Edad:
                                                        </label>
                                                        <asp:Label ID="lblEdad" runat="server" Text="Label"></asp:Label>
                                                        <asp:Label ID="lblUnidadEdad" runat="server" Text="Label"></asp:Label>
                                                        <label>
                                                        Sexo:
                                                        </label>
                                                        <asp:Label ID="lblSexo" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:HyperLink ID="hplModificarPaciente" runat="server" CssClass="myLittleLink" ToolTip="Cambiar el paciente asociado al protocolo">Cambiar Paciente</asp:HyperLink>
                                                        &nbsp;<asp:HyperLink ID="hplActualizarPaciente" runat="server" CssClass="myLittleLink" ToolTip="Actualizar datos del paciente actual.">Datos del Paciente</asp:HyperLink>
                                                    </td>
                                                    
                                                </tr>
                                            </table>
                                        </asp:Panel>                                          
                                      
                                     </td>
                                <td style="vertical-align: top" align="right">   
                                     
                                    <asp:Panel ID="pnlNavegacion" runat="server">
                                   <table>
                                       <tr>
                                            <td style="vertical-align: top" align="right">   
                                               <asp:Label ID="lblEstado" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                Text="Label" Visible="False"></asp:Label>

                                           </td>
                                       </tr>
                                       <tr>
                                           <td style="vertical-align: top" align="right">   
                                                   <asp:Label ID="lblUsuario" runat="server" Text="Label" CssClass="myLabel"></asp:Label> 
                                            
                                           </td>
                                       </tr>
                                       <tr>
                                          <td style="vertical-align: top" align="right">    <asp:LinkButton ID="lnkAnterior" runat="server" CssClass="myLittleLink" 
                                onclick="lnkAnterior_Click" ToolTip="Ir al protocolo anterior cargado">&lt;Anterior</asp:LinkButton> &nbsp;|&nbsp; 
                                              <asp:LinkButton 
                                ID="lnkSiguiente" runat="server" CssClass="myLittleLink" 
                                onclick="lnkSiguiente_Click" ToolTip="Ir al siguiente protocolo cargado">Siguiente&gt;</asp:LinkButton> 
                                </td>
                                       </tr>
                                   </table>
                                             
                                </asp:Panel>
                                </td>
                                </tr>
                                </table>
                                  
                             
                                </div>
                            
                                        
                             
                                     </td>
                                         <td>
                                             &nbsp;</td>
                                    
                                        
                           
                                        
                                </tr>
                           
                                <tr>
                                    <td class="myLabelIzquierda" colspan="10">
                                       <hr /></td>
                                </tr>
                                <tr>
            
                                
                                    <td class="myLabelIzquierda">
                                        Fecha:</td>
                                    <td>
                                        
                                        <asp:Label ID="lblIdPaciente" runat="server" Text="Label" Visible="False"></asp:Label>
                               
           
                                     
                    <input id="txtFecha" runat="server" type="text"  class="myTexto"  maxlength="10"   style="width: 80px; position=absolute; z-index=0"  onblur="valFecha(this)"  
                        onkeyup="mascara(this,'/',patron,true)" tabindex="0"/>
           
               </td>
                                    <td class="myLabelIzquierda" align="right">
                                        
                                        Fecha Orden: </td>
                                    <td class="myLabelIzquierda">
                                        
                                        <input id="txtFechaOrden" runat="server" type="text" maxlength="10" 
                        style="width: 80px"  onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="1" class="myTexto"  /></td>
                                     <td class="myLabelIzquierda">
                                        Fecha Toma Muestra:</td>
                                    <td>
                                        
                    <input id="txtFechaTomaMuestra" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="2" class="myTexto" 
                                style="width: 80px"  /></td>
                                      <td class="myLabelIzquierda" align="right">
                                         Nro. de Origen: </td>
                                      <td class="myLabelIzquierda">
                                      <asp:TextBox ID="txtNumeroOrigen" runat="server" CssClass="myTexto" Width="120px" 
                                            TabIndex="3" MaxLength="50"></asp:TextBox>
                                        </td>
                                    <td align="left">
                                        &nbsp;</td>
                        <td rowspan="3">
                         
                        </td>
                                </tr>
                                <tr>
                                    <td class="myLabelIzquierda">
                                                                                Efector Solicitante:       </td>
                                    <td   colspan="3">
                            <anthem:DropDownList ID="ddlEfector" runat="server" 
                                ToolTip="Seleccione el efector" TabIndex="4" CssClass="myList" 
                                AutoCallBack="True" onselectedindexchanged="ddlEfector_SelectedIndexChanged">
                            </anthem:DropDownList>
                                        
					                 
                                        
                                            </td>
                                     <td class="myLabelIzquierda">
                                            Médico Solicitante:</td>
                                    <td colspan="4">
                            <anthem:DropDownList ID="ddlEspecialista" runat="server" 
                                ToolTip="Seleccione el especialista" TabIndex="5" CssClass="myList">
                                <asp:ListItem Value="0">No identificado</asp:ListItem>
                            </anthem:DropDownList>
                                        
                          
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="myLabelIzquierda">
                                        Origen/Servicio:   <asp:RangeValidator ID="rvOrigen" runat="server" 
                                ControlToValidate="ddlOrigen" ErrorMessage="Origen" MaximumValue="999999" 
                                MinimumValue="1" Type="Integer" ValidationGroup="0">*</asp:RangeValidator>
                                        </td><td   colspan="3">
                            <anthem:DropDownList ID="ddlOrigen" runat="server"  
                                ToolTip="Seleccione el origen" TabIndex="6" CssClass="myList">
                            </anthem:DropDownList>                                        
                                        <asp:DropDownList ID="ddlSectorServicio" runat="server" TabIndex="7" Width="180px">
                                        </asp:DropDownList>
                                        
					                    <asp:RangeValidator ID="rvSectorServicio" runat="server" 
                                ControlToValidate="ddlSectorServicio" ErrorMessage="Sector" MaximumValue="999999" 
                                MinimumValue="1" Type="Integer" ValidationGroup="0">*</asp:RangeValidator>
                                        
                                            </td>
                                     <td class="myLabelIzquierda">
                                        Prioridad:</td>
                                        <td>
                            <asp:DropDownList ID="ddlPrioridad" runat="server" 
                                ToolTip="Seleccione la prioridad" TabIndex="7" CssClass="myList">
                            </asp:DropDownList>
					                    <asp:RangeValidator ID="rvPrioridad" runat="server" 
                                ControlToValidate="ddlPrioridad" ErrorMessage="Prioridad" MaximumValue="999" 
                                MinimumValue="1" Type="Integer" ValidationGroup="0">*</asp:RangeValidator>
                                      
                                        </td>
                                      <td class="myLabelIzquierda" align="right">  
                                         Sala/Cama:                        
                                        </td>
      
                                      <td class="myLabelIzquierda">  
                                      <asp:TextBox ID="txtSala" runat="server" CssClass="myTexto" Width="50px" 
                                            TabIndex="8" MaxLength="50"></asp:TextBox>
                                        <asp:TextBox ID="txtCama" runat="server" CssClass="myTexto" Width="50px" 
                                            TabIndex="9" MaxLength="50"></asp:TextBox>                                           
                                      
                                        </td>
      
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                               
                                <tr>
                                       <td class="myLabelIzquierda" colspan="9">
                                       
                                            <asp:ScriptManager ID="scriptMgr" runat="server">
                                            </asp:ScriptManager>
                                            <asp:UpdatePanel ID="upOSocial" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table id="tableOSociales" class="tabla3">
                                                        <tr>
                                                            <td class="myLabelIzquierda" valign="top">Financiador / O.S:&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td>
                                                                <div id="OS" runat="server" style="float: right; margin-top: 6px;
                                                    margin-right: 0px;" visible="false">
                                                                    &nbsp;</div>
                                                                <div style="font-family: Verdana; position:relative; z-index:1;" >
                                                                    <uc1:OSociales ID="OSociales" runat="server" TabIndex="10" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                       </td>
                                   
                                </tr>
                               
                               	<tr>
						<td colspan="9"  >  
						<asp:Panel CssClass="myLabelIzquierda" runat="server" ID="pnlMuestra" Width="100%">
						<hr />
                          Muestra a Analizar: &nbsp; &nbsp; 
                            <anthem:TextBox ID="txtCodigoMuestra" Width="50px"  CssClass="myTexto" runat="server"  ontextchanged="txtCodigoMuestra_TextChanged" AutoCallBack="true"></anthem:TextBox> 
                            <anthem:DropDownList ID="ddlMuestra" runat="server" AutoCallBack="true"  onselectedindexchanged="ddlMuestra_SelectedIndexChanged"  CssClass="myList" >
                            </anthem:DropDownList>
                            <anthem:RangeValidator ID="rvMuestra" runat="server"     ErrorMessage="Muestra" 
                                ControlToValidate="ddlMuestra" Enabled="False" MaximumValue="9999999" 
                                MinimumValue="1" Type="Integer" ValidationGroup="0">*</anthem:RangeValidator>
                            
                            </asp:Panel>
						</td>
					</tr>                                                          
                               	
                            </table>
                            <%--    <li><a href="#tab3">Diagnosticos Frecuentes</li>--%>
                                        
                                            </td>
					</tr>
					<tr>
						<td colspan="2" ><hr /></td>
					</tr>
					
					
					
					<tr>
						<td colspan="2"  >
					
						
					<div id="tabContainer" style="width: 1050px; z-index:2; position:relative;" >  
						       <ul style="font-size: small">
    <li ><a href="#tab1">Analisis</a></li>    
    <li><a href="#tab2">Diagnósticos<img alt="tiene diagnostico" runat="server" id="diag" visible="false" style="border:none;" src="~/App_Themes/default/images/red_pin.gif" /></a></li>
    <li><a href="#tab3">Impresiones</a></li>
    <li><a href="#tab4">Nuevo Solicitante Externo</a></li>
    <li id="tituloCalidad" runat="server"><a  href="#tab5">Incidencias<img alt="tiene incidencias" runat="server" id="inci" visible="false" style="border:none;" src="~/App_Themes/default/images/red_pin.gif" /></a></li>
    

</ul>
                          
                            <div id="tab1" style="height: 280px">
                   

                                 <table style="width:1000px;">
                                <tr>
                                    <td>
                                  <table cellpadding="1" cellspacing="0" >
                    <tr >
                        <td style="width: 10px; height: 13px">
                        </td>
                        <td style="width: 50px;" class="tituloCelda">
                            Codigo</td>
                        <td style="width: 350px;"  class="tituloCelda">
                            Descripcion</td>
                        <td style="width: 80px; " class="tituloCelda">
                            S/ Muestra</td>                    
                        <td style="width: 18px;" class="tituloCelda">
                        </td>
                    </tr>
                </table>
                
                <div  onkeydown="enterToTab(event)" style="width:545px;height:180pt;overflow:scroll;overflow-x:hidden;border:1px solid #CCCCCC;"> 
                       
                    <table  class="mytablaIngreso"  border="0" id="tabla" cellpadding="0" cellspacing="0" >
	  	                <tbody id="Datos" >
		                    
		                </tbody>
	                </table>
	                
                    <input type="hidden" runat="server" name="TxtCantidadFilas" id="TxtCantidadFilas" value="0" />
                </div>
                      <div> <asp:CheckBox ID="chkRecordarPractica" runat="server" CssClass="myLabelGris"
                                Text="Recordar análisis" />
                          
                                             
                            
						
                                            <asp:CustomValidator ID="cvValidacionInput" runat="server" 
                                                ErrorMessage="Debe completar al menos un analisis" 
                                    ValidationGroup="0" Font-Size="8pt" onservervalidate="cvValidacionInput_ServerValidate" 
                                             ></asp:CustomValidator>
                                                 
                                </div>                 
                                      </td>
                                    <td style="vertical-align: top">
                                       
                                       <fieldset id="Fieldset3" title="Analisis" style="width:95%;text-align:left;">
                                       <legend class="myLabelIzquierda">Analisis</legend>
                                       <table>   <tr> <td class="myLabelIzquierda"  >Rutina</td></tr>
                                         <tr><td class="myLabelIzquierda" >  <anthem:DropDownList ID="ddlRutina" runat="server" AutoCallBack="True"                                                
                                                
                                CssClass="myList" TabIndex="20" 
                                onselectedindexchanged="ddlRutina_SelectedIndexChanged">
                                          </anthem:DropDownList>
                                      <br />               <input id="Button2" type="button" value="Agregar" 
                                onclick="AgregarDeLaListaRutina();"
                                class="myButtonGris" style="width: 100px" />
                                </td></tr>
                                       <tr> <td class="myLabelIzquierda"  >Búsqueda por Nombre</td></tr>
                                       <tr><td class="myLabelIzquierda" >	<anthem:DropDownList ID="ddlItem" runat="server" AutoCallBack="True" 
                                                onselectedindexchanged="ddlItem_SelectedIndexChanged" 
                                                TextDuringCallBack="Cargando ..." 
                                CssClass="myList" TabIndex="20">
                                            </anthem:DropDownList>	                                                   
                            <input id="Button1" type="button" value="Agregar" 
                                onclick="AgregarDeLaLista();" 
                                class="myButtonGris" style="width: 100px" /></td>
                                       
                                       </tr>
                                      
                                       </table>
                                       </fieldset>
                                        </td>
                                </tr>
                                </table>
                                <input type="hidden" runat="server" name="TxtDatosCargados" id="TxtDatosCargados" value="" />                                
                                   <input type="hidden" runat="server" name="TxtDatos" id="TxtDatos" value="" />                                
                <input id="txtTareas" name="txtTareas" runat="server" type="hidden"  />
                            </div>
                          
                            <div id="tab2" style="height: 280px">
                           
                                <fieldset id="Fieldset2"                                  >
                                <legend class="myLabelIzquierda">Diagnósticos Presuntivos - Codificación CIE 10</legend>
                                <table>
                                <tr>
                                <td>
                                    
						 <div id="tab11" runat="server">
                                     <table align="left" width="100%">
                                         <tr>
                                           <td class="myLabelIzquierda" colspan="3"  >
                                               Codigo:
                                                 <anthem:TextBox ID="txtCodigoDiagnostico" runat="server" AutoCallBack="True" 
                                                     CssClass="myTexto" ontextchanged="txtCodigoDiagnostico_TextChanged"></anthem:TextBox>
                                             </td>  
                                           
                                         </tr>
                                           <tr>
                                           <td class="myLabelIzquierda" colspan="3"  >
                                            Nombre:<anthem:TextBox ID="txtNombreDiagnostico" runat="server" AutoCallBack="True" 
                                                     CssClass="myTexto" ontextchanged="txtNombreDiagnostico_TextChanged" 
                                                   Width="268px"></anthem:TextBox></td>
                                           </tr>
                                         
                                           <tr>
                                           <td class="myLabelIzquierda" colspan="4"  >
                                              <anthem:Button ID="btnBusquedaDiagnostico" CssClass="myButtonGris" runat="server" Text="Buscar" 
                                                   onclick="btnBusquedaDiagnostico_Click" />
                              <anthem:Button ID="btnBusquedaFrecuente" CssClass="myButtonRojo" runat="server" Text="Ver Frecuentes" 
                                                   onclick="btnBusquedaFrecuente_Click" /></td>
                                         </tr>
                                         <tr>
                                           <td class="myLabelIzquierda" colspan="4"  >
                                               <hr /></td>
                                         </tr>
                                         
                                        
                                         <tr>
                                           <td class="myLabelIzquierda"  colspan="4" >
                                               Diagnósticos encontrados       </td>                                          
                                               
                                         </tr>
                                         <tr>
                                           <td class="myLabelIzquierda" colspan="4" >
                                             
                            
						
                                               <anthem:ListBox ID="lstDiagnosticos" runat="server" AutoCallBack="True" 
                                                   CssClass="myTexto" Height="120px" Width="400px">
                                               </anthem:ListBox>
                                              
                                             </td>
                                             
                                        </tr>
                                        </table>
                                      </div>
                                      	                                       
                                     
                                 </td>
                                             <td>
                                                 <anthem:ImageButton ID="btnAgregarDiagnostico" runat="server" 
                                                     ImageUrl="~/App_Themes/default/images/añadir.jpg" 
                                                     onclick="btnAgregarDiagnostico_Click1" /><br />
                                                     <p></p>
                                                 <anthem:ImageButton ID="btnSacarDiagnostico" runat="server" 
                                                     ImageUrl="~/App_Themes/default/images/sacar.jpg" 
                                                     onclick="btnSacarDiagnostico_Click" />
                                             </td>                                         
                                             <td style="vertical-align: top">
                                             <p class="myLabelIzquierda">Diagnósticos del Paciente</p>
                                                 <anthem:ListBox ID="lstDiagnosticosFinal" runat="server" CssClass="myTexto" 
                                                     Height="200px" Width="400px" SelectionMode="Multiple">
                                                 </anthem:ListBox>
                                             </td>   
                                
                                </tr>
                                </table>
                                        
                                </fieldset>                                                                                          
                            </div>
                            
                            
                                
                             <div id="tab3" class="tab_content" style="height: 310px">
                                 <table style="width: 100%;">
                                     <tr>
                                         <td class="myLabelIzquierda" style="vertical-align: top; width: 222px;">
                                             <asp:Label ID="lblImprimeComprobantePaciente"    runat="server" Text="Comprobante para el paciente:&nbsp;"></asp:Label>
                                       
                                         </td>
                                         <td  style="vertical-align: top;">
                                             <asp:CheckBox ID="chkImprimir" runat="server" CssClass="myLabel"  Text="Imprimir" />
                                         </td>
                                         <td  class="myLabelIzquierda">
                                             <asp:DropDownList CssClass="myList"   ToolTip="Seleccione Impresora" ID="ddlImpresora" runat="server">
                                             </asp:DropDownList>
                                         </td>
                                         <td><anthem:LinkButton CssClass="myLink" ID="lnkReimprimirComprobante" onclick="lnkReimprimirComprobante_Click" runat="server">Reimprimir Comprobante</anthem:LinkButton>
                                         </td>
                                     </tr>
                                        
                                      <tr>
                                        <td colspan="4"><hr /></td>
                                      </tr>
                                     
                                     <tr>
                                         <td class="myLabelIzquierda" style="vertical-align: top; width: 222px;">
                                             <asp:Label ID="lblImprimeCodigoBarras" runat="server" Text=" Imprimir códigos de barras:"></asp:Label>   
                                             <anthem:RadioButtonList ID="rdbSeleccionarAreasEtiquetas" runat="server" 
                                                 AutoCallBack="True" 
                                                 onselectedindexchanged="rdbSeleccionarAreasEtiquetas_SelectedIndexChanged">
                                                 <asp:ListItem Value="1">Marcar Todas</asp:ListItem>
                                                 <asp:ListItem Value="0">Desmarcar Todas</asp:ListItem>
                                             </anthem:RadioButtonList>                  


                                         </td>
                                         <td class="myLabel" style="vertical-align: top">
                                             <anthem:CheckBoxList ID="chkAreaCodigoBarra" runat="server" RepeatColumns="2"></anthem:CheckBoxList>                                             
                                                
                                         </td>
                                         <td  class="myLabelIzquierda" style="vertical-align: top">
                                          
                                             <asp:DropDownList CssClass="myList" ToolTip="Seleccione Impresora de Etiqueta" ID="ddlImpresoraEtiqueta" runat="server">
                                             </asp:DropDownList>
                                         </td>
                                         <td style="vertical-align: top">
                                         <anthem:LinkButton CssClass="myLink" ID="lnkReimprimirCodigoBarras"  onclick="lnkReimprimirCodigoBarras_Click" runat="server">Reimprimir Código de Barras</anthem:LinkButton>
                                         </td>
                                     </tr>
                                     
                                     <tr>
                                        <td colspan="4"><hr /></td>
                                     </tr>
                                        
                                     <tr>
                                         <td class="myLabelIzquierda" style="vertical-align: top; width: 222px;">
                                             <asp:CheckBox ID="chkRecordarConfiguracion" runat="server" CssClass="myLabelGris"  Text="Recordar ésta configuracion" />
                                         </td>
                                         <td>
                                            &nbsp;     
                                         </td>                                        
                                         <td colspan="2" align="right" class="myLabelIzquierda">                                             


                                             <asp:CheckBox ID="chkCodificaPaciente" runat="server" 
                                                 Text="Codificar datos del paciente en todas las etiquetas" 
                                                 ForeColor="#CC3300" />
                                         </td>
                                        
                                     </tr>                                                                          
                                 </table>
                             
                             </div>
                             
                             
                             <div id="tab4" class="tab_content"  style="height: 280px">
                        
                                <table style="width:100%;">
                                    <tr>
                                        <td class="myLabelIzquierda">                                            
                                            Matricula:                                            
                                        </td>
                                        <td class="myLabelIzquierda">                                            
                                            <asp:TextBox ID="txtMatricula" runat="server" CssClass="myTexto" 
                                                Width="52px">                                                                                         
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMatricula" runat="server" 
                                                ControlToValidate="txtMatricula" ErrorMessage="Matricula" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda">                                            
                                            Apellido:                                            
                                        </td>
                                        <td class="myLabelIzquierda">                                            
                                            <asp:TextBox ID="txtApellidoSolicitante" runat="server" CssClass="myTexto" 
                                                Height="16px" Width="130px">                                                                                                                                                                                
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvApellidoSolicitante" runat="server" 
                                                ControlToValidate="txtApellidoSolicitante" ErrorMessage="Apellido" 
                                                ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda">                                            
                                            Nombre:
                                        </td>
                                        <td class="myLabelIzquierda">                                            
                                            <asp:TextBox ID="txtNombreSolicitante" runat="server" CssClass="myTexto" 
                                               Width="131px">                                                                                                                                    
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNombreSolicitante" runat="server" 
                                                ControlToValidate="txtNombreSolicitante" ErrorMessage="Nombre" 
                                                ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" colspan="2">
                                            <anthem:Button ID="btnGuardarSolicitante" runat="server" Visible="false" 
                                                CssClass="myButtonGris" onclick="btnGuardarSolicitante_Click" Text="Guardar" 
                                                ValidationGroup="1" Width="65px" />
                                            <anthem:Button ID="btnCancelarSolicitante" runat="server" Visible="false"
                                                CssClass="myButtonGris" onclick="btnCancelarSolicitante_Click" Text="Cancelar" 
                                                Width="66px" />
                                        </td>
                                    </tr>
                                </table>
                              <div class="myLabelRojo"> El solicitante externo solo se habilita cuando el efector solicitante es externo.</div>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                                    HeaderText="Debe completar los datos del solicitante" ShowMessageBox="True" 
                                    ValidationGroup="1" ShowSummary="False" />
                          
                             </div>
                         
                                <div id="tab5">
                                    <asp:Panel  ID="pnlIncidencia" runat="server" Height="310px">   
                                    
                                    <uc1:IncidenciaEdit ID="IncidenciaEdit1" runat="server" />
                                     </asp:Panel>
                                </div>
                             
                        </div>
                             
						
						
                       
                
                        </td>
					</tr>
					
					<tr>
							<td colspan="2" style="vertical-align: top" class="myLabelIzquierda" ><hr /> Observaciones Internas:  						
                                    <asp:TextBox ID="txtObservacion" runat="server"
                                                TextMode="MultiLine"  TabIndex="23" Width="800px"  Rows="2"></asp:TextBox>             </td>
						</tr>																
						
						
						
				
						
					<tr>
					<td colspan="2"><hr /></td>
					</tr>
						
					
						
						
						
					<tr>
						
						<td align="left" >
						
                                            <asp:Button ID="btnCancelar" runat="server" Text="Regresar" 
                                                onclick="btnCancelar_Click" CssClass="myButton" TabIndex="24" 
                                                CausesValidation="False" Width="120px" />                                                                                          
                                             <anthem:TextBox ID="txtCodigo" runat="server" BorderColor="White" ForeColor="White" 
                                BackColor="White" BorderStyle="Solid" BorderWidth="0px"></anthem:TextBox>
                                              <anthem:TextBox ID="txtCodigosRutina"  runat="server" BorderColor="White" 
                                ForeColor="White" BackColor="White" BorderStyle="Solid" BorderWidth="0px"></anthem:TextBox>
                                                 
                        </td>
						
						<td  align="right">
						
                                               <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="0" AccessKey="s" CausesValidation="true"
                                          ToolTip="Alt+Shift+S: Guarda Protocolo"  onclick="btnGuardar_Click" CssClass="myButton" TabIndex="24" Height="24px" /></td>
						
					</tr>
				
						
					
                     <input id="hidToken" type="hidden" runat="server" />
						
						
						
						
						</table>
                           
			</div>
  </td>
  </tr>
  </table> 
		
			  <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                     HeaderText="Debe completar los datos requeridos:" ShowMessageBox="True" 
                     ValidationGroup="0" ShowSummary="False" />			

<script language="javascript" type="text/javascript">

var contadorfilas = 0;
InicioPagina();

document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtCantidadFilas").ClientID %>').value  = contadorfilas;

function VerificaLargo (source, arguments)
    {                
    var Observacion = arguments.Value.toString().length;
 //   alert(Observacion);
    if (Observacion>4000 )
        arguments.IsValid=false;    
    else   
        arguments.IsValid=true;    //Si llego hasta aqui entonces la validación fue exitosa        
}              
        
        
        
        function InicioPagina()
        {    
            if (document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatosCargados").ClientID %>').value == "")
            {///protocolo nuevo
                CrearFila(true);         
            }
            else
            {        ///modificacion de protocolo
                AgregarCargados();              
            }
              
        }
        
        
        function NuevaFila()
        {
            Grilla = document.getElementById('Datos');
         
             
            fila = document.createElement('tr');
            fila.id = 'cod_'+contadorfilas;
            fila.name='cod_'+contadorfilas;
        	
            celda1 = document.createElement('td');
            oNroFila = document.createElement('input');
            oNroFila.type = 'text';
            oNroFila.readOnly=true;
            
            oNroFila.runat = 'server';
            oNroFila.name = 'NroFila_'+contadorfilas;
            oNroFila.id = 'NroFila_'+contadorfilas;
            //oNroFila.onfocus= function() {PasarFoco(this)}
            oNroFila.className = 'fila';
            celda1.appendChild(oNroFila);
            fila.appendChild(celda1);
        			
            celda2 = document.createElement('td');		
            oCodigo = document.createElement('input');
            
            oCodigo.type = 'text';
            oCodigo.runat = 'server';
            oCodigo.name = 'Codigo_'+contadorfilas;
            oCodigo.id = 'Codigo_'+contadorfilas;
            oCodigo.className = 'codigo';            
            oCodigo.onblur= function () {              
                CargarTarea(this);
            };

            oCodigo.setAttribute("onkeypress", "javascript:return Enter(this, event)"); 
            //oCodigo onkeypress = function(){ return Enter(this, event) };
            //oCodigo.setAttribute( = function () { alert('hola'); if (event.keyCode == 13) event.keyCode = 9; };
            //oCodigo.onchange = function () {CargarDatos()};
            celda2.appendChild(oCodigo);
    	    fila.appendChild(celda2);
        	
    	    celda3 = document.createElement('td');		
            oTarea = document.createElement('input');
            oTarea.type = 'text';
            oTarea.readOnly=true;
            oTarea.runat = 'server';
            oTarea.name = 'Tarea_'+contadorfilas;
            oTarea.id = 'Tarea_'+contadorfilas;
            oTarea.className = 'descripcion';
            oTarea.onchange = function () {CargarDatos()};
            celda3.appendChild(oTarea);
    	    fila.appendChild(celda3);
        	
    	    celda4 = document.createElement('td');		
            oDesde = document.createElement('input');
            oDesde.type = 'checkbox';
            oDesde.runat = 'server';         
            
            
            
            oDesde.name = 'Desde_'+contadorfilas;
            oDesde.id = 'Desde_'+contadorfilas;
               oDesde.alt="Sin muestra";
            
            oDesde.className = 'muestra';
            oDesde.onblur= function () {CargarDatos(); };

            celda4.appendChild(oDesde);
    	    fila.appendChild(celda4);
        	

        	        	
            celda6 = document.createElement('td');
            oBoton = document.createElement('input');
            oBoton.className='boton';
            oBoton.name= 'boton_'+contadorfilas;
            oBoton.type = 'button';
            oBoton.value= 'X';
            oBoton.onclick = function () {borrarfila(this)};
            celda6.appendChild(oBoton);
            fila.appendChild(celda6);
        	
            Grilla.appendChild(fila);
            contadorfilas = contadorfilas + 1;
        }
    
  
       function CrearFila(validar)
        {
            var valFila = contadorfilas - 1;
	        if (UltimaFilaCompleta(valFila, validar))
	        {
	   
	            NuevaFila();
    	       
    	        valFila = contadorfilas - 1;
    	        document.getElementById('NroFila_' + valFila).value = contadorfilas;
    	        
	            if (contadorfilas > 1)
	            {
	                var filaAnt = contadorfilas - 2;

	            }
    	        
	            document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtCantidadFilas").ClientID %>').value = contadorfilas;	           	            
	            document.getElementById('Codigo_' + valFila).focus();
	        }
        }
        
        
        function UltimaFilaCompleta(fila, validar)
        {
            if ((fila >= 0) && (validar))
            { 
                var cod = document.getElementById('Codigo_' + fila);
                if (cod.value == '') 
                {
       
                    return false;
                }

            }
            
            return true;
        }
        
        function CargarDatos()
        {
            var str = '';            
	        for (var i=0; i<contadorfilas; i++)
	        {	        
	            var nroFila = document.getElementById('NroFila_' + i);
	            var cod = document.getElementById('Codigo_' + i);
	            var tarea = document.getElementById('Tarea_' + i);
	            var desde = document.getElementById('Desde_' + i);	    	            		        
		        if (cod.value!='')
		         str = str + nroFila.value + '#' + cod.value + '#' + tarea.value + '#' + desde.checked + '@';
	        }	     
	         document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatos").ClientID %>').value = str;
	        
	        
        }
        
        function PasarFoco(Fila)
        {
            var fila = Fila.id.substr(8);            
            document.getElementById('Codigo_' + fila).focus();
        }
        
        function CargarTarea(codigo)
        {
            var nroFila = codigo.name.replace('Codigo_', '');
            var tarea = document.getElementById('Tarea_' + nroFila);            
            var sinMu = document.getElementById('Desde_' + nroFila); 
             	     

           var lista =     document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtTareas").ClientID %>').value ;                             
          
            if (codigo.value == '')
            {
                tarea.value = '';
            }
            else
            {            
          
   
	            if (verificarRepetidos(codigo,tarea))	            
                {             
                    var i = lista.indexOf('#' + codigo.value + '#',0);                  
                    if (i < 0)
                    {
                        codigo.value = '';
                        tarea.value = '';
                        alert('El codigo de analisis no existe o no es válido.');
                        document.getElementById('Codigo_' + nroFila).focus();
                       
                    }
                    else
                    {          
                    
                           if (!verificaDisponible (codigo))
          {
           
                        alert('El codigo ' + codigo.value +' no está disponible. Verifique con al administrador del sistema.');
                        codigo.value = '';
                        tarea.value = '';
                        document.getElementById('Codigo_' + nroFila).focus();
          }
          else                                         
          {
                        var j = lista.indexOf('@',i);
                        i = lista.indexOf('#',i+1) +1;                    
                                        
//                        alert(i);alert(j);
                        tarea.value = lista.substring(i,j).replace ('#True','').replace ('#False',''); 
                    
                        //  sinMu.checked= sinMuestra;
                         CargarDatos();
                         CrearFila(true);                
                         }
                        
                    }
                }
               
            }
        }
        
        
        function verificaDisponible (objCodigo)
        { 
            var devolver=true;
            var esnuevo=true;
            var listaDatos=document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatosCargados").ClientID %>').value ;


            var sTabla1 = listaDatos.split(';');                                    
            for (var i=0; i<(sTabla1.length); i++)
            {
                var sItem=sTabla1[i].split('#'); 
                var valorCodigo = sItem[0];
                if (valorCodigo==objCodigo.value)
                {
                    esnuevo=false; break;
                }
            }

            if (esnuevo)
            {         //no esta el codigo
                var listaItem =     document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtTareas").ClientID %>').value ;                             
                var sTabla = listaItem.split('@');                                 
                for (var i=0; i<(sTabla.length-1); i++)
                {
                    var sFila = sTabla[i].split('#');	                
                    if  (sFila[1]!="")
                    {
                        if (objCodigo.value== sFila[1])	                    
                        {
                            if (sFila[3]=="False")// campo que indica si está disponible
                            {
                                devolver=false;
                                break;
                            }
                        }
                    }	 
                }
            }
            return devolver;
        }
        
        
        function verificarRepetidos(objCodigo, objTarea)
        {
            ///Verifica si ya fue cargado en el txtDatos
            var devolver=true;
            var codigo=objCodigo.value;
            if (objTarea.value=='')
            {
                var listaExistente =document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtDatos").ClientID %>').value ;
                var cantidad=1;
                var sTabla = listaExistente.split('@');                                 
                for (var i=0; i<(sTabla.length-1); i++)
                {
                    var sFila = sTabla[i].split('#');	                
                    if  (sFila[1]!="")
                    {
                        if (codigo== sFila[1]) cantidad+=1;	                        	                     
                    }	 

                }

                if (cantidad>1)
                {
                    objCodigo.value = '';
                    objTarea.value = '';
                    alert('El código '+ codigo +' ya fue cargado. No se admiten analisis repetidos.');	
                    objCodigo.focus();	                    
                    devolver=false;       
                }
                else
                    devolver=true;
                ///Fin Verifica si ya fue cargado en el txtDatos
            }
            else
                devolver=true;
                
            return devolver;
        }
        
        
        function borrarfila(obj)
        {
            if(contadorfilas > 1)
            {
	            var delRow = obj.parentNode.parentNode;
	            var tbl = delRow.parentNode.parentNode;
	            var rIndex = delRow.sectionRowIndex;
	            Grilla = document.getElementById('Datos'); 
	            Grilla.parentNode.deleteRow(rIndex);
	            //alert('entra aca');
	            OrdenarDatos();
	            
	            contadorfilas = contadorfilas - 1;
            }
            else
            {
                
	            var cod = document.getElementById('Codigo_0').value = '';
	            var tarea = document.getElementById('Tarea_0').value = '';
	            var desde = document.getElementById('Desde_0').value = '';	           
	            	            
	            CargarDatos();
            }
        }
        
        
        
        function OrdenarDatos()
        {
            var pos = 0;
            var str = '';
            
	        for (var i=0; i<contadorfilas; i++)
	        {
	            var nroFila = document.getElementById('NroFila_' + i);
	            
	            if (nroFila != null)
	            {
	                nroFila.name = 'NroFila_' + pos;
	                nroFila.id = 'NroFila_' + pos;
	                nroFila.value = pos + 1;
	                var cod = document.getElementById('Codigo_' + i);
	                cod.name = 'Codigo_' + pos;
	                cod.id = 'Codigo_' + pos;
	                var tarea = document.getElementById('Tarea_' + i);
	                tarea.name = 'Tarea_' + pos;
	                tarea.id = 'Tarea_' + pos;
	                var desde = document.getElementById('Desde_' + i);
	                desde.name = 'Desde_' + pos;
	                desde.id = 'Desde_' + pos;
	                	                
	                pos = pos + 1;	                	               
	                str = str + nroFila.value + '#' + cod.value + '#' + tarea.value + '#' + desde.value + '@';
	            }   
	        }	        	        
	         document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatos").ClientID %>').value = str;
	      
        }
        
        function AgregarDeLaLista()
        {    
            var elvalor= document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtCodigo").ClientID %>').value;
            if (elvalor!='')
            {
                var con= contadorfilas-1;                   
	            if (UltimaFilaCompleta(con, true))	     
	            {
	            NuevaFila();
	            }       
                document.getElementById( 'Codigo_'+con).value=elvalor;          
                CargarTarea( document.getElementById( 'Codigo_'+con)); 

                OrdenarDatos();
            }
        }
        
        
        function AgregarDeLaListaRutina()
        {      
            var elvalor= document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtCodigosRutina").ClientID %>').value;
            if (elvalor!='')
            {
                var sTabla = elvalor.split(';');                                    
	            for (var i=0; i<(sTabla.length); i++)
	            {
	            
	                var valorCodigo = sTabla[i];	         
	                var con= contadorfilas-1;	            

	                document.getElementById( 'Codigo_'+con).value=valorCodigo;          
                    CargarTarea( document.getElementById( 'Codigo_'+con)); 
                
	            }
	                          
            }                    
        }
        
        
        function AgregarCargados()
        {      
    //    alert('entra');
            CrearFila(true); 
            var elvalor= document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("TxtDatosCargados").ClientID %>').value;    
           
            if (elvalor!='')
            {                           	            
                var sTabla = elvalor.split(';');                                    
	            for (var i=0; i<(sTabla.length); i++)
	            {
	                var sItem=sTabla[i].split('#'); 	                
	                
	                var valorCodigo = sItem[0];	  
	                var sinMuestra=true;
	                if  (      sItem[1]=='No') sinMuestra=true;
	                else 	   sinMuestra=false; 	                      	               
	                
	                var con= contadorfilas-1;	               
	                document.getElementById( 'Codigo_'+con).value=valorCodigo;   
	                   
                    CargarTarea( document.getElementById( 'Codigo_'+con)); 
                      var desde = document.getElementById('Desde_' + con);	    	
                      var boton= document.getElementById( 'boton_'+con); 
                               
                            		        
		         if  (sItem[2]=='True') 
	                document.getElementById( 'Codigo_'+con).className='codigoConResultado';
		          desde.checked= sinMuestra;
		        
		         
	            }
            }                    
        }
        
        

     function PreguntoImprimir() {
         if (confirm('¿Está seguro de enviar a imprimir a la impresora seleccionada?'))
             return true;
         else
             return false;
     }


    </script>
   
 
 </asp:Content>

