using System;
using System.Data.SqlClient;

public partial class RegisterMember : System.Web.UI.Page
{
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text); // Secure password hashing
        int roleId = 3; // Role ID for 'Member'

        // Check if username is unique
        string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
        SqlParameter[] checkParams = { new SqlParameter("@Username", username) };
        int count = (int)dbHelper.ExecuteScalar(checkQuery, checkParams);

        if (count > 0)
        {
            lblMessage.Text = "Username already exists. Please choose a different username.";
        }
        else
        {
            string query = "INSERT INTO Users (Username, PasswordHash, RoleID, IsActive) VALUES (@Username, @PasswordHash, @RoleID, 1)";
            SqlParameter[] parameters = {
                new SqlParameter("@Username", username),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@RoleID", roleId)
            };

            dbHelper.ExecuteNonQuery(query, parameters);
            Response.Write("<script>alert('Member registered successfully');</script>");
        }
    }
}
