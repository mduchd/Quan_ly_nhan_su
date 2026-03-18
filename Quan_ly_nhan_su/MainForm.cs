using System;
using System.Windows.Forms;
using Quan_ly_nhan_su.GUI;
using Quan_ly_nhan_su.GUI.ChamCongNghiPhep;

namespace Quan_ly_nhan_su
{
    public partial class MainForm : System.Windows.Forms.Form
    {
<<<<<<< HEAD
        private readonly ucNhanVien _ucNhanVien = new();
        private readonly ucBangLuong _ucBangLuong = new();
        private readonly ucChamCong _ucChamCong = new();
        private readonly ucTaoDonNghiPhep _ucTaoDonNghiPhep = new();

        public Form()
        {
            InitializeComponent();

            btnQLNhanSu.Click += button2_Click;

            OpenControl(_ucNhanVien);
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
=======
        private string quyen;
        public MainForm(string quyen)
        {
            InitializeComponent();
            this.quyen = quyen;
            ThietLapPhanQuyen();
            ucBangLuong bangLuong = new ucBangLuong();
            bangLuong.Dock = DockStyle.Fill;
            this.Controls.Add(bangLuong);
            this.Text = "Phần mềm Quản lý Nhân sự - Phân hệ Tiền Lương";
            this.Size = new System.Drawing.Size(850, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
>>>>>>> aa205d7053074df71b7d9a0ba3ae7bef943a8772
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
                btnQLNhanSu.Visible = false;
                btnTienLuong.Visible = true;
                btnChamCong.Visible = true;
                btnNghiPhep.Visible = true;

                lblVaiTro.Text = "Vai trò: Quản lý";
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {
            lblLogo_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenControl(_ucChamCong);
        }

        private void pnlDesktop_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnNghiPhep_Click(object sender, EventArgs e)
        {
            OpenControl(_ucTaoDonNghiPhep);
        }


        private void btnTienLuong_Click(object sender, EventArgs e)
        {
            OpenControl(_ucBangLuong);
        }
<<<<<<< HEAD
=======

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
                } else
                {
                    new frmDangNhap().Show();
                }
                this.Close();
            }
        }



        // Add event handlers or methods here as needed

>>>>>>> aa205d7053074df71b7d9a0ba3ae7bef943a8772
    }
}