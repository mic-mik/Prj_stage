using System;
using System.Data;
using System.Data.SqlClient;

public partial class CollectPayment : System.Web.UI.Page
{
    private string connectionString = DAL.GetConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int fineId = Convert.ToInt32(Request.QueryString["FineID"]);
            LoadFineDetails(fineId);
        }
    }

    private void LoadFineDetails(int fineId)
    {
        string query = "SELECT Fines.FineAmount, Users.UserID, Users.Username " +
                       "FROM Fines " +
                       "INNER JOIN Users ON Fines.UserID = Users.UserID " +
                       "WHERE Fines.FineID = @FineID";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@FineID", SqlDbType.Int).Value = fineId;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Store fine amount and user ID in ViewState for later use
                        ViewState["fineAmount"] = reader.GetDecimal(reader.GetOrdinal("FineAmount"));
                        ViewState["UserID"] = reader.GetInt32(reader.GetOrdinal("UserID"));
                        ViewState["FineID"] = fineId;

                        lblFineDetails.Text = "Member: " + reader["Username"].ToString() +
                                              "<br /> Fine Amount: " + ViewState["fineAmount"].ToString();
                    }
                }
            }
        }
    }

    protected void btnProcessPayment_Click(object sender, EventArgs e)
    {
        string paymentMethod = ddlPaymentMethod.SelectedValue;
        if (ViewState["FineID"] != null && ViewState["UserID"] != null && ViewState["fineAmount"] != null)
        {
            RecordPayment((int)ViewState["FineID"], (int)ViewState["UserID"], (decimal)ViewState["fineAmount"], paymentMethod);
            lblMessage.Text = "Payment processed successfully!";
        }
        else
        {
            lblMessage.Text = "Unable to process payment. Please try again.";
        }
    }

    private void RecordPayment(int fineId, int userId, decimal amount, string method)
    {
        string paymentQuery = "INSERT INTO Payments (UserID, PaymentDate, Amount, PaymentMethod) " +
                              "VALUES (@UserID, GETDATE(), @Amount, @PaymentMethod)";

        string updateFineQuery = "UPDATE Fines SET IsPaid = 1 WHERE FineID = @FineID";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            using (SqlCommand cmdPayment = new SqlCommand(paymentQuery, conn))
            {
                cmdPayment.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                cmdPayment.Parameters.Add("@Amount", SqlDbType.Decimal).Value = amount;
                cmdPayment.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar).Value = method;
                cmdPayment.ExecuteNonQuery();
            }

            using (SqlCommand cmdFineUpdate = new SqlCommand(updateFineQuery, conn))
            {
                cmdFineUpdate.Parameters.Add("@FineID", SqlDbType.Int).Value = fineId;
                cmdFineUpdate.ExecuteNonQuery();
            }
        }
    }
}
