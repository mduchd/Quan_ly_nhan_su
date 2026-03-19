namespace Quan_ly_nhan_su.GUI
{
    partial class frmDangNhap
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlLeft = new Panel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnDangNhap = new Guna.UI2.WinForms.Guna2Button();
            txtTaiKhoan = new Guna.UI2.WinForms.Guna2TextBox();
            txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.DarkCyan;
            pnlLeft.Controls.Add(guna2PictureBox1);
            pnlLeft.Controls.Add(guna2HtmlLabel1);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Margin = new Padding(3, 4, 3, 4);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(431, 548);
            pnlLeft.TabIndex = 0;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = Color.White;
            guna2HtmlLabel1.Location = new Point(94, 373);
            guna2HtmlLabel1.Margin = new Padding(3, 4, 3, 4);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(252, 56);
            guna2HtmlLabel1.TabIndex = 1;
            guna2HtmlLabel1.Text = "HRM SYSTEM";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(575, 45);
            label2.Name = "label2";
            label2.Size = new Size(169, 41);
            label2.TabIndex = 1;
            label2.Text = "Đăng nhập";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(463, 139);
            label3.Name = "label3";

            label3.Size = new Size(71, 20);

            label3.TabIndex = 2;
            label3.Text = "Tài khoản";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(463, 269);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 3;
            label4.Text = "Mật khẩu";
            // 
            // btnDangNhap
            // 
            btnDangNhap.BorderRadius = 15;
            btnDangNhap.CustomizableEdges = customizableEdges3;
            btnDangNhap.DisabledState.BorderColor = Color.DarkGray;
            btnDangNhap.DisabledState.CustomBorderColor = Color.DarkGray;
            btnDangNhap.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnDangNhap.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnDangNhap.FillColor = Color.DarkCyan;
            btnDangNhap.Font = new Font("Segoe UI", 9F);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(543, 407);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnDangNhap.Size = new Size(225, 56);
            btnDangNhap.TabIndex = 7;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // txtTaiKhoan
            // 
            txtTaiKhoan.BorderColor = Color.Black;
            txtTaiKhoan.BorderRadius = 15;
            txtTaiKhoan.CustomizableEdges = customizableEdges5;
            txtTaiKhoan.DefaultText = "";
            txtTaiKhoan.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtTaiKhoan.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtTaiKhoan.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtTaiKhoan.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtTaiKhoan.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtTaiKhoan.Font = new Font("Segoe UI", 9F);
            txtTaiKhoan.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtTaiKhoan.IconLeft = Properties.Resources.user;
            txtTaiKhoan.IconLeftOffset = new Point(5, 0);
            txtTaiKhoan.Location = new Point(463, 180);
            txtTaiKhoan.Margin = new Padding(3, 5, 3, 5);
            txtTaiKhoan.Name = "txtTaiKhoan";
            txtTaiKhoan.PlaceholderText = "";
            txtTaiKhoan.SelectedText = "";
            txtTaiKhoan.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtTaiKhoan.Size = new Size(387, 45);
            txtTaiKhoan.TabIndex = 8;
            // 
            // txtMatKhau
            // 

            txtMatKhau.BorderColor = Color.Black;
            txtMatKhau.BorderRadius = 15;
            txtMatKhau.CustomizableEdges = customizableEdges7;
            txtMatKhau.DefaultText = "";
            txtMatKhau.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtMatKhau.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtMatKhau.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtMatKhau.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtMatKhau.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtMatKhau.Font = new Font("Segoe UI", 9F);
            txtMatKhau.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtMatKhau.IconLeft = Properties.Resources.eye;
            txtMatKhau.IconLeftOffset = new Point(5, 0);
            txtMatKhau.Location = new Point(463, 303);
            txtMatKhau.Margin = new Padding(3, 5, 3, 5);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PlaceholderText = "";
            txtMatKhau.SelectedText = "";
            txtMatKhau.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtMatKhau.Size = new Size(387, 45);
            txtMatKhau.TabIndex = 9;
            // 
            // guna2PictureBox1
            // 
            guna2PictureBox1.BackColor = Color.White;
            guna2PictureBox1.BorderRadius = 50;
            guna2PictureBox1.CustomizableEdges = customizableEdges1;
            guna2PictureBox1.Image = Properties.Resources.management;
            guna2PictureBox1.ImageRotate = 0F;
            guna2PictureBox1.Location = new Point(94, 139);
            guna2PictureBox1.Name = "guna2PictureBox1";
            guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2PictureBox1.Size = new Size(227, 182);
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            guna2PictureBox1.TabIndex = 2;
            guna2PictureBox1.TabStop = false;
            guna2PictureBox1.Click += guna2PictureBox1_Click;

            // 
            // frmDangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(896, 548);
            Controls.Add(txtMatKhau);
            Controls.Add(txtTaiKhoan);
            Controls.Add(btnDangNhap);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(pnlLeft);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDangNhap";
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlLeft;
        private Label label2;
        private Label label3;
        private Label label4;
        private Guna.UI2.WinForms.Guna2Button btnDangNhap;
        private Guna.UI2.WinForms.Guna2TextBox txtTaiKhoan;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhau;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
    }
}