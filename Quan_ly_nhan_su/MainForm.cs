
using System;
using System.Windows.Forms;
using Quan_ly_nhan_su.GUI.ChamCongNghiPhep;

using Quan_ly_nhan_su.GUI; 


namespace Quan_ly_nhan_su
{
    public partial class MainForm : System.Windows.Forms.Form
    {
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

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pnlDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNghiPhep_Click(object sender, EventArgs e)
        {

            pnlDesktop.SuspendLayout();
            pnlDesktop.Controls.Clear();
            if(this.quyen == "Admin")
            {
                ucXuLyNghiPhep uc = new ucXuLyNghiPhep();
                uc.Dock = DockStyle.Fill;
                pnlDesktop.Controls.Add(uc);
            }
            else if(this.quyen == "User")
            {
                ucTaoDonNghiPhep uc = new ucTaoDonNghiPhep();
                uc.Dock = DockStyle.Fill;
                pnlDesktop.Controls.Add(uc);
            }
            //ucChamCong uc = new ucChamCong();
            //ucItemYeuCau uc = new ucItemYeuCau();
            //ucTaoDonNghiPhep uc = new ucTaoDonNghiPhep();
            //uc.Dock = DockStyle.Fill;
            //pnlDesktop.Controls.Add(uc);
            //uc.loadDanhSachNghiPhep();
            pnlDesktop.ResumeLayout();
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



        // Add event handlers or methods here as needed

    }
}