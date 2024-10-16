using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ReturnedBooksReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadReturnedBooks();
        }
    }

    private void LoadReturnedBooks()
    {
        string query = "SELECT Users.Username, Documents.Title, Loans.LoanDate, Loans.ReturnDate " +
                "FROM Loans " +
                "INNER JOIN Users ON Loans.UserID = Users.UserID " +
                "INNER JOIN Documents ON Loans.DocumentID = Documents.DocumentID where Loans.IsReturned=1";


        using (SqlConnection con = new SqlConnection(DAL.GetConnectionString()))
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvReturnedBooks.DataSource = dt;
            gvReturnedBooks.DataBind();
        }
    }
}
