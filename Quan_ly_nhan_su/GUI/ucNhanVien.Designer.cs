namespace Quan_ly_nhan_su.GUI
{
    partial class ucNhanVien
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpInput = new GroupBox();
            lblStatus = new Label();
            chkIsActive = new CheckBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblStartDate = new Label();
            dtpStartDate = new DateTimePicker();
            lblGender = new Label();
            cbGender = new ComboBox();
            btnDeleteDept = new Button();
            btnEditDept = new Button();
            btnAddDept = new Button();
            btnClear = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            cbDept = new ComboBox();
            lblDept = new Label();
            txtPosition = new TextBox();
            lblPosition = new Label();
            dtpDob = new DateTimePicker();
            lblDob = new Label();
            txtName = new TextBox();
            lblName = new Label();
            txtId = new TextBox();
            lblId = new Label();
            pnlSearch = new Panel();
            txtSearch = new TextBox();
            lblSearch = new Label();
            gridEmployees = new DataGridView();
            grpInput.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridEmployees).BeginInit();
            SuspendLayout();
            // 
            // grpInput
            // 
            grpInput.Controls.Add(lblStatus);
            grpInput.Controls.Add(chkIsActive);
            grpInput.Controls.Add(lblAddress);
            grpInput.Controls.Add(txtAddress);
            grpInput.Controls.Add(lblEmail);
            grpInput.Controls.Add(txtEmail);
            grpInput.Controls.Add(lblPhone);
            grpInput.Controls.Add(txtPhone);
            grpInput.Controls.Add(lblStartDate);
            grpInput.Controls.Add(dtpStartDate);
            grpInput.Controls.Add(lblGender);
            grpInput.Controls.Add(cbGender);
            grpInput.Controls.Add(btnDeleteDept);
            grpInput.Controls.Add(btnEditDept);
            grpInput.Controls.Add(btnAddDept);
            grpInput.Controls.Add(btnClear);
            grpInput.Controls.Add(btnDelete);
            grpInput.Controls.Add(btnEdit);
            grpInput.Controls.Add(btnAdd);
            grpInput.Controls.Add(cbDept);
            grpInput.Controls.Add(lblDept);
            grpInput.Controls.Add(txtPosition);
            grpInput.Controls.Add(lblPosition);
            grpInput.Controls.Add(dtpDob);
            grpInput.Controls.Add(lblDob);
            grpInput.Controls.Add(txtName);
            grpInput.Controls.Add(lblName);
            grpInput.Controls.Add(txtId);
            grpInput.Controls.Add(lblId);
            grpInput.Dock = DockStyle.Top;
            grpInput.Location = new Point(0, 0);
            grpInput.Name = "grpInput";
            grpInput.Size = new Size(1804, 380);
            grpInput.TabIndex = 0;
            grpInput.TabStop = false;
            grpInput.Text = "Thông tin nhân viên";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(737, 212);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(78, 20);
            lblStatus.TabIndex = 28;
            lblStatus.Text = "Trạng thái";
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(874, 210);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(86, 24);
            chkIsActive.TabIndex = 27;
            chkIsActive.Text = "Đang làm";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(24, 256);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(55, 20);
            lblAddress.TabIndex = 26;
            lblAddress.Text = "Địa chỉ";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(149, 253);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(1257, 27);
            txtAddress.TabIndex = 25;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(24, 212);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 24;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(149, 209);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(532, 27);
            txtEmail.TabIndex = 23;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(24, 168);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(41, 20);
            lblPhone.TabIndex = 22;
            lblPhone.Text = "SĐT";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(149, 165);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(532, 27);
            txtPhone.TabIndex = 21;
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Location = new Point(737, 168);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(96, 20);
            lblStartDate.TabIndex = 20;
            lblStartDate.Text = "Ngày vào làm";
            // 
            // dtpStartDate
            // 
            dtpStartDate.CustomFormat = "dd/MM/yyyy";
            dtpStartDate.Format = DateTimePickerFormat.Custom;
            dtpStartDate.Location = new Point(874, 165);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(532, 27);
            dtpStartDate.TabIndex = 19;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(737, 124);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(65, 20);
            lblGender.TabIndex = 18;
            lblGender.Text = "Giới tính";
            // 
            // cbGender
            // 
            cbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGender.FormattingEnabled = true;
            cbGender.Location = new Point(874, 121);
            cbGender.Name = "cbGender";
            cbGender.Size = new Size(532, 28);
            cbGender.TabIndex = 17;
            // 
            // btnDeleteDept
            // 
            btnDeleteDept.Location = new Point(1364, 76);
            btnDeleteDept.Name = "btnDeleteDept";
            btnDeleteDept.Size = new Size(42, 30);
            btnDeleteDept.TabIndex = 16;
            btnDeleteDept.Text = "-";
            btnDeleteDept.UseVisualStyleBackColor = true;
            btnDeleteDept.Click += btnDeleteDept_Click;
            // 
            // btnEditDept
            // 
            btnEditDept.Location = new Point(1316, 76);
            btnEditDept.Name = "btnEditDept";
            btnEditDept.Size = new Size(42, 30);
            btnEditDept.TabIndex = 15;
            btnEditDept.Text = "✎";
            btnEditDept.UseVisualStyleBackColor = true;
            btnEditDept.Click += btnEditDept_Click;
            // 
            // btnAddDept
            // 
            btnAddDept.Location = new Point(1268, 76);
            btnAddDept.Name = "btnAddDept";
            btnAddDept.Size = new Size(42, 30);
            btnAddDept.TabIndex = 14;
            btnAddDept.Text = "+";
            btnAddDept.UseVisualStyleBackColor = true;
            btnAddDept.Click += btnAddDept_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(1286, 294);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(120, 36);
            btnClear.TabIndex = 13;
            btnClear.Text = "Làm mới";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1149, 294);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 36);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(1012, 294);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(120, 36);
            btnEdit.TabIndex = 11;
            btnEdit.Text = "Sửa";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(875, 294);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 36);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // cbDept
            // 
            cbDept.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDept.FormattingEnabled = true;
            cbDept.Location = new Point(874, 77);
            cbDept.Name = "cbDept";
            cbDept.Size = new Size(532, 28);
            cbDept.TabIndex = 9;
            // 
            // lblDept
            // 
            lblDept.AutoSize = true;
            lblDept.Location = new Point(737, 80);
            lblDept.Name = "lblDept";
            lblDept.Size = new Size(76, 20);
            lblDept.TabIndex = 8;
            lblDept.Text = "Phòng ban";
            // 
            // txtPosition
            // 
            txtPosition.Location = new Point(874, 33);
            txtPosition.Name = "txtPosition";
            txtPosition.Size = new Size(532, 27);
            txtPosition.TabIndex = 7;
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Location = new Point(737, 36);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(61, 20);
            lblPosition.TabIndex = 6;
            lblPosition.Text = "Chức vụ";
            // 
            // dtpDob
            // 
            dtpDob.CustomFormat = "dd/MM/yyyy";
            dtpDob.Format = DateTimePickerFormat.Custom;
            dtpDob.Location = new Point(149, 121);
            dtpDob.Name = "dtpDob";
            dtpDob.Size = new Size(532, 27);
            dtpDob.TabIndex = 5;
            // 
            // lblDob
            // 
            lblDob.AutoSize = true;
            lblDob.Location = new Point(24, 126);
            lblDob.Name = "lblDob";
            lblDob.Size = new Size(74, 20);
            lblDob.TabIndex = 4;
            lblDob.Text = "Ngày sinh";
            // 
            // txtName
            // 
            txtName.Location = new Point(149, 77);
            txtName.Name = "txtName";
            txtName.Size = new Size(532, 27);
            txtName.TabIndex = 3;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(24, 80);
            lblName.Name = "lblName";
            lblName.Size = new Size(54, 20);
            lblName.TabIndex = 2;
            lblName.Text = "Họ tên";
            // 
            // txtId
            // 
            txtId.Location = new Point(149, 33);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(532, 27);
            txtId.TabIndex = 1;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(24, 36);
            lblId.Name = "lblId";
            lblId.Size = new Size(51, 20);
            lblId.TabIndex = 0;
            lblId.Text = "Mã NV";
            // 
            // gridEmployees
            // 
            gridEmployees.AllowUserToAddRows = false;
            gridEmployees.AllowUserToDeleteRows = false;
            gridEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridEmployees.Dock = DockStyle.Fill;
            gridEmployees.Location = new Point(0, 392);
            gridEmployees.MultiSelect = false;
            gridEmployees.Name = "gridEmployees";
            gridEmployees.ReadOnly = true;
            gridEmployees.RowHeadersWidth = 51;
            gridEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridEmployees.Size = new Size(1804, 321);
            gridEmployees.TabIndex = 1;
            gridEmployees.SelectionChanged += gridEmployees_SelectionChanged;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 380);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(1804, 52);
            pnlSearch.TabIndex = 2;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(149, 13);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nhập tên, chức vụ, SĐT, email...";
            txtSearch.Size = new Size(1257, 27);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(24, 16);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(70, 20);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Tìm kiếm";
            // 
            // ucNhanVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridEmployees);
            Controls.Add(pnlSearch);
            Controls.Add(grpInput);
            Name = "ucNhanVien";
            Size = new Size(1804, 713);
            grpInput.ResumeLayout(false);
            grpInput.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridEmployees).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpInput;
        private Label lblId;
        private TextBox txtId;
        private Label lblName;
        private TextBox txtName;
        private Label lblDob;
        private DateTimePicker dtpDob;
        private Label lblPosition;
        private TextBox txtPosition;
        private Label lblDept;
        private ComboBox cbDept;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClear;
        private DataGridView gridEmployees;
        private Button btnAddDept;
        private Button btnEditDept;
        private Button btnDeleteDept;
        private Label lblGender;
        private ComboBox cbGender;
        private Label lblStartDate;
        private DateTimePicker dtpStartDate;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblStatus;
        private CheckBox chkIsActive;
        private Panel pnlSearch;
        private TextBox txtSearch;
        private Label lblSearch;
    }
}
