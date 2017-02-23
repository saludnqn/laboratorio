<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfesionalList.aspx.cs" Inherits="WebLab.Profesionales.ProfesionalList" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

 <link href="../App_Themes/default/style.css" rel="stylesheet" type="text/css" />  
     

   
  
   
    </asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">           
	<br />   &nbsp;
       <div align="left" style="width:1000px">
   
		<table width="600px" align="center" class="myTabla">
			<tr>
			
				<td ><b  class="mytituloTabla">MEDICOS SOLICITANTES</b>
                    </td>
			
			<td align="right"> <a href="../Help/Documentos/Medico Solicitante.htm" target="_blank"  > <img style="border:none;" alt="Ayuda" src="../App_Themes/default/images/information.png" /></a></td>
			</tr>
			<tr>
				<td colspan="2" >
                                    <hr class="hrTitulo" /></td>
			</tr>
			<tr>
				<td colspan="2">

	

                                           <div  style="width:100%;height:450pt;overflow:scroll;overflow-x:hidden;border:1px solid #CCCCCC;"> 
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" DataKeyNames="idProfesional" 
            ForeColor="#333333" onrowcommand="gvLista_RowCommand1" 
            onrowdatabound="gvLista_RowDataBound" Font-Size="8pt" Width="97%" 
                        EmptyDataText="No hay areas creadas" BorderColor="#3A93D2" 
                        BorderStyle="Solid" BorderWidth="3px" GridLines="Horizontal">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="8pt" />
            <Columns>                
           
                
                 <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="numeroDocumento" HeaderText="Documento" />
                        
                <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton ID="Modificar" runat="server" ImageUrl="~/App_Themes/default/images/editar.jpg"
                             CommandName="Modificar" />
                            </ItemTemplate>
                          
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                          
                        </asp:TemplateField>
              
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#3A93D2" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        </div>
                </td>
			</tr>
			<tr>
				<td colspan="2" ><hr /></td>
			</tr>
			<tr>
				<td align="right" colspan="2" >
                                    <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                                        Text="Agregar" Font-Size="10pt" CssClass="myButton" />
                                </td>
			</tr>
			<tr>
				<td colspan="2" >
                                    &nbsp;</td>
			</tr>
			</table>

</div>	

   

</asp:Content>