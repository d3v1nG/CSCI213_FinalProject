<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="FinalProject.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="width: 1604px">
    <h1>Hospital Web Dashboard Logon<br />
    </h1>
    <p>
        &nbsp;</p>
    <form id="form1" runat="server">
        <p>
            <asp:Login ID="LoginWidget" runat="server" Height="173px" OnAuthenticate="Login1_Authenticate" Width="645px">
            </asp:Login>
        </p>
    </form>
</body>
</html>
