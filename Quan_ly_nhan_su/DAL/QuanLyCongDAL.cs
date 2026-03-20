using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Quan_ly_nhan_su.DAL
{
    internal class QuanLyCongDAL
    {
        private string connectionString = "Data Source=DESKTOP-9K5QG8P;Initial Catalog=QuanLyNhanSu;Integrated Security=True";

        public DataTable LayDanhSachLichSuCong(string tuKHoa, DateTime? tuNgay, DateTime? denNgay)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConection(connectionString)) {
                string query = "@ 
                        SELECT c.maNV, n.hoTen, c.ngay, c.gioVao, c.gioRa, c.trangThai
                        FROM CHAM_CONG c
                        INNER JOIN NHAN_VIEN n ON c.MaNV = n.MaNV
                        WHERE 1 = 1";
                    "";

                if (!string.IsNullOrEmpty(thKhoa))
                {
                    query += " AND (c.maNV LIKE @tukhoa OR n.HoTen LIKE @tukhoa)";
                }

                if (tuNgay.HasValue && denNgay.HasValue)
                {
                    query += " AND c.Ngay >= @tuNgay AND c.Ngay <= @denNgay";
                }

                // Sap xep: Ngay moi len dau, nguoi den som len dau
                query += " ORDER BY c.Ngay DESC, c.GioVao ASC";

                using (SQlCommand cmd = new SqlCommand(query, conn))
                {

                }
        }
    }
}
