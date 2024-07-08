using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Urban_Ease_2
{
    public partial class contact : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string insertQry = "INSERT INTO [contact] (fname, lname, email, number, msg) VALUES (@fname, @lname, @email, @number, @msg)";

                using (SqlCommand insertCmd = new SqlCommand(insertQry, con))
                {
                    insertCmd.Parameters.AddWithValue("@fname", fname.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@lname", lname.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@email", email.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@number", number.Text);
                    insertCmd.Parameters.AddWithValue("@msg", msg.Text);

                    insertCmd.ExecuteNonQuery();
                }
            }



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
        
    }
}