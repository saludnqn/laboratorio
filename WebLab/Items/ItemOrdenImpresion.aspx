<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemOrdenImpresion.aspx.cs" Inherits="WebLab.Items.ItemOrdenImpresion" MasterPageFile="~/Site1.Master" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">


   
  
<script src="../script/jquery.min.js" type="text/javascript"></script>
   

<script type="text/javascript">
$(document).ready(function()   
{   
    $(".tab_content").hide();   
    $("ul.tabs li:first").addClass("active").show();   
    $(".tab_content:first").show();   
  
    $("ul.tabs li").click(function()   
       {   
        $("ul.tabs li").removeClass("active");   
        $(this).addClass("active");   
        $(".tab_content").hide();   
 
        var activeTab = $(this).find("a").attr("href");   
        $(activeTab).fadeIn();   
        return false;   
    });   
});
</script>
   
  
   
    </asp:Content>
    
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
<br />   &nbsp;
    

    <div align="left" style="width:1000px">
    <table align="center" class="myTabla" style="width: 600px">
<tr>
    <td class="mytituloTabla" colspan="2">
        ORDEN DE IMPRESION</td>
    <td align="right" style="width: 319px"> <a href="../Help/Documentos/Orden de Impresión.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
</tr>

<tr>
    <td colspan="3">
          <hr class="hrTitulo" /></td>
</tr>

<tr>
    <td class="myLabelIzquierdaGde" colspan="3" >
                Servicio:&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlServicio" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlServicio_SelectedIndexChanged1">
                            </asp:DropDownList>
                                        
                </td>
</tr>



<tr>
    <td colspan="3">
        <hr /></td>
</tr>

<tr>
<td colspan="3">
 <ul class="tabs">
     <li><a href="#tab3">Orden</a></li>        
    <li><a href="#tab1">Areas</a></li>       
    <li><a href="#tab2">Analisis</a></li>     
</ul>

<div class="tab_container">
<div id="tab3" class="tab_content">
<table>
<tr><td>

     <asp:RadioButtonList ID="rdbOrdenImpresion" runat="server" 
                                                CssClass="myLabelIzquierda">
                                                <asp:ListItem Value="0">1. Según Orden determinado por el usuario</asp:ListItem>
                                                <asp:ListItem Value="1">2. Segun Orden de ingreso del protocolo</asp:ListItem>
                                                
                                            </asp:RadioButtonList>
</td>
</tr>
<tr>

<td>
     <asp:Button ID="btnGuardar" runat="server" CssClass="myButton" 
                                                onclick="btnGuardar_Click" Text="Guardar" />
</td>
</tr>
<tr>

<td>
     &nbsp;</td>
</tr>
<tr>

<td class="myLabelIzquierda">
   1. Configure el orden de las áreas y de las practicas en las solapas siguientes.<br />
   2. El orden que se tomará en la impresión es el orden en que fueron ingresadas las practicas en el ingreso del protocolo.</td>
</tr>
<tr>

<td>
     &nbsp;</td>
</tr>
</table> </div>
    <div id="tab1" class="tab_content">
       <table  width="520px" align="center" class="myTabla">
            <tr>
                <td class="myLabelIzquierdaGde" colspan="2">
                    Lista de Areas&nbsp;
                            &nbsp;
                </td>
            </tr>
            <tr>
                <td class="myLabelIzquierdaGde" colspan="2">
                    <hr /></td>
            </tr>
            <tr>
                <td rowspan="2">
                    &nbsp;
                    &nbsp;
                                            <anthem:ListBox ID="lstArea" runat="server" 
                        Height="300px" Width="400px">
                                            </anthem:ListBox>
                </td>
                <td>
                    &nbsp;
                            <anthem:ImageButton ID="imgSubirArea" runat="server" 
                                ImageUrl="~/App_Themes/default/images/arrow-up.gif" 
                                ToolTip="Subir analisis" onclick="imgSubirArea_Click" />
                </td>
            </tr>
            <tr>
                <td>
                            <anthem:ImageButton ID="imgBajarArea" runat="server" 
                                ImageUrl="~/App_Themes/default/images/arrow-down.gif" 
                                ToolTip="Bajar analisis" onclick="imgBajarArea_Click" />
                </td>
            </tr>
            <tr>
            <td colspan="2">Utilice las flechas para establecer el orden de impresión de las áreas.</td>
            </tr>
            <tr>
            <td colspan="2"><hr /></td>
            </tr>
            <tr>
            <td colspan="2" align="right">
                                        
                                            <asp:Button ID="btnGuardarArea" runat="server" Text="Guardar" CssClass="myButton" 
                                Width="86px" onclick="btnGuardarArea_Click" />
                                </td>
            </tr>
        </table>
    </div>
    <div id="tab2" class="tab_content">
     <table  width="520px" align="center" class="myTabla">
					<tr>
						<td colspan="2" class="style2"></td>
					</tr>
						<tr>
							<td class="myLabelIzquierda" colspan="2"  >Area:<asp:RangeValidator ID="rvArea" runat="server" ControlToValidate="ddlArea" 
                                ErrorMessage="Area" MaximumValue="999999" MinimumValue="1" Type="Integer" 
                                ValidationGroup="0">*</asp:RangeValidator>
                                        &nbsp;     &nbsp;     
                                                                    
                            <anthem:DropDownList ID="ddlArea" runat="server" 
                                ToolTip="Seleccione el area" TabIndex="5" CssClass="myList" 
                                    AutoCallBack="True" onselectedindexchanged="ddlArea_SelectedIndexChanged">
                            </anthem:DropDownList>
                                        
					                        </td>
					</tr>
					<tr>
						<td   colspan="2"><hr /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" colspan="2" >
                                            Analisis del Area</td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" rowspan="3" >
                                            <anthem:ListBox ID="lstAnalisis" runat="server" Height="300px" Width="400px">
                                            </anthem:ListBox>
                            <br />
                        </td>
						<td class="myLabelIzquierda" >
                            <anthem:ImageButton ID="imgSubirAnalisis" runat="server" 
                                ImageUrl="~/App_Themes/default/images/arrow-up.gif" onclick="imgSubirAnalisis_Click" 
                                ToolTip="Subir analisis" />
                        </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" >
                            <br />
                            <br />
                        </td>
						
					</tr>
					<tr>
						<td class="myLabelIzquierda" >
                            <anthem:ImageButton ID="imgBajarAnalisis" runat="server" 
                                ImageUrl="~/App_Themes/default/images/arrow-down.gif" onclick="imgBajarAnalisis_Click" 
                                ToolTip="Bajar analisis" />
                        </td>
						
					</tr>
					<tr>
						<td   colspan="2">
                                            Utilice las flechas para establecer el orden de impresión de los analisis.</td>
						
					</tr>
					<tr>
						<td   colspan="2">
                                          <hr /></td>
						
					</tr>
					<tr>
						<td colspan="2" align="right">
                                        
                                            <asp:Button ID="btnGuardarAnalisis" runat="server" Text="Guardar" 
                                                onclick="btnGuardarAnalisis_Click" CssClass="myButton" 
                                Width="86px" />
                                </td>
						
					</tr>
					</table>
    </div>
</div>
</td>
</tr>
</table>

 </div>
 </asp:Content>