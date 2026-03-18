using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Quan_ly_nhan_su.GUI
{
    public partial class ucNhanVien : UserControl
    {
        private class Employee
        {
            public int Id { get; set; }
            public string FullName { get; set; } = string.Empty;
            public DateTime DateOfBirth { get; set; }
            public string Gender { get; set; } = "Nam";
            public string Position { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public DateTime StartDate { get; set; }
            public bool IsActive { get; set; } = true;
            public int DepartmentId { get; set; }
        }

        private class Department
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        private readonly List<Department> _departments = new();
        private readonly List<Employee> _employees = new();

        private const string EmployeeIdColumnName = "Mã_NV";

        public ucNhanVien()
        {
            InitializeComponent();
            // ApplyTheme(); // Bỏ code can thiệp màu để nhận chỉnh sửa từ Properties (Designer)
            InitMockData();
            LoadCombobox();
            LoadGridData();
            ClearInputs();

            SizeChanged += ucNhanVien_SizeChanged;
            ApplyResponsiveLayout();
        }

        private void ApplyTheme()
        {
            var primary = Color.SteelBlue;
            var primaryDark = Color.FromArgb(46, 102, 153);
            var danger = Color.IndianRed;
            var success = Color.SeaGreen;
            var neutral = Color.SlateGray;

            grpInput.ForeColor = primaryDark;
            pnlSearch.BackColor = Color.FromArgb(245, 249, 253);

            btnAdd.FillColor = success;
            btnEdit.FillColor = primary;
            btnDelete.FillColor = danger;
            btnClear.FillColor = neutral;
            btnExport.FillColor = Color.MediumPurple;

            btnAddDept.FillColor = success;
            btnEditDept.FillColor = primary;
            btnDeleteDept.FillColor = danger;

            cbDept.BorderColor = primary;
            cbGender.BorderColor = primary;

            txtId.BorderColor = primary;
            txtName.BorderColor = primary;
            txtPosition.BorderColor = primary;
            txtPhone.BorderColor = primary;
            txtEmail.BorderColor = primary;
            txtAddress.BorderColor = primary;
            txtSearch.BorderColor = primary;

            dtpDob.BorderColor = primary;
            dtpDob.FillColor = Color.White;
            dtpStartDate.BorderColor = primary;
            dtpStartDate.FillColor = Color.White;

            gridEmployees.ColumnHeadersDefaultCellStyle.BackColor = primary;
            gridEmployees.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridEmployees.EnableHeadersVisualStyles = false;
            gridEmployees.GridColor = Color.FromArgb(220, 230, 241);
            gridEmployees.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 255);
        }

        private void ucNhanVien_SizeChanged(object? sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void InitMockData()
        {
            _departments.Add(new Department { Id = 1, Name = "Phòng IT" });
            _departments.Add(new Department { Id = 2, Name = "Phòng Kế toán" });
            _departments.Add(new Department { Id = 3, Name = "Phòng Hành chính" });

            _employees.Add(new Employee
            {
                Id = 1,
                FullName = "Nguyễn Văn A",
                DateOfBirth = new DateTime(1995, 5, 20),
                Gender = "Nam",
                Position = "Dev",
                PhoneNumber = "0912345678",
                Email = "a.nguyen@company.com",
                Address = "Quận 1, TP.HCM",
                StartDate = new DateTime(2021, 1, 10),
                IsActive = true,
                DepartmentId = 1
            });

            _employees.Add(new Employee
            {
                Id = 2,
                FullName = "Trần Thị B",
                DateOfBirth = new DateTime(1998, 8, 15),
                Gender = "Nữ",
                Position = "Kế toán trưởng",
                PhoneNumber = "0987654321",
                Email = "b.tran@company.com",
                Address = "Quận 3, TP.HCM",
                StartDate = new DateTime(2022, 6, 1),
                IsActive = true,
                DepartmentId = 2
            });
        }

        private void LoadCombobox()
        {
            cbDept.DataSource = _departments;
            cbDept.DisplayMember = nameof(Department.Name);
            cbDept.ValueMember = nameof(Department.Id);

            cbGender.DataSource = new List<string> { "Nam", "Nữ", "Khác" };
        }

        private void LoadGridData()
        {
            var filteredEmployees = GetFilteredEmployees();

            var displayData = filteredEmployees.Select(e => new
            {
                Mã_NV = e.Id,
                Họ_Tên = e.FullName,
                Ngày_Sinh = e.DateOfBirth.ToString("dd/MM/yyyy"),
                Giới_Tính = e.Gender,
                Chức_Vụ = e.Position,
                SĐT = e.PhoneNumber,
                Email = e.Email,
                Ngày_Vào_Làm = e.StartDate.ToString("dd/MM/yyyy"),
                Trạng_Thái = e.IsActive ? "Đang làm" : "Nghỉ việc",
                Phòng_Ban = _departments.FirstOrDefault(d => d.Id == e.DepartmentId)?.Name
            }).OrderBy(x => x.Mã_NV).ToList();

            gridEmployees.DataSource = null;
            gridEmployees.DataSource = displayData;
        }

        private IEnumerable<Employee> GetFilteredEmployees()
        {
            var keyword = txtSearch.Text.Trim();
            return _employees.Where(e =>
                string.IsNullOrWhiteSpace(keyword) ||
                e.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                e.Position.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                e.PhoneNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                e.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                e.Address.Contains(keyword, StringComparison.OrdinalIgnoreCase));
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

            var id = Convert.ToInt32(gridEmployees.CurrentRow.Cells[EmployeeIdColumnName].Value);
            var emp = _employees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return;
            }

            txtId.Text = emp.Id.ToString();
            txtName.Text = emp.FullName;
            dtpDob.Value = emp.DateOfBirth;
            cbGender.SelectedItem = emp.Gender;
            txtPosition.Text = emp.Position;
            txtPhone.Text = emp.PhoneNumber;
            txtEmail.Text = emp.Email;
            txtAddress.Text = emp.Address;
            dtpStartDate.Value = emp.StartDate;
            chkIsActive.Checked = emp.IsActive;
            cbDept.SelectedValue = emp.DepartmentId;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryReadInput(out var fullName, out var dateOfBirth, out var gender, out var position, out var phoneNumber,
                out var email, out var address, out var startDate, out var isActive, out var departmentId))
            {
                return;
            }

            var newId = _employees.Count > 0 ? _employees.Max(x => x.Id) + 1 : 1;
            var newEmp = new Employee
            {
                Id = newId,
                FullName = fullName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Position = position,
                PhoneNumber = phoneNumber,
                Email = email,
                Address = address,
                StartDate = startDate,
                IsActive = isActive,
                DepartmentId = departmentId
            };

            _employees.Add(newEmp);
            LoadGridData();
            SelectEmployeeRow(newId);
            MessageBox.Show("Thêm thành công!", "Thông báo");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out var id))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryReadInput(out var fullName, out var dateOfBirth, out var gender, out var position, out var phoneNumber,
                out var email, out var address, out var startDate, out var isActive, out var departmentId, id))
            {
                return;
            }

            var emp = _employees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            emp.FullName = fullName;
            emp.DateOfBirth = dateOfBirth;
            emp.Gender = gender;
            emp.Position = position;
            emp.PhoneNumber = phoneNumber;
            emp.Email = email;
            emp.Address = address;
            emp.StartDate = startDate;
            emp.IsActive = isActive;
            emp.DepartmentId = departmentId;

            LoadGridData();
            SelectEmployeeRow(id);
            MessageBox.Show("Cập nhật thành công!", "Thông báo");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out var id))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            var emp = _employees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return;
            }

            _employees.Remove(emp);
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
            var dataToExport = GetFilteredEmployees().OrderBy(x => x.Id).ToList();
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
            sb.AppendLine("MaNV,HoTen,NgaySinh,GioiTinh,ChucVu,SoDienThoai,Email,DiaChi,NgayVaoLam,TrangThai,PhongBan");

            foreach (var eData in dataToExport)
            {
                var departmentName = _departments.FirstOrDefault(d => d.Id == eData.DepartmentId)?.Name ?? string.Empty;
                sb.AppendLine(string.Join(',',
                    EscapeCsv(eData.Id.ToString()),
                    EscapeCsv(eData.FullName),
                    EscapeCsv(eData.DateOfBirth.ToString("dd/MM/yyyy")),
                    EscapeCsv(eData.Gender),
                    EscapeCsv(eData.Position),
                    EscapeCsv(eData.PhoneNumber),
                    EscapeCsv(eData.Email),
                    EscapeCsv(eData.Address),
                    EscapeCsv(eData.StartDate.ToString("dd/MM/yyyy")),
                    EscapeCsv(eData.IsActive ? "Dang lam" : "Nghi viec"),
                    EscapeCsv(departmentName)));
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
            LoadGridData();
        }

        private bool TryReadInput(
            out string fullName,
            out DateTime dateOfBirth,
            out string gender,
            out string position,
            out string phoneNumber,
            out string email,
            out string address,
            out DateTime startDate,
            out bool isActive,
            out int departmentId,
            int? editingEmployeeId = null)
        {
            fullName = txtName.Text.Trim();
            dateOfBirth = dtpDob.Value.Date;
            gender = cbGender.SelectedItem?.ToString() ?? "Nam";
            position = txtPosition.Text.Trim();
            phoneNumber = txtPhone.Text.Trim();
            email = txtEmail.Text.Trim();
            address = txtAddress.Text.Trim();
            startDate = dtpStartDate.Value.Date;
            isActive = chkIsActive.Checked;
            departmentId = 0;

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Tên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (dateOfBirth > DateTime.Today)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(position))
            {
                MessageBox.Show("Chức vụ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPosition.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length is < 10 or > 11 || !phoneNumber.All(char.IsDigit))
            {
                MessageBox.Show("SĐT phải gồm 10-11 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (startDate < dateOfBirth)
            {
                MessageBox.Show("Ngày vào làm phải sau ngày sinh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var phoneNumberToCheck = phoneNumber;
            var emailToCheck = email;

            var duplicated = _employees.FirstOrDefault(x =>
                x.Id != editingEmployeeId &&
                (x.PhoneNumber.Equals(phoneNumberToCheck, StringComparison.OrdinalIgnoreCase)
                || x.Email.Equals(emailToCheck, StringComparison.OrdinalIgnoreCase)));

            if (duplicated != null)
            {
                MessageBox.Show("SĐT hoặc email đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbDept.SelectedValue is not int selectedDepartmentId)
            {
                MessageBox.Show("Vui lòng chọn phòng ban.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbDept.Focus();
                return false;
            }

            departmentId = selectedDepartmentId;
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

            if (_departments.Any(d => d.Name.Equals(deptName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Tên phòng ban đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newId = _departments.Count > 0 ? _departments.Max(d => d.Id) + 1 : 1;
            _departments.Add(new Department { Id = newId, Name = deptName.Trim() });
            LoadCombobox();
            cbDept.SelectedValue = newId;
        }

        private void btnEditDept_Click(object sender, EventArgs e)
        {
            if (cbDept.SelectedValue is not int selectedDepartmentId)
            {
                return;
            }

            var department = _departments.FirstOrDefault(d => d.Id == selectedDepartmentId);
            if (department == null)
            {
                return;
            }

            var deptName = PromptText("Cập nhật tên phòng ban:", "Sửa phòng ban", department.Name);
            if (string.IsNullOrWhiteSpace(deptName))
            {
                return;
            }

            if (_departments.Any(d => d.Id != selectedDepartmentId && d.Name.Equals(deptName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Tên phòng ban đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            department.Name = deptName.Trim();
            LoadCombobox();
            cbDept.SelectedValue = selectedDepartmentId;
            LoadGridData();
        }

        private void btnDeleteDept_Click(object sender, EventArgs e)
        {
            if (cbDept.SelectedValue is not int selectedDepartmentId)
            {
                return;
            }

            if (_employees.Any(e => e.DepartmentId == selectedDepartmentId))
            {
                MessageBox.Show("Không thể xóa phòng ban đang có nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var department = _departments.FirstOrDefault(d => d.Id == selectedDepartmentId);
            if (department == null)
            {
                return;
            }

            var confirm = MessageBox.Show($"Xóa phòng ban '{department.Name}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            _departments.Remove(department);
            LoadCombobox();
            LoadGridData();
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

        private void SelectEmployeeRow(int employeeId)
        {
            foreach (DataGridViewRow row in gridEmployees.Rows)
            {
                if (row.Cells[EmployeeIdColumnName].Value == null)
                {
                    continue;
                }

                if (Convert.ToInt32(row.Cells[EmployeeIdColumnName].Value) != employeeId)
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
            const int buttonTop = 294;
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

        private void pnlSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
