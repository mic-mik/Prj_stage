using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BorrowBook : Page
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
        string connString = DAL.GetConnectionString();
        using (SqlConnection conn = new SqlConnection(connString))
        {
            string query = "SELECT TOP (1000) [DocumentID], [Title], [Author], [ISBN], [DocumentType], [Status], [CreatedAt], [Publisher], [PhotoPath] FROM [LibraryDB].[dbo].[Documents] where status='available'";
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();

            // Convert PhotoPath from ~ to ..
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["PhotoPath"] != DBNull.Value)
                {
                    row["PhotoPath"] = row["PhotoPath"].ToString().Replace("~", "..");
                }
            }

            // Bind the ListView to the document list
            lvDocuments.DataSource = dataTable;
            lvDocuments.DataBind();
        }
    }

    protected void btnBorrow_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        int documentId = Convert.ToInt32(btn.CommandArgument);
        int userId = Convert.ToInt32(Session["UserID"]);

        
        DateTime dueDate = DateTime.Now.AddDays(1); 

        string query = "INSERT INTO Loans (UserID, DocumentID, LoanDate, DueDate) VALUES (@UserID, @DocumentID, @LoanDate, @DueDate)";
        SqlParameter[] parameters =
        {
            new SqlParameter("@UserID", userId),
            new SqlParameter("@DocumentID", documentId),
            new SqlParameter("@LoanDate", DateTime.Now),
            new SqlParameter("@DueDate", dueDate)
        };

        dbHelper.ExecuteNonQuery(query, parameters);

        // Update document status
        string updateDocumentQuery = "UPDATE Documents SET Status = 'Borrowed' WHERE DocumentID = @DocumentID";
        SqlParameter[] updateParams = { new SqlParameter("@DocumentID", documentId) };
        dbHelper.ExecuteNonQuery(updateDocumentQuery, updateParams);

        lblMessage.Text = "Book borrowed successfully!";
        lblMessage.Visible = true;
        LoadDocuments(); // Refresh the ListView to reflect the changes
    }
}
