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
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = Request.QueryString["token"];
                if (string.IsNullOrEmpty(token))
                {
                    LabelMessage.Text = "Invalid password reset token.";
                    ResetPasswordButton.Enabled = false;
                }
            }
        }

        protected void ResetPasswordButton_Click(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            string newPassword = NewPassword.Text;
            string confirmPassword = ConfirmPassword.Text;

            if (newPassword != confirmPassword)
            {
                LabelMessage.Text = "Passwords do not match.";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString))
                {
                    con.Open();
                    string selectQry = "SELECT id FROM [signin] WHERE reset_token = @Token AND reset_token_expiry > GETDATE()";
                    SqlCommand selectCmd = new SqlCommand(selectQry, con);
                    selectCmd.Parameters.AddWithValue("@Token", token);

                    object result = selectCmd.ExecuteScalar();

                    if (result != null)
                    {
                        string userId = result.ToString();
                        string updateQry = "UPDATE [signin] SET password = @NewPassword, reset_token = NULL, reset_token_expiry = NULL WHERE id = @UserId";
                        SqlCommand updateCmd = new SqlCommand(updateQry, con);
                        updateCmd.Parameters.AddWithValue("@NewPassword", HashPassword(newPassword));
                        updateCmd.Parameters.AddWithValue("@UserId", userId);

                        updateCmd.ExecuteNonQuery();

                        LabelSuccess.Text = "Your password has been reset successfully.";
                    }
                    else
                    {
                        LabelMessage.Text = "Invalid or expired token.";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Error occurred: " + ex.Message);
                LabelMessage.Text = "An error occurred. Please try again later.";
            }
        }

        public static string HashPassword(string password)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}