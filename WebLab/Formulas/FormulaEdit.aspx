<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormulaEdit.aspx.cs" Inherits="WebLab.Formulas.FormulaEdit" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>


<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
  <script type="text/javascript" src="../script/Mascara.js"></script>
    <script type="text/javascript" src="../script/ValidaFecha.js"></script>   
</asp:Content>


<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<br />   &nbsp;
    
 <div align="left" style="width:1000px">
     
       
		<table align="center" class="myTabla" style="width: 650px">
			<tr>
						<td><b class="mytituloTabla">FORMULA</b></td>
						<td align="right"> <a href="../Help/Documentos/Formulas y Controles.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
			<tr>
						<td colspan="2"><hr class="hrTitulo" /></td>
					</tr>
			<tr>
				<td class="myLabelIzquierda" >Analisis:</td>
				<td  >
                            <anthem:TextBox ID="txtCodigo" runat="server" CssClass="myTexto" 
                               style="text-transform:uppercase"   
                        ontextchanged="txtCodigo_TextChanged" Width="60px" AutoCallBack="True" 
                                TabIndex="1" 
                        ToolTip="Ingrese el codigo del analisis al cual le aplicará el control"></anthem:TextBox>
                    <anthem:DropDownList ID="ddlItem" runat="server" CssClass="myList" TabIndex="2" 
                        ToolTip="Seleccione el analisis para el cual se aplicará la formula" 
                                AutoCallBack="True" onselectedindexchanged="ddlItem_SelectedIndexChanged">
                    </anthem:DropDownList>
                    <asp:RangeValidator ID="rvItem" runat="server" 
                        ControlToValidate="ddlItem" ErrorMessage="Analisis" 
                        MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">*</asp:RangeValidator>
                             <anthem:Label ID="lblMensaje" runat="server" Font-Size="9pt" 
                                ForeColor="#FF3300"></anthem:Label>
                             </td>
			</tr>
			<tr>
			<td class="myLabelIzquierda" style="vertical-align: top" >Formula:</td>
				<td  >
                    <asp:TextBox ID="txtFormula" runat="server" Width="422px" CssClass="myTexto" 
                        TextMode="MultiLine" TabIndex="3" ToolTip="Ingrese la formula"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvFormula" 
                        runat="server" ControlToValidate="txtFormula" 
                        ErrorMessage="Formula" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
				<td colspan="2"  ><hr /></td>
			</tr>
					<tr>
						<td class="myLabelIzquierda" colspan="2" >Condicion de Aplicación</td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Sexo:</td>
						<td>
                            <anthem:DropDownList ID="ddlSexo" runat="server" 
                                ToolTip="Seleccione el sexo" CssClass="myList" AutoCallBack="True" 
                                TabIndex="4">
                                <asp:ListItem Selected="True" Value="I">Ambos Sexos</asp:ListItem>
                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                            </anthem:DropDownList>
                                        
                                            </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Unidad Edad:</td>
						<td>
                                 <asp:DropDownList ID="ddlUnidadEdad" runat="server" CssClass="myList" 
                                     TabIndex="7" ToolTip="Seleccione la unidad de la edad" 
                                AutoPostBack="True" onselectedindexchanged="ddlUnidadEdad_SelectedIndexChanged">
                                     <asp:ListItem Selected="True" Value="-1">Todas las Edades</asp:ListItem>
                                     <asp:ListItem Value="0">Años</asp:ListItem>
                                     <asp:ListItem Value="1">Meses</asp:ListItem>
                                     <asp:ListItem Value="2">Días</asp:ListItem>
                                 </asp:DropDownList>
                                        </td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Edad Desde:<asp:RequiredFieldValidator 
                                ID="rfvEdadDesde" runat="server" 
                                ControlToValidate="txtEdadDesde" ErrorMessage="Edad Desde" 
                                ValidationGroup="0">*</asp:RequiredFieldValidator>
                                        
                        </td>
						<td>
                            <input id="txtEdadDesde" name="txtEdadDesde" type="text" runat="server" 
                                onblur="valNumeroSinPunto(this)" class="myTexto" style="width: 40px" 
                                tabindex="5" /></td>
					</tr>
					<tr>
						<td class="myLabelIzquierda" >Edad Hasta:<asp:RequiredFieldValidator 
                                ID="rfvEdadHasta" runat="server" 
                                ControlToValidate="txtEdadHasta" ErrorMessage="Edad Hasta" 
                                ValidationGroup="0">*</asp:RequiredFieldValidator>
                                            </td>
						<td>
                            <input id="txtEdadHasta" type="text" runat="server" onblur="valNumeroSinPunto(this)" 
                                class="myTexto" style="width: 40px" tabindex="6" /> </td>
						</tr>
					<tr>
				<td colspan="2"  ><hr /></td>
			</tr>
			<tr>
				<td style="vertical-align: top" align="left"    >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="FormulaList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td class="myLabel" align="right" >
                    <asp:Button ID="btnGuardar" 
                        runat="server" 
            Text="Guardar" CssClass="myButton" onclick="btnGuardar_Click" ValidationGroup="0" 
                        TabIndex="4" ToolTip="Haga clic aquí para guardar los datos" />
                    </td>
			</tr>
			<tr>
				<td style="vertical-align: top" align="center"    >&nbsp;</td>
				<td class="myLabel" align="justify" >
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        HeaderText="Debe completar los datos marcados como requeridos:" 
                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="0" />
                    </td>
			</tr>
			
			</table>

	
	</div>
	</asp:Content>

