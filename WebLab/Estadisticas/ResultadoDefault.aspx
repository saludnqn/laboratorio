<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultadoDefault.aspx.cs" Inherits="WebLab.Estadisticas.ResultadoDefault" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

  <%--  <link type="text/css"rel="stylesheet"      href="../App_Themes/default/style.css" />  --%>
  <%--  <link type="text/css"rel="stylesheet"      href="../script/jquery-ui-1.7.1.custom.css" />  
      <link rel="stylesheet" type="text/css" href="../App_Themes/default/principal/style.css" />--%>
     
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">          
<br />   &nbsp;
    
    <link rel="stylesheet" type="text/css" href="~/App_Themes/default/principal/style.css" />
 
 

 <div align="left" style="width:800px">
                 <table  width="600px" align="center" cellpadding="1" cellspacing="1">
					<tr>
						<td class="mytituloTabla" colspan="3">REPORTES ESTADISTICOS POR RESULTADO</td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" colspan="3">
                            <hr class="hrTitulo" /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">
                            <asp:ImageButton ID="imgResultadoPredefinido" runat="server" 
                                ImageUrl="~/App_Themes/default/principal/images/estadistica3.jpg"
                                onclick="imgResultadoPredefinido_Click" />
                                        
                                </td>
						<td class="myLabelIzquierda">
                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
						<td >
					<div class="mytituloGris">Estadisticas por Resultado Predefinido</div>
						<br />
                           <div class="myLabelGris">Muestra para una practica nomenclada la cantidad de casos obtenidos para cada resultado predefinido.
                           Por ejemplo, la cantidad de casos por resultado de Chagas, HIV, ToxoPlasmosis, etc.&nbsp;<a href="ReportePorResultado.aspx" >Ingresar</a></div>                       
                           </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">
                           </td>
						<td class="myLabelIzquierda">
                            &nbsp;</td>
						<td class="myLabelIzquierda">
                            <hr /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">
                            <asp:ImageButton ID="imgResultadoNumerico" runat="server" 
                               ImageUrl="~/App_Themes/default/principal/images/estadistica3.jpg"
                                onclick="imgResultadoNumerico_Click" />                                        
                                </td>
						<td class="myLabelIzquierda">
                            &nbsp;</td>
						<td>
						
							<div class="mytituloGris">	Estadisticas por Resultado Numericos</div>
							<br />
                           <div class="myLabelGris">
                            Muestra para una practica nomenclada de tipo Numerica; la cantidad de casos obtenidos para un rango de valores, diferenciados por grupos etareos.&nbsp;
                            <a href="ReportePorResultadoNum.aspx" >Ingresar</a></div>
                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda">
                            &nbsp;</td>
						<td class="myLabelIzquierda">
                            &nbsp;</td>
						<td class="myLabelIzquierda">
                            &nbsp;</td>
					</tr>				
					<tr>
						<td class="myLabelIzquierda" colspan="3"><hr /></td>
					</tr>
						<tr>
						<td align="right">
                            &nbsp;&nbsp;
                                
                                </td>
						<td align="right">
                            &nbsp;</td>
						<td align="right">
                            &nbsp;</td>
						<tr>
						<td  >
                                        
                                            &nbsp;</td>
						
						<td  >
                                        
                                            &nbsp;</td>
						
						<td  >
                                        
                                            &nbsp;</td>
						
					</tr>
					<tr>
						<td  >
                            &nbsp;</td>
						
						<td  >
                            &nbsp;</td>
						
						<td  >
                            &nbsp;</td>
						
					</tr>
					</table>						
 </div>
 </asp:Content>
