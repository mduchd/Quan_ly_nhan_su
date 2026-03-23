using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Quan_ly_nhan_su.DAL
{
    internal class TaiKhoanDAL
    {
        public bool KiemTraDangNhap(string taikhoan, string matkhau)
        {
            bool hopLe = false;
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT 1 From TaiKhoan WHERE TenDangNhap = @taikhoan AND MatKhau = @matkhau";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@taikhoan", taikhoan);
                    cmd.Parameters.AddWithValue("@matkhau", matkhau);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        hopLe = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Loi CSDL: " + ex.Message);
                }
            }

            return hopLe;
        }
    }
}




