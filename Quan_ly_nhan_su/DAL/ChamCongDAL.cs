using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Quan_ly_nhan_su.DTO;
using System.Drawing.Text;

// truy xất DB 
namespace Quan_ly_nhan_su.DAL
{
    internal class ChamCongDAL
    {
        private string connectString = @"Data Source=NgocDuy; Initial Catalog=QL_Nhansu; Integrated Security= true";
        public ChamCongDAL() { }

        public bool CapNhatTrangThaiCheckIn(string maNV, int trangThaiMoi)
        {
            bool ketqua = false;

            using (SqlConnection conn = new SqlConnection(connectString)) {
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
                    MessageBox.Show("Chi tiết lỗi : " + ex.Message, "Lỗi");
                }
            }
            return ketqua;
        }
        public bool CapNhatTrangThaiCheckOut(string maNV)
        {
            bool ketQua = false;
            using (SqlConnection conn = new SqlConnection(connectString))
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
                    MessageBox.Show("Chi tiết lỗi : " + ex.Message, "Lỗi");
                }
            }
            return ketQua;
        }
        public int KiemTraTrangThai(string maNV)
        {
            int trangthai = -1;
            using (SqlConnection conn = new SqlConnection(connectString))
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
            using (SqlConnection conn = new SqlConnection(connectString))
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
                    MessageBox.Show("Chi tiết lỗi : " + ex.Message);
                }
            }
            return nv;
        }
    }
}

