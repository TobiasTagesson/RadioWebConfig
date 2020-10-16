<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddVehicle.aspx.cs" Inherits="RadioWebConfig.AddVehicle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Stylesheets/StationTruckStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Lägg till fordon</h1>
            <input id="truckNoTB" runat="server"/>
        <%--<button id ="submittruck" onclick="AddTruck">Lägg till</button>--%>
            <asp:Button ID="submittruck" CssClass="submitBtn" onclick="AddTruck_Click" runat="server" Text="Lägg till"></asp:Button>
        </div>
    </form>
</body>
</html>
