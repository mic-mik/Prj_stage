using System;
using System.Data.SqlClient;

public partial class AddBook : System.Web.UI.Page
{
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnAddBook_Click(object sender, EventArgs e)
    {
        string photoPath = null;

        // Check if a file is uploaded
        if (fuDocumentPhoto.HasFile)
        {
            try
            {
                // Define the folder path to save the photo
                string folderPath = Server.MapPath("~/Uploads/");
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }

                // Save the file to the server
                string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(fuDocumentPhoto.FileName);
                fuDocumentPhoto.SaveAs(System.IO.Path.Combine(folderPath, fileName));
                photoPath = "~/Uploads/" + fileName;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error uploading photo: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        string query = "INSERT INTO Documents (Title, Author, Publisher, ISBN, DocumentType, Barcode, Status, PhotoPath) " +
                       "VALUES (@Title, @Author, @Publisher, @ISBN, @DocumentType, @Barcode, 'Available', @PhotoPath)";

        SqlParameter[] parameters = new SqlParameter[]
        {
        new SqlParameter("@Title", txtTitle.Text),
        new SqlParameter("@Author", txtAuthor.Text),
        new SqlParameter("@Publisher", txtPublisher.Text),
        new SqlParameter("@ISBN", txtISBN.Text),
        new SqlParameter("@DocumentType", txtCategory.Text),
        new SqlParameter("@Barcode", txtBarcode.Text),
        new SqlParameter("@PhotoPath", photoPath ?? (object)DBNull.Value)
        };

        int result = dbHelper.ExecuteNonQuery(query, parameters);

        if (result > 0)
        {
            lblMessage.Text = "Book added successfully!";
        }
        else
        {
            lblMessage.Text = "Error adding book. Please try again.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

}
