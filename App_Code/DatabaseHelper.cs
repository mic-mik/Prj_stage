using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class DatabaseHelper
{
    private readonly string connectionString;

    public DatabaseHelper()
    {
        connectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }

    public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
    public object ExecuteScalar(string query, SqlParameter[] parameters)
    {
        object result = null;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                result = command.ExecuteScalar();
            }
        }

        return result;
    }
    public SqlDataReader ExecuteReader(string query)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(query, connection);
        connection.Open();

        return command.ExecuteReader(CommandBehavior.CloseConnection);
    }
}
