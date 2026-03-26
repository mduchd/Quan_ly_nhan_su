using Quan_ly_nhan_su.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Quan_ly_nhan_su.DTO;
using System.Security.RightsManagement;
using Quan_ly_nhan_su.BUS;


namespace Quan_ly_nhan_su.GUI
{
    public partial class frmChamCong : Form
    {
        private bool isThoat = true;
        public frmChamCong()
        {
            InitializeComponent();
            timerClock.Interval = 1000;
            timerClock.Tick += new EventHandler(timerClock_Tick);
            timerClock.Start();


        }
        private void AssignClickEventToAll(Control parentControl, EventHandler clickEvent)
        {
            parentControl.Click -= clickEvent;
            parentControl.Click += clickEvent;


            foreach (Control child in parentControl.Controls)
            {
                AssignClickEventToAll(child, clickEvent);
            }
        }

        private void LoadLichSuCuaNhanVien(string maNV)
        {
            flpDanhSachChamCong.SuspendLayout();
            flpDanhSachChamCong.Controls.Clear();
            ChamCongBUS bus = new ChamCongBUS();
            DataTable dtLichSu = bus.LayLichSuChamCong(maNV);
            string[] day = { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            foreach (DataRow row in dtLichSu.Rows)
            {
                if (row["NgayChamCong"] != DBNull.Value && row["GioVao"] != DBNull.Value)
                {
                    DateTime ngayDayDu = Convert.ToDateTime(row["NgayChamCong"]);
                    TimeSpan gioVao = (TimeSpan)row["GioVao"];
                    TimeSpan? gioRa = null;
                    if (row["GioRa"] != DBNull.Value)
                    {
                        gioRa = (TimeSpan)row["GioRa"];
                    }


                    int ngay = ngayDayDu.Day;
                    int thang = ngayDayDu.Month;
                    int nam = ngayDayDu.Year;
                    string thu = day[(int)ngayDayDu.DayOfWeek];

                    ucItemChamCong uct = new ucItemChamCong();
                    uct.LichSuNgay(ngay, thang, nam, thu, gioVao, gioRa);
                    uct.Width = flpDanhSachChamCong.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 5;

                    flpDanhSachChamCong.Controls.Add(uct);
                }
            }
            flpDanhSachChamCong.ResumeLayout();
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


            flpDanhSachChamCong.SizeChanged += (s, ev) =>
            {
                flpDanhSachChamCong.SuspendLayout();
                foreach (Control item in flpDanhSachChamCong.Controls)
                {
                    item.Width = flpDanhSachChamCong.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 5;
                }
                flpDanhSachChamCong.ResumeLayout();
            };
            pnThongTinNV.Visible = false;
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
            ChamCongBUS bus = new ChamCongBUS();
            string ketqua = bus.XuLyCheckIn(maNhanVien);
            if (ketqua == "Thành công")
            {
                MessageBox.Show(maNhanVien + "Đã Check-in thành công!");
                lbGioVao.Text = DateTime.Now.ToString("HH:mm");
                khoaNutCheckIn();
                txtNhapMaNV.Clear();
                pnThongTinNV.Visible = false;
                LoadLichSuCuaNhanVien(maNhanVien);

            }
            else
            {
                MessageBox.Show(ketqua, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            ChamCongBUS bus = new ChamCongBUS();
            TimeSpan gioVao, gioRa, tongThoiGian;
            string ketqua = bus.XuLyCheckOut(maNhanVien, out gioVao, out gioRa, out tongThoiGian);
            if (ketqua == "Thành công")
            {
                MessageBox.Show(maNhanVien + "đã Check-out thành công");
                lbGioVao.Text = gioVao.ToString(@"hh\:mm");
                int soGio = tongThoiGian.Hours;
                int soPhut = tongThoiGian.Minutes;
                lbTongGio.Text = $"{soGio}h {soPhut:D2}m";

                khoaNutCheckIn();
                txtNhapMaNV.Clear();
                pnThongTinNV.Visible = false;
                LoadLichSuCuaNhanVien(maNhanVien);

            }
            else
            {
                MessageBox.Show(ketqua, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void txtNhapMaNV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string MaNV = txtNhapMaNV.Text.Trim();

                if (string.IsNullOrEmpty(MaNV))
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên!");
                    return;
                }

                ChamCongBUS bus = new ChamCongBUS();
                int trangThai = bus.KiemTraTrangThai(MaNV);

                if (trangThai == -1)
                {
                    MessageBox.Show("Mã nhân viên này không có trong hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pnThongTinNV.Visible = false;
                    flpDanhSachChamCong.Controls.Clear();
                    return;
                }

                TTNV();
                LoadLichSuCuaNhanVien(MaNV);


                if (trangThai == 1)
                {
                    khoaNutCheckIn();
                    ChamCongDTO thongtin = bus.LayThongTinChamCong(MaNV);
                    if (thongtin != null && thongtin.GioVao != null)
                    {
                        lbGioVao.Text = thongtin.GioVao.Value.ToString(@"hh\:mm");
                        lbGioRa.Text = "--:--";
                        lbTongGio.Text = "0.00h";
                    }
                }
                else if (trangThai == 0)
                {
                    pnThongTinNV.Enabled = true;
                    upDateColor();
                    lbGioVao.Text = "--:--";
                    lbGioRa.Text = "--:--";
                    lbTongGio.Text = "0.00h";
                }
            }
        }

        public void TTNV()
        {
            ChamCongBUS bus = new ChamCongBUS();
            try
            {

                string MaNV = txtNhapMaNV.Text.Trim();
                var thongtin = bus.LayThongTinNhanVien(MaNV);
                lblHoTenNhanVien.Text = thongtin.tenNV;
                lblSoDienThoai.Text = thongtin.SDT;
                lblDiaChiNhanVien.Text = thongtin.DiaChi;
                pnThongTinNV.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message);
            }
        }



        private void btnBack_Click(object sender, EventArgs e)
        {
            isThoat = false;
            this.Close();
            
        }

        private void lbDate_Click(object sender, EventArgs e)
        {

        }

        private void frmChamCong_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isThoat)
            {
                Environment.Exit(0);
            }

        }

        private void frmChamCong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerClock != null)
            {
                timerClock.Stop();
                timerClock.Dispose();
            }
        }

        private void txtNhapMaNV_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNhapMaNV.Text.Trim()))
            {
                upDateColor();
            }
        }
    }
}
