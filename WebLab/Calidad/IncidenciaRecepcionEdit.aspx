<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncidenciaRecepcionEdit.aspx.cs" Inherits="WebLab.Calidad.IncidenciaRecepcionEdit" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="head">

     <link type="text/css"rel="stylesheet"      href="../script/jquery-ui-1.7.1.custom.css" />  

     <script language="Javascript" type="text/javascript" src="../FusionCharts/FusionCharts.js"></script>

  <script type="text/javascript"      src="../script/jquery.min.js"></script> 
  <script type="text/javascript"      src="../script/jquery-ui.min.js"></script> 
    
      <script type="text/javascript"     src="../script/jquery.ui.datepicker-es.js"></script>   
      
      <script type="text/javascript">


          $(function () {
              $("#<%=txtFecha.ClientID %>").datepicker({
                  showOn: 'button',
                  buttonImage: '../App_Themes/default/images/calend1.jpg',
                  buttonImageOnly: true
              });
          });

     
  </script>  
  
  
   	 <script type="text/javascript" src="../script/Mascara.js"></script>
    <script type="text/javascript" src="../script/ValidaFecha.js"></script>   
  
  

  
    </asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <br />   &nbsp;

        <div align="left" style="width:800px;">
 <table  width="800px" align="center" class="myTabla">
					<tr>
		<td  rowspan="6">
    
            
    
        </td>
		<td  rowspan="6"  > &nbsp;</td>
        <td  rowspan="6"  style="vertical-align: top" >
        <table>
        <tr>
        <td class="mytituloTabla" colspan="2"><asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label><hr /></td>
                                            </tr>

                                            
					<tr>
		<td class="myLabelIzquierda" colspan="2">
            <asp:Panel ID="pnlIncidencia" runat="server" Width="100%">
            <table width="100%">
            <tr>
            <td>Número:</td>
            <td> 
                <asp:Label ID="lblNumero" runat="server" Text="Label"></asp:Label>
            </td>
            </tr>
            <tr>
            <td>Fecha Registro:</td>
            <td> 
                <asp:Label ID="lblFecha" runat="server" Text="Label"></asp:Label>
            </td>
            </tr>
            <tr>
            <td>Usuario:</td>
            <td> 
                <asp:Label ID="lblUsuario" runat="server" Text="Label"></asp:Label>
            </td>
            </tr>
            <tr>
            <td colspan="2"><hr />
            </td>
            </tr>
            </table>
            </asp:Panel>
                        </td>
                                            </tr>
                                            <tr>
                                            <td class="myLabelIzquierda">
  Fecha: <input id="txtFecha" runat="server" type="text" maxlength="10" 
                         onblur="valFecha(this)" 
                        onkeyup="mascara(this,'/',patron,true)" tabindex="0" class="myTexto" 
                                style="width: 70px" 
        title="Ingrese la fecha de inicio"  /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ControlToValidate="txtFecha" ErrorMessage="Complete la fecha de la incidencia" 
                                                    ValidationGroup="0"></asp:RequiredFieldValidator>
                                                </td></tr>

                                            <tr>
                                            <td class="myLabelIzquierda">
                                                Origen:
                                                <asp:DropDownList ID="ddlEfectorOrigen" runat="server"  CssClass="myList" TabIndex="4" ToolTip="Seleccione el efector">
                                                </asp:DropDownList>
                                                </td></tr>

                                            			<tr>
		<td class="myLabelIzquierda">
                                                            <asp:TreeView ID="TreeView1" runat="server" ImageSet="Arrows">
                                                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                                <Nodes>
                                                                    <asp:TreeNode Expanded="True" SelectAction="Expand" ShowCheckBox="False" 
                                                                        Text="No cumple con Indicación para toma de muestra" Value="1">
                                                                        <asp:TreeNode ShowCheckBox="True" Text="no la recibió" Value="2"></asp:TreeNode>
                                                                        <asp:TreeNode ShowCheckBox="True" Text="no la entendió bien" Value="3">
                                                                        </asp:TreeNode>
                                                                        <asp:TreeNode ShowCheckBox="True" Text="no la cumplió como debía" Value="4">
                                                                        </asp:TreeNode>
                                                                    </asp:TreeNode>
                                                                    <asp:TreeNode ShowCheckBox="True" Text="No trae solicitud médica." Value="5">
                                                                    </asp:TreeNode>
                                                                    <asp:TreeNode ShowCheckBox="True" Text="Faltan datos personales." Value="6">
                                                                    </asp:TreeNode>
                                                                    <asp:TreeNode ShowCheckBox="True" Text="Solicitud Médica ilegible." Value="7">
                                                                    </asp:TreeNode>
                                                                    <asp:TreeNode ShowCheckBox="True" Text="Fuera de horario." Value="8">
                                                                    </asp:TreeNode>
                                                                </Nodes>
                                                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="25px" 
                                                                    HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                                                <ParentNodeStyle Font-Bold="False" />
                                                                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
                                                                    HorizontalPadding="0px" VerticalPadding="0px" BackColor="Yellow" />
                                                            </asp:TreeView>
                                                            <br />
                                                            </td><td class="myLabelIzquierda">
                                                          <%--  <anthem:CheckBoxList ID="chkIncidencia0" runat="server" 
                                                                >
                                                            </anthem:CheckBoxList>
                                                            
                                                            <anthem:CheckBoxList ID="chkIncidencia" runat="server" 
                                                                onselectedindexchanged="chkIncidencia_SelectedIndexChanged" 
                                                                AutoCallBack="True">
                                                            </anthem:CheckBoxList>
                                                          --%>  
                                                                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                                                    ErrorMessage="Debe seleccionar al menos una incidencia" 
                                                                    onservervalidate="CustomValidator1_ServerValidate" ValidationGroup="0"></asp:CustomValidator>
                                                            
                                                            </td>
                                            </tr>

                                            			<tr>
		<td   colspan="2"><hr /></td>
                                            </tr>

                                            			<tr>
		<td style="vertical-align: top"  ><asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" CausesValidation="False" 
                                                ToolTip="Regresa a la pagina anterior" onclick="lnkRegresar_Click1" >Regresar</asp:LinkButton></td>
                                                            <td class="style7" align="right">
                                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"  CssClass="myButton" 
                                                                Height="25px" Width="100px" ValidationGroup="0" TabIndex="2" 
                                                                onclick="btnGuardar_Click" />
                                                            
                                                            </td>
                                            </tr>

                                        
                                            			<tr>
		<td style="vertical-align: top"  >
            <br />
            <br />
            <br />
                                                            </td><td class="style7" align="left">
                                                                &nbsp;</td>
                                            </tr>

                                        
        </table>
        </td>
		
                                            </tr>
                                            </table>

</div>
		    
 </asp:Content>