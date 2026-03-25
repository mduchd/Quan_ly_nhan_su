namespace Quan_ly_nhan_su.GUI
{
    partial class frmDoiMatKhau
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtMatKhauCu = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMatKhauMoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtXacNhan = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTitle.Location = new System.Drawing.Point(100, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 30);
            this.lblTitle.Text = "ĐỔI MẬT KHẨU";

            // txtMatKhauCu
            this.txtMatKhauCu.BorderRadius = 10;
            this.txtMatKhauCu.Location = new System.Drawing.Point(50, 80);
            this.txtMatKhauCu.Name = "txtMatKhauCu";
            this.txtMatKhauCu.PasswordChar = '●';
            this.txtMatKhauCu.PlaceholderText = "Mật khẩu cũ";
            this.txtMatKhauCu.Size = new System.Drawing.Size(280, 40);
            this.txtMatKhauCu.UseSystemPasswordChar = true;

            // txtMatKhauMoi
            this.txtMatKhauMoi.BorderRadius = 10;
            this.txtMatKhauMoi.Location = new System.Drawing.Point(50, 140);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.PasswordChar = '●';
            this.txtMatKhauMoi.PlaceholderText = "Mật khẩu mới";
            this.txtMatKhauMoi.Size = new System.Drawing.Size(280, 40);
            this.txtMatKhauMoi.UseSystemPasswordChar = true;

            // txtXacNhan
            this.txtXacNhan.BorderRadius = 10;
            this.txtXacNhan.Location = new System.Drawing.Point(50, 200);
            this.txtXacNhan.Name = "txtXacNhan";
            this.txtXacNhan.PasswordChar = '●';
            this.txtXacNhan.PlaceholderText = "Xác nhận mật khẩu mới";
            this.txtXacNhan.Size = new System.Drawing.Size(280, 40);
            this.txtXacNhan.UseSystemPasswordChar = true;

            // btnXacNhan
            this.btnXacNhan.BorderRadius = 15;
            this.btnXacNhan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacNhan.FillColor = System.Drawing.Color.SteelBlue;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(90, 270);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(200, 45);
            this.btnXacNhan.Text = "Xác nhận đổi";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);

            // frmDoiMatKhau
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 350);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtMatKhauCu);
            this.Controls.Add(this.txtMatKhauMoi);
            this.Controls.Add(this.txtXacNhan);
            this.Controls.Add(this.btnXacNhan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoiMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi Mật Khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Guna.UI2.WinForms.Guna2TextBox txtMatKhauCu;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhauMoi;
        private Guna.UI2.WinForms.Guna2TextBox txtXacNhan;
        private Guna.UI2.WinForms.Guna2Button btnXacNhan;
        private System.Windows.Forms.Label lblTitle;
    }
}
