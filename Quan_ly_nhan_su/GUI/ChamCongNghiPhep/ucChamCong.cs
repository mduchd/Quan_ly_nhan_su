using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Quan_ly_nhan_su.GUI.ChamCongNghiPhep
{
    public partial class ucChamCong : UserControl
    {
        public ucChamCong()
        {
            InitializeComponent();
            this.Load += UcChamCong_Load;
        }

        private void UcChamCong_Load(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ucChamCong_Load(object sender, EventArgs e)
        {
            LoadDanhSachYeuCau();
            flpDanhSach.SizeChanged += (s, ev) =>
                {
                    flpDanhSach.SuspendLayout();
                    foreach (Control item in flpDanhSach.Controls)
                    {
                        // Ép tất cả các thẻ con phải rộng bằng chiều ngang thực tế của khung (trừ hao 15px thanh cuộn)
                        item.Width = flpDanhSach.ClientSize.Width - 15;
                    }
                    flpDanhSach.ResumeLayout();
                };
        }
        private void LoadDanhSachYeuCau()
        {
            flpDanhSach.SuspendLayout();
            flpDanhSach.Controls.Clear();
            TaoTheYeuCau("Nguyễn Văn An", "Phòng Kỹ thuật • NV-1024", "Nghỉ phép năm", "20/10 - 22/10 (3 ngày)", "Giải quyết việc gia đình cá nhân.");
            TaoTheYeuCau("Trần Thị Mai", "Phòng Marketing • NV-0982", "Nghỉ ốm", "18/10 (1 ngày)", "Bị sốt xuất huyết, có giấy xác nhận của bác sĩ.");
            TaoTheYeuCau("Lê Hoàng Nam", "Phòng Kinh doanh • NV-1150", "Nghỉ không lương", "25/10 - 26/10 (2 ngày)", "Đi khám sức khỏe định kỳ cho bố mẹ.");
            flpDanhSach.ResumeLayout();


        }
        private void TaoTheYeuCau(string ten, string phongBan, string loaiNghi, 
                                    string thowiGian, string lyDo)
        {
            ucItemYeuCau1 item = new ucItemYeuCau1();
            item.SetData(ten, phongBan, loaiNghi, thowiGian, lyDo);
            item.Width = flpDanhSach.Width - 25; // Điều chỉnh chiều rộng của item
            flpDanhSach.Controls.Add(item);
        }
    }
}
