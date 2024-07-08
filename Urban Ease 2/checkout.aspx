<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="Urban_Ease_2.checkout" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #orderTotal {
            min-width: 100px;
            text-align: right;
        }
    </style>

    <div class="untree_co-section">
        <div class="container">
            <div class="row">
                <div class="col-md-6 mb-5 mb-md-0">
                    <h2 class="h3 mb-3 text-black">Billing Details</h2>
                    <div class="p-3 p-lg-5 border bg-white">
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="fname" class="text-black">First Name <span class="text-danger">*</span></label>
                                <input type="text" required="" class="form-control" id="fname" runat="server" name="c_fname">
                            </div>
                            <div class="col-md-6">
                                <label for="lname" class="text-black">Last Name <span class="text-danger">*</span></label>
                                <input type="text" required="" class="form-control" id="lname" runat="server" name="c_lname">
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <label for="c_address" class="text-black">Address <span class="text-danger">*</span></label>
                                <input type="text" required="" class="form-control" id="address" runat="server" name="c_address" placeholder="Street address">
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="state" class="text-black">State / Country <span class="text-danger">*</span></label>
                                <input type="text" required="" class="form-control" id="state" runat="server" name="c_state_country">
                            </div>
                            <div class="col-md-6">
                                <label for="zip" class="text-black">Postal / Zip <span class="text-danger">*</span></label>
                                <input type="text" required="" class="form-control" id="zip" runat="server" name="c_postal_zip">
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="email" class="text-black">Email Address <span class="text-danger">*</span></label>
                                <input type="text" required="" class="form-control" id="email" runat="server" name="c_email_address">
                            </div>
                            <div class="col-md-6">
                                <label for="phone" class="text-black">Phone <span class="text-danger">*</span></label>
                                <input type="text" required="" class="form-control" id="phone" runat="server" name="c_phone" placeholder="Phone Number">
                            </div>
                        </div>
                        <div class="form-group row mb-5">
                            <div class="col-md-6">
                                <label for="date" class="text-black">Date <span class="text-danger">*</span></label>
                                <input type="date" required="" class="form-control" id="date" runat="server" name="c_date">
                            </div>
                            <div class="col-md-6">
                                <label for="time" class="text-black">Time <span class="text-danger">*</span></label>
                                <input type="time" required="" class="form-control" id="time" runat="server" name="c_time">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="c_order_notes" class="text-black">Order Notes</label>
                            <textarea name="c_order_notes" id="c_order_notes" runat="server" cols="30" rows="5" class="form-control" placeholder="Write your notes here..."></textarea>
                        </div>
                        <input type="hidden" id="product_ids" runat="server" name="product_ids" />
                        <input type="hidden" id="user_id" runat="server" name="user_id" />
                    </div>

                </div>

                <div class="col-md-6">
                    <h2 class="h3 mb-3 text-black">Your Order</h2>
                    <div class="p-3 p-lg-5 border bg-white">
                        <table class="table site-block-order-table mb-5">
                            <thead>
                                <tr>
                                    <th>Product</th>
                                    <th>Total</th>
                                    <th></th> <!-- Column for delete button -->
                                </tr>
                            </thead>
                            <tbody id="orderSummaryBody">
                                <!-- Order summary items will be dynamically added here -->
                            </tbody>
                        </table>
                        <div class="border-top py-3">
                            <div class="d-flex justify-content-between">
                                <div class="text-black font-weight-bold"><strong>Order Total</strong></div>
                                <div id="orderTotal" class="text-black font-weight-bold">₹<span id="totalAmount"></span></div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="Save_address" class="btn btn-black btn-lg py-3 btn-block" runat="server" OnClick="Save_address_Click" Text="Place Order" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        // Function to remove item from cart
        function removeFromCart(itemId) {
            console.log('Removing item from cart:', itemId);

            let cartItems = JSON.parse(localStorage.getItem('cart')) || [];
            let cartPrices = JSON.parse(localStorage.getItem('cartPrices')) || {};

            cartItems = cartItems.filter(item => item !== itemId.toString());
            delete cartPrices[itemId.toString()];

            localStorage.setItem('cart', JSON.stringify(cartItems));
            localStorage.setItem('cartPrices', JSON.stringify(cartPrices));

            console.log('Item removed from cart:', itemId);

            // After removing item from cart, update the order summary
            displayOrderSummary();
        }

        // Function to display order summary on the checkout page
        function displayOrderSummary() {
            try {
                // Retrieve order summary data from local storage
                let cartPrices = JSON.parse(localStorage.getItem('cartPrices')) || {};
                let subtotalAmount = 0;

                let orderSummaryTable = document.getElementById('orderSummaryBody');
                let totalAmountElement = document.getElementById('totalAmount'); // Get total amount element
                let productIdsField = document.getElementById('<%= product_ids.ClientID %>'); // Get the hidden input field for product IDs

                let productIds = [];

                orderSummaryTable.innerHTML = ''; // Clear existing order summary

                // Iterate over order summary data and populate the table
                for (let itemId in cartPrices) {
                    if (Object.prototype.hasOwnProperty.call(cartPrices, itemId)) {
                        let item = cartPrices[itemId];
                        console.log('Processing item:', item); // Debugging: Log each item to console
                        let row = orderSummaryTable.insertRow();
                        let nameCell = row.insertCell(0);
                        let priceCell = row.insertCell(1);
                        let removeCell = row.insertCell(2);

                        nameCell.textContent = item.name; // Corrected property access
                        priceCell.textContent = '₹' + item.price.toFixed(2); // Corrected property access

                        let removeIcon = document.createElement('i');
                        removeIcon.className = 'fas fa-times text-danger';
                        removeIcon.style.cursor = 'pointer';
                        removeIcon.onclick = function () { removeFromCart(itemId); };
                        removeCell.appendChild(removeIcon);

                        subtotalAmount += item.price; // Corrected property access
                        productIds.push(itemId); // Collect product IDs
                    }
                }

                // Update subtotal amount
                totalAmountElement.textContent = subtotalAmount.toFixed(2);

                // Set the value of the hidden input field for product IDs
                productIdsField.value = productIds.join(',');

            } catch (error) {
                console.error('Error displaying order summary:', error);
            }
        }

        // Call displayOrderSummary when the page is fully loaded
        window.addEventListener('load', displayOrderSummary);
    </script>
</asp:Content>
