using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Quan_ly_nhan_su.DAL
{
    internal class DbContext
    {

        // server = . la localhost
        public static string ConnectionString = @"Server=.;Database=QL_Nhansu; Integrated Security=true;TrustServerCertificate=True";

        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

    }
}
