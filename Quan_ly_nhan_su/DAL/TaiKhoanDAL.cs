<<<<<<< HEAD
﻿using System;
=======
using System;
using System.Collections.Generic;
using System.Text;
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
using Microsoft.Data.SqlClient;

namespace Quan_ly_nhan_su.DAL
{
    internal class TaiKhoanDAL
    {
        public bool KiemTraDangNhap(string taikhoan, string matkhau)
        {
<<<<<<< HEAD
            using SqlConnection conn = DbContext.GetSqlConnection();
            conn.Open();

            const string sql = "SELECT Quyen From TAI_KHOAN WHERE TenDangNhap = @taikhoan AND MatKhau = @matkhau";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@taikhoan", taikhoan);
            cmd.Parameters.AddWithValue("@matkhau", matkhau);

            object result = cmd.ExecuteScalar();
            return result?.ToString();
=======
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
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
        }
    }
}




