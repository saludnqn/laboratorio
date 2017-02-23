<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlEdit.aspx.cs" Inherits="WebLab.Formulas.ControlEdit" MasterPageFile="~/Site1.Master" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <title>LABORATORIO</title>    
    
</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 

    <br />   &nbsp;
    
 <div align="left" style="width:1000px">
       
		<table width="650px" align="center" class="myTabla">
			<tr>
						<td><b class="mytituloTabla">CONTROL</b></td>
						<td align="right"> <a href="../Help/Documentos/Formulas y Controles.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
					</tr>
			<tr>
						<td colspan="2"><hr class="hrTitulo" /></td>
					</tr>
			<tr>
				<td class="myLabelIzquierda" >Analisis:</td>
				<td  >
                            <anthem:TextBox ID="txtCodigo" runat="server" CssClass="myTexto" 
                               style="text-transform:uppercase"   ontextchanged="txtCodigo_TextChanged" 
                                Width="60px" AutoCallBack="True" 
                                TabIndex="1" 
                                ToolTip="Ingrese el codigo del analisis al cual le aplicará el control"></anthem:TextBox>
                    <anthem:DropDownList ID="ddlItem" runat="server" CssClass="myList" TabIndex="2" 
                        ToolTip="Seleccione el analisis para el que aplicará el control" 
                                AutoCallBack="True" onselectedindexchanged="ddlItem_SelectedIndexChanged">
                    </anthem:DropDownList>
                    <asp:RangeValidator ID="rvItem" runat="server" 
                        ControlToValidate="ddlItem" ErrorMessage="Analisis" 
                        MaximumValue="999999" MinimumValue="1" Type="Integer" ValidationGroup="0">*</asp:RangeValidator>
                             <anthem:Label ID="lblMensaje" runat="server" Font-Size="9pt" 
                                ForeColor="#FF3300">
                            </anthem:Label>
                             </td>
			</tr>
		
			<tr>
			<td class="myLabelIzquierda" >Formula:</td>
				<td  >
                    <asp:TextBox ID="txtFormula" runat="server" Width="400px" CssClass="myTexto" 
                        TextMode="MultiLine" TabIndex="3" ToolTip="Ingrese la fórmula"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvFormula" 
                        runat="server" ControlToValidate="txtFormula" 
                        ErrorMessage="Formula" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
				<td class="myLabelIzquierda" >Operación:</td>
				<td  >
                    <asp:DropDownList ID="ddlOperacion" runat="server" CssClass="myList" 
                        TabIndex="4" ToolTip="Seleccione la operación de control">
                        <asp:ListItem Selected="True" Value="0">Seleccione operación</asp:ListItem>
                        <asp:ListItem Value="1">igual a</asp:ListItem>
                        <asp:ListItem Value="2">no igual a</asp:ListItem>
                        <asp:ListItem Value="3">mayor que</asp:ListItem>
                        <asp:ListItem Value="4">mayor o igual que</asp:ListItem>
                        <asp:ListItem Value="5">menor que</asp:ListItem>
                        <asp:ListItem Value="6">menor o igual que</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RangeValidator ID="rvOperacion" runat="server" 
                        ControlToValidate="ddlOperacion" ErrorMessage="Operacion" 
                        MaximumValue="999999" MinimumValue="1" ValidationGroup="0">*</asp:RangeValidator>
                             </td>
			</tr>
				<tr>
				<td class="myLabelIzquierda" >Formula Control:</td>
				<td  >
                    <asp:TextBox ID="txtFormulaControl" runat="server" Width="400px" 
                        CssClass="myTexto" TabIndex="5" ToolTip="Ingrese la fórmula de control"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvFormulaControl" 
                        runat="server" ControlToValidate="txtFormulaControl" 
                        ErrorMessage="Formula Control" ValidationGroup="0">*</asp:RequiredFieldValidator>
                             </td>
			</tr>
			<tr>
				<td colspan="2"  ><hr /></td>
			</tr>
			<tr>
				<td style="vertical-align: top" align="center"    ><img alt="" 
                        src="../App_Themes/default/images/icono_ayuda.gif" /></td>
				<td style="font-size: 11px; font-family: Arial, Helvetica, sans-serif; color: #000000; font-weight: normal;" 
                    align="justify" >
                    &nbsp;Los controles se fijan para validar el ingreso de resultados.
                    <br />1. Seleccione para que analisis se aplica el control.<br />2.
                    Ingrese en el campo formula la expresión matematica que desea controlar.<br />
                    3. Seleccione la operación de control.<br />
                    4. Ingrese la expresión matematica o valor numerico contra el cual desa 
                    comparar.<br />
                    <br />
                    Por ejemplo: Para fijar que la Formula Leucocitaria debe ser igual a 100; 
                    complete los siguientes datos:<br />
                    <li>Analisis: Formula Leucocitaria.</li><br />
                     <li>Formula: [codigo mielocitos]+ [codigo netrofilos]+ [codigo eosinofilos]+ 
                         [codigo basofilos]+ [codigo linfocitos]</li><br />
                   <li>  Operación: es igual</li><br />
                    <li>Formula Control: 100</li></td>
			</tr>
			<tr>
				<td   colspan="2"><hr /></td>
			</tr>
			<tr>
				<td    >
                                            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="myLink" 
                                                PostBackUrl="FormulaList.aspx" CausesValidation="False">Regresar</asp:LinkButton>
                        </td>
				<td   align="right">
                    <asp:Button ID="btnGuardar" 
                        runat="server" 
            Text="Guardar" CssClass="myButton" onclick="btnGuardar_Click" ValidationGroup="0" 
                        TabIndex="6" ToolTip="Haga clic aquí para guardar los datos" />
                </td>
			</tr>
			<tr>
				<td    >
                                            &nbsp;</td>
				<td   align="right">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        HeaderText="Debe completar los datos marcados como requeridos:" 
                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="0" />
                </td>
			</tr>
			</table>

	
	</div>
	</asp:Content>


