<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultadoHTEdit.aspx.cs" Inherits="WebLab.Resultados.ResultadoHTEdit" MasterPageFile="~/Site1.Master" %>

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

    <table style="width: 1000px;" align="center">
        <tr>
            <td class="mytituloTabla" colspan="2"><b>
                <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></b>
            </td>
            <td class="mytituloTabla" colspan="2" align="right">
                <asp:Label ID="lblArea" runat="server" CssClass="myLinkRojo" 
                    Font-Bold="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="mytituloTabla" colspan="4"><hr /></td>
        </tr>
       
        <tr>
            <td colspan="4">  
                           
             <asp:Panel ID="pnlControl" runat="server" Visible="False" >
                     
                  <INPUT TYPE="button" accesskey="m" title="Alt+Shift+M"  runat="server" NAME="marcar" id="lnkMarcarControl" VALUE="Marcar todos" onClick="seleccionar_todo()" style="font-size: 11px; color: #333333; text-decoration: underline; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" class="myLabelIzquierda">
                  <INPUT TYPE="button"  accesskey="z" runat="server" title="Alt+Shift+Z" NAME="desmarcar" id="lnkDesmarcarControl"  VALUE="Desmarcar todos" onClick="desmarcar_todo()" style="font-size: 11px; color: #333333; text-decoration: underline; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" class="myLabelIzquierda">						   
                     
                </asp:Panel>
               
                    <b style="color: #008000; font-weight: bold">C</b> (Controlado)      &nbsp;&nbsp;&nbsp;&nbsp;   <b style="color: #0000FF; font-weight: bold">V</b> (Validado) 
                   <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label>
                      <asp:CustomValidator ID="cvValidaControles" runat="server" ErrorMessage="CustomValidator" Font-Size="11pt" onservervalidate="cvValidaControles_ServerValidate" ValidationGroup="0" Font-Bold="True"></asp:CustomValidator>                   
                 
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td width="50" style="vertical-align: top">
               <asp:Panel ID="PanelPrimeraColumna" runat="server" HorizontalAlign="Left"  ScrollBars="Both" Width="80px" Height="420px" style="OVERFLOW-Y: hidden; OVERFLOW-X: hidden;" >                               
                   <asp:Table ID="tContenido0"  Runat="server" CssClass="mytable1" Width="80px" ></asp:Table> 
                                           </asp:Panel>
                                    
                                                    </td>
                                                    <td>
                                    
               <asp:Panel onkeydown="enterToTab(event)" ID="Panel1" runat="server"  ScrollBars="Auto"   onscroll="hacerScrollHorizontal();"   Width="1100px" Height="410px">                               
                   <asp:Table Width="100%" ID="tContenido" BorderStyle="Solid" BorderWidth="1px"
                                                   Runat="server" CssClass="mytable1" ></asp:Table> 
                                           </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">
                                           <hr /></td>
        </tr>
        <tr>
            <td align="left">
                                            <asp:LinkButton TabIndex="500" accesskey="r" title="Alt+Shift+R" ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                onclick="lnkRegresar_Click">Regresar</asp:LinkButton>
            </td>
            <td align="right" colspan="2">
                &nbsp;&nbsp;&nbsp;
                </td>
            <td align="right">
                <asp:CheckBox ID="chkFormula" runat="server" Text="Aplicar fórmulas" 
                    Checked="True" CssClass="myLabel" />
                    <asp:Button ID="btnValidarPendiente" AccessKey="P" ToolTip="Alt+Shift+P:Valida solo lo pendiente de validar"   onclick="btnValidarPendiente_Click" 
                    Visible="false" runat="server" CssClass="myButtonGris" Text="Validar pendiente"
                                 ValidationGroup="0" Width="120px" TabIndex="600" />  &nbsp;&nbsp;&nbsp;
    
                     <asp:Button ID="btnValidarPendienteySalir" AccessKey="t"    ToolTip="Alt+Shift+T:Valida solo lo pendiente de validar y sale de la pantalla" 
                    onclick="btnValidarPendienteySalir_Click" Visible="false" runat="server" 
                    CssClass="myButtonGris" Text="Validar pendiente y Salir"
                                 ValidationGroup="0" Width="150px" TabIndex="600" />  &nbsp;&nbsp; &nbsp;
                     <asp:Button AccessKey="l"  ID="btnGuardarParcial" runat="server" CssClass="myButton" Text="Guardar Parcial" 
                    ValidationGroup="0" onclick="btnGuardarParcial_Click" 
                    ToolTip="Alt+Shift+L:Guarda los resultados sin salir de la pantalla" Width="120px" />
                &nbsp;
                <asp:Button AccessKey="s" ID="btnGuardar" runat="server" CssClass="myButton" Text="Guardar y Salir" 
                    onclick="btnGuardar_Click" ValidationGroup="0" 
                    ToolTip="Alt+Shift+S:Guarda los resultados y sale de la pantalla." Width="120px" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="4">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    HeaderText="Datos incorrectos. Verifique." ShowMessageBox="True" 
                    ShowSummary="False" ValidationGroup="0" />
            </td>
        </tr>
    </table>    
    
    <script type="text/javascript">


function hacerScrollHorizontal(){

var PanelPrimeraColumna = document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("PanelPrimeraColumna").ClientID %>');
var PanelScroll = document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("Panel1").ClientID %>');
PanelPrimeraColumna.scrollTop  = PanelScroll.scrollTop  ;
}

var anchoTable= document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("tContenido").ClientID %>').offsetWidth;
//alert(anchoTable);
if (anchoTable>1000)
{
//alert('entrar');
var obj =document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("PanelPrimeraColumna").ClientID %>');
obj.style.height = "390px";

}


function seleccionar_todo() {//Funcion que permite seleccionar todos los checkbox excepto el chkFormula

    form = document.forms[0];

    for (i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == "checkbox") {
            if (form.elements[i].name.indexOf("chkFormula") == -1)
                form.elements[i].checked = 1;
        }
    }
}

function desmarcar_todo() {//Funcion que permite desmarcar todos los checkbox excepto el chkFormula

    form = document.forms[0];

    for (i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == "checkbox") {
            if (form.elements[i].name.indexOf("chkFormula") == -1)
                form.elements[i].checked = 0;
        }
    }
} 


</script>

    
     </asp:Content>