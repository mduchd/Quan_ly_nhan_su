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
            int thangHienTai = DateTime.Now.Month;
            int namHienTai = DateTime.Now.Year;

            // Hiển thị text lên một Label tiêu đề (Ví dụ: "Tháng 3, 2026")
            // lblThangNam.Text = $"Tháng {thangHienTai}, {namHienTai}"; 

            // Gọi hàm render lịch
            
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
       

        // Hàm xử lý logic đổ nút bấm vào bảng
       

        private void ucTaoDonNghiPhep_Load(object sender, EventArgs e)
        {
           
            // Lấy tháng và năm hiện tại để hiển thị
            int thang = DateTime.Now.Month;
            int nam = DateTime.Now.Year;

            // Hiển thị text lên một Label tiêu đề (Ví dụ: "Tháng 3, 2026")
            // lblThangNam.Text = $"Tháng {thangHienTai}, {namHienTai}"; 

            // Gọi hàm render lịch
            //VeLichThuan(thang, nam);

        }
        //private void VeLichThuan(int month, int year)
        //{
        //    for (int i = tableLayoutPanel2.Controls.Count - 1; i >= 0; i--)
        //    {
        //        Control ctrl = tableLayoutPanel2.Controls[i];
        //        if (tableLayoutPanel2.GetRow(ctrl) > 0)
        //        {
        //            tableLayoutPanel2.Controls.Remove(ctrl);
        //            ctrl.Dispose(); //
        //        }
        //    }
        //    DateTime ngayDauThang = new DateTime(year, month, 1);
        //    int soNgayCuaThang = DateTime.DaysInMonth(year, month);

        //    // Trong C#: Chủ nhật = 0, Thứ 2 = 1, ... Thứ 7 = 6
        //    // Bảng của bạn: Thứ 2 = Cột 0, ..., Chủ nhật = Cột 6
        //    // -> Công thức quy đổi để mùng 1 rơi vào đúng cột:
        //    int thuCuaNgayMung1 = (int)ngayDauThang.DayOfWeek;
        //    int cotHienTai = (thuCuaNgayMung1 == 0) ? 6 : thuCuaNgayMung1 - 1;

        //    int hangHienTai = 1; // Bắt đầu đẩy ngày từ hàng số 1

        //    // 3. VÒNG LẶP SINH RA LỊCH
        //    for (int ngay = 1; ngay <= soNgayCuaThang; ngay++)
        //    {
        //        // Sử dụng Label thuần của WinForms
        //        Label lblNgay = new Label();
        //        lblNgay.Text = ngay.ToString();
        //        lblNgay.Dock = DockStyle.Fill;
        //        lblNgay.TextAlign = ContentAlignment.MiddleCenter; // Canh chữ ra giữa ô
        //        lblNgay.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        //        lblNgay.Margin = new Padding(2);
        //        lblNgay.Cursor = Cursors.Hand;

        //        // Thiết lập màu cơ bản
        //        lblNgay.BackColor = Color.Transparent;
        //        lblNgay.ForeColor = Color.Black;

        //        // TÔ MÀU DEMO THEO TRẠNG THÁI
        //        // (Lưu ý: Label mặc định của WinForms là hình chữ nhật, nên nền sẽ là ô vuông chứ không bo tròn được như Guna)
        //        if (ngay == 5)
        //        {
        //            lblNgay.BackColor = Color.LightGreen;
        //            lblNgay.ForeColor = Color.DarkGreen;
        //        }
        //        else if (ngay >= 24 && ngay <= 26)
        //        {
        //            lblNgay.BackColor = Color.LightGoldenrodYellow;
        //            lblNgay.ForeColor = Color.DarkOrange;
        //        }

        //        // ĐẨY VÀO BẢNG
        //        tableLayoutPanel2.Controls.Add(lblNgay, cotHienTai, hangHienTai);

        //        // TÍNH TOÁN TỌA ĐỘ CHO NGÀY MAI
        //        cotHienTai++;
        //        if (cotHienTai > 6) // Vượt quá Chủ Nhật (cột 6)
        //        {
        //            cotHienTai = 0; // Quay lại Thứ 2 (cột 0)
        //            hangHienTai++;  // Rớt xuống hàng dưới
        //        }
        //    }
        //}
    }
}
