using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Urban_Ease_2
{
    public partial class water_purifier : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand selectCmd = new SqlCommand("SELECT service_id, s_name, price, partner_name, description, photo FROM [services] WHERE category = @category", con);
                selectCmd.Parameters.AddWithValue("@category", "Native Water Purifier");

                SqlDataReader dr = selectCmd.ExecuteReader();

                while (dr.Read())
                {
                    string serviceName = dr["s_name"].ToString();
                    decimal price = Convert.ToDecimal(dr["price"]);
                    string partnerName = dr["partner_name"].ToString();
                    string description = dr["description"].ToString();
                    int itemId = Convert.ToInt32(dr["service_id"]);

                    byte[] photoBytes = dr["photo"] as byte[];
                    string base64Image = (photoBytes != null) ? Convert.ToBase64String(photoBytes) : "";

                    // Generate HTML for the card using the retrieved values
                    Literal1.Text += $@"
<div class='card shadow-lg mb-4' style='border-radius: 20px; overflow: hidden; background: linear-gradient(145deg, #2c3e50, #3498db);'>
    <div class='row g-0'>
        <div class='col-md-6' style='position: relative;'>
            <div class='image-overlay'></div>
            {(string.IsNullOrEmpty(base64Image) ? "<div class='placeholder-image' style='height: 100%; background: #ecf0f1;'></div>" : $"<img src='data:image;base64,{base64Image}' class='img-fluid' style='height: 100%; object-fit: cover; filter: brightness(0.8);' alt='Service Image'>")}
            <div class='image-overlay-content' style='position: absolute; bottom: 20px; left: 20px; background: rgba(0, 0, 0, 0.5); padding: 10px 20px; border-radius: 10px;'>
                <h5 class='card-title' style='font-weight: bold; color: #fff;'>{serviceName}</h5>
            </div>
        </div>
        <div class='col-md-6'>
            <div class='card-body' style='padding: 40px; background: rgba(0, 0, 0, 0.6); color: #ecf0f1;'>
                <p class='card-text mb-3'>Price: {price.ToString("C")}</p>
                <p class='card-text mb-3'>Partner Name: {partnerName}</p>
                <p class='card-text mb-3'>Description: {description}</p>
                <button type='button' class='btn btn-primary btn-sm btn-add-to-cart' data-itemId='{itemId}' data-itemName='{serviceName}' data-itemPrice='{price}' style='border-radius: 20px; background-color: #3498db; color: #fff; border: none;'>Add to Cart</button>
            </div>
        </div>
    </div>
</div>";

                }


                dr.Close(); // Close the data reader after processing
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Literal1.Text = "Error loading items: " + ex.Message;
            }
            finally
            {
                con.Close(); // Close the connection after processing all records
            }
        }
        protected string GetImageUrl(object img)
        {
            if (img != DBNull.Value && img != null && img is byte[])
            {
                byte[] imageData = (byte[])img;
                string base64String = Convert.ToBase64String(imageData);
                return "data:image/jpeg;base64," + base64String;
            }
            else
            {
                // Handle the case where img is DBNull.Value or not a byte array
                return ""; // Replace with a placeholder image URL or an empty string
            }
        }
    }
}
