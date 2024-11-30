using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tt_pr
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsLoggedIn"] != null && (bool)Session["IsLoggedIn"])
            {
                Response.Redirect("~/Home.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string netID = txtNetID.Text;
            string utdID = txtUTDID.Text;

            // Replace with your actual login validation logic
            if (netID == "btw190002" && utdID == "password")
            {
                Session["IsLoggedIn"] = true;
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid Net ID or UTD-ID.";
            }
        }
    }
}