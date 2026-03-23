using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.DAL
{
    internal class ChamCongDAL
    {
        private readonly QuanLyCongDAL _quanLyCongDAL = new();

        public bool ThemYeuCau(ChamCongDTO yeuCau)
        {
            const string query = @"
                INSERT INTO YeuCauNghiPhep(TenNhanVien, PhongBan, LoaiNghi, TuNgay, DenNgay, LyDo)
                VALUES (@TenNhanVien, @PhongBan, @LoaiNghi, @TuNgay, @DenNgay, @LyDo)";

            using var conn = DbContext.GetSqlConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@TenNhanVien", SqlDbType.NVarChar, 100).Value = yeuCau.TenNhanVien;
            cmd.Parameters.Add("@PhongBan", SqlDbType.NVarChar, 100).Value = yeuCau.PhongBan;
            cmd.Parameters.Add("@LoaiNghi", SqlDbType.NVarChar, 100).Value = yeuCau.LoaiNghi;
            cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = yeuCau.TuNgay.Date;
            cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = yeuCau.DenNgay.Date;
            cmd.Parameters.Add("@LyDo", SqlDbType.NVarChar, 500).Value = yeuCau.LyDo;

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

            var employeeTable = AttendanceSchemaHelper.ResolveEmployeeTable(conn);
            if (string.IsNullOrWhiteSpace(employeeTable))
            {
                return null;
            }

            var employeeIdColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "MaNV", "maNV");
            var employeeNameColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "TenNV", "HoTen", "hoTen");
            var employeePhoneColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "SoDienThoai", "DienThoai", "SDT");
            var employeeAddressColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "DiaChi", "DiaChiThuongTru");
            var employeeDepartmentColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "PhongBan", "TenPhongBan");
            var employeePositionColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "ChucVu");
            var employeeDateColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "NgayChamCong");
            var employeeCheckInColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "GioVao", "gioVao");
            var employeeCheckOutColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, null, "GioRa", "gioRa");

            if (string.IsNullOrWhiteSpace(employeeIdColumn) || string.IsNullOrWhiteSpace(employeeNameColumn))
            {
                return null;
            }

            var phoneExpression = string.IsNullOrWhiteSpace(employeePhoneColumn) ? "N''" : $"n.[{employeePhoneColumn}]";
            var addressExpression = string.IsNullOrWhiteSpace(employeeAddressColumn) ? "N''" : $"n.[{employeeAddressColumn}]";
            var departmentExpression = string.IsNullOrWhiteSpace(employeeDepartmentColumn) ? "N''" : $"n.[{employeeDepartmentColumn}]";
            var positionExpression = string.IsNullOrWhiteSpace(employeePositionColumn) ? "N''" : $"n.[{employeePositionColumn}]";
            var dateExpression = string.IsNullOrWhiteSpace(employeeDateColumn) ? "CAST(NULL AS date)" : $"n.[{employeeDateColumn}]";
            var checkInExpression = string.IsNullOrWhiteSpace(employeeCheckInColumn) ? "CAST(NULL AS time)" : $"n.[{employeeCheckInColumn}]";
            var checkOutExpression = string.IsNullOrWhiteSpace(employeeCheckOutColumn) ? "CAST(NULL AS time)" : $"n.[{employeeCheckOutColumn}]";

            var query = $@"
                SELECT TOP 1
                    n.[{employeeIdColumn}] AS MaNV,
                    n.[{employeeNameColumn}] AS TenNV,
                    {phoneExpression} AS SoDienThoai,
                    {addressExpression} AS DiaChi,
                    {departmentExpression} AS PhongBan,
                    {positionExpression} AS ChucVu,
                    {dateExpression} AS NgayChamCong,
                    {checkInExpression} AS GioVao,
                    {checkOutExpression} AS GioRa
                FROM [{employeeTable}] n
                WHERE n.[{employeeIdColumn}] = @MaNV";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV.Trim();

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
                ChucVu = reader["ChucVu"]?.ToString() ?? string.Empty,
                NgayChamCong = ToNullableDateTime(reader["NgayChamCong"]),
                GioVao = ToNullableTimeSpan(reader["GioVao"]),
                GioRa = ToNullableTimeSpan(reader["GioRa"])
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
                    gioVao = ToNullableTimeSpan(row["gioVao"]),
                    gioRa = ToNullableTimeSpan(row["gioRa"]),
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
            using var tran = conn.BeginTransaction();

            try
            {
                var employeeTable = AttendanceSchemaHelper.ResolveEmployeeTable(conn, tran);
                var employeeIdColumn = string.IsNullOrWhiteSpace(employeeTable)
                    ? null
                    : AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, tran, "MaNV", "maNV");

                if (string.IsNullOrWhiteSpace(employeeTable) || string.IsNullOrWhiteSpace(employeeIdColumn) || !EmployeeExists(conn, tran, employeeTable, employeeIdColumn, maNV))
                {
                    tran.Rollback();
                    message = "Không tìm thấy nhân viên với mã đã nhập.";
                    return false;
                }

                var detailedTable = AttendanceSchemaHelper.ResolveDetailedAttendanceTable(conn, tran);
                if (!string.IsNullOrWhiteSpace(detailedTable))
                {
                    var canContinue = HandleCheckInOnDetailedAttendance(conn, tran, detailedTable, maNV, thoiDiem, out message);
                    if (!canContinue)
                    {
                        tran.Rollback();
                        return false;
                    }
                }

                UpdateEmployeeAttendanceSnapshot(conn, tran, employeeTable, employeeIdColumn, maNV, thoiDiem, null, clearCheckOut: true);

                tran.Commit();
                message = "Check-in thành công.";
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                message = "Lỗi khi check-in: " + ex.Message;
                return false;
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
            using var tran = conn.BeginTransaction();

            try
            {
                var employeeTable = AttendanceSchemaHelper.ResolveEmployeeTable(conn, tran);
                var employeeIdColumn = string.IsNullOrWhiteSpace(employeeTable)
                    ? null
                    : AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, tran, "MaNV", "maNV");

                if (string.IsNullOrWhiteSpace(employeeTable) || string.IsNullOrWhiteSpace(employeeIdColumn) || !EmployeeExists(conn, tran, employeeTable, employeeIdColumn, maNV))
                {
                    tran.Rollback();
                    message = "Không tìm thấy nhân viên với mã đã nhập.";
                    return false;
                }

                var employeeState = LoadEmployeeAttendanceState(conn, tran, employeeTable, employeeIdColumn, maNV);
                var detailedTable = AttendanceSchemaHelper.ResolveDetailedAttendanceTable(conn, tran);
                var hasDetailedRecord = false;
                TimeSpan? detailedCheckIn = null;

                if (!string.IsNullOrWhiteSpace(detailedTable))
                {
                    var canContinue = HandleCheckOutOnDetailedAttendance(conn, tran, detailedTable, maNV, thoiDiem, out hasDetailedRecord, out detailedCheckIn, out message);
                    if (!canContinue)
                    {
                        tran.Rollback();
                        return false;
                    }
                }

                if (!hasDetailedRecord)
                {
                    if (!employeeState.NgayChamCong.HasValue || employeeState.NgayChamCong.Value.Date != thoiDiem.Date || !employeeState.GioVao.HasValue)
                    {
                        tran.Rollback();
                        message = "Bạn chưa check-in hôm nay.";
                        return false;
                    }

                    if (employeeState.GioRa.HasValue)
                    {
                        tran.Rollback();
                        message = "Nhân viên đã check-out hôm nay.";
                        return false;
                    }
                }

                var gioVaoSnapshot = detailedCheckIn ?? employeeState.GioVao;
                var thoiDiemGioVao = gioVaoSnapshot.HasValue ? thoiDiem.Date.Add(gioVaoSnapshot.Value) : thoiDiem;
                UpdateEmployeeAttendanceSnapshot(conn, tran, employeeTable, employeeIdColumn, maNV, thoiDiemGioVao, thoiDiem);
                UpdateMonthlyWorkedDays(conn, tran, maNV, thoiDiem);

                tran.Commit();
                message = "Check-out thành công.";
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                message = "Lỗi khi check-out: " + ex.Message;
                return false;
            }
        }

        private static bool HandleCheckInOnDetailedAttendance(SqlConnection conn, SqlTransaction tran, string detailedTable, string maNV, DateTime thoiDiem, out string message)
        {
            message = string.Empty;

            var employeeIdColumn = AttendanceSchemaHelper.ResolveColumn(conn, detailedTable, tran, "MaNV", "maNV");
            var dateColumn = AttendanceSchemaHelper.ResolveColumn(conn, detailedTable, tran, "Ngay", "ngay", "NgayChamCong");
            var checkInColumn = AttendanceSchemaHelper.ResolveColumn(conn, detailedTable, tran, "GioVao", "gioVao", "CheckIn");

            if (string.IsNullOrWhiteSpace(employeeIdColumn) || string.IsNullOrWhiteSpace(dateColumn) || string.IsNullOrWhiteSpace(checkInColumn))
            {
                return true;
            }

            var query = $@"
                SELECT TOP 1
                    c.[{checkInColumn}] AS GioVao
                FROM [{detailedTable}] c
                WHERE c.[{employeeIdColumn}] = @MaNV
                  AND CAST(c.[{dateColumn}] AS date) = @Ngay
                ORDER BY c.[{dateColumn}] DESC";

            using var cmd = new SqlCommand(query, conn, tran);
            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;
            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = thoiDiem.Date;

            using var reader = cmd.ExecuteReader();
            var daCoBanGhiHomNay = reader.Read();
            var gioVaoDaCo = daCoBanGhiHomNay ? ToNullableTimeSpan(reader["GioVao"]) : null;
            reader.Close();

            if (gioVaoDaCo.HasValue)
            {
                message = "Nhân viên đã check-in hôm nay.";
                return false;
            }

            if (daCoBanGhiHomNay)
            {
                var updateQuery = $@"
                    UPDATE [{detailedTable}]
                    SET [{checkInColumn}] = @GioVao
                    WHERE [{employeeIdColumn}] = @MaNV
                      AND CAST([{dateColumn}] AS date) = @Ngay";

                using var updateCmd = new SqlCommand(updateQuery, conn, tran);
                updateCmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;
                updateCmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = thoiDiem.Date;
                AddColumnValue(updateCmd, "@GioVao", detailedTable, checkInColumn, conn, tran, thoiDiem, isDateOnly: false, isTimeOnly: true);
                _ = updateCmd.ExecuteNonQuery();
                return true;
            }

            var providedColumns = new List<string> { employeeIdColumn, dateColumn, checkInColumn };
            if (!AttendanceSchemaHelper.CanInsertWithColumns(conn, detailedTable, providedColumns, tran))
            {
                return true;
            }

            var insertQuery = $@"
                INSERT INTO [{detailedTable}]([{employeeIdColumn}], [{dateColumn}], [{checkInColumn}])
                VALUES (@MaNV, @Ngay, @GioVao)";

            using var insertCmd = new SqlCommand(insertQuery, conn, tran);
            insertCmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;
            AddColumnValue(insertCmd, "@Ngay", detailedTable, dateColumn, conn, tran, thoiDiem, isDateOnly: true, isTimeOnly: false);
            AddColumnValue(insertCmd, "@GioVao", detailedTable, checkInColumn, conn, tran, thoiDiem, isDateOnly: false, isTimeOnly: true);
            _ = insertCmd.ExecuteNonQuery();
            return true;
        }

        private static bool HandleCheckOutOnDetailedAttendance(SqlConnection conn, SqlTransaction tran, string detailedTable, string maNV, DateTime thoiDiem, out bool hasDetailedRecord, out TimeSpan? gioVaoChiTiet, out string message)
        {
            message = string.Empty;
            hasDetailedRecord = false;
            gioVaoChiTiet = null;

            var employeeIdColumn = AttendanceSchemaHelper.ResolveColumn(conn, detailedTable, tran, "MaNV", "maNV");
            var dateColumn = AttendanceSchemaHelper.ResolveColumn(conn, detailedTable, tran, "Ngay", "ngay", "NgayChamCong");
            var checkInColumn = AttendanceSchemaHelper.ResolveColumn(conn, detailedTable, tran, "GioVao", "gioVao", "CheckIn");
            var checkOutColumn = AttendanceSchemaHelper.ResolveColumn(conn, detailedTable, tran, "GioRa", "gioRa", "CheckOut");

            if (string.IsNullOrWhiteSpace(employeeIdColumn) ||
                string.IsNullOrWhiteSpace(dateColumn) ||
                string.IsNullOrWhiteSpace(checkInColumn) ||
                string.IsNullOrWhiteSpace(checkOutColumn))
            {
                return true;
            }

            var query = $@"
                SELECT TOP 1
                    c.[{checkInColumn}] AS GioVao,
                    c.[{checkOutColumn}] AS GioRa
                FROM [{detailedTable}] c
                WHERE c.[{employeeIdColumn}] = @MaNV
                  AND CAST(c.[{dateColumn}] AS date) = @Ngay
                ORDER BY c.[{dateColumn}] DESC";

            using var cmd = new SqlCommand(query, conn, tran);
            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;
            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = thoiDiem.Date;

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                reader.Close();
                return true;
            }

            hasDetailedRecord = true;
            var gioVao = ToNullableTimeSpan(reader["GioVao"]);
            var gioRa = ToNullableTimeSpan(reader["GioRa"]);
            gioVaoChiTiet = gioVao;
            reader.Close();

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

            var updateQuery = $@"
                UPDATE [{detailedTable}]
                SET [{checkOutColumn}] = @GioRa
                WHERE [{employeeIdColumn}] = @MaNV
                  AND CAST([{dateColumn}] AS date) = @Ngay";

            using var updateCmd = new SqlCommand(updateQuery, conn, tran);
            updateCmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;
            updateCmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = thoiDiem.Date;
            AddColumnValue(updateCmd, "@GioRa", detailedTable, checkOutColumn, conn, tran, thoiDiem, isDateOnly: false, isTimeOnly: true);
            _ = updateCmd.ExecuteNonQuery();
            return true;
        }

        private static void UpdateEmployeeAttendanceSnapshot(SqlConnection conn, SqlTransaction tran, string employeeTable, string employeeIdColumn, string maNV, DateTime gioVao, DateTime? gioRa, bool clearCheckOut = false)
        {
            var dateColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, tran, "NgayChamCong");
            var checkInColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, tran, "GioVao", "gioVao");
            var checkOutColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, tran, "GioRa", "gioRa");

            var setClauses = new List<string>();
            using var cmd = new SqlCommand
            {
                Connection = conn,
                Transaction = tran
            };

            if (!string.IsNullOrWhiteSpace(dateColumn))
            {
                setClauses.Add($"[{dateColumn}] = @NgayChamCong");
                AddColumnValue(cmd, "@NgayChamCong", employeeTable, dateColumn, conn, tran, gioVao, isDateOnly: true, isTimeOnly: false);
            }

            if (!string.IsNullOrWhiteSpace(checkInColumn))
            {
                setClauses.Add($"[{checkInColumn}] = @GioVao");
                AddColumnValue(cmd, "@GioVao", employeeTable, checkInColumn, conn, tran, gioVao, isDateOnly: false, isTimeOnly: true);
            }

            if (!string.IsNullOrWhiteSpace(checkOutColumn))
            {
                setClauses.Add($"[{checkOutColumn}] = @GioRa");
                if (gioRa.HasValue)
                {
                    AddColumnValue(cmd, "@GioRa", employeeTable, checkOutColumn, conn, tran, gioRa.Value, isDateOnly: false, isTimeOnly: true);
                }
                else if (clearCheckOut)
                {
                    AddNullColumnValue(cmd, "@GioRa", employeeTable, checkOutColumn, conn, tran);
                }
                else
                {
                    setClauses.RemoveAt(setClauses.Count - 1);
                }
            }

            if (setClauses.Count == 0)
            {
                return;
            }

            cmd.CommandText = $@"
                UPDATE [{employeeTable}]
                SET {string.Join(", ", setClauses)}
                WHERE [{employeeIdColumn}] = @MaNV";
            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;
            _ = cmd.ExecuteNonQuery();
        }

        private static void UpdateMonthlyWorkedDays(SqlConnection conn, SqlTransaction tran, string maNV, DateTime thoiDiem)
        {
            var summaryTable = AttendanceSchemaHelper.ResolveMonthlyAttendanceTable(conn, tran);
            if (string.IsNullOrWhiteSpace(summaryTable))
            {
                return;
            }

            var employeeIdColumn = AttendanceSchemaHelper.ResolveColumn(conn, summaryTable, tran, "MaNV", "maNV");
            var monthColumn = AttendanceSchemaHelper.ResolveColumn(conn, summaryTable, tran, "ThangNam", "Thang", "KyLuong");
            var workDayColumn = AttendanceSchemaHelper.ResolveColumn(conn, summaryTable, tran, "SoNgayLam", "NgayCong", "TongNgayLam");

            if (string.IsNullOrWhiteSpace(employeeIdColumn) || string.IsNullOrWhiteSpace(monthColumn) || string.IsNullOrWhiteSpace(workDayColumn))
            {
                return;
            }

            var thangNam = thoiDiem.ToString("MM/yyyy", CultureInfo.InvariantCulture);

            var checkQuery = $@"
                SELECT TOP 1 [{workDayColumn}]
                FROM [{summaryTable}]
                WHERE [{employeeIdColumn}] = @MaNV
                  AND [{monthColumn}] = @ThangNam";

            using var checkCmd = new SqlCommand(checkQuery, conn, tran);
            checkCmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;
            checkCmd.Parameters.Add("@ThangNam", SqlDbType.NVarChar, 20).Value = thangNam;
            var existingValue = checkCmd.ExecuteScalar();

            if (existingValue != null)
            {
                var updateQuery = $@"
                    UPDATE [{summaryTable}]
                    SET [{workDayColumn}] = ISNULL([{workDayColumn}], 0) + 1
                    WHERE [{employeeIdColumn}] = @MaNV
                      AND [{monthColumn}] = @ThangNam";

                using var updateCmd = new SqlCommand(updateQuery, conn, tran);
                updateCmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;
                updateCmd.Parameters.Add("@ThangNam", SqlDbType.NVarChar, 20).Value = thangNam;
                _ = updateCmd.ExecuteNonQuery();
                return;
            }

            var providedColumns = new[] { employeeIdColumn, monthColumn, workDayColumn };
            if (!AttendanceSchemaHelper.CanInsertWithColumns(conn, summaryTable, providedColumns, tran))
            {
                return;
            }

            var insertQuery = $@"
                INSERT INTO [{summaryTable}]([{employeeIdColumn}], [{monthColumn}], [{workDayColumn}])
                VALUES (@MaNV, @ThangNam, @SoNgayLam)";

            using var insertCmd = new SqlCommand(insertQuery, conn, tran);
            insertCmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;
            insertCmd.Parameters.Add("@ThangNam", SqlDbType.NVarChar, 20).Value = thangNam;
            insertCmd.Parameters.Add("@SoNgayLam", SqlDbType.Int).Value = 1;
            _ = insertCmd.ExecuteNonQuery();
        }

        private static EmployeeAttendanceState LoadEmployeeAttendanceState(SqlConnection conn, SqlTransaction tran, string employeeTable, string employeeIdColumn, string maNV)
        {
            var dateColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, tran, "NgayChamCong");
            var checkInColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, tran, "GioVao", "gioVao");
            var checkOutColumn = AttendanceSchemaHelper.ResolveColumn(conn, employeeTable, tran, "GioRa", "gioRa");

            if (string.IsNullOrWhiteSpace(dateColumn) && string.IsNullOrWhiteSpace(checkInColumn) && string.IsNullOrWhiteSpace(checkOutColumn))
            {
                return new EmployeeAttendanceState();
            }

            var dateExpression = string.IsNullOrWhiteSpace(dateColumn) ? "CAST(NULL AS date)" : $"n.[{dateColumn}]";
            var checkInExpression = string.IsNullOrWhiteSpace(checkInColumn) ? "CAST(NULL AS time)" : $"n.[{checkInColumn}]";
            var checkOutExpression = string.IsNullOrWhiteSpace(checkOutColumn) ? "CAST(NULL AS time)" : $"n.[{checkOutColumn}]";

            var query = $@"
                SELECT TOP 1
                    {dateExpression} AS NgayChamCong,
                    {checkInExpression} AS GioVao,
                    {checkOutExpression} AS GioRa
                FROM [{employeeTable}] n
                WHERE n.[{employeeIdColumn}] = @MaNV";

            using var cmd = new SqlCommand(query, conn, tran);
            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return new EmployeeAttendanceState();
            }

            return new EmployeeAttendanceState
            {
                NgayChamCong = ToNullableDateTime(reader["NgayChamCong"]),
                GioVao = ToNullableTimeSpan(reader["GioVao"]),
                GioRa = ToNullableTimeSpan(reader["GioRa"])
            };
        }

        private static bool EmployeeExists(SqlConnection conn, SqlTransaction tran, string employeeTable, string employeeIdColumn, string maNV)
        {
            var query = $@"
                SELECT COUNT(1)
                FROM [{employeeTable}]
                WHERE [{employeeIdColumn}] = @MaNV";

            using var cmd = new SqlCommand(query, conn, tran);
            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = maNV;
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private static void AddColumnValue(SqlCommand cmd, string parameterName, string tableName, string columnName, SqlConnection conn, SqlTransaction tran, DateTime value, bool isDateOnly, bool isTimeOnly)
        {
            var dataTypeName = AttendanceSchemaHelper.GetColumnDataType(conn, tableName, columnName, tran);
            var parameter = cmd.Parameters.Add(parameterName, MapSqlDbType(dataTypeName));
            parameter.Value = ConvertDateTimeValue(value, dataTypeName, isDateOnly, isTimeOnly);
        }

        private static void AddNullColumnValue(SqlCommand cmd, string parameterName, string tableName, string columnName, SqlConnection conn, SqlTransaction tran)
        {
            var dataTypeName = AttendanceSchemaHelper.GetColumnDataType(conn, tableName, columnName, tran);
            var parameter = cmd.Parameters.Add(parameterName, MapSqlDbType(dataTypeName));
            parameter.Value = DBNull.Value;
        }

        private static object ConvertDateTimeValue(DateTime value, string? dataTypeName, bool isDateOnly, bool isTimeOnly)
        {
            var normalizedType = (dataTypeName ?? string.Empty).ToLowerInvariant();

            return normalizedType switch
            {
                "date" => value.Date,
                "datetime" or "datetime2" or "smalldatetime" => isDateOnly ? value.Date : value,
                "time" => value.TimeOfDay,
                "char" or "nchar" or "varchar" or "nvarchar" => isTimeOnly
                    ? value.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
                    : isDateOnly
                        ? value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        : value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                _ => isTimeOnly ? value.TimeOfDay : value
            };
        }

        private static SqlDbType MapSqlDbType(string? dataTypeName)
        {
            return (dataTypeName ?? string.Empty).ToLowerInvariant() switch
            {
                "char" => SqlDbType.Char,
                "nchar" => SqlDbType.NChar,
                "varchar" => SqlDbType.VarChar,
                "nvarchar" => SqlDbType.NVarChar,
                "date" => SqlDbType.Date,
                "datetime" => SqlDbType.DateTime,
                "datetime2" => SqlDbType.DateTime2,
                "smalldatetime" => SqlDbType.SmallDateTime,
                "time" => SqlDbType.Time,
                "int" => SqlDbType.Int,
                "bigint" => SqlDbType.BigInt,
                "bit" => SqlDbType.Bit,
                "decimal" => SqlDbType.Decimal,
                "numeric" => SqlDbType.Decimal,
                "float" => SqlDbType.Float,
                "real" => SqlDbType.Real,
                _ => SqlDbType.NVarChar
            };
        }

        private static DateTime? ToNullableDateTime(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            if (value is DateTime dateTime)
            {
                return dateTime;
            }

            return DateTime.TryParse(value.ToString(), out var parsed) ? parsed : null;
        }

        private static TimeSpan? ToNullableTimeSpan(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            if (value is TimeSpan timeSpan)
            {
                return timeSpan;
            }

            if (value is DateTime dateTime)
            {
                return dateTime.TimeOfDay;
            }

            return TimeSpan.TryParse(value.ToString(), out var parsed) ? parsed : null;
        }

        private sealed class EmployeeAttendanceState
        {
            public DateTime? NgayChamCong { get; init; }
            public TimeSpan? GioVao { get; init; }
            public TimeSpan? GioRa { get; init; }
        }
    }
}
