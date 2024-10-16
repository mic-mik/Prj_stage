using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class ReturnBook : System.Web.UI.Page
{
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadLoans();
        }
    }

    private void LoadLoans()
    {
        int userId = Convert.ToInt32(Session["UserID"]);
        string query = @"
            SELECT l.LoanID, d.Title, d.PhotoPath, l.IsReturned AS Status 
            FROM Loans l 
            INNER JOIN Documents d ON l.DocumentID = d.DocumentID 
            WHERE l.UserID = @UserID AND l.IsReturned = 0";

        SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };
        DataTable dtLoans = dbHelper.ExecuteQuery(query, parameters);

        // Update the photo path if needed (replace '~' with '..' or other adjustments)
        foreach (DataRow row in dtLoans.Rows)
        {
            if (row["PhotoPath"] != DBNull.Value)
            {
                row["PhotoPath"] = row["PhotoPath"].ToString().Replace("~", ".."); // adjust the path as per your requirement
            }
        }

        // Bind the ListView to display loans with photos
        lvLoans.DataSource = dtLoans;
        lvLoans.DataBind();
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        try
        {
            // Get LoanID from the CommandArgument of the clicked button
            int loanId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            int userId = Convert.ToInt32(Session["UserID"]);

            // Query to get the DocumentID and check if the book is already returned
            string loanQuery = @"
                SELECT d.DocumentID, d.Title 
                FROM Loans l 
                INNER JOIN Documents d ON l.DocumentID = d.DocumentID 
                WHERE l.UserID = @UserID AND l.IsReturned = 0 AND l.LoanID = @LoanID";

            SqlParameter[] loanParameters =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@LoanID", loanId)
            };

            DataTable dtLoans = dbHelper.ExecuteQuery(loanQuery, loanParameters);
            if (dtLoans.Rows.Count == 0)
            {
                lblMessage.Text = "No matching loan found.";
                lblMessage.Visible = true;
                return;
            }

            string documentID = dtLoans.Rows[0]["DocumentID"].ToString();
            DateTime returnDate = DateTime.Now;

            // Update the loan status to mark it as returned and set the return date
            string updateQuery = @"
                UPDATE Loans 
                SET IsReturned = 1, ReturnDate = @ReturnDate 
                WHERE LoanID = @LoanID; 
                UPDATE Documents 
                SET Status = 'Available' 
                WHERE DocumentID = @DocumentID";

            SqlParameter[] updateParameters =
            {
                new SqlParameter("@LoanID", loanId),
                new SqlParameter("@ReturnDate", returnDate),
                new SqlParameter("@DocumentID", documentID)
            };
            dbHelper.ExecuteNonQuery(updateQuery, updateParameters);

            // Calculate fine if overdue
            string fineQuery = "SELECT DueDate FROM Loans WHERE LoanID = @LoanID";
            SqlParameter[] fineParams = { new SqlParameter("@LoanID", loanId) };
            DataTable dtFine = dbHelper.ExecuteQuery(fineQuery, fineParams);

            if (dtFine.Rows.Count > 0)
            {
                DateTime dueDate = (DateTime)dtFine.Rows[0]["DueDate"];
                if (returnDate > dueDate)
                {
                    decimal fineAmount = (decimal)(returnDate - dueDate).TotalDays * 1; // $1 per day fine
                    string insertFineQuery = "INSERT INTO Fines (UserID, FineAmount) VALUES (@UserID, @FineAmount)";
                    SqlParameter[] fineParameters =
                    {
                        new SqlParameter("@UserID", userId),
                        new SqlParameter("@FineAmount", fineAmount)
                    };
                    dbHelper.ExecuteNonQuery(insertFineQuery, fineParameters);
                }
            }

            // Display success message
            lblMessage.Text = "Book returned successfully!";
            lblMessage.CssClass = "text-success";
            lblMessage.Visible = true;

            // Reload the loans list
            LoadLoans();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "An error occurred while returning the book.";
            lblMessage.Visible = true;
        }
    }
}
