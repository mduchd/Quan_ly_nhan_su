using System;
using System.Windows.Forms;
using Quan_ly_nhan_su.GUI;
using Quan_ly_nhan_su.GUI.ChamCongNghiPhep;

namespace Quan_ly_nhan_su
{
    public partial class Form : System.Windows.Forms.Form
    {
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
    }
}