using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Services;
using System.Web.UI;
using Newtonsoft.Json.Linq;

namespace Urban_Ease_2.Vendor
{
    public partial class vendor_home : Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateVendorDetailsAndServices();
                SetVendorNameInMasterPage();
                LoadOrderHistory();
            }
        }

        private void Log(string message)
        {
            string logFilePath = Server.MapPath("~/App_Data/log.txt");
            string logDirectory = Path.GetDirectoryName(logFilePath);

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }

        protected void PopulateVendorDetailsAndServices()
        {
            try
            {
                con.Open();
                Log("Connection opened successfully.");

                string vendorId = Request.QueryString["vendorId"];
                Log($"Vendor ID: {vendorId}");
                if (string.IsNullOrEmpty(vendorId))
                {
                    LiteralNewOrders.Text = "Error: Vendor ID not specified.";
                    return;
                }

                SqlCommand partnerCmd = new SqlCommand(@"
SELECT [firstname], [lastname], [category], [Subcategory]
FROM [partner]
WHERE [id] = @vendorId", con);
                partnerCmd.Parameters.AddWithValue("@vendorId", vendorId);
                SqlDataReader partnerReader = partnerCmd.ExecuteReader();

                if (!partnerReader.Read())
                {
                    LiteralNewOrders.Text = "Vendor not found.";
                    Log("Vendor not found.");
                    partnerReader.Close();
                    return;
                }

                string firstName = partnerReader["firstname"].ToString();
                string lastName = partnerReader["lastname"].ToString();
                string category = partnerReader["category"].ToString();
                string subcategory = partnerReader["Subcategory"].ToString();
                partnerReader.Close();

                Log($"Vendor details: {firstName} {lastName}, Category: {category}, Subcategory: {subcategory}");

                SqlCommand serviceCmd = new SqlCommand(@"
SELECT [service_id], [s_name] 
FROM [services] 
WHERE [category] = @category AND [s_name] = @subcategory", con);
                serviceCmd.Parameters.AddWithValue("@category", category);
                serviceCmd.Parameters.AddWithValue("@subcategory", subcategory);
                SqlDataReader serviceReader = serviceCmd.ExecuteReader();

                bool serviceFound = false;
                string serviceId = null;
                string serviceName = null;

                if (serviceReader.Read())
                {
                    serviceId = serviceReader["service_id"].ToString();
                    serviceName = serviceReader["s_name"].ToString();
                    serviceFound = true;
                }
                serviceReader.Close();

                if (serviceFound)
                {
                    Log($"Service found: {serviceName} (ID: {serviceId})");

                    SqlCommand addressCmd = new SqlCommand(@"
SELECT [id], [product_id], [fname], [lname], [address], [state], [zip], [email], [phone], [note], [date], [time]
FROM [address]
WHERE [status] = 'Pending'", con); // Only select orders with status 'Pending'
                    SqlDataReader addressReader = addressCmd.ExecuteReader();

                    bool matchFound = false;
                    while (addressReader.Read())
                    {
                        string productIds = addressReader["product_id"].ToString();
                        string[] productIdArray = productIds.Split(',');

                        if (Array.Exists(productIdArray, productId => productId.Trim() == serviceId))
                        {
                            int addressId = (int)addressReader["id"];
                            string fname = addressReader["fname"].ToString();
                            string lname = addressReader["lname"].ToString();
                            string address = addressReader["address"].ToString();
                            string state = addressReader["state"].ToString();
                            string zip = addressReader["zip"].ToString();
                            string addressEmail = addressReader["email"].ToString();
                            string phone = addressReader["phone"].ToString();
                            string note = addressReader["note"].ToString();
                            string date = Convert.ToDateTime(addressReader["date"]).ToString("dd, MMMM, yyyy");
                            string time = string.Empty;

                            try
                            {
                                time = TimeSpan.Parse(addressReader["time"].ToString()).ToString(@"hh\:mm");
                            }
                            catch (FormatException)
                            {
                                time = "Invalid time format";
                            }

                            LiteralNewOrders.Text += $@"
<div class='order-card' id='order-card-{addressId}' data-product-id='{serviceId}'>
    <h3>Order #{addressId}</h3>
    <p><strong>Service:</strong> {serviceName}</p>
    <p><strong>Customer:</strong> {fname} {lname}</p>
    <p><strong>Phone No.:</strong> {phone}</p>
    <p><strong>Note.:</strong> {note}</p>
    <p><strong>Date:</strong> {date}</p>
    <p><strong>Time:</strong> {time}</p>
    <p><strong>Address:</strong> {address}</p>
    <p><strong>Zip Code:</strong> {zip}</p>
    <p><strong>State:</strong> {state}</p>
    <div id='order-actions-{addressId}' class='order-actions'>
        <button class='btn-accept' id='accept-{addressId}'>Accept</button>
        <button class='btn-decline'>Decline</button>
    </div>
    <div id='otp-section-{addressId}' class='otp-section' style='display:none;'>
        <input type='text' id='otp-input-{addressId}' class='otp-input' placeholder='Enter OTP' disabled>
        <button class='btn-submit-otp' id='submit-otp-{addressId}'>Submit OTP</button>
    </div>
</div>";
                            matchFound = true;
                        }
                    }
                    addressReader.Close();

                    if (!matchFound)
                    {
                        LiteralNewOrders.Text += "<p>You don't have new orders for now. Will notify you as soon as you get new orders.</p>";
                    }
                }
                else
                {
                    LiteralNewOrders.Text += "<p>No matching services found for this vendor.</p>";
                }
            }
            catch (Exception ex)
            {
                LiteralNewOrders.Text = "Error loading vendor details: " + ex.Message;
                Log($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();
                Log("Connection closed.");
            }
        }

        private void SetVendorNameInMasterPage()
        {
            try
            {
                con.Open();
                Log("Connection opened for setting vendor name.");

                string vendorId = Request.QueryString["vendorId"];
                if (string.IsNullOrEmpty(vendorId))
                {
                    return;
                }

                SqlCommand cmd = new SqlCommand(@"
            SELECT [firstname], [lastname] 
            FROM [partner] 
            WHERE [id] = @vendorId", con);
                cmd.Parameters.AddWithValue("@vendorId", vendorId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string firstName = reader["firstname"].ToString();
                    string lastName = reader["lastname"].ToString();
                    string vendorName = $"{firstName} {lastName}";

                    Vendor masterPage = this.Master as Vendor;
                    if (masterPage != null)
                    {
                        masterPage.SetVendorName(vendorName);
                    }

                    Log($"Vendor name set: {vendorName}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log($"Error setting vendor name: {ex.Message}");
            }
            finally
            {
                con.Close();
                Log("Connection closed after setting vendor name.");
            }
        }

        private void LoadOrderHistory()
        {
            try
            {
                con.Open();
                Log("Connection opened for loading order history.");

                string vendorId = Request.QueryString["vendorId"];
                if (string.IsNullOrEmpty(vendorId))
                {
                    LiteralOrderHistory.Text = "Error: Vendor ID not specified.";
                    return;
                }

                SqlCommand cmd = new SqlCommand(@"
                SELECT [id], [product_id], [fname], [lname], [address], [state], [zip], [email], [phone], [note], [date], [time]
                FROM [address]
                WHERE [status] = 'Completed'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int addressId = (int)reader["id"];
                    string fname = reader["fname"].ToString();
                    string lname = reader["lname"].ToString();
                    string address = reader["address"].ToString();
                    string state = reader["state"].ToString();
                    string zip = reader["zip"].ToString();
                    string email = reader["email"].ToString();
                    string phone = reader["phone"].ToString();
                    string note = reader["note"].ToString();
                    string date = Convert.ToDateTime(reader["date"]).ToString("dd, MMMM, yyyy");
                    string time = string.Empty;

                    try
                    {
                        time = TimeSpan.Parse(reader["time"].ToString()).ToString(@"hh\:mm");
                    }
                    catch (FormatException)
                    {
                        time = "Invalid time format";
                    }

                    LiteralOrderHistory.Text += $@"
                    <div class='order-card' id='order-card-{addressId}'>
                        <h3>Order #{addressId}</h3>
                        <p><strong>Customer:</strong> {fname} {lname}</p>
                        <p><strong>Phone No.:</strong> {phone}</p>
                        <p><strong>Note.:</strong> {note}</p>
                        <p><strong>Date:</strong> {date}</p>
                        <p><strong>Time:</strong> {time}</p>
                        <p><strong>Address:</strong> {address}</p>
                        <p><strong>Zip Code:</strong> {zip}</p>
                        <p><strong>State:</strong> {state}</p>
                    </div>";
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                LiteralOrderHistory.Text = "Error loading order history: " + ex.Message;
                Log($"Error loading order history: {ex.Message}");
            }
            finally
            {
                con.Close();
                Log("Connection closed after loading order history.");
            }
        }

        [WebMethod]
        public static string AcceptOrder(int orderId)
        {
            try
            {
                string otp = GenerateOTP();
                SaveOTPToDatabase(orderId, otp);
                SendOTPEmail(orderId, otp);
                return "Order accepted and OTP sent.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod]
        public static string VerifyOTP(int orderId, string otp, string productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT OTP, status_per_product, product_id FROM address WHERE id = @orderId", con);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string savedOtp = reader["OTP"].ToString();
                    if (savedOtp == otp)
                    {
                        string statusPerProduct = reader["status_per_product"].ToString();
                        JObject statusObj = string.IsNullOrEmpty(statusPerProduct) ? new JObject() : JObject.Parse(statusPerProduct);
                        statusObj[productId] = "Completed";

                        // Check if all product statuses are "Completed"
                        string productIds = reader["product_id"].ToString();
                        string[] productIdArray = productIds.Split(',');
                        bool allCompleted = true;
                        foreach (string id in productIdArray)
                        {
                            if (!statusObj.ContainsKey(id.Trim()) || statusObj[id.Trim()].ToString() != "Completed")
                            {
                                allCompleted = false;
                                break;
                            }
                        }

                        reader.Close();

                        // Update the status_per_product and overall status if all products are completed
                        cmd = new SqlCommand("UPDATE address SET status_per_product = @statusPerProduct, status = @overallStatus WHERE id = @orderId", con);
                        cmd.Parameters.AddWithValue("@statusPerProduct", statusObj.ToString());
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.Parameters.AddWithValue("@overallStatus", allCompleted ? "Completed" : "Pending");
                        cmd.ExecuteNonQuery();

                        return "OTP verified and order status updated for the specific product.";
                    }
                    else
                    {
                        return "Invalid OTP";
                    }
                }
                else
                {
                    return "Order not found";
                }
            }
        }

        private static string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private static void SaveOTPToDatabase(int orderId, string otp)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE address SET OTP = @otp WHERE id = @orderId", con);
                cmd.Parameters.AddWithValue("@otp", otp);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                cmd.ExecuteNonQuery();
            }
        }

        private static void SendOTPEmail(int orderId, string otp)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT email, fname FROM address WHERE id = @orderId", con);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string email = reader["email"].ToString();
                    string fname = reader["fname"].ToString();

                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress("urban.ease4all@gmail.com"),
                        Subject = "Order Confirmation and OTP",
                        Body = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f5f5f5;
                            color: #333333;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                            border: 1px solid #dddddd;
                            border-radius: 10px;
                            background-color: #ffffff;
                        }}
                        h1, h2, p {{
                            margin: 0;
                            padding: 0;
                        }}
                        h1 {{
                            color: #007bff;
                            font-size: 24px;
                            margin-bottom: 20px;
                        }}
                        h2 {{
                            color: #333333;
                            font-size: 20px;
                            margin-bottom: 10px;
                        }}
                        p {{
                            font-size: 16px;
                            line-height: 1.5;
                            margin-bottom: 10px;
                        }}
                        .otp {{
                            font-size: 28px;
                            color: #007bff;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h1>Order Confirmation and OTP</h1>
                        <p>Dear {fname},</p>
                        <p>Your service order has been accepted by the vendor.</p>
                        <p>Please use the following One-Time Password (OTP) to confirm the completion of the service:</p>
                        <h2 class='otp'>{otp}</h2>
                        <p>Thank you for choosing Urban Ease for your service needs. If you have any questions or concerns, feel free to contact us.</p>
                        <p>Best regards,</p>
                        <p>Team Urban Ease</p>
                    </div>
                </body>
                </html>",
                        IsBodyHtml = true
                    };

                    mail.To.Add(email);

                    SmtpClient smtpClient = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("urban.ease4all@gmail.com", "evzt sfbi itnx xrxa"),
                        DeliveryMethod = SmtpDeliveryMethod.Network
                    };

                    smtpClient.Send(mail);
                }
            }
        }

    }
}
