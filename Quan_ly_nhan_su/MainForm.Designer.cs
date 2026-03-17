namespace Quan_ly_nhan_su
{
    partial class Form
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
            pnlSidebar = new Panel();
            btnNghiPhep = new Button();
            this.btnTienLuong = new Button();
            btnChamCong = new Button();
            this.btnQLNhanSu = new Button();
            lblLogo = new Label();
            pnlDesktop = new Panel();
            pnlSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.SteelBlue;
            pnlSidebar.Controls.Add(btnNghiPhep);
            pnlSidebar.Controls.Add(this.btnTienLuong);
            pnlSidebar.Controls.Add(btnChamCong);
            pnlSidebar.Controls.Add(this.btnQLNhanSu);
            pnlSidebar.Controls.Add(lblLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(249, 561);
            pnlSidebar.TabIndex = 0;
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
            this.btnTienLuong.BackColor = Color.Transparent;
            this.btnTienLuong.Dock = DockStyle.Top;
            this.btnTienLuong.FlatAppearance.BorderSize = 0;
            this.btnTienLuong.FlatStyle = FlatStyle.Flat;
            this.btnTienLuong.ForeColor = Color.White;
            this.btnTienLuong.Location = new Point(0, 180);
            this.btnTienLuong.Name = "btnTienLuong";
            this.btnTienLuong.Padding = new Padding(25, 0, 0, 0);
            this.btnTienLuong.Size = new Size(249, 50);
            this.btnTienLuong.TabIndex = 2;
            this.btnTienLuong.Text = "Tính tiền lương";
            this.btnTienLuong.TextAlign = ContentAlignment.MiddleLeft;
            this.btnTienLuong.UseVisualStyleBackColor = false;
            this.btnTienLuong.Click += this.btnTienLuong_Click;
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
            this.btnQLNhanSu.BackColor = Color.Transparent;
            this.btnQLNhanSu.Dock = DockStyle.Top;
            this.btnQLNhanSu.FlatAppearance.BorderSize = 0;
            this.btnQLNhanSu.FlatStyle = FlatStyle.Flat;
            this.btnQLNhanSu.ForeColor = Color.White;
            this.btnQLNhanSu.Location = new Point(0, 80);
            this.btnQLNhanSu.Name = "btnQLNhanSu";
            this.btnQLNhanSu.Padding = new Padding(25, 0, 0, 0);
            this.btnQLNhanSu.Size = new Size(249, 50);
            this.btnQLNhanSu.TabIndex = 1;
            this.btnQLNhanSu.Text = "Quản lý nhân sự";
            this.btnQLNhanSu.TextAlign = ContentAlignment.MiddleLeft;
            this.btnQLNhanSu.UseVisualStyleBackColor = false;
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
            // Form
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(pnlDesktop);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HRM Workspace";
            WindowState = FormWindowState.Maximized;
            pnlSidebar.ResumeLayout(false);
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
    }
}
