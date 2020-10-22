    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowTrucks.aspx.cs" Inherits="RadioWebConfig.ShowTrucks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Stylesheets/StationTruckStyle.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
        <script src="https://kit.fontawesome.com/a076d05399.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="toptext">
            <asp:HyperLink ID="home" runat="server" NavigateUrl="~/Default.aspx" CssClass="homelink">Celab</asp:HyperLink>
             <div class="logout" id="loggingOut"><a href="#" id="LO"><i class="fas fa-undo"></i>Logga ut</a></div>     
        </div>
        <div>
            <div id="semiRubrik">
        <h1 id="rubrik">Fordon  <a> (Stationskod:  <%= station %>)</a></h1>
         </div>
        </div>
        <div id="addTruckBtn"><a id="addLink" onclick="goToAddTruck()">Lägg till fordon</a></div>

        <asp:Label runat="server" ID="lbl" Text=""></asp:Label>
            <asp:Repeater ID="rpt" runat="server" ClientIDMode="AutoID">
            <ItemTemplate>
                <tr class="dt">
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <div id="backtostationlink"><a id="backtostationstext" href="Stations.aspx">Tillbaka till stationssidan</a></div>
        <div>
        </div>
    </form>
</body>
</html>
        <script type="text/javascript" src='js/AddStation.js'></script>
        <script type="text/javascript" src="js/LogOut.js"></script>
