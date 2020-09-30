 <%@ Page Language="C#" Title="Admin" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="RadioWebConfig.Admin"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>

<%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
<%--<head runat="server">
    <title>Admin</title>
              <%--<script src="//ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>--%>

    <%--<link rel="stylesheet" href="Stylesheets/AdminStyle.css" />--%>

<%--</head>--%>


<%--<body>--%>
    <div class="toptext">
        <asp:HyperLink ID="home" runat="server" NavigateUrl="~/Default.aspx" CssClass="homelink">Celab</asp:HyperLink>
    </div>

<%--     <form id="form1">--%>

         <div id="tabs" style="width: 400px">
             <div class="tab-trigger">
             <ul class="tabMenu">
                 <li><label><a class="wide"<%-- id="focused"--%> href="#tab1">Lägg till användare</a></label></li>
                 <li><label><a class="wide" href="#tab2">Ta bort användare</a></label></li>
             </ul> 
             </div>

             <%--<div class="tab-content">--%>

            <div class="adduser-box" id="tab1">
            <h1>Lägg till användare</h1>
            <div class="textboxAdd">
                <i class="fa fa-user" aria-hidden="true"></i>
                <input type="text" autocomplete="on"  id="userInput" runat="server" placeholder="Användarnamn" name="" value=""/>
            </div>
            <div class="textboxAdd">
                <i class="fa fa-lock" aria-hidden="true"></i>
                <input type="password" id="pwInput" placeholder="Lösenord" value="" form="form1" name="pwInputName"/>
               
            </div>
                <div class="textboxAdd">
                    <%--<asp:CompareValidator ID="compareValidator" runat="server" ControlToValidate="pwInput"></asp:CompareValidator>--%>
                <i class="fa fa-lock" aria-hidden="true"></i>
                <input type="password" id="cPassword" placeholder="Bekräfta lösenord" value=""/>
                    <a id="pwmessage"></a>
<%--                                <input type="button" id="btnSubmit" value="Kolla lösen" onclick="Validate()"/>--%>
<%--                                <input type="button" id="btnSubmit" value="Submit"/>--%>


            </div>
                <div>
                    <input type="radio" id="userRadio" name="selectuser" value="0" checked="checked" />
                    <label for="user">User</label>
                    <input type="radio" id="adminRadio" name="selectuser" value="1" />
                    <label for="admin">Admin</label>
                    <input type="radio" id="superUserRadio" name="selectuser" value="2" />
                    <label for="superUser">SuperUser</label>
                </div>
            <asp:Button ID="btn1" class="addBtn" runat="server" onclick="AddUser_Click" Text="Lägg till" disabled="disabled"></asp:Button>
                    <%--<input type="button" class="addBtn" id="btn1" value="Submit" onserverclick="AddUser_Click" disabled="disabled" />--%>
             <a class="errormsg" runat="server" visible="false" id="errorMessage1">Misslyckades att lägga till användare, försök igen</a>
                <a class="errormsg" runat="server" visible="false" id="userNameExists">Användarnamnet finns redan. Försök igen<br /></a>
                <a class="errormsg" runat="server" visible="false" id="forbiddenChars">Ej tillåtet att använda " eller ' </a>
                <a class="errormsg" runat="server" visible="false" id="userAdded">Användare tillagd</a>
                <a class="errormsg" runat="server" visible="false" id="inputEmptyField">Fältet får inte vara tomt</a>


        </div>


            <div class="deleteuser-box" id="tab2">
            <h1>Ta bort användare</h1>
            <div class="textboxAdd">
                <i class="fa fa-user" aria-hidden="true"></i>
                <input type="text" id="deleteUserInput" class="deleteUserClass" runat="server" placeholder="Ange användarnamn" name="" value=""/>
            </div>
          
                <asp:Button ID="btn2" class="addBtn" runat="server" OnClick="DeleteUser_Click" Text="Radera" OnClientClick="ConfirmDelete()" />
             <a class="errormsg" runat="server" visible="false" id="errorMessage2">Hittar ingen användare med det namnet </a>
        </div>
             </div>
             <%--</div>--%>
                     

    <%--</form>--%>

      <script src='js/AddUserPage.js'></script>

<%--</body>--%>
</asp:Content>
<%--</html>--%>
