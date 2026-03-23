using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Quan_ly_nhan_su.DAL
{
    internal class QuanLyCongDAL
    {


        public DataTable LayDanhSachLichSuCong(string tuKhoa, DateTime? tuNgay, DateTime? denNgay)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = DbContext.GetSqlConnection())
            {
                // Sửa lại cú pháp chuỗi @"..." và bổ sung tính TongGio
                string query = @"
                        SELECT c.maNV, n.hoTen, c.ngay, c.gioVao, c.gioRa, 
                        CASE 
                            WHEN c.gioVao IS NOT NULL AND c.gioRa IS NOT NULL 
                            THEN CAST(DATEDIFF(MINUTE, c.gioVao, c.gioRa) / 60 AS VARCHAR) + 'h ' + 
                                 CAST(DATEDIFF(MINUTE, c.gioVao, c.gioRa) % 60 AS VARCHAR) + 'm'
                            ELSE '--'
                        END AS TongGio,
                        c.trangThai
                        FROM ChamCong c
                        INNER JOIN NhanVien n ON c.MaNV = n.MaNV
                        WHERE 1 = 1";

                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    query += " AND (c.maNV LIKE @tuKhoa OR n.HoTen LIKE @tuKhoa)";
                }

                if (tuNgay.HasValue && denNgay.HasValue)
                {
                    query += " AND c.Ngay >= @tuNgay AND c.Ngay <= @denNgay";
                }

                // Sap xep: Ngay moi len dau, nguoi den som len dau
                query += " ORDER BY c.Ngay DESC, c.GioVao ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Truyền giá trị thật vào các biến @tuKhoa, @tuNgay, @denNgay
                    if (!string.IsNullOrEmpty(tuKhoa))
                    {
                        cmd.Parameters.AddWithValue("@tuKhoa", "%" + tuKhoa.Trim() + "%");
                    }

                    if (tuNgay.HasValue && denNgay.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@tuNgay", tuNgay.Value.Date);
                        cmd.Parameters.AddWithValue("@denNgay", denNgay.Value.Date.AddDays(1).AddSeconds(-1));
                    }

                    // Mở kết nối và thực thi lấy dữ liệu
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lỗi Database: " + ex.Message);
                    }
                }
            }
            return dt; // Trả bảng dữ liệu về cho tầng BUS
        }
    }
}