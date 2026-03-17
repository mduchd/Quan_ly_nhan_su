using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Quan_ly_nhan_su.GUI.ChamCongNghiPhep
{
    public partial class ucTaoDonNghiPhep : UserControl
    {
        public ucTaoDonNghiPhep()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        
            // Xóa dữ liệu cũ trong bảng (nếu có) trước khi tạo tháng mới
            // Sự kiện chạy ngay khi UserControl vừa được mở lên
private void UserControlNghiPhep_Load(object sender, EventArgs e)
        {
            // Lấy tháng và năm hiện tại để hiển thị
            int thangHienTai = DateTime.Now.Month;
            int namHienTai = DateTime.Now.Year;

            // Hiển thị text lên một Label tiêu đề (Ví dụ: "Tháng 3, 2026")
            // lblThangNam.Text = $"Tháng {thangHienTai}, {namHienTai}"; 

            // Gọi hàm render lịch
            HienThiLich(thangHienTai, namHienTai);
        }

        // Hàm xử lý logic đổ nút bấm vào bảng
        private void HienThiLich(int month, int year)
        {
            // 1. Dọn dẹp các nút của tháng cũ (nếu có), CHỈ XÓA BUTTON, giữ lại Label tiêu đề T2-CN
            for (int i = tableLayoutPanel1.Controls.Count - 1; i >= 0; i--)
            {
                if (tableLayoutPanel1.Controls[i] is Guna.UI2.WinForms.Guna2Button)
                {
                    tableLayoutPanel1.Controls.RemoveAt(i);
                }
            }

            // 2. Tính toán ngày tháng
            DateTime ngayDauThang = new DateTime(year, month, 1);
            int soNgayCuaThang = DateTime.DaysInMonth(year, month);

            // Tính xem mùng 1 rơi vào cột nào (Quy ước: Thứ 2 = Cột 0, ..., Chủ Nhật = Cột 6)
            int dayOfWeek = (int)ngayDauThang.DayOfWeek; // Trong C#: 0 = Sunday, 1 = Monday...
            int cotBatDau = (dayOfWeek == 0) ? 6 : dayOfWeek - 1;

            int hangHienTai = 1; // Bắt đầu đổ ngày từ hàng số 1 (vì hàng 0 là chữ T2-CN rồi)
            int cotHienTai = cotBatDau;

            // 3. Vòng lặp tạo ngày
            for (int i = 1; i <= soNgayCuaThang; i++)
            {
                // Khởi tạo nút
                Guna.UI2.WinForms.Guna2Button btnDay = new Guna.UI2.WinForms.Guna2Button();
                btnDay.Text = i.ToString();
                btnDay.Size = new Size(40, 40);
                btnDay.BorderRadius = 20; // Bo tròn thành hình oval/tròn
                btnDay.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btnDay.Cursor = Cursors.Hand;
                btnDay.Anchor = AnchorStyles.None; // Rất quan trọng: Giúp nút luôn nằm chính giữa ô

                // Mặc định là ngày thường (Không màu nền)
                btnDay.FillColor = Color.Transparent;
                btnDay.ForeColor = Color.Black;

                // (TÙY CHỌN) Test thử tô màu theo mẫu thiết kế của bạn
                if (i == 5) // Ngày 5: Đã duyệt (Màu xanh)
                {
                    btnDay.FillColor = Color.FromArgb(200, 240, 210);
                    btnDay.ForeColor = Color.FromArgb(0, 150, 50);
                }
                else if (i >= 24 && i <= 26) // Ngày 24-26: Đang chờ (Màu vàng)
                {
                    btnDay.FillColor = Color.FromArgb(255, 245, 200);
                    btnDay.ForeColor = Color.FromArgb(200, 120, 0);
                }

                // Đẩy nút vào bảng ở đúng tọa độ (Cột, Hàng)
                tableLayoutPanel1.Controls.Add(btnDay, cotHienTai, hangHienTai);

                // Tính toán tọa độ cho ngày tiếp theo
                cotHienTai++;
                if (cotHienTai > 6) // Nếu vượt qua Chủ Nhật (cột 6) thì xuống dòng mới
                {
                    cotHienTai = 0; // Quay lại Thứ 2
                    hangHienTai++;  // Xuống hàng tiếp theo
                }
            }
        
        }
    }
}
