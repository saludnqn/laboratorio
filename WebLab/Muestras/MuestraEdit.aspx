<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MuestraEdit.aspx.cs" Inherits="WebLab.Muestras.MuestraEdit" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 

<br />   &nbsp;
          <div align="left" style="width:1000px">
     
       
		<table width="500px" align="center" class="myTabla">
			<tr>
						<td colspan="2"><b  class="mytituloTabla">TIPO DE MUESTRA</b></td>
						<td align="right"> <a href="../Help/Documentos/Tipo de Muestra.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
			<tr>
						<td colspan="3">    <hr class="hrTitulo" /></td>
					</tr>
			<tr>
			<td class="myLabelIzquierda" >Nombre:</td>
				<td align="left" colspan="2"  >
                    <asp:TextBox ID="txtNombre" runat="server" Width="350px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese el nombre del tipo de muestra" 
                        MaxLength="500"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvNombre" 
                        runat="server" ControlToValidate="txtNombre" 
                        ErrorMessage="RequiredFieldValidator" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
			<td class="myLabelIzquierda" >Codigo Whonet:</td>
				<td align="left" colspan="2"  >
                    <asp:TextBox ID="txtCodigo" runat="server" Width="100px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese el nombre del tipo de muestra" 
                        MaxLength="50"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvNombre0" 
                        runat="server" ControlToValidate="txtCodigo" 
                        ErrorMessage="RequiredFieldValidator" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ControlToValidate="txtCodigo" 
                        ErrorMessage="El código ingresado ya existe. Verifique" 
                        onservervalidate="CustomValidator1_ServerValidate" ValidationGroup="0"></asp:CustomValidator>
                             </td>
			</tr>
			<tr>
				<td   colspan="3"><hr /></td>
			</tr>
			<tr>
				<td  >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="MuestraList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td   align="right">
                    <asp:Button ID="btnGuardar" 
                        runat="server" onclick="btnGuardar_Click" 
            Text="Guardar" CssClass="myButton" 
                        ToolTip="Haga clic aquí para guardar el tipo de muestra" ValidationGroup="0" />
                </td>
				<td   align="right">
                    &nbsp;</td>
			</tr>
			</table>

	
	</div>
	</asp:Content>
