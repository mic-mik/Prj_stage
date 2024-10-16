using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReserveBook : Page
{
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDocuments();
        }
    }

    private void LoadDocuments()
    {
        string connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connString))
        {
            string query = "SELECT DocumentID, Title, Author, ISBN, Publisher, Status, PhotoPath FROM Documents WHERE Status = 'Available'";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["PhotoPath"] != DBNull.Value)
                {
                    row["PhotoPath"] = row["PhotoPath"].ToString().Replace("~", "..");
                }
            }

            lvDocuments.DataSource = dataTable;
            lvDocuments.DataBind();
        }
    }

    protected void btnReserve_Click(object sender, EventArgs e)
    {
        LinkButton btnReserve = (LinkButton)sender;
        int documentId = Convert.ToInt32(btnReserve.CommandArgument);
        int userId = Convert.ToInt32(Session["UserID"]);
        DateTime expiryDate = DateTime.Now.AddDays(1);

        // Check if the user already has an active reservation for the same document
        if (CheckExistingReservation(userId, documentId))
        {
            lblMessage.Text = "You already have an active reservation for this document.";
            return;
        }

        string query = "INSERT INTO Reservations (UserID, DocumentID, ReservationDate, ExpiryDate) " +
                       "VALUES (@UserID, @DocumentID, GETDATE(), @ExpiryDate)";
        SqlParameter[] parameters =
        {
            new SqlParameter("@UserID", userId),
            new SqlParameter("@DocumentID", documentId),
            new SqlParameter("@ExpiryDate", expiryDate)
        };
        dbHelper.ExecuteNonQuery(query, parameters);

        // Update the document status
        UpdateDocumentStatus(documentId, "Reserved");

        lblMessage.Text = "Book reserved successfully!";
        LoadDocuments();
    }
    private bool CheckExistingReservation(int userId, int documentId)
    {
        string query = "SELECT COUNT(*) FROM Reservations WHERE UserID = @UserID AND DocumentID = @DocumentID AND IsActive = 1";
        SqlParameter[] parameters =
        {
            new SqlParameter("@UserID", userId),
            new SqlParameter("@DocumentID", documentId)
        };
        int count = (int)dbHelper.ExecuteScalar(query, parameters);
        return count > 0;
    }

    private void UpdateDocumentStatus(int documentId, string status)
    {
        string query = "UPDATE Documents SET Status = @Status WHERE DocumentID = @DocumentID";
        SqlParameter[] parameters =
        {
            new SqlParameter("@Status", status),
            new SqlParameter("@DocumentID", documentId)
        };
        dbHelper.ExecuteNonQuery(query, parameters);
    }
}
