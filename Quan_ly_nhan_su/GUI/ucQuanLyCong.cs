using System;
using System.Data;
using System.Windows.Forms;
using Quan_ly_nhan_su.BUS; // Gọi tầng BUS lên

namespace Quan_ly_nhan_su.GUI
{
    public partial class ucQuanLyCong : UserControl
    {
        // 1. Khai báo tầng BUS của bạn
        private QuanLyCongBUS bus = new QuanLyCongBUS();

        public ucQuanLyCong()
        {
            InitializeComponent();
        }

        // 2. Hàm Load mặc định (Chạy khi vừa mở Form)
        private void ucQuanLyCong_Load(object sender, EventArgs e)
        {
            // Mặc định chọn từ ngày mùng 1 đến ngày cuối tháng
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            // Lấy dữ liệu
            LoadData();
        }

        // 3. Hàm Lõi: Đẩy dữ liệu lên bảng
        private void LoadData()
        {
            try
            {
                // Lấy thông tin từ các ô trên giao diện
                string tuKhoa = txtTimKiem.Text;
                DateTime tuNgay = dtpTuNgay.Value;
                DateTime denNgay = dtpDenNgay.Value;

                // Gọi đúng tên hàm LayDanhSachLichSuCong từ file BUS của bạn
                DataTable dt = bus.LayDanhSachLichSuCong(tuKhoa, tuNgay, denNgay);
                dgvDanhSachCong.DataSource = dt;

                // Trang điểm lại tên cột cho đẹp
                if (dgvDanhSachCong.Columns.Count > 0)
                {
                    dgvDanhSachCong.Columns["maNV"].HeaderText = "Mã NV";
                    dgvDanhSachCong.Columns["hoTen"].HeaderText = "Họ Tên";
                    dgvDanhSachCong.Columns["ngay"].HeaderText = "Ngày";
                    dgvDanhSachCong.Columns["ngay"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvDanhSachCong.Columns["gioVao"].HeaderText = "Giờ Vào";
                    dgvDanhSachCong.Columns["gioRa"].HeaderText = "Giờ Ra";

                    // Kiểm tra xem SQL có trả về cột TongGio không thì mới đổi tên (tránh lỗi nếu DAL chưa cập nhật TongGio)
                    if (dgvDanhSachCong.Columns.Contains("TongGio"))
                    {
                        dgvDanhSachCong.Columns["TongGio"].HeaderText = "Tổng Giờ";
                    }

                    dgvDanhSachCong.Columns["trangThai"].HeaderText = "Trạng Thái";

                    // Phóng to các cột cho lấp đầy bảng
                    dgvDanhSachCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                // Nếu dính lỗi "quá 365 ngày" bên BUS, nó sẽ hiện thông báo ở đây
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // KHU VỰC CỦA CÁC NÚT BẤM
        // ==========================================

        // Nút Lọc
        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        // Nút Làm mới (Mình để tên btnRefresh_Click theo đúng ảnh bạn chụp lần trước)
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            ucQuanLyCong_Load(sender, e);
        }

        // Nút Xuất Excel
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachCong.Rows.Count == 0 || dgvDanhSachCong.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}