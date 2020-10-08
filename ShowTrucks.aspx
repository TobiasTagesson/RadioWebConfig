<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowTrucks.aspx.cs" Inherits="RadioWebConfig.ShowTrucks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label runat="server" ID="lbl" Text="Fordon:"></asp:Label>
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
