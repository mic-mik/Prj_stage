using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class AccountingDashboard : System.Web.UI.Page
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
        string query = "SELECT Fines.FineID, Users.Username, Fines.FineAmount, Fines.FineDate " +
                       "FROM Fines " +
                       "INNER JOIN Users ON Fines.UserID = Users.UserID " +
                       "WHERE Fines.IsPaid = 0";
        DataTable dtFines = dbHelper.ExecuteQuery(query);
        gvFines.DataSource = dtFines;
        gvFines.DataBind();
    }

    protected void btnCollect_Click(object sender, EventArgs e)
    {
        Button btnCollect = (Button)sender;
        int fineId = Convert.ToInt32(btnCollect.CommandArgument);
        Response.Redirect("CollectPayment.aspx?FineID="+fineId);
    }
}
