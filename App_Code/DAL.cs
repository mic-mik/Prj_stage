using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
	public DAL()
	{
    }
    public static SqlConnection GetConnection()
    {
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        return conn;
    }
    public static string GetConnectionString()
    {
        string connection = System.Configuration.ConfigurationManager.
                ConnectionStrings["constr"].ConnectionString;
        return connection;
    }
  
  
  


}