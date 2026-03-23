using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.DAL
{
    public class NhanVienDAL
    {
        public List<NhanVienDTO> LayDanhSachNhanVien(string tuKhoa, int pageNumber, int pageSize, out int totalRecords)
        {
            var ds = new List<NhanVienDTO>();
            totalRecords = 0;

            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 20;

            const string countQuery = @"
                SELECT COUNT(1)
                FROM NhanVien
                WHERE (@TuKhoa = '' OR MaNV LIKE @LikeTuKhoa OR TenNV LIKE @LikeTuKhoa OR SoDienThoai LIKE @LikeTuKhoa OR Email LIKE @LikeTuKhoa)";

            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            var dataQuery = @"
                SELECT MaNV, TenNV, NgaySinh, GioiTinh, ChucVu, SoDienThoai, Email, DiaChi, NgayVaoLam, TrangThai, PhongBan, LuongCung
                FROM NhanVien
                WHERE (@TuKhoa = '' OR MaNV LIKE @LikeTuKhoa OR TenNV LIKE @LikeTuKhoa OR SoDienThoai LIKE @LikeTuKhoa OR Email LIKE @LikeTuKhoa)
                ORDER BY MaNV
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var keyword = tuKhoa.Trim();

            using (var countCmd = new SqlCommand(countQuery, conn))
            {
                countCmd.Parameters.Add("@TuKhoa", SqlDbType.NVarChar, 100).Value = keyword;
                countCmd.Parameters.Add("@LikeTuKhoa", SqlDbType.NVarChar, 110).Value = $"%{keyword}%";
                totalRecords = Convert.ToInt32(countCmd.ExecuteScalar());
            }

            var offset = (pageNumber - 1) * pageSize;

            using var dataCmd = new SqlCommand(dataQuery, conn);
            dataCmd.Parameters.Add("@TuKhoa", SqlDbType.NVarChar, 100).Value = keyword;
            dataCmd.Parameters.Add("@LikeTuKhoa", SqlDbType.NVarChar, 110).Value = $"%{keyword}%";
            dataCmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
            dataCmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

            using var reader = dataCmd.ExecuteReader();
            while (reader.Read())
            {
                ds.Add(new NhanVienDTO
                {
                    MaNV = reader["MaNV"]?.ToString() ?? string.Empty,
                    TenNV = reader["TenNV"]?.ToString() ?? string.Empty,
                    NgaySinh = reader["NgaySinh"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(reader["NgaySinh"]),
                    GioiTinh = reader["GioiTinh"]?.ToString() ?? "Nam",
                    ChucVu = reader["ChucVu"]?.ToString() ?? string.Empty,
                    SoDienThoai = reader["SoDienThoai"]?.ToString() ?? string.Empty,
                    Email = reader["Email"]?.ToString() ?? string.Empty,
                    DiaChi = reader["DiaChi"]?.ToString() ?? string.Empty,
                    NgayVaoLam = reader["NgayVaoLam"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(reader["NgayVaoLam"]),
                    TrangThai = reader["TrangThai"] != DBNull.Value && Convert.ToBoolean(reader["TrangThai"]),
                    PhongBan = reader["PhongBan"]?.ToString() ?? string.Empty,
                    LuongCung = reader["LuongCung"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["LuongCung"]),
                    NgayChamCong = null,
                    GioVao = null,
                    GioRa = null
                });
            }

            return ds;
        }

        public List<NhanVienDTO> LayDanhSachNhanVien(string tuKhoa = "")
        {
            return LayDanhSachNhanVien(tuKhoa, 1, int.MaxValue / 4, out _);
        }

        public bool ThemNhanVien(NhanVienDTO nv)
        {
            const string query = @"
                INSERT INTO NhanVien(MaNV, TenNV, NgaySinh, GioiTinh, ChucVu, SoDienThoai, Email, DiaChi, NgayVaoLam, TrangThai, PhongBan, LuongCung)
                VALUES(@MaNV, @TenNV, @NgaySinh, @GioiTinh, @ChucVu, @SoDienThoai, @Email, @DiaChi, @NgayVaoLam, @TrangThai, @PhongBan, @LuongCung)";

            using var conn = DbContext.GetSqlConnection();
            using var cmd = new SqlCommand(query, conn);
            FillNhanVienParameters(cmd, nv);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CapNhatNhanVien(NhanVienDTO nv)
        {
            const string query = @"
                UPDATE NhanVien
                SET TenNV = @TenNV,
                    NgaySinh = @NgaySinh,
                    GioiTinh = @GioiTinh,
                    ChucVu = @ChucVu,
                    SoDienThoai = @SoDienThoai,
                    Email = @Email,
                    DiaChi = @DiaChi,
                    NgayVaoLam = @NgayVaoLam,
                    TrangThai = @TrangThai,
                    PhongBan = @PhongBan,
                    LuongCung = @LuongCung
                WHERE MaNV = @MaNV";

            using var conn = DbContext.GetSqlConnection();
            using var cmd = new SqlCommand(query, conn);
            FillNhanVienParameters(cmd, nv);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool XoaNhanVien(string maNV)
        {
            const string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";

            using var conn = DbContext.GetSqlConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = maNV;
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool KiemTraTrungSoDienThoaiHoacEmail(string soDienThoai, string email, string? maNVHienTai = null)
        {
            const string query = @"
                SELECT COUNT(1)
                FROM NhanVien
                WHERE (SoDienThoai = @SoDienThoai OR Email = @Email)
                  AND (@MaNVHienTai IS NULL OR MaNV <> @MaNVHienTai)";

            using var conn = DbContext.GetSqlConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@SoDienThoai", SqlDbType.VarChar, 20).Value = soDienThoai;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;
            cmd.Parameters.Add("@MaNVHienTai", SqlDbType.VarChar, 20).Value = (object?)maNVHienTai ?? DBNull.Value;
            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public string TaoMaNhanVienMoi()
        {
            const string query = @"
                SELECT TOP 1 MaNV
                FROM NhanVien
                WHERE MaNV LIKE 'NV%'
                ORDER BY TRY_CONVERT(INT, SUBSTRING(MaNV, 3, LEN(MaNV) - 2)) DESC";

            using var conn = DbContext.GetSqlConnection();
            using var cmd = new SqlCommand(query, conn);
            conn.Open();

            var maCuoi = cmd.ExecuteScalar()?.ToString();
            if (string.IsNullOrWhiteSpace(maCuoi))
            {
                return "NV001";
            }

            var so = 0;
            if (maCuoi.StartsWith("NV", StringComparison.OrdinalIgnoreCase))
            {
                int.TryParse(maCuoi.Substring(2), out so);
            }

            return $"NV{(so + 1):D3}";
        }

        public List<string> LayDanhSachPhongBan()
        {
            var ds = new List<string>();
            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            const string query = @"
                SELECT TenPhongBan
                FROM PhongBan
                WHERE TenPhongBan IS NOT NULL AND LTRIM(RTRIM(TenPhongBan)) <> ''
                ORDER BY TenPhongBan";

            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ds.Add(reader["TenPhongBan"].ToString() ?? string.Empty);
            }

            return ds;
        }

        public bool ThemPhongBan(string tenPhongBan)
        {
            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            string query = "INSERT INTO PhongBan (TenPhongBan) VALUES (@TenPhongBan)";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@TenPhongBan", SqlDbType.NVarChar, 100).Value = tenPhongBan;
            
            try
            {
                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DoiTenPhongBan(string tenCu, string tenMoi)
        {
            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            string query = "UPDATE PhongBan SET TenPhongBan = @TenMoi WHERE TenPhongBan = @TenCu";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenMoi", tenMoi);
            cmd.Parameters.AddWithValue("@TenCu", tenCu);

            try
            {
                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool XoaPhongBan(string tenPhongBan)
        {
            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            string query = "DELETE FROM PhongBan WHERE TenPhongBan = @TenPhongBan";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenPhongBan", tenPhongBan);

            try
            {
                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool KiemTraPhongBanDangDuocSuDung(string tenPhongBan)
        {
            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            string query = "SELECT COUNT(1) FROM NhanVien WHERE PhongBan = @TenPhongBan";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenPhongBan", tenPhongBan);

            return (int)cmd.ExecuteScalar() > 0;
        }

        private static void FillNhanVienParameters(SqlCommand cmd, NhanVienDTO nv)
        {
            cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = nv.MaNV;
            cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar, 150).Value = nv.TenNV;
            cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = nv.NgaySinh.Date;
            cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 10).Value = nv.GioiTinh;
            cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar, 100).Value = nv.ChucVu;
            cmd.Parameters.Add("@SoDienThoai", SqlDbType.VarChar, 20).Value = nv.SoDienThoai;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = nv.Email;
            cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 255).Value = nv.DiaChi;
            cmd.Parameters.Add("@NgayVaoLam", SqlDbType.Date).Value = nv.NgayVaoLam.Date;
            cmd.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = nv.TrangThai;
            cmd.Parameters.Add("@PhongBan", SqlDbType.NVarChar, 100).Value = nv.PhongBan;
            cmd.Parameters.Add("@LuongCung", SqlDbType.Decimal).Value = nv.LuongCung;
        }
    }
}
