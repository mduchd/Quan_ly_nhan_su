using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Quan_ly_nhan_su.DAL
{
    internal class DbContext
    {
        // Sử dụng Server=. giúp kết nối đến SQL Server mặc định trên bất kỳ máy nào mà không cần tên máy cụ thể
        // Thêm "Integrated Security=True" và đảm bảo tên Database chính xác 100%
        public static string ConnectionString = @"Server=.;Database=QL_Nhansu;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}