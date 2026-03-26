using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Quan_ly_nhan_su.DTO;
using Quan_ly_nhan_su.BUS;
using Quan_ly_nhan_su.DAL;
using ClosedXML.Excel;
using Guna.UI2.WinForms;

namespace Quan_ly_nhan_su.GUI
{
    public partial class ucBangLuong : UserControl
    {
        BangLuongBUS bus = new BangLuongBUS();
        BangLuongDAL dal = new BangLuongDAL();
        List<BangLuongDTO> dsGoc = new List<BangLuongDTO>();

        public ucBangLuong()
        {
            InitializeComponent();
            InitUI();
            LoadBangLuong();
        }

        private Label lblTieuDe;
        private Guna2TextBox txtTimKiem;
        private Guna2Button btnLamMoi;
        private Guna2Button btnXuatExcel;
        private Guna2DataGridView dgvBangLuong;
        private Label lblTongQuyLuong;
        private Label lblThangNam;
        private Guna2DateTimePicker dtpThangNam;

        // BIẾN MỚI CHO TÍNH NĂNG CẬP NHẬT LƯƠNG
        private Guna2TextBox txtMaNV;
        private Guna2TextBox txtTenNV;
        private Guna2TextBox txtLuongCung;
        private Guna2Button btnCapNhatLuong;

        private void InitUI()
        {
            this.BackColor = Color.FromArgb(242, 245, 250);
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            lblTieuDe = new Label() { Text = "BẢNG TÍNH LƯƠNG NHÂN VIÊN", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(0, 51, 102) };

            txtTimKiem = new Guna2TextBox() { Location = new Point(20, 70), Size = new Size(350, 36), BorderRadius = 8, PlaceholderText = "Nhập mã hoặc tên nhân viên để tìm...", Font = new Font("Segoe UI", 10, FontStyle.Regular) };
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;

            btnLamMoi = new Guna2Button() { Text = "Làm mới", Location = new Point(380, 70), Size = new Size(100, 36), BorderRadius = 8, FillColor = Color.FromArgb(189, 195, 199), ForeColor = Color.Black, Cursor = Cursors.Hand };
            btnLamMoi.Click += (s, e) => { txtTimKiem.Text = ""; };

            btnXuatExcel = new Guna2Button() { Text = "Xuất Excel", Location = new Point(490, 70), Size = new Size(100, 36), BorderRadius = 8, FillColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, Cursor = Cursors.Hand };
            btnXuatExcel.Click += BtnXuatExcel_Click;

            lblThangNam = new Label() { Text = "Chọn tháng:", Location = new Point(590, 78), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Italic) };

            dtpThangNam = new Guna2DateTimePicker() { Location = new Point(695, 70), Size = new Size(110, 36), BorderRadius = 8, Format = DateTimePickerFormat.Custom, CustomFormat = "MM/yyyy", ShowUpDown = false, FillColor = Color.White, BorderColor = Color.FromArgb(213, 218, 223), BorderThickness = 1 };
            dtpThangNam.ValueChanged += (s, e) => LoadBangLuong(txtTimKiem.Text);

            // GIAO DIỆN MỚI: Các ô nhập liệu để cập nhật lương (Đặt ở Y = 120)
            txtMaNV = new Guna2TextBox() { Location = new Point(20, 120), Size = new Size(100, 36), ReadOnly = true, PlaceholderText = "Mã NV", BorderRadius = 8 };
            txtTenNV = new Guna2TextBox() { Location = new Point(130, 120), Size = new Size(200, 36), ReadOnly = true, PlaceholderText = "Tên NV", BorderRadius = 8 };
            txtLuongCung = new Guna2TextBox() { Location = new Point(340, 120), Size = new Size(150, 36), PlaceholderText = "Lương cứng mới...", BorderRadius = 8 };

            btnCapNhatLuong = new Guna2Button() { Text = "Cập nhật Lương", Location = new Point(500, 120), Size = new Size(150, 36), BorderRadius = 8, FillColor = Color.Orange, ForeColor = Color.White, Cursor = Cursors.Hand };
            btnCapNhatLuong.Click += BtnCapNhatLuong_Click;

            // ĐẨY BẢNG XUỐNG DƯỚI MỘT CHÚT (Y = 170) ĐỂ TRÁNH BỊ ĐÈ
            dgvBangLuong = new Guna2DataGridView()
            {
                Location = new Point(20, 170),
                Size = new Size(785, 260),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ThemeStyle = { AlternatingRowsStyle = { BackColor = Color.White }, RowsStyle = { Height = 40 } },
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                GridColor = Color.Black
            };
            dgvBangLuong.CellClick += DgvBangLuong_CellClick; // Bắt sự kiện click vào dòng

            lblTongQuyLuong = new Label() { Location = new Point(20, 445), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.DarkRed, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };

            this.Controls.Add(lblTieuDe);
            this.Controls.Add(txtTimKiem);
            this.Controls.Add(btnLamMoi);
            this.Controls.Add(btnXuatExcel);
            this.Controls.Add(lblThangNam);
            this.Controls.Add(dtpThangNam);

            this.Controls.Add(txtMaNV);
            this.Controls.Add(txtTenNV);
            this.Controls.Add(txtLuongCung);
            this.Controls.Add(btnCapNhatLuong);

            this.Controls.Add(dgvBangLuong);
            this.Controls.Add(lblTongQuyLuong);
        }

