<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="WebLab.login" %>

<asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" 
        DisplayRememberMe="False" LoginButtonText="Acceder" 
        onauthenticate="Login1_Authenticate" 
        PasswordRequiredErrorMessage="Contraseña es requerida" 
        TitleText="Nueva autenticación de usuario" UserNameLabelText="Usuario:" 
        UserNameRequiredErrorMessage="Usuario es requerido"
                    Width="350px" BorderColor="#E6E2D8" BorderPadding="4" 
    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" 
    ForeColor="#333333">
    <TextBoxStyle Font-Size="0.8em" />
    <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
        BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
    <LayoutTemplate>
        <table border="0" cellpadding="4" cellspacing="0" 
            style="border-collapse:collapse;">
            <tr>
                <td>
                    <table border="0" cellpadding="0" style="width:350px;">
                        <tr>
                            <td align="center" rowspan="5" 
                                style="color:White;font-size:0.9em;font-weight:bold; vertical-align: top;">
                                <img src="App_Themes/default/principal/images/userLogin.jpg" /></td>
                            <td align="center" colspan="2" 
                                style="color:White;background-color:#333333;font-size:0.9em;font-weight:bold;">
                                Nueva autenticación de usuario</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuario:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="Usuario es requerido" 
                                    ToolTip="Usuario es requerido" ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                    ControlToValidate="Password" ErrorMessage="Contraseña es requerida" 
                                    ToolTip="Contraseña es requerida" ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" 
                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" 
                                    Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" Text="Acceder" 
                                    ValidationGroup="ctl00$Login1" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </LayoutTemplate>
    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
    <TitleTextStyle BackColor="#333333" Font-Bold="True" Font-Size="0.9em" 
        ForeColor="White" />
    </asp:Login>
