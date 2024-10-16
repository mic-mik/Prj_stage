using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class BorrowedBooksReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBorrowedBooks();
        }
    }

    private void LoadBorrowedBooks()
    {
        string query = "SELECT Users.Username, Documents.Title, Loans.LoanDate, Loans.DueDate " +
                       "FROM Loans " +
                       "INNER JOIN Users ON Loans.UserID = Users.UserID " +
                       "INNER JOIN Documents ON Loans.DocumentID = Documents.DocumentID " +
                       "WHERE Loans.IsReturned = 0";  // Fetch only borrowed books not yet returned

        using (SqlConnection con = new SqlConnection(DAL.GetConnectionString()))
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvBorrowedBooks.DataSource = dt;
            gvBorrowedBooks.DataBind();
        }
    }
}
