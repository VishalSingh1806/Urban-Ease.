<%@ Page Title="Add Partner" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="add_partner.aspx.cs" Inherits="Urban_Ease_2.admin.add_partner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            margin-left: 300px;
            margin-right:300px;
        }
        
        .form-group {
            margin-bottom: 15px;
        }
        .form-control {
            width: 100%;
            padding: 10px;
            font-size: 14px;
        }
        .btn-primary {
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
        }
        .btn-primary:hover {
            background-color: #0056b3;
        }
        h2 {
            margin-left: 300px;
        }
        .error-message {
            color: red;
            font-size: 12px;
        }
    </style>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script>
        function syncPassword() {
            var passwordField = document.getElementById('<%= Password.ClientID %>');
            var hiddenPasswordField = document.getElementById('<%= HiddenPassword.ClientID %>');
            hiddenPasswordField.value = passwordField.value;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Add New Partner</h2>
    <asp:Panel ID="AddPartnerPanel" CssClass="form-container" runat="server">
        <div class="form-group">
            <label for="FirstName">First Name</label>
            <asp:TextBox ID="FirstName" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label ID="FirstNameError" CssClass="error-message" runat="server"></asp:Label>
        </div>
        <div class="form-group">
            <label for="LastName">Last Name</label>
            <asp:TextBox ID="LastName" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label ID="LastNameError" CssClass="error-message" runat="server"></asp:Label>
        </div>
        <div class="form-group">
            <label for="Password">Password</label>
            <asp:TextBox ID="Password" CssClass="form-control" runat="server" TextMode="Password" oninput="syncPassword()"></asp:TextBox>
            <asp:Label ID="PasswordError" CssClass="error-message" runat="server"></asp:Label>
            <asp:HiddenField ID="HiddenPassword" runat="server" />
        </div>
        <div class="form-group">
            <label for="ContactNumber">Contact Number</label>
            <asp:TextBox ID="ContactNumber" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label ID="ContactNumberError" CssClass="error-message" runat="server"></asp:Label>
        </div>
        <div class="form-group">
            <label for="Email">Email</label>
            <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label ID="EmailError" CssClass="error-message" runat="server"></asp:Label>
        </div>
        <div class="form-group">
            <label for="WorkCategory">Work Category</label>
            <asp:DropDownList ID="WorkCategory" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="WorkCategory_SelectedIndexChanged">
                <asp:ListItem Text="Select Category" Value=""></asp:ListItem>
                <asp:ListItem Text="Woman's Section" Value="Woman's Section"></asp:ListItem>
                <asp:ListItem Text="Man's Care" Value="Man's Care"></asp:ListItem>
                <asp:ListItem Text="Repair & Maintenance" Value="repair"></asp:ListItem>
                <asp:ListItem Text="Cleaning & Pest Control" Value="Cleaning & Pest Control"></asp:ListItem>
                <asp:ListItem Text="Electrician, Plumber & Carpenter" Value="Electrician, Plumber & Carpenter"></asp:ListItem>
                <asp:ListItem Text="Native Water Purifier" Value="Native Water Purifier"></asp:ListItem>
                <asp:ListItem Text="Smart Locks" Value="Smart Locks"></asp:ListItem>
                <asp:ListItem Text="Painting & Waterproofing" Value="Painting & Waterproofing"></asp:ListItem>
                <asp:ListItem Text="Wall Decor" Value="Wall Decor"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="WorkCategoryError" CssClass="error-message" runat="server"></asp:Label>
        </div>
        <div class="form-group">
            <label for="SubCategory">Subcategory</label>
            <asp:DropDownList ID="SubCategory" CssClass="form-control" runat="server"></asp:DropDownList>
            <asp:Label ID="SubCategoryError" CssClass="error-message" runat="server"></asp:Label>
        </div>
        <div class="form-group">
            <label for="Location">Location</label>
            <asp:TextBox ID="Location" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label ID="LocationError" CssClass="error-message" runat="server"></asp:Label>
        </div>
       
        <div class="form-group">
            <asp:Button ID="AddPartnerButton" CssClass="btn-primary" runat="server" Text="Add Partner" OnClick="AddPartnerButton_Click" />
        </div>
    </asp:Panel>
</asp:Content>
