using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Xml.Linq;

public partial class client : System.Web.UI.MasterPage
{
    private readonly string connectionString = DAL.GetConnectionString();
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["UserID"] == null)
        //{
        //    Response.Redirect("~/home.aspx"); // Redirect to login if not authenticated
        //}
        //int roleId = (int)Session["RoleID"];
        //if (roleId != 3) // Check if the user is an member
        //{
        //    Response.Redirect("~/home.aspx"); // Redirect to login page
        //}
        // lblUsername.Text = Session["Username"].ToString();
        if (!IsPostBack)
        {
            if (Session["UserID"] != null)
            {
                // User is logged in
                //lblUsername.Text = Session["Username"].ToString();
                pnlAuth.Visible = false; // Hide login and signup buttons
                pnlLogout.Visible = true; // Show logout button
                dvAuthLink.Visible = true; 
            }
            else
            {
                // User is not logged in
                pnlAuth.Visible = true; // Show login and signup buttons
                pnlLogout.Visible = false; // Hide logout button
                dvAuthLink.Visible = false; 
            }
        }

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/?searchTerm="+Server.UrlEncode(txtSearch.Text));
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtLoginEmail.Text.Trim();
        string password = txtLoginPassword.Text.Trim();

        // Ensure username and password are not empty
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Response.Write("<script>alert('Username and password are required.');</script>");
            return;
        }

        string query = "SELECT UserID, PasswordHash, RoleID FROM Users WHERE Username = @Username AND IsActive = 1";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Checks if a record was found
                        {
                            string storedHash = reader["PasswordHash"].ToString();
                            if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                            {
                                int userId = (int)reader["UserID"];
                                int roleId = (int)reader["RoleID"];

                                // Set session variables
                                Session["UserID"] = userId;
                                Session["RoleID"] = roleId;
                                Session["Username"] = username;

                                // Redirect to homepage or dashboard based on role
                                RedirectBasedOnRole(roleId);
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid password. Please try again.');</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('User not found or inactive. Please check your username.');</script>");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('An error occurred during login. Please try again later.');</script>");
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (txtRegisterPassword.Text != txtRegisterConfirmPassword.Text)
        {
            Response.Write("<script>alert('Password do not match.');</script>");
            return;
        }
        string username = txtRegisterEmail.Text;
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(txtRegisterPassword.Text); // Secure password hashing
        int roleId = 3; // Role ID for 'Member'

        // Check if username is unique
        string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
        SqlParameter[] checkParams = { new SqlParameter("@Username", username) };
        int count = (int)dbHelper.ExecuteScalar(checkQuery, checkParams);

        if (count > 0)
        {
            Response.Write("<script>alert('Username already exists. Please choose a different username.');</script>");
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
            //Response.Redirect("Home.aspx");
        }
    }

    private void RedirectBasedOnRole(int roleId)
    {
        switch (roleId)
        {
            case 1: // Admin
                Response.Redirect("administrator/ManageUsers.aspx");
                break;
            case 2: // Librarian
                Response.Redirect("librarian/ManageBooks.aspx");
                break;
            case 3: // Member
                Response.Redirect("~/");
                break;
            case 4: // Attendant
                Response.Redirect("attendant/RegisterMember.aspx");
                break;
            case 5: // manager
                Response.Redirect("manager/BorrowedBooksReport.aspx");
                break;
            case 6: // Accounting
                Response.Redirect("accounting/AccountingDashboard.aspx");
                break;
            default:
                Response.Write("<script>alert('Unknown role. Please contact the administrator.');</script>");
                break;
        }
    }
    protected void LinqLogout_Click(object sender, EventArgs e)
    {

        if (Session["UserID"] != null)
        {
            Session["UserID"] = null;
            Session["RoleID"] = null;
            Response.Redirect("~/");
        }
    }

}
