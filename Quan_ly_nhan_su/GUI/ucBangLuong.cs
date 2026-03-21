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
        // --- 1. KHAI BÁO LẠI CÁC BIẾN BẰNG GUNA CONTROL ---
        private Label lblTieuDe;
        private Guna2TextBox txtTimKiem;
        private Guna2Button btnLamMoi;
        private Guna2Button btnXuatExcel;
        private Guna2DataGridView dgvBangLuong;
        private Label lblTongQuyLuong;
        private Label lblThangNam;
        private Guna2DateTimePicker dtpThangNam;

        // --- 2. HÀM INIT CẬP NHẬT GIAO DIỆN MỚI ---
        private void InitUI()
        {
            this.BackColor = Color.FromArgb(242, 245, 250); // Màu nền xám xanh nhạt cực sang
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            lblTieuDe = new Label() { Text = "BẢNG TÍNH LƯƠNG NHÂN VIÊN", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(0, 51, 102) };

            // Guna2TextBox: Bo góc, có sẵn chữ mờ (Placeholder)
            txtTimKiem = new Guna2TextBox()
            {
                Location = new Point(20, 70),
                Size = new Size(350, 36),
                BorderRadius = 8, // Bo góc mềm mại
                PlaceholderText = "Nhập mã hoặc tên nhân viên để tìm...", // Chữ mờ hướng dẫn
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;

            // Guna2Button: Bo góc, đổi màu mượt mà
            btnLamMoi = new Guna2Button()
            {
                Text = "Làm mới",
                Location = new Point(380, 70),
                Size = new Size(100, 36),
                BorderRadius = 8,
                FillColor = Color.FromArgb(189, 195, 199),
                ForeColor = Color.Black,
                Cursor = Cursors.Hand
            };
            btnLamMoi.Click += (s, e) => { txtTimKiem.Text = ""; };

            btnXuatExcel = new Guna2Button()
            {
                Text = "Xuất Excel",
                Location = new Point(490, 70),
                Size = new Size(100, 36),
                BorderRadius = 8,
                FillColor = Color.FromArgb(46, 204, 113), // Xanh lá mượt
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnXuatExcel.Click += BtnXuatExcel_Click;

            lblThangNam = new Label() { Text = "Chọn tháng:", Location = new Point(590, 78), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Italic) };

            // Guna2DateTimePicker: Chỉnh viền và góc y hệt textbox
            dtpThangNam = new Guna2DateTimePicker()
            {
                Location = new Point(695, 70),
                Size = new Size(110, 36),
                BorderRadius = 8,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MM/yyyy",
                ShowUpDown = false,
                FillColor = Color.White,
                BorderColor = Color.FromArgb(213, 218, 223),
                BorderThickness = 1
            };
            dtpThangNam.ValueChanged += (s, e) => LoadBangLuong(txtTimKiem.Text);

            // Guna2DataGridView: Tự động có Theme siêu đẹp, không cần tự chỉnh màu từng dòng
            dgvBangLuong = new Guna2DataGridView()
            {
                Location = new Point(20, 130),
                Size = new Size(785, 300),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ThemeStyle = { AlternatingRowsStyle = { BackColor = Color.White }, RowsStyle = { Height = 40 } },
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                GridColor = Color.Black 
            };

            lblTongQuyLuong = new Label()
            {
                Location = new Point(20, 445),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
            };

            this.Controls.Add(lblTieuDe);
            this.Controls.Add(txtTimKiem);
            this.Controls.Add(btnLamMoi);
            this.Controls.Add(btnXuatExcel);
            this.Controls.Add(lblThangNam);
            this.Controls.Add(dtpThangNam);
            this.Controls.Add(dgvBangLuong);
            this.Controls.Add(lblTongQuyLuong);
        }
        private void LoadBangLuong(string tuKhoa = "")
        {
            string thangDuocChon = dtpThangNam.Value.ToString("MM/yyyy");

            // 2. Truyền cả Từ khóa VÀ Tháng năm xuống tầng DAL để lọc
            dsGoc = dal.GetDanhSachBangLuong(tuKhoa, thangDuocChon);

            foreach (var nv in dsGoc)
            {
                nv.TongLuong = bus.TinhTongLuong(nv);
            }
            HienThiLenBang(dsGoc);
        }
        private void HienThiLenBang(List<BangLuongDTO> danhSach) { 
            dgvBangLuong.DataSource = null;
            dgvBangLuong.DataSource = danhSach;
            if (dgvBangLuong.Columns.Count > 0)
            {
                dgvBangLuong.Columns["MaNV"].HeaderText = "Mã NV";
                dgvBangLuong.Columns["TenNV"].HeaderText = "Tên Nhân Viên";
                dgvBangLuong.Columns["LuongCung"].HeaderText = "Lương Cứng";
                dgvBangLuong.Columns["SoNgayLam"].HeaderText = "Ngày Làm";
                dgvBangLuong.Columns["SoNgayNghi"].HeaderText = "Ngày Nghỉ";
                dgvBangLuong.Columns["TongLuong"].HeaderText = "Tổng Lương (VNĐ)";
                dgvBangLuong.Columns["LuongCung"].DefaultCellStyle.Format = "N0";
                dgvBangLuong.Columns["TongLuong"].DefaultCellStyle.Format = "N0";
            }
            decimal tongTien = danhSach.Sum(nv => nv.TongLuong);
            lblTongQuyLuong.Text = $"Tổng quỹ Lương: {tongTien:N0} VNĐ";
        }
        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadBangLuong(txtTimKiem.Text);
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
                saveDialog.FileName = "BaoCaoBangLuong.xlsx"; 

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
                        }
                        for (int i = 0; i < dgvBangLuong.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvBangLuong.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dgvBangLuong.Rows[i].Cells[j].Value?.ToString();
                            }
                        }
                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show("Xuất file Excel thành công rực rỡ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
