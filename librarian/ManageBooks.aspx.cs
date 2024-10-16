using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class ManageBooks : System.Web.UI.Page
{
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBooks();
        }
    }

    private void LoadBooks()
    {
        string query = "SELECT DocumentID, Title, Author, ISBN, DocumentType, PhotoPath FROM Documents";
        DataTable dtBooks = dbHelper.ExecuteQuery(query);

        // Update PhotoPath to replace "~" with ".." 
        foreach (DataRow row in dtBooks.Rows)
        {
            if (row["PhotoPath"] != DBNull.Value)
            {
                row["PhotoPath"] = row["PhotoPath"].ToString().Replace("~", "..");
            }
        }

        lvBooks.DataSource = dtBooks;
        lvBooks.DataBind();
    }


    protected void btnAddBook_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddBook.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        // Find the button that raised the event
        Button btnEdit = (Button)sender;
        int documentId = Convert.ToInt32(btnEdit.CommandArgument);
        Response.Redirect("EditBook.aspx?DocumentID=" + documentId);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        // Find the button that raised the event
        Button btnDelete = (Button)sender;
        int documentId = Convert.ToInt32(btnDelete.CommandArgument);
        DeleteBook(documentId);
        LoadBooks(); // Reload the ListView after deletion
    }

    private void DeleteBook(int documentId)
    {
        // First, retrieve the photo path of the book to delete the image file from the server
        string selectQuery = "SELECT PhotoPath FROM Documents WHERE DocumentID = @DocumentID";
        SqlParameter[] selectParameters = { new SqlParameter("@DocumentID", documentId) };
        DataTable dtBook = dbHelper.ExecuteQuery(selectQuery, selectParameters);

        if (dtBook.Rows.Count == 1)
        {
            string photoPath = dtBook.Rows[0]["PhotoPath"].ToString();
            if (!string.IsNullOrEmpty(photoPath))
            {
                string fullPhotoPath = Server.MapPath(photoPath);
                if (System.IO.File.Exists(fullPhotoPath))
                {
                    System.IO.File.Delete(fullPhotoPath); // Delete the file from the server
                }
            }
        }

        // Then, delete the book record from the database
        string deleteQuery = "DELETE FROM Documents WHERE DocumentID = @DocumentID";
        SqlParameter[] deleteParameters = { new SqlParameter("@DocumentID", documentId) };
        dbHelper.ExecuteNonQuery(deleteQuery, deleteParameters);
    }

}
