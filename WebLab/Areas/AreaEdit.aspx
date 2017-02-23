<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaEdit.aspx.cs" Inherits="WebLab.Areas.AreaEdit" MasterPageFile="~/Site1.Master" %>


<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
 <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
</asp:Content>


 
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<br />   &nbsp;
    <div align="left" style="width:1000px">
  
       
		<table width="500px" align="center" class="myTabla">
			<tr>
						<td><b  class="mytituloTabla">Area</b></td>
						<td align="right"> <a href="../Help/Documentos/Área.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
			<tr>
						<td colspan="2"><hr class="hrTitulo" /></td>
					</tr>
			<tr>
				<td class="myLabelIzquierda" >Nombre:</td>
				<td  >
                    <asp:TextBox ID="txtNombre" runat="server" Width="284px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese el nombre del area" MaxLength="50"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvNombre" 
                        runat="server" ControlToValidate="txtNombre" 
                        ErrorMessage="Nombre" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" >Tipo de Servicio:</td>
				<td  >
                    <asp:DropDownList ID="ddlTipoServicio" runat="server" CssClass="myList" 
                        TabIndex="2" ToolTip="Seleccione el servicio">
                    </asp:DropDownList>
                    <asp:RangeValidator ID="rvTipoServicio" runat="server" 
                        ControlToValidate="ddlTipoServicio" ErrorMessage="TipoServicio" 
                        MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">*</asp:RangeValidator>
                             </td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" >Imprimir Código de Barras:</td>
				<td  >
                    <asp:CheckBox ID="chkImprimeCodigoBarras" runat="server" />
                   </td>
			</tr>
			<tr>
				<td   colspan="2"><hr /></td>
			</tr>
			<tr>
				<td  >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="AreaList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td   align="right"><asp:Button ID="btnGuardar" 
                        runat="server" onclick="btnGuardar_Click" 
            Text="Guardar" CssClass="myButton" TabIndex="4" 
                        ToolTip="Haga clic aquí para guardar los datos" ValidationGroup="0" />
                </td>
			</tr>
			<tr>
				<td  >
                                            &nbsp;</td>
				<td   align="right">
			
	
                 <asp:ValidationSummary ID="vs" runat="server" 
                     HeaderText="Debe completar los datos marcados como requeridos:" 
                     ShowMessageBox="True" ShowSummary="False" ValidationGroup="0" />
                </td>
			</tr>
			</table>

	
		</div>
	</asp:Content>
