<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParametrosEdit.aspx.cs" Inherits="WebLab.ParametrosEdit" MasterPageFile="~/Site1.Master" %>
   <%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>



<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
 <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<link href="script/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" /> 
<link href="App_Themes/default/style.css" rel="stylesheet" type="text/css" /> 
<script src="script/jquery.min.js" type="text/javascript"></script>  
<script src="script/jquery-ui.min.js" type="text/javascript"></script> 
<script type="text/javascript" src="script/ValidaFecha.js"></script>

<script type="text/javascript">  

$(function() {

$("#tabContainer").tabs();
var currTab = $("#<%= HFCurrTabIndex.ClientID %>").val();

$("#tabContainer").tabs({ selected: currTab });
});


</script> 
    
    <style type="text/css">
        .style1
        {
            font-size: 9pt;
            font-family: Arial, Helvetica, sans-serif;
            color: #333333;
            font-weight: normal;
            text-transform: inherit;
            width: 17px;
            border-style: none;
            background-color: #FFFFFF;
        }
        .style3
        {
            font-size: 9pt;
            font-family: Arial, Helvetica, sans-serif;
            color: #333333;
            font-weight: normal;
            text-transform: inherit;
            width: 41%;
            border-style: none;
            background-color: #FFFFFF;
        }
        .style5
        {
            width: 601px;
        }
        .style6
        {
            font-size: 10pt;
            font-family: Calibri;
            color: #333333;
            font-weight: bold;
            width: 325px;
            background-color: #FFFFFF;
        }
        .style7
        {
            font-size: 9pt;
            font-family: Arial, Helvetica, sans-serif;
            color: #333333;
            font-weight: normal;
            text-transform: inherit;
            width: 80px;
            border-style: none;
            background-color: #FFFFFF;
        }
        .style8
        {
            font-size: 9pt;
            font-family: Arial, Helvetica, sans-serif;
            color: #333333;
            font-weight: normal;
            text-transform: inherit;
            width: 266px;
            border-style: none;
            background-color: #FFFFFF;
        }
        .style9
        {
            width: 594px;
        }
        .style10
        {
            font-size: 10pt;
            font-family: Calibri;
            color: #333333;
            font-weight: bold;
            width: 295px;
            background-color: #FFFFFF;
        }
        .style11
        {
            width: 289px;
        }
        .style12
        {
            font-size: 10pt;
            font-family: Calibri;
            color: #333333;
            font-weight: bold;
            width: 322px;
            background-color: #FFFFFF;
        }
        .style13
        {
            width: 262px;
        }
    </style>
    
