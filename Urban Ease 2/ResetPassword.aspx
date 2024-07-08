<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Urban_Ease_2.ResetPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Reset Password</h2>
            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelSuccess" runat="server" ForeColor="Green"></asp:Label>
            <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" Placeholder="Enter new password"></asp:TextBox>
            <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" Placeholder="Confirm new password"></asp:TextBox>
            <asp:Button ID="ResetPasswordButton" runat="server" Text="Reset Password" OnClick="ResetPasswordButton_Click" />
        </div>
    </form>
</body>
</html>
