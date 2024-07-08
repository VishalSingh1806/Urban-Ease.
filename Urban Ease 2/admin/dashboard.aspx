<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="Urban_Ease_2.admin.dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- This section is typically used to include any specific head content -->
    <style>
    .scrollable-table {
        max-height: 250px;
        overflow-y: auto;
    }

    .table th, .table td {
        text-align: center;
    }

    .booking-id-column {
        width: 100px; /* Adjust as needed */
    }

    .service-column {
        width: 350px; /* Adjust as needed */
    }

    .partner-id-column {
        width: 150px; /* Adjust as needed */
    }

    /* Enhanced card styles */
    .card {
        border-radius: 10px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        transition: all 0.3s ease;
        margin-bottom: 20px;
        max-width: calc(100% - 160px); /* Adjust the calculation as needed */
        margin-left: 90px;
        margin-right: auto;
    }

    .card:hover {
        box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
    }

    .card-header {
        background-color: #f8f9fa;
        border-bottom: 1px solid #e8e9eb;
        padding: 15px;
        border-top-left-radius: 10px;
        border-top-right-radius: 10px;
    }

    .card-title {
        margin: 0;
        font-size: 18px;
        font-weight: 600;
    }

    .card-body {
        padding: 15px;
    }

    .table-responsive {
        margin-top: 10px;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Main content section -->
    <div class="wrapper">
        <!-- Sidebar -->
        <!-- Main content area -->
        <div class="main-panel" id="main-panel">
            <!-- Panel header with a large chart -->
            <div class="panel-header panel-header-lg">
                <canvas id="bigDashboardChart"></canvas>
            </div>
            <!-- Content area -->
            <div class="content">
               <!-- Partner Management Section -->
<!-- Partner Management Section -->
<div class="row" id="partnerManagementSection">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-header">
                <h5 class="card-title">Partner Management</h5>
            </div>
            <div class="card-body scrollable-table">
                <div class="table-responsive">
                    <asp:GridView ID="partnerTable" runat="server" CssClass="table" AutoGenerateColumns="False"
                        DataKeyNames="id" OnRowEditing="partnerTable_RowEditing" OnRowUpdating="partnerTable_RowUpdating" OnRowCancelingEdit="partnerTable_RowCancelingEdit" OnRowCommand="partnerTable_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="Partner ID" ReadOnly="True" />
                            <asp:BoundField DataField="firstname" HeaderText="First Name" />
                            <asp:BoundField DataField="lastname" HeaderText="Last Name" />
                            <asp:BoundField DataField="Subcategory" HeaderText="Subcategory" />
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</div>


<!-- Service Management Section -->
<div class="row" id="serviceManagementSection">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-header">
                <h5 class="card-title">Service Management</h5>
            </div>
            <div class="card-body scrollable-table">
                <div class="table-responsive">
                    <asp:GridView ID="serviceTable" runat="server" CssClass="table" AutoGenerateColumns="False"
                        DataKeyNames="ServiceID" OnRowEditing="serviceTable_RowEditing" OnRowUpdating="serviceTable_RowUpdating" OnRowCancelingEdit="serviceTable_RowCancelingEdit" OnRowCommand="serviceTable_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="ServiceID" HeaderText="Service ID" ReadOnly="True" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Price" HeaderText="Price" />
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</div>

                <!-- Booking Management Section -->
<div class="row" id="bookingManagementSection">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-header">
                <h5 class="card-title">Booking Management</h5>
            </div>
            <div class="card-body scrollable-table">
                <div class="table-responsive">
                    <asp:GridView ID="bookingTable" runat="server" CssClass="table" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="BookingID" HeaderText="Booking ID" ItemStyle-CssClass="booking-id-column" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Phone No" HeaderText="Phone No" />
                            <asp:BoundField DataField="Service" HeaderText="Service" ItemStyle-CssClass="service-column" />
                            <asp:BoundField DataField="PName" HeaderText="Partner's Name" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <%# Eval("Status") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</div>

            </div>
        </div>
    </div>
</asp:Content>
