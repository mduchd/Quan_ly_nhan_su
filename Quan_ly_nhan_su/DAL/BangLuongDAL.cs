using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.DAL
{
    public class BangLuongDAL
    {
        private string connectionString = @"Server=LAPTOP-Q7PBMSMT\PHAMTUAN;Database=QuanLyNhanSu_TienLuong;Trusted_Connection=True;TrustServerCertificate=True;";

        public List<BangLuongDTO> GetDanhSachBangLuong(string tuKhoa = "")
        {
            List<BangLuongDTO> dsNhanVien = new List<BangLuongDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT n.MaNV, n.TenNV, n.LuongCung, c.SoNgayLam
                                FROM NhanVien n
                                Join ChamCong c ON n.MaNV = c.MaNV
                                Where n.TenNV LIKE @TuKhoa OR n.MaNV LIKE @TuKhoa";
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
            return dsNhanVien;
        }
        
    }
}