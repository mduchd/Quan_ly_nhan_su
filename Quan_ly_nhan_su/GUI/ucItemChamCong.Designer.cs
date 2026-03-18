namespace Quan_ly_nhan_su.GUI
{
    partial class ucItemChamCong
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            lblDmy = new Label();
            lblThoiGian = new Label();
            lblNgay = new Label();
            lblTongThoiGian = new Label();
            guna2Panel1.SuspendLayout();
            guna2Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.BorderColor = Color.Silver;
            guna2Panel1.BorderRadius = 10;
            guna2Panel1.Controls.Add(lblTongThoiGian);
            guna2Panel1.Controls.Add(lblThoiGian);
            guna2Panel1.Controls.Add(lblDmy);
            guna2Panel1.Controls.Add(guna2Panel2);
            guna2Panel1.CustomizableEdges = customizableEdges3;
            guna2Panel1.Dock = DockStyle.Top;
            guna2Panel1.FillColor = Color.White;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel1.Size = new Size(1578, 123);
            guna2Panel1.TabIndex = 0;
            guna2Panel1.Paint += guna2Panel1_Paint;
            // 
            // guna2Panel2
            // 
            guna2Panel2.BorderRadius = 25;
            guna2Panel2.Controls.Add(lblNgay);
            guna2Panel2.CustomizableEdges = customizableEdges1;
            guna2Panel2.FillColor = Color.FromArgb(224, 224, 224);
            guna2Panel2.Location = new Point(35, 29);
            guna2Panel2.Name = "guna2Panel2";
            guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel2.Size = new Size(60, 60);
            guna2Panel2.TabIndex = 0;
            // 
            // lblDmy
            // 
            lblDmy.AutoSize = true;
            lblDmy.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDmy.Location = new Point(138, 29);
            lblDmy.Name = "lblDmy";
            lblDmy.Size = new Size(170, 21);
            lblDmy.TabIndex = 1;
            lblDmy.Text = "Thứ Hai, 23 Tháng 10";
            // 
            // lblThoiGian
            // 
            lblThoiGian.AutoSize = true;
            lblThoiGian.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblThoiGian.ForeColor = Color.Gray;
            lblThoiGian.Location = new Point(138, 58);
            lblThoiGian.Name = "lblThoiGian";
            lblThoiGian.Size = new Size(103, 20);
            lblThoiGian.TabIndex = 2;
            lblThoiGian.Text = "07:55 - 17:05";
            // 
            // lblNgay
            // 
            lblNgay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblNgay.AutoSize = true;
            lblNgay.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNgay.Location = new Point(12, 15);
            lblNgay.Name = "lblNgay";
            lblNgay.Size = new Size(36, 28);
            lblNgay.TabIndex = 0;
            lblNgay.Text = "23";
            // 
            // lblTongThoiGian
            // 
            lblTongThoiGian.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTongThoiGian.AutoSize = true;
            lblTongThoiGian.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTongThoiGian.ForeColor = Color.MediumSeaGreen;
            lblTongThoiGian.Location = new Point(1469, 53);
            lblTongThoiGian.Name = "lblTongThoiGian";
            lblTongThoiGian.Size = new Size(78, 25);
            lblTongThoiGian.TabIndex = 3;
            lblTongThoiGian.Text = "8h 10m";
            // 
            // ucItemChamCong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(guna2Panel1);
            Name = "ucItemChamCong";
            Size = new Size(1578, 139);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            guna2Panel2.ResumeLayout(false);
            guna2Panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Label lblTongThoiGian;
        private Label lblThoiGian;
        private Label lblDmy;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Label lblNgay;
    }
}
