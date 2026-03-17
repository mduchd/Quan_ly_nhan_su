using System;
using System.Windows.Forms;
using Quan_ly_nhan_su.GUI;

namespace Quan_ly_nhan_su
{
    public partial class MainForm : Form
    {
        private readonly ucNhanVien _ucNhanVien = new();

        public MainForm()
        {
            InitializeComponent();

            button2.Click += button2_Click;
        }

        private void OpenControl(UserControl control)
        {
            if (control.Parent == pnlDesktop)
            {
                control.BringToFront();
                return;
            }

            control.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(control);
            control.BringToFront();
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            OpenControl(_ucNhanVien);
        }

        private void lblLogo_Click(object? sender, EventArgs e)
        {
            OpenControl(_ucNhanVien);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            lblLogo_Click(sender, e);
        }
    }
}
