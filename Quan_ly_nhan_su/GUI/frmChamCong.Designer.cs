namespace Quan_ly_nhan_su.GUI
{
    partial class frmChamCong
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            label1 = new Label();
            lbDate = new Label();
            lbClock = new Label();
            pnCheckOut = new Guna.UI2.WinForms.Guna2Panel();
            icCheckOut = new FontAwesome.Sharp.IconPictureBox();
            lbCheckOut = new Label();
            pnCheckIn = new Guna.UI2.WinForms.Guna2Panel();
            icCheckIn = new FontAwesome.Sharp.IconPictureBox();
            lbCheckIn = new Label();
            guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            lbTongGio = new Label();
            lbGioRa = new Label();
            lbGioVao = new Label();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label6 = new Label();
            label14 = new Label();
            flpDanhSachChamCong = new FlowLayoutPanel();
            guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            txtNhapMaNV = new Guna.UI2.WinForms.Guna2TextBox();
            timerClock = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            pnThongTinNV = new Guna.UI2.WinForms.Guna2Panel();
            lblDiaChiNhanVien = new Label();
            lblSoDienThoai = new Label();
            lblHoTenNhanVien = new Label();
            lblDiaChi = new Label();
            lblSDT = new Label();
            lblHoTenNV = new Label();
            pnCheckOut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)icCheckOut).BeginInit();
            pnCheckIn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)icCheckIn).BeginInit();
            guna2Panel4.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnThongTinNV.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(799, 32);
            label1.Name = "label1";
            label1.Size = new Size(145, 41);
            label1.TabIndex = 0;
            label1.Text = "Chấm Công";
            // 
            // lbDate
            // 
            lbDate.Anchor = AnchorStyles.Top;
            lbDate.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbDate.ForeColor = Color.DarkGray;
            lbDate.Location = new Point(763, 103);
            lbDate.Name = "lbDate";
            lbDate.Size = new Size(216, 21);
            lbDate.TabIndex = 2;
            lbDate.Text = "Thứ Tư, 18 Tháng 3, 2026";
            lbDate.Click += lbDate_Click;
            // 
            // lbClock
            // 
            lbClock.Anchor = AnchorStyles.Top;
            lbClock.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbClock.Location = new Point(789, 133);
            lbClock.Name = "lbClock";
            lbClock.Size = new Size(164, 47);
            lbClock.TabIndex = 3;
            lbClock.Text = "08:00:00";
            lbClock.Click += lbClock_Click;
            // 
            // pnCheckOut
            // 
            pnCheckOut.BorderRadius = 10;
            pnCheckOut.BorderThickness = 1;
            pnCheckOut.Controls.Add(icCheckOut);
            pnCheckOut.Controls.Add(lbCheckOut);
            pnCheckOut.Cursor = Cursors.Hand;
            pnCheckOut.CustomizableEdges = customizableEdges1;
            pnCheckOut.Dock = DockStyle.Fill;
            pnCheckOut.FillColor = Color.FromArgb(255, 128, 0);
            pnCheckOut.Location = new Point(846, 3);
            pnCheckOut.Name = "pnCheckOut";
            pnCheckOut.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnCheckOut.Size = new Size(808, 94);
            pnCheckOut.TabIndex = 6;
            pnCheckOut.Click += pnCheckOut_Click;
            // 
            // icCheckOut
            // 
            icCheckOut.BackColor = Color.FromArgb(255, 128, 0);
            icCheckOut.IconChar = FontAwesome.Sharp.IconChar.SignOut;
            icCheckOut.IconColor = Color.White;
            icCheckOut.IconFont = FontAwesome.Sharp.IconFont.Auto;
            icCheckOut.IconSize = 40;
            icCheckOut.Location = new Point(397, 17);
            icCheckOut.Name = "icCheckOut";
            icCheckOut.Size = new Size(48, 40);
            icCheckOut.SizeMode = PictureBoxSizeMode.StretchImage;
            icCheckOut.TabIndex = 9;
            icCheckOut.TabStop = false;
            // 
            // lbCheckOut
            // 
            lbCheckOut.AutoSize = true;
            lbCheckOut.BackColor = Color.FromArgb(255, 128, 0);
            lbCheckOut.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbCheckOut.ForeColor = Color.White;
            lbCheckOut.Location = new Point(371, 60);
            lbCheckOut.Name = "lbCheckOut";
            lbCheckOut.Size = new Size(103, 25);
            lbCheckOut.TabIndex = 1;
            lbCheckOut.Text = "Check-out";
            // 
            // pnCheckIn
            // 
            pnCheckIn.BorderRadius = 10;
            pnCheckIn.BorderThickness = 1;
            pnCheckIn.Controls.Add(icCheckIn);
            pnCheckIn.Controls.Add(lbCheckIn);
            pnCheckIn.Cursor = Cursors.Hand;
            pnCheckIn.CustomizableEdges = customizableEdges3;
            pnCheckIn.Dock = DockStyle.Fill;
            pnCheckIn.FillColor = Color.MediumSeaGreen;
            pnCheckIn.Location = new Point(3, 3);
            pnCheckIn.Name = "pnCheckIn";
            pnCheckIn.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnCheckIn.Size = new Size(837, 94);
            pnCheckIn.TabIndex = 7;
            pnCheckIn.Click += pnCheckIn_Click;
            pnCheckIn.Paint += guna2Panel3_Paint;
            // 
            // icCheckIn
            // 
            icCheckIn.BackColor = Color.MediumSeaGreen;
            icCheckIn.IconChar = FontAwesome.Sharp.IconChar.SignIn;
            icCheckIn.IconColor = Color.White;
            icCheckIn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            icCheckIn.IconSize = 40;
            icCheckIn.Location = new Point(383, 17);
            icCheckIn.Name = "icCheckIn";
            icCheckIn.Size = new Size(48, 40);
            icCheckIn.SizeMode = PictureBoxSizeMode.StretchImage;
            icCheckIn.TabIndex = 8;
            icCheckIn.TabStop = false;
            // 
            // lbCheckIn
            // 
            lbCheckIn.AutoSize = true;
            lbCheckIn.BackColor = Color.MediumSeaGreen;
            lbCheckIn.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbCheckIn.ForeColor = Color.White;
            lbCheckIn.Location = new Point(358, 60);
            lbCheckIn.Name = "lbCheckIn";
            lbCheckIn.Size = new Size(89, 25);
            lbCheckIn.TabIndex = 0;
            lbCheckIn.Text = "Check-in";
            // 
            // guna2Panel4
            // 
            guna2Panel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2Panel4.BackColor = SystemColors.Control;
            guna2Panel4.BorderColor = Color.FromArgb(224, 224, 224);
            guna2Panel4.BorderRadius = 10;
            guna2Panel4.BorderThickness = 2;
            guna2Panel4.Controls.Add(lbTongGio);
            guna2Panel4.Controls.Add(lbGioRa);
            guna2Panel4.Controls.Add(lbGioVao);
            guna2Panel4.Controls.Add(iconButton2);
            guna2Panel4.Controls.Add(label10);
            guna2Panel4.Controls.Add(label9);
            guna2Panel4.Controls.Add(label8);
            guna2Panel4.Controls.Add(label6);
            guna2Panel4.CustomizableEdges = customizableEdges5;
            guna2Panel4.FillColor = Color.White;
            guna2Panel4.Location = new Point(18, 377);
            guna2Panel4.Name = "guna2Panel4";
            guna2Panel4.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Panel4.Size = new Size(1657, 91);
            guna2Panel4.TabIndex = 8;
            // 
            // lbTongGio
            // 
            lbTongGio.AutoSize = true;
            lbTongGio.BackColor = Color.White;
            lbTongGio.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTongGio.ForeColor = Color.LightSkyBlue;
            lbTongGio.Location = new Point(1322, 58);
            lbTongGio.Name = "lbTongGio";
            lbTongGio.Size = new Size(38, 21);
            lbTongGio.TabIndex = 7;
            lbTongGio.Text = "--:--";
            // 
            // lbGioRa
            // 
            lbGioRa.AutoSize = true;
            lbGioRa.BackColor = Color.White;
            lbGioRa.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbGioRa.ForeColor = Color.Gray;
            lbGioRa.Location = new Point(818, 59);
            lbGioRa.Name = "lbGioRa";
            lbGioRa.Size = new Size(45, 20);
            lbGioRa.TabIndex = 6;
            lbGioRa.Text = "  --:--";
            // 
            // lbGioVao
            // 
            lbGioVao.AutoSize = true;
            lbGioVao.BackColor = Color.White;
            lbGioVao.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbGioVao.Location = new Point(299, 59);
            lbGioVao.Name = "lbGioVao";
            lbGioVao.Size = new Size(41, 20);
            lbGioVao.TabIndex = 5;
            lbGioVao.Text = " --:--";
            lbGioVao.Click += label11_Click;
            // 
            // iconButton2
            // 
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            iconButton2.IconColor = SystemColors.MenuHighlight;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 20;
            iconButton2.Location = new Point(6, 5);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(31, 29);
            iconButton2.TabIndex = 4;
            iconButton2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.White;
            label10.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label10.ForeColor = Color.Silver;
            label10.Location = new Point(1311, 31);
            label10.Name = "label10";
            label10.Size = new Size(72, 17);
            label10.TabIndex = 3;
            label10.Text = "TỔNG GIỜ";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.White;
            label9.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label9.ForeColor = Color.Silver;
            label9.Location = new Point(818, 31);
            label9.Name = "label9";
            label9.Size = new Size(52, 17);
            label9.TabIndex = 2;
            label9.Text = "GIỜ RA";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.White;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label8.ForeColor = Color.Silver;
            label8.Location = new Point(294, 31);
            label8.Name = "label8";
            label8.Size = new Size(62, 17);
            label8.TabIndex = 1;
            label8.Text = "GIỜ VÀO";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.White;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(43, 11);
            label6.Name = "label6";
            label6.Size = new Size(129, 20);
            label6.TabIndex = 0;
            label6.Text = "Tóm tắt hôm nay";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(18, 483);
            label14.Name = "label14";
            label14.Size = new Size(151, 21);
            label14.TabIndex = 9;
            label14.Text = "Lịch sử chấm công";
            // 
            // flpDanhSachChamCong
            // 
            flpDanhSachChamCong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flpDanhSachChamCong.AutoScroll = true;
            flpDanhSachChamCong.Location = new Point(24, 531);
            flpDanhSachChamCong.Name = "flpDanhSachChamCong";
            flpDanhSachChamCong.Size = new Size(1671, 615);
            flpDanhSachChamCong.TabIndex = 11;
            // 
            // guna2Panel5
            // 
            guna2Panel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2Panel5.CustomizableEdges = customizableEdges7;
            guna2Panel5.FillColor = Color.FromArgb(224, 224, 224);
            guna2Panel5.ForeColor = Color.Cyan;
            guna2Panel5.Location = new Point(18, 79);
            guna2Panel5.Name = "guna2Panel5";
            guna2Panel5.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Panel5.Size = new Size(1677, 3);
            guna2Panel5.TabIndex = 12;
            // 
            // txtNhapMaNV
            // 
            txtNhapMaNV.Anchor = AnchorStyles.Top;
            txtNhapMaNV.BorderColor = Color.FromArgb(224, 224, 224);
            txtNhapMaNV.BorderRadius = 10;
            txtNhapMaNV.BorderThickness = 2;
            txtNhapMaNV.CustomizableEdges = customizableEdges9;
            txtNhapMaNV.DefaultText = "";
            txtNhapMaNV.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNhapMaNV.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNhapMaNV.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNhapMaNV.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNhapMaNV.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNhapMaNV.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNhapMaNV.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNhapMaNV.Location = new Point(713, 185);
            txtNhapMaNV.Margin = new Padding(4, 5, 4, 5);
            txtNhapMaNV.Name = "txtNhapMaNV";
            txtNhapMaNV.PlaceholderText = "Vui lòng nhập mã nhân viên...";
            txtNhapMaNV.SelectedText = "";
            txtNhapMaNV.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txtNhapMaNV.Size = new Size(321, 60);
            txtNhapMaNV.TabIndex = 13;
            txtNhapMaNV.TextAlign = HorizontalAlignment.Center;
            txtNhapMaNV.TextChanged += txtNhapMaNV_TextChanged;
            txtNhapMaNV.KeyDown += txtNhapMaNV_KeyDown;
            // 
            // timerClock
            // 
            timerClock.Tick += timerClock_Tick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.8750763F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.1249237F));
            tableLayoutPanel1.Controls.Add(pnCheckOut, 1, 0);
            tableLayoutPanel1.Controls.Add(pnCheckIn, 0, 0);
            tableLayoutPanel1.Location = new Point(18, 258);
            tableLayoutPanel1.Margin = new Padding(3, 3, 10, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1657, 100);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // btnBack
            // 
            btnBack.BorderColor = Color.FromArgb(224, 224, 224);
            btnBack.BorderRadius = 10;
            btnBack.BorderThickness = 2;
            btnBack.CustomizableEdges = customizableEdges11;
            btnBack.DisabledState.BorderColor = Color.DarkGray;
            btnBack.DisabledState.CustomBorderColor = Color.DarkGray;
            btnBack.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnBack.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnBack.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(18, 19);
            btnBack.Name = "btnBack";
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnBack.Size = new Size(117, 45);
            btnBack.TabIndex = 16;
            btnBack.Text = "Back";
            btnBack.Click += btnBack_Click;
            // 
            // pnThongTinNV
            // 
            pnThongTinNV.Controls.Add(lblDiaChiNhanVien);
            pnThongTinNV.Controls.Add(lblSoDienThoai);
            pnThongTinNV.Controls.Add(lblHoTenNhanVien);
            pnThongTinNV.Controls.Add(lblDiaChi);
            pnThongTinNV.Controls.Add(lblSDT);
            pnThongTinNV.Controls.Add(lblHoTenNV);
            pnThongTinNV.CustomizableEdges = customizableEdges13;
            pnThongTinNV.Location = new Point(1061, 103);
            pnThongTinNV.Name = "pnThongTinNV";
            pnThongTinNV.ShadowDecoration.CustomizableEdges = customizableEdges14;
            pnThongTinNV.Size = new Size(611, 142);
            pnThongTinNV.TabIndex = 17;
            // 
            // lblDiaChiNhanVien
            // 
            lblDiaChiNhanVien.AutoSize = true;
            lblDiaChiNhanVien.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDiaChiNhanVien.Location = new Point(98, 81);
            lblDiaChiNhanVien.Name = "lblDiaChiNhanVien";
            lblDiaChiNhanVien.Size = new Size(0, 21);
            lblDiaChiNhanVien.TabIndex = 5;
            // 
            // lblSoDienThoai
            // 
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSoDienThoai.Location = new Point(478, 21);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Size = new Size(0, 21);
            lblSoDienThoai.TabIndex = 4;
            // 
            // lblHoTenNhanVien
            // 
            lblHoTenNhanVien.AutoSize = true;
            lblHoTenNhanVien.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHoTenNhanVien.Location = new Point(98, 21);
            lblHoTenNhanVien.Name = "lblHoTenNhanVien";
            lblHoTenNhanVien.Size = new Size(0, 21);
            lblHoTenNhanVien.TabIndex = 3;
            // 
            // lblDiaChi
            // 
            lblDiaChi.AutoSize = true;
            lblDiaChi.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDiaChi.Location = new Point(21, 81);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new Size(71, 21);
            lblDiaChi.TabIndex = 2;
            lblDiaChi.Text = "Địa chỉ: ";
            // 
            // lblSDT
            // 
            lblSDT.AutoSize = true;
            lblSDT.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSDT.Location = new Point(348, 20);
            lblSDT.Name = "lblSDT";
            lblSDT.Size = new Size(124, 21);
            lblSDT.TabIndex = 1;
            lblSDT.Text = "Số Điện Thoại: ";
            // 
            // lblHoTenNV
            // 
            lblHoTenNV.AutoSize = true;
            lblHoTenNV.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHoTenNV.Location = new Point(21, 20);
            lblHoTenNV.Name = "lblHoTenNV";
            lblHoTenNV.Size = new Size(71, 21);
            lblHoTenNV.TabIndex = 0;
            lblHoTenNV.Text = "Họ Tên: ";
            // 
            // frmChamCong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1714, 1061);
            Controls.Add(pnThongTinNV);
            Controls.Add(btnBack);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(txtNhapMaNV);
            Controls.Add(guna2Panel5);
            Controls.Add(flpDanhSachChamCong);
            Controls.Add(label14);
            Controls.Add(guna2Panel4);
            Controls.Add(lbClock);
            Controls.Add(lbDate);
            Controls.Add(label1);
            Name = "frmChamCong";
            Padding = new Padding(0, 0, 10, 0);
            FormClosing += frmChamCong_FormClosing;
            FormClosed += frmChamCong_FormClosed;
            Load += frmChamCong_Load;
            pnCheckOut.ResumeLayout(false);
            pnCheckOut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)icCheckOut).EndInit();
            pnCheckIn.ResumeLayout(false);
            pnCheckIn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)icCheckIn).EndInit();
            guna2Panel4.ResumeLayout(false);
            guna2Panel4.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            pnThongTinNV.ResumeLayout(false);
            pnThongTinNV.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label lbDate;
        private Label lbClock;
        private Guna.UI2.WinForms.Guna2Panel pnCheckOut;
        private Guna.UI2.WinForms.Guna2Panel pnCheckIn;
        private Label lbCheckOut;
        private Label lbCheckIn;
        private FontAwesome.Sharp.IconPictureBox icCheckIn;
        private FontAwesome.Sharp.IconPictureBox icCheckOut;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label6;
        private FontAwesome.Sharp.IconButton iconButton2;
        private Label lbGioVao;
        private Label lbTongGio;
        private Label lbGioRa;
        private Label label14;
        private FlowLayoutPanel flpDanhSachChamCong;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2TextBox txtNhapMaNV;
        private System.Windows.Forms.Timer timerClock;
        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private Guna.UI2.WinForms.Guna2Panel pnThongTinNV;
        private Label lblHoTenNV;
        private Label lblDiaChi;
        private Label lblSDT;
        private Label lblDiaChiNhanVien;
        private Label lblSoDienThoai;
        private Label lblHoTenNhanVien;
    }
}