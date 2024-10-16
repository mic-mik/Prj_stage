using System;
using System.Data;
using System.Data.SqlClient;

public partial class ReserveDocument : System.Web.UI.Page
{
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadMembers();
            LoadDocuments();
        }
    }

    private void LoadMembers()
    {
        string query = "SELECT UserID, Username FROM Users WHERE RoleID = 3"; // RoleID 3 is for members
        DataTable members = dbHelper.ExecuteQuery(query);

        ddlMembers.DataSource = members;
        ddlMembers.DataTextField = "Username";  // Display member's name in dropdown
        ddlMembers.DataValueField = "UserID";   // Use UserID as the value
        ddlMembers.DataBind();
        ddlMembers.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Member", "0"));
    }

    private void LoadDocuments()
    {
        string query = "SELECT DocumentID, Title FROM Documents WHERE Status = 'Available'"; // Show only available documents
        DataTable documents = dbHelper.ExecuteQuery(query);

        ddlDocuments.DataSource = documents;
        ddlDocuments.DataTextField = "Title";      // Display document title in dropdown
        ddlDocuments.DataValueField = "DocumentID"; // Use DocumentID as the value
        ddlDocuments.DataBind();
        ddlDocuments.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Document", "0"));
    }

    protected void btnReserve_Click(object sender, EventArgs e)
    {
        int memberId = Convert.ToInt32(ddlMembers.SelectedValue);
        int documentId = Convert.ToInt32(ddlDocuments.SelectedValue);
        DateTime expiryDate = DateTime.Now.AddDays(7); // Reservation expiry set to 7 days

        if (memberId > 0 && documentId > 0)
        {
            string query = "INSERT INTO Reservations (UserID, DocumentID, ReservationDate, ExpiryDate, IsActive) VALUES (@UserID, @DocumentID, GETDATE(), @ExpiryDate, 1);" +
                "UPDATE Documents SET Status = 'Reserved' WHERE DocumentID = @DocumentID;";
            SqlParameter[] parameters = {
                new SqlParameter("@UserID", memberId),
                new SqlParameter("@DocumentID", documentId),
                new SqlParameter("@ExpiryDate", expiryDate)
            };

            dbHelper.ExecuteNonQuery(query, parameters);
            LoadMembers();
            LoadDocuments();
            lblMessage.Text = "Changes done!";
        }
        else
        {
            Response.Write("<script>alert('Please select a valid Member and Document.');</script>");
        }
    }
}
