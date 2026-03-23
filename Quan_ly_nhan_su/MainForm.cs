using System;
using System.Windows.Forms;
using Quan_ly_nhan_su.GUI;

namespace Quan_ly_nhan_su
{
    public partial class MainForm : Form
    {
<<<<<<< HEAD
        private readonly string _quyen;
        private bool _isLoggingOut;
=======
        
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
        private readonly ucNhanVien _ucNhanVien = new();
        private readonly ucBangLuong _ucBangLuong = new();
        private readonly ucQuanLyCong _ucQuanLyCong = new();

        public MainForm()
        {
            InitializeComponent();
<<<<<<< HEAD
            _quyen = quyen;
            ThietLapPhanQuyen();
            OpenControl(_ucNhanVien);
=======

          
        
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
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

<<<<<<< HEAD
        public void ThietLapPhanQuyen()
        {
            if (_quyen == "User")
            {
                btnQLNhanSu.Visible = false;
                btnTienLuong.Visible = false;
                btnChamCong.Visible = false;
                lblVaiTro.Text = "Vai trò: Nhân viên";
            }
            else if (_quyen == "Admin")
            {
                btnQLNhanSu.Visible = true;
                btnTienLuong.Visible = true;
                btnChamCong.Visible = true;
                lblVaiTro.Text = "Vai trò: Quản lý";
            }
        }
=======
        private void button2_Click(object? sender, EventArgs e)
        {
            OpenControl(_ucNhanVien);
        }

        private void lblLogo_Click(object? sender, EventArgs e)
        {
            OpenControl(_ucNhanVien);

        }

     
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120

        private void pnlDesktop_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnTienLuong_Click_1(object sender, EventArgs e)
        {
            OpenControl(_ucBangLuong);
        }

        private void btnQLNhanSu_Click_1(object sender, EventArgs e)
        {
            OpenControl(_ucNhanVien);
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            OpenControl(_ucQuanLyCong);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rs != DialogResult.Yes)
            {
                return;
            }

            _isLoggingOut = true;

            Form? frmLogin = Application.OpenForms["frmDangNhap"];
            if (frmLogin != null)
            {
                frmLogin.Show();
            }
            else
            {
                new frmDangNhap().Show();
            }

            Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_isLoggingOut)
            {
                Application.Exit();
            }
        }
    }
}
