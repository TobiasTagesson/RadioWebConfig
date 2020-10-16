    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowTrucks.aspx.cs" Inherits="RadioWebConfig.ShowTrucks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Stylesheets/StationTruckStyle.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
            <div class="toptext">
        <asp:HyperLink ID="home" runat="server" NavigateUrl="~/Default.aspx" CssClass="homelink">Celab</asp:HyperLink>
    </div>
        <h1 id="rubrik">Fordon</h1>
<%--        <div id="addTruckBtn"><a id="addLink" href="AddVehicle.aspx?<% =Request.QueryString["name="];%>">Lägg till fordon</a></div>--%>
        <div id="addTruckBtn"><a id="addLink" onclick="goToAddTruck()">Lägg till fordon</a></div>

        <%--<asp:Button ID="addTruckBtn" OnClick="addTruckBtn_Click" Text="Lägg till fordon" runat="server" />--%>
       <%-- <div id="addTruckBtn"><a class='trucks' id='saveAs' href='Default.aspx?Station={station}&Truck={Path.GetFileName(dir)}&SaveAs=True' ></a> onclick="AddTruck()">Lägg till fordon</div>--%>
        <div></div>
        <asp:Label runat="server" ID="lbl" Text=""></asp:Label>
        <asp:Repeater ID="rpt" runat="server" ClientIDMode="AutoID">
        <ItemTemplate>
            <tr class="dt">
                <%--<td class="row"><%#Eval("Folder")%><a href="#"></a></br></td>--%>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
        <div>
        </div>
    </form>
</body>
</html>
        <script type="text/javascript" src='js/AddStation.js'></script>
