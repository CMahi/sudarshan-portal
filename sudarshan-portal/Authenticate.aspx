<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Authenticate.aspx.cs" Inherits="Authenticate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="JS/utility.js" type="text/javascript"></script>
    <script src="JS/validator.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="display: none" runat="server">
            <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>

            <asp:TextBox ID="txt_Password" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
</html>
