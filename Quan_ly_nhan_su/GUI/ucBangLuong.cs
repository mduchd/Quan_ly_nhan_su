using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Quan_ly_nhan_su.DTO;
using Quan_ly_nhan_su.BUS;
using Quan_ly_nhan_su.DAL;
using ClosedXML.Excel;

namespace Quan_ly_nhan_su.GUI
{
    public partial class ucBangLuong : UserControl
    {
        private Label lblTieuDe;
        private DataGridView dgvBangLuong;
        private TextBox txtTimKiem;
        private Button btnLamMoi;
        private Button btnXuatExcel;
        private Label lblTongQuyLuong;


        BangLuongBUS bus = new BangLuongBUS();
        BangLuongDAL dal = new BangLuongDAL();

        List<BangLuongDTO> dsGoc = new List<BangLuongDTO>();
        public ucBangLuong()
        {
            InitializeComponent();
            InitUI();
            LoadBangLuong();
        }
        private void InitUI()
        {
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblTieuDe = new Label()
            {
                Text = "BẢNG TÍNH LƯƠNG NHÂN VIÊN",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 102)
            };

            txtTimKiem = new TextBox()
            {
                Location = new Point(20, 70),
                Width = 350,
                Font = new Font("Segoe UI", 11, FontStyle.Regular)
            };
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;

            btnLamMoi = new Button()
            {
                Text = "Refresh",
                Location = new Point(380, 68),
                Size = new Size(100, 30),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.Click += (s, e) =>
            {
                txtTimKiem.Text = "";
            };
            dgvBangLuong = new DataGridView()
            {
                Location = new Point(20, 130),
                Size = new Size(760, 300),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowTemplate =
                {
                    Height = 35
                },
                ColumnHeadersHeight = 40,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            lblTongQuyLuong = new Label()
            {
                Location = new Point(20, 445),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
            };
            btnXuatExcel = new Button()
            {
                Text = "Xuất Excel",
                Location = new Point(490, 68),
                Size = new Size(100, 30),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnXuatExcel.FlatAppearance.BorderSize = 0;
            btnXuatExcel.Click += BtnXuatExcel_Click;

            this.Controls.Add(lblTieuDe);
            this.Controls.Add(txtTimKiem);
            this.Controls.Add(btnLamMoi);
            this.Controls.Add(dgvBangLuong);
            this.Controls.Add(lblTongQuyLuong);
            this.Controls.Add(btnXuatExcel);
        }
        private void LoadBangLuong(string tuKhoa = "")
        {
            dsGoc = dal.GetDanhSachBangLuong(tuKhoa);
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
