<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Urban_Ease_2.Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .carousel-inner img {
            border-radius: 10px;
            transition: transform 0.3s ease;
        }

            .carousel-inner img:hover {
                transform: scale(1.05);
            }

        .product-item {
            text-align: center;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            border-radius: 10px;
            padding: 15px;
            background: #fff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

            .product-item:hover {
                transform: translateY(-10px);
                box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
            }

        .product-title {
            font-size: 1rem;
            font-weight: 500;
            color: #495057;
        }

        .custom-swal-popup {
            background: #fff;
            border-radius: 10px;
        }

        .option-link {
            text-decoration: none;
            color: #333;
        }

            .option-link img {
                border-radius: 50%;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            }

            .option-link p {
                font-size: 0.9rem;
                margin: 10px 0 0;
            }
    </style>
    <!-- Start Hero Section -->
    <div class="hero">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-5">
                    <div class="intro-excerpt">
                        <h1>Modern City Requires <span clsas="d-block">Modern Solution</span></h1>
                        <p class="mb-4">Redefining City Living with Comfort and Convenience. Where Urban Life Finds Tranquility and Ease.</p>
                        <p>
                            <asp:Button ID="Button" class="btn btn-secondary me-2" runat="server" Text="Book Now" />
                            <a href="services.aspx"></a>
                        </p>
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="hero-img-wrap">
                        <img src="images/couch.png" class="img-fluid">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Hero Section -->

    <!-- Start We Help Section -->
    <div class="we-help-section">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-7 mb-5 mb-lg-0">
                    <div class="imgs-grid">
                        <div class="grid grid-1">
                            <img src="images/h1.png" alt="Untree.co">
                        </div>
                        <div class="grid grid-2">
                            <img src="images/h2.png" alt="Untree.co">
                        </div>
                        <div class="grid grid-3">
                            <img src="images/h3.png" alt="Untree.co">
                        </div>
                    </div>
                </div>
                <div class="col-lg-5 ps-lg-5">
                    <h2 class="section-title mb-4">We Help You Make Your House Look Awesome.</h2>
                    <p>
                        At Urban Ease, we're your partners in making your house look truly awesome. With our expert services and attention to detail, we transform your vision into reality, ensuring every corner of your home reflects your unique style and personality.
                    </p>
                    <ul class="list-unstyled custom-list my-4">
                        <li>Comprehensive Services</li>
                        <li>Skilled Professionals</li>
                        <li>Personalized Solutions</li>
                        <li>Attention to Detail</li>
                    </ul>
                    <p>
                        <asp:Button ID="Button1" class="btn" runat="server" Text="Explore Now" /></p>
                </div>
            </div>
        </div>
    </div>
    <!-- End We Help Section -->

    <!-- Start New and noteworthy -->
    <h2 style="margin-left: 150px; font-weight: bold; color: #333; margin-bottom: 40px;">New and noteworthy</h2>
    <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="decor.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/n1.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Wall Decor (Panel)</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="locks.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/n2.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Native Smart Lock</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="water_purifier.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/n3.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Native Water Purifier</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="cleaning.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/n4.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Kitchen Cleaning</h3>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Add more carousel items as needed -->
            <div class="carousel-item">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="painting.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/n5.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">House Painter</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="Woman.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/n6.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Lesar Hair Removel</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="Woman.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/n7.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Spa Ayurveda</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="Woman.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/n8.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Hair Studio For Women</h3>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End New and noteworthy -->

    <!-- Start Most booked services -->
    <h2 style="margin-left: 150px; margin-bottom: 40px; font-weight: bold; color: #333; margin-top: 70px;">Most booked services</h2>
    <div id="carouselExampleIndicator" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="cleaning.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/m1.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Intense Bathroom Cleaning</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="electronics.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/m2.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">AC Install</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="man.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/m3.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Hair Cut for Men</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="cleaning.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/m4.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Sofa Cleaning</h3>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Add more carousel items as needed -->
            <div class="carousel-item">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="electronics.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/m5.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Top Load (Fully automatic)</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="Plumber.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/m6.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Tap Repair</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="cleaning.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/m7.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Anti-rust deep cleaning AC service</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="electronics.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/m8.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Chimney Cleaning</h3>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Most booked services -->

    <!-- Start Mid Corsole -->
    <div class="container">
        <div id="carouselExample" class="carousel slide" style="margin-top: 80px;">
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <a href="man.aspx" style="display: inline-block; overflow: hidden;">
                        <img src="images/mid%20carsole-%201.jpg"
                            class="d-block w-100"
                            alt="Image"
                            style="transition: transform 0.3s ease; width: 100%;"
                            onmouseover="this.style.transform = 'scale(1.05)';"
                            onmouseout="this.style.transform = 'scale(1)';">
                    </a>
                </div>
            </div>
        </div>
    </div>
    <!-- End Mid Corsole -->

    <!-- Start Cleaning & pestcontrol -->
    <h2 style="margin-left: 150px; margin-bottom: 40px; font-weight: bold; color: #333; margin-top: 70px;">Cleaning & pest control</h2>
    <div id="carouselExampleIndicatora" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="cleaning.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/cpc1.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Bathroom & Kitchen Cleaning</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="cleaning.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/cpc2.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Full Home Cleaning</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="cleaning.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/cpc3.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">General Pest Control</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="cleaning.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/cpc4.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Sofa & Carpet Cleaning</h3>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Cleaning & pestcontrol -->

    <!-- Start ro Corsole -->
    <div class="container">
        <div id="carouselExampl" class="carousel slide" style="margin-top: 80px;">
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <a href="water_purifier.aspx" style="display: inline-block; overflow: hidden;">
                        <img src="images/ro-coroaole.png"
                            class="d-block w-100"
                            alt="Image"
                            style="transition: transform 0.3s ease; width: 100%;"
                            onmouseover="this.style.transform = 'scale(1.05)';"
                            onmouseout="this.style.transform = 'scale(1)';">
                    </a>
                </div>
            </div>
        </div>
    </div>
    <!-- End ro Corsole -->

    <!-- Start AC repair -->
    <h2 style="margin-left: 150px; font-weight: bold; color: #333; margin-bottom: 40px; margin-top: 70px;">AC & Applince repair</h2>
    <div id="carouselExampleIndicato" class="carousel slide" data-ride="carousel" style="margin-bottom: 100px">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="decor.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/acc1.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Wall Decor (Panel)</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="locks.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/acc2.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Native Smart Lock</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="water_purifier.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/acc3.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Native Water Purifier</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="cleaning.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/acc4.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Kitchen Cleaning</h3>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Add more carousel items as needed -->
            <div class="carousel-item">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="painting.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/acc5.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">House Painter</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="Woman.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/acc6.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Lesar Hair Removel</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="Woman.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/acc7.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Spa Ayurveda</h3>
                            </a>
                        </div>
                        <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                            <a class="product-item" href="Woman.aspx" style="text-decoration: none; color: #333; display: block; transition: background-color 0.3s ease;">
                                <img src="images/acc8.png" class="img-fluid product-thumbnail" style="height: 200px; object-fit: cover; margin-bottom: 10px; width: 100%; border-radius: 10%;">
                                <h3 class="product-title" style="font-size: 16px; margin-left: 25px;">Hair Studio For Women</h3>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End AC repair -->
    <script>
        $(document).ready(function () {
            $('#carouselExampleIndicators').carousel();
        });
    </script>
    <script type='text/javascript'>
        document.addEventListener('DOMContentLoaded', function () {
            // Function to show the SweetAlert popup
            function showOptionsPopup() {
                const options = [
                    { Name: "Woman's Care", Url: "Woman.aspx", ImageUrl: "images/sb1.png" },
                    { Name: "Man's Care", Url: "man.aspx", ImageUrl: "images/sb2.png" },
                    { Name: "AC Repairs & Maintenance", Url: "repair.aspx", ImageUrl: "images/sb3.png" },
                    { Name: "Cleaning & Pest Control", Url: "cleaning.aspx", ImageUrl: "images/sb4.png" },
                    { Name: "Electrician, Plumber & Carpenter", Url: "Plumber.aspx", ImageUrl: "images/sb5.png" },
                    { Name: "Native Water Purifier", Url: "water_purifier.aspx", ImageUrl: "images/sb6.png" },
                    { Name: "Smart Locks", Url: "locks.aspx", ImageUrl: "images/sb7.png" },
                    { Name: "Painting & Waterproofing", Url: "painting.aspx", ImageUrl: "images/sb8.png" },
                    { Name: "Wall Decor", Url: "decor.aspx", ImageUrl: "images/sb9.png" }
                    // Add more options as needed
                ];

                // Build HTML markup for options
                const optionsHtml = options.map(option => `
                <div class='col-4 text-center mb-3'>
                    <a href='${option.Url}' class='option-link' data-name='${option.Name}' data-url='${option.Url}'>
                        <img src='${option.ImageUrl}' class='img-fluid rounded' alt='${option.Name}' style='max-width: 70px; max-height: 70px;'>
                        <p class='mt-2' style='font-weight: 500; color: #333; font-size: 12px; margin: 0;'>${option.Name}</p>
                    </a>
                </div>
            `).join('');

                // Show SweetAlert popup
                const swalInstance = Swal.fire({
                    title: 'Select What You Need',
                    html: `<div class='row justify-content-center'>${optionsHtml}</div>`,
                    showConfirmButton: false,
                    showCancelButton: false,
                    customClass: {
                        popup: 'custom-swal-popup'
                    }
                });

                // Handle option click event
                document.querySelectorAll('.option-link').forEach(link => {
                    link.addEventListener('click', function (e) {
                        e.preventDefault(); // Prevent the default link behavior
                        const url = this.getAttribute('data-url');

                        // Close SweetAlert popup
                        swalInstance.close();

                        // Redirect to the selected option URL
                        window.location.href = url;
                    });
                });
            }

            // Attach click event listener to Button
            const button = document.getElementById('<%= Button.ClientID %>');
            if (button) {
                button.addEventListener('click', function (event) {
                    event.preventDefault(); // Prevent the default button click behavior
                    showOptionsPopup(); // Call the function to show the popup
                });
            }

            // Attach click event listener to Button1
            const button1 = document.getElementById('<%= Button1.ClientID %>');
            if (button1) {
                button1.addEventListener('click', function (event) {
                    event.preventDefault(); // Prevent the default button click behavior
                    showOptionsPopup(); // Call the function to show the popup
                });
            }
        });
    </script>

</asp:Content>
