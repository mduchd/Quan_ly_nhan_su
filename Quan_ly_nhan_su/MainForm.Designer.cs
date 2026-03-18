namespace Quan_ly_nhan_su
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlSidebar = new Panel();
            lblVaiTro = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnDangXuat = new Guna.UI2.WinForms.Guna2Button();
            btnNghiPhep = new Button();
            btnTienLuong = new Button();
            btnChamCong = new Button();
            btnQLNhanSu = new Button();
            lblLogo = new Label();
            pnlDesktop = new Panel();
            pnlSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.SteelBlue;
            pnlSidebar.Controls.Add(lblVaiTro);
            pnlSidebar.Controls.Add(btnDangXuat);
            pnlSidebar.Controls.Add(btnNghiPhep);
            pnlSidebar.Controls.Add(btnTienLuong);
            pnlSidebar.Controls.Add(btnChamCong);
            pnlSidebar.Controls.Add(btnQLNhanSu);
            pnlSidebar.Controls.Add(lblLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(249, 561);
            pnlSidebar.TabIndex = 0;
            // 
            // lblVaiTro
            // 
            lblVaiTro.BackColor = Color.Transparent;
            lblVaiTro.Dock = DockStyle.Bottom;
            lblVaiTro.ForeColor = Color.White;
            lblVaiTro.Location = new Point(0, 483);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(249, 22);
            lblVaiTro.TabIndex = 8;
            lblVaiTro.Text = "Vai trò";
            lblVaiTro.TextAlignment = ContentAlignment.TopCenter;
            lblVaiTro.Click += guna2HtmlLabel1_Click;
            // 
            // btnDangXuat
            // 
            btnDangXuat.BorderRadius = 15;
            btnDangXuat.CustomizableEdges = customizableEdges1;
            btnDangXuat.DisabledState.BorderColor = Color.DarkGray;
            btnDangXuat.DisabledState.CustomBorderColor = Color.DarkGray;
            btnDangXuat.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnDangXuat.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnDangXuat.Dock = DockStyle.Bottom;
            btnDangXuat.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDangXuat.ForeColor = Color.White;
            btnDangXuat.Location = new Point(0, 505);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnDangXuat.Size = new Size(249, 56);
            btnDangXuat.TabIndex = 7;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // btnNghiPhep
            // 
            btnNghiPhep.BackColor = Color.Transparent;
            btnNghiPhep.Dock = DockStyle.Top;
            btnNghiPhep.FlatAppearance.BorderSize = 0;
            btnNghiPhep.FlatStyle = FlatStyle.Flat;
            btnNghiPhep.ForeColor = Color.White;
            btnNghiPhep.Location = new Point(0, 230);
            btnNghiPhep.Name = "btnNghiPhep";
            btnNghiPhep.Padding = new Padding(25, 0, 0, 0);
            btnNghiPhep.Size = new Size(249, 50);
            btnNghiPhep.TabIndex = 4;
            btnNghiPhep.Text = "Nghỉ phép";
            btnNghiPhep.TextAlign = ContentAlignment.MiddleLeft;
            btnNghiPhep.UseVisualStyleBackColor = false;
            btnNghiPhep.Click += btnNghiPhep_Click;
            // 
            // btnTienLuong
            // 
            btnTienLuong.BackColor = Color.Transparent;
            btnTienLuong.Dock = DockStyle.Top;
            btnTienLuong.FlatAppearance.BorderSize = 0;
            btnTienLuong.FlatStyle = FlatStyle.Flat;
            btnTienLuong.ForeColor = Color.White;
            btnTienLuong.Location = new Point(0, 180);
            btnTienLuong.Name = "btnTienLuong";
            btnTienLuong.Padding = new Padding(25, 0, 0, 0);
            btnTienLuong.Size = new Size(249, 50);
            btnTienLuong.TabIndex = 2;
            btnTienLuong.Text = "Tính tiền lương";
            btnTienLuong.TextAlign = ContentAlignment.MiddleLeft;
            btnTienLuong.UseVisualStyleBackColor = false;
            btnTienLuong.Click += btnTienLuong_Click;
            // 
            // btnChamCong
            // 
            btnChamCong.BackColor = Color.Transparent;
            btnChamCong.Dock = DockStyle.Top;
            btnChamCong.FlatAppearance.BorderSize = 0;
            btnChamCong.FlatStyle = FlatStyle.Flat;
            btnChamCong.ForeColor = Color.White;
            btnChamCong.Location = new Point(0, 130);
            btnChamCong.Name = "btnChamCong";
            btnChamCong.Padding = new Padding(25, 0, 0, 0);
            btnChamCong.Size = new Size(249, 50);
            btnChamCong.TabIndex = 0;
            btnChamCong.Text = "Chấm công";
            btnChamCong.TextAlign = ContentAlignment.MiddleLeft;
            btnChamCong.UseVisualStyleBackColor = false;
            btnChamCong.Click += button1_Click;
            // 
            // btnQLNhanSu
            // 
            btnQLNhanSu.BackColor = Color.Transparent;
            btnQLNhanSu.Dock = DockStyle.Top;
            btnQLNhanSu.FlatAppearance.BorderSize = 0;
            btnQLNhanSu.FlatStyle = FlatStyle.Flat;
            btnQLNhanSu.ForeColor = Color.White;
            btnQLNhanSu.Location = new Point(0, 80);
            btnQLNhanSu.Name = "btnQLNhanSu";
            btnQLNhanSu.Padding = new Padding(25, 0, 0, 0);
            btnQLNhanSu.Size = new Size(249, 50);
            btnQLNhanSu.TabIndex = 1;
            btnQLNhanSu.Text = "Quản lý nhân sự";
            btnQLNhanSu.TextAlign = ContentAlignment.MiddleLeft;
            btnQLNhanSu.UseVisualStyleBackColor = false;
            // 
            // lblLogo
            // 
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(249, 80);
            lblLogo.TabIndex = 3;
            lblLogo.Text = "HRM System";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblLogo.Click += label1_Click;
            // 
            // pnlDesktop
            // 
            pnlDesktop.Dock = DockStyle.Fill;
            pnlDesktop.Location = new Point(249, 0);
            pnlDesktop.Name = "pnlDesktop";
            pnlDesktop.Size = new Size(735, 561);
            pnlDesktop.TabIndex = 1;
            pnlDesktop.Paint += pnlDesktop_Paint;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(pnlDesktop);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HRM Workspace";
            WindowState = FormWindowState.Maximized;
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Button button3;
        private Button button2;
        private Button btnChamCong;
        private Panel pnlDesktop;
        private Label lblLogo;
        private Button button4;
        private Button btnNghiPhep;
        private Button btnTienLuong;
        private Button btnQLNhanSu;
        private Guna.UI2.WinForms.Guna2Button btnDangXuat;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblVaiTro;
    }
}
