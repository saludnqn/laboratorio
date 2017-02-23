<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncidenciaEdit.ascx.cs" Inherits="WebLab.Calidad.IncidenciaEdit" %>

		<table>
     
		
<tr>
		<td align="left">
                                                   
                                                                <asp:TreeView ID="TreeView2" runat="server" ImageSet="Arrows">
                                                                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                                    <Nodes>
                                                                        <asp:TreeNode Expanded="False" ShowCheckBox="True" 
                                                                            Text="Muestra mal identificada" Value="9"></asp:TreeNode>
                                                                        <asp:TreeNode ShowCheckBox="True" Text="Muestra insuficiente" Value="10">
                                                                        </asp:TreeNode>
                                                                        <asp:TreeNode Expanded="False" SelectAction="Expand" 
                                                                            Text="Muestra de sangre en condición inadecuada" Value="11">
                                                                            <asp:TreeNode ShowCheckBox="True" Text="coagulada" Value="12"></asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="hemolizada" Value="13"></asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="lipémica" Value="14"></asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="con interferentes" Value="15">
                                                                            </asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="ictérica" Value="16"></asp:TreeNode>
                                                                        </asp:TreeNode>
                                                                        <asp:TreeNode Expanded="False" SelectAction="Expand" ShowCheckBox="False" 
                                                                            Text="Muestra de orina en condición inadecuada" Value="17">
                                                                            <asp:TreeNode ShowCheckBox="True" Text="contaminada" Value="18"></asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="mal recolectada" Value="19">
                                                                            </asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="mal conservada" Value="20">
                                                                            </asp:TreeNode>
                                                                        </asp:TreeNode>
                                                                        <asp:TreeNode Expanded="False" SelectAction="Expand" ShowCheckBox="False" 
                                                                            Text="Muestra de materia fecal en condición inadecuada (Microbiologia)" Value="28">
                                                                            <asp:TreeNode ShowCheckBox="True" Text="escasa muestra" Value="29">
                                                                            </asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="mal conservada" Value="30">
                                                                            </asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="mal recolectada" Value="31">
                                                                            </asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="no corresponde (heces formes)" Value="32">
                                                                            </asp:TreeNode>
                                                                        </asp:TreeNode>
                                                                         <asp:TreeNode Expanded="False" SelectAction="Expand" ShowCheckBox="False" 
                                                                            Text="Líquido de punción en condición inadecuada (Microbiologia)" Value="33">
                                                                            <asp:TreeNode ShowCheckBox="True" Text="coagulado" Value="34">
                                                                            </asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="hemolizado" Value="35">
                                                                            </asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="escasa muestra" Value="36">
                                                                            </asp:TreeNode>
                                                                        </asp:TreeNode>
                                                                         <asp:TreeNode Expanded="False" SelectAction="Expand" ShowCheckBox="False" 
                                                                            Text="Muestras derivadas en condiciones inadecuadas" Value="21">
                                                                            <asp:TreeNode ShowCheckBox="True" Text="mal conservadas" Value="22">
                                                                            </asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="no cumple Bioseguridad" Value="23">
                                                                            </asp:TreeNode>
                                                                            <asp:TreeNode ShowCheckBox="True" Text="tipo de muestra incorrecta" Value="24">
                                                                            </asp:TreeNode>
                                                                        </asp:TreeNode>
                                                                        <asp:TreeNode ShowCheckBox="True" Text="Muestra no recibida" Value="25">
                                                                        </asp:TreeNode>
                                                                        <asp:TreeNode ShowCheckBox="True" Text="Datos personales mal ingresados al SIL" 
                                                                            Value="26"></asp:TreeNode>
                                                                        <asp:TreeNode ShowCheckBox="True" 
                                                                            Text="Error en ingreso de determinaciones al SIL" Value="27"></asp:TreeNode>
                                                                    </Nodes>
                                                                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="25px" 
                                                                        HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                                                    <ParentNodeStyle Font-Bold="False" />
                                                                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
                                                                        HorizontalPadding="0px" VerticalPadding="0px" />
                                                                </asp:TreeView>
                                                            </td>
</tr>

<tr>
		<td><hr /></td>
</tr>

                                            			                                        
        </table>
