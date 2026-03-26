using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using Quan_ly_nhan_su.DTO;
using System.Drawing.Text;



namespace Quan_ly_nhan_su.DAL
{
    internal class ChamCongDAL
    {



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

            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    DateTime now = DateTime.Now;
                    string query = "insert into ChiTietChamCong(MaNV, NgayChamCong, GioVao) values (@MaNV, @NgayChamCong, @GioVao)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@NgayChamCong", now.Date);
                        cmd.Parameters.AddWithValue("@GioVao", now.TimeOfDay);
                        cmd.ExecuteNonQuery();
                    }
                    string queryUpdate = "Update NhanVien set Trangthai= @Trangthai where MaNV = @MaNV";
                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@Trangthai", trangThaiMoi);
                        cmdUpdate.Parameters.AddWithValue("@MaNV", maNV);
                        if (cmdUpdate.ExecuteNonQuery() > 0)
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
        public bool CapNhatTrangThaiCheckOut(string maNV, double tongGio)
        {
            bool ketQua = false;
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    DateTime now = DateTime.Now;
                    string queryUpdateHistory = @"Update ChiTietChamCong set  GioRa = @GioRa,  TongGio = @TongGio Where MaNV = @MaNV And NgayChamCong = @Ngay and GioRa Is null";
                    using (SqlCommand cmd1 = new SqlCommand(queryUpdateHistory, conn))
                    {
                        cmd1.Parameters.AddWithValue("@GioRa", DateTime.Now.TimeOfDay);
                        cmd1.Parameters.AddWithValue("@MaNV", maNV);
                        cmd1.Parameters.AddWithValue("@TongGio", tongGio);
                        cmd1.Parameters.AddWithValue("@Ngay", now.Date);
                        cmd1.ExecuteNonQuery();

                    }
                    string queryUpdateStatus = "Update NhanVien Set Trangthai = 0 where MaNV = @MaNV";
                    using (SqlCommand cmd2 = new SqlCommand(queryUpdateStatus, conn))
                    {
                        cmd2.Parameters.AddWithValue("@MaNV", maNV);
                        if (cmd2.ExecuteNonQuery() > 0)
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
                    string query = "Select Trangthai from NhanVien where MaNV = @MaNV";
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
       
        public int KiemTraTT(string maNV)
        {
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    // Kiem tra xem nhan vien ton tai khong
                    string queryUserCheck = "Select count(*) from NhanVien where MaNV = @MaNV";
                    using (SqlCommand cmdCheck = new SqlCommand(queryUserCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaNV", maNV);
                        int count = (int)cmdCheck.ExecuteScalar();
                        if (count == 0) return -1; // Neu - 1 thi khong tim thay nhan vien 
                    }
                    string query = "Select GioVao from ChiTietChamCong where MaNV = @MaNV and NgayChamCong = CAST(GetDate() as Date) and GioRa is null";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        object result = cmd.ExecuteScalar();
                        // Neu cos ket qua thi la dang check in
                        if(result != null && result != DBNull.Value)
                        {
                            return 1;
                        }
                    }
                    return 0; // Neu cac buoc tren ma chua return 1 thi la chua check in
                }
                catch(Exception ex)
                {
                    throw new Exception("Chi tiet loi ow ham kiem tra trang thai : " + ex.Message);
                }
            }
        }
        public ChamCongDTO LayThongTinChamCong(string maNV)
        {
            ChamCongDTO nv = null;
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    string query = "Select MaNV, GioVao from ChiTietChamCong where MaNV = @MaNV AND NgayChamCong = @Ngay AND GioRa IS NULL";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ngay", DateTime.Now.Date);
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

        public (string tenNV, string SDT, string DiaChi) LayThongTinNhanVien(string maNhanVien)
        {
            string query = "Select TenNV, SoDienThoai, DiaChi from NhanVien where MaNV = @MaNV";
            string tenNV = "";
            string SDT = "";
            string DiaChi = "";


            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maNhanVien);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                tenNV = reader["TenNV"].ToString();
                                SDT = reader["SoDienThoai"].ToString();
                                DiaChi = reader["DiaChi"].ToString();


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lỗi lấy thông tin nhân viên : " + ex.Message);
                    }
                }
                return (tenNV, SDT, DiaChi);
            }
        }
        public DataTable LayLichSuChamCong(string maNV)
        {
            DataTable dt = new DataTable();
            string query = "Select NgayChamCong, GioVao, GioRa from ChiTietChamCong where MaNV = @MaNV order by NgayChamCong DESC, GioVao DESC";
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lỗi lấy lịch sử : " + ex.Message);
                    }
                }
            }
            return dt;
        }
    }
}

