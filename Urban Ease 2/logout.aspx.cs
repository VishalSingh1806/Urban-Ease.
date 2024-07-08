using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Urban_Ease_2
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            string script = @"<script>
                                        Swal.fire({
                                            icon: 'success',
                                            title: 'Sign Out successfully!',
                                            showConfirmButton: false,
                                            timer: 1500
                                        }).then(function () {
                window.location.href = 'Home.aspx'; } );
                                    </script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script);
        }
    }
}