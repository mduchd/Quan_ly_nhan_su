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
                if (TryFillFromDetailedAttendance(conn, dt, tuKhoa, tuNgay, denNgay))
                {
                    return dt;
                }

                if (TryFillFromEmployeeFallback(conn, dt, tuKhoa, tuNgay, denNgay))
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi Database: " + ex.Message, ex);
            }

            return dt;
        }

        private static bool TryFillFromDetailedAttendance(SqlConnection conn, DataTable dt, string tuKhoa, DateTime? tuNgay, DateTime? denNgay)
        {
            var attendanceTable = AttendanceSchemaHelper.ResolveDetailedAttendanceTable(conn);
            if (string.IsNullOrWhiteSpace(attendanceTable))
            {
                return false;
            }

            var attendanceEmployeeIdColumn = AttendanceSchemaHelper.ResolveColumn(conn, attendanceTable, null, "MaNV", "maNV");
            var attendanceDateColumn = AttendanceSchemaHelper.ResolveColumn(conn, attendanceTable, null, "Ngay", "ngay", "NgayChamCong");
            var attendanceCheckInColumn = AttendanceSchemaHelper.ResolveColumn(conn, attendanceTable, null, "GioVao", "gioVao", "CheckIn");
            var attendanceCheckOutColumn = AttendanceSchemaHelper.ResolveColumn(conn, attendanceTable, null, "GioRa", "gioRa", "CheckOut");
            var attendanceStatusColumn = AttendanceSchemaHelper.ResolveColumn(conn, attendanceTable, null, "TrangThai", "trangThai", "Status");

            if (string.IsNullOrWhiteSpace(attendanceEmployeeIdColumn) || string.IsNullOrWhiteSpace(attendanceDateColumn))
            {
                return false;
            }

            var employeeTable = AttendanceSchemaHelper.ResolveEmployeeTable(conn);
            var employeeIdColumn = string.IsNullOrWhiteSpace(employeeTable)
                ? null
                : AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "MaNV", "maNV");
            var employeeNameColumn = string.IsNullOrWhiteSpace(employeeTable)
                ? null
                : AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "TenNV", "HoTen", "hoTen");

            var joinSql = string.Empty;
            var employeeNameExpression = $"c.[{attendanceEmployeeIdColumn}]";
            if (!string.IsNullOrWhiteSpace(employeeTable) && !string.IsNullOrWhiteSpace(employeeIdColumn))
            {
                joinSql = $" LEFT JOIN [{employeeTable}] n ON c.[{attendanceEmployeeIdColumn}] = n.[{employeeIdColumn}]";
                if (!string.IsNullOrWhiteSpace(employeeNameColumn))
                {
                    employeeNameExpression = $"n.[{employeeNameColumn}]";
                }
            }

            var checkInExpression = string.IsNullOrWhiteSpace(attendanceCheckInColumn)
                ? "CAST(NULL AS time)"
                : $"c.[{attendanceCheckInColumn}]";
            var checkOutExpression = string.IsNullOrWhiteSpace(attendanceCheckOutColumn)
                ? "CAST(NULL AS time)"
                : $"c.[{attendanceCheckOutColumn}]";
            var totalHourExpression = string.IsNullOrWhiteSpace(attendanceCheckInColumn) || string.IsNullOrWhiteSpace(attendanceCheckOutColumn)
                ? "N'--'"
                : $@"CASE
                        WHEN c.[{attendanceCheckInColumn}] IS NOT NULL AND c.[{attendanceCheckOutColumn}] IS NOT NULL
                        THEN CAST(DATEDIFF(MINUTE, c.[{attendanceCheckInColumn}], c.[{attendanceCheckOutColumn}]) / 60 AS VARCHAR(10)) + 'h ' +
                             CAST(DATEDIFF(MINUTE, c.[{attendanceCheckInColumn}], c.[{attendanceCheckOutColumn}]) % 60 AS VARCHAR(10)) + 'm'
                        ELSE N'--'
                    END";
            var statusExpression = string.IsNullOrWhiteSpace(attendanceStatusColumn)
                ? "N''"
                : $"CAST(c.[{attendanceStatusColumn}] AS NVARCHAR(100))";

            var query = $@"
                SELECT
                    c.[{attendanceEmployeeIdColumn}] AS maNV,
                    {employeeNameExpression} AS hoTen,
                    CAST(c.[{attendanceDateColumn}] AS date) AS ngay,
                    {checkInExpression} AS gioVao,
                    {checkOutExpression} AS gioRa,
                    {totalHourExpression} AS TongGio,
                    {statusExpression} AS trangThai
                FROM [{attendanceTable}] c
                {joinSql}
                WHERE 1 = 1";

            var keyword = (tuKhoa ?? string.Empty).Trim();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query += $@"
                    AND (
                        c.[{attendanceEmployeeIdColumn}] LIKE @LikeTuKhoa
                        OR {employeeNameExpression} LIKE @LikeTuKhoa
                    )";
            }

            if (tuNgay.HasValue && denNgay.HasValue)
            {
                query += $@"
                    AND c.[{attendanceDateColumn}] >= @TuNgay
                    AND c.[{attendanceDateColumn}] < @DenNgayExclusive";
            }

            query += $@"
                ORDER BY c.[{attendanceDateColumn}] DESC";
            if (!string.IsNullOrWhiteSpace(attendanceCheckInColumn))
            {
                query += $", c.[{attendanceCheckInColumn}] ASC";
            }

            using var cmd = new SqlCommand(query, conn);
            AddSearchParameters(cmd, keyword);
            AddDateParameters(cmd, tuNgay, denNgay);

            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt.Rows.Count > 0;
        }

        private static bool TryFillFromEmployeeFallback(SqlConnection conn, DataTable dt, string tuKhoa, DateTime? tuNgay, DateTime? denNgay)
        {
            var employeeTable = AttendanceSchemaHelper.ResolveEmployeeTable(conn);
            if (string.IsNullOrWhiteSpace(employeeTable))
            {
                return false;
            }

            var employeeIdColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "MaNV", "maNV");
            var employeeNameColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "TenNV", "HoTen", "hoTen");
            var employeeDateColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "NgayChamCong");
            var employeeCheckInColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "GioVao", "gioVao");
            var employeeCheckOutColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "GioRa", "gioRa");

            if (string.IsNullOrWhiteSpace(employeeIdColumn) || string.IsNullOrWhiteSpace(employeeDateColumn))
            {
                return false;
            }

            var employeeNameExpression = string.IsNullOrWhiteSpace(employeeNameColumn)
                ? $"n.[{employeeIdColumn}]"
                : $"n.[{employeeNameColumn}]";
            var checkInExpression = string.IsNullOrWhiteSpace(employeeCheckInColumn)
                ? "CAST(NULL AS time)"
                : $"n.[{employeeCheckInColumn}]";
            var checkOutExpression = string.IsNullOrWhiteSpace(employeeCheckOutColumn)
                ? "CAST(NULL AS time)"
                : $"n.[{employeeCheckOutColumn}]";
            var totalHourExpression = string.IsNullOrWhiteSpace(employeeCheckInColumn) || string.IsNullOrWhiteSpace(employeeCheckOutColumn)
                ? "N'--'"
                : $@"CASE
                        WHEN n.[{employeeCheckInColumn}] IS NOT NULL AND n.[{employeeCheckOutColumn}] IS NOT NULL
                        THEN CAST(DATEDIFF(MINUTE, n.[{employeeCheckInColumn}], n.[{employeeCheckOutColumn}]) / 60 AS VARCHAR(10)) + 'h ' +
                             CAST(DATEDIFF(MINUTE, n.[{employeeCheckInColumn}], n.[{employeeCheckOutColumn}]) % 60 AS VARCHAR(10)) + 'm'
                        ELSE N'--'
                    END";

            var query = $@"
                SELECT
                    n.[{employeeIdColumn}] AS maNV,
                    {employeeNameExpression} AS hoTen,
                    CAST(n.[{employeeDateColumn}] AS date) AS ngay,
                    {checkInExpression} AS gioVao,
                    {checkOutExpression} AS gioRa,
                    {totalHourExpression} AS TongGio,
                    N'' AS trangThai
                FROM [{employeeTable}] n
                WHERE n.[{employeeDateColumn}] IS NOT NULL";

            var keyword = (tuKhoa ?? string.Empty).Trim();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query += $@"
                    AND (
                        n.[{employeeIdColumn}] LIKE @LikeTuKhoa
                        OR {employeeNameExpression} LIKE @LikeTuKhoa
                    )";
            }

            if (tuNgay.HasValue && denNgay.HasValue)
            {
                query += $@"
                    AND n.[{employeeDateColumn}] >= @TuNgay
                    AND n.[{employeeDateColumn}] < @DenNgayExclusive";
            }

            query += $@"
                ORDER BY n.[{employeeDateColumn}] DESC";
            if (!string.IsNullOrWhiteSpace(employeeCheckInColumn))
            {
                query += $", n.[{employeeCheckInColumn}] ASC";
            }

            using var cmd = new SqlCommand(query, conn);
            AddSearchParameters(cmd, keyword);
            AddDateParameters(cmd, tuNgay, denNgay);

            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt.Rows.Count > 0;
        }

        private static void AddSearchParameters(SqlCommand cmd, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return;
            }

            cmd.Parameters.Add("@LikeTuKhoa", SqlDbType.NVarChar, 110).Value = $"%{keyword}%";
        }

        private static void AddDateParameters(SqlCommand cmd, DateTime? tuNgay, DateTime? denNgay)
        {
            if (!tuNgay.HasValue || !denNgay.HasValue)
            {
                return;
            }

            cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.Value.Date;
            cmd.Parameters.Add("@DenNgayExclusive", SqlDbType.Date).Value = denNgay.Value.Date.AddDays(1);
        }
    }
}
