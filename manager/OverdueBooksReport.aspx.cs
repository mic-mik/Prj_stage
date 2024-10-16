using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class OverdueBooksReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadOverdueBooks();
        }
    }

    private void LoadOverdueBooks()
    {
        string query = "SELECT Users.Username, Documents.Title, Loans.LoanDate, Loans.DueDate " +
                       "FROM Loans " +
                       "INNER JOIN Users ON Loans.UserID = Users.UserID " +
                       "INNER JOIN Documents ON Loans.DocumentID = Documents.DocumentID " +
                       "WHERE Loans.DueDate < GETDATE() AND Loans.IsReturned = 0";  // Fetch only overdue books

        using (SqlConnection con = new SqlConnection(DAL.GetConnectionString()))
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvOverdueBooks.DataSource = dt;
            gvOverdueBooks.DataBind();
        }
    }
}
