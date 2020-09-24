<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="RadioWebConfig.LogIn" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <title>LogIn</title>
    <link rel="stylesheet" href="Stylesheets/loginStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
            <div class="login-box">
            <h1>Login</h1>
            <div class="textbox">
                <i class="fa fa-user" aria-hidden="true"></i>
                <input type="text" autocomplete="on" id="userInput" runat="server" placeholder="Användarnamn (Stationskod)" name="" value=""/>
            </div>
            <div class="textbox">
                <i class="fa fa-lock" aria-hidden="true"></i>
                <input type="password" id="pwInput" runat="server" placeholder="Lösenord" name="" value=""/>
               
            </div>
            <asp:Button ID="btn" runat="server" onclick="ValidateUser_Click" Text="Logga in"></asp:Button>
             <a runat="server" visible="false" id="errorMessage">Inloggningen misslyckades, försök igen</a>
        </div>
    </form>
</body>
</html>
