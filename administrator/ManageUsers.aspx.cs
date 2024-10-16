using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class ManageUsers : System.Web.UI.Page
{
    private readonly string connectionString = DAL.GetConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUsers();
        }
    }

    private void LoadUsers()
    {
        string query = "SELECT Users.UserID, Users.Username, Roles.RoleName " +
                       "FROM Users " +
                       "INNER JOIN Roles ON Users.RoleID = Roles.RoleID";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dtUsers = new DataTable();
                        adapter.Fill(dtUsers);
                        lvUsers.DataSource = dtUsers;
                        lvUsers.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddUser.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton btnEdit = (LinkButton)sender;
        int userId = Convert.ToInt32(btnEdit.CommandArgument);
        Response.Redirect("EditUser.aspx?UserID=" + userId);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btnDelete = (LinkButton)sender;
        int userId = Convert.ToInt32(btnDelete.CommandArgument);
        DeleteUser(userId);
        LoadUsers();
    }

    private void DeleteUser(int userId)
    {
        string query = "DELETE FROM Users WHERE UserID = @UserID";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@UserID", userId));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}
