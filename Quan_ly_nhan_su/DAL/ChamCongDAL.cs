using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.DAL
{
    internal class ChamCongDAL
    {
        private readonly QuanLyCongDAL _quanLyCongDAL = new();

        public bool ThemYeuCau(ChamCongDTO yeuCau)
        {
            string query = @"
                INSERT INTO YeuCauNghiPhep(TenNhanVien, PhongBan, LoaiNghi, TuNgay, DenNgay, LyDo)
                VALUES (@TenNhanVien, @PhongBan, @LoaiNghi, @TuNgay, @DenNgay, @LyDo)";

            using var conn = DbContext.GetSqlConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenNhanVien", yeuCau.TenNhanVien);
            cmd.Parameters.AddWithValue("@PhongBan", yeuCau.PhongBan ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@LoaiNghi", yeuCau.LoaiNghi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@TuNgay", yeuCau.TuNgay.Date);
            cmd.Parameters.AddWithValue("@DenNgay", yeuCau.DenNgay.Date);
            cmd.Parameters.AddWithValue("@LyDo", yeuCau.LyDo ?? (object)DBNull.Value);

            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm yêu cầu nghỉ phép.", ex);
            }
        }

        public NhanVienDTO? LayThongTinNhanVien(string maNV)
        {
            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            string query = @"
                SELECT MaNV, TenNV, SoDienThoai, DiaChi, PhongBan, ChucVu
                FROM NhanVien
                WHERE MaNV = @MaNV";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNV", maNV.Trim());

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return new NhanVienDTO
            {
                MaNV = reader["MaNV"]?.ToString() ?? string.Empty,
                TenNV = reader["TenNV"]?.ToString() ?? string.Empty,
                SoDienThoai = reader["SoDienThoai"]?.ToString() ?? string.Empty,
                DiaChi = reader["DiaChi"]?.ToString() ?? string.Empty,
                PhongBan = reader["PhongBan"]?.ToString() ?? string.Empty,
                ChucVu = reader["ChucVu"]?.ToString() ?? string.Empty
            };
        }

        public List<LichSuCongDTO> LayLichSuChamCongGanDay(string maNV, int soBanGhi = 3)
        {
            var lichSu = new List<LichSuCongDTO>();
            var dt = _quanLyCongDAL.LayDanhSachLichSuCong(maNV, DateTime.Today.AddYears(-1), DateTime.Today);

            foreach (DataRow row in dt.Rows)
            {
                var ma = row["maNV"]?.ToString();
                if (!string.Equals(ma, maNV, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                lichSu.Add(new LichSuCongDTO
                {
                    maNV = ma ?? string.Empty,
                    hoTen = row["hoTen"]?.ToString() ?? string.Empty,
                    ngay = row["ngay"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["ngay"]),
                    gioVao = row["gioVao"] == DBNull.Value ? null : (TimeSpan?)row["gioVao"],
                    gioRa = row["gioRa"] == DBNull.Value ? null : (TimeSpan?)row["gioRa"],
                    trangThai = row["trangThai"]?.ToString() ?? string.Empty
                });

                if (lichSu.Count >= soBanGhi)
                {
                    break;
                }
            }

            return lichSu;
        }

        public bool CheckIn(string maNV, DateTime thoiDiem, out string message)
        {
            message = string.Empty;
            maNV = (maNV ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(maNV))
            {
                message = "Vui lòng nhập mã nhân viên.";
                return false;
            }

            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            // 1. Kiểm tra tồn tại nhân viên
            string checkNvQuery = "SELECT COUNT(1) FROM NhanVien WHERE MaNV = @MaNV";
            using (var cmdCheck = new SqlCommand(checkNvQuery, conn))
            {
                cmdCheck.Parameters.AddWithValue("@MaNV", maNV);
                int exists = (int)cmdCheck.ExecuteScalar();
                if (exists == 0)
                {
                    message = "Không tìm thấy nhân viên với mã đã nhập.";
                    return false;
                }
            }

            // 2. Kiểm tra xem hôm nay đã Check-in chưa
            string checkDaChamCongQuery = "SELECT GioVao FROM ChamCong WHERE MaNV = @MaNV AND NgayChamCong = @Ngay";
            using (var cmdCheckDaChamCong = new SqlCommand(checkDaChamCongQuery, conn))
            {
                cmdCheckDaChamCong.Parameters.AddWithValue("@MaNV", maNV);
                cmdCheckDaChamCong.Parameters.AddWithValue("@Ngay", thoiDiem.Date);

                using var reader = cmdCheckDaChamCong.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["GioVao"] != DBNull.Value)
                    {
                        message = "Nhân viên đã check-in hôm nay.";
                        return false;
                    }
                }
            }

            // 3. Thực hiện Check-in
            string checkInQuery = @"
                IF EXISTS(SELECT 1 FROM ChamCong WHERE MaNV = @MaNV AND NgayChamCong = @Ngay)
                    UPDATE ChamCong SET GioVao = @GioVao WHERE MaNV = @MaNV AND NgayChamCong = @Ngay
                ELSE
                    INSERT INTO ChamCong (MaNV, NgayChamCong, GioVao) VALUES (@MaNV, @Ngay, @GioVao)";

            using (var cmdIn = new SqlCommand(checkInQuery, conn))
            {
                cmdIn.Parameters.AddWithValue("@MaNV", maNV);
                cmdIn.Parameters.AddWithValue("@Ngay", thoiDiem.Date);
                cmdIn.Parameters.AddWithValue("@GioVao", thoiDiem.TimeOfDay);

                cmdIn.ExecuteNonQuery();
                message = "Check-in thành công.";
                return true;
            }
        }

        public bool CheckOut(string maNV, DateTime thoiDiem, out string message)
        {
            message = string.Empty;
            maNV = (maNV ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(maNV))
            {
                message = "Vui lòng nhập mã nhân viên.";
                return false;
            }

            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            // 1. Kiểm tra tồn tại nhân viên
            string checkNvQuery = "SELECT COUNT(1) FROM NhanVien WHERE MaNV = @MaNV";
            using (var cmdCheck = new SqlCommand(checkNvQuery, conn))
            {
                cmdCheck.Parameters.AddWithValue("@MaNV", maNV);
                int exists = (int)cmdCheck.ExecuteScalar();
                if (exists == 0)
                {
                    message = "Không tìm thấy nhân viên với mã đã nhập.";
                    return false;
                }
            }

            // 2. Kiểm tra log chấm công trong ngày
            TimeSpan? gioVao = null;
            TimeSpan? gioRa = null;

            string checkDaChamCongQuery = "SELECT GioVao, GioRa FROM ChamCong WHERE MaNV = @MaNV AND NgayChamCong = @Ngay";
            using (var cmdCheckDaChamCong = new SqlCommand(checkDaChamCongQuery, conn))
            {
                cmdCheckDaChamCong.Parameters.AddWithValue("@MaNV", maNV);
                cmdCheckDaChamCong.Parameters.AddWithValue("@Ngay", thoiDiem.Date);

                using var reader = cmdCheckDaChamCong.ExecuteReader();
                if (reader.Read())
                {
                    gioVao = reader["GioVao"] == DBNull.Value ? null : (TimeSpan?)reader["GioVao"];
                    gioRa = reader["GioRa"] == DBNull.Value ? null : (TimeSpan?)reader["GioRa"];
                }
            }

            if (!gioVao.HasValue)
            {
                message = "Bạn chưa check-in hôm nay.";
                return false;
            }

            if (gioRa.HasValue)
            {
                message = "Nhân viên đã check-out hôm nay.";
                return false;
            }

            // 3. Thực hiện Check-out và Tính tổng giờ
            var tsGioVao = gioVao.Value;
            var tsGioRa = thoiDiem.TimeOfDay;
            var tongGioPhut = (tsGioRa - tsGioVao).TotalHours;

            string checkOutQuery = @"
                UPDATE ChamCong 
                SET GioRa = @GioRa, TongGio = @TongGio 
                WHERE MaNV = @MaNV AND NgayChamCong = @Ngay";

            using (var cmdOut = new SqlCommand(checkOutQuery, conn))
            {
                cmdOut.Parameters.AddWithValue("@MaNV", maNV);
                cmdOut.Parameters.AddWithValue("@Ngay", thoiDiem.Date);
                cmdOut.Parameters.AddWithValue("@GioRa", tsGioRa);
                cmdOut.Parameters.AddWithValue("@TongGio", (decimal)tongGioPhut);

                cmdOut.ExecuteNonQuery();
                message = "Check-out thành công.";
                return true;
            }
        }
    }
}
