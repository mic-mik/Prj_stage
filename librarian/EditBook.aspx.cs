using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class EditBook : System.Web.UI.Page
{
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBookData();
        }
    }

    private void LoadBookData()
    {
        int documentId = Convert.ToInt32(Request.QueryString["DocumentID"]);
        string query = "SELECT Title, Author, ISBN, DocumentType, PhotoPath FROM Documents WHERE DocumentID = @DocumentID";
        SqlParameter[] parameters = { new SqlParameter("@DocumentID", documentId) };
        DataTable dtBook = dbHelper.ExecuteQuery(query, parameters);

        if (dtBook.Rows.Count == 1)
        {
            txtTitle.Text = dtBook.Rows[0]["Title"].ToString();
            txtAuthor.Text = dtBook.Rows[0]["Author"].ToString();
            txtISBN.Text = dtBook.Rows[0]["ISBN"].ToString();
            txtDocumentType.Text = dtBook.Rows[0]["DocumentType"].ToString();

            // Display the existing photo, if available
            string photoPath = dtBook.Rows[0]["PhotoPath"].ToString();
            if (!string.IsNullOrEmpty(photoPath))
            {
                imgDocumentPhoto.ImageUrl = photoPath;
                imgDocumentPhoto.Visible = true;
            }
            else
            {
                imgDocumentPhoto.Visible = false;
            }
        }
        else
        {
            Response.Redirect("ManageBooks.aspx");
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int documentId = Convert.ToInt32(Request.QueryString["DocumentID"]);
        string title = txtTitle.Text;
        string author = txtAuthor.Text;
        string isbn = txtISBN.Text;
        string documentType = txtDocumentType.Text;
        string photoPath = null;

        // Check if a new file is uploaded
        if (fuDocumentPhoto.HasFile)
        {
            try
            {
                // Define the folder path to save the new photo
                string folderPath = Server.MapPath("~/Uploads/");
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }

                // Save the file to the server with a unique name
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

        string query = "UPDATE Documents SET Title = @Title, Author = @Author, ISBN = @ISBN, DocumentType = @DocumentType";

        // Append photo path update if a new photo was uploaded
        if (!string.IsNullOrEmpty(photoPath))
        {
            query += ", PhotoPath = @PhotoPath";
        }

        query += " WHERE DocumentID = @DocumentID";

        var parameters = new List<SqlParameter>
    {
        new SqlParameter("@Title", title),
        new SqlParameter("@Author", author),
        new SqlParameter("@ISBN", isbn),
        new SqlParameter("@DocumentType", documentType),
        new SqlParameter("@DocumentID", documentId)
    };

        // Add photo parameter if a new photo was uploaded
        if (!string.IsNullOrEmpty(photoPath))
        {
            parameters.Add(new SqlParameter("@PhotoPath", photoPath));
        }

        dbHelper.ExecuteNonQuery(query, parameters.ToArray());
        Response.Redirect("ManageBooks.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageBooks.aspx");
    }
}
