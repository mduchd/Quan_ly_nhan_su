using System.Windows.Forms;

using Quan_ly_nhan_su.DTO;
using System.Data.SqlClient;
namespace Quan_ly_nhan_su.GUI.ChamCongNghiPhep
{
    public partial class ucItemYeuCau1 : UserControl
    {
        private int _maYeuCau;
        public event EventHandler TrangThaiThayDoi;
        public ucItemYeuCau1()
        {
            InitializeComponent();
        }

        // Hàm này để nhận dữ liệu từ form cha bơm vào
        public void SetData(int maYeuCau, string ten, string phongBan, string loaiNghi, string thoiGian, string lyDo, string trangThai)
        {
            _maYeuCau = maYeuCau;
            lblTenNhanVien.Text = ten;
            lblPhongBan.Text = phongBan;
            lblLoaiNghi.Text = loaiNghi;
            lblThoiGian.Text = thoiGian;
            lblLyDo.Text = "\"" + lyDo + "\"";

            btnTrangThai.Text = "● " + trangThai.ToUpper();
            if(trangThai == "Đã phê duyệt")
            {
                btnTrangThai.FillColor = Color.LightGreen;
                btnTrangThai.ForeColor = Color.DarkGreen;
            }
            else if(trangThai == "Đã từ chối")
            {
                btnTrangThai.FillColor = Color.MistyRose;
                btnTrangThai.ForeColor = Color.Red;
            }

        }

        private void btnPheDuyet_Click(object sender, EventArgs e)
        {
            if (_maYeuCau <= 0) return;
            string connectionString = @"Data Source=NgocDuy; Initial Catalog=Ql_Nhansu; Integrated Security = true";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Update YeuCauNghiPhep Set TrangThai = 'Đã phê duyệt' Where MaYeuCau = @MaYeuCau";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("MaYeuCau", _maYeuCau);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Đã phê duyệt đơn nghỉ phép", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TrangThaiThayDoi?.Invoke(this, EventArgs.Empty);
            }
            btnPheDuyet.Text = "Đã phê duyệt";
            ChamCongDTO yeuCau = new ChamCongDTO();
            yeuCau.TrangThai = "● ĐÃ PHÊ DUYỆT";
            btnTrangThai.FillColor = Color.LightGreen;
            btnTrangThai.ForeColor = Color.DarkGreen;
        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void lblLyDo_Click(object sender, EventArgs e)
        {
        }
        private void lblTenNhanVien_Click(object sender, EventArgs e)
        {
        }
        private void btnTrangThai_Click(object sender, EventArgs e)
        {

        }
    }
}