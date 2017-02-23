<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultadosporServicio.aspx.cs" Inherits="WebLab.Informes.ResultadosporServicio" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>



<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>



<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

    <link type="text/css"rel="stylesheet"      href="../script/jquery-ui-1.7.1.custom.css" />  

  <script type="text/javascript"      src="../script/jquery.min.js"></script> 
  <script type="text/javascript"      src="../script/jquery-ui.min.js"></script> 
    
      <script type="text/javascript"     src="../script/jquery.ui.datepicker-es.js"></script>   
      
      <script type="text/javascript">


          $(function () {
              $("#<%=txtFechaDesde.ClientID %>").datepicker({
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
        .auto-style1 {
            width: 1150px;
        }
        .auto-style4 {
            width: 96px;
        }
        .auto-style5 {
            width: 1047px;
        }
    </style>
  
 

   
    </asp:Content>
 
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          
    <br />   &nbsp;  <div align="left" style="width: 1000px" >


				 <table  width="600px" align="center"                      
                     
                   
                     class="myTabla" >
				
					<tr>
					<td colspan="5">
					
					<table  width="1150px" align="center"                      
                     
                   
                     cellpadding="1" cellspacing="1" class="auto-style1">
					<tr>
						<td class="mytituloTabla" colspan="2">
                            RESULTADOS POR SERVICIO
                        <hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="auto-style4" >Fecha:</td>
						<td class="auto-style5">
                    <input id="txtFechaDesde" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="3" class="myTexto" 
                                style="width: 70px"  /></td>
					</tr>
					
                            
						
						<tr>
						<td class="auto-style4" >Sector/Servicio:</td>
						<td class="auto-style5">
                                        <asp:DropDownList ID="ddlSectorServicio" runat="server" TabIndex="2" 
                                            ToolTip="Seleccione el sector">
                                        </asp:DropDownList>
                                        
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlSectorServicio" ErrorMessage="Seleccione un servicio/sector" MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0"></asp:RangeValidator>
                                        
                                            </td>
                            </tr>
						</table>
					</td>
					</tr>
					<tr>
						<td   colspan="5">
                                            <hr />
                                        
					
                                                                 <asp:Button ID="btnBuscarControl" runat="server" CssClass="myButton" 
                                                                     onclick="btnBuscarControl_Click" TabIndex="15" Text="Buscar" 
                                                                     ValidationGroup="0" Width="77px" />
                                                                                                         
					    </td>
						
					</tr>
					<tr>
						<td   colspan="5"> 
                            &nbsp;</td>
						
					</tr>
					<tr>
						<td   colspan="5">
                                            &nbsp;</td>
						
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

    function VerificaLargo(source, arguments) {
        var Observacion = arguments.Value.toString().length;
        //   alert(Observacion);
        if (Observacion > 4000)
            arguments.IsValid = false;
        else
            arguments.IsValid = true;    //Si llego hasta aqui entonces la validación fue exitosa        
    }



    function PreguntoEliminar() {
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
