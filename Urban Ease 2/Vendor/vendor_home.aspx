<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="vendor_home.aspx.cs" Inherits="Urban_Ease_2.Vendor.vendor_home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        .otp-input {
            display: block;
            margin: 10px 0;
            padding: 10px;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 4px;
            width: 100%;
            max-width: 300px;
        }
        .otp-section {
            margin-top: 10px;
        }
        .order-card {
            border: 1px solid #ddd;
            padding: 15px;
            margin: 10px 0;
            border-radius: 5px;
        }
        .order-actions button {
            margin-right: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <section id="new-orders" class="content-section">
            <h2>New Orders</h2>
            <asp:Literal ID="LiteralNewOrders" runat="server"></asp:Literal>
        </section>
        <section id="order-history" class="content-section">
            <h2>Order History</h2>
            <asp:Literal ID="LiteralOrderHistory" runat="server"></asp:Literal>
        </section>
    </main>

   <script>
       $(document).ready(function () {
           $('.btn-accept').click(function (event) {
               event.preventDefault();

               var orderId = this.id.split('-').pop();

               $.ajax({
                   type: "POST",
                   url: "vendor_home.aspx/AcceptOrder",
                   data: JSON.stringify({ orderId: orderId }),
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {
                       $('#otp-section-' + orderId).show();
                       $('#accept-' + orderId).hide();
                       $('#otp-input-' + orderId).prop('disabled', false);
                   },
                   error: function (xhr, status, error) {
                       console.log('Error: ' + xhr.responseText);
                   }
               });
           });

           $('.btn-submit-otp').click(function (event) {
               event.preventDefault();
               var orderId = this.id.split('-').pop();
               var otp = $('#otp-input-' + orderId).val();
               var productId = $('#order-card-' + orderId).data('product-id');

               if (otp) {
                   $.ajax({
                       type: "POST",
                       url: "vendor_home.aspx/VerifyOTP",
                       data: JSON.stringify({ orderId: orderId, otp: otp, productId: productId }),
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (response) {
                           if (response.d === "OTP verified and order status updated for the specific product.") {
                               Swal.fire({
                                   icon: 'success',
                                   title: 'Success',
                                   text: 'OTP verified. Order moved to history.',
                                   confirmButtonText: 'OK'
                               }).then((result) => {
                                   if (result.isConfirmed) {
                                       // Move order to history section
                                       $('#order-card-' + orderId).appendTo('#order-history');
                                       $('#otp-section-' + orderId).remove();
                                   }
                               });
                           } else {
                               Swal.fire({
                                   icon: 'error',
                                   title: 'Error',
                                   text: response.d,
                                   confirmButtonText: 'OK'
                               });
                           }
                       },
                       error: function (xhr, status, error) {
                           console.log('Error: ' + xhr.responseText);
                       }
                   });
               } else {
                   Swal.fire({
                       icon: 'warning',
                       title: 'Warning',
                       text: 'Please enter the OTP.',
                       confirmButtonText: 'OK'
                   });
               }
           });
       });
   </script>


</asp:Content>
