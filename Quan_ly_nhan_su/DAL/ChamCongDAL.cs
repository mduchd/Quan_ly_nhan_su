using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
// truy xất DB 
namespace Quan_ly_nhan_su.DAL
{
    internal class ChamCongDAL
    {
        private string connectionString = @"Data Source=NgocDuy; Initial Catalog=QuanLyNhanSu_DB; Integrated Security=True";
        public DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable(); 
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                conn.Close();
            }
            return data;
        }
    }
}
