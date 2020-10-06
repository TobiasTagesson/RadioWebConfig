<%@ Page Language="C#" Title="Admin" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Admin2.aspx.cs" Inherits="RadioWebConfig.Admin2" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>
            <link rel="stylesheet" href="Stylesheets/AdminStyle.css" />

    <div class="toptext">
        <asp:HyperLink ID="home" runat="server" NavigateUrl="~/Default.aspx" CssClass="homelink">Celab</asp:HyperLink>
    </div>
    <div class="container">
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
                <i class="fa fa-lock" aria-hidden="true"></i>
                <input type="password" id="cPassword" placeholder="Bekräfta lösenord" value=""/>
                    <a id="pwmessage"></a>

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
             <a class="errormsg" runat="server" visible="false" id="errorMessage1">Misslyckades att lägga till användare, försök igen</a>
                <a class="errormsg" runat="server" visible="false" id="userNameExists">Användarnamnet finns redan. Försök igen<br /></a>
                <a class="errormsg" runat="server" visible="false" id="forbiddenChars">Otillåtna tecken i lösenordet</a>
                <a class="errormsg" runat="server" visible="false" id="userAdded">Användare tillagd</a>
                <a class="errormsg" runat="server" visible="false" id="inputEmptyField">Fältet får inte vara tomt</a>


        </div>

            <div class="deleteuser-box" id="tab2">
            <h1>Ta bort användare</h1>
            <div class="textboxAdd">
                <i class="fa fa-user" aria-hidden="true"></i>
                <input type="text" id="deleteUserInput" class="deleteUserClass" runat="server" placeholder="Ange användarnamn" name="" value=""/>
            </div>
          
                <asp:Button ID="btn2" class="delBtn" runat="server" OnClick="DeleteUser_Click" Text="Radera" OnClientClick="ConfirmDelete()" />
             <a class="errormsg" runat="server" visible="false" id="errorMessage2">Hittar ingen användare med det namnet </a>
        </div>

                </div>     


      <script src='js/AddUserPage.js'></script>

</asp:Content>
