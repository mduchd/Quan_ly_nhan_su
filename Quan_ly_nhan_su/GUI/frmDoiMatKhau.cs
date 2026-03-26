using System;
using System.Windows.Forms;
using Quan_ly_nhan_su.BUS;

namespace Quan_ly_nhan_su.GUI
{
    public partial class frmDoiMatKhau : Form
    {
        private string _tk;
        private TaiKhoanBUS bus = new TaiKhoanBUS();

        public frmDoiMatKhau(string taikhoan)
        {
            _tk = taikhoan;
            InitializeComponent();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string mkCu = txtMatKhauCu.Text;
            string mkMoi = txtMatKhauMoi.Text;
            string mkXacNhan = txtXacNhan.Text;

            string ketQua = bus.DoiMatKhau(_tk, mkCu, mkMoi, mkXacNhan);

            if (ketQua == "Thành công")
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
