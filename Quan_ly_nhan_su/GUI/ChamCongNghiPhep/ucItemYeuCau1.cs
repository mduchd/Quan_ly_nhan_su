using System.Windows.Forms;

namespace Quan_ly_nhan_su.GUI.ChamCongNghiPhep
{
    public partial class ucItemYeuCau1 : UserControl
    {
        public ucItemYeuCau1()
        {
            InitializeComponent();
        }

        // Hàm này để nhận dữ liệu từ form cha bơm vào
        public void SetData(string ten, string phongBan, string loaiNghi, string thoiGian, string lyDo)
        {
            lblTenNhanVien.Text = ten;
            lblPhongBan.Text = phongBan;
            lblLoaiNghi.Text = loaiNghi;
            lblThoiGian.Text = thoiGian;
            lblLyDo.Text = "\"" + lyDo + "\"";
        }
    }
}