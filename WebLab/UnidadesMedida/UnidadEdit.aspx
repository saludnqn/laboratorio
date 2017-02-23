<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnidadEdit.aspx.cs" Inherits="WebLab.UnidadesMedida.UnidadEdit" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<br />   &nbsp;
    
         <div align="left" style="width:1000px">
     
       
		<table width="500px" align="center" class="myTabla">
			<tr>
						<td colspan="2"><b  class="mytituloTabla">Unidad de Medida</b></td>
					<td   align="right">
                    <a href="../Help/Documentos/Unidad de Medida.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
			<tr>
						<td colspan="3">    <hr class="hrTitulo"  /></td>
					</tr>
			<tr>
			<td class="myLabelIzquierda" >Nombre:</td>
				<td colspan="2"  >
                    <asp:TextBox ID="txtNombre" runat="server" Width="284px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese el nombre de la unidad de medida" 
                        MaxLength="50"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                        runat="server" ControlToValidate="txtNombre" 
                        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
				<td   colspan="3"><hr /></td>
			</tr>
			<tr>
				<td  >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="UnidadList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td  align="right"><asp:Button ID="btnGuardar" 
                        runat="server" onclick="btnGuardar_Click" 
            Text="Guardar" CssClass="myButton" TabIndex="2" 
                        ToolTip="Haga clic aquí para guardar" />
                </td>
				<td  align="right">&nbsp;</td>
			</tr>
			</table>

	</div>
	</asp:Content>