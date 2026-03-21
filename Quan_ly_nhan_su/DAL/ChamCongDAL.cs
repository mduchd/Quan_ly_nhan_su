using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Quan_ly_nhan_su.DTO;
using System.Drawing.Text;
// truy xất DB 
namespace Quan_ly_nhan_su.DAL
{
    internal class ChamCongDAL
    {
        private string connectionString = @"Data Source=NgocDuy; Initial Catalog=QL_Nhansu; Integrated Security=True";
        public DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable(); 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                conn.Close();
            }
            return data;
        }
        public bool ThemYeuCau(ChamCongDTO yeuCau)
        {
            string query = @"Insert into YeuCauNghiPhep(TenNhanVien, PhongBan, LoaiNghi, TuNgay, DenNgay, LyDo)
                            Values (@TenNhanVien, @PhongBan, @LoaiNghi, @TuNgay, @DenNgay, @LyDo)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {


                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenNhanVien", yeuCau.TenNhanVien);
                        cmd.Parameters.AddWithValue("@PhongBan", yeuCau.PhongBan);
                        cmd.Parameters.AddWithValue("@LoaiNghi", yeuCau.LoaiNghi);
                        cmd.Parameters.AddWithValue("@TuNgay", yeuCau.TuNgay);
                        cmd.Parameters.AddWithValue("@DenNgay", yeuCau.DenNgay);
                        cmd.Parameters.AddWithValue("@LyDo", yeuCau.LyDo);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch(Exception ex) {

                    MessageBox.Show("Lỗi chi tiết từ hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
