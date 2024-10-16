using System;
using System.Data;
using System.Data.SqlClient;

public partial class PayFineSuccess : System.Web.UI.Page
{
    private readonly string connectionString = DAL.GetConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["fineId"] != null)
            {
                int fineId = Convert.ToInt32(Request.QueryString["fineId"]);
                MarkFineAsPaid(fineId);
            }
        }
    }

    private void MarkFineAsPaid(int fineId)
    {
        decimal fineAmount = 0;

        // Retrieve the fine amount
        string query = "SELECT FineAmount FROM Fines WHERE FineID = @FineID";

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@FineID", fineId);
            conn.Open();

            // Execute the query and retrieve the fine amount
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    fineAmount = reader.GetDecimal(0);
                }
            }
        }

        if (fineAmount > 0)
        {
            // Mark the fine as paid
            string updateFineQuery = "UPDATE Fines SET IsPaid = 1 WHERE FineID = @FineID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(updateFineQuery, conn))
            {
                cmd.Parameters.AddWithValue("@FineID", fineId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Record the payment
            string insertPaymentQuery = "INSERT INTO Payments (UserID, PaymentDate, Amount, PaymentMethod) VALUES (@UserID, @PaymentDate, @Amount, @PaymentMethod)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(insertPaymentQuery, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserID"]));
                cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Amount", fineAmount);
                cmd.Parameters.AddWithValue("@PaymentMethod", "Credit Card");
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Payment successful! Thank you for your payment.";
        }
        else
        {
            lblMessage.Text = "Error: Fine not found or already paid.";
        }
    }
}
