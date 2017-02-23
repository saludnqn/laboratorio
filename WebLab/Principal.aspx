<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="WebLab.Principal" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajx" %>
<%@ Register Src="~/PeticionList.ascx" TagPrefix="uc1" TagName="PeticionList" %>




  <asp:Content ID="content1" ContentPlaceHolderID="head" runat="server"> 
      </asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
       
    

    <link rel="stylesheet" type="text/css" href="App_Themes/default/principal/style.css" />
 
 
  <ajx:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
    EnableScriptLocalization="true">
  </ajx:toolkitscriptmanager>
 <table width="100%">

 <tr>
 <td style="vertical-align: top" width="60%"> 
 <div class="left_content"> 
 
 <asp:Panel ID="pnlTurno" runat="server">
    
                 <div class="services_block">
					<img src="App_Themes/default/principal/images/Calendar.png"  height="32px" title="" border="0" class="icon_left" width="32px" />
                  <div class="services_details">
                     <h3><a href="Turnos/TurnoList.aspx?tipo=generacion"  target="_parent" >Turnos</a></h3>
                     <div align="left">                                          
                         Genere desde esta opción los turnos programados.                                                                     
                    </div>
                  </div>
                </div>
              <br />  <br /> 
    </asp:Panel>           
                       
           <asp:Panel ID="pnlRecepcion" runat="server">
             <div class="services_block">
					<img src="App_Themes/default/principal/images/Patientfile.png" alt="" width="32" height="32" border="0" class="icon_left" title="" />
                    <div class="services_details">
                     <h3 class="Estilo2"><a href="#" class="Estilo1">
                         Recepción de Pacientes </a></h3>                         
                   <p> 
                   <asp:Label ID="lblProximoProtocolo" runat="server" 
                               Text=" Próximo Número de Protocolo Disponible:" Font-Bold="True" 
                           Font-Italic="True"></asp:Label>
                   
                           <asp:Label ID="lblProximoProtocolo1" runat="server" Font-Bold="True"   Font-Italic="True"
                               Font-Size="11pt" ForeColor="#CC3300" Text="Label"></asp:Label>
                       <asp:LinkButton ID="lnkUltimoNumeroSector" runat="server" 
                           Font-Bold="True"    Font-Italic="True"
                               Font-Size="11pt" ForeColor="#CC3300" 
                           onclick="lnkUltimoNumeroSector_Click" Visible="False">Ver</asp:LinkButton>
                      <asp:Panel ID="pnlProtocolo" runat="server" Width="100%">
                      Pacientes sin turnos
                           <a href="Protocolos/Default.aspx?idServicio=1&idUrgencia=0" target="_parent" >Laboratorio General</a> &nbsp;                                                    
                           <a href="Protocolos/Default.aspx?idServicio=3&idUrgencia=0" target="_parent" >Microbiología. </a>                        
                        </asp:Panel>   
                           <br />                   
                       <asp:Panel ID="pnlTurnoRecepcion" runat="server">
                           <a href="Turnos/TurnoList.aspx?tipo=recepcion" target="_parent">Pacientes con 
                           turnos.</a> Cargue desde acá los protocolos para los pacientes con turnos.<br />
                       </asp:Panel>
                                               </div>
                 
                
                 
              </div> 
               <br />   
               </asp:Panel>              
         
               <asp:Panel ID="pnlHojaTrabajo" runat="server">   
                 <div class="services_block">
					<img width="48px" height="48px" src="App_Themes/default/principal/images/testtubes.png" alt="" width="32" height="32" border="0" class="icon_left" title="" />
                    <div class="services_details">
                    <h3 class="Estilo1"><a href="Informes/Informe.aspx?Tipo=HojaTrabajo">Impresión de Hojas de Trabajo</a></h3>
                    <p>
                        Genere desde acá las hojas de trabajo para las áreas de su laboratorio.</p>                    
                    </div>
              </div>    <br />
                     </asp:Panel>                       
        
                <asp:Panel ID="pnlCargaResultado" runat="server">  
                <div class="services_block">
					<img  src="App_Themes/default/principal/images/dutyroster.png" alt="" width="32px" height="32px" border="0" class="icon_left" title="" />
                    <div class="services_details">
                     <h3 class="Estilo1"><a href="Resultados/ResultadoBusqueda.aspx?idServicio=1&Operacion=Carga&modo=Normal">Carga de Resultados</a></h3>
                     <p>
                         Protocolos con resultados pendientes:
                         <asp:Label ID="lblResultadoPendiente" runat="server"  
                             Font-Size="11pt" ForeColor="Red" Text="Label" Font-Bold="True"></asp:Label>
                         </p>   
                         <a href="Resultados/ResultadoBusqueda.aspx?idServicio=3&Operacion=Carga&modo=Normal" title="Acceso directo a Microbiologia">Microbiologia</a>                 
                    </div>
              </div>  <br />
			</asp:Panel> 
				
				 
			 <asp:Panel ID="pnlValidacion" runat="server">  
                <div class="services_block">
					<img  src="App_Themes/default/principal/images/validacion2.png" alt="" width="32px" height="32px" border="0" class="icon_left" title="" />
                    <div class="services_details">
                     <h3 class="Estilo1"><a href="Resultados/ResultadoBusqueda.aspx?idServicio=1&Operacion=Valida&modo=Normal">Validación de Resultados</a></h3>
                     <p>
                         Protocolos pendientes de validar (en proceso):
                         <asp:Label ID="lblValidaPendiente" runat="server"  
                             Font-Size="11pt" ForeColor="Red" Text="Label" Font-Bold="True"></asp:Label>
                         </p>                    
                              <a href="Resultados/ResultadoBusqueda.aspx?idServicio=3&Operacion=Valida&modo=Normal" title="Acceso directo a Microbiologia">Microbiología.</a>  
                              <br /> 
                    </div>
              </div> <br />
			</asp:Panel> 
				
			<asp:Panel ID="pnlImpresion" runat="server"> 
				<div class="services_block">
					<img src="App_Themes/default/principal/images/Printersettings.png" alt="" width="32" height="32" border="0" class="icon_left" title="" />
                    <div class="services_details">
                     <h3 class="Estilo1"><a href="ImpresionResult/ImprimirBusqueda.aspx?idServicio=1&modo=Normal" target="_parent">Impresión de Resultados</a></h3>
                     <p>
                         Protocolos terminados sin imprimir: <asp:Label ID="lblProtocoloPendiente" runat="server"  
                             Font-Size="11pt" ForeColor="Red" Text="Label" Font-Bold="True"></asp:Label>
                                       </p>     
                                          <a href="ImpresionResult/ImprimirBusqueda.aspx?idServicio=3&modo=Normal" title="Acceso directo a Microbiologia">Microbiología.</a>                 
                    </div>												
				
              </div>	      <br />
      	 </asp:Panel>
      		<asp:Panel ID="pnlUrgencia" runat="server">
                 <div class="services_block">
                     <img border="0" class="icon_left" height="32px" 
                         src="App_Themes/default/principal/images/urgencia.png" title="" width="32px" /><div 
                         class="services_details">
                         <h3>
                             <a 
                                 href="Protocolos/Default.aspx?idServicio=1&idUrgencia=1">Modulo de Urgencias</a>
                                
                                 </h3>
                         <div align="left">
                             Desde acá podrá cargar el protocolo, sus resultados, validar e imprimir de forma 
                             rápida en un solo paso.</div>
                     </div>
                 </div>
                 <br />
     </asp:Panel>
           <br />
      <asp:Panel ID="pnlResultados" runat="server" >
                 <div class="services_block">
                     <img border="0" class="icon_left" height="32px" 
                         src="App_Themes/default/principal/images/resultados.png" title="" width="32px" /><div 
                         class="services_details">
                         <h3>
                             <a 
                                 href="Informes/historiaClinicafiltro.aspx?Tipo=PacienteValidado">Consulta de Resultados</a></h3>
                         <div align="left">
                             Historial de Resultados: Consulte para un paciente los resultados obtenidos en el laboratorio.<hr /> <b>
                     
                          <h3>   Para consultar resultados en la RED PROVINCIAL DE LABORATORIOS hacer clic <a href="http://www.saludnqn.gob.ar/Sips/" target="_blank">aquí</a></h3></b>
                            
                             &nbsp; &nbsp; &nbsp; <em>Si tiene algun inconveniente para acceder a este link consultar con su soporte informático local.</em>
                             </div>
                     </div>
                 </div>
                 
                 <br />
               
     </asp:Panel> 
     <%--  <asp:Panel ID="pnlResultadoAnalisis" runat="server" Visible="false" >
                 <div class="services_block">
                     <img border="0" class="icon_left" height="32px" 
                         src="App_Themes/default/principal/images/resultados.png" title="" width="32px" /><div 
                         class="services_details">
                         <h3>
                             <a 
                                 href="Informes/historiaClinicafiltro.aspx?Tipo=Analisis">Historial Resultados por Analisis</a></h3>
                         <div align="left">
                             Consulte para un paciente los resultados obtenidos para un análisis o practica de laboratorio.
                             </div>
                     </div>
                 </div>
                 </asp:Panel>    --%>
               
                   <asp:Panel ID="pnlNuevoUsuario" runat="server" >  <br /><br />
                 <div class="services_block">
                     <img border="0" class="icon_left" height="32px" 
                         src="App_Themes/default/principal/images/userLogin.jpg" title="" width="32px" /><div 
                         class="services_details">
                         <h3>
                             <a 
                                 href="login.aspx">Nuevo Usuario</a></h3>
                         <div align="left">
                             Cierra sesión actual y permite abrir nueva sesión de usuario.
                             </div>
                     </div>
                 </div>
                       <br />
                 </asp:Panel>  
     <asp:Panel ID="pnlSivila" runat="server">
      <div class="services_block">
            <img border="0" class="icon_left" 
                         src="App_Themes/default//images/snvs.PNG" title=""  />
                 <div 
                         class="services_details">
                         <h3>
                             <a 
                                 href="Estadisticas/ReportePorResultadoSivila.aspx">Exportación de datos para SIVILA</a></h3>
                         <div align="left">
                           Generación de archivos automaticos para el envío de datos a SIVILA.
                             </div>
                     </div>
                 </div> 
         </asp:Panel>
                  </div>
 </td>

     

 <td style="vertical-align: top" width="20%" align="right"> 
    	 
     <uc1:PeticionList runat="server" id="PeticionList1" />
                <%--<div id="divPeticion" visible="false" runat="server"  style="width:310px;height:300px;overflow:scroll;overflow-x:hidden;" 
             align="right" > 
                  
             <br />

                        <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="UpdateTimer" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <asp:Timer runat="server" ID="UpdateTimer" Interval="3000" OnTick="UpdateTimer_Tick" />
                    <asp:Label ID="lblActualizacion" CssClass="myLabelLitlle" runat="server" ></asp:Label>     
                    <br /> 

            
                <asp:DataList ID="DataList2" runat="server" onitemdatabound="DataList2_ItemDataBound"                                                 
