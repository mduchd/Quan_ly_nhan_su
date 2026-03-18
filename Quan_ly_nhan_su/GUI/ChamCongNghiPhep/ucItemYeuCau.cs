using System;
using System.Windows.Forms;

namespace Quan_ly_nhan_su.GUI.ChamCongNghiPhep
{
    public partial class ucItemYeuCau : UserControl
    {
        public ucItemYeuCau()
        {
            InitializeComponent();

            // Đăng ký sự kiện: Khi Form vừa load lên thì gọi hàm tạo danh sách
            this.Load += UcChamCong_Load;
        }

        private void UcChamCong_Load(object sender, EventArgs e)
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
            // 1. Nín thở - Tạm dừng vẽ để khỏi bị nháy màn hình
            flpDanhSach.SuspendLayout();
            flpDanhSach.Controls.Clear();

            // 2. Tạo dữ liệu giả (Mock Data) y hệt bản thiết kế
            TaoTheYeuCau("Nguyễn Văn An", "Phòng Kỹ thuật • NV-1024", "Nghỉ phép năm", "20/10 - 22/10 (3 ngày)", "Giải quyết việc gia đình cá nhân.");
            TaoTheYeuCau("Trần Thị Mai", "Phòng Marketing • NV-0982", "Nghỉ ốm", "18/10 (1 ngày)", "Bị sốt xuất huyết, có giấy xác nhận của bác sĩ.");
            TaoTheYeuCau("Lê Hoàng Nam", "Phòng Kinh doanh • NV-1150", "Nghỉ không lương", "25/10 - 26/10 (2 ngày)", "Đi khám sức khỏe định kỳ cho bố mẹ.");

            // 3. Thở ra - Cập nhật giao diện mượt mà
            flpDanhSach.ResumeLayout();
        }

        // Hàm hỗ trợ: Khởi tạo 1 thẻ và nhét vào khung
        private void TaoTheYeuCau(string ten, string phongBan, string loaiNghi, string thoiGian, string lyDo)
        {
            // Khởi tạo 1 thẻ UserControl mới
            ucItemYeuCau1 item = new ucItemYeuCau1();

            // Bơm dữ liệu vào thẻ
            item.SetData(ten, phongBan, loaiNghi, thoiGian, lyDo);

            // Căn chỉnh chiều rộng của thẻ cho vừa khít với khung giữa (trừ hao 25px cho thanh cuộn)
            item.Width = flpDanhSach.Width - 25;

            // Thêm thẻ vào FlowLayoutPanel
            flpDanhSach.Controls.Add(item);
        }

        private void flpDanhSach_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}