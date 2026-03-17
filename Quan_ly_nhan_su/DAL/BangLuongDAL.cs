using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.DAL
{
    public class BangLuongDAL
    {
        private string connectionString = @"Server=LAPTOP-Q7PBMSMT\PHAMTUAN;Database=QuanLyNhanSu_TienLuong;Trusted_Connection=True;TrustServerCertificate=True;";

        public List<BangLuongDTO> GetDanhSachBangLuong(string tuKhoa = "", string thangNam = "")
        {
            List<BangLuongDTO> dsNhanVien = new List<BangLuongDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Bọc ngoặc tròn () ở điều kiện OR để logic không bị sai khi kết hợp với AND
                string query = @"SELECT n.MaNV, n.TenNV, n.LuongCung, c.SoNgayLam
                        FROM NhanVien n
                        JOIN ChamCong c ON n.MaNV = c.MaNV
                        WHERE (n.TenNV LIKE @TuKhoa OR n.MaNV LIKE @TuKhoa)";

                // Nếu có chọn tháng năm thì mới nối thêm vào truy vấn
                if (!string.IsNullOrEmpty(thangNam))
                {
                    query += " AND c.ThangNam = @ThangNam";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");

                    // Truyền giá trị tháng năm xuống SQL
                    if (!string.IsNullOrEmpty(thangNam))
                    {
                        cmd.Parameters.AddWithValue("@ThangNam", thangNam);
                    }

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
            return dsNhanVien;
        }

    }
}