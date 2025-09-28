using System.Data;
using System.Data.SqlClient;

namespace ADONet.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Query(string query,params SqlParameterModel[] paramters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            if (paramters != null)
            {
                foreach (var para in paramters)
                {
                    sqlCommand.Parameters.AddWithValue(para.Name, para.Value);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            return dt;
        }        
    }

    public class SqlParameterModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}