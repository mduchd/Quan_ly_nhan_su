using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Quan_ly_nhan_su.DAL
{
    internal static class AttendanceSchemaHelper
    {
        public static string? ResolveEmployeeTable(SqlConnection conn, SqlTransaction? tran = null)
        {
            return ResolveExistingTable(conn, tran, "NhanVien", "NHAN_VIEN");
        }

        public static string? ResolveDetailedAttendanceTable(SqlConnection conn, SqlTransaction? tran = null)
        {
            foreach (var tableName in new[] { "ChiTietChamCong", "CHAM_CONG", "ChamCong" })
            {
                if (!TableExists(conn, tableName, tran))
                {
                    continue;
                }

                var employeeIdColumn = ResolveColumn(conn, tableName, tran, "MaNV", "maNV");
                var dateColumn = ResolveColumn(conn, tableName, tran, "Ngay", "ngay", "NgayChamCong");
                if (!string.IsNullOrWhiteSpace(employeeIdColumn) && !string.IsNullOrWhiteSpace(dateColumn))
                {
                    return tableName;
                }
            }

            return null;
        }

        public static string? ResolveMonthlyAttendanceTable(SqlConnection conn, SqlTransaction? tran = null)
        {
            foreach (var tableName in new[] { "ChamCong", "CHAM_CONG" })
            {
                if (!TableExists(conn, tableName, tran))
                {
                    continue;
                }

                var employeeIdColumn = ResolveColumn(conn, tableName, tran, "MaNV", "maNV");
                var monthColumn = ResolveColumn(conn, tableName, tran, "ThangNam", "Thang", "KyLuong");
                var workDayColumn = ResolveColumn(conn, tableName, tran, "SoNgayLam", "NgayCong", "TongNgayLam");
                if (!string.IsNullOrWhiteSpace(employeeIdColumn) &&
                    !string.IsNullOrWhiteSpace(monthColumn) &&
                    !string.IsNullOrWhiteSpace(workDayColumn))
                {
                    return tableName;
                }
            }

            return null;
        }

        public static string? ResolveExistingTable(SqlConnection conn, SqlTransaction? tran = null, params string[] tableNames)
        {
            foreach (var tableName in tableNames)
            {
                if (TableExists(conn, tableName, tran))
                {
                    return tableName;
                }
            }

            return null;
        }

        public static bool TableExists(SqlConnection conn, string tableName, SqlTransaction? tran = null)
        {
            const string query = @"
                SELECT COUNT(1)
                FROM sys.tables
                WHERE name = @TableName";

            using var cmd = new SqlCommand(query, conn, tran);
            cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 128).Value = tableName;
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public static string? ResolveColumn(SqlConnection conn, string tableName, SqlTransaction? tran = null, params string[] candidateColumns)
        {
            foreach (var candidateColumn in candidateColumns)
            {
                const string query = @"
                    SELECT TOP 1 c.name
                    FROM sys.tables t
                    INNER JOIN sys.columns c ON t.object_id = c.object_id
                    WHERE t.name = @TableName
                      AND c.name = @ColumnName";

                using var cmd = new SqlCommand(query, conn, tran);
                cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 128).Value = tableName;
                cmd.Parameters.Add("@ColumnName", SqlDbType.NVarChar, 128).Value = candidateColumn;

                var columnName = cmd.ExecuteScalar()?.ToString();
                if (!string.IsNullOrWhiteSpace(columnName))
                {
                    return columnName;
                }
            }

            return null;
        }

        public static bool CanInsertWithColumns(SqlConnection conn, string tableName, IEnumerable<string> providedColumns, SqlTransaction? tran = null)
        {
            const string query = @"
                SELECT
                    c.name,
                    c.is_nullable,
                    c.is_identity,
                    c.is_computed,
                    CASE WHEN dc.object_id IS NULL THEN 0 ELSE 1 END AS has_default
                FROM sys.tables t
                INNER JOIN sys.columns c ON t.object_id = c.object_id
                LEFT JOIN sys.default_constraints dc ON c.default_object_id = dc.object_id
                WHERE t.name = @TableName";

            var provided = new HashSet<string>(providedColumns, StringComparer.OrdinalIgnoreCase);

            using var cmd = new SqlCommand(query, conn, tran);
            cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 128).Value = tableName;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var name = reader["name"]?.ToString() ?? string.Empty;
                var isNullable = reader["is_nullable"] != DBNull.Value && Convert.ToBoolean(reader["is_nullable"]);
                var isIdentity = reader["is_identity"] != DBNull.Value && Convert.ToBoolean(reader["is_identity"]);
                var isComputed = reader["is_computed"] != DBNull.Value && Convert.ToBoolean(reader["is_computed"]);
                var hasDefault = reader["has_default"] != DBNull.Value && Convert.ToBoolean(reader["has_default"]);

                if (isNullable || isIdentity || isComputed || hasDefault)
                {
                    continue;
                }

                if (!provided.Contains(name))
                {
                    return false;
                }
            }

            return true;
        }

        public static string? GetColumnDataType(SqlConnection conn, string tableName, string columnName, SqlTransaction? tran = null)
        {
            const string query = @"
                SELECT TOP 1 ty.name
                FROM sys.tables t
                INNER JOIN sys.columns c ON t.object_id = c.object_id
                INNER JOIN sys.types ty ON c.user_type_id = ty.user_type_id
                WHERE t.name = @TableName
                  AND c.name = @ColumnName";

            using var cmd = new SqlCommand(query, conn, tran);
            cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 128).Value = tableName;
            cmd.Parameters.Add("@ColumnName", SqlDbType.NVarChar, 128).Value = columnName;
            return cmd.ExecuteScalar()?.ToString();
        }
    }
}
