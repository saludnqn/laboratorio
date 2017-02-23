<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultadoItemEdit.aspx.cs" Inherits="WebLab.Resultados.ResultadoItemEdit" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

      
  

                   <script type="text/javascript">

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


function enterToTab(pEvent) {
        if (window.event.keyCode == 13  )
        {     
          
            window.event.keyCode = 9; 
            
        }
    }


  </script>  
  
   


   
    </asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">               
<br />  &nbsp;    
    
<div align="left" style="width: 1000px">

                 <table  width="1100px"                                            
                     style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal; color: #000000" 
                     cellpadding="1" cellspacing="1" align="center">										
					<tr>
						<td>
                            <asp:Label ID="lblTitulo" CssClass="mytituloTabla" runat="server" Text="CARGA DE RESULTADOS" ></asp:Label></td>
						<td align="right">
                               	<asp:ImageButton ImageUrl="~/App_Themes/default/images/actualizar.gif"  ID="btnActualizar"  runat="server"  ToolTip="Ctrl+F4"   onclick="btnActualizarPracticas_Click"
                        ></asp:ImageButton> 
                                           </td>
					</tr>																
					<tr>
						<td>				
                            <asp:Label ID="lblItem" runat="server" CssClass="mytituloGris"></asp:Label><br />   
                          <%--  <asp:Label ID="lblMensaje" Visible="true" CssClass="myLabelIzquierda" runat="server" Text="0"></asp:Label>--%>
                             <asp:CustomValidator ID="cvValidaControles" runat="server" 
                                ErrorMessage="CustomValidator" Font-Size="9pt" 
                                onservervalidate="cvValidaControles_ServerValidate" 
                                ValidationGroup="0"></asp:CustomValidator>          &nbsp; &nbsp;&nbsp; &nbsp;                  
                                <asp:Button ID="btnAceptarValorFueraLimite" Width="200px" CssClass="myButtonRojo"  onclick="btnAceptarValorFueraLimite_Click" runat="server" Text="Aceptar valor fuera de límite" Visible="false" />  
                            <asp:Label ID="lblIdValorFueraLimite" Visible="false" runat="server" Text="0"></asp:Label>
                              <asp:Label ID="lblIdValorFueraLimite1" Visible="false" runat="server" Text="0"></asp:Label>
                                   
                              
                              
                        </td>
						<td align="right">
                        <INPUT TYPE="button" accesskey="m" title="Alt+Shift+M"  runat="server" NAME="marcar" id="lnkMarcar" VALUE="Marcar todos" onClick="seleccionar_todo()" style="font-size: 11px; color: #333333; text-decoration: underline; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" class="myLabelIzquierda">
                  <INPUT TYPE="button" accesskey="z" title="Alt+Shift+Z" runat="server" NAME="desmarcar" id="lnkDesmarcar"  VALUE="Desmarcar todos" onClick="desmarcar_todo()" style="font-size: 11px; color: #333333; text-decoration: underline; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" class="myLabelIzquierda">
					
                          <%--  <asp:LinkButton     ID="lnkMarcar" runat="server" CssClass="myLittleLink" 
                                 onclick="lnkMarcar_Click">Marcar todas</asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="lnkDesmarcar" runat="server" 
                                 CssClass="myLittleLink" onclick="lnkDesmarcar_Click">Desmarcar todas</asp:LinkButton>    --%>                                     
                        </td>
					</tr>																
					<tr>
						<td colspan="2">
                             <hr /></td>
					</tr>																
                   <tr>																											
						<td  colspan="2" style="vertical-align: top" >                           												        
       
                                         
						                           <asp:Panel onkeydown="enterToTab(event)"  ID="Panel1" BackColor="#F2F2F2" runat="server" ScrollBars="Vertical" Width="100%" Height="600px" BorderStyle="Solid" BorderWidth="1" BorderColor="#CCCCCC">                                                                                                                
                                               <asp:Table ID="tContenido" 
                                            
                   
                                                   Runat="server" CellPadding="0" CellSpacing="0" CssClass="mytable1" 
                                                   Width="100%" ></asp:Table></asp:Panel>
                                                  
                                         </td>
						
                                          


				</tr>		
					<tr>
						
						<td style="vertical-align: top" align="left">        
    <asp:HyperLink accesskey="r" title="Alt+Shift+R" ID="hypRegresar" runat="server" CssClass="myLink">Regresar</asp:HyperLink>    
                                         </td>
						
						<td style="vertical-align: top" align="right">   
                                 
           <asp:Button ID="btnValidarPendiente"   onclick="btnValidarPendiente_Click" Visible="false"  AccessKey="P" runat="server" CssClass="myButtonGris" Text="Validar pendiente"  ToolTip="Alt+Shift+P:Validar solo lo pendiente"
                                 ValidationGroup="0" Width="120px" TabIndex="600" />  &nbsp;&nbsp;&nbsp;
    
                            <asp:Button accesskey="s" title="Alt+Shift+S" ID="btnGuardar" runat="server" CssClass="myButton" Text="Guardar" 
                                onclick="btnGuardar_Click" ValidationGroup="0" />
                                         </td>
						
					</tr>
                    <tr>
						
						<td style="vertical-align: top" align="left">        
    
                                         </td>
						
						<td style="vertical-align: top" align="right">        
                                             
                                         </td>
						
					</tr>
					</table>
						


                           
                        </div>
                        <script src="../script/Resources/jquery.min.js" type="text/javascript"></script>
 <link href="../script/Resources/jquery-ui-1.8.20.css" rel="stylesheet" type="text/css" />   
    <script src="../script/Resources/jQuery-ui-1.8.18.min.js" type="text/javascript"></script>
					   <script type="text/javascript">

                       

					       function seleccionar_todo() {//Funcion que permite seleccionar todos los checkbox 

					           form = document.forms[0];

					           for (i = 0; i < form.elements.length; i++) {
					               if (form.elements[i].type == "checkbox") {					            
					                       form.elements[i].checked = 1;
					               }
					           }
					       }

					       function desmarcar_todo() {//Funcion que permite desmarcar todos los checkbox

					           form = document.forms[0];

					           for (i = 0; i < form.elements.length; i++) {
					               if (form.elements[i].type == "checkbox") {					                   
					                       form.elements[i].checked = 0;
					               }
					           }
					       }
					       function AntecedenteAnalisisView(idAnalisis, idPaciente, ancho, alto) {
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

					           $('<iframe src="AntecedentesAnalisisView.aspx?idAnalisis=' + idAnalisis + '&idPaciente=' + idPaciente + '" />').dialog({
					               title: 'Evolucion',
					               autoOpen: true,
					               width: ancho,
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

        //var operacion='Valida';
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


 
 function editDiagnostico(idProtocolo) {
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
 

</script>				
 
</asp:Content>