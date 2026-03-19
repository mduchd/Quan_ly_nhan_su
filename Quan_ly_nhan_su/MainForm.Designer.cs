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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlSidebar = new Panel();
            btnNghiPhep = new Guna.UI2.WinForms.Guna2Button();
            btnTienLuong = new Guna.UI2.WinForms.Guna2Button();
            btnChamCong = new Guna.UI2.WinForms.Guna2Button();
            btnQLNhanSu = new Guna.UI2.WinForms.Guna2Button();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            lblVaiTro = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnDangXuat = new Guna.UI2.WinForms.Guna2Button();
            lblLogo = new Label();
            pnlDesktop = new Panel();
            pnlSidebar.SuspendLayout();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.SteelBlue;
            pnlSidebar.Controls.Add(btnNghiPhep);
            pnlSidebar.Controls.Add(btnTienLuong);
            pnlSidebar.Controls.Add(btnChamCong);
            pnlSidebar.Controls.Add(btnQLNhanSu);
            pnlSidebar.Controls.Add(guna2Panel1);
            pnlSidebar.Controls.Add(lblLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(249, 561);
            pnlSidebar.TabIndex = 0;
            // 
            // btnNghiPhep
            // 
            btnNghiPhep.BorderRadius = 15;
            btnNghiPhep.CustomizableEdges = customizableEdges1;
            btnNghiPhep.DisabledState.BorderColor = Color.DarkGray;
            btnNghiPhep.DisabledState.CustomBorderColor = Color.DarkGray;
            btnNghiPhep.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnNghiPhep.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnNghiPhep.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNghiPhep.ForeColor = Color.White;
            btnNghiPhep.Location = new Point(12, 311);
            btnNghiPhep.Name = "btnNghiPhep";
            btnNghiPhep.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnNghiPhep.Size = new Size(225, 56);
            btnNghiPhep.TabIndex = 13;
            btnNghiPhep.Text = "Nghỉ Phép";
            btnNghiPhep.Click += btnNghiPhep_Click_1;
            // 
            // btnTienLuong
            // 
            btnTienLuong.BorderRadius = 15;
            btnTienLuong.CustomizableEdges = customizableEdges3;
            btnTienLuong.DisabledState.BorderColor = Color.DarkGray;
            btnTienLuong.DisabledState.CustomBorderColor = Color.DarkGray;
            btnTienLuong.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnTienLuong.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnTienLuong.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTienLuong.ForeColor = Color.White;
            btnTienLuong.Location = new Point(12, 163);
            btnTienLuong.Name = "btnTienLuong";
            btnTienLuong.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnTienLuong.Size = new Size(225, 56);
            btnTienLuong.TabIndex = 12;
            btnTienLuong.Text = "Tiền Lương";
            btnTienLuong.Click += btnTienLuong_Click_1;
            // 
            // btnChamCong
            // 
            btnChamCong.BorderRadius = 15;
            btnChamCong.CustomizableEdges = customizableEdges5;
            btnChamCong.DisabledState.BorderColor = Color.DarkGray;
            btnChamCong.DisabledState.CustomBorderColor = Color.DarkGray;
            btnChamCong.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnChamCong.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnChamCong.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChamCong.ForeColor = Color.White;
            btnChamCong.Location = new Point(12, 237);
            btnChamCong.Name = "btnChamCong";
            btnChamCong.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnChamCong.Size = new Size(225, 56);
            btnChamCong.TabIndex = 11;
            btnChamCong.Text = "Chấm công";
            btnChamCong.Click += btnChamCong_Click;
            // 
            // btnQLNhanSu
            // 
            btnQLNhanSu.BorderRadius = 15;
            btnQLNhanSu.CustomizableEdges = customizableEdges7;
            btnQLNhanSu.DisabledState.BorderColor = Color.DarkGray;
            btnQLNhanSu.DisabledState.CustomBorderColor = Color.DarkGray;
            btnQLNhanSu.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnQLNhanSu.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnQLNhanSu.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQLNhanSu.ForeColor = Color.White;
            btnQLNhanSu.Location = new Point(12, 83);
            btnQLNhanSu.Name = "btnQLNhanSu";
            btnQLNhanSu.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnQLNhanSu.Size = new Size(225, 56);
            btnQLNhanSu.TabIndex = 10;
            btnQLNhanSu.Text = "Quản lý nhân sự";
            btnQLNhanSu.Click += btnQLNhanSu_Click_1;
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(lblVaiTro);
            guna2Panel1.Controls.Add(btnDangXuat);
            guna2Panel1.CustomizableEdges = customizableEdges11;
            guna2Panel1.Dock = DockStyle.Bottom;
            guna2Panel1.Location = new Point(0, 436);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges12;
            guna2Panel1.Size = new Size(249, 125);
            guna2Panel1.TabIndex = 9;
            // 
            // lblVaiTro
            // 
            lblVaiTro.BackColor = Color.Transparent;
            lblVaiTro.ForeColor = Color.White;
            lblVaiTro.Location = new Point(85, 18);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(43, 17);
            lblVaiTro.TabIndex = 8;
            lblVaiTro.Text = "Vai Trò: ";
            // 
            // btnDangXuat
            // 
            btnDangXuat.BorderRadius = 15;
            btnDangXuat.CustomizableEdges = customizableEdges9;
            btnDangXuat.DisabledState.BorderColor = Color.DarkGray;
            btnDangXuat.DisabledState.CustomBorderColor = Color.DarkGray;
            btnDangXuat.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnDangXuat.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnDangXuat.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDangXuat.ForeColor = Color.White;
            btnDangXuat.Image = Properties.Resources.logout;
            btnDangXuat.Location = new Point(22, 50);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnDangXuat.Size = new Size(194, 56);
            btnDangXuat.TabIndex = 7;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.Click += btnDangXuat_Click;
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
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(pnlDesktop);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HRM Workspace";
            WindowState = FormWindowState.Maximized;
            FormClosed += MainForm_FormClosed;
            pnlSidebar.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Button button3;
        private Button button2;
        private Panel pnlDesktop;
        private Label lblLogo;
        private Button button4;
      
        private Guna.UI2.WinForms.Guna2Button btnDangXuat;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblVaiTro;
        private Guna.UI2.WinForms.Guna2Button btnChamCong;
        private Guna.UI2.WinForms.Guna2Button btnQLNhanSu;
        private Guna.UI2.WinForms.Guna2Button btnNghiPhep;
        private Guna.UI2.WinForms.Guna2Button btnTienLuong;
    }
}
