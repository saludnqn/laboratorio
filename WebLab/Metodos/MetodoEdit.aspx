<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MetodoEdit.aspx.cs" Inherits="WebLab.Metodos.MetodoEdit" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<br />   &nbsp;
    
      <div align="left" style="width:1000px">
     
       
		<table width="500px" align="center" class="myTabla">
			<tr>
						<td><b  class="mytituloTabla"> Metodo</b></td>
						<td align="right"> <a href="../Help/Documentos/Métodos.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
			<tr>
						<td colspan="2">    <hr class="hrTitulo" /></td>
					</tr>
			<tr>
			<td class="myLabelIzquierda" >Nombre:</td>
				<td  >
                    <asp:TextBox ID="txtNombre" runat="server" Width="400px" CssClass="myTexto" 
                        TabIndex="1" ToolTip="Ingrese el nombre del método" MaxLength="50"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvNombre" 
                        runat="server" ControlToValidate="txtNombre" 
                        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
				<td   colspan="2"><hr /></td>
			</tr>
			<tr>
				<td  >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="MetodoList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td   align="right">
                    <asp:Button ID="btnGuardar" 
                        runat="server" onclick="btnGuardar_Click" 
            Text="Guardar" CssClass="myButton" ToolTip="Haga clic aquí para guardar el método" />
                </td>
			</tr>
			</table>

	
	</div>
	</asp:Content>
