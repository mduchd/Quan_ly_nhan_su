using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Quan_ly_nhan_su.GUI
{
    public partial class ucItemChamCong : UserControl
    {
        public ucItemChamCong()
        {
            InitializeComponent();
        }
        public void SetDataChamCong(string ngay, string dmy, string tonggio, string thoigian)
        {
            lblNgay.Text = ngay;
            lblDmy.Text = dmy;
            lblTongThoiGian.Text = tonggio;
            lblThoiGian.Text = thoigian;
        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