width="290px" CellPadding="4" ForeColor="#333333" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
>
                                               
                                            
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                              
                                                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                               
                                            
<HeaderTemplate>
   PETICIONES GUARDIA
   </HeaderTemplate>



                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                <ItemTemplate>
                                                    <table width="100%">
                                                        <tr>                                                           
                                                            <td  class="myLabelIzquierda"> 
                                                        PETICION NRO.&nbsp;         <%# DataBinder.Eval(Container.DataItem, "idPeticion")%> 
                                                            <br />                                                               
                                                              <b><%# DataBinder.Eval(Container.DataItem, "fechaHoraRegistro")%></b>    &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                                                  <asp:HyperLink ID="hplPeticionEdit"  NavigateUrl=<%# DataBinder.Eval(Container.DataItem, "idPeticion")%>  runat="server"><b>Recepcionar</b></asp:HyperLink>                                                        
                                                            </td>
                                                        </tr>
                                                        <tr>                                                        
                                                            <td >
                                                           <b style="color: #FF0000; font-weight: bold; text-transform: uppercase"><%# DataBinder.Eval(Container.DataItem, "paciente")%>  </b></td>                                                             
                                                            
                                                        </tr> 
                                                          <tr>                                                        
                                                            <td >
                                                           <b>  DU:</b> <%# DataBinder.Eval(Container.DataItem, "DU")%>  </td>                                                             
                                                            
                                                        </tr>                                                        
                                                        <tr>
                                                        
                                                            <td >
                                                                
                                                           
                                                              <b>  De:</b> <%# DataBinder.Eval(Container.DataItem, "origen")%> 
                                                            </td>
                                                            
                                                        </tr>
                                                                                                                                                          
                                                    </table>
                                                </ItemTemplate>

                                            </asp:DataList>
                                            </ContentTemplate>
