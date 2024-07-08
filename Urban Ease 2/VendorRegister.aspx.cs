using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace Urban_Ease_2
{
    public partial class VendorRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // You can add logic here if needed for Page_Load
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (IsValidProfessionSelected())
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        string insertQry = "INSERT INTO [vregister] (fname, lname, email, profession, message) VALUES (@fname, @lname, @email, @profession, @message)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQry, con))
                        {
                            insertCmd.Parameters.AddWithValue("@fname", fname.Text.Trim());
                            insertCmd.Parameters.AddWithValue("@lname", lname.Text.Trim());
                            insertCmd.Parameters.AddWithValue("@email", email.Text.Trim());
                            insertCmd.Parameters.AddWithValue("@message", message.Value);
                            insertCmd.Parameters.AddWithValue("@profession", DropDownList1.SelectedItem.Text);

                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    SendConfirmationEmail();

                    // RegisterStartupScript to show SweetAlert on client-side
                    string script = @"<script>
                                     Swal.fire({
                                         icon: 'success',
                                         title: 'Registration Successful!',
                                         text: 'You will receive a confirmation email shortly.',
                                         showConfirmButton: false,
                                         timer: 3000
                                     }).then(function () {
                                         window.location.href = 'Home.aspx';
                                     });
                                 </script>";

                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script);
                }
                catch (Exception ex)
                {
                    // Log error or display to user
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            else
            {
                // Display error message if no valid profession is selected
                string script = @"<script>
                                 Swal.fire({
                                     icon: 'error',
                                     title: 'Please select a valid profession.',
                                     showConfirmButton: false,
                                     timer: 1500
                                 });
                             </script>";

                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script);
            }
        }

        private bool IsValidProfessionSelected()
        {
            return DropDownList1.SelectedItem != null &&
                   !string.IsNullOrEmpty(DropDownList1.SelectedItem.Text) &&
                   !DropDownList1.SelectedItem.Text.Equals("Select Profession", StringComparison.OrdinalIgnoreCase);
        }

        private void SendConfirmationEmail()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.To.Add(email.Text.Trim());
                mail.From = new MailAddress("urban.ease4all@gmail.com");
                mail.Subject = "Thank you for registering with us.";

                string emailBody = $@"
                    <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f8f9fa;
                                color: #333;
                                padding: 20px;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: 0 auto;
                                background-color: #ffffff;
                                padding: 30px;
                                border-radius: 8px;
                                box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                            }}
                            h2 {{
                                text-align: center;
                                color: #A02eff;
                            }}
                            p {{
                                font-size: 16px;
                                line-height: 1.6;
                            }}
                            .btn {{
                                display: inline-block;
                                padding: 10px 20px;
                                margin-top: 20px;
                                text-decoration: none;
                                background-color: #A02eff;
                                color: #ffffff;
                                border-radius: 5px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h2>Welcome to Urban Ease!</h2>
                            <p>Dear {fname.Text.Trim()},</p>
                            <p>Thank you for registering with Urban Ease! We're excited to have you join our community.</p>
                            <p>Your registration details have been received successfully. Our team will review your submission and contact you shortly.</p>
                            <p>If you have any questions or need assistance, please feel free to reach out to us at <strong>support@urbanease.com</strong>.</p>
                            <p>Best Regards,</p>
                            <p>The Urban Ease Team</p>
                        </div>
                    </body>
                    </html>";

                mail.Body = emailBody;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("urban.ease4all@gmail.com", "evzt sfbi itnx xrxa");

                    smtp.Send(mail);
                }
            }
        }
    }
}
