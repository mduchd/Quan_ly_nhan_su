using Quan_ly_nhan_su.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Quan_ly_nhan_su.DTO;

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
            DateTime now = DateTime.Now;
            lbClock.Text = DateTime.Now.ToString("HH:mm:ss");

            string[] day = { "Chủ nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy" };
            string thu = day[(int)now.DayOfWeek];
            lbDate.Text = $"{thu}, {now.Day} tháng {now.Month}, {now.Year}";
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
            string maNhanVien = txtNhapMaNV.Text.Trim();
            if (string.IsNullOrEmpty(txtNhapMaNV.Text.Trim()))

            {
                MessageBox.Show("Vui lòng nhập mã nhân viên trước khi Check-in!");
                return;
            }
            
            
            ChamCongDAL dal = new ChamCongDAL();
            int trangThaiHienTai = dal.KiemTraTrangThai(maNhanVien);

            if(trangThaiHienTai == -1)
            {
                MessageBox.Show("Mã nhân viên này không có trong hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(trangThaiHienTai == 1)
            {
                MessageBox.Show("Bạn đã Check-in rồi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                khoaNutCheckIn();
                return;
            }
            bool isSuccess = dal.CapNhatTrangThaiCheckIn(maNhanVien, 1);
            if (isSuccess)
            {
                MessageBox.Show(maNhanVien + " đã Check-in thành công!");
                lbGioVao.Text = DateTime.Now.ToString("HH:mm"); 
                khoaNutCheckIn();
                txtNhapMaNV.Clear();
                
            }
            else
            {
                MessageBox.Show("Check-in thất bại. Vui lòng thử lại!");
                return;
            }
        }
        private void khoaNutCheckIn()
        {
            pnCheckIn.FillColor = Color.Gray;
            icCheckIn.BackColor = Color.Gray;
            lbCheckIn.BackColor = Color.Gray;
            lbCheckIn.ForeColor = Color.White;
            pnCheckIn.Enabled = false;
        }
        private void upDateColor()
        {
            pnCheckIn.FillColor = Color.MediumSeaGreen;
            icCheckIn.BackColor = Color.MediumSeaGreen;
            lbCheckIn.BackColor = Color.MediumSeaGreen;
            lbCheckIn.ForeColor = Color.White;
            pnCheckOut.FillColor = Color.FromArgb(255, 128, 0);
            icCheckOut.BackColor = Color.FromArgb(255, 128, 0);
            lbCheckOut.BackColor = Color.FromArgb(255, 128, 0);
            lbCheckOut.ForeColor = Color.White;
            pnCheckIn.Enabled = true;
            pnCheckOut.Enabled = true;
        }

        private void pnCheckOut_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtNhapMaNV.Text.Trim();
            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên trước khi Check-Out");
                return;
            }
            ChamCongDAL dal = new ChamCongDAL();
            int trangThaiHienTai = dal.KiemTraTrangThai(maNhanVien);
            if(trangThaiHienTai == -1)
            {
                MessageBox.Show("Mã nhân viên này không có trong hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(trangThaiHienTai == 0)
            {
                MessageBox.Show("Bạn chưa Check-In. Vui lòng Check-In trước khi Check-Out", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            bool isSuccess = dal.CapNhatTrangThaiCheckOut(maNhanVien);
            if (isSuccess)
            {
                MessageBox.Show(maNhanVien + " đã Check-out thành công!");
                ChamCongDTO thongtin = dal.LayThongTinChamCong(maNhanVien);
                if(thongtin != null && thongtin.GioVao != null)
                {
                    lbGioVao.Text = thongtin.GioVao.Value.ToString("hh\\:mm");
                    lbGioRa.Text = DateTime.Now.ToString("HH:mm");
                    TimeSpan gioRa = DateTime.Now.TimeOfDay;
                    TimeSpan gioVao = thongtin.GioVao.Value;
                    TimeSpan tongThoiGian = gioRa - gioVao;
                    int soGio = tongThoiGian.Hours;
                    int soPhut = tongThoiGian.Minutes;

                    // Hiển thị lên giao diện
                    lbTongGio.Text = $"{soGio}h {soPhut:D2}m";
           
                }
                
                khoaNutCheckIn();
                txtNhapMaNV.Clear();
            }
            else
            {
                MessageBox.Show("Check-out thất bại. Vui lòng thử lại!");
            }
            
            

        }

        private void lbTongGio_Click(object sender, EventArgs e)
        {

        }

        private void txtNhapMaNV_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // tắt âm thanh "ding" khi nhấn Enter
                string MaNV = txtNhapMaNV.Text.Trim();

                if (string.IsNullOrEmpty(MaNV))
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên!");
                    return;
                }
                ChamCongDAL dal = new ChamCongDAL();
                int trangThai = dal.KiemTraTrangThai(MaNV);
                if(trangThai == 1) 
                {
                    khoaNutCheckIn();
                    ChamCongDTO thongtin = dal.LayThongTinChamCong(MaNV);
                    if(thongtin != null && thongtin.GioVao != null)
                    {
                        lbGioVao.Text = thongtin.GioVao.Value.ToString("hh\\:mm");
                        lbGioRa.Text = "--:--";
                        lbTongGio.Text = "0.00h";
                    }
                }
                else if(trangThai == 0) 
                {
                    upDateColor();
                    lbGioVao.Text = "--:--";
                    lbGioRa.Text = "--:--";
                    lbTongGio.Text = "0.00h";
                }
                else
                {
                    pnThongTinNV.Visible = true;
                    pnThongTinNV.BringToFront();
                }
            }
        }

        private void pnCheckOut_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
