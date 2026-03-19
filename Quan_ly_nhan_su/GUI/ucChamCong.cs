using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Quan_ly_nhan_su.GUI
{
    public partial class ucChamCong : UserControl
    {
        public ucChamCong()
        {
            InitializeComponent();



        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }


        private void LoadDanhSachChamCong()
        {
            flpDanhSachChamCong.SuspendLayout();
            flpDanhSachChamCong.Controls.Clear();
            TaoTheChamCong("23", "Thứ 2, 23/10/2024", "8h 10m", "08:00 - 17:00");
            TaoTheChamCong("20", "Thứ 2, 23/10/2024", "8h 10m", "08:00 - 17:00");
            TaoTheChamCong("19", "Thứ 2, 23/10/2024", "8h 10m", "08:00 - 17:00");
            flpDanhSachChamCong.ResumeLayout();
        }
        private void TaoTheChamCong(string ngay, string dmy, string tonggio, string thoigian)
        {
            ucItemChamCong item = new ucItemChamCong();
            item.SetDataChamCong(ngay, dmy, tonggio, thoigian);
            item.Width = flpDanhSachChamCong.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 5;
            flpDanhSachChamCong.Controls.Add(item);
        }

        private void flpDanhSachChamCong_Paint(object sender, PaintEventArgs e)
        {
            foreach (Control ctrl in flpDanhSachChamCong.Controls)
            {

                ctrl.Width = flpDanhSachChamCong.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 5;
            }
        }

        private void ucChamCong_Load_1(object sender, EventArgs e)
        {
            LoadDanhSachChamCong();
            flpDanhSachChamCong.SizeChanged += (s, ev) =>
            {
                flpDanhSachChamCong.SuspendLayout();
                foreach (Control item in flpDanhSachChamCong.Controls)
                {
                    item.Width = flpDanhSachChamCong.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 5;
                }
                flpDanhSachChamCong.ResumeLayout();
            };
        }

        private void ucChamCong_Load(object sender, EventArgs e)
        {

        }
    }
}
