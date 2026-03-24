using System;
using Microsoft.Data.SqlClient;

namespace Quan_ly_nhan_su.DAL
{
    internal class TaiKhoanDAL
    {
        public string KiemTraDangNhap(string taikhoan, string matkhau)
        {
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();

                    const string sql = "SELECT Quyen FROM TAI_KHOAN WHERE TenDangNhap = @taikhoan AND MatKhau = @matkhau";
                    using SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@taikhoan", taikhoan);
                    cmd.Parameters.AddWithValue("@matkhau", matkhau);

                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi CSDL khi kiểm tra đăng nhập: " + ex.Message, ex);
                }
            }
        }
    }
}
