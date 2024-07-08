using System;

namespace Urban_Ease_2.Vendor
{
    public partial class Vendor : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void SetVendorName(string vendorName)
        {
            LiteralVendorName.Text = $"Welcome, {vendorName}";
        }
    }
}