</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
   <br />   &nbsp;
    
    <asp:HiddenField runat="server" ID="HFCurrTabIndex"   /> 
    <table style="width:1000px;" align="center" >
        <tr>
            <td>
              <b class="mytituloTabla">CONFIGURACION DEL SISTEMA</b></td>
            
           <td align="right"> <a href="Help/Documentos/Parámetros del Sistema.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="App_Themes/default/images/information.png" /></a></td>
            
        </tr>
           
        <tr>
            <td colspan="2">
                  <hr class="hrTitulo" /></td>
            
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblZona" runat="server" Text="" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt"></asp:Label>
                &nbsp;-
                <asp:Label ID="lblEfector" runat="server" Text="" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt"></asp:Label></td>
            
        </tr>
        <tr>
            <td colspan="2">
                <hr /></td>
            
        </tr>
         <tr>
            <td colspan="2">
            
             <div id="tabContainer" style="border: 0px solid #C0C0C0">  
             <ul class="myLabel">
                <li><a href="#tab0">General</a></li>   
                <li><a href="#tab1">Protocolos</a></li>    
                <li><a href="#tab3">Turnos</a></li>
                <li><a href="#tab2">Calendario</a></li>        
                <li><a href="#tab4">Carga/Valid.Resultados</a></li>
                <li><a href="#tab5">Laboratorio General</a></li>
                <li><a href="#tab6">Microbiología</a></li>
                <li><a href="#tab8">Imagen de Impresión</a></li>
                <li><a href="#tab7">Impresoras</a></li>
                <li><a href="#tab9">Codigo de Barras</a></li>                 
            </ul>

 <div id="tab0" class="tab_content" style="border: 1px solid #C0C0C0">
     <table style="width:1000px;">
         <tr>
             <td   style="vertical-align: top" colspan="2" class="myLabelIzquierdaGde">
                 Configuración de Accesos Directos de Pantalla Principal</td>
             <td class="style3">
                 &nbsp;
             </td>
         </tr>
         <tr>
             <td class="myLabelDerechaGde" style="vertical-align: top" colspan="2">
                <asp:CheckBoxList ID="chkAccesoPrincipal" runat="server">
                     <asp:ListItem Value="0">Turnos</asp:ListItem>
                     <asp:ListItem Value="1">Recepción</asp:ListItem>
                     <asp:ListItem Value="2">Impresion de Hojas de Trabajo</asp:ListItem>
                     <asp:ListItem Value="3">Carga de Resultados</asp:ListItem>
                     <asp:ListItem Value="4">Validacion</asp:ListItem>
                     <asp:ListItem Value="5">Impresión de Resultados</asp:ListItem>
                     <asp:ListItem Value="6">Modulo de Urgencia</asp:ListItem>
                     <asp:ListItem Value="7">Consulta de Resultados</asp:ListItem>
                 </asp:CheckBoxList>
         
             </td>
             <td class="style3">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="myLabel" colspan="2">
                 </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="myLabelIzquierda" colspan="2">
                 La visualización de los accesos directos queda sujeta a los permisos del 
                 usuario.&nbsp;              </td>
             <td>
                 &nbsp;
             </td>
         </tr>
         <tr>
             <td   style="vertical-align: top" colspan="2">
                 <hr /></td>
             <td>
                 &nbsp;</td>
         </tr>
         <%--       <tr>
                                        <td class="myLabelIzquierda">
                                            Reutilizar Números dados de baja:<td class="myLabelDerechaGde">
                                            
                                            <asp:DropDownList ID="ddlUtilizarNumeroEliminado" runat="server" 
                                                CssClass="myLabelDerechaGde" 
                                                onselectedindexchanged="ddlRecordarOrigenProtocolo_SelectedIndexChanged">
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>--%>
         </table>
 </div>
    <div id="tab1" class="tab_content" style="border: 1px solid #C0C0C0">
                                <table style="width:1000px;">
                                    <tr>
                                        <td style="vertical-align: top">
                                           <b  class="myLabelIzquierda"> Tipo de Numeración de Protocolos:</b>
                                               </td>
                                            <td class="myLabelDerechaGde">
                                            <asp:RadioButtonList ID="rdbTipoNumeracionProtocolo" runat="server" 
                                                CssClass="myLabelDerechaGde" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Autonumérica única</asp:ListItem>
                                                <asp:ListItem Value="1">Por día</asp:ListItem>
                                                <asp:ListItem Value="2">Por servicio/sector</asp:ListItem>
                                                <asp:ListItem Value="3">Autonumérica diferenciada por Tipo de Servicio</asp:ListItem>
                                            </asp:RadioButtonList>
                                             
                                        </td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabel" colspan="2">
                                     <asp:Label ID="lblMensajeNumeracion" runat="server" Font-Italic="False" 
                                                ForeColor="#CC3300" 
                                                Text="No es posible modificar el tipo de numeración; ya que hay protocolos cargados" 
                                                Visible="False" CssClass="myLabelDerechaGde"></asp:Label></tr>
                        
                             <%-- <tr>
                                        <td class="myLabelIzquierdaGde">
                                            Imprimir fecha y hora de impresión:</td>
                                        <td>
                                            
                                            <asp:DropDownList ID="ddlImprimirFechaHora" runat="server" 
                                                CssClass="myLabelDerechaGde">
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>--%>
                        
                                    <tr>
                                        <td class="myLabelIzquierdaGde" colspan="2">
                                            <hr /></td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierdaGde" colspan="2">
                                            Carga de Protocolos</td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierda">
                                            Recordar el último Origen cargado:</td>
                                        <td class="myLabelIzquierda">
                                            
                                            <asp:DropDownList ID="ddlRecordarOrigenProtocolo" runat="server" 
                                                
                                                onselectedindexchanged="ddlRecordarOrigenProtocolo_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierda">
                                            Recordar el último Sector cargado:</td>
                                        <td class="myLabelIzquierda">
                                            
                                            <asp:DropDownList ID="ddlRecordarSectorProtocolo" runat="server" 
                                               
                                                onselectedindexchanged="ddlRecordarOrigenProtocolo_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierda">
                                            Fecha de Orden:</td>
                                        <td class="myLabelIzquierda">
                                            
                                            <asp:DropDownList ID="ddlFechaOrden" runat="server"                                             
                                                >
                                                <asp:ListItem Value="0">Sin Dato</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="1">Fecha Actual</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierda">
                                            Fecha Toma Muestra:</td>
                                        <td class="myLabelIzquierda">
                                            
                                            <asp:DropDownList ID="ddlFechaTomaMuestra" runat="server" >
                                                <asp:ListItem Value="0" >Sin Dato</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">Fecha Actual</asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierda">
                                                                                        Origen por defecto:</td>
                                        <td class="myLabelDerechaGde">
                                            
         <asp:DropDownList ID="ddlOrigenUrgencia" runat="server">
         </asp:DropDownList>
                                        &nbsp;(Solo para&nbsp; el Módulo Urgencias)</td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierda">
                                            Sector por defecto:</td>
                                        <td class="myLabelDerechaGde">
                                            
         <asp:DropDownList ID="ddlSectorUrgencia" runat="server">
         </asp:DropDownList>
                                        &nbsp;(Solo para el Módulo Urgencias)</td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierdaGde" colspan="2">
                                           <hr /></td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierda">
                                            Habilita modificación de Protocolos Terminados:</td>
                                        <td class="myLabelIzquierda">
                                            
                                            <asp:DropDownList ID="ddlModificaProtocoloTerminado" runat="server" 
                                              
                                                onselectedindexchanged="ddlRecordarOrigenProtocolo_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierda">
                                            Habilita eliminación de Protocolos:</td>
                                        <td class="myLabelIzquierda">
                                            
                                            <asp:DropDownList ID="ddlEliminaProtocoloTerminado" runat="server" 
                                               
                                                onselectedindexchanged="ddlRecordarOrigenProtocolo_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierdaGde" colspan="2">
                                           <hr /></td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierda">
                                            Tamaño máximo de la lista de Protocolos:</td>
                                        <td class="myLabelIzquierda">
                                            
                                            Mostrar
                                            
                                            <asp:DropDownList ID="ddlPaginadoProtocolo" runat="server" 
                                             
                                                onselectedindexchanged="ddlRecordarOrigenProtocolo_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="25">25</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        &nbsp;protocolos por página</td>
                                    </tr>
                        
                                    <tr>
                                        <td class="myLabelIzquierdaGde" colspan="2">
                                           <hr /></td>
                                    </tr>
                        
                      <%--  <tr>
                                        <td class="myLabel" style="width: 328px">
                                           </td>
                                        <td class="myLabelDerechaGde" style="width: 652px">
                                            &nbsp;</td>
                                    </tr>--%>
                        
                                   
                                   
                                
                                   
                                   
                                    <tr>
                                        <td class="myLabelIzquierda">
                                            Formato por defecto del
                                            Listado Ordenado: </td>
                                        <td>
                                            
                                            <asp:RadioButtonList ID="rdbTipoListaProtocolo" runat="server" CssClass="myLabelDerechaGde"
                                                RepeatDirection="Horizontal" >
                                                <asp:ListItem Value="0" Selected="True">Formato Reducido (Nombre)</asp:ListItem>
                                                    <asp:ListItem Value="2">Formato Reducido (Codigo)</asp:ListItem>
                                              <%--  <asp:ListItem Value="1">Formato Extendido</asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                        
                                   
                                   
                                    <tr>
                                        <td class="myLabelIzquierdaGde" colspan="2">
                                            <hr /></td>
                                    </tr>
                        
                                   
                                   
                                    </table>
                            </div>
                       
    <div id="tab3" class="tab_content" style="border: 1px solid #C0C0C0">
                              <table  >
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top">
                                            ¿Trabaja con turnos:?</td>
                                        <td class="myLabelIzquierda" style="vertical-align: top" class="style18" rowspan="2">
                                            
                                            <asp:DropDownList ID="ddlTurno" runat="server" >
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top">
                                            (Habilita en la opcion de menu el acceso a generar turnos)</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top" colspan="2">
                                           <hr /></td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top">
                                            Habilita generación de comprobante de Turnos:</td>
                                        <td class="myLabelIzquierda" style="vertical-align: top" rowspan="3">
                                            <asp:DropDownList ID="ddlTurnoComprobante" runat="server" >
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top">
                                            (Muestra la opción de imprimir un comprobante del turno <br /> despues de guardado el 
                                            mismo)</td>
                                    </tr>
                                    <tr>
                                        <td class="style17" style="vertical-align: top">
                                           </td>
                                    </tr>
                                   <tr>
                                        <td class="myLabel" style="vertical-align: top" colspan="2">
                                           <hr /></td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda">
                                      <img src="App_Themes/default/images/new.png" />      Envía mensaje de texto al momento de cancelar turno:</td>
                                           <td class="myLabelIzquierda" style="vertical-align: top">
                                                   <asp:DropDownList ID="ddlSmsCancelaTurno" runat="server" >
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td class="myLabel" style="vertical-align: top" colspan="2">
                                           <hr /></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <asp:Button ID="btnFeriado" runat="server"  OnClientClick="editFeriado(); return false;" Text="Configurar Feriados" 
                                                 ToolTip="Configurar Feriados" Width="200px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                       
    <div id="tab2" class="tab_content" style="border: 1px solid #C0C0C0">
                               <table style="width:1000px;">
                                    <tr class="myLabelIzquierda">
                                      <td class="myLabelIzquierda">
                                            Días para la entrega de Resultados:</td>
                                        <td>
                                            <anthem:RadioButtonList ID="rdbDiasEspera" runat="server" 
                                                onselectedindexchanged="rdbDiasEspera_SelectedIndexChanged" 
                                                RepeatDirection="Horizontal" Width="100%" CssClass="myLabel">
                                                <Items>
                                                    <asp:ListItem Selected="True" Value="0">Calcular segun la duración de los 
                                                    analisis</asp:ListItem>
                                                    <asp:ListItem Value="1">Valor Predeterminado</asp:ListItem>
                                                </Items>
                                            </anthem:RadioButtonList>
                                       
                                    </tr>
                                   <tr>
                                        <td class="myLabelIzquierda">
                                            Días de espera predeterminado:</td>
                                        <td class="myLabelIzquierda">
                                            <input id="txtDiasEntrega" runat="server" class="myTexto" maxlength="5" 
                                                onblur="valNumero(this)" size="5" type="text" /><anthem:RequiredFieldValidator 
                                                ID="rfvDiasEspera" runat="server" ControlToValidate="rdbDiasEspera" 
                                                Enabled="False" ErrorMessage="*">*</anthem:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top" colspan="2">
                                           <hr /></td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top">
                                            Calendario de Entrega:</td>
                                        <td>
                            <anthem:RadioButtonList ID="rdbTipoDias" runat="server" AutoCallBack="True" 
                                onselectedindexchanged="rdbTipoDias_SelectedIndexChanged" 
                                RepeatDirection="Horizontal" CssClass="myLabelDerechaGde">
                                <Items>
                                    <asp:ListItem Value="0">Todos los días</asp:ListItem>
                                    <asp:ListItem Value="1">Días habiles</asp:ListItem>
                                </Items>
                            </anthem:RadioButtonList>
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top">
                                            &nbsp;</td>
                                        <td>
                            <anthem:CheckBoxList ID="cklDias" runat="server" RepeatColumns="5" 
                                RepeatDirection="Horizontal" CssClass="myLabelDerechaGde">
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
                                        
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        
                        
                        
                        
                        
                 
    <div id="tab4" class="tab_content" style="border: 1px solid #C0C0C0">
                             <table style="width:1000px;">
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 368px" >
                                            Carga de Resultados por Defecto:</td>
                                        <td style="width: 4px">
                                            &nbsp;</td>
                                        <td class="myLabelIzquierda" style="width: 603px">
                                            <asp:RadioButtonList ID="rdbCargaResultados" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="0">Por lista de Protocolos</asp:ListItem>
                                                <asp:ListItem Value="1">Por Hoja de Trabajo</asp:ListItem>
                                                <asp:ListItem Value="2">Por Análisis</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="width: 368px" >
                                            (Aplicable a los filtros de busqueda para la carga de resultados)</td>
                                        <td style="width: 4px">
                                            &nbsp;</td>
                                        <td style="width: 603px">
                                            &nbsp;</td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierdaGde" colspan="3" >
                                            <hr /></td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 368px" >
                                            Orden de carga/validación de resultados:</td>
                                        <td style="width: 4px">
                                            
                                            &nbsp;</td>
                                        <td class="myLabelIzquierda" rowspan="2" style="width: 603px">
                                            
                                              <asp:RadioButtonList ID="rdbOrdenCargaResultados" 
                                                  runat="server">
                                                <asp:ListItem Selected="True" Value="0">Orden de carga en la recepcion del 
                                                  paciente</asp:ListItem>
                                                <asp:ListItem Value="1">Orden de impresion de resultados</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="width: 368px" >
                                            (Solo aplicable para la carga y validación de resultados
                                            <br />
                                             por Lista de Protocolos)</td>
                                        <td style="width: 4px">
                                            
                                            &nbsp;</td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" colspan="3" >
                                           <hr /></td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 368px" >
                                            ¿Aplicar Fórmula por Defecto?:</td>
                                        <td style="width: 4px">
                                            
                                            &nbsp;</td>
                                        <td class="myLabelIzquierda" style="width: 603px">
                                            
                                            <asp:DropDownList ID="ddlAplicaFormula" runat="server" >
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="width: 368px" >
                                            (Aplicable solo para la carga y validación de resultados por Lista de Protocolos.
                                            Despues de guardar aplica las formulas)</td>
                                        <td style="width: 4px">
                                            
                                            &nbsp;</td>
                                        <td style="width: 603px">
                                            
                                            &nbsp;</td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" colspan="3" >
                                           <hr /></td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 368px" >
                                            <b class="mytituloRojo">Firma Electrónica:</b> <br />¿Requiere nueva autenticación para validar?</td>
                                        <td style="width: 4px">
                                            
                                            &nbsp;</td>
                                        <td class="myLabelIzquierda" style="width: 603px">
                                            
                                            <asp:DropDownList ID="ddlAutenticaValidacion" runat="server" >
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="width: 368px" >
                                            (Solicitará nuevamente que el usuario se autentique<br />
&nbsp;al ingresar el módulo de validación)</td>
                                        <td style="width: 4px">
                                            
                                            &nbsp;</td>
                                        <td style="width: 603px">
                                            
                                            &nbsp;</td>
                                        <td style="width: 7px"  >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td   style="vertical-align: top" colspan="4">
                                           <hr /></td>
                                    </tr>
                               
                                    </table>
                            </div>
                        
                        
                        
                   
    <div id="tab5" class="tab_content" style="border: 1px solid #C0C0C0">
    <table width="100%">
                                 
                  
                 
               </table>
                                <table style="width:100%;">
                                    <tr>
                                        <td class="myLabelIzquierda" colspan="2">
                                           Comprobante para el Paciente<hr /></td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Habilita generación de comprobante de Protocolo:</td>
                                        <td class="myLabelIzquierda" style="width: 652px">
                                            <asp:DropDownList ID="ddlProtocoloComprobante" runat="server" 
                                               >
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList> 
                                         
                                        </td>
                                    </tr>
                                    <%--  <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top" colspan="3">
                                         </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                            Logo de Impresión de Resultados:</td>
                                        <td class="myLabelIzquierda" style="width: 652px">
                                            <asp:FileUpload ID="fupLogo" runat="server" CssClass="myTexto" Width="250px" 
                                                ondatabinding="fupLogo_DataBinding" />                                                                                      
                                            <asp:CheckBox ID="chkBorrarImagen" runat="server" CssClass="myLabelDerechaGde" 
                                                Text="Borrar Imagen" />
                                        </td>
                                        <td   rowspan="3" class="style7" style="width: 6px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            (La imagen deberá ser de formato PNG.<br />
                                            El tamaño recomendado de la imagen es de 2.54x2.54 cm.)</td>
                                        <td style="width: 652px">
                                          <div  style="width:150px;height:100pt;overflow:scroll;border:1px solid #808080;" title="Imagen Logo"> 
                                          
                                            <asp:Image ID="Image1" runat="server" ImageUrl="" Visible="false" />
                                            </div>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Texto Adicional al Pie:</td>
                                        <td class="myLabelIzquierda" style="width: 652px">
                                            <asp:TextBox ID="txtTextoAdicionalComprobante" runat="server"    CssClass="myTexto" 
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                 
                                    
                                    <tr>
                                        <td  colspan="2">
                                           <br /> &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" colspan="2">
                                            Impresión de Resultados<hr /></td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Encabezado en linea 1: </td>
                                        <td class="myLabelIzquierda" style="width: 652px">
                                            <asp:TextBox ID="txtEncabezado1" runat="server" CssClass="myTexto" 
                                                Width="300px" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Encabezado en linea 2:</td>
                                        <td class="myLabelIzquierda" style="width: 652px">
                                            <asp:TextBox ID="txtEncabezado2" runat="server" CssClass="myTexto" 
                                                Width="300px" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Encabezado en linea 3:</td>
                                        <td class="myLabelIzquierda" style="width: 652px">
                                            <asp:TextBox ID="txtEncabezado3" runat="server" CssClass="myTexto" 
                                                Width="300px" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                       
                                  
                                           
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                            Tamaño de la Hoja de Resultados:</td> 
                                        <td class="myLabelIzquierda">
                                            
                                            <asp:DropDownList ID="ddlTipoHojaImpresionResultados" runat="server"  >
                                              <asp:ListItem Value="A4">A4</asp:ListItem>
                                                <asp:ListItem Value="A5">A5</asp:ListItem>
                                            </asp:DropDownList>  
                                                        </td>
                                    </tr>
                                   
                                       <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                            Impresión de Resultados:</td>
                                        <td rowspan="2" style="vertical-align: top">
                                          <asp:RadioButtonList CssClass="myLabelDerechaGde" ID="rdbTipoImpresionResultado" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Imprimir en Hojas Separadas</asp:ListItem>
                                                <asp:ListItem Value="0">Imprimir a continuación</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                      
                                       <tr>
                                  <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            Opción solo aplicable para Tamaño de Papel A4</td>
                                            </tr>
                                    <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top" colspan="2">
                                           </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Datos del Protocolo a imprimir:</td>
                                        <td  rowspan="2" class="style8" style="width: 652px">
                                            <asp:CheckBoxList ID="chkDatosProtocoloImprimir" runat="server" CssClass="myLabelDerechaGde" 
                                                RepeatColumns="3">
                                                <asp:ListItem Value="0">Nro. Registro</asp:ListItem>                                                                                           
                                                <asp:ListItem Value="1">Fecha de Entrega</asp:ListItem> 
                                                    <asp:ListItem Value="2">Sector</asp:ListItem>
                                                <asp:ListItem Value="3">Solicitante</asp:ListItem>
                                                <asp:ListItem Value="4">Origen</asp:ListItem>
                                                <asp:ListItem Value="5">Prioridad</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            (Seleccionar los datos opcionales a imprimir del protocolo 
                                            <br />
                                            en la hoja de resultados)</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top" colspan="2">
                                           </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                            Datos del Paciente a imprimir:</td>
                                        <td rowspan="2">
                                            <asp:CheckBoxList ID="chkDatosPacienteImprimir" runat="server" CssClass="myLabelDerechaGde" 
                                                RepeatColumns="4">
                                                <asp:ListItem Enabled="False" Selected="True" Value="0">Apellido y Nombres</asp:ListItem>                                                                                           
                                                <asp:ListItem Value="1">DNI</asp:ListItem> 
                                                    <asp:ListItem Value="2" Enabled="False">Nro. HC (sin uso)</asp:ListItem>
                                                <asp:ListItem Value="3">Edad</asp:ListItem>
                                                <asp:ListItem Value="4">Fecha Nacimiento</asp:ListItem>
                                                <asp:ListItem Value="5">Sexo</asp:ListItem>
                                                <asp:ListItem Value="6">Domicilio</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            (Seleccionar los datos opcionales a imprimir del paciente 
                                            <br />
                                            en la hoja de resultados)</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top" colspan="2">
                                          </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                          Firma Electrónica:</td>
                                        <td class="myLabelIzquierda">
                                            
                                            <asp:DropDownList ID="ddlImprimePieResultados" runat="server" 
                                            >
                                                <asp:ListItem Selected="True" Value="0">No usar</asp:ListItem>
                                               
                                                <asp:ListItem Value="2">Imprimir por cada determinación</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            (Se imprimirá el apellido y nombre de la/s persona/s que validaron los resultados)</td>
                                        <td class="myLabel" style="vertical-align: top;">
                                            *Imprimir por cada determinación: solo disponible en formato A4</td>
                                    </tr>
                                    <%--     <tr>
                                        <td class="myLabel" style="width: 328px">
                                            (Habilita la opcion de imprimir el protocolo 
                                            <br />
                                            despues de guardado el mismo)</td>
                                        <td class="myLabelDerechaGde" style="width: 652px">
                                            &nbsp;</td>
                                    </tr>--%>
                            
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                          ¿Activa alerta Petición Electrónica?:</td>
                                        <td class="myLabel" style="vertical-align: top;">
                                            <asp:DropDownList ID="ddlPeticionElectronica" runat="server" 
                                            >
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                                              
                                </table>
                                
                                
                            </div>
            <div id="tab6" class="tab_content" style="border: 1px solid #C0C0C0">
            
            <table style="width:100%;">
               
                    
               </table>
          
                                <table style="width:100%;">
                                    <tr>
                                        <td class="myLabelIzquierda" colspan="2">
                                           Comprobante para el Paciente<hr /></td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Habilita generación de comprobante de Protocolo:</td>
                                        <td class="myLabelIzquierda" style="width: 652px">
                                            <asp:DropDownList ID="ddlProtocoloComprobanteMicrobiologia" runat="server" 
                                            >
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <%--  <asp:ListItem Value="2">Imprimir por cada determinación</asp:ListItem>--%>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Texto Adicional al Pie:</td>
                                        <td class="myLabelIzquierda"  style="width: 652px">
                                            <asp:TextBox ID="txtTextoAdicionalComprobanteMicrobiologia" runat="server"  CssClass="myTexto" 
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                      
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            &nbsp;</td>
                                        <td style="width: 652px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" colspan="2">
                                            Impresión de Resultados<hr /></td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Encabezado en linea 1: </td>
                                        <td class="myLabelIzquierda" style="width: 652px">
                                            <asp:TextBox ID="txtEncabezado1Microbiologia" runat="server"  CssClass="myTexto" 
                                                Width="300px" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Encabezado en linea 2:</td>
                                        <td class="myLabelIzquierda"  style="width: 652px">
                                            <asp:TextBox ID="txtEncabezado2Microbiologia" runat="server" CssClass="myTexto" 
                                                Width="300px" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Encabezado en linea 3:</td>
                                        <td class="myLabelIzquierda"  style="width: 652px">
                                            <asp:TextBox ID="txtEncabezado3Microbiologia" runat="server" CssClass="myTexto" 
                                                Width="300px" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierdaGde" colspan="3">
                                          </td>
                                  
                                            <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                            Tamaño de la Hoja de Resultados:</td> 
                                        <td colspan="2" class="myLabelIzquierda">
                                            
                                            <asp:DropDownList ID="ddlTipoHojaImpresionResultadosMicrobiologia" runat="server" 
                                             >
                                                <asp:ListItem Value="A4">A4</asp:ListItem>
                                                <asp:ListItem Value="A5">A5</asp:ListItem>
                                            </asp:DropDownList>  
                                                        </td>
                                    </tr>
                                      </tr>
                                       <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                            Impresión de Resultados:</td>
                                        <td colspan="2" rowspan="2" style="vertical-align: top">
                                          <asp:RadioButtonList CssClass="myLabelDerechaGde" ID="rdbTipoImpresionResultadoMicrobiologia" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Imprimir en Hojas Separadas</asp:ListItem>
                                                <asp:ListItem Value="0">Imprimir a continuación</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                   
                                       <tr>
                                  <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            Opción solo aplicable para Tamaño de Papel A4</td>
                                            </tr>
                                    <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top" colspan="3">
                                           </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="width: 328px">
                                            Datos del Protocolo a imprimir:</td>
                                        <td  rowspan="2" class="style8" style="width: 652px">
                                            <asp:CheckBoxList ID="chkDatosProtocoloImprimirMicrobiologia" runat="server" CssClass="myLabelDerechaGde" 
                                                RepeatColumns="3">
                                                <asp:ListItem Value="0">Nro. Registro</asp:ListItem>                                                                                           
                                                <asp:ListItem Value="1">Fecha de Entrega</asp:ListItem> 
                                                    <asp:ListItem Value="2">Sector</asp:ListItem>
                                                <asp:ListItem Value="3">Solicitante</asp:ListItem>
                                                <asp:ListItem Value="4">Origen</asp:ListItem>
                                                <asp:ListItem Value="5">Prioridad</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            (Seleccionar los datos opcionales a imprimir del protocolo 
                                            <br />
                                            en la hoja de resultados)</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top" colspan="3">
                                           </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                            Datos del Paciente a imprimir:</td>
                                        <td colspan="2" rowspan="2">
                                            <asp:CheckBoxList ID="chkDatosPacienteImprimirMicrobiologia" runat="server" CssClass="myLabelDerechaGde" 
                                                RepeatColumns="4">
                                                <asp:ListItem Enabled="False" Selected="True" Value="0">Apellido y Nombres</asp:ListItem>                                                                                           
                                                <asp:ListItem Value="1">DNI</asp:ListItem> 
                                                    <asp:ListItem Value="2" Enabled="False">Numero HC (sin uso)</asp:ListItem>
                                                <asp:ListItem Value="3">Edad</asp:ListItem>
                                                <asp:ListItem Value="4">Fecha Nacimiento</asp:ListItem>
                                                <asp:ListItem Value="5">Sexo</asp:ListItem>
                                                <asp:ListItem Value="6">Domicilio</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            (Seleccionar los datos opcionales a imprimir del paciente 
                                            <br />
                                            en la hoja de resultados)</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top" colspan="3">
                                          </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                          Firma Electrónica:</td>
                                        <td class="myLabelIzquierda" colspan="2">
                                            
                                            <asp:DropDownList ID="ddlImprimePieResultadosMicrobiologia" runat="server" 
                                                >
                                                <asp:ListItem Selected="True" Value="0">No usar</asp:ListItem>
                                                <asp:ListItem Value="1">Imprimir al Pie</asp:ListItem>
                                              <%--  <asp:ListItem Value="2">Imprimir por cada determinación</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            (Se imprimirá el apellido y nombre de la/s persona/s que validaron los resultados)</td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <%-- <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top" colspan="3">
                                         </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                            Logo de Impresión de Resultados:</td>
                                        <td style="width: 652px">
                                            <asp:FileUpload ID="fupLogoMicrobiologia" runat="server" CssClass="myTexto" Width="250px" 
                                                ondatabinding="fupLogo_DataBinding" />                                                                                      
                                            <asp:CheckBox ID="chkBorrarImagenMicrobiologia" runat="server" CssClass="myLabelDerechaGde" 
                                                Text="Borrar Imagen" />
                                        </td>
                                        <td   rowspan="3" class="style7" style="width: 6px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            (La imagen deberá ser de formato PNG.<br />
                                            El tamaño recomendado de la imagen es de 2.54x2.54 cm.)</td>
                                        <td style="width: 652px">
                                          <div  style="width:150px;height:100pt;overflow:scroll;border:1px solid #808080;" title="Imagen Logo"> 
                                          
                                            <asp:Image ID="Image2" runat="server" ImageUrl="" Visible="false" />
                                            </div>
                                        </td>
                                    </tr>--%>
                            
                                </table>
                            </div>
     
     <div id="tab9"  class="tab_content" style="border: 1px solid #C0C0C0; width: 1000px;"> 

 



