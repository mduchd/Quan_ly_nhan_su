using Quan_ly_nhan_su.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Quan_ly_nhan_su;
namespace Quan_ly_nhan_su.GUI
{
    public partial class frmDangNhap : System.Windows.Forms.Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            TaiKhoanBUS bus = new TaiKhoanBUS();
            string taikhoan = txtTaiKhoan.Text.Trim();
            string matkhau = txtMatKhau.Text;

            if (string.IsNullOrWhiteSpace(taikhoan) || string.IsNullOrWhiteSpace(matkhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTaiKhoan.Focus();
                return;
            }

            try
            {
                string quyen = bus.DangNhap(taikhoan, matkhau);
                if (!string.IsNullOrWhiteSpace(quyen))
                {
                    MainForm mainForm = new MainForm(quyen);
                    mainForm.Show();
                    this.Hide();
                    return;
                }

                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Clear();
                txtMatKhau.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {


        }

        private void btnMoMayChamCong_Click(object sender, EventArgs e)
        {
            frmChamCong chamCongForm = new frmChamCong();
            chamCongForm.Show();

            this.Hide();
        }
    }
}
