<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GermenEdit.aspx.cs" Inherits="WebLab.Germenes.GermenEdit" MasterPageFile="~/Site1.Master" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<br />   &nbsp;
    
          <div align="left" style="width:1000px">
     
       
		<table width="500px" align="center" class="myTabla">
			<tr>
						<td colspan="2"><b  class="mytituloTabla"> MICROORGANISMOS </b>&nbsp;</td>
						<td align="right"> 
						<a href="../Help/Documentos/Microorganismo.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
			<tr>
						<td colspan="3">    <hr class="hrTitulo" /></td>
					</tr>
			<tr>
			<td class="myLabelIzquierda" >Codigo:</td>
				<td  >
                   <anthem:TextBox ID="txtCodigo" runat="server" MaxLength="50" 
                                Width="150px" style="text-transform:uppercase"  
                                ToolTip="Ingrese el codigo del analisis" 
                                CssClass="myTexto" ontextchanged="txtCodigo_TextChanged" 
                                AutoCallBack="True" TabIndex="1" />&nbsp;&nbsp;   
                            <anthem:Label ID="lblMensajeCodigo" runat="server" Font-Bold="True" 
                                ForeColor="#CC3300" Visible="False" TabIndex="100">El codigo ya existe. Verifique.</anthem:Label></td>
				<td  >
                    &nbsp;</td>
			</tr>
			<tr>
			<td class="myLabelIzquierda" >Nombre:</td>
				<td  >
                    <asp:TextBox ID="txtNombre" runat="server" Width="400px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese el nombre del germen" MaxLength="250"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvNombre" 
                        runat="server" ControlToValidate="txtNombre" 
                        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                             </td>
				<td  >
                    &nbsp;</td>
			</tr>
			<tr>
				<td   colspan="3"><hr /></td>
			</tr>
			<tr>
				<td  >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="GermenList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td   align="right">
                    <asp:Button ID="btnGuardar" 
                        runat="server" onclick="btnGuardar_Click" 
            Text="Guardar" CssClass="myButton" ToolTip="Haga clic aquí para guardar el germen" />
                </td>
				<td   align="right">
                    &nbsp;</td>
			</tr>
			</table>

	
		</div>
	</asp:Content>