</asp:UpdatePanel>
                                            </div>--%>                             
    	 

         <br /> 


<div id="mensajeria" runat="server"  style="width:305px;height:400px;overflow:scroll;overflow-x:hidden;" 
             align="right" > 
             <asp:ImageButton ID="imgAgregarMensaje" runat="server"  
                 ImageUrl="~/App_Themes/default/images/svn_added.png" 
                 onclick="imgAgregarMensaje_Click" ToolTip="Agregar Mensaje" />
             <br />
                                            <asp:DataList ID="DataList1" runat="server"
  onitemdatabound="DataList1_ItemDataBound"                                                 
width="290px" CellPadding="4" ForeColor="#333333" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
>
                                               
                                            
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                              
                                                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                               
                                            
<HeaderTemplate>
    Mensajes Internos
   </HeaderTemplate>



                                                <HeaderStyle BackColor="Aquamarine" Font-Bold="True" ForeColor="#333333" />
                                                <ItemTemplate>
                                                    <table width="100%">
                                                        <tr>                                                           
                                                            <td  class="myLabelIzquierda">                                                                
                                                              <b><%# DataBinder.Eval(Container.DataItem, "fechaHoraRegistro")%></b>    &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                                                  <asp:HyperLink ID="hplMensajeEdit" NavigateUrl=<%# DataBinder.Eval(Container.DataItem, "idMensaje")%>  runat="server"><b>Eliminar</b></asp:HyperLink>                                                        
                                                            </td>
                                                        </tr>
                                                        <tr>                                                        
                                                            <td >
                                                           <b>   De:</b><%# DataBinder.Eval(Container.DataItem, "remitente")%></td>                                                             
                                                            
                                                        </tr>                                                       
                                                        <tr>
                                                        
                                                            <td >
                                                                
                                                           
                                                              <b>  Para:</b><b style="color: #FF0000; font-weight: bold"> <%# DataBinder.Eval(Container.DataItem, "destinatario")%> </b>
                                                            </td>
                                                            
                                                        </tr>
                                                               <tr>
                                                        
                                                            <td >
                                                                
                                                           
                                                                <b> Mensaje:</b><p> <%# DataBinder.Eval(Container.DataItem, "mensaje")%> </p>
                                                            </td>
                                                            
                                                        </tr>                                                                                                    
                                                    </table>
                                                </ItemTemplate>

                                            </asp:DataList></div>
                                             <br />
                                            </td>
 
 </tr>
 </table>
 
 <br />&nbsp;
 
  
<br />&nbsp;
</div>
    
 
 <div class="myLabelIzquierda"> 
 <table>
 <tr>
 <td rowspan="2"> &nbsp;&nbsp;<a href="install_flashplayer11x32_mssa_aih.exe"><img title="Haga clic aquí para descargar instalador" src="App_Themes/default/images/adobeFp.jpg" /></a></td>
 <td>Para acceder a gráficos generados por este sistema requiere instalar Adobe Flash Player.</td>
 </tr>
 <tr>
 <td colspan="2"><a href="install_flashplayer11x32_mssa_aih.exe">Descargar</a>

     </td>
 </tr>
 </table>
 
<br />&nbsp;
</div>
    
                                                                                       
    </asp:Content>


