<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SectorEdit.aspx.cs" Inherits="WebLab.Sectores.SectorEdit" MasterPageFile="~/Site1.Master" %>

 
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<br />   &nbsp;
    
          <div align="left" style="width:1000px">
       
		<table width="500px" align="center" class="myTabla">
			<tr>
				<td class="myLabelIzquierda" ><b  class="mytituloTabla">SERVICIOS</b></td>
				<td   colspan="2" align="right">
                    <a href="../Help/Documentos/Servicios.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" colspan="3" >    <hr class="hrTitulo" /></td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" >Nombre:</td>
				<td   colspan="2">
                    <asp:TextBox ID="txtNombre" runat="server" Width="284px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese el nombre del sector" MaxLength="50"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvNombre" 
                        runat="server" ControlToValidate="txtNombre" 
                        ErrorMessage="Nombre" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" style="vertical-align: top" >Abreviatura:</td>
				<td colspan="2">
                    <asp:TextBox ID="txtAbreviatura" runat="server" Width="100px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese la abreviatura" MaxLength="3"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvAbreviatura" 
                        runat="server" ControlToValidate="txtAbreviatura" 
                        ErrorMessage="Abreviatura" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             <br />
                    <asp:CustomValidator ID="cvLetra" runat="server" 
                        ControlToValidate="txtAbreviatura" 
                        ErrorMessage="No es posible usar esta letra, la misma ya fue usada." 
                        onservervalidate="cvLetra_ServerValidate" ValidationGroup="0"></asp:CustomValidator>
                             </td>
			</tr>
            <%--</form>--%>
			<tr>
				<td class="myLabelIzquierda" colspan="2" ><img alt="" 
                        src="../App_Themes/default/images/icono_ayuda.gif" /></td>
				<td>
                    La abreviatura serivirá como prefijo para la numeración de protocolos 
                    discriminadas por sector.</td>
			</tr>
			<tr>
				<td   colspan="3"><hr /></td>
			</tr>
			<tr>
				<td  >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="SectorList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td   align="right" colspan="2"><asp:Button ID="btnGuardar" 
                        runat="server" onclick="btnGuardar_Click" 
            Text="Guardar" CssClass="myButton" TabIndex="4" 
                        ToolTip="Haga clic aquí para guardar los datos" ValidationGroup="0" />
                </td>
			</tr>
			<tr>
				<td  >
                                            &nbsp;</td>
				<td   align="right" colspan="2">
			
	
                 <asp:ValidationSummary ID="vs" runat="server" 
                     HeaderText="Debe completar los datos marcados como requeridos:" 
                     ShowMessageBox="True" ShowSummary="False" ValidationGroup="0" />
                </td>
			</tr>
			</table>

	
	</div>
	</asp:Content>

