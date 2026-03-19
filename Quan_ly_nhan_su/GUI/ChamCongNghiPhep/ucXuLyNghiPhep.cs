using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Quan_ly_nhan_su.GUI.ChamCongNghiPhep
{
    public partial class ucXuLyNghiPhep : UserControl
    {
        private List<YeuCauNghiPhep> _danhSachGoc = new List<YeuCauNghiPhep>();
        public ucXuLyNghiPhep()
        {
            InitializeComponent();
            this.Load += ucChamCong_Load;
        }



        private void ucChamCong_Load(object sender, EventArgs e)
        {
            LoadDanhSachYeuCau();
            HienThiDanhSach(_danhSachGoc);
            flpDanhSach.SizeChanged += (s, ev) =>
                {
                    flpDanhSach.SuspendLayout();
                    foreach (Control item in flpDanhSach.Controls)
                    {

                        item.Width = flpDanhSach.ClientSize.Width - 15;
                    }
                    flpDanhSach.ResumeLayout();
                };
        }
        private void LoadDanhSachYeuCau()
        {
            _danhSachGoc = new List<YeuCauNghiPhep>
            {
                new YeuCauNghiPhep { MaYeuCau = 1, TenNhanVien = "Nguyễn Văn An", PhongBan = "Phòng kỹ thuật • NV-1024", LoaiNghi = "Nghỉ phép năm", ThoiGian = "20/10 - 22/10 (3 ngày)", LyDo = "Giải quyết việc gia đình cá nhân.", TrangThai = "Chờ duyệt" },
                new YeuCauNghiPhep { MaYeuCau = 2, TenNhanVien = "Trần Thị Mai", PhongBan = "Phòng Marketing • NV-0982", LoaiNghi = "Nghỉ ốm", ThoiGian = "18/10 (1 ngày)", LyDo = "Bị sốt xuất huyết, có giấy xác nhận của bác sĩ.", TrangThai = "Chờ duyệt" },
                new YeuCauNghiPhep { MaYeuCau = 3, TenNhanVien = "Lê Hoàng Nam", PhongBan = "Phòng kinh doanh • NV-1150", LoaiNghi = "Nghỉ không lương", ThoiGian = "25/10 - 26/10 (2 ngày)", LyDo = "Đi khám sức khỏe định kỳ cho bố mẹ.", TrangThai = "Chờ duyệt" }
            };


        }
        private void HienThiDanhSach(List<YeuCauNghiPhep> danhSach)
        {
            flpDanhSach.SuspendLayout();
            flpDanhSach.Controls.Clear();
            foreach (var yeuCau in danhSach)
            {
                TaoTheYeuCau(yeuCau.MaYeuCau, yeuCau.TenNhanVien, yeuCau.PhongBan, yeuCau.LoaiNghi, yeuCau.ThoiGian, yeuCau.LyDo, yeuCau.TrangThai);
            }
            flpDanhSach.ResumeLayout();
        }

        private void TaoTheYeuCau(int maYeuCau, string ten, string phongBan, string loaiNghi,
                                    string thoiGian, string lyDo, string trangThai)
        {
            ucItemYeuCau1 item = new ucItemYeuCau1();
            item.SetData(maYeuCau, ten, phongBan, loaiNghi, thoiGian, lyDo, trangThai);
            item.Width = flpDanhSach.Width - 10;
            item.TrangThaiThayDoi += Item_TrangThaiDaThayDoi;
            flpDanhSach.Controls.Add(item);
        }
        private void Item_TrangThaiDaThayDoi(object sender, EventArgs e)
        {
            LoadDanhSachYeuCau();
            // Khi trạng thái của một yêu cầu thay đổi, ta sẽ làm mới lại danh sách để cập nhật giao diện
            btnLamMoi.PerformClick();

        }

        private void flpDanhSach_Paint(object sender, PaintEventArgs e)
        {
            foreach (Control ctrl in flpDanhSach.Controls)
            {
                ctrl.Width = flpDanhSach.ClientSize.Width - 5;
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            var query = _danhSachGoc.AsEnumerable();
            string tuKhoa = tbTimKiem.Text.Trim().ToLower();
            string phongBan = cbPhongBan.Text;
            string trangThai = cbTrangThai.Text;
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(x => x.TenNhanVien.ToLower().Contains(tuKhoa) || x.PhongBan.ToLower().Contains(tuKhoa));
            }
            if (!string.IsNullOrEmpty(phongBan) &&  phongBan != "Tất cả phòng ban")
            {
                query = query.Where(x => x.PhongBan.ToLower().Contains(phongBan.ToLower()));
            }
            if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả trạng thái")
            {
                query = query.Where(x => x.TrangThai.ToLower() == trangThai.ToLower());
            }
           
            HienThiDanhSach(query.ToList());

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbTimKiem_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tbTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                e.SuppressKeyPress = true;
                string key = tbTimKiem.Text.Trim().ToLower();
                var filter = _danhSachGoc.Where(x => x.TenNhanVien.ToLower().Contains(key) || x.PhongBan.ToLower().Contains(key)).ToList();
                HienThiDanhSach(filter);
            }
        }
    }
    public class YeuCauNghiPhep
    {
        public int MaYeuCau { get; set; }
        public string TenNhanVien { get; set; }
        public string PhongBan { get; set; }
        public string LoaiNghi { get; set; }
        public string ThoiGian { get; set; }
        public string LyDo { get; set; }
        public string TrangThai { get; set; }
       


    }
}