<table>
<tr>
<td style="vertical-align: top">  <img src="App_Themes/default/images/barCodeEjemplo.png" /></td>
<td style="vertical-align: top">   &nbsp; &nbsp;</td>


<td>

<table class="style5">
      
       <tr>
                        <td class="style6">
                            SERVICIO LABORATORIO GENERAL Genera Código de Barras:</td>
                        <td align="left" class="style8" >
                            <anthem:CheckBox ID="chkImprimeCodigoBarrasLaboratorio" runat="server" Text="Si" 
                                oncheckedchanged="chkImprimeCodigoBarrasLaboratorio_CheckedChanged" 
                           
                                AutoCallBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="myLabelIzquierda" style="vertical-align: top; " colspan="2">
                           <hr /></td>
                    </tr>
                    <tr>
                    <td colspan="2">
                    <anthem:Panel ID="pnlLaboratorio" Enabled="false" runat="server">
                    <table>
                                        <tr>
                        <td class="myLabelIzquierda" style="vertical-align: top; width: 156px;">
                            Fuente de código de barras:</td>
                        <td class="myLabel" align="left">
                            <asp:RadioButtonList ID="ddlFuente" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="CCode39">Code 39</asp:ListItem>
                                <asp:ListItem Value="Ccode39M43">Code 39 Módulo 43</asp:ListItem>
                          <%--      <asp:ListItem Value="EAN-13">EAN-13</asp:ListItem>--%>
                            </asp:RadioButtonList>
                        </td>
                                            <td class="style1" rowspan="3" style="vertical-align: top">
                                              </td>
                    </tr>
                    <tr>
                        <td class="myLabelIzquierda" style="width: 156px">
                            Datos del protocolo a incluir:</td>
                        <td class="myLabel">
                            <asp:CheckBoxList ID="chkProtocolo" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Enabled="False" Selected="True">Numero de Protocolo</asp:ListItem>
                                <asp:ListItem>Fecha</asp:ListItem>
                                <asp:ListItem>Origen</asp:ListItem>
                                <asp:ListItem>Sector</asp:ListItem>
                                <asp:ListItem>Nro. Origen</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="myLabelIzquierda" style="width: 156px">
                            Datos del paciente a incluir:</td>
                        <td class="myLabel" align="left">
                            <asp:CheckBoxList ID="chkPaciente" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Apellido y Nombre</asp:ListItem>
                                <asp:ListItem>Sexo</asp:ListItem>
                                <asp:ListItem>Edad</asp:ListItem>
                                <asp:ListItem>Nro. Documento</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
 
 
                    </table>
                    </anthem:Panel>
                    </td>
                    </tr>
     </table>

     <br />     <br />  

     <table class="style9">
          <tr>
                        <td class="style10"  style="vertical-align: top; ">
                            SERVICIO MICROBIOLOGIA - Generar Código de Barras:</td>
                        <td align="left" class="style11"  >
                            <anthem:CheckBox ID="chkImprimeCodigoBarrasMicrobiologia" runat="server" Text="Si" 
                             
                                oncheckedchanged="chkImprimeCodigoBarrasMicrobiologia_CheckedChanged" 
                                AutoCallBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="myLabelIzquierda" style="vertical-align: top; " colspan="2">
                           <hr /></td>
                    </tr>
                    <tr>
                    <td colspan="2">
                    <anthem:Panel ID="pnlMicrobiologia" Enabled=false runat="server">
                    <table>
                                        <tr>
                        <td class="myLabelIzquierda" style="vertical-align: top; width: 156px;">
                            Fuente de código de barras:</td>
                        <td class="myLabel" align="left">
                            <asp:RadioButtonList ID="rdbFuente2" runat="server" 
                                RepeatDirection="Horizontal">
                               <asp:ListItem Value="CCode39">Code 39</asp:ListItem>
                                <asp:ListItem Value="Ccode39M43">Code 39 Módulo 43</asp:ListItem>
                          <%--      <asp:ListItem Value="EAN-13">EAN-13</asp:ListItem>--%>
                            </asp:RadioButtonList>
                        </td>
                                            <td class="myLabel" rowspan="3">
                                            </td>
                    </tr>
                    <tr>
                        <td class="myLabelIzquierda" style="width: 156px">
                            Datos del protocolo a incluir:</td>
                        <td class="myLabel">
                            <asp:CheckBoxList ID="chkProtocolo2" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Enabled="False" Selected="True">Numero de Protocolo</asp:ListItem>
                                <asp:ListItem>Fecha</asp:ListItem>
                                <asp:ListItem>Origen</asp:ListItem>
                                <asp:ListItem>Sector</asp:ListItem>
                                <asp:ListItem>Nro. Origen</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="myLabelIzquierda" style="width: 156px">
                            Datos del paciente a incluir:</td>
                        <td class="myLabel" align="left">
                            <asp:CheckBoxList ID="chkPaciente2" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Apellido y Nombre</asp:ListItem>
                                <asp:ListItem>Sexo</asp:ListItem>
                                <asp:ListItem>Edad</asp:ListItem>
                                <asp:ListItem>Nro. Documento</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
 
 
                    </table>
                    </anthem:Panel>
                    </td>
                    </tr>
     
     </table>


        <br />         <br />

     <table class="style9">
          <tr>
                        <td class="style12"  style="vertical-align: top; ">
                            SERVICIO PESQUISA NEONATAL - Generar Código de Barras:</td>
                        <td align="left" class="style13" >
                            <anthem:CheckBox ID="chkImprimeCodigoBarrasPesquisa" runat="server" Text="Si" 
                             
                                oncheckedchanged="chkImprimeCodigoBarrasPesquisa_CheckedChanged" 
                                AutoCallBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="myLabelIzquierda" style="vertical-align: top; " colspan="2">
                           <hr /></td>
                    </tr>
                    <tr>
                    <td colspan="2">
                    <anthem:Panel ID="pnlPesquisa" Enabled="false" runat="server">
                    <table>
                                        <tr>
                        <td class="myLabelIzquierda" style="vertical-align: top; width: 156px;">
                            Fuente de código d e barras:</td>
                        <td class="myLabel" align="left">
                            <asp:RadioButtonList ID="rdbFuente3" runat="server" 
                                RepeatDirection="Horizontal">
                               <asp:ListItem Value="CCode39">Code 39</asp:ListItem>
                                <asp:ListItem Value="Ccode39M43">Code 39 Módulo 43</asp:ListItem>
                          <%--      <asp:ListItem Value="EAN-13">EAN-13</asp:ListItem>--%>
                            </asp:RadioButtonList>
                        </td>
                                            <td class="myLabel" rowspan="3">
                                            </td>
                    </tr>
                    <tr>
                        <td class="myLabelIzquierda" style="width: 156px">
                            Datos del protocolo a incluir:</td>
                        <td class="myLabel">
                            <asp:CheckBoxList ID="chkProtocolo3" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Enabled="False" Selected="True">Numero de Protocolo</asp:ListItem>
                                <asp:ListItem>Fecha</asp:ListItem>
                                <asp:ListItem>Origen</asp:ListItem>
                                <asp:ListItem>Sector</asp:ListItem>
                                <asp:ListItem>Nro. Origen</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="myLabelIzquierda" style="width: 156px">
                            Datos del paciente a incluir:</td>
                        <td class="myLabel" align="left">
                            <asp:CheckBoxList ID="chkPaciente3" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Apellido y Nombre</asp:ListItem>
                                <asp:ListItem>Sexo</asp:ListItem>
                                <asp:ListItem>Edad</asp:ListItem>
                                <asp:ListItem>Nro. Documento</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
 
 
                    </table>
                    </anthem:Panel>
                    </td>
                    </tr>
     
     </table>
     </td>
