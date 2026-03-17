using System;
using System.Windows.Forms;
using Quan_ly_nhan_su.GUI; 

namespace Quan_ly_nhan_su
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ucBangLuong bangLuong = new ucBangLuong();
            bangLuong.Dock = DockStyle.Fill;
            this.Controls.Add(bangLuong);
            this.Text = "Phần mềm Quản lý Nhân sự - Phân hệ Tiền Lương";
            this.Size = new System.Drawing.Size(850, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}