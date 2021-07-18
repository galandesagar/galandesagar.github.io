using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Threading.Tasks;

/// <summary>
/// SqlDataAccess is being used for accessing MSSQL database quickly and easily. 
/// Requires a connection string that is named MsSql defined on web.config file. This connection string is used as default. 
/// For using different connection strings you should pass the name of the connection string as a parameter with methods.
/// </summary>
public class SqlServerRepository
{
    // Default connection string. a connection named MsSql should be defined in web.config file.
    public const string CONNECTION_STRING_NAME = "MsSql";

    //This returns the connection string  
    private static string _connectionString = string.Empty;
    public static string ConnectionString
    {
        get
        {
            if (_connectionString == string.Empty)
            {
                _connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            }
            return _connectionString;
        }
    }


    /// <summary>
    /// Brings a SqlCommand object to be able to add some parameters in it. After you send this to Execute method.
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public SqlCommand GetCommand(string sql)
    {
        SqlConnection conn = new SqlConnection(ConnectionString);
        SqlCommand sqlCmd = new SqlCommand(sql, conn);
        return sqlCmd;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public DataTable Execute(string sql)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = GetCommand(sql);
        cmd.Connection.Open();
        dt.Load(cmd.ExecuteReader());
        cmd.Connection.Close();
        return dt;
    }

    public  Task<DataTable> ExecuteAsync(string sql)
    {


        return Task<DataTable>.Factory.StartNew(()=>Execute(sql));

    }

    /// <summary>
    /// Datatable Döndür
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public DataTable Execute(SqlCommand command)
    {
        DataTable dt = new DataTable();
        command.Connection.Open();
        //command.ExecuteNonQuery();
        dt.Load(command.ExecuteReader());
        command.Connection.Close();
        return dt;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public int ExecuteNonQuery(string sql)
    {
        SqlCommand cmd = GetCommand(sql);
        cmd.Connection.Open();
        int result = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public int ExecuteNonQuery(SqlCommand command)
    {
        SqlConnection conn = new SqlConnection(ConnectionString);
        command.Connection = conn;
        command.Connection.Open();
        int result = command.ExecuteNonQuery();
        command.Connection.Close();
        return result;
    }
    //public Task<int> ExecuteNonQueryAsync(SqlCommand command)
    //{
    //    SqlConnection conn = new SqlConnection(ConnectionString);
    //    command.Connection = conn;
    //    command.Connection.Open();
        
    //    var result = Task<int>.Factory.StartNew(() => command.ExecuteNonQuery()).Wait(5);
    //    command.Connection.Close();
    //    return Task<1>;
    //}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="spName"></param>
    /// <returns></returns>
    public int ExecuteStoredProcedure(string spName)
    {
        SqlCommand cmd = GetCommand(spName);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        int result = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public DataTable ExecuteStoredProcedure(SqlCommand command)
    {
        var dt = new DataTable();
        command.CommandType = CommandType.StoredProcedure;
        SqlConnection conn = new SqlConnection(ConnectionString);
        command.Connection = conn;
        command.Connection.Open();
        dt.Load(command.ExecuteReader());
        command.Connection.Close();
        return dt;
    }

}