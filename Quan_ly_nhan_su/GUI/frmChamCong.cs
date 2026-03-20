using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Quan_ly_nhan_su.GUI
{
    public partial class frmChamCong : Form
    {
     
        public frmChamCong()
        {
            InitializeComponent();
            timerClock.Interval = 1000;
            timerClock.Tick += new EventHandler(timerClock_Tick);
            timerClock.Start();


        }
        private void AssignClickEventToAll(Control parentControl, EventHandler clickEvent)
        {
            parentControl.Click += clickEvent;
           
          
            foreach (Control child in parentControl.Controls)
            {
                AssignClickEventToAll(child, clickEvent);
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }


        private void LoadDanhSachChamCong()
        {
            flpDanhSachChamCong.SuspendLayout();
            flpDanhSachChamCong.Controls.Clear();
            TaoTheChamCong("23", "Thứ 2, 23/10/2024", "8h 10m", "08:00 - 17:00");
            TaoTheChamCong("20", "Thứ 2, 23/10/2024", "8h 10m", "08:00 - 17:00");
            TaoTheChamCong("19", "Thứ 2, 23/10/2024", "8h 10m", "08:00 - 17:00");
            flpDanhSachChamCong.ResumeLayout();
        }
        private void TaoTheChamCong(string ngay, string dmy, string tonggio, string thoigian)
        {
            ucItemChamCong item = new ucItemChamCong();
            item.SetDataChamCong(ngay, dmy, tonggio, thoigian);
            item.Width = flpDanhSachChamCong.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 5;
            flpDanhSachChamCong.Controls.Add(item);
        }
        private void flpDanhSachChamCong_Paint(object sender, PaintEventArgs e)
        {
            foreach (Control ctrl in flpDanhSachChamCong.Controls)
            {

                ctrl.Width = flpDanhSachChamCong.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 5;
            }
        }

       
        

        private void frmChamCong_Load(object sender, EventArgs e)
        {
            AssignClickEventToAll(pnCheckIn, pnCheckIn_Click);
            AssignClickEventToAll(pnCheckOut, pnCheckOut_Click);

            LoadDanhSachChamCong();
            flpDanhSachChamCong.SizeChanged += (s, ev) =>
            {
                flpDanhSachChamCong.SuspendLayout();
                foreach (Control item in flpDanhSachChamCong.Controls)
                {
                    item.Width = flpDanhSachChamCong.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 5;
                }
                flpDanhSachChamCong.ResumeLayout();
            };
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lbClock.Text = DateTime.Now.ToString("HH:mm:ss");

        }

        private void lbClock_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnCheckIn_Click(object sender, EventArgs e)
        {
            lbGioVao.Text = DateTime.Now.ToString("HH:mm");
            pnCheckIn.FillColor = Color.Gray;
            icCheckIn.BackColor = Color.Gray;
            lbCheckIn.BackColor = Color.Gray;
            lbCheckIn.ForeColor = Color.White;
            pnCheckIn.Enabled = false;
            MessageBox.Show("Bạn đã Check-in thành công!");
        }

        private void pnCheckOut_Click(object sender, EventArgs e)
        {
            if(lbCheckIn.Enabled == true)
            {
                MessageBox.Show("Bạn chưa Check-in. Vui lòng Check-in trước khi Check-out!");
                return;
            }
            lbGioRa.Text = DateTime.Now.ToString("HH:mm");
            lbTongGio.Text = (DateTime.Parse(lbGioRa.Text) - DateTime.Parse(lbGioVao.Text)).TotalHours.ToString("0.00") + "h";
            pnCheckOut.FillColor = Color.Gray;
            pnCheckOut.FillColor = Color.Gray;
            icCheckOut.BackColor = Color.Gray;
            lbCheckOut.BackColor = Color.Gray;
            lbCheckOut.ForeColor = Color.White;
            pnCheckOut.Enabled = false;
            MessageBox.Show("Bạn đã Check-out thành công!");
        }

        private void lbTongGio_Click(object sender, EventArgs e)
        {
            
        }
    }
}