</tr>
</table>
     
 
   
     </div>
     <div id="tab7" class="tab_content" style="border: 1px solid #C0C0C0; width: 1000px;">
     <table  style="width: 800px" >
    
     <tr>
     <td colspan="2" class="myLabelIzquierda">Impresora del Sistema:
  
         <anthem:DropDownList ID="ddlImpresora" runat="server">
         </anthem:DropDownList>
         <anthem:Button CssClass="myButtonGris" ToolTip="Agrega la impresora a la lista del SIL." ID="btnAgregarImpresora" runat="server" Text="Agregar" 
             onclick="btnAgregarImpresora_Click" />
             
    </td>
    </tr>
    <tr>
    <td colspan="2"><hr /></td>
    </tr>
         
         <tr>
     <td colspan="2" class="myLabelIzquierda" >
         <anthem:ListBox ID="lstImpresora" runat="server" Height="200px" Width="450px"></anthem:ListBox>
         <br />
           <anthem:ImageButton  OnClick ="btnSacarImpresora_Click" ToolTip="Sacar impresora de la lista"    ID="btnBorrar" runat="server" ImageUrl="App_Themes/default/images/eliminar.jpg" />
            
             </td>
     
     </tr>
     
         <tr>
     <td class="myLabelIzquierda" align="left" colspan="2">
     <hr />
         <asp:Button ID="Button1" CssClass="myButton"  Width="150px" runat="server" onclick="btnGuardarImpresora_Click" 
             Text="Guardar Impresoras" />
             </td>
     
     </tr>
        
     </table>
     </div>
     <div id="tab8" class="tab_content" style="border: 1px solid #C0C0C0; width: 1000px;">
     <table>
       <tr>
                                        <td class="myLabelIzquierdaGde" style="vertical-align: top" colspan="3">
                                         </td>
                                    </tr>
                                    <tr>
                                        <td class="myLabelIzquierda" style="vertical-align: top; width: 328px;">
                                            Logo de Impresión de Resultados:</td>
                                        <td class="myLabelIzquierda" style="width: 652px">
                                            <asp:FileUpload ID="fupLogo" runat="server" CssClass="myTexto" Width="250px" 
                                                ondatabinding="fupLogo_DataBinding" />                                                                                      
                                           
                                        </td>
                                        <td   rowspan="3" class="style7" style="width: 6px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="myLabel" style="vertical-align: top; width: 328px;">
                                            La imagen deberá ser de formato PNG.<br />
                                            El tamaño recomendado de la imagen es de 2.54x2.54 cm.<br />
                                            Esta imagen se imprimirá en el margen superior derecho del encabezado.</td>
                                        <td style="width: 652px">
                                          <div  style="width:150px;height:100pt;overflow:scroll;border:1px solid #808080;" title="Imagen Logo"> 
                                          
                                            <asp:Image ID="Image1" runat="server" ImageUrl="" Visible="false" />
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                    <td colspan="2">
                                    <hr />
                                     <asp:CheckBox ID="chkBorrarImagen" runat="server" CssClass="myLabelDerechaGde" 
                                                Text="Borrar Imagen" />
                                    </td>
                                    </tr>
     </table>
     </div>
                        </div> 
                      </td>      </tr>
                      
               <tr>
            <td colspan="2">         
                      <div align="right">
                                            
            <asp:Button ID="btnGuardar" runat="server" CssClass="myButton" Text="Actualizar" onclick="btnGuardar_Click" /></div>   
                     </td>
                 </tr>

            
      
        </table>
          
      <script src="script/Resources/jquery.min.js" type="text/javascript"></script>
 <link href="script/Resources/jquery-ui-1.8.20.css" rel="stylesheet" type="text/css" />   
    <script src="script/Resources/jQuery-ui-1.8.18.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

                      function editFeriado() {
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
        $('<iframe src="Turnos/FeriadoEdit.aspx" />').dialog({
            title: 'Feriados',
            autoOpen: true,
            width:480,
            height: 400,
            modal: true,
            resizable: false,
            autoResize: true,
            overlay: {
                opacity: 0.5,
                background: "black"
            }
        }).width(500);
    }
        
   
    
    </script>

    
    
    
    

    
</asp:Content>
