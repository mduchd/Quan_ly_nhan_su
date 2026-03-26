using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.DAL
{
    public class BangLuongDAL
    {
        public List<BangLuongDTO> GetDanhSachBangLuong(string tuKhoa = "", string thangNam = "")
        {
            var dsBangLuong = new List<BangLuongDTO>();
            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            DateTime thangDuocChon = ResolveMonth(thangNam);
            int soNgayChuan = DateTime.DaysInMonth(thangDuocChon.Year, thangDuocChon.Month);

            string keyword = tuKhoa.Trim();

            string query = @"
                SELECT 
                    n.MaNV, 
                    n.TenNV, 
                    n.LuongCung,
                    ISNULL(c.SoNgayLam, 0) AS SoNgayLam
                FROM NhanVien n
                LEFT JOIN (
                    SELECT MaNV, COUNT(DISTINCT NgayChamCong) AS SoNgayLam
                    FROM ChiTietChamCong
                    WHERE YEAR(NgayChamCong) = @Nam AND MONTH(NgayChamCong) = @Thang
                      AND GioVao IS NOT NULL AND GioRa IS NOT NULL
                    GROUP BY MaNV
                ) c ON n.MaNV = c.MaNV
                WHERE (@TuKhoa = '' OR n.MaNV LIKE @LikeTuKhoa OR n.TenNV LIKE @LikeTuKhoa)
                ORDER BY n.MaNV";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nam", thangDuocChon.Year);
            cmd.Parameters.AddWithValue("@Thang", thangDuocChon.Month);
            cmd.Parameters.AddWithValue("@TuKhoa", keyword);
            cmd.Parameters.AddWithValue("@LikeTuKhoa", "%" + keyword + "%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var dto = new BangLuongDTO
                {
                    MaNV = reader["MaNV"]?.ToString() ?? string.Empty,
                    TenNV = reader["TenNV"]?.ToString() ?? string.Empty,
                    LuongCung = reader["LuongCung"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["LuongCung"]),
                    SoNgayChuan = soNgayChuan,
                    SoNgayLam = reader["SoNgayLam"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SoNgayLam"])
                };

                dsBangLuong.Add(dto);
            }

            return dsBangLuong;
        }

        // HÀM MỚI: Cập nhật Lương Cứng
        public bool CapNhatLuongCung(string maNV, decimal luongMoi)
        {
            using var conn = DbContext.GetSqlConnection();
            conn.Open();
            string query = "UPDATE NhanVien SET LuongCung = @LuongCung WHERE MaNV = @MaNV";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LuongCung", luongMoi);
            cmd.Parameters.AddWithValue("@MaNV", maNV);
            return cmd.ExecuteNonQuery() > 0;
        }

        private static DateTime ResolveMonth(string thangNam)
        {
            if (!string.IsNullOrWhiteSpace(thangNam))
            {
                if (DateTime.TryParseExact(thangNam, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out var m)) return m;
                if (DateTime.TryParseExact(thangNam, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out m)) return m;
            }
            return DateTime.Today;
        }
    }
}