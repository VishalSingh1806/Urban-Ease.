using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Urban_Ease_2
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateUserProfileDropdown(); // Call method to update user profile dropdown
            }
        }

        private void UpdateUserProfileDropdown()
        {
            if (Session["user"] != null)
            {
                // User is logged in
                ((HtmlAnchor)FindControl("loginOption")).Visible = false; // Hide login option
                ((HtmlAnchor)FindControl("logoutOption")).Visible = true; // Show logout option

                // Fetch and update user profile details
                string userId = Session["user"].ToString();
                FetchUserProfileDetails(userId);
            }
            else
            {
                // User is not logged in
                ((HtmlAnchor)FindControl("loginOption")).Visible = true; // Show login option
                ((HtmlAnchor)FindControl("logoutOption")).Visible = false; // Hide logout option
            }
        }


        protected void showProfileModal(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                string userId = Session["user"].ToString();
                FetchUserProfileDetails(userId);
                FetchUserOrderDetails(userId); // Fetching order details
                ScriptManager.RegisterStartupScript(this, GetType(), "profileModal", "$('#profileModal').modal('show');", true);
            }
        }

        private void FetchUserOrderDetails(string userId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString))
                {
                    con.Open();
                    string selectQry = @"
                SELECT 
                    STRING_AGG(s.s_name, ', ') AS ProductNames, 
                    SUM(s.price) AS TotalPrice, 
                    a.date AS OrderDate
                FROM 
                    address a
                CROSS APPLY STRING_SPLIT(a.product_id, ',') AS ps
                INNER JOIN 
                    services s ON ps.value = s.service_id
                WHERE 
                    a.u_id = @userId
                GROUP BY 
                    a.date";

                    SqlCommand selectCmd = new SqlCommand(selectQry, con);
                    selectCmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataAdapter adapter = new SqlDataAdapter(selectCmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    orderDetailsGridView.DataSource = dt;
                    orderDetailsGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Oops! An error occurred: " + ex.Message);
            }
        }

        private void FetchUserProfileDetails(string userId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString))
                {
                    con.Open();
                    string selectQry = "SELECT firstname, lastname, email, number FROM [signin] WHERE id=@userId";
                    SqlCommand selectCmd = new SqlCommand(selectQry, con);
                    selectCmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = selectCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string firstName = reader["firstname"].ToString();
                        string lastName = reader["lastname"].ToString();
                        profileFirstName.Text = $"{firstName} {lastName}";
                        profileEmail.Text = reader["email"].ToString();
                        profileNumber.Text = reader["number"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Oops! An error occurred: " + ex.Message);
            }
        }


        protected void Login_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString))
                {
                    con.Open();
                    string selectQry = "SELECT id, v_id, type FROM [signin] WHERE email=@username AND password=@password AND is_active=1";
                    SqlCommand selectCmd = new SqlCommand(selectQry, con);
                    selectCmd.Parameters.AddWithValue("@username", username.Text);
                    selectCmd.Parameters.AddWithValue("@password", Lpassword.Text);

                    SqlDataReader selectReader = selectCmd.ExecuteReader();
                    if (selectReader.Read())
                    {
                        int userType = Convert.ToInt32(selectReader["type"]);
                        string vendorId = selectReader["v_id"].ToString();
                        string userId = selectReader["id"].ToString();

                        if (userType == 2 && string.IsNullOrEmpty(vendorId))
                        {
                            Label1.Text = "Error: Vendor ID not found.";
                            return;
                        }

                        Label1.Text = "User Verified";
                        Session["user"] = userId; // Set user ID in session

                        switch (userType)
                        {
                            case 1:
                                Response.Redirect("~/admin/dashboard.aspx");
                                break;
                            case 2:
                                Response.Redirect($"~/Vendor/vendor_home.aspx?vendorId={vendorId}");
                                break;
                            case 3:
                                Response.Redirect($"~/Home.aspx?userId={userId}");
                                break;
                            default:
                                Label1.Text = "Invalid user type";
                                break;
                        }

                        // Call method to update user profile dropdown
                        UpdateUserProfileDropdown();
                    }
                    else
                    {
                        Label1.Text = "Invalid credentials";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Error occurred: " + ex.Message);
                Label1.Text = "An error occurred during login. Please try again later.";
            }
        }

        protected void signup_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString))
                {
                    con.Open();

                    // Check if email already exists
                    string CheckEmailOnce = "SELECT id FROM [signin] WHERE email = @Email";
                    SqlCommand checkEmailCmd = new SqlCommand(CheckEmailOnce, con);
                    checkEmailCmd.Parameters.AddWithValue("@Email", email.Text);

                    object result = checkEmailCmd.ExecuteScalar();

                    if (result != null)
                    {
                        lblErrorMsg.Text = "Email address already exists. Please try with a different email address.";
                        lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    // Generate activation code
                    Random rnd = new Random();
                    int myRandomNo = rnd.Next(10000000, 99999999);
                    string activation_code = myRandomNo.ToString();

                    // Insert user details into database
                    string insertQry = "INSERT INTO [signin] (firstname, lastname, email, number, password, activation_code, is_active, type) " +
                                       "VALUES (@firstname, @lastname, @Email, @number, @password, @activation_code, @is_active, @type)";
                    SqlCommand insertCmd = new SqlCommand(insertQry, con);
                    insertCmd.Parameters.AddWithValue("@firstname", firstName.Text);
                    insertCmd.Parameters.AddWithValue("@lastname", lastName.Text);
                    insertCmd.Parameters.AddWithValue("@Email", email.Text);
                    insertCmd.Parameters.AddWithValue("@password", password.Text); // TODO: Hash the password
                    insertCmd.Parameters.AddWithValue("@number", number.Text);
                    insertCmd.Parameters.AddWithValue("@activation_code", activation_code);
                    insertCmd.Parameters.AddWithValue("@is_active", 0);
                    insertCmd.Parameters.AddWithValue("@type", 3); // Set user type as 3

                    insertCmd.ExecuteNonQuery();

                    con.Close();

                    // Display signup success message
                    ScriptManager.RegisterStartupScript(this, GetType(), "signupAlert", "showSignupSuccessAlert();", true);

                    // Send activation email
                    SendActivationEmail(email.Text, firstName.Text, activation_code);
                }
                UpdateUserProfileDropdown();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Error occurred: " + ex.Message);
                lblErrorMsg.Text = "An error occurred during signup. Please try again later.";
                lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void SendActivationEmail(string recipientEmail, string firstName, string activationCode)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(recipientEmail, "Urban Ease"));
            mail.From = new MailAddress("urban.ease4all@gmail.com", "Urban Ease");
            mail.Subject = "Welcome to Urban Ease! Activate Your Account";

            string emailBody = @"
                <html>
                <head>
                    <style>
                        body { font-family: Arial, sans-serif; }
                        .container { padding: 20px; border: 4px solid #A02eff; background-color: #fff; }
                        .button { border: 2px solid #A05eff; color: #fff; background-color: #A02eff; text-decoration: none; padding: 12px 20px; }
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <p style='font-size: 18px; font-weight: 600'>Hello " + firstName + @"!</p>
                        <p>Welcome to Urban Ease! We are thrilled to have you as a new member of our community.</p>
                        <p>Your account has been successfully created. You're now set to explore and benefit from our platform.</p>
                        <p>To activate your account, simply click the button below:</p>
                        <p style='text-align: center; margin: 24px 0;'>
                            <a href='http://localhost:57380/Activate.aspx?activation_code=" + activationCode + "&email=" + Base64Encode(recipientEmail) + @"' class='button'>ACTIVATE ACCOUNT</a>
                        </p>
                        <p>If you have additional queries, please feel free to reach us at <span style='font-weight: 500;'>+91 XXXX XXX XXX</span> or drop us an email at <a href='mailto:urban.ease4all@gmail.com' style='text-decoration: none; color: orange;'>urban.ease4all@gmail.com</a></p>
                        <p style='font-size: 18px; font-weight: 600'>Thanks & Regards</p>
                        <p>Team Urban Ease</p>
                    </div>
                </body>
                </html>";

            mail.Body = emailBody;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("urban.ease4all@gmail.com", "evzt sfbi itnx xrxa");
            smtp.Send(mail);
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            string userEmail = txtForgotPasswordEmail.Text;

            try
            {
                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["conString"].ConnectionString))
                {
                    con.Open();

                    string selectQry = "SELECT id, firstname FROM [signin] WHERE email = @Email";
                    SqlCommand selectCmd = new SqlCommand(selectQry, con);
                    selectCmd.Parameters.AddWithValue("@Email", userEmail);
                    SqlDataReader reader = selectCmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string userId = reader["id"].ToString();
                        string firstName = reader["firstname"].ToString();
                        reader.Close();

                        string otp = GenerateOTP();
                        DateTime tokenExpiry = DateTime.Now.AddMinutes(15);

                        string updateQry = "UPDATE [signin] SET reset_token = @OTP, reset_token_expiry = @TokenExpiry WHERE id = @UserId";
                        SqlCommand updateCmd = new SqlCommand(updateQry, con);
                        updateCmd.Parameters.AddWithValue("@OTP", otp);
                        updateCmd.Parameters.AddWithValue("@TokenExpiry", tokenExpiry);
                        updateCmd.Parameters.AddWithValue("@UserId", userId);
                        updateCmd.ExecuteNonQuery();

                        SendOTPEmail(userEmail, firstName, otp);
                        lblForgotPasswordMessage.Text = "An OTP has been sent to your email.";
                        txtOTP.Visible = true;
                        btnValidateOTP.Visible = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "otpSentAlert", "Swal.fire('Success', 'An OTP has been sent to your email.', 'success');", true);
                    }
                    else
                    {
                        lblForgotPasswordMessage.Text = "Email address not found.";
                    }

                    lblForgotPasswordMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Error occurred: " + ex.Message);
                lblForgotPasswordMessage.Text = "An error occurred. Please try again later.";
                lblForgotPasswordMessage.Visible = true;
            }
        }

        protected void btnValidateOTP_Click(object sender, EventArgs e)
        {
            string userEmail = txtForgotPasswordEmail.Text;
            string enteredOTP = txtOTP.Text;

            try
            {
                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["conString"].ConnectionString))
                {
                    con.Open();

                    string selectQry = "SELECT id, reset_token, reset_token_expiry FROM [signin] WHERE email = @Email";
                    SqlCommand selectCmd = new SqlCommand(selectQry, con);
                    selectCmd.Parameters.AddWithValue("@Email", userEmail);
                    SqlDataReader reader = selectCmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string userId = reader["id"].ToString();
                        string storedOTP = reader["reset_token"].ToString();
                        DateTime tokenExpiry = Convert.ToDateTime(reader["reset_token_expiry"]);

                        if (storedOTP == enteredOTP && DateTime.Now <= tokenExpiry)
                        {
                            lblForgotPasswordMessage.Text = "OTP validated. Please enter your new password.";
                            txtNewPassword.Visible = true;
                            txtConfirmPassword.Visible = true;
                            btnResetPassword.Visible = true;
                            ViewState["UserId"] = userId;
                        }
                        else
                        {
                            lblForgotPasswordMessage.Text = "Invalid OTP or OTP expired.";
                        }
                    }
                    else
                    {
                        lblForgotPasswordMessage.Text = "Email address not found.";
                    }

                    lblForgotPasswordMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Error occurred: " + ex.Message);
                lblForgotPasswordMessage.Text = "An error occurred. Please try again later.";
                lblForgotPasswordMessage.Visible = true;
            }
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userId = ViewState["UserId"] as string;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (newPassword == confirmPassword)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["conString"].ConnectionString))
                    {
                        con.Open();

                        string updateQry = "UPDATE [signin] SET password = @Password, reset_token = NULL, reset_token_expiry = NULL WHERE id = @UserId";
                        SqlCommand updateCmd = new SqlCommand(updateQry, con);
                        updateCmd.Parameters.AddWithValue("@Password", newPassword); // Ideally, hash the password before storing
                        updateCmd.Parameters.AddWithValue("@UserId", userId);
                        updateCmd.ExecuteNonQuery();

                        lblForgotPasswordMessage.Text = "Your password has been reset successfully.";
                        txtNewPassword.Visible = false;
                        txtConfirmPassword.Visible = false;
                        btnResetPassword.Visible = false;

                        // Show success pop-up
                        ScriptManager.RegisterStartupScript(this, GetType(), "passwordResetSuccess", @"
                            Swal.fire('Success', 'Your password has been reset successfully.', 'success').then(function() {
                                $('#forgotPasswordForm').hide();
                                $('#loginForm').show();
                            });
                        ", true);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine("Error occurred: " + ex.Message);
                    lblForgotPasswordMessage.Text = "An error occurred. Please try again later.";
                }
            }
            else
            {
                lblForgotPasswordMessage.Text = "Passwords do not match.";
            }

            lblForgotPasswordMessage.Visible = true;
        }

        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private void SendOTPEmail(string recipientEmail, string firstName, string otp)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(recipientEmail, "Urban Ease"));
            mail.From = new MailAddress("urban.ease4all@gmail.com", "Urban Ease");
            mail.Subject = "Urban Ease OTP for Password Reset";

            string emailBody = @"
                <html>
                <head>
                    <style>
                        body { font-family: Arial, sans-serif; }
                        .container { padding: 20px; border: 4px solid #A02eff; background-color: #fff; }
                        .otp { font-size: 24px; font-weight: bold; }
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <p style='font-size: 18px; font-weight: 600'>Hello " + firstName + @"!</p>
                        <p>Your OTP for password reset is:</p>
                        <p class='otp'>" + otp + @"</p>
                        <p>This OTP is valid for 15 minutes.</p>
                        <p>If you did not request this, please ignore this email.</p>
                        <p style='font-size: 18px; font-weight: 600'>Thanks & Regards</p>
                        <p>Team Urban Ease</p>
                    </div>
                </body>
                </html>";

            mail.Body = emailBody;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("urban.ease4all@gmail.com", "evzt sfbi itnx xrxa");
            smtp.Send(mail);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
