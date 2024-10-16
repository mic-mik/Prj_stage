<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayFineSuccess.aspx.cs" Inherits="PayFineSuccess" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Success</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Payment Successful</h2>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <br />
            <a href="PayFine.aspx">Back to Fines</a>
        </div>
    </form>
</body>
</html>
