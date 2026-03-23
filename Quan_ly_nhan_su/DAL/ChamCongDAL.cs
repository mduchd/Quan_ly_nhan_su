using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using Quan_ly_nhan_su.DTO;
using System.Drawing.Text;

// truy xất DB 
namespace Quan_ly_nhan_su.DAL
{
    internal class ChamCongDAL
    {

        // Thay dòng cũ bằng dòng này:

        public DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection conn = DbContext.GetSqlConnection())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                    // conn.Close(); // Dùng 'using' thì không cần Close thủ công, nó tự đóng rồi
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi để biết tại sao nút không chạy
                throw new Exception("Lỗi thực thi truy vấn: " + ex.Message);
            }
            return data;
        }


        public bool CapNhatTrangThaiCheckIn(string maNV, int trangThaiMoi)
        {
            bool ketqua = false;

            using (SqlConnection conn = DbContext.GetSqlConnection()) {
                try
                {
                    conn.Open();
                    string query = "Update NhanVienChamCong Set Trangthai = @Trangthai, NgayChamCong = @NgayChamCong, GioVao = @GioVao where MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(query, conn)) {
                        DateTime now = DateTime.Now;
                        cmd.Parameters.AddWithValue("@Trangthai", trangThaiMoi);
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@NgayChamCong", now.Date);
                        cmd.Parameters.AddWithValue("@GioVao", now.TimeOfDay);

                        int soDongCapNhat = cmd.ExecuteNonQuery();
                        if (soDongCapNhat > 0)
                        {
                            ketqua = true;
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Chi tiết lỗi : " + ex.Message);
                }
            }
            return ketqua;
        }
        public bool CapNhatTrangThaiCheckOut(string maNV)
        {
            bool ketQua = false;
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"Update NhanVienChamCong set Trangthai = 0, GioRa = @GioRa Where MaNV = @MaNV And Trangthai = 1";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@GioRa", DateTime.Now.TimeOfDay);
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        int soDongCapNhat = cmd.ExecuteNonQuery();
                        if (soDongCapNhat > 0)
                        {
                            ketQua = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Chi tiết lỗi : " + ex.Message);
                }
            }
            return ketQua;
        }
        public int KiemTraTrangThai(string maNV)
        {
            int trangthai = -1;
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    string query = "Select Trangthai from NhanVienChamCong where MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            trangthai = Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi kiểm tra trangh thái : " + ex.Message);
                }
            }
            return trangthai;

        }
      
    public ChamCongDTO LayThongTinChamCong(string maNV)
        {
            ChamCongDTO nv = null;
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    string query = "Select MaNV, GioVao from NhanVienChamCong where MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nv = new ChamCongDTO();
                                nv.MaNV = reader["MaNV"].ToString();
                                if (reader["GioVao"] != DBNull.Value)
                                {
                                    nv.GioVao = (TimeSpan)reader["GioVao"];
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Chi tiết lỗi : " + ex.Message);
                }
            }
            return nv;
        }
    }
}

