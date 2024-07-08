using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Urban_Ease_2.admin
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPartnerData();
                LoadServiceData();
                LoadBookingData();
            }
        }

        private void LoadPartnerData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT TOP 1000 id, firstname, lastname, Subcategory FROM partner", connection);
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                partnerTable.DataSource = dataTable;
                partnerTable.DataBind();
            }
        }

        private void LoadServiceData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT TOP 1000 service_id AS ServiceID, s_name AS Name, price AS Price FROM services", connection);
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                serviceTable.DataSource = dataTable;
                serviceTable.DataBind();
            }
        }

        private void LoadBookingData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"
    SELECT 
        a.id AS BookingID, 
        CONCAT(a.fname, ' ', a.lname) AS Name, 
        a.phone AS [Phone No], 
        STRING_AGG(s.s_name, ', ') AS Service,
        STRING_AGG(s.partner_name, ', ') AS PName, 
        CONVERT(varchar, a.date, 23) AS Date,
        a.status AS Status
    FROM 
        address a 
    CROSS APPLY 
        STRING_SPLIT(a.product_id, ',') AS p 
    JOIN 
        services s ON TRY_CAST(p.value AS INT) = s.service_id 
    GROUP BY 
        a.id, 
        a.fname, 
        a.lname, 
        a.phone, 
        a.date,
        a.status
    ORDER BY 
        a.id";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        bookingTable.DataSource = dataTable;
                        bookingTable.DataBind();
                    }
                    else
                    {
                        Console.WriteLine("No data found for Booking Management.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General Error: " + ex.Message);
                }
            }
        }

        // Partner GridView Event Handlers
        protected void partnerTable_RowEditing(object sender, GridViewEditEventArgs e)
        {
            partnerTable.EditIndex = e.NewEditIndex;
            LoadPartnerData();
        }

        protected void partnerTable_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = partnerTable.Rows[e.RowIndex];
            int partnerId = Convert.ToInt32(partnerTable.DataKeys[e.RowIndex].Values[0]);
            string firstname = (row.Cells[1].Controls[0] as TextBox).Text;
            string lastname = (row.Cells[2].Controls[0] as TextBox).Text;
            string subcategory = (row.Cells[3].Controls[0] as TextBox).Text;

            UpdatePartner(partnerId, firstname, lastname, subcategory);

            partnerTable.EditIndex = -1;
            LoadPartnerData();
        }

        protected void partnerTable_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            partnerTable.EditIndex = -1;
            LoadPartnerData();
        }

        private void UpdatePartner(int partnerId, string firstname, string lastname, string subcategory)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE partner SET firstname = @firstname, lastname = @lastname, subcategory = @subcategory WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", partnerId);
                command.Parameters.AddWithValue("@firstname", firstname);
                command.Parameters.AddWithValue("@lastname", lastname);
                command.Parameters.AddWithValue("@subcategory", subcategory);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected void partnerTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int partnerId = Convert.ToInt32(e.CommandArgument);
                DeletePartner(partnerId);
            }
        }

        private void DeletePartner(int partnerId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM partner WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", partnerId);
                connection.Open();
                command.ExecuteNonQuery();
                LoadPartnerData(); // Refresh the data
            }
        }

        // Service GridView Event Handlers
        protected void serviceTable_RowEditing(object sender, GridViewEditEventArgs e)
        {
            serviceTable.EditIndex = e.NewEditIndex;
            LoadServiceData();
        }

        protected void serviceTable_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = serviceTable.Rows[e.RowIndex];
            int serviceId = Convert.ToInt32(serviceTable.DataKeys[e.RowIndex].Values[0]);
            string name = (row.Cells[1].Controls[0] as TextBox).Text;
            decimal price = Convert.ToDecimal((row.Cells[2].Controls[0] as TextBox).Text);

            UpdateService(serviceId, name, price);

            serviceTable.EditIndex = -1;
            LoadServiceData();
        }

        protected void serviceTable_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            serviceTable.EditIndex = -1;
            LoadServiceData();
        }

        private void UpdateService(int serviceId, string name, decimal price)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE services SET s_name = @name, price = @price WHERE service_id = @id", connection);
                command.Parameters.AddWithValue("@id", serviceId);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected void serviceTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int serviceId = Convert.ToInt32(e.CommandArgument);
                DeleteService(serviceId);
            }
        }

        private void DeleteService(int serviceId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM services WHERE service_id = @id", connection);
                command.Parameters.AddWithValue("@id", serviceId);
                connection.Open();
                command.ExecuteNonQuery();
                LoadServiceData(); // Refresh the data
            }
        }
    }
}
