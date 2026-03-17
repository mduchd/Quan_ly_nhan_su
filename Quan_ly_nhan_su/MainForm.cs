
using System;
using System.Windows.Forms;
using Quan_ly_nhan_su.GUI.ChamCongNghiPhep;

namespace Quan_ly_nhan_su
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
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

        private void button4_Click(object sender, EventArgs e)
        {
            pnlDesktop.Controls.Clear();
            //ucChamCong uc = new ucChamCong();
            ucTaoDonNghiPhep uc = new ucTaoDonNghiPhep();
            uc.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(uc);
            //uc.loadDanhSachNghiPhep();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }



        // Add event handlers or methods here as needed

    }
}
