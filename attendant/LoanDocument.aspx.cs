using System;
using System.Data.SqlClient;

public partial class LoanDocument : System.Web.UI.Page
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
        string query = "SELECT UserID, Username FROM Users WHERE RoleID = 3"; // Fetch only members
        ddlMember.DataSource = dbHelper.ExecuteQuery(query);
        ddlMember.DataTextField = "Username";
        ddlMember.DataValueField = "UserID";
        ddlMember.DataBind();
    }

    private void LoadDocuments()
    {
        string query = "SELECT DocumentID, Title FROM Documents WHERE Status = 'Available'";
        ddlDocument.DataSource = dbHelper.ExecuteQuery(query);
        ddlDocument.DataTextField = "Title";
        ddlDocument.DataValueField = "DocumentID";
        ddlDocument.DataBind();
    }

    protected void btnLoan_Click(object sender, EventArgs e)
    {
        int memberId = Convert.ToInt32(ddlMember.SelectedValue);
        int documentId = Convert.ToInt32(ddlDocument.SelectedValue);
        DateTime dueDate = Convert.ToDateTime(txtDueDate.Text);

        string query = "INSERT INTO Loans (UserID, DocumentID, LoanDate, DueDate, IsReturned) VALUES (@UserID, @DocumentID, GETDATE(), @DueDate, 0);" +
            "UPDATE Documents SET Status = 'Borrowed' WHERE DocumentID = @DocumentID;";
        SqlParameter[] parameters = {
            new SqlParameter("@UserID", memberId),
            new SqlParameter("@DocumentID", documentId),
            new SqlParameter("@DueDate", dueDate)
        };

        dbHelper.ExecuteNonQuery(query, parameters);
        LoadMembers();
        LoadDocuments();
        lblMessage.Text = "Changes done!";
    }
}
