using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; // Đảm bảo dùng đúng thư viện này
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.DAL
{
    public class BangLuongDAL
    {
        // SỬA TÊN SERVER THÀNH DẤU CHẤM (.)
        private string connectionString = @"Server=.;Database=QL_Nhansu;Trusted_Connection=True;TrustServerCertificate=True;";

        public List<BangLuongDTO> GetDanhSachBangLuong(string tuKhoa = "")
        {
            List<BangLuongDTO> dsNhanVien = new List<BangLuongDTO>();

            // Dùng try-catch để nếu lỗi nó báo rõ cho mình biết
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT n.MaNV, n.TenNV, n.LuongCung, c.SoNgayLam
                                    FROM NhanVien n
                                    JOIN ChamCong c ON n.MaNV = c.MaNV
                                    WHERE n.TenNV LIKE @TuKhoa OR n.MaNV LIKE @TuKhoa";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BangLuongDTO nv = new BangLuongDTO();
                                nv.MaNV = reader["MaNV"].ToString();
                                nv.TenNV = reader["TenNV"].ToString();
                                nv.LuongCung = Convert.ToDecimal(reader["LuongCung"]);
                                nv.SoNgayLam = Convert.ToInt32(reader["SoNgayLam"]);
                                dsNhanVien.Add(nv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Nếu vẫn lỗi, nó sẽ hiện thông báo chi tiết ở đây
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối DB: " + ex.Message);
            }

            return dsNhanVien;
        }
    }
}