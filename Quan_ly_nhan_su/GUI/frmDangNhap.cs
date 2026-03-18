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
            string taikhoan = txtTaiKhoan.Text;
            string matkhau = txtMatKhau.Text;

            string quyen = bus.DangNhap(taikhoan, matkhau);
            if (quyen != null)
            {
                MainForm mainForm = new MainForm(quyen);
                mainForm.Show();
                this.Hide();
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {


        }
    }
}
