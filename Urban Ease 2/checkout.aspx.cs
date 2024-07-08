using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Services;
using System.Web.UI;

namespace Urban_Ease_2
{
    public partial class checkout : Page
    {
        // Declare the hidden field control
        protected global::System.Web.UI.HtmlControls.HtmlInputHidden user_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the user ID from the session
                if (Session["UserID"] != null)
                {
                    user_id.Value = Session["UserID"].ToString();
                    // Debugging: Log the user ID
                    System.Diagnostics.Debug.WriteLine("User ID from Session: " + Session["UserID"]);
                }
                else
                {
                    // Redirect to login page if session is null
                    Response.Redirect("Login.aspx");
                }
            }
        }

        [WebMethod]
        public static List<CheckoutCartItem> GetCartItems(List<int> cartItems)
        {
            List<CheckoutCartItem> items = new List<CheckoutCartItem>();

            // Database connection
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Build SQL query to retrieve details for cart items
                string query = "SELECT service_id, s_name, price FROM services WHERE service_id IN (" + string.Join(",", cartItems) + ")";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Inside the GetCartItems method
                        while (dr.Read())
                        {
                            int itemId = Convert.ToInt32(dr["service_id"]);
                            string itemName = dr["s_name"].ToString();
                            decimal itemPrice = Convert.ToDecimal(dr["price"]);

                            // Debugging: Log fetched data
                            Console.WriteLine($"Item ID: {itemId}, Item Name: {itemName}, Item Price: {itemPrice}");

                            // Create a new CheckoutCartItem object and add to the list
                            items.Add(new CheckoutCartItem { ServiceId = itemId, ServiceName = itemName, ServicePrice = itemPrice });
                        }
                    }
                }

                con.Close();
            }

            return items;
        }

        protected void Save_address_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve billing details from the form
                string firstName = fname.Value;
                string lastName = lname.Value;
                string address = this.address.Value;
                string stateCountry = state.Value;
                string postalZip = zip.Value;
                string email = this.email.Value;
                string phone = this.phone.Value;
                string note = c_order_notes.Value;

                // Retrieve date and time from the form
                string date = this.date.Value;
                string time = this.time.Value;

                // Retrieve product IDs from the hidden input field
                string productIds = product_ids.Value;

                // Retrieve user ID from the session
                string userId = Session["UserID"] != null ? Session["UserID"].ToString() : "0";

                // Check if user ID is valid
                if (string.IsNullOrEmpty(userId) || userId == "0")
                {
                    throw new Exception("Kindly Log in first to complete the purchase!");
                }

                // Database connection
                string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Build SQL query to insert data into the database
                    string query = @"
                INSERT INTO address (u_id, fname, lname, address, state, zip, email, phone, note, product_id, date, time, status)
                VALUES (@UserID, @FirstName, @LastName, @Address, @State, @Zip, @Email, @Phone, @Note, @ProductIds, @Date, @Time, @Status)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SQL query to prevent SQL injection
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@State", stateCountry);
                        cmd.Parameters.AddWithValue("@Zip", postalZip);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Note", note);
                        cmd.Parameters.AddWithValue("@ProductIds", productIds);
                        cmd.Parameters.AddWithValue("@Date", date);
                        cmd.Parameters.AddWithValue("@Time", time);
                        cmd.Parameters.AddWithValue("@Status", "Pending");

                        // Execute the SQL command
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
                var cartDetails = GetCartItems(new List<int>(Array.ConvertAll(productIds.Split(','), int.Parse)));

                // Send order confirmation email
                SendOrderConfirmationEmail(email, firstName, cartDetails);

                // Clear input fields
                ClearInputFields();

                // Display success message using SweetAlert and clear local storage
                string script = @"
        Swal.fire({
            title: 'Success',
            text: 'Your order has been placed successfully! You will shortly receive an email about your order confirmation and OTP.',
            icon: 'success',
            showConfirmButton: false,
            timer: 1500
        }).then(() => {
            localStorage.clear();
            window.location.href = 'Home.aspx';
        });";
                ScriptManager.RegisterStartupScript(this, GetType(), "PlaceOrderSuccess", script, true);
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                string script = $"Swal.fire('Error', '{ex.Message}', 'error');";
                ScriptManager.RegisterStartupScript(this, GetType(), "PlaceOrderError", script, true);
            }
        }

        private void SendOrderConfirmationEmail(string recipientEmail, string firstName, List<CheckoutCartItem> cartDetails)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(recipientEmail, "Urban Ease."));
            mail.From = new MailAddress("urban.ease4all@gmail.com", "Urban Ease.");
            mail.Subject = "Your Order Confirmation";

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
                    <p>Thank you for your order from Urban Ease. Below are the details of your order:</p>
                    <table style='width: 100%; border-collapse: collapse;'>
                        <thead>
                            <tr>
                                <th style='border: 1px solid #ddd; padding: 8px;'>Product</th>
                                <th style='border: 1px solid #ddd; padding: 8px;'>Price</th>
                            </tr>
                        </thead>
                        <tbody>";

            decimal totalAmount = 0;
            foreach (var item in cartDetails)
            {
                emailBody += $@"
                <tr>
                    <td style='border: 1px solid #ddd; padding: 8px;'>{item.ServiceName}</td>
                    <td style='border: 1px solid #ddd; padding: 8px;'>₹{item.ServicePrice}</td>
                </tr>";
                totalAmount += item.ServicePrice;
            }

            emailBody += $@"
                        </tbody>
                        <tfoot>
                            <tr>
                                <td style='border: 1px solid #ddd; padding: 8px;'><strong>Total</strong></td>
                                <td style='border: 1px solid #ddd; padding: 8px;'><strong>₹{totalAmount}</strong></td>
                            </tr>
                        </tfoot>
                    </table>
                    <p>If you have additional queries, please feel free to reach us at <span style='font-weight: 500;'>+91 XXXX XXX XXX</span> or drop us an email at <a href='mailto:urban.ease4all@gmail.com'>urban.ease4all@gmail.com</a>.</p>
                    <p>Thank you for choosing Urban Ease. We look forward to serving you!</p>
                    <p style='font-weight: 600'>Team Urban Ease</p>
                </div>
            </body>
            </html>";

            mail.Body = emailBody;
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("urban.ease4all@gmail.com", "evzt sfbi itnx xrxa"),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            try
            {
                smtpClient.Send(mail);
            }
            catch (SmtpException smtpEx)
            {
                // Log and display SMTP specific errors
                Console.WriteLine("SMTP Exception: " + smtpEx.Message);
                throw new Exception("There was a problem sending the confirmation email. Please try again later.");
            }
            catch (Exception ex)
            {
                // Log and display general errors
                Console.WriteLine("General Exception: " + ex.Message);
                throw new Exception("An unexpected error occurred while sending the confirmation email.");
            }
        }

        private void ClearInputFields()
        {
            fname.Value = "";
            lname.Value = "";
            address.Value = "";
            state.Value = "";
            zip.Value = "";
            email.Value = "";
            phone.Value = "";
            c_order_notes.Value = "";
            product_ids.Value = "";
            date.Value = "";
            time.Value = "";
        }
    }

    public class CheckoutCartItem
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
    }
}
