using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : Page
{
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFines();
        }
    }

    private void LoadFines()
    {
        int userId = Convert.ToInt32(Session["UserID"]);
        string query = "SELECT * FROM Fines WHERE UserID = @UserID AND IsPaid = 0";
        SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };
        DataTable dtFines = dbHelper.ExecuteQuery(query, parameters);
        lvFines.DataSource = dtFines;
        lvFines.DataBind();
    }

    protected void PayFine(object sender, EventArgs e)
    {
        string fineId = ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument;

        //Button btn = (Button)sender;
        //int fineId = Convert.ToInt32(btn.CommandArgument);

        // Retrieve the fine amount
        string query = "SELECT FineAmount FROM Fines WHERE FineID = @FineID";
        SqlParameter[] parameters = { new SqlParameter("@FineID", fineId) };
        DataTable dtFine = dbHelper.ExecuteQuery(query, parameters);

        if (dtFine.Rows.Count > 0)
        {
            decimal fineAmount = (decimal)dtFine.Rows[0]["FineAmount"];
            fineAmount = Math.Truncate(fineAmount);

            // Redirect to PayPal's sandbox for payment processing
            Response.Write("<form action='https://www.sandbox.paypal.com/cgi-bin/webscr' method='post' name ='buyCredits' id='buyCredits'>");
            Response.Write("<input type='hidden' name='cmd' value='_xclick'>");
            Response.Write("<input type='hidden' name='business' value='mzrsahil-facilitator-1@gmail.com'>");
            Response.Write("<input type='hidden' name='currency_code' value='USD'>"); // Change currency if needed
            Response.Write("<input type='hidden' name='item_name' value='Fine Payment'>");
            Response.Write("<input type='hidden' name='item_number' value='" + fineId + "'>");
            Response.Write("<input type='hidden' name='amount' value='" + fineAmount + "'>");
            Response.Write("<input type='hidden' name='return' value='http://localhost:62940/PayFineSuccess.aspx?fineId=" + fineId + "'>");
            Response.Write("<input type='hidden' name='cancel_return' value='http://localhost:62940/PayFine.aspx'>");
            Response.Write("</form>");
            Response.Write("<script type='text/javascript'>");
            Response.Write("document.getElementById('buyCredits').submit();");
            Response.Write("</script>");
        }
    }
}
