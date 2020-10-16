<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stations.aspx.cs" Inherits="RadioWebConfig.Stations" %>

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

        <h1 id="rubrik">Stationer</h1>
        <button id="addStationBtn" onclick="AddStation()">Lägg till station</button>
             <asp:Label runat="server" ID="lbl" Text=""></asp:Label>
<%--    <asp:Repeater ID="rpt" runat="server" ClientIDMode="AutoID">
        <ItemTemplate>
            <tr class="dt">
                <td class="row"><%#Eval("Folder")%><a href="#"></a></br></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>--%>

        <%--<asp:ListBox ID="foldersList" runat="server">
           
        </asp:ListBox>--%>


    <%--    <div id="stationcode" runat="server">
            <a href="#"></a>
        </div>--%>
      
    </form>
</body>
</html>
        <script type="text/javascript" src='js/AddStation.js'></script>

