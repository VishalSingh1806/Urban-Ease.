using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace Urban_Ease_2.admin
{
    public partial class add_services : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate categories on initial load
                PopulateCategories();
                // Populate service names based on default category (e.g., Woman's Section)
                PopulateServiceNames("1");
            }
        }

        protected void PopulateCategories()
        {
            // Define categories and populate categoryDropdown
            categoryDropdown.Items.Clear();
            categoryDropdown.Items.Add(new ListItem("Woman's Section", "1"));
            categoryDropdown.Items.Add(new ListItem("Man's Care", "2"));
            categoryDropdown.Items.Add(new ListItem("repair", "3"));
            categoryDropdown.Items.Add(new ListItem("Cleaning & Pest Control", "4"));
            categoryDropdown.Items.Add(new ListItem("Electrician, Plumber & Carpenter", "5"));
            categoryDropdown.Items.Add(new ListItem("Native Water Purifier", "6"));
            categoryDropdown.Items.Add(new ListItem("Smart Locks", "7"));
            categoryDropdown.Items.Add(new ListItem("Painting & Waterproofing", "8"));
            categoryDropdown.Items.Add(new ListItem("Wall Decor", "9"));
            // Add other categories as needed
        }

        protected void categoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoryId = categoryDropdown.SelectedValue;
            PopulateServiceNames(categoryId);
        }

        private void PopulateServiceNames(string categoryId)
        {
            serviceNameDropdown.Items.Clear(); // Clear existing items

            // Example: Populate serviceNameDropdown based on selected category
            switch (categoryId)
            {
                case "1": // Woman's Section
                    serviceNameDropdown.Items.Add(new ListItem("Pedicure", "Pedicure"));
                    serviceNameDropdown.Items.Add(new ListItem("Manicure", "Manicure"));
                    serviceNameDropdown.Items.Add(new ListItem("Facial & Cleanup", "Facial & Cleanup"));
                    serviceNameDropdown.Items.Add(new ListItem("Hair Care", "Hair Care"));
                    serviceNameDropdown.Items.Add(new ListItem("Bleach & Detan", "Bleach & Detan"));
                    serviceNameDropdown.Items.Add(new ListItem("Threading & Waxing", "Threading & Waxing"));
                    break;

                case "2": // Man's Care
                    serviceNameDropdown.Items.Add(new ListItem("Pedicure", "Pedicure"));
                    serviceNameDropdown.Items.Add(new ListItem("Men/Kids Haircut", "Men/Kids Haircut"));
                    serviceNameDropdown.Items.Add(new ListItem("Face Care", "Face Care"));
                    serviceNameDropdown.Items.Add(new ListItem("Shave/Beard Grooming", "Shave/Beard Grooming"));
                    serviceNameDropdown.Items.Add(new ListItem("Hair Color", "Hair Color"));
                    serviceNameDropdown.Items.Add(new ListItem("Massage", "Massage"));
                    break;

                case "3": // Repair 
                    serviceNameDropdown.Items.Add(new ListItem("Cooler Repair", "Cooler Repair"));
                    serviceNameDropdown.Items.Add(new ListItem("Chimney Repair", "Chimney Repair"));
                    serviceNameDropdown.Items.Add(new ListItem("Geyser Repair", "Geyser Repair"));
                    serviceNameDropdown.Items.Add(new ListItem("Laptop Repair", "Laptop Repair"));
                    serviceNameDropdown.Items.Add(new ListItem("Microwave Repair", "Microwave Repair"));
                    serviceNameDropdown.Items.Add(new ListItem("Fridge Repair", "Fridge Repair"));
                    serviceNameDropdown.Items.Add(new ListItem("TV Repair", "TV Repair"));
                    serviceNameDropdown.Items.Add(new ListItem("Water Purifier Repair", "Water Purifier Repair"));
                    serviceNameDropdown.Items.Add(new ListItem("Washing Machine Repair", "Washing Machine Repair"));
                    break;

                case "4": // Cleaning & Pest Control
                    serviceNameDropdown.Items.Add(new ListItem("Bathroom & Kitchen Cleaning", "Bathroom & Kitchen Cleaning"));
                    serviceNameDropdown.Items.Add(new ListItem("Full House Cleaning", "Full House Cleaning"));
                    serviceNameDropdown.Items.Add(new ListItem("Sofa & Carpet Cleaning", "Sofa & Carpet Cleaning"));
                    serviceNameDropdown.Items.Add(new ListItem("General Pest Control", "General Pest Control"));
                    serviceNameDropdown.Items.Add(new ListItem("Termite Control", "Termite Control"));
                    serviceNameDropdown.Items.Add(new ListItem("Bed Bug Control", "Bed Bug Control"));
                    break;

                case "5": // Electrician, Plumber & Carpenter
                    serviceNameDropdown.Items.Add(new ListItem("Electrician", "Electrician"));
                    serviceNameDropdown.Items.Add(new ListItem("Plumber", "Plumber"));
                    serviceNameDropdown.Items.Add(new ListItem("Carpenter", "Carpenter"));
                    serviceNameDropdown.Items.Add(new ListItem("Ceiling Installation", "Ceiling Installation"));
                    serviceNameDropdown.Items.Add(new ListItem("Furniture Assembly", "Furniture Assembly"));
                    break;

                case "6": // Native Water Purifier
                    serviceNameDropdown.Items.Add(new ListItem("Native Water Purifier", "Native Water Purifier"));
                    break;

                case "7": // Smart Locks
                    serviceNameDropdown.Items.Add(new ListItem("Smart Locks", "Smart Locks"));
                    break;

                case "8": // Painting & Waterproofing
                    serviceNameDropdown.Items.Add(new ListItem("Full House Painting", "Full House Painting"));
                    serviceNameDropdown.Items.Add(new ListItem("2 Rooms Painting", "2 Rooms Painting"));
                    break;

                case "9": // Wall Decor
                    serviceNameDropdown.Items.Add(new ListItem("Wall Decor", "Wall Decor"));
                    break;

                // Add other cases for different categories with corresponding service names

                default:
                    break;
            }
        }

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            string categoryName = categoryDropdown.SelectedItem.Text;
            string serviceName = serviceNameDropdown.SelectedValue;
            decimal price = decimal.Parse(servicePrice.Text);
            string partnerNameValue = partnerName.Text;
            string descriptionValue = serviceDescription.Text;

            if (servicePhotos.HasFile)
            {
                try
                {
                    byte[] fileData;
                    using (BinaryReader reader = new BinaryReader(servicePhotos.PostedFile.InputStream))
                    {
                        fileData = reader.ReadBytes(servicePhotos.PostedFile.ContentLength);
                    }

                    string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string insertQuery = "INSERT INTO services (category, s_name, price, partner_name, description, photo) " +
                                             "VALUES (@category, @serviceName, @price, @partnerName, @description, @photo)";

                        SqlCommand cmd = new SqlCommand(insertQuery, conn);
                        cmd.Parameters.AddWithValue("@category", categoryName);
                        cmd.Parameters.AddWithValue("@serviceName", serviceName);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@partnerName", partnerNameValue);
                        cmd.Parameters.AddWithValue("@description", descriptionValue);
                        cmd.Parameters.AddWithValue("@photo", fileData);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Display success message
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Service added successfully');", true);
                        }
                        else
                        {
                            // Display error message if insertion failed
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Failed to add service');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log detailed error information
                    Console.WriteLine("Error: " + ex.Message);
                    // Display user-friendly error message
                    ClientScript.RegisterStartupScript(GetType(), "alert", $"alert('Error: {ex.Message}');", true);
                }
            }
            else
            {
                // Display error message if no file is uploaded
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please upload a file.');", true);
            }
        }
    }
}
