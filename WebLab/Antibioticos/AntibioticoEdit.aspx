<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AntibioticoEdit.aspx.cs" Inherits="WebLab.Antibioticos.AntibioticoEdit" MasterPageFile="~/Site1.Master" %>

 
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<br />   &nbsp;
    
      <div align="left" style="width:1000px">
     
       
		<table width="500px" align="center" class="myTabla">
			<tr>
				<td class="myLabelIzquierda" colspan="2" ><b  class="mytituloTabla">ANTIBIOTICO</b></td>
				<td align="right">
                    <a href="../Help/Documentos/Antibiotico.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" colspan="3" >    <hr class="hrTitulo" /></td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" >Descripción:</td>
				<td>
                    <asp:TextBox ID="txtNombre" runat="server" Width="350px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese el nombre del sector" MaxLength="250"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvNombre" 
                        runat="server" ControlToValidate="txtNombre" 
                        ErrorMessage="Descripción" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
				<td>
                    &nbsp;</td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" >Abreviatura:</td>
				<td>
                    <asp:TextBox ID="txtAbreviatura" runat="server" Width="150px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese la abreviatura" MaxLength="50"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvAbreviatura" 
                        runat="server" ControlToValidate="txtAbreviatura" 
                        ErrorMessage="Abreviatura" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
				<td>
                    &nbsp;</td>
			</tr>
            <%--</form>--%>
			
			<tr>
				<td   colspan="2"><hr /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td  >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="AntibioticoList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td   align="right"><asp:Button ID="btnGuardar" 
                        runat="server" onclick="btnGuardar_Click" 
            Text="Guardar" CssClass="myButton" TabIndex="4" 
                        ToolTip="Haga clic aquí para guardar los datos" ValidationGroup="0" />
                </td>
				<td   align="right">&nbsp;</td>
			</tr>
			<tr>
				<td  >
                                            &nbsp;</td>
				<td   align="right">
			
	
                 <asp:ValidationSummary ID="vs" runat="server" 
                     HeaderText="Debe completar los datos marcados como requeridos:" 
                     ShowMessageBox="True" ShowSummary="False" ValidationGroup="0" />
                </td>
				<td   align="right">
			
	
                    &nbsp;</td>
			</tr>
			</table>

	
	</div>
	</asp:Content>

