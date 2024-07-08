<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="locks.aspx.cs" Inherits="Urban_Ease_2.locks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <!-- Left Sidebar with Grid-like Cards -->
            <div class="col-md-3 mb-4" id="sidebar" style="margin-top: 40px; margin-left: -70px; margin-right: 40px">
                <div class="card border-0 shadow" style="border-radius: 15px; overflow: hidden; position: sticky; top: 80px;">
                    <div class="card-body" style="background-color: #f8f9fa; border-radius: 8px; padding: 20px;">
                        <!-- Heading above the grid -->

                        <h2 class="card-title" style="font-weight: bold; color: #333; font-size: 28px; margin-bottom: 10px;">Native Smart Locks</h2>
                        <hr style="margin-bottom: 20px; border-top: 2px solid #333;" />

                        <div class="col-12">
                            <ul style="list-style-type: none; padding: 0; margin: 0;">
                                <li style="font-weight: bold; color: #333; font-size: 16px; margin-bottom: 8px;">
                                    <span style="color: green; margin-right: 10px;">✓</span> Verified Professionals
                                </li>
                                <li style="font-weight: bold; color: #333; font-size: 16px; margin-bottom: 8px;">
                                    <span style="color: green; margin-right: 10px;">✓</span> Hassle-Free Booking
                                </li>
                                <li style="font-weight: bold; color: #333; font-size: 16px; margin-bottom: 8px;">
                                    <span style="color: green; margin-right: 10px;">✓</span> Transparent Pricing
                                </li>
                            </ul>
                        </div>
                    </div>

                    <!-- Additional CSS for better styling -->
                    <style>
                        .card-body h2 {
                            font-size: 28px;
                            margin-bottom: 20px;
                        }

                        .card-body ul li {
                            transition: color 0.2s;
                        }

                            .card-body ul li:hover {
                                color: #000;
                            }
                    </style>

                </div>

            </div>


            <!-- Main Content Area with Grid System -->
            <div class="col-md-9 order-md-2">
                <!-- Carousel for pictures -->
                <div id="pictureCarousel" class="carousel slide mb-4 mt-4" data-bs-ride="carousel" data-interval="3000" style="border-radius: 15px; overflow: hidden;">
                    <div class="carousel-inner">
                        <!-- Picture Items (sample) -->
                        <div class="carousel-item active">
                            <img src="https://res.cloudinary.com/urbanclap/image/upload/t_high_res_category/w_873,dpr_2,fl_progressive:steep,q_auto:low,f_auto,c_limit/images/growth/luminosity/1695987154340-42756a.jpeg" class="d-block w-100" style="object-fit: cover; height: 500px;" alt="Image 1">
                        </div>


                        <!-- Add more picture items here -->
                    </div>

                </div>

                <!-- Grid system for sidebars -->
                <div class="row">
                    <!-- Empty Area -->
                    <div class="col-md-8">
                        <!-- This area is intentionally left empty for your additional content -->
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </div>
                    <!-- Right Sidebar -->
                    <div class="col-md-4 mb-4" style="margin-top: 40px;">
                        <div class="card border-0 shadow" style="border-radius: 15px; overflow: hidden; position: sticky; top: 80px;">
                            <div class="card-body text-center">
                                <h5 class="card-title" style="font-weight: bold; color: #333;">Cart</h5>
                                <hr style="margin-bottom: 10px" />
                                <div id="cart-sidebar">
                                    <div id="empty-cart-message" style="display: none;">
                                        <img class="img-fluid mx-auto d-block" src="images/shopping-cart-pn.png" style="width: 90px; height: 90px;" alt="Cart Image" />
                                        <p class="card-title">No items in your cart</p>
                                    </div>
                                    <!-- Cart items will be dynamically populated here -->
                                    <table class="table site-block-order-table mb-5">
                                        <tbody id="orderSummaryBody">
                                            <!-- Order summary items will be dynamically added here -->
                                        </tbody>
                                    </table>
                                    <!-- Button to show total amount and redirect to checkout -->
                                    <button id="checkoutButton" class="btn btn-primary" style="margin-top: 20px;" onclick="redirectToCheckout(event)">
                                        ₹<span style="margin-right: 90px !important;" id="totalAmount">0.00</span>Total
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- New Right Sidebar (Duplicate) -->
                        <div class="card border-0 shadow mt-4" style="border-radius: 15px; overflow: hidden; position: sticky; top: 380px;">
                            <div class="card-body" style="position: relative;">
                                <!-- Image in the top-right corner -->
                                <img src="images/promiss.jpg" class="img-fluid" style="position: absolute; top: 10px; right: 10px; width: 50px; height: 50px;">

                                <h2 class="card-title" style="font-weight: bold; color: #333; font-size: 28px; margin-bottom: 10px; text-align: left;">UE Promise</h2>
                                <hr style="margin-bottom: 20px; border-top: 2px solid #333;" />
                                <div class="col-12">
                                    <ul style="list-style-type: none; padding: 0; margin: 0; text-align: left;">
                                        <li style="font-weight: bold; color: #333; font-size: 16px; margin-bottom: 8px;">
                                            <span style="color: green; margin-right: 10px;">✓</span>Booking Convenience
                                        </li>
                                        <li style="font-weight: bold; color: #333; font-size: 16px; margin-bottom: 8px;">
                                            <span style="color: green; margin-right: 10px;">✓</span>Clear Pricing
                                        </li>
                                        <li style="font-weight: bold; color: #333; font-size: 16px; margin-bottom: 8px;">
                                            <span style="color: green; margin-right: 10px;">✓</span>Verified Experts
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

    <!-- JavaScript to handle adding items to cart -->
    <script>
        function redirectToCheckout(event) {
            event.preventDefault(); // Prevent default form submission or link behavior
            console.log('Redirecting to checkout...');
            window.location.href = 'checkout.aspx';
        }

        // Function to add item to cart
        function addToCart(itemId, itemName, itemPrice) {
            console.log('Adding item to cart:', itemId, itemName, itemPrice);

            let cartItems = JSON.parse(localStorage.getItem('cart')) || [];
            let cartPrices = JSON.parse(localStorage.getItem('cartPrices')) || {};

            if (!cartItems.includes(itemId.toString())) {
                cartItems.push(itemId.toString());
            }

            cartPrices[itemId.toString()] = { name: itemName, price: itemPrice };

            localStorage.setItem('cart', JSON.stringify(cartItems));
            localStorage.setItem('cartPrices', JSON.stringify(cartPrices));

            console.log('Item added to cart:', itemId);

            // After adding item to cart, update the cart sidebar
            updateCartSidebar();
        }

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

            // After removing item from cart, update the cart sidebar
            updateCartSidebar();
        }

        // Function to update the right sidebar with cart details
        function updateCartSidebar() {
            try {
                let cartPrices = JSON.parse(localStorage.getItem('cartPrices')) || {};
                let orderSummaryTable = document.getElementById('orderSummaryBody');
                let emptyCartMessage = document.getElementById('empty-cart-message');
                let checkoutButton = document.getElementById('checkoutButton');
                let totalAmountElement = document.getElementById('totalAmount');

                if (Object.keys(cartPrices).length === 0) {
                    // If cart is empty, display a message and hide the checkout button
                    emptyCartMessage.style.display = 'block';
                    orderSummaryTable.innerHTML = '';
                    checkoutButton.style.display = 'none';
                } else {
                    // If cart has items, populate the order summary table and show the checkout button
                    emptyCartMessage.style.display = 'none';
                    orderSummaryTable.innerHTML = '';

                    let subtotalAmount = 0;
                    for (let itemId in cartPrices) {
                        if (Object.prototype.hasOwnProperty.call(cartPrices, itemId)) {
                            let item = cartPrices[itemId];
                            let row = orderSummaryTable.insertRow();
                            let nameCell = row.insertCell(0);
                            let priceCell = row.insertCell(1);
                            let removeCell = row.insertCell(2);

                            nameCell.textContent = item.name;
                            priceCell.textContent = '₹' + item.price.toFixed(2);

                            let removeIcon = document.createElement('i');
                            removeIcon.className = 'fas fa-times text-danger';
                            removeIcon.style.cursor = 'pointer';
                            removeIcon.onclick = function () { removeFromCart(itemId); };
                            removeCell.appendChild(removeIcon);

                            subtotalAmount += item.price;
                        }
                    }

                    // Update subtotal amount and show the checkout button
                    totalAmountElement.textContent = subtotalAmount.toFixed(2);
                    checkoutButton.style.display = 'block';
                }

                // Duplicate the sidebar functionality for the second sidebar
                let orderSummaryTable2 = document.getElementById('orderSummaryBody2');
                let emptyCartMessage2 = document.getElementById('empty-cart-message-2');
                let checkoutButton2 = document.getElementById('checkoutButton2');
                let totalAmountElement2 = document.getElementById('totalAmount2');

                if (Object.keys(cartPrices).length === 0) {
                    emptyCartMessage2.style.display = 'block';
                    orderSummaryTable2.innerHTML = '';
                    checkoutButton2.style.display = 'none';
                } else {
                    emptyCartMessage2.style.display = 'none';
                    orderSummaryTable2.innerHTML = '';

                    let subtotalAmount2 = 0;
                    for (let itemId in cartPrices) {
                        if (Object.prototype.hasOwnProperty.call(cartPrices, itemId)) {
                            let item = cartPrices[itemId];
                            let row2 = orderSummaryTable2.insertRow();
                            let nameCell2 = row2.insertCell(0);
                            let priceCell2 = row2.insertCell(1);
                            let removeCell2 = row2.insertCell(2);

                            nameCell2.textContent = item.name;
                            priceCell2.textContent = '₹' + item.price.toFixed(2);

                            let removeIcon2 = document.createElement('i');
                            removeIcon2.className = 'fas fa-times text-danger';
                            removeIcon2.style.cursor = 'pointer';
                            removeIcon2.onclick = function () { removeFromCart(itemId); };
                            removeCell2.appendChild(removeIcon2);

                            subtotalAmount2 += item.price;
                        }
                    }

                    totalAmountElement2.textContent = subtotalAmount2.toFixed(2);
                    checkoutButton2.style.display = 'block';
                }
            } catch (error) {
                console.error('Error updating cart sidebar:', error);
            }
        }

        // Function to bind click event to dynamically added 'Add' buttons
        function bindAddToCartButtons() {
            const addToCartButtons = document.querySelectorAll('.btn-add-to-cart');
            addToCartButtons.forEach(button => {
                button.addEventListener('click', () => {
                    const itemId = parseInt(button.getAttribute('data-itemId'));
                    const itemName = button.getAttribute('data-itemName');
                    const itemPrice = parseFloat(button.getAttribute('data-itemPrice'));

                    console.log('Clicked Item:', itemId, itemName, itemPrice);

                    if (!isNaN(itemId) && itemName && !isNaN(itemPrice)) {
                        addToCart(itemId, itemName, itemPrice);
                    } else {
                        console.log('Invalid item details:', itemId, itemName, itemPrice);
                    }
                });
            });
        }

        // Call bindAddToCartButtons when the page finishes loading
        window.addEventListener('load', bindAddToCartButtons);

        // Call updateCartSidebar function once on page load to initially populate the cart sidebar
        window.addEventListener('load', updateCartSidebar);
    </script>

</asp:Content>
