<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="VendorRegister.aspx.cs" Inherits="Urban_Ease_2.VendorRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Hero section start -->
    <div class="container col-xxl-8 px-4 py-5">
        <div class="row flex-lg-row-reverse align-items-center g-5 py-5">
            <div class="col-10 col-sm-8 col-lg-6">
                <img src="https://dnx.solutions/wp-content/uploads/2022/06/PROFESSIONAL-SERVICES.png" class="d-block mx-lg-auto img-fluid" alt="Bootstrap Themes" width="700" height="500" loading="lazy">
            </div>
            <div class="col-lg-6">
                <h1 class="display-5 fw-bold text-body-emphasis lh-1 mb-3">Earn More. Earn Respect. Safety Ensured.</h1>
                <p class="lead">Our tagline embodies the essence of what we stand for and what we offer to our partners. It reflects our commitment to empowering individuals to increase their earnings while earning the respect and recognition they deserve.</p>
            </div>
        </div>
    </div>
    <!-- Hero section end -->

    <!-- Start Contact Form -->
    <div class="container">
        <div class="block">
            <div class="row justify-content-center">
                <div class="col-md-8 col-lg-8 pb-4">
                    <div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="text-black" for="fname">First name</label>
                                    <asp:TextBox class="form-control" ID="fname" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="text-black" for="lname">Last name</label>
                                    <asp:TextBox type="text" class="form-control" ID="lname" runat="server"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="text-black" for="email">Email address</label>
                                    <asp:TextBox type="email" class="form-control" ID="email" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="text-black" for="profession">Select Profession</label>
                                    <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                                        <asp:ListItem Disabled="True" Selected="True" Text="Select Profession"></asp:ListItem>
                                        <asp:ListItem>Barber</asp:ListItem>
                                        <asp:ListItem>Painter</asp:ListItem>
                                        <asp:ListItem>Construction</asp:ListItem>
                                        <asp:ListItem>Plumber</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                        <div class="form-group mb-3">
                            <label class="text-black" for="message">Message</label>

                            <textarea name="" class="form-control" id="message" runat="server" cols="30" rows="5"></textarea>
                        </div>


                        <asp:Button ID="Button1" class="btn btn-primary btn-lg" OnClick="Button1_Click" runat="server" Text="Send Message" />
                    </div>
                </div>
            </div>
            <div class="row mb-5">
                <div class="col-lg-4">
                    <div class="service no-shadow align-items-center link horizontal d-flex active" data-aos="fade-left" data-aos-delay="0">
                        <div class="service-icon color-1 mb-3">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-geo-alt-fill" viewBox="0 0 16 16">
                                <path d="M8 16s6-5.686 6-10A6 6 0 0 0 2 6c0 4.314 6 10 6 10zm0-7a3 3 0 1 1 0-6 3 3 0 0 1 0 6z" />
                            </svg>
                        </div>
                        <div class="service-contents">
                            <p>43 Raymouth Rd. Baltemoer, London 3910</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="service no-shadow align-items-center link horizontal d-flex active" data-aos="fade-left" data-aos-delay="0">
                        <div class="service-icon color-1 mb-3">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-envelope-fill" viewBox="0 0 16 16">
                                <path d="M.05 3.555A2 2 0 0 1 2 2h12a2 2 0 0 1 1.95 1.555L8 8.414.05 3.555zM0 4.697v7.104l5.803-3.558L0 4.697zM6.761 8.83l-6.57 4.027A2 2 0 0 0 2 14h12a2 2 0 0 0 1.808-1.144l-6.57-4.027L8 9.586l-1.239-.757zm3.436-.586L16 11.801V4.697l-5.803 3.546z" />
                            </svg>
                        </div>
                        <div class="service-contents">
                            <p>info@yourdomain.com</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="service no-shadow align-items-center link horizontal d-flex active" data-aos="fade-left" data-aos-delay="0">
                        <div class="service-icon color-1 mb-3">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-telephone-fill" viewBox="0 0 16 16">
                                <path fill-rule="evenodd" d="M1.885.511a1.745 1.745 0 0 1 2.61.163L6.29 2.98c.329.423.445.974.315 1.494l-.547 2.19a.678.678 0 0 0 .178.643l2.457 2.457a.678.678 0 0 0 .644.178l2.189-.547a1.745 1.745 0 0 1 1.494.315l2.306 1.794c.829.645.905 1.87.163 2.611l-1.034 1.034c-.74.74-1.846 1.065-2.877.702a18.634 18.634 0 0 1-7.01-4.42 18.634 18.634 0 0 1-4.42-7.009c-.362-1.03-.037-2.137.703-2.877L1.885.511z" />
                            </svg>
                        </div>
                        <div class="service-contents">
                            <p>+1 294 3925 3939</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- End Contact Form -->
</asp:Content>
