using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using Quan_ly_nhan_su.BUS;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.GUI
{
    public partial class ucNhanVien : UserControl
    {
        private readonly NhanVienBUS _nhanVienBUS = new();
        private List<string> _departments = new();
        private List<NhanVienDTO> _employees = new();
        private readonly Panel _pnlPaging = new();
        private readonly Button _btnPrevPage = new();
        private readonly Button _btnNextPage = new();
        private readonly Label _lblPageInfo = new();
        private int _currentPage = 1;
        private int _totalRecords;
        private int _totalPages = 1;
        private const int PageSize = 20;

        private const string EmployeeIdColumnName = "MaNV";

        public ucNhanVien()
        {
            InitializeComponent();
            InitializePagingControls();
            LoadCombobox();
            LoadGridData(resetPage: true);
            ClearInputs();

            SizeChanged += ucNhanVien_SizeChanged;
            ApplyResponsiveLayout();
        }

        private void ucNhanVien_SizeChanged(object? sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void LoadCombobox()
        {
            _departments = _nhanVienBUS.LayDanhSachPhongBan();

            var selectedDept = cbDept.SelectedItem?.ToString();
            cbDept.DataSource = null;
            cbDept.DataSource = _departments;

            if (!string.IsNullOrWhiteSpace(selectedDept) && _departments.Contains(selectedDept))
            {
                cbDept.SelectedItem = selectedDept;
            }

            cbGender.DataSource = new List<string> { "Nam", "Nữ", "Khác" };
        }

        private void LoadGridData(bool resetPage = false)
        {
            if (resetPage)
            {
                _currentPage = 1;
            }

            _employees = _nhanVienBUS.LayDanhSachNhanVien(txtSearch.Text.Trim(), _currentPage, PageSize, out _totalRecords);
            _totalPages = Math.Max(1, (int)Math.Ceiling(_totalRecords / (double)PageSize));

            if (_currentPage > _totalPages)
            {
                _currentPage = _totalPages;
                _employees = _nhanVienBUS.LayDanhSachNhanVien(txtSearch.Text.Trim(), _currentPage, PageSize, out _totalRecords);
            }

            var displayData = _employees.Select(e => new
            {
                MaNV = e.MaNV,
                HoTen = e.TenNV,
                NgaySinh = e.NgaySinh.ToString("dd/MM/yyyy"),
                GioiTinh = e.GioiTinh,
                ChucVu = e.ChucVu,
                SoDienThoai = e.SoDienThoai,
                Email = e.Email,
                NgayVaoLam = e.NgayVaoLam.ToString("dd/MM/yyyy"),
                NgayChamCong = e.NgayChamCong?.ToString("dd/MM/yyyy") ?? string.Empty,
                GioVao = e.GioVao?.ToString(@"hh\:mm") ?? string.Empty,
                GioRa = e.GioRa?.ToString(@"hh\:mm") ?? string.Empty,
                TrangThai = e.TrangThai ? "Đang làm" : "Nghỉ việc",
                PhongBan = e.PhongBan
            }).OrderBy(x => x.MaNV).ToList();

            gridEmployees.DataSource = null;
            gridEmployees.DataSource = displayData;
            ConfigureGridHeaders();

            UpdatePagingControls();
        }

        private void ConfigureGridHeaders()
        {
            if (gridEmployees.Columns.Count == 0)
            {
                return;
            }

            gridEmployees.Columns["MaNV"].HeaderText = "Mã NV";
            gridEmployees.Columns["HoTen"].HeaderText = "Họ Tên";
            gridEmployees.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            gridEmployees.Columns["GioiTinh"].HeaderText = "Giới Tính";
            gridEmployees.Columns["ChucVu"].HeaderText = "Chức Vụ";
            gridEmployees.Columns["SoDienThoai"].HeaderText = "SĐT";
            gridEmployees.Columns["NgayVaoLam"].HeaderText = "Ngày Vào Làm";
            gridEmployees.Columns["NgayChamCong"].HeaderText = "Ngày Chấm Công";
            gridEmployees.Columns["GioVao"].HeaderText = "Giờ Vào";
            gridEmployees.Columns["GioRa"].HeaderText = "Giờ Ra";
            gridEmployees.Columns["TrangThai"].HeaderText = "Trạng Thái";
            gridEmployees.Columns["PhongBan"].HeaderText = "Phòng Ban";
        }

        private void ClearInputs()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            dtpDob.Value = DateTime.Now;
            dtpStartDate.Value = DateTime.Now;
            cbGender.SelectedIndex = 0;
            chkIsActive.Checked = true;
            if (cbDept.Items.Count > 0)
            {
                cbDept.SelectedIndex = 0;
            }

            txtName.Focus();
        }

        private void gridEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (gridEmployees.CurrentRow?.Cells[EmployeeIdColumnName].Value == null)
            {
                return;
            }

            var maNV = gridEmployees.CurrentRow.Cells[EmployeeIdColumnName].Value?.ToString() ?? string.Empty;
            var emp = _employees.FirstOrDefault(x => x.MaNV.Equals(maNV, StringComparison.OrdinalIgnoreCase));
            if (emp == null)
            {
                return;
            }

            txtId.Text = emp.MaNV;
            txtName.Text = emp.TenNV;
            dtpDob.Value = emp.NgaySinh;
            cbGender.SelectedItem = emp.GioiTinh;
            txtPosition.Text = emp.ChucVu;
            txtPhone.Text = emp.SoDienThoai;
            txtEmail.Text = emp.Email;
            txtAddress.Text = emp.DiaChi;
            dtpStartDate.Value = emp.NgayVaoLam;
            chkIsActive.Checked = emp.TrangThai;

            if (!string.IsNullOrWhiteSpace(emp.PhongBan) && _departments.Contains(emp.PhongBan))
            {
                cbDept.SelectedItem = emp.PhongBan;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryReadInput(out var nhanVien))
            {
                return;
            }

            nhanVien.MaNV = _nhanVienBUS.TaoMaNhanVienMoi();
            if (!_nhanVienBUS.ThemNhanVien(nhanVien))
            {
                MessageBox.Show("Không thể thêm nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadGridData();
            if (_totalPages > _currentPage)
            {
                _currentPage = _totalPages;
                LoadGridData();
            }
            SelectEmployeeRow(nhanVien.MaNV);
            MessageBox.Show("Thêm thành công!", "Thông báo");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var maNV = txtId.Text.Trim();
            if (string.IsNullOrWhiteSpace(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryReadInput(out var nhanVien, maNV))
            {
                return;
            }

            nhanVien.MaNV = maNV;
            var current = _employees.FirstOrDefault(x => x.MaNV.Equals(maNV, StringComparison.OrdinalIgnoreCase));
            if (current != null)
            {
                nhanVien.NgayChamCong = current.NgayChamCong;
                nhanVien.GioVao = current.GioVao;
                nhanVien.GioRa = current.GioRa;
            }

            if (!_nhanVienBUS.CapNhatNhanVien(nhanVien))
            {
                MessageBox.Show("Không thể cập nhật nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadGridData();
            SelectEmployeeRow(maNV);
            MessageBox.Show("Cập nhật thành công!", "Thông báo");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var maNV = txtId.Text.Trim();
            if (string.IsNullOrWhiteSpace(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            if (!_nhanVienBUS.XoaNhanVien(maNV))
            {
                MessageBox.Show("Không thể xóa nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadGridData();
            ClearInputs();
            MessageBox.Show("Xóa thành công!", "Thông báo");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var dataToExport = _employees.OrderBy(x => x.MaNV).ToList();
            if (dataToExport.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = $"NhanVien_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                Title = "Xuất danh sách nhân viên"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("MaNV,TenNV,NgaySinh,GioiTinh,ChucVu,SoDienThoai,Email,DiaChi,NgayVaoLam,TrangThai,PhongBan");

            foreach (var eData in dataToExport)
            {
                sb.AppendLine(string.Join(',',
                    EscapeCsv(eData.MaNV),
                    EscapeCsv(eData.TenNV),
                    EscapeCsv(eData.NgaySinh.ToString("dd/MM/yyyy")),
                    EscapeCsv(eData.GioiTinh),
                    EscapeCsv(eData.ChucVu),
                    EscapeCsv(eData.SoDienThoai),
                    EscapeCsv(eData.Email),
                    EscapeCsv(eData.DiaChi),
                    EscapeCsv(eData.NgayVaoLam.ToString("dd/MM/yyyy")),
                    EscapeCsv(eData.TrangThai ? "Đang làm" : "Nghỉ việc"),
                    EscapeCsv(eData.PhongBan)));
            }

            File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(true));
            MessageBox.Show("Xuất CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string EscapeCsv(string value)
        {
            if (value.Contains('"'))
            {
                value = value.Replace("\"", "\"\"");
            }

            return value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0 ? $"\"{value}\"" : value;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadGridData(resetPage: true);
        }

        private void InitializePagingControls()
        {
            _pnlPaging.Dock = DockStyle.Bottom;
            _pnlPaging.Height = 42;
            _pnlPaging.Padding = new Padding(8, 6, 8, 6);

            _lblPageInfo.Dock = DockStyle.Left;
            _lblPageInfo.TextAlign = ContentAlignment.MiddleLeft;
            _lblPageInfo.AutoSize = false;
            _lblPageInfo.Width = 320;

            _btnNextPage.Dock = DockStyle.Right;
            _btnNextPage.Width = 90;
            _btnNextPage.Text = "Sau >";
            _btnNextPage.Click += BtnNextPage_Click;

            _btnPrevPage.Dock = DockStyle.Right;
            _btnPrevPage.Width = 90;
            _btnPrevPage.Text = "< Trước";
            _btnPrevPage.Click += BtnPrevPage_Click;

            _pnlPaging.Controls.Add(_btnNextPage);
            _pnlPaging.Controls.Add(_btnPrevPage);
            _pnlPaging.Controls.Add(_lblPageInfo);
            Controls.Add(_pnlPaging);
        }

        private void BtnPrevPage_Click(object? sender, EventArgs e)
        {
            if (_currentPage <= 1)
            {
                return;
            }

            _currentPage--;
            LoadGridData();
        }

        private void BtnNextPage_Click(object? sender, EventArgs e)
        {
            if (_currentPage >= _totalPages)
            {
                return;
            }

            _currentPage++;
            LoadGridData();
        }

        private void UpdatePagingControls()
        {
            _lblPageInfo.Text = $"Trang {_currentPage}/{_totalPages} - Tổng {_totalRecords} nhân viên";
            _btnPrevPage.Enabled = _currentPage > 1;
            _btnNextPage.Enabled = _currentPage < _totalPages;
        }

        private bool TryReadInput(out NhanVienDTO nhanVien, string? editingMaNV = null)
        {
            nhanVien = new NhanVienDTO
            {
                TenNV = txtName.Text.Trim(),
                NgaySinh = dtpDob.Value.Date,
                GioiTinh = cbGender.SelectedItem?.ToString() ?? "Nam",
                ChucVu = txtPosition.Text.Trim(),
                SoDienThoai = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                DiaChi = txtAddress.Text.Trim(),
                NgayVaoLam = dtpStartDate.Value.Date,
                TrangThai = chkIsActive.Checked,
                PhongBan = cbDept.SelectedItem?.ToString()?.Trim() ?? string.Empty,
                LuongCung = 0
            };

            if (string.IsNullOrWhiteSpace(nhanVien.TenNV))
            {
                MessageBox.Show("Tên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (nhanVien.NgaySinh > DateTime.Today)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(nhanVien.ChucVu))
            {
                MessageBox.Show("Chức vụ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPosition.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nhanVien.SoDienThoai) || nhanVien.SoDienThoai.Length is < 10 or > 11 || !nhanVien.SoDienThoai.All(char.IsDigit))
            {
                MessageBox.Show("SĐT phải gồm 10-11 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nhanVien.Email) || !IsValidEmail(nhanVien.Email))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (nhanVien.NgayVaoLam < nhanVien.NgaySinh)
            {
                MessageBox.Show("Ngày vào làm phải sau ngày sinh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(nhanVien.PhongBan))
            {
                MessageBox.Show("Vui lòng chọn phòng ban.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbDept.Focus();
                return false;
            }

            var biTrung = _nhanVienBUS.KiemTraTrungSoDienThoaiHoacEmail(nhanVien.SoDienThoai, nhanVien.Email, editingMaNV);
            if (biTrung)
            {
                MessageBox.Show("SĐT hoặc email đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                _ = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnAddDept_Click(object sender, EventArgs e)
        {
            var deptName = PromptText("Nhập tên phòng ban mới:", "Thêm phòng ban");
            if (string.IsNullOrWhiteSpace(deptName))
            {
                return;
            }

            deptName = deptName.Trim();

            if (_departments.Any(d => d.Equals(deptName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Tên phòng ban đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_nhanVienBUS.ThemPhongBan(deptName))
            {
                MessageBox.Show("Không thể thêm phòng ban.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadCombobox();
            cbDept.SelectedItem = deptName;
        }

        private void btnEditDept_Click(object sender, EventArgs e)
        {
            if (cbDept.SelectedItem is not string selectedDepartment)
            {
                return;
            }

            var deptName = PromptText("Cập nhật tên phòng ban:", "Sửa phòng ban", selectedDepartment);
            if (string.IsNullOrWhiteSpace(deptName))
            {
                return;
            }

            if (_departments.Any(d => !d.Equals(selectedDepartment, StringComparison.OrdinalIgnoreCase) && d.Equals(deptName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Tên phòng ban đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_nhanVienBUS.DoiTenPhongBan(selectedDepartment, deptName.Trim()))
            {
                MessageBox.Show("Không thể cập nhật phòng ban.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadCombobox();
            cbDept.SelectedItem = deptName.Trim();
            LoadGridData();
        }

        private void btnDeleteDept_Click(object sender, EventArgs e)
        {
            if (cbDept.SelectedItem is not string selectedDepartment)
            {
                return;
            }

            if (_nhanVienBUS.KiemTraPhongBanDangDuocSuDung(selectedDepartment))
            {
                MessageBox.Show("Không thể xóa phòng ban đang có nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Xóa phòng ban '{selectedDepartment}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            if (!_nhanVienBUS.XoaPhongBan(selectedDepartment))
            {
                MessageBox.Show("Không thể xóa phòng ban.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadCombobox();
        }

        private static string PromptText(string message, string title, string defaultValue = "")
        {
            using var form = new Form();
            using var lbl = new Label();
            using var txt = new TextBox();
            using var btnOk = new Button();
            using var btnCancel = new Button();

            form.Text = title;
            form.ClientSize = new Size(420, 140);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.StartPosition = FormStartPosition.CenterParent;

            lbl.Text = message;
            lbl.SetBounds(12, 12, 392, 20);

            txt.Text = defaultValue;
            txt.SetBounds(12, 40, 392, 27);

            btnOk.Text = "OK";
            btnOk.DialogResult = DialogResult.OK;
            btnOk.SetBounds(248, 90, 75, 30);

            btnCancel.Text = "Hủy";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.SetBounds(329, 90, 75, 30);

            form.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            return form.ShowDialog() == DialogResult.OK ? txt.Text.Trim() : string.Empty;
        }

        private void SelectEmployeeRow(string maNV)
        {
            foreach (DataGridViewRow row in gridEmployees.Rows)
            {
                if (row.Cells[EmployeeIdColumnName].Value == null)
                {
                    continue;
                }

                if (!string.Equals(row.Cells[EmployeeIdColumnName].Value?.ToString(), maNV, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                row.Selected = true;
                gridEmployees.CurrentCell = row.Cells[0];
                return;
            }
        }

        private void ApplyResponsiveLayout()
        {
            const int margin = 24;
            const int labelWidth = 110;
            const int inputHeight = 27;
            const int row1Top = 33;
            const int row2Top = 77;
            const int row3Top = 121;
            const int row4Top = 165;
            const int row5Top = 209;
            const int row6Top = 253;
            const int buttonTop = 280;
            const int buttonWidth = 110;
            const int buttonGap = 12;
            const int columnGap = 32;
            const int minColumnWidth = 250;
            const int deptButtonWidth = 42;
            const int deptButtonGap = 6;

            var availableWidth = grpInput.ClientSize.Width - (margin * 2);
            if (availableWidth <= 0)
            {
                return;
            }

            var columnWidth = (availableWidth - columnGap) / 2;
            if (columnWidth < minColumnWidth)
            {
                columnWidth = minColumnWidth;
            }

            var inputWidth = columnWidth - labelWidth;
            if (inputWidth < 120)
            {
                inputWidth = 120;
            }

            var leftLabelX = margin;
            var leftInputX = leftLabelX + labelWidth;

            var rightLabelX = leftLabelX + columnWidth + columnGap;
            var rightInputX = rightLabelX + labelWidth;

            lblId.SetBounds(leftLabelX, row1Top + 3, labelWidth, lblId.Height);
            txtId.SetBounds(leftInputX, row1Top, inputWidth, inputHeight);

            lblName.SetBounds(leftLabelX, row2Top + 3, labelWidth, lblName.Height);
            txtName.SetBounds(leftInputX, row2Top, inputWidth, inputHeight);

            lblDob.SetBounds(leftLabelX, row3Top + 3, labelWidth, lblDob.Height);
            dtpDob.SetBounds(leftInputX, row3Top, inputWidth, inputHeight);

            lblPhone.SetBounds(leftLabelX, row4Top + 3, labelWidth, lblPhone.Height);
            txtPhone.SetBounds(leftInputX, row4Top, inputWidth, inputHeight);

            lblEmail.SetBounds(leftLabelX, row5Top + 3, labelWidth, lblEmail.Height);
            txtEmail.SetBounds(leftInputX, row5Top, inputWidth, inputHeight);

            lblPosition.SetBounds(rightLabelX, row1Top + 3, labelWidth, lblPosition.Height);
            txtPosition.SetBounds(rightInputX, row1Top, inputWidth, inputHeight);

            lblDept.SetBounds(rightLabelX, row2Top + 3, labelWidth, lblDept.Height);
            var deptButtonsWidth = (deptButtonWidth * 3) + (deptButtonGap * 2);
            cbDept.SetBounds(rightInputX, row2Top, inputWidth - deptButtonsWidth - deptButtonGap, inputHeight + 1);
            btnAddDept.SetBounds(cbDept.Right + deptButtonGap, row2Top - 1, deptButtonWidth, 30);
            btnEditDept.SetBounds(btnAddDept.Right + deptButtonGap, row2Top - 1, deptButtonWidth, 30);
            btnDeleteDept.SetBounds(btnEditDept.Right + deptButtonGap, row2Top - 1, deptButtonWidth, 30);

            lblGender.SetBounds(rightLabelX, row3Top + 3, labelWidth, lblGender.Height);
            cbGender.SetBounds(rightInputX, row3Top, inputWidth, inputHeight + 1);

            lblStartDate.SetBounds(rightLabelX, row4Top + 3, labelWidth, lblStartDate.Height);
            dtpStartDate.SetBounds(rightInputX, row4Top, inputWidth, inputHeight);

            lblStatus.SetBounds(rightLabelX, row5Top + 3, labelWidth, lblStatus.Height);
            chkIsActive.SetBounds(rightInputX, row5Top + 2, inputWidth, inputHeight);

            lblAddress.SetBounds(leftLabelX, row6Top + 3, labelWidth, lblAddress.Height);
            txtAddress.SetBounds(leftInputX, row6Top, (rightInputX + inputWidth) - leftInputX, inputHeight);

            var totalButtonWidth = (buttonWidth * 5) + (buttonGap * 4);
            var buttonStartX = Math.Max(rightInputX + inputWidth - totalButtonWidth, margin);

            btnAdd.SetBounds(buttonStartX, buttonTop, buttonWidth, 36);
            btnEdit.SetBounds(btnAdd.Right + buttonGap, buttonTop, buttonWidth, 36);
            btnDelete.SetBounds(btnEdit.Right + buttonGap, buttonTop, buttonWidth, 36);
            btnClear.SetBounds(btnDelete.Right + buttonGap, buttonTop, buttonWidth, 36);
            btnExport.SetBounds(btnClear.Right + buttonGap, buttonTop, buttonWidth, 36);
        }

    }
}
