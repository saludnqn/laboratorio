<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObservacionesEdit.aspx.cs" Inherits="WebLab.Resultados.ObservacionesEdit"  %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">       
     <link type="text/css"rel="stylesheet"      href="../App_Themes/default/style.css" />  
      <link type="text/css"rel="stylesheet"      href="../App_Themes/default/principal/style.css" />        
</head>
<body >  
    <form id="form1" runat="server"  >           
  
             
           <table>           
                <tr>
                <td style="vertical-align: top" colspan="2">                                               
                              <asp:Label CssClass="mytituloGris" ID="lblObservacionAnalisis" runat="server" Text="Label"></asp:Label>
                              
                              <hr />
                              </td>
                              </tr> 
                              <tr>
                <td style="vertical-align: top"  colspan="2" class="myLabelIzquierda">                                               
                
                               Predefinidas:&nbsp;<asp:DropDownList CssClass="myList" ID="ddlObservacionesCodificadas" runat="server" Width="250px">
                               </asp:DropDownList>
                               <anthem:Button  OnClick="btnAgregarObsCodificada_Click" 
                                  ToolTip="Agrega la observacion predefinida" CssClass="myButtonGrisLittle" 
                                  ID="btnAgregarObsCodificada" runat="server" Text="Agregar" Width="45px" />
                         &nbsp;
                    
                           
                               <anthem:Button OnClick="btnBorrarObservacionesAnalisis_Click" 
                                  CssClass="myButtonGrisLittle" ToolTip="Borra el texto de observaciones" ID="btnBorrarObservacionAnalisis"
                                   runat="server" Text="Limpiar" Width="45px" />
                                    
					
                         <hr />       
                    </td>
                </tr>
                
              
               
               
                <tr>
                <td style="vertical-align: top"  colspan="2">
                    
                           
                               <anthem:TextBox  ID="txtObservacionAnalisis" runat="server" TextMode="MultiLine" MaxLength="500" Rows="10" Width="450px" CssClass="myTexto"></anthem:TextBox>
                                    
					
                               
                                    <br />
					
                    </td>
                </tr>
                
              
               <hr />
               
                <tr>
                <td style="vertical-align: top" align="left">
                    
                           <asp:Button 
                        onclick="btnBorrarGuardarObservacionAnalisis_Click" 
                        ToolTip="Borra y Guarda la observacion registrada"  CssClass="myButton" 
                        ID="btnBorrarGuardarObservacionAnalisis" runat="server" Text="Borrar" />
                                    
					</td>
                <td style="vertical-align: top" align="right">
                    
                           <asp:Button onclick="btnGuardarObservacionesAnalisis_Click" ToolTip="Guarda la observacion registrada"  CssClass="myButton" ID="btnGuardarObservacionAnalisis" runat="server" Text="Guardar" />&nbsp;&nbsp;&nbsp;
                               <asp:Button onclick="btnValidarObservacionesAnalisis_Click" ToolTip="Guarda y Valida la observacion registrada"  CssClass="myButton" ID="btnValidarObservacionAnalisis" runat="server" Text="Validar" />
                                    
                               </td>
                </tr>
                
              
               
               
                <tr>
                <td style="vertical-align: top" align="left" colspan="2">
                    
                           <asp:Label CssClass="myLabelIzquierda" 
                        Visible="False" ID="lblUsuarioObservacionAnalisis" runat="server"></asp:Label>
                                    
					</td>
                </tr>
                
              
               
               
           </table>
      
         
 </form>
</body>
  
</html>
