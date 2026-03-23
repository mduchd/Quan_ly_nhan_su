using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.DAL
{
    public class BangLuongDAL
    {
        public List<BangLuongDTO> GetDanhSachBangLuong(string tuKhoa = "", string thangNam = "")
        {
            using var conn = DbContext.GetSqlConnection();
            conn.Open();

            var thangDuocChon = ResolveMonth(thangNam);
            var dsNhanVien = LoadEmployees(conn, tuKhoa);
            var soNgayLamMap = LoadWorkedDays(conn, thangDuocChon);
            var soNgayChuan = DateTime.DaysInMonth(thangDuocChon.Year, thangDuocChon.Month);

            foreach (var nv in dsNhanVien)
            {
                nv.SoNgayChuan = soNgayChuan;
                nv.SoNgayLam = soNgayLamMap.TryGetValue(nv.MaNV, out var soNgayLam) ? soNgayLam : 0;
            }

            return dsNhanVien;
        }

        private static List<BangLuongDTO> LoadEmployees(SqlConnection conn, string tuKhoa)
        {
            var employeeTable = AttendanceSchemaHelper.ResolveEmployeeTable(conn);
            if (string.IsNullOrWhiteSpace(employeeTable))
            {
                return new List<BangLuongDTO>();
            }

            var employeeIdColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "MaNV", "maNV");
            var employeeNameColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "TenNV", "HoTen", "hoTen");
            var salaryColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "LuongCung", "Luong", "MucLuong");

            if (string.IsNullOrWhiteSpace(employeeIdColumn) || string.IsNullOrWhiteSpace(employeeNameColumn))
            {
                return new List<BangLuongDTO>();
            }

            var salaryExpression = string.IsNullOrWhiteSpace(salaryColumn)
                ? "CAST(0 AS DECIMAL(18,2))"
                : $"CAST(ISNULL(n.[{salaryColumn}], 0) AS DECIMAL(18,2))";

            var query = $@"
                SELECT
                    n.[{employeeIdColumn}] AS MaNV,
                    n.[{employeeNameColumn}] AS TenNV,
                    {salaryExpression} AS LuongCung
                FROM [{employeeTable}] n
                WHERE (@TuKhoa = N'' OR n.[{employeeIdColumn}] LIKE @LikeTuKhoa OR n.[{employeeNameColumn}] LIKE @LikeTuKhoa)
                ORDER BY n.[{employeeIdColumn}]";

            var dsNhanVien = new List<BangLuongDTO>();
            var keyword = (tuKhoa ?? string.Empty).Trim();

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@TuKhoa", SqlDbType.NVarChar, 100).Value = keyword;
            cmd.Parameters.Add("@LikeTuKhoa", SqlDbType.NVarChar, 110).Value = $"%{keyword}%";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dsNhanVien.Add(new BangLuongDTO
                {
                    MaNV = reader["MaNV"]?.ToString() ?? string.Empty,
                    TenNV = reader["TenNV"]?.ToString() ?? string.Empty,
                    LuongCung = reader["LuongCung"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["LuongCung"])
                });
            }

            return dsNhanVien;
        }

        private static Dictionary<string, int> LoadWorkedDays(SqlConnection conn, DateTime thangDuocChon)
        {
            var ngayBatDau = new DateTime(thangDuocChon.Year, thangDuocChon.Month, 1);
            var ngayKetThuc = ngayBatDau.AddMonths(1);

            var result = LoadWorkedDaysFromDetailedAttendance(conn, ngayBatDau, ngayKetThuc);
            MergeMissingWorkedDays(result, LoadWorkedDaysFromSummaryTable(conn, thangDuocChon));
            MergeMissingWorkedDays(result, LoadWorkedDaysFromEmployeeTable(conn, ngayBatDau, ngayKetThuc));
            return result;
        }

        private static Dictionary<string, int> LoadWorkedDaysFromDetailedAttendance(SqlConnection conn, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var attendanceTable = AttendanceSchemaHelper.ResolveDetailedAttendanceTable(conn);
            if (string.IsNullOrWhiteSpace(attendanceTable))
            {
                return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            }

            var employeeIdColumn = AttendanceSchemaHelper.ResolveColumn(conn, attendanceTable, null, "MaNV", "maNV");
            var dateColumn = AttendanceSchemaHelper.ResolveColumn(conn, attendanceTable, null, "Ngay", "ngay", "NgayChamCong");
            var checkInColumn = AttendanceSchemaHelper.ResolveColumn(conn, attendanceTable, null, "GioVao", "gioVao", "CheckIn");
            var checkOutColumn = AttendanceSchemaHelper.ResolveColumn(conn, attendanceTable, null, "GioRa", "gioRa", "CheckOut");

            if (string.IsNullOrWhiteSpace(employeeIdColumn) || string.IsNullOrWhiteSpace(dateColumn))
            {
                return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            }

            var attendanceCondition = string.IsNullOrWhiteSpace(checkInColumn) && string.IsNullOrWhiteSpace(checkOutColumn)
                ? "1 = 1"
                : string.IsNullOrWhiteSpace(checkOutColumn)
                    ? $"c.[{checkInColumn}] IS NOT NULL"
                    : string.IsNullOrWhiteSpace(checkInColumn)
                        ? $"c.[{checkOutColumn}] IS NOT NULL"
                        : $"(c.[{checkInColumn}] IS NOT NULL OR c.[{checkOutColumn}] IS NOT NULL)";

            var query = $@"
                SELECT
                    c.[{employeeIdColumn}] AS MaNV,
                    COUNT(DISTINCT CAST(c.[{dateColumn}] AS date)) AS SoNgayLam
                FROM [{attendanceTable}] c
                WHERE c.[{dateColumn}] >= @NgayBatDau
                  AND c.[{dateColumn}] < @NgayKetThuc
                  AND {attendanceCondition}
                GROUP BY c.[{employeeIdColumn}]";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@NgayBatDau", SqlDbType.Date).Value = ngayBatDau.Date;
            cmd.Parameters.Add("@NgayKetThuc", SqlDbType.Date).Value = ngayKetThuc.Date;

            return ReadWorkedDayDictionary(cmd);
        }

        private static Dictionary<string, int> LoadWorkedDaysFromSummaryTable(SqlConnection conn, DateTime thangDuocChon)
        {
            var summaryTable = AttendanceSchemaHelper.ResolveMonthlyAttendanceTable(conn);
            if (string.IsNullOrWhiteSpace(summaryTable))
            {
                return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            }

            var employeeIdColumn = AttendanceSchemaHelper.ResolveColumn(conn, summaryTable, null, "MaNV", "maNV");
            var monthColumn = AttendanceSchemaHelper.ResolveColumn(conn, summaryTable, null, "ThangNam", "Thang", "KyLuong");
            var workDayColumn = AttendanceSchemaHelper.ResolveColumn(conn, summaryTable, null, "SoNgayLam", "NgayCong", "TongNgayLam");

            if (string.IsNullOrWhiteSpace(employeeIdColumn) || string.IsNullOrWhiteSpace(monthColumn) || string.IsNullOrWhiteSpace(workDayColumn))
            {
                return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            }

            var query = $@"
                SELECT
                    c.[{employeeIdColumn}] AS MaNV,
                    CAST(ISNULL(c.[{workDayColumn}], 0) AS INT) AS SoNgayLam
                FROM [{summaryTable}] c
                WHERE c.[{monthColumn}] = @ThangNam";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@ThangNam", SqlDbType.NVarChar, 20).Value = thangDuocChon.ToString("MM/yyyy", CultureInfo.InvariantCulture);

            return ReadWorkedDayDictionary(cmd);
        }

        private static Dictionary<string, int> LoadWorkedDaysFromEmployeeTable(SqlConnection conn, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var employeeTable = AttendanceSchemaHelper.ResolveEmployeeTable(conn);
            if (string.IsNullOrWhiteSpace(employeeTable))
            {
                return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            }

            var employeeIdColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "MaNV", "maNV");
            var dateColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "NgayChamCong");
            var checkInColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "GioVao", "gioVao");

            if (string.IsNullOrWhiteSpace(employeeIdColumn) || string.IsNullOrWhiteSpace(dateColumn))
            {
                return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            }

            var attendedExpression = string.IsNullOrWhiteSpace(checkInColumn)
                ? "1"
                : $"CASE WHEN n.[{checkInColumn}] IS NOT NULL THEN 1 ELSE 0 END";

            var query = $@"
                SELECT
                    n.[{employeeIdColumn}] AS MaNV,
                    {attendedExpression} AS SoNgayLam
                FROM [{employeeTable}] n
                WHERE n.[{dateColumn}] >= @NgayBatDau
                  AND n.[{dateColumn}] < @NgayKetThuc";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@NgayBatDau", SqlDbType.Date).Value = ngayBatDau.Date;
            cmd.Parameters.Add("@NgayKetThuc", SqlDbType.Date).Value = ngayKetThuc.Date;

            return ReadWorkedDayDictionary(cmd);
        }

        private static Dictionary<string, int> ReadWorkedDayDictionary(SqlCommand cmd)
        {
            var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var maNV = reader["MaNV"]?.ToString();
                if (string.IsNullOrWhiteSpace(maNV))
                {
                    continue;
                }

                var soNgayLam = reader["SoNgayLam"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SoNgayLam"]);
                result[maNV] = soNgayLam;
            }

            return result;
        }

        private static void MergeMissingWorkedDays(Dictionary<string, int> target, Dictionary<string, int> source)
        {
            foreach (var item in source)
            {
                if (!target.ContainsKey(item.Key))
                {
                    target[item.Key] = item.Value;
                }
            }
        }

        private static DateTime ResolveMonth(string thangNam)
        {
            if (DateTime.TryParseExact(
                thangNam,
                "MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var thangDuocChon))
            {
                return new DateTime(thangDuocChon.Year, thangDuocChon.Month, 1);
            }

            var now = DateTime.Now;
            return new DateTime(now.Year, now.Month, 1);
        }
    }
}
