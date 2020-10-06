    <%@ Page Title="RadioWebConfig" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RadioWebConfig._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <script runat="server">

        //Method to call javascript functions from menuitems
        void NavigationMenu_MenuItemClick(Object sender, MenuEventArgs e)
        {
            string js = e.Item.Value.ToString();
            //string script = $"<script>{js}


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", js, true);

        }

    </script>

    <%--<body>--%>
        <div class="topbar">
            <header>Celab</header>
              <input type="checkbox" id="check2">
        <label for="check2">
            <i class="fas fa-bars" id="btn2"></i>
            <i class="fas fa-times" id="cancel2"></i>
        </label>
            <div class="menu">
                
            <ul>
           
                
                <li class="download" id="downloadFile"><a href="#"><i class="fas fa-download"></i>Spara</a></li>
                <li class="open" id="openFile"><a href="#" id="openF"><i class="fas fa-folder-open"></i>Öppna</a>
                    <ul id="dropDown">
                    </ul>
                </li>
                <li class="logout" id="loggingOut"><a href="#" id="LO"><i class="fas fa-undo"></i>Logga ut</a></li>     
            </ul>
            </div>
        </div>
        <input type="checkbox" id="check">
        <label for="check">
            <i class="fas fa-bars" id="btn"></i>
            <i class="fas fa-times" id="cancel"></i>
        </label>
         
        <div id="leftBar" class="sidebar">
            <header>RadioWebConfig</header>
            <ul>
                <li class="status" id="statusInfo"><a href="#"><i class="fas fa-clock"></i>Status</a></li>
                <li class="talkgroup" id="talkgroupInfo"><a href="#"><i class="fas fa-phone"></i>Talgrupper</a></li>
                <li class="port" id="portInfo"><a href="#"><i class="fas fa-route"></i>Portar</a></li>
                <li class="shortNumber" id="shortNumberInfo"><a href="#"><i class="fas fa-list-ol"></i>Kortnummer</a></li>
                <li class="Url" id="urlInfo"><a href="#"><i class="fas fa-link"></i>Länkar</a></li>
                <li class="quickBtn" id="quickButtonInfo"><a href="#"><i class="fas fa-running"></i>Snabbknappar</a></li>
                <li class="adminBtn" id="adminInfo"><a href="#"><i class="fas fa-crown"></i>Övrigt</a></li>
                <li class="addUserBtn" id="userInfo"><a href="/Admin2.aspx"><i class="fas fa-pen"></i>Lägg till användare</a></li>
                <li class="preview" id="previewFile"><a href="#"><i class="fas fa-search-plus"></i>Förhandsgranska</a></li>

                

              <%--<asp:Menu ID="mTopMenu" runat="server" OnMenuItemClick="NavigationMenu_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="AdminMenu" NavigateUrl="~/Admin.aspx">
                        <asp:MenuItem Text="Logga ut" Value="loggingOut();"/>
                        <asp:MenuItem Text="Adminsida" NavigateUrl="~/Admin.aspx"/>
                        <asp:MenuItem Text="<span style='cursor:pointer;' id='userInfo' class 'AddUserHeader'>Lägg till USER<span>" Selectable="False" Value="ChangeUser"/>

                        </asp:MenuItem>
                    </Items>
                </asp:Menu>--%>
 
            </ul>
        </div>
         <%--<input type="hidden" runat="server" id="adminProp" value="" />--%>

     <div class="TextboxField">
        <div id="statusButton" hidden="hidden">
                <asp:DataList ID="statusDataList" runat="server" RepeatColumns="5">
                    <ItemTemplate>
                        <div class="InfoStructure">
                           
                        <h1 class="StatusHeader">
                            Status
                        </h1>
                        
                       <p class="row" id="statusName">Namn<asp:TextBox class="statusTB" id="TextBox1" runat="server">-</asp:TextBox></p> 
                       <p class="row" id="status">Status<asp:TextBox class="statusTB" id="TextBox2" runat="server">0</asp:TextBox></p>
                       <p class="row" id="statusDest1">Dest1<asp:TextBox class="statusTB" id="TextBox3" runat="server">0</asp:TextBox></p>
                       <p class="row" id="statusDest2">Dest2<asp:TextBox class="statusTB" id="TextBox4" runat="server">0</asp:TextBox></p>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
        </div>
        <div id="talkgroupButton" hidden="hidden">
                <asp:DataList ID="tgDataList" runat="server" RepeatColumns="5">
                    <ItemTemplate>
                        <div class="InfoStructure">
                        <h1 class="TgHeader">
                            Talgrupp
                        </h1>
                        
                       <p class="row" id="tgName">Namn<asp:TextBox class="tgTB" id="TextBox1" runat="server">-</asp:TextBox></p> 
                       <p class="row" id="gissi">Gissi<asp:TextBox class="tgTB" id="TextBox2" runat="server">0</asp:TextBox></p>
                         </div>
                    </ItemTemplate>
                </asp:DataList>
        </div>
        <div id="portButton" hidden="hidden">
               <asp:DataList ID="portDataList" runat="server" RepeatColumns="5">
                    <ItemTemplate>
                        <div class="InfoStructure">
                        <h1 class="PortHeader">
                            Port
                        </h1>
                        
                       <p class="row" id="portName">Namn<asp:TextBox class="portTB" id="TextBox1" runat="server">-</asp:TextBox></p> 
                       <p class="row" id="portStatus">Status<asp:TextBox class="portTB" id="TextBox2" runat="server">0</asp:TextBox></p>
                       <p class="row" id="portDest">Dest<asp:TextBox class="portTB" id="TextBox3" runat="server">0</asp:TextBox></p>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
        </div>
        <div id="shortNumberButton" hidden="hidden">
                <asp:DataList ID="shortNrDataList" runat="server" RepeatColumns="5">
                    <ItemTemplate>
                        <div class="InfoStructure">
                        <h1 class="ShortNrHeader">
                            Kortnummer
                        </h1>
                        
                       <p class="row" id="shortName">Namn<asp:TextBox class="shortTB" id="TextBox1" runat="server">-</asp:TextBox></p> 
                       <p class="row" id="shortNumber">Nummer<asp:TextBox class="shortTB" id="TextBox2" runat="server">0</asp:TextBox></p>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
        </div>
        <div id="urlButton" hidden="hidden">
                 <asp:DataList ID="urlDataList" runat="server" RepeatColumns="5">
                    <ItemTemplate>
                        <div class="InfoStructure">
                        <h1 class="UrlHeader">
                            Länk
                        </h1>
                        
                       <p class="row" id="urlName">Namn<asp:TextBox class="urlTB" id="TextBox1" runat="server">-</asp:TextBox></p> 
                       <p class="row" id="url">URL<asp:TextBox class="urlTB" id="TextBox2" runat="server">-</asp:TextBox></p>
                           <p class="row" id="urlLink">Besök URL:</p> 
                            <input type="button" id="linkBtn" class="urlHL" onclick="redirect(this.value)" />
                            <%--<input type="text" id="txt" class="urlHL" hidden="hidden"/>--%>
                        </div>
                    </ItemTemplate>

                </asp:DataList>
        </div>
        <div id="quickButton" hidden="hidden">
                <asp:DataList ID="quickButtonDataList" runat="server" RepeatColumns="5">
                    <ItemTemplate>
                        <div class="InfoStructure">
                        <h1 class="QuickHeader">
                            Snabbknapp
                        </h1>
                        
                       <p class="row" id="quickName">Namn<asp:TextBox class="quickTB" id="TextBox1" runat="server">-</asp:TextBox></p> 
                       <p class="row" id="quickStatus">Status<asp:TextBox class="quickTB" id="TextBox2" runat="server">0</asp:TextBox></p>
                       <p class="row" id="quickDest1">Dest1<asp:TextBox class="quickTB" id="TextBox3" runat="server">0</asp:TextBox></p>
                       <p class="row" id="quickDest2">Dest2<asp:TextBox class="quickTB" id="TextBox4" runat="server">0</asp:TextBox></p>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
        </div>


