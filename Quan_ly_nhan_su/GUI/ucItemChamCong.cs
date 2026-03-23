using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Quan_ly_nhan_su.DAL;

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

        private void lblNgay_Click(object sender, EventArgs e)
        {
            
        }
        public void LichSuNgay(int ngay, int thang, int nam, string thu, TimeSpan giovao, TimeSpan giora)
        {
            
            
            lblNgay.Text = ngay.ToString();
            lblDmy.Text = $"{thu}, {ngay} tháng {thang}";
            lblThoiGian.Text = $"{giovao.ToString(@"hh\:mm")} - {giora.ToString(@"hh\:mm")}";
            TimeSpan tongGio = giora - giovao;
            int sogio = tongGio.Hours;
            int sophut = tongGio.Minutes;
            lblTongThoiGian.Text = $"{sogio}h {sophut:D2}m";

        }
    }
}
