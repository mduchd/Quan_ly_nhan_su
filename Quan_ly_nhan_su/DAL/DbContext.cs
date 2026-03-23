using Microsoft.Data.SqlClient;

namespace Quan_ly_nhan_su.DAL
{
    internal class DbContext
    {
        public static string ConnectionString { get; } = @"Server=.;Database=QL_Nhansu;Integrated Security=true;TrustServerCertificate=True";

        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
