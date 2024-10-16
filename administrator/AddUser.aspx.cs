using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class AddUser : Page
{
    private readonly string connectionString = DAL.GetConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadRoles();
        }
    }

    private void LoadRoles()
    {
        string query = "SELECT RoleID, RoleName FROM Roles";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dtRoles = new DataTable();
                        adapter.Fill(dtRoles);
                        ddlRoles.DataSource = dtRoles;
                        ddlRoles.DataTextField = "RoleName";
                        ddlRoles.DataValueField = "RoleID";
                        ddlRoles.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;

        // Check if the username already exists
        if (IsUsernameTaken(username))
        {
            lblMessage.Text = "Username already exists. Please choose a different username.";
            return;
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);
        int roleId = Convert.ToInt32(ddlRoles.SelectedValue);

        string query = "INSERT INTO Users (Username, PasswordHash, RoleID) VALUES (@Username, @PasswordHash, @RoleID)";
        SqlParameter[] parameters =
        {
            new SqlParameter("@Username", username),
            new SqlParameter("@PasswordHash", passwordHash),
            new SqlParameter("@RoleID", roleId)
        };

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            Response.Redirect("ManageUsers.aspx");
        }
        catch (Exception ex)
        {
        }
    }

    private bool IsUsernameTaken(string username)
    {
        string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
        SqlParameter[] parameters = { new SqlParameter("@Username", username) };

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Return true if username exists
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
