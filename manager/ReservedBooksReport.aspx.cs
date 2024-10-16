using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ReservedBooksReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadReservedBooks();
        }
    }

    private void LoadReservedBooks()
    {
        string query = "SELECT Users.Username, Documents.Title, Reservations.ReservationDate, Reservations.ExpiryDate " +
                       "FROM Reservations " +
                       "INNER JOIN Users ON Reservations.UserID = Users.UserID " +
                       "INNER JOIN Documents ON Reservations.DocumentID = Documents.DocumentID " +
                       "WHERE Reservations.IsActive = 1";  // Fetch only active reservations

        using (SqlConnection con = new SqlConnection(DAL.GetConnectionString()))
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvReservedBooks.DataSource = dt;
            gvReservedBooks.DataBind();
        }
    }
}
