using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Quan_ly_nhan_su.DAL
{
    internal class TaiKhoanDAL
    {
        public string KiemTraDangNhap(string taikhoan, string matkhau)
        {
            string quyen = null;
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT Quyen From TAI_KHOAN WHERE TenDangNhap = @taikhoan AND MatKhau = @matkhau";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@taikhoan", taikhoan);
                    cmd.Parameters.AddWithValue("@matkhau", matkhau);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        quyen = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Loi CSDL: " + ex.Message);
                }
            }

            return quyen;
        }
    }
}