        private void LoadBangLuong(string tuKhoa = "")
        {
            try
            {
                string thangDuocChon = dtpThangNam.Value.ToString("MM/yyyy");
                dsGoc = dal.GetDanhSachBangLuong(tuKhoa, thangDuocChon);

                foreach (var nv in dsGoc)
                {
                    nv.TongLuong = bus.TinhTongLuong(nv);
                }
                HienThiLenBang(dsGoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu bảng lương: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HienThiLenBang(List<BangLuongDTO> danhSach)
        {
            try
            {
                dgvBangLuong.AutoGenerateColumns = false;
                dgvBangLuong.Columns.Clear();

                // 1. TẮT TỰ ĐỘNG CO GIÃN ĐỂ CHO PHÉP BẢNG TRÀN RA NGOÀI
                dgvBangLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                // 2. ÉP BẬT THANH CUỘN (Cả dọc cả ngang)
                dgvBangLuong.ScrollBars = ScrollBars.Both;

                // 3. SET CHIỀU RỘNG TỪNG CỘT THẬT TO (Tổng Width > 1000px sẽ tự kích hoạt thanh cuộn ngang)
                dgvBangLuong.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MaNV", DataPropertyName = "MaNV", HeaderText = "Mã NV", Width = 100 });

                // Cột Tên NV: Cho rộng 300px hiển thị thoải mái họ tên dài
                dgvBangLuong.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TenNV", DataPropertyName = "TenNV", HeaderText = "Tên Nhân Viên", Width = 300 });

                // Cột Lương Cứng: Cho 200px hiển thị trăm tỉ
                dgvBangLuong.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LuongCung", DataPropertyName = "LuongCung", HeaderText = "Lương Cứng", Width = 200 });

                // Cột Làm / Nghỉ: 100px cho thoáng
                dgvBangLuong.Columns.Add(new DataGridViewTextBoxColumn() { Name = "SoNgayLam", DataPropertyName = "SoNgayLam", HeaderText = "Làm", Width = 100 });
                dgvBangLuong.Columns.Add(new DataGridViewTextBoxColumn() { Name = "SoNgayNghi", DataPropertyName = "SoNgayNghi", HeaderText = "Nghỉ", Width = 100 });

                // Cột Tổng Lương: 250px cực kỳ rộng rãi
                dgvBangLuong.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TongLuong", DataPropertyName = "TongLuong", HeaderText = "Tổng Lương (VNĐ)", Width = 250 });

                dgvBangLuong.DataSource = danhSach;

                // Định dạng tiền tệ và căn lề
                dgvBangLuong.Columns["LuongCung"].DefaultCellStyle.Format = "N0";
                dgvBangLuong.Columns["TongLuong"].DefaultCellStyle.Format = "N0";
                dgvBangLuong.Columns["SoNgayLam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBangLuong.Columns["SoNgayNghi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                decimal tongTien = danhSach != null ? danhSach.Sum(nv => nv.TongLuong) : 0;
                lblTongQuyLuong.Text = $"Tổng quỹ Lương: {tongTien:N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị bảng: " + ex.Message, "Lỗi UI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadBangLuong(txtTimKiem.Text);
        }

        // SỰ KIỆN MỚI: Bắn dữ liệu lên Textbox khi click vào bảng
        private void DgvBangLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvBangLuong.CurrentRow != null)
            {
                var nv = (BangLuongDTO)dgvBangLuong.CurrentRow.DataBoundItem;
                txtMaNV.Text = nv.MaNV;
                txtTenNV.Text = nv.TenNV;
                txtLuongCung.Text = nv.LuongCung.ToString("0");
            }
        }

        // SỰ KIỆN MỚI: Lưu lương cứng xuống Database
        private void BtnCapNhatLuong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên trên bảng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (decimal.TryParse(txtLuongCung.Text, out decimal luongMoi))
            {
                if (bus.CapNhatLuongCung(txtMaNV.Text, luongMoi))
                {
                    MessageBox.Show("Cập nhật lương cứng thành công!", "Tuyệt vời", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBangLuong(txtTimKiem.Text);
                    txtMaNV.Text = ""; txtTenNV.Text = ""; txtLuongCung.Text = "";
                }
                else
                {
                    MessageBox.Show("Lương không hợp lệ hoặc lỗi CSDL!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvBangLuong.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveDialog.FileName = $"BangLuong_{dtpThangNam.Value:MM_yyyy}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Bảng Lương");

                        for (int i = 0; i < dgvBangLuong.Columns.Count; i++)
                        {
                            var cell = worksheet.Cell(1, i + 1);
                            cell.Value = dgvBangLuong.Columns[i].HeaderText;
                            cell.Style.Font.Bold = true;
                            cell.Style.Fill.BackgroundColor = XLColor.LightBlue;
                            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }

                        for (int i = 0; i < dgvBangLuong.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvBangLuong.Columns.Count; j++)
                            {
                                var value = dgvBangLuong.Rows[i].Cells[j].Value;
                                var cell = worksheet.Cell(i + 2, j + 1);

                                if (value != null)
                                {
                                    cell.Value = XLCellValue.FromObject(value);
                                }

                                if (dgvBangLuong.Columns[j].Name == "LuongCung" || dgvBangLuong.Columns[j].Name == "TongLuong")
                                {
                                    cell.Style.NumberFormat.Format = "#,##0 \"VNĐ\"";
                                }

                                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }
                        }

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show("Xuất báo cáo lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}