using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Urban_Ease_2
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Capture the userId from the query string
                string userId = Request.QueryString["userId"];

                // Check if userId is not null or empty
                if (!string.IsNullOrEmpty(userId))
                {
                    // Store the userId in the session
                    Session["UserID"] = userId;
                }

                // Debugging: Log the user ID
                System.Diagnostics.Debug.WriteLine("User ID from Query String: " + userId);
                System.Diagnostics.Debug.WriteLine("User ID stored in Session: " + Session["UserID"]);
            }
        }

    }
}