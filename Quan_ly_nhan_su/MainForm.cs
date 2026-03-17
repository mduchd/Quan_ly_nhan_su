
using System;
using System.Windows.Forms;
using Quan_ly_nhan_su.GUI.ChamCongNghiPhep;

using Quan_ly_nhan_su.GUI; 


namespace Quan_ly_nhan_su
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            ucBangLuong bangLuong = new ucBangLuong();
            bangLuong.Dock = DockStyle.Fill;
            this.Controls.Add(bangLuong);
            this.Text = "Phần mềm Quản lý Nhân sự - Phân hệ Tiền Lương";
            this.Size = new System.Drawing.Size(850, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
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
            pnlDesktop.Controls.Clear();
            //ucChamCong uc = new ucChamCong();
            ucTaoDonNghiPhep uc = new ucTaoDonNghiPhep();
            uc.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(uc);
            //uc.loadDanhSachNghiPhep();
        }


        private void btnTienLuong_Click(object sender, EventArgs e)
        {
            pnlDesktop.Controls.Clear();
            ucBangLuong uc = new ucBangLuong();
            uc.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(uc);

        }



        // Add event handlers or methods here as needed

    }
}