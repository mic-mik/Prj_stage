using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Xml.Linq;

public partial class Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string searchTerm = Request.QueryString["searchTerm"];

            if (!string.IsNullOrEmpty(searchTerm))
            {
                LoadDocuments(searchTerm);
            }
            else
            {
                LoadDocuments(null);
            }
        }
    }

    private void LoadDocuments(string searchTerm = null)
    {
        string connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connString))
        {
            string query = "SELECT TOP (1000) [DocumentID], [Title], [Author], [ISBN], [DocumentType], [Status], [CreatedAt], [Publisher], [PhotoPath] FROM [LibraryDB].[dbo].[Documents]";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " WHERE [Title] LIKE @searchTerm OR [Author] LIKE @searchTerm";
            }

            SqlCommand cmd = new SqlCommand(query, conn);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
            }

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

}