<%--         adminmeny där den inloggade admin-användarens data visas--%>
         


          <div id="adminButton" hidden="hidden">
                <asp:DataList ID="adminDataList" runat="server" RepeatColumns="1">
                    <ItemTemplate> 
                        <div class="InfoStructure">
                        <h1 class="AdminHeader">
                            Adminknapp
                        </h1>

                       <p class="row" id="adminName">Namn<asp:TextBox class="adminTB" id="TextBox1" runat="server">-</asp:TextBox></p> 
                       <p class="row" id="adminLicense">Licensnummer<asp:TextBox class="adminTB" id="TextBox2" runat="server">-</asp:TextBox></p>
                       <p class="row" id="adminOrgNr">OrgNr/Stationskod<asp:TextBox class="adminTB" id="TextBox3" runat="server">-</asp:TextBox></p>
                       <p class="row" id="admin ISSI">ISSI<asp:TextBox class="adminTB" id="TextBox4" runat="server">-</asp:TextBox></p>

                       
                        </div>
                    </ItemTemplate>
                </asp:DataList>
              
        </div>
         <asp:HiddenField ID="AdminHidden" runat="server" />

         <div id="addUserButton" hidden="hidden">
                <asp:DataList ID="addCustomerDataList" runat="server">
                    <ItemTemplate> 
                        <div class="InfoStructure">
                        <h1 class="AddUserHeader">
                            LäggTillAnvändare
                        </h1>
                      <%-- <p class="row" id="userName">Namn<asp:TextBox class="nameTB" id="TextBox1" runat="server">-</asp:TextBox></p> 
                       <p class="row" id="password">Lösenord<asp:TextBox class="userPasswordTB" id="TextBox2" runat="server">-</asp:TextBox></p> 
                       <p class="row" id="userLicense">Licensnummer<asp:TextBox class="userLicenseTB" id="TextBox3" runat="server">-</asp:TextBox></p>
                       <p class="row" id="userOrgNr">OrgNr<asp:TextBox class="userOrgNrTB" id="TextBox4" runat="server">-</asp:TextBox></p>
                       <p class="row" id="userISSI">ISSI(Stationskod?)<asp:TextBox class="userISSI" id="TextBox5" runat="server">-</asp:TextBox></p>--%>
                       
                        </div>
                    </ItemTemplate>
                </asp:DataList>
              
        </div>
    </div>
        <script type='text/javascript' src='js/Status.js'></script>
        <script type='text/javascript' src='js/Talkgroups.js'></script>
        <script type='text/javascript' src='js/Port.js'></script>
        <script type='text/javascript' src='js/ShortNumber.js'></script>
        <script type='text/javascript' src='js/Url.js'></script>
        <script type='text/javascript' src='js/QuickButtons.js'></script>
        <script type='text/javascript' src='js/index.js'></script>
        <script type='text/javascript' src='js/SaveFile.js'></script>
        <script type='text/javascript' src='js/OpenFile.js'></script>
        <script type='text/javascript' src='js/LogOut.js'></script>
        <script type="text/javascript" src='js/Admin.js'></script>
        <script type="text/javascript" src='js/AddUser.js'></script>


    <%--</body>--%>
</asp:Content>