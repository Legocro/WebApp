using System.Data.SqlClient;
using System.Data;

namespace WebApp.Models
{
    public static class DB
    {
        private static string server = "localhost";
        private static string database = "WebApp";
        private static SqlConnection connection = new SqlConnection();
        public static string connectionString = String.Format("Data Source={0};Initial Catalog={1};Integrated Security=true", server, database);

        public static void OpenConnection()
        {

            DB.connection = new SqlConnection(DB.connectionString);
            DB.connection.Open();

        }

        public static void CloseConnection()
        {
            if (DB.connection.State == ConnectionState.Closed)
                return;
            DB.connection.Close();
        }

        public static SqlDataReader GetDataReader(string query) => new SqlCommand(query, DB.connection).ExecuteReader();

        public static object GetScalar(string query) => new SqlCommand(query, DB.connection).ExecuteScalar();

        public static int ExecuteCommand(string command) => new SqlCommand(command, DB.connection).ExecuteNonQuery();
    }
}
