using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class ReturnDocument : System.Web.UI.Page
{
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadActiveLoans();
        }
    }

    private void LoadActiveLoans()
    {
        string query = "SELECT LoanID, (SELECT Username FROM Users WHERE UserID = Loans.UserID) AS Username, (SELECT Title FROM Documents WHERE DocumentID = Loans.DocumentID) AS DocumentTitle FROM Loans WHERE IsReturned = 0";
        DataTable dt = dbHelper.ExecuteQuery(query);

        ddlLoan.DataSource = dt;
        ddlLoan.DataTextField = "DocumentTitle";
        ddlLoan.DataValueField = "LoanID";
        ddlLoan.DataBind();

        ddlLoan.Items.Insert(0, new ListItem("--Select Loan--", ""));
    }

    protected void ddlLoan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLoan.SelectedValue != "")
        {
            int loanId = Convert.ToInt32(ddlLoan.SelectedValue);
            string query = "SELECT (SELECT Username FROM Users WHERE UserID = Loans.UserID) AS Username, (SELECT Title FROM Documents WHERE DocumentID = Loans.DocumentID) AS DocumentTitle FROM Loans WHERE LoanID = @LoanID";
            SqlParameter[] parameters = { new SqlParameter("@LoanID", loanId) };
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                lblMemberName.Text = dt.Rows[0]["Username"].ToString();
                lblDocTitle.Text = dt.Rows[0]["DocumentTitle"].ToString();
            }
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        int loanId = Convert.ToInt32(ddlLoan.SelectedValue);

        // Fetch DocumentID related to the LoanID and UserID, if the book is already returned
        string loanQuery = "SELECT d.DocumentID, d.Title FROM Loans l " +
                           "INNER JOIN Documents d ON l.DocumentID = d.DocumentID " +
                           "WHERE l.IsReturned = 0 AND l.LoanID = @LoanID";
        SqlParameter[] loanParameters =
        {
            new SqlParameter("@LoanID", loanId)
        };

        DataTable dtLoans = dbHelper.ExecuteQuery(loanQuery, loanParameters);
        if (dtLoans.Rows.Count == 0)
        {
            return;
        }
        string documentID = dtLoans.Rows[0]["DocumentID"].ToString();


        string query = "UPDATE Loans SET IsReturned = 1, ReturnDate = GETDATE() WHERE LoanID = @LoanID;" +
            "UPDATE Documents SET Status = 'Available' WHERE DocumentID = @DocumentID;";
        SqlParameter[] parameters = { new SqlParameter("@LoanID", loanId) , new SqlParameter("@DocumentID", documentID) };

        dbHelper.ExecuteNonQuery(query, parameters);
        LoadActiveLoans(); // Reload loans list after return
        lblMessage.Text = "Changes done!";
    }
}
