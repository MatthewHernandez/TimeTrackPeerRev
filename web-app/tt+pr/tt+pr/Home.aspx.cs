using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tt_pr;

using System;

namespace tt_pr
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsLoggedIn"] == null || !(bool)Session["IsLoggedIn"])
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            var selectedDate = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
            ClientScript.RegisterStartupScript(this.GetType(), "updateSelectedDate", $"updateSelectedDate('{selectedDate}');", true);
        }
    }
}