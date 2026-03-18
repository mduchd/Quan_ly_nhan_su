using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Quan_ly_nhan_su.GUI.ChamCongNghiPhep
{
    public partial class ucNghiPhep : UserControl
    {
        public ucNghiPhep()
        {
            InitializeComponent();
        }
        public void SetData(string ten, string loaiNghi, string thoiGian, string lyDo, string phongBan)
        {
            lblTen.Text = ten;
            lblLoaiNghi.Text = loaiNghi;
            lblThoiGian.Text = thoiGian;
            lblLyDo.Text = lyDo;
            lblPhongBan.Text = phongBan;
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
