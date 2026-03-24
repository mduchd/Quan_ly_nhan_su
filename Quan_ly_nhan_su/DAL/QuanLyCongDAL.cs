using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Quan_ly_nhan_su.DAL
{
    internal class QuanLyCongDAL
    {
        public DataTable LayDanhSachLichSuCong(string tuKhoa, DateTime? tuNgay, DateTime? denNgay)
        {
            var dt = new DataTable();

            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            try
            {
                // Câu truy vấn SQL THUẦN TÚY nối bảng ChamCong và NhanVien
                string query = @"
                    SELECT 
                        c.MaNV as maNV,
                        n.TenNV as hoTen,
                        c.NgayChamCong as ngay,
                        c.GioVao as gioVao,
                        c.GioRa as gioRa,
                        c.TongGio as TongGio,
                        N'' as trangThai
                    FROM CHiTietChamCong c
                    LEFT JOIN NhanVien n ON c.MaNV = n.MaNV
                    WHERE 1 = 1 ";

                var keyword = (tuKhoa ?? string.Empty).Trim();
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query += " AND (c.MaNV LIKE @TuKhoa OR n.TenNV LIKE @TuKhoa) ";
                }

                if (tuNgay.HasValue && denNgay.HasValue)
                {
                    query += " AND c.NgayChamCong >= @TuNgay AND c.NgayChamCong <= @DenNgay ";
                }

                query += " ORDER BY c.NgayChamCong DESC, c.GioVao ASC ";

                using var cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    cmd.Parameters.AddWithValue("@TuKhoa", "%" + keyword + "%");
                }

                if (tuNgay.HasValue && denNgay.HasValue)
                {
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Value.Date);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay.Value.Date);
                }

                using var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi Database: " + ex.Message, ex);
            }

            return dt;
        }
    }
}
