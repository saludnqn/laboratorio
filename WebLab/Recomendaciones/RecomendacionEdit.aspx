<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecomendacionEdit.aspx.cs" Inherits="WebLab.Recomendaciones.RecomendacionEdit" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<br />   &nbsp;
    
       <div align="left" style="width:1000px">
     
       
		<table width="600px" align="center" class="myTabla">
			<tr>
						<td><b class="mytituloTabla">Recomendacion</b></td>
						<td align="right"> <a href="../Help/Documentos/Recomendaciones.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
			<tr>
						<td colspan="2">    <hr class="hrTitulo" /></td>
					</tr>
			<tr>
				<td class="myLabelIzquierda" >Nombre</td>
				<td  >
                    <asp:TextBox ID="txtNombre" runat="server" Width="284px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese el nombre de la recomendación"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvNombre" 
                        runat="server" ControlToValidate="txtNombre" 
                        ErrorMessage="Nombre" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" style="vertical-align: top"  >Descripción detallada:</td>
				<td  >
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" 
                        Width="100%" CssClass="myTextoArea" Height="150px" TabIndex="2" 
                        ToolTip="Ingrese la descripción de la recomendación"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescripcion" 
                        runat="server" ControlToValidate="txtDescripcion" 
                        ErrorMessage="Descripcion" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
				<td   colspan="2"><hr /></td>
			</tr>
			<tr>
				<td  >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="RecomendacionList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td   align="right">
                    <asp:Button ID="btnGuardar" 
                        runat="server" 
            Text="Guardar" CssClass="myButton" onclick="btnGuardar_Click" ValidationGroup="0" 
                        TabIndex="3" ToolTip="Haga clic aquí para guardar" />
                </td>
			</tr>
			<tr>
				<td  >
                                            &nbsp;</td>
				<td   align="right">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        HeaderText="Debe completar los datos marcados como obligatorios:" 
                        ValidationGroup="0" ShowMessageBox="True" ShowSummary="False" />
                </td>
			</tr>
			</table>

	
		</div>
	</asp:Content>
