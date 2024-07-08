<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="add_services.aspx.cs" Inherits="Urban_Ease_2.admin.add_services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Include any specific head content here -->
    <style>
        /* Additional styles for the add_services.aspx page */
        .form-container {
            max-width: 600px;
            margin: 50px auto; /* Center the form horizontally with top margin */
            padding: 30px;
            background-color: #f9f9f9;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .form-group {
            margin-bottom: 20px;
        }
        .form-label {
            font-weight: bold;
            display: block; /* Display labels as block elements */
            margin-bottom: 8px;
        }
        .form-control {
            width: 100%;
            padding: 12px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 16px;
            box-sizing: border-box; /* Include padding in width calculation */
        }
        .btn-add-service {
            width: 100%;
            padding: 12px;
            font-size: 16px;
            border: none;
            border-radius: 5px;
            background-color: #007bff;
            color: #fff;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .btn-add-service:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2 style="text-align: center; margin-bottom: 30px;">Add New Service</h2>
        <div class="form-group">
            <label class="form-label" for="categoryDropdown">Category:</label>
            <asp:DropDownList ID="categoryDropdown" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="categoryDropdown_SelectedIndexChanged">
               
                <asp:ListItem Text="Woman's Section" Value="1"></asp:ListItem>
                <asp:ListItem Text="Man's Care" Value="2"></asp:ListItem>
                <asp:ListItem Text="Repair" Value="3"></asp:ListItem>
                <asp:ListItem Text="Cleaning & Pest Control" Value="4"></asp:ListItem>
                <asp:ListItem Text="Electrician, Plumber & Carpenter" Value="5"></asp:ListItem>
                <asp:ListItem Text="Native Water Purifier" Value="6"></asp:ListItem>
                <asp:ListItem Text="Smart Locks" Value="7"></asp:ListItem>
                <asp:ListItem Text="Painting & Waterproofing" Value="8"></asp:ListItem>
                <asp:ListItem Text="Wall Decor" Value="9"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label class="form-label" for="serviceNameDropdown">Service Name:</label>
            <asp:DropDownList ID="serviceNameDropdown" runat="server" CssClass="form-control">
               
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label class="form-label" for="servicePrice">Price:</label>
            <asp:TextBox ID="servicePrice" runat="server" CssClass="form-control" placeholder="Enter price"></asp:TextBox>
        </div>
        <div class="form-group">
            <label class="form-label" for="partnerName">Partner Name:</label>
            <asp:TextBox ID="partnerName" runat="server" CssClass="form-control" placeholder="Enter partner's name"></asp:TextBox>
        </div>
        <div class="form-group">
            <label class="form-label" for="serviceDescription">Description:</label>
            <asp:TextBox ID="serviceDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Enter service description"></asp:TextBox>
        </div>
       <div class="form-group">
    <label class="form-label" for="servicePhotos">Service Photos or Examples:</label>
    <div class="input-group">
        <input type="text" id="photoSearchInput" class="form-control" placeholder="Search photos...">
        <div class="input-group-append">
            <button type="button" class="btn btn-outline-secondary" onclick="searchPhotos()">Search</button>
        </div>
    </div>
    <asp:FileUpload ID="servicePhotos" runat="server" CssClass="form-control" accept="image/*" />
    <small class="form-text text-muted">Upload images showcasing the service (optional).</small>
</div>

        <asp:Button ID="btnAddService" runat="server" Text="Add Service" CssClass="btn-add-service" OnClick="btnAddService_Click" />
    </div>
    <script>
    function searchPhotos() {
        var input, filter, photos, i, txtValue;
        input = document.getElementById('photoSearchInput');
        filter = input.value.toUpperCase();
        photos = document.getElementById('servicePhotos');

        // Get all files (images) uploaded in the file input
        var fileList = photos.files;

        for (i = 0; i < fileList.length; i++) {
            // Check each file name against the search filter
            txtValue = fileList[i].name.toUpperCase();
            if (txtValue.indexOf(filter) > -1) {
                // Show the file if it matches the search criteria
                // You can customize the display here, e.g., highlight matching files
                console.log("Match found: " + fileList[i].name);
            }
        }
    }
    </script>

</asp:Content>
