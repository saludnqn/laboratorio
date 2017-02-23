<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinSesion.aspx.cs" Inherits="WebLab.FinSesion" %>

<%@ Register src="login.ascx" tagname="login" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sistema de Laboratorio</title>
     
     <link type="text/css"rel="stylesheet"      href="App_Themes/default/style.css" />  
      <link rel="stylesheet" type="text/css" href="App_Themes/default/principal/style.css" />
</head>
<body>
    <form id="form1" runat="server">
   <div align="center" class="myTabla"         
        style="font-family: Arial; font-size: 12px; font-weight: bold; width: 400px;">
        <br />
        	<div class="mytituloGris">
        Sistema de Gestión de Laboratorios de Análisis Clínicos
		<br />
		</div>
		<br />
    La sesión de usuario ha caducado. 
        <br />
   <br />
   Vuelva a identificarse nuevamente en el sistema.
        <br />
        <br />
        <uc1:login ID="login1" runat="server" />
   
    </div>
    </form>
</body>
</html>
