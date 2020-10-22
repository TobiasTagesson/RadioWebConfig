<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddVehicle.aspx.cs" Inherits="RadioWebConfig.AddVehicle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Stylesheets/StationTruckStyle.css" />
        <script src="https://kit.fontawesome.com/a076d05399.js"></script>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
         <div class="toptext">
            <asp:HyperLink ID="home" runat="server" NavigateUrl="~/Default.aspx" CssClass="homelink">Celab</asp:HyperLink>
             <div class="logout" id="loggingOut"><a href="#" id="LO"><i class="fas fa-undo"></i>Logga ut</a></div>     

        </div>
        <div>
            <h1 id="rubrik">Lägg till fordon</h1>
            <input id="truckNoTB" class="truckInput" runat="server"/>

            <asp:Button ID="submitBtn" CssClass="submitBtn" onclick="AddTruck_Click" runat="server" Text="Lägg till" disabled="true"></asp:Button>
            <a id="valmessage"></a>
        </div>
        <div id="backtostationlink"><a id="backtostationstext" href="ShowTrucks.aspx?name=<%= stationNo %>">Tillbaka till fordonssidan</a></div>

    </form>
</body>
</html>
        <script type="text/javascript" src="js/LogOut.js"></script>
        <script type="text/javascript" src="js/AddVehicle.js"></script>

