using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class EditUser : Page
{
    private readonly string connectionString = DAL.GetConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadRoles();
            LoadUserData();
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

    private void LoadUserData()
    {
        int userId = Convert.ToInt32(Request.QueryString["UserID"]);
        string query = "SELECT Username, RoleID FROM Users WHERE UserID = @UserID";
        SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dtUser = new DataTable();
                        adapter.Fill(dtUser);

                        if (dtUser.Rows.Count == 1)
                        {
                            txtUsername.Text = dtUser.Rows[0]["Username"].ToString();
                            ddlRoles.SelectedValue = dtUser.Rows[0]["RoleID"].ToString();
                        }
                        else
                        {
                            Response.Redirect("ManageUsers.aspx");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.QueryString["UserID"]);
        string username = txtUsername.Text;

        // Check if the username already exists (excluding the current user)
        if (IsUsernameTaken(username, userId))
        {
            lblMessage.Text = "Username already exists. Please choose a different username.";
            return;
        }

        string passwordHash = string.IsNullOrWhiteSpace(txtPassword.Text) ? null : BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);
        int roleId = Convert.ToInt32(ddlRoles.SelectedValue);

        string query = "UPDATE Users SET Username = @Username, RoleID = @RoleID" +
                       (passwordHash != null ? ", PasswordHash = @PasswordHash" : "") +
                       " WHERE UserID = @UserID";

        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@Username", username),
            new SqlParameter("@RoleID", roleId),
            new SqlParameter("@UserID", userId)
        };

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (passwordHash != null)
                    {
                        Array.Resize(ref parameters, parameters.Length + 1);
                        parameters[parameters.Length - 1] = new SqlParameter("@PasswordHash", passwordHash);
                    }

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

    private bool IsUsernameTaken(string username, int currentUserId)
    {
        string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND UserID != @CurrentUserId";
        SqlParameter[] parameters =
        {
            new SqlParameter("@Username", username),
            new SqlParameter("@CurrentUserId", currentUserId)
        };

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageUsers.aspx");
    }
}
