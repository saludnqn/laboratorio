<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObservacionEdit.aspx.cs" Inherits="WebLab.Observaciones.ObservacionEdit" MasterPageFile="~/Site1.Master" %>

 
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<br />   &nbsp;
    
    <div align="left" style="width:1000px">
       
		<table align="center" class="myTabla" style="width: 600px">
			<tr>
				<td class="myLabelIzquierda" colspan="2"><b  class="mytituloTabla">OBSERVACION CODIFICADAS</b></td>
				<td align="right" style="width: 4px" >
                    &nbsp;</td>
				<td align="right" style="width: 147px">
                    <a href="../Help/Documentos/Servicios.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" colspan="4" ><hr /></td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" style="vertical-align: top"  >Tipo de Servicio:</td>
				<td class="myLabelIzquierda" colspan="3"  >
                  <asp:DropDownList ID="ddlTipoServicio" runat="server" CssClass="myList" 
                        TabIndex="2" ToolTip="Seleccione el servicio">
                    </asp:DropDownList>
                    <asp:RangeValidator ID="rvTipoServicio" runat="server" 
                        ControlToValidate="ddlTipoServicio" ErrorMessage="Tipo de Servicio" 
                        MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">*</asp:RangeValidator></td>
			</tr>
		
			<tr>
				<td class="myLabelIzquierda" style="width: 103px" >Código:</td>
				<td class="myLabelIzquierda" style="width: 328px" >
                    <asp:TextBox ID="txtAbreviatura" runat="server" Width="100px" CssClass="myTexto" 
                        TabIndex="2" ToolTip="Ingrese el codigo" MaxLength="5"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvAbreviatura" 
                        runat="server" ControlToValidate="txtAbreviatura" 
                        ErrorMessage="Codigo" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
				<td colspan="2">
                    &nbsp;</td>
			</tr>
        
			<tr>
				<td class="myLabelIzquierda" style="vertical-align: top"  >Descripción:</td>
				<td colspan="3"  >
                    <asp:TextBox ID="txtNombre"  runat="server" Width="400px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese la descripcion a imprimir" MaxLength="500" 
                        TextMode="MultiLine" Rows="5"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvNombre" 
                        runat="server" ControlToValidate="txtNombre" 
                        ErrorMessage="Descripcion" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
				<td   colspan="4"><hr /></td>
			</tr>
			<tr>
				<td style="width: 250px" colspan="2"  >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="ObservacionList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td   align="right" colspan="2">
                    <asp:Button ID="btnGuardar" 
                        runat="server" onclick="btnGuardar_Click" 
            Text="Guardar" CssClass="myButton" TabIndex="3" 
                        ToolTip="Haga clic aquí para guardar los datos" ValidationGroup="0" />
                </td>
			</tr>
			<tr>
				<td colspan="4"  >
			
	
                 <asp:ValidationSummary ID="vs" runat="server" 
                     HeaderText="Debe completar los datos marcados como requeridos:" 
                     ShowMessageBox="True" ShowSummary="False" ValidationGroup="0" />
                </td>
			</tr>
			</table>

	</div>
	</asp:Content>

