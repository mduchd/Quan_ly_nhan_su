using System;
using System.Data;
using System.Windows.Forms;
using Quan_ly_nhan_su.BUS; // Gọi tầng BUS lên
using ClosedXML.Excel;

namespace Quan_ly_nhan_su.GUI
{
    public partial class ucQuanLyCong : UserControl
    {
        // 1. Khai báo tầng BUS của bạn
        private readonly QuanLyCongBUS bus = new();

        public ucQuanLyCong()
        {
            InitializeComponent();
            Load += ucQuanLyCong_Load;
        }

        // 2. Hàm Load mặc định (Chạy khi vừa mở Form)
        private void ucQuanLyCong_Load(object? sender, EventArgs e)
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
                    SetColumnHeader("maNV", "Mã NV");
                    SetColumnHeader("hoTen", "Họ Tên");
                    SetColumnHeader("ngay", "Ngày");
                    var ngayColumn = dgvDanhSachCong.Columns["ngay"];
                    if (ngayColumn != null)
                    {
                        ngayColumn.DefaultCellStyle.Format = "dd/MM/yyyy";
                    }

                    SetColumnHeader("gioVao", "Giờ Vào");
                    SetColumnHeader("gioRa", "Giờ Ra");

                    // Kiểm tra xem SQL có trả về cột TongGio không thì mới đổi tên (tránh lỗi nếu DAL chưa cập nhật TongGio)
                    var tongGioColumn = dgvDanhSachCong.Columns["TongGio"];
                    if (tongGioColumn != null)
                    {
                        tongGioColumn.HeaderText = "Tổng Giờ";
                    }

                    SetColumnHeader("trangThai", "Trạng Thái");

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
        private void btnLoc_Click(object? sender, EventArgs e)
        {
            LoadData();
        }

        // Nút Làm mới (Mình để tên btnRefresh_Click theo đúng ảnh bạn chụp lần trước)
        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            txtTimKiem.Clear();
            ucQuanLyCong_Load(sender, e);
        }

        // Nút Xuất Excel
        private void btnXuatExcel_Click(object? sender, EventArgs e)
        {
            if (dgvDanhSachCong.Rows.Count == 0 || dgvDanhSachCong.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveDialog.FileName = $"LichSuChamCong_{DateTime.Now:ddMMyyyy}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Lịch Sử Chấm Công");

                        // 1. Tạo Tiêu đề (Header)
                        for (int i = 0; i < dgvDanhSachCong.Columns.Count; i++)
                        {
                            var cell = worksheet.Cell(1, i + 1);
                            cell.Value = dgvDanhSachCong.Columns[i].HeaderText;
                            cell.Style.Font.Bold = true;
                            cell.Style.Fill.BackgroundColor = XLColor.BabyBlue;
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }

                        // 2. Đổ dữ liệu từ Grid vào Excel
                        for (int i = 0; i < dgvDanhSachCong.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvDanhSachCong.Columns.Count; j++)
                            {
                                var value = dgvDanhSachCong.Rows[i].Cells[j].Value;
                                var cell = worksheet.Cell(i + 2, j + 1);

                                if (value != null)
                                {
                                    // Chuyển đổi giá trị sang kiểu dữ liệu Excel phù hợp (Số, Ngày, Giờ)
                                    cell.Value = XLCellValue.FromObject(value);
                                    
                                    // Nếu là cột Ngày, định dạng dd/MM/yyyy
                                    if (dgvDanhSachCong.Columns[j].Name.ToLower().Contains("ngay"))
                                    {
                                        cell.Style.DateFormat.Format = "dd/MM/yyyy";
                                    }
                                }
                                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }
                        }

                        // 3. Tự động giãn cột và lưu
                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show("Xuất lịch sử chấm công ra Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void SetColumnHeader(string columnName, string headerText)
        {
            if (!dgvDanhSachCong.Columns.Contains(columnName))
            {
                return;
            }

            var column = dgvDanhSachCong.Columns[columnName];
            if (column != null)
            {
                column.HeaderText = headerText;
            }
        }
    }
}
