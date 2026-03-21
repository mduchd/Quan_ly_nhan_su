
using System;
using System.Windows.Forms;
using Quan_ly_nhan_su.GUI;

using Quan_ly_nhan_su.GUI; 

namespace Quan_ly_nhan_su
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private string quyen;
        private readonly ucNhanVien _ucNhanVien = new();

        public MainForm(string quyen)
        {
            InitializeComponent();

            this.quyen = quyen;
            ThietLapPhanQuyen();
        }

        private void OpenControl(UserControl control)
        {
            if (control.Parent == pnlDesktop)
            {
                control.BringToFront();
                return;
            }

            control.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(control);
            control.BringToFront();
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            OpenControl(_ucNhanVien);
        }

        private void lblLogo_Click(object? sender, EventArgs e)
        {
            OpenControl(_ucNhanVien);

        }

        public void ThietLapPhanQuyen()
        {
            if (this.quyen == "User")
            {
                btnQLNhanSu.Visible = false;
                btnTienLuong.Visible = false;

                lblVaiTro.Text = "Vai trò: Nhân viên";
            }
            else if (this.quyen == "Admin")
            {
                btnQLNhanSu.Visible = true;
                btnTienLuong.Visible = true;
                btnChamCong.Visible = true;

                lblVaiTro.Text = "Vai trò: Quản lý";
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
           


        }

        private void pnlDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNghiPhep_Click(object sender, EventArgs e)
        {

            
        }


        private void btnTienLuong_Click(object sender, EventArgs e)
        {
            pnlDesktop.Controls.Clear();
            ucBangLuong uc = new ucBangLuong();
            uc.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(uc);

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không ?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rs == DialogResult.Yes)
            {
                Form frmLogin = Application.OpenForms["frmDangNhap"];
                if (frmLogin != null)
                {
                    frmLogin.Show();
                }
                else
                {
                    new frmDangNhap().Show();
                }
                this.Close();
            }
        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void btnNghiPhep_Click_1(object sender, EventArgs e)
        {
       
            //uc.loadDanhSachNghiPhep();
        }

        private void btnTienLuong_Click_1(object sender, EventArgs e)
        {
            pnlDesktop.Controls.Clear();
            ucBangLuong uc = new ucBangLuong();
            uc.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(uc);
        }


        private void btnQLNhanSu_Click_1(object sender, EventArgs e)
        {
            pnlDesktop.Controls.Clear();
            ucNhanVien uc = new ucNhanVien();
            uc.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(uc);
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            pnlDesktop.Controls.Clear();
            ucQuanLyCong uc = new ucQuanLyCong();
            uc.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(uc);
        }
    }
}