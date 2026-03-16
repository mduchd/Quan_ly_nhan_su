using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace Quan_ly_nhan_su.GUI.ChamCongNghiPhep
{
    public partial class ucChamCong : UserControl
    {
        public ucChamCong()
        {
            InitializeComponent();

            // Ép layout chuẩn ngay khi vừa khởi tạo màn hình
            CauHinhLayoutLichDoiNhom();
        }

        // =======================================================
        // HÀM SỬA LỖI CUỘN CHO BẢNG "TỔNG QUAN LỊCH ĐỘI NHÓM"
        // =======================================================
        private void CauHinhLayoutLichDoiNhom()
        {
            // 1. Tắt cuộn ở cái khung trắng ngoài cùng (guna2Panel1) để nó không giành cuộn của thằng con
            guna2Panel1.AutoScroll = false;

            // 2. Ép Header (Thứ 2, Thứ 3...) dính chặt lên trần
            tableLayoutPanel3.Dock = DockStyle.Top;

            // 3. Ép khung danh sách (flowLayoutPanel1) lấp kín phần trống bên dưới và BẬT cuộn
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.AutoSize = false;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Margin = new Padding(0); // Xóa lề thừa để thanh cuộn không bị lẹm

            // 4. Bảng dữ liệu chứa các dòng nhân viên phải tự do phình to khi có nhiều người
            tableLayoutPanel4.AutoSize = true;

            // 5. CHỐT HẠ (Quan trọng nhất): Phân chia lại thứ tự Lớp (Z-Order)
            // Lệnh này đảm bảo Header nằm trên cùng, Body nằm dưới, không ai đè ai.
            tableLayoutPanel3.BringToFront();
            flowLayoutPanel1.BringToFront();
        }

        // =======================================================
        // PHẦN XỬ LÝ CHO BẢNG "XỬ LÝ YÊU CẦU NGHỈ PHÉP" (Bảng trên)
        // =======================================================
        public void loadDanhSachNghiPhep()
        {
            flpDanhSach.SuspendLayout();
            flpDanhSach.Controls.Clear();

            int itemWidth = flpDanhSach.ClientRectangle.Width - flpDanhSach.Padding.Horizontal - 25;
            string query = "Select * from YeuCauNghiPhep";
            Quan_ly_nhan_su.DAL.ChamCongDAL dataChamCong = new Quan_ly_nhan_su.DAL.ChamCongDAL();
            DataTable dtNghiPhep = dataChamCong.ExecuteQuery(query);

            foreach (DataRow row in dtNghiPhep.Rows)
            {
                ucNghiPhep item = new ucNghiPhep();
                item.Width = itemWidth;
                string ten = row["TenNhanVien"].ToString();
                string loaiNghi = row["LoaiNghi"].ToString();
                string lyDo = row["LyDo"].ToString();
                string phongBan = row["PhongBan"].ToString();

                // Xử lý ngày tháng cho đẹp (ví dụ: 20/03 - 22/03)
                DateTime tuNgay = Convert.ToDateTime(row["TuNgay"]);
                DateTime denNgay = Convert.ToDateTime(row["DenNgay"]);
                string thoiGian = $"{tuNgay:dd/MM} - {denNgay:dd/MM}";

                // Truyền dữ liệu thật vào thẻ
                item.SetData(ten, loaiNghi, thoiGian, lyDo, phongBan);

                flpDanhSach.Controls.Add(item);
            }
            flpDanhSach.ResumeLayout();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDanhSachNghiPhep();
        }

        private void flpDanhSach_Resize(object sender, EventArgs e)
        {
            flpDanhSach.SuspendLayout();
            int newWidth = flpDanhSach.ClientSize.Width - flpDanhSach.Padding.Horizontal - 25;
            foreach (Control ctrl in flpDanhSach.Controls)
            {
                if (ctrl is ucNghiPhep)
                {
                    ctrl.Width = newWidth;
                }
            }
            flpDanhSach.ResumeLayout();
        }

        // =======================================================
        // CÁC HÀM RỖNG (GIỮ NGUYÊN ĐỂ KHÔNG BỊ LỖI DESIGNER)
        // =======================================================
        private void frmTest_Load(object sender, EventArgs e) { }
        private void flpDanhSach_Paint_1(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e) { }
        private void panel16_Paint(object sender, PaintEventArgs e) { }
        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e) { }
    }
}