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
    public partial class Activate : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {


            string activation_code = Request.QueryString["activation_code"].ToString();
            string email_id = Request.QueryString["email"].ToString();
            string email = Base64Decode(email_id).ToString();

            con.Open();
            string matchQry = "select id from [signin] where email='" + email + "' and activation_code='" + activation_code + "' and is_active=0";
            SqlCommand matchCmd = new SqlCommand(matchQry, con);
            SqlDataReader matchReader = matchCmd.ExecuteReader();
            if (matchReader.HasRows)
            {

                con.Close();

                // update activation_code and is_active
                con.Open();
                string updateQry = "update [signin] set activation_code='0', is_active=1 where email='" + email + "'";
                SqlCommand updateCmd = new SqlCommand(updateQry, con);
                updateCmd.ExecuteNonQuery();
                string script = @"<script>
                             Swal.fire({
                                 icon: 'success',
                                 title: 'Sign Up succesfully!',
                                 showConfirmButton: false,
                                 timer: 1500
                             }).then(function () {
     window.location.href = 'Home.aspx'; } );
                         </script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script);
                
                con.Close();

            }
            else
            {
                con.Close();
            }

            // Response.Redirect("login.aspx");
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}