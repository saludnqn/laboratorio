<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="calendarioView.aspx.cs" Inherits="WebLab.Turnos.calendarioView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../App_Themes/default/style.css" />
</head>

<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label CssClass="myLabelIzquierda" ID="item" runat="server" ></asp:Label><hr />
    <div align="left" style="border: 1px solid #C0C0C0; overflow:scroll; overflow-x:hidden; height:200px; background-color: #F8F8F8; width:220px">
    <asp:gridview CssClass="myLabelIzquierda" ID="gv" runat="server" CellPadding="2" ForeColor="#333333" 
            GridLines="None" EmptyDataText="No hay días habilitados." 
            AutoGenerateColumns="False" CellSpacing="2" Width="200px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Dia" HeaderText="Día"  />
            <asp:BoundField DataField="fecha" HeaderText="Fecha"  DataFormatString="{0:d}" />
            <asp:BoundField DataField="CantidadTurnosDisponibles" 
                HeaderText="Turnos disponibles" />
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Font-Size="10pt" 
            Font-Names="Arial" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:gridview>
        </div>
    </div>
    </form>
</body>
</html>
