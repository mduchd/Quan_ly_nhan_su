using System;
using Microsoft.Data.SqlClient;

namespace Quan_ly_nhan_su.DAL
{
    internal class TaiKhoanDAL
    {
        public bool KiemTraDangNhap(string taikhoan, string matkhau)
        {
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    const string sql = "SELECT COUNT(*) FROM TAI_KHOAN WHERE TenDangNhap = @taikhoan AND MatKhau = @matkhau";
                    using SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@taikhoan", taikhoan);
                    cmd.Parameters.AddWithValue("@matkhau", matkhau);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi CSDL khi kiểm tra đăng nhập: " + ex.Message, ex);
                }
            }
        }

        public bool DoiMatKhau(string taikhoan, string matkhauCu, string matkhauMoi)
        {
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE TAI_KHOAN SET MatKhau = @MatKhauMoi WHERE TenDangNhap = @TaiKhoan AND MatKhau = @MatKhauCu";
                    using SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TaiKhoan", taikhoan);
                    cmd.Parameters.AddWithValue("@MatKhauCu", matkhauCu);
                    cmd.Parameters.AddWithValue("@MatKhauMoi", matkhauMoi);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi CSDL khi đổi mật khẩu: " + ex.Message);
                }
            }
        }
    }
}
