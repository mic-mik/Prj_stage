using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class client : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/"); // Redirect to login if not authenticated
        }
        int roleId = (int)Session["RoleID"];
        if (roleId != 6) // Check if the user is an accounting
        {
            Response.Redirect("~/"); // Redirect to login page
        }
        lblUsername.Text = Session["Username"].ToString();
       
    }

    protected void LinqLogout_Click(object sender, EventArgs e)
    {

        if (Session["UserID"] != null)
        {
            Session["UserID"] = null;
            Session["RoleID"] = null;
            Response.Redirect("~/Default.aspx");
        }
    }

}
