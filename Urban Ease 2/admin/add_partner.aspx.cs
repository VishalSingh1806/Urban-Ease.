using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Urban_Ease_2.admin
{
    public partial class add_partner : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Perform any setup necessary, such as loading categories from a database
            }
            else
            {
                // Restore the password from the hidden field on postback
                Password.Attributes["value"] = HiddenPassword.Value;
            }
        }

        protected void WorkCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubCategory.Items.Clear();
            string selectedCategory = WorkCategory.SelectedValue;

            switch (selectedCategory)
            {
                case "Woman's Section":
                     // Woman's Section
                    SubCategory.Items.Add(new ListItem("Pedicure", "Pedicure"));
                    SubCategory.Items.Add(new ListItem("Manicure", "Manicure"));
                    SubCategory.Items.Add(new ListItem("Facial & Cleanup", "Facial & Cleanup"));
                    SubCategory.Items.Add(new ListItem("Hair Care", "Hair Care"));
                    SubCategory.Items.Add(new ListItem("Bleach & Detan", "Bleach & Detan"));
                    SubCategory.Items.Add(new ListItem("Threading & Waxing", "Threading & Waxing"));
                    break;
                case "Man's Care":
                    SubCategory.Items.Add(new ListItem("Pedicure", "Pedicure"));
                    SubCategory.Items.Add(new ListItem("Men/Kids Haircut", "Men/Kids Haircut"));
                    SubCategory.Items.Add(new ListItem("Face Care", "Face Care"));
                    SubCategory.Items.Add(new ListItem("Shave/Beard Grooming", "Shave/Beard Grooming"));
                    SubCategory.Items.Add(new ListItem("Hair Color", "Hair Color"));
                    SubCategory.Items.Add(new ListItem("Massage", "Massage"));
                    break;

                case "repair":
                    SubCategory.Items.Add(new ListItem("Cooler Repair", "Cooler Repair"));
                    SubCategory.Items.Add(new ListItem("Chimney Repair", "Chimney Repair"));
                    SubCategory.Items.Add(new ListItem("Geyser Repair", "Geyser Repair"));
                    SubCategory.Items.Add(new ListItem("Laptop Repair", "Laptop Repair"));
                    SubCategory.Items.Add(new ListItem("Microwave Repair", "Microwave Repair"));
                    SubCategory.Items.Add(new ListItem("Fridge Repair", "Fridge Repair"));
                    SubCategory.Items.Add(new ListItem("TV Repair", "TV Repair"));
                    SubCategory.Items.Add(new ListItem("Water Purifier Repair", "Water Purifier Repair"));
                    SubCategory.Items.Add(new ListItem("Washing Machine Repair", "Washing Machine Repair"));
                    break;

                case "Cleaning & Pest Control":
                    SubCategory.Items.Add(new ListItem("Bathroom & Kitchen Cleaning", "Bathroom & Kitchen Cleaning"));
                    SubCategory.Items.Add(new ListItem("Full House Cleaning", "Full House Cleaning"));
                    SubCategory.Items.Add(new ListItem("Sofa & Carpet Cleaning", "Sofa & Carpet Cleaning"));
                    SubCategory.Items.Add(new ListItem("General Pest Control", "General Pest Control"));
                    SubCategory.Items.Add(new ListItem("Termite Control", "Termite Control"));
                    SubCategory.Items.Add(new ListItem("Bed Bug Control", "Bed Bug Control"));
                    break;

                case "Electrician, Plumber & Carpenter":
                    SubCategory.Items.Add(new ListItem("Electrician", "Electrician"));
                    SubCategory.Items.Add(new ListItem("Plumber", "Plumber"));
                    SubCategory.Items.Add(new ListItem("Carpenter", "Carpenter"));
                    SubCategory.Items.Add(new ListItem("Ceiling Installation", "Ceiling Installation"));
                    SubCategory.Items.Add(new ListItem("Furniture Assembly", "Furniture Assembly"));
                    break;

                case "Native Water Purifier":
                    SubCategory.Items.Add(new ListItem("Native Water Purifier", "Native Water Purifier"));
                    break;

                case "Smart Locks":
                    SubCategory.Items.Add(new ListItem("Smart Locks", "Smart Locks"));
                    break;
                case "Painting & Waterproofing":
                    SubCategory.Items.Add(new ListItem("Full House Painting", "Full House Painting"));
                    SubCategory.Items.Add(new ListItem("2 Rooms Painting", "2 Rooms Painting"));
                    break;
                case "Wall Decor":
                    SubCategory.Items.Add(new ListItem("Wall Decor", "Wall Decor"));
                    break;

                default:
                    SubCategory.Items.Add(new ListItem("Select Subcategory", ""));
                    break;
            }
        }

        protected void AddPartnerButton_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            ClearErrorMessages();

            string firstName = FirstName.Text.Trim();
            string lastName = LastName.Text.Trim();
            string password = Password.Text.Trim();
            string contactNumber = ContactNumber.Text.Trim();
            string email = Email.Text.Trim();
            string workCategory = WorkCategory.SelectedValue;
            string subCategory = SubCategory.SelectedValue;
            string location = Location.Text.Trim();
            

            if (string.IsNullOrEmpty(firstName))
            {
                FirstNameError.Text = "First name is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(lastName))
            {
                LastNameError.Text = "Last name is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(password))
            {
                PasswordError.Text = "Password is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(contactNumber) || !float.TryParse(contactNumber, out _))
            {
                ContactNumberError.Text = "Valid contact number is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(email))
            {
                EmailError.Text = "Email is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(workCategory))
            {
                WorkCategoryError.Text = "Work category is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(subCategory))
            {
                SubCategoryError.Text = "Subcategory is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(location))
            {
                LocationError.Text = "Location is required.";
                isValid = false;
            }
            

            if (!isValid) return;

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        // Insert into partner table and get the inserted ID
                        string partnerQuery = "INSERT INTO partner (firstname, lastname, password, category, location, email, type, number, Subcategory) OUTPUT INSERTED.Id VALUES (@FirstName, @LastName, @Password, @Category, @Location, @Email, @Type, @Number, @Subcategory)";
                        int partnerId;
                        using (SqlCommand partnerCommand = new SqlCommand(partnerQuery, con, transaction))
                        {
                            partnerCommand.Parameters.AddWithValue("@FirstName", firstName);
                            partnerCommand.Parameters.AddWithValue("@LastName", lastName);
                            partnerCommand.Parameters.AddWithValue("@Password", password);
                            partnerCommand.Parameters.AddWithValue("@Category", workCategory);
                            partnerCommand.Parameters.AddWithValue("@Location", location);
                            partnerCommand.Parameters.AddWithValue("@Email", email);
                            partnerCommand.Parameters.AddWithValue("@Type", 2);
                            partnerCommand.Parameters.AddWithValue("@Number", contactNumber);
                            partnerCommand.Parameters.AddWithValue("@Subcategory", subCategory);

                            partnerId = (int)partnerCommand.ExecuteScalar();
                        }

                        // Insert into signin table using the retrieved partner ID
                        string signinQuery = "INSERT INTO signin (firstname, lastname, email, password, number, type, v_id) VALUES (@FirstName, @LastName, @Email, @Password, @Number, @Type, @VId)";
                        using (SqlCommand signinCommand = new SqlCommand(signinQuery, con, transaction))
                        {
                            signinCommand.Parameters.AddWithValue("@FirstName", firstName);
                            signinCommand.Parameters.AddWithValue("@LastName", lastName);
                            signinCommand.Parameters.AddWithValue("@Email", email);
                            signinCommand.Parameters.AddWithValue("@Password", password);
                            signinCommand.Parameters.AddWithValue("@Number", contactNumber);
                            signinCommand.Parameters.AddWithValue("@Type", 2);
                            signinCommand.Parameters.AddWithValue("@VId", partnerId);

                            signinCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        // Clear the form fields
                        FirstName.Text = "";
                        LastName.Text = "";
                        Password.Text = "";
                        ContactNumber.Text = "";
                        Email.Text = "";
                        WorkCategory.SelectedIndex = 0;
                        SubCategory.Items.Clear();
                        Location.Text = "";
                        

                        // Show success message using SweetAlert
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "Swal.fire({ icon: 'success', title: 'Partner Added Successfully', showConfirmButton: false, timer: 1500 });", true);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // Log the error (ex.Message) if needed
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"Swal.fire({{ icon: 'error', title: 'Error', text: '{ex.Message}' }});", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (ex.Message) if needed
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"Swal.fire({{ icon: 'error', title: 'Error', text: '{ex.Message}' }});", true);
            }
        }


        private void ClearErrorMessages()
        {
            FirstNameError.Text = "";
            LastNameError.Text = "";
            PasswordError.Text = "";
            ContactNumberError.Text = "";
            EmailError.Text = "";
            WorkCategoryError.Text = "";
            SubCategoryError.Text = "";
            LocationError.Text = "";
            
        }
    }
}
