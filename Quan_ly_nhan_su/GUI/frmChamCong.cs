<<<<<<< HEAD
=======
﻿using Quan_ly_nhan_su.DAL;
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
<<<<<<< HEAD
using Quan_ly_nhan_su.BUS;
using Quan_ly_nhan_su.DTO;
=======
using Quan_ly_nhan_su.DTO;
using System.Security.RightsManagement;
using Quan_ly_nhan_su.BUS;

>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120

namespace Quan_ly_nhan_su.GUI
{
    public partial class frmChamCong : Form
    {
<<<<<<< HEAD
        private readonly ChamCongBUS _chamCongBUS = new();
        private string _currentEmployeeId = string.Empty;

=======
        private bool isThoat = true;
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
        public frmChamCong()
        {
            InitializeComponent();
            timerClock.Interval = 1000;
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

<<<<<<< HEAD
        private void guna2Button1_Click(object? sender, EventArgs e)
        {
        }

        private void iconButton1_Click(object? sender, EventArgs e)
        {
        }

        private void label10_Click(object? sender, EventArgs e)
        {
        }

        private void label12_Click(object? sender, EventArgs e)
        {
        }

        private void LoadDanhSachChamCong(IEnumerable<LichSuCongDTO> lichSu)
        {
            flpDanhSachChamCong.SuspendLayout();
            flpDanhSachChamCong.Controls.Clear();

            foreach (var item in lichSu.OrderByDescending(x => x.ngay))
            {
                var tongGio = item.gioVao.HasValue && item.gioRa.HasValue
                    ? FormatDuration(item.gioRa.Value - item.gioVao.Value)
                    : "--";
                var thoiGian = $"{FormatTime(item.gioVao)} - {FormatTime(item.gioRa)}";
                TaoTheChamCong(
                    item.ngay.Day.ToString("00"),
                    $"{GetDayOfWeekText(item.ngay.DayOfWeek)}, {item.ngay:dd/MM/yyyy}",
                    tongGio,
                    thoiGian);
            }

            flpDanhSachChamCong.ResumeLayout();
        }

        private void TaoTheChamCong(string ngay, string dmy, string tonggio, string thoigian)
        {
            var item = new ucItemChamCong();
            item.SetDataChamCong(ngay, dmy, tonggio, thoigian);
            item.Width = flpDanhSachChamCong.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 5;
            flpDanhSachChamCong.Controls.Add(item);
        }

        private void flpDanhSachChamCong_Paint(object? sender, PaintEventArgs e)
=======
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
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
        {
            foreach (Control ctrl in flpDanhSachChamCong.Controls)
            {
                ctrl.Width = flpDanhSachChamCong.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 5;
            }
        }

        private void frmChamCong_Load(object? sender, EventArgs e)
        {
            AssignClickEventToAll(pnCheckIn, pnCheckIn_Click);
            AssignClickEventToAll(pnCheckOut, pnCheckOut_Click);
            ResetAttendanceSummary();

<<<<<<< HEAD
=======

>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
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

        private void timerClock_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            lbClock.Text = now.ToString("HH:mm:ss");
            lbDate.Text = $"{GetDayOfWeekText(now.DayOfWeek)}, {now.Day} tháng {now.Month}, {now.Year}";
        }

        private void lbClock_Click(object? sender, EventArgs e)
        {
        }

        private void label11_Click(object? sender, EventArgs e)
        {
        }

        private void guna2Panel3_Paint(object? sender, PaintEventArgs e)
        {
        }

        private void pnCheckIn_Click(object? sender, EventArgs e)
        {
<<<<<<< HEAD
            var maNV = ResolveTargetEmployeeId(isCheckOut: false);
            if (!EnsureEmployeeLoaded(maNV, showError: true, out _))
            {
                return;
            }

            var ketQua = _chamCongBUS.CheckIn(maNV, DateTime.Now);
            if (!string.Equals(ketQua, "Thành công", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(ketQua, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtNhapMaNV.Clear();
            LoadEmployeeContext(maNV);
            MessageBox.Show("Bạn đã check-in thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pnCheckOut_Click(object? sender, EventArgs e)
        {
            var maNV = ResolveTargetEmployeeId(isCheckOut: true);
            if (!EnsureEmployeeLoaded(maNV, showError: true, out _))
            {
                return;
            }

            var ketQua = _chamCongBUS.CheckOut(maNV, DateTime.Now);
            if (!string.Equals(ketQua, "Thành công", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(ketQua, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtNhapMaNV.Clear();
            LoadEmployeeContext(maNV);
            MessageBox.Show("Bạn đã check-out thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lbTongGio_Click(object? sender, EventArgs e)
        {
        }

        private void txtNhapMaNV_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            var maNV = txtNhapMaNV.Text.Trim();

            if (string.IsNullOrWhiteSpace(maNV))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EnsureEmployeeLoaded(maNV, showError: true, out _))
            {
                return;
            }

            LoadEmployeeContext(maNV);
        }

        private bool EnsureEmployeeLoaded(string maNV, bool showError, out NhanVienDTO? nhanVien)
        {
            nhanVien = null;
            maNV = (maNV ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(maNV))
            {
                if (showError)
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return false;
            }

            nhanVien = _chamCongBUS.LayThongTinNhanVien(maNV);
            if (nhanVien == null)
            {
                if (showError)
                {
                    MessageBox.Show("Không tìm thấy nhân viên với mã đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                _currentEmployeeId = string.Empty;
                pnThongTinNV.Visible = false;
                flpDanhSachChamCong.Controls.Clear();
                ResetAttendanceSummary();
                return false;
            }

            return true;
        }

        private void LoadEmployeeContext(string maNV)
        {
            if (!EnsureEmployeeLoaded(maNV, showError: false, out var nhanVien) || nhanVien == null)
            {
                return;
            }

            _currentEmployeeId = nhanVien.MaNV;
            var lichSu = _chamCongBUS.LayLichSuGanDay(maNV, 3);

            label7.Text = string.IsNullOrWhiteSpace(nhanVien.TenNV) ? "Chưa cập nhật" : nhanVien.TenNV;
            label12.Text = string.IsNullOrWhiteSpace(nhanVien.SoDienThoai) ? "Chưa cập nhật" : nhanVien.SoDienThoai;
            label11.Text = string.IsNullOrWhiteSpace(nhanVien.DiaChi) ? "Chưa cập nhật" : nhanVien.DiaChi;
            pnThongTinNV.Visible = true;
            pnThongTinNV.BringToFront();

            LoadDanhSachChamCong(lichSu);
            ApplyAttendanceSummary(nhanVien, lichSu);
        }

        private void ApplyAttendanceSummary(NhanVienDTO nhanVien, List<LichSuCongDTO> lichSu)
        {
            var lichSuHomNay = lichSu.FirstOrDefault(x => x.ngay.Date == DateTime.Today);
            var gioVao = lichSuHomNay?.gioVao;
            var gioRa = lichSuHomNay?.gioRa;

            if (!gioVao.HasValue && nhanVien.NgayChamCong?.Date == DateTime.Today)
            {
                gioVao = nhanVien.GioVao;
                gioRa = nhanVien.GioRa;
            }

            if (!gioVao.HasValue)
            {
                ResetAttendanceSummary();
                return;
            }

            lbGioVao.Text = FormatTime(gioVao);
            lbGioRa.Text = FormatTime(gioRa);
            lbTongGio.Text = gioRa.HasValue ? FormatDuration(gioRa.Value - gioVao.Value) : "--";

            SetCheckInState(false);
            SetCheckOutState(!gioRa.HasValue);
        }

        private void ResetAttendanceSummary()
        {
            lbGioVao.Text = "--:--";
            lbGioRa.Text = "--:--";
            lbTongGio.Text = "--";
            SetCheckInState(true);
            SetCheckOutState(true);
        }

        private void SetCheckInState(bool enabled)
        {
            pnCheckIn.Enabled = enabled;
            pnCheckIn.FillColor = enabled ? Color.MediumSeaGreen : Color.Gray;
            icCheckIn.BackColor = pnCheckIn.FillColor;
            lbCheckIn.BackColor = pnCheckIn.FillColor;
            lbCheckIn.ForeColor = Color.White;
=======
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
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
        }

        private void SetCheckOutState(bool enabled)
        {
<<<<<<< HEAD
            pnCheckOut.Enabled = enabled;
            pnCheckOut.FillColor = enabled ? Color.FromArgb(255, 128, 0) : Color.Gray;
            icCheckOut.BackColor = pnCheckOut.FillColor;
            lbCheckOut.BackColor = pnCheckOut.FillColor;
            lbCheckOut.ForeColor = Color.White;
        }

        private string ResolveTargetEmployeeId(bool isCheckOut)
        {
            var maNVNhap = txtNhapMaNV.Text.Trim();
            if (isCheckOut && !pnCheckIn.Enabled && !string.IsNullOrWhiteSpace(_currentEmployeeId))
            {
                return _currentEmployeeId;
=======
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
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
            }

            if (!string.IsNullOrWhiteSpace(maNVNhap))
            {
                return maNVNhap;
            }

            return _currentEmployeeId;
        }

<<<<<<< HEAD
        private static string FormatTime(TimeSpan? time)
        {
            return time.HasValue ? time.Value.ToString(@"hh\:mm") : "--:--";
        }

        private static string FormatDuration(TimeSpan duration)
        {
            if (duration < TimeSpan.Zero)
            {
                return "--";
            }

            return $"{(int)duration.TotalHours}h {duration.Minutes}m";
        }

        private static string GetDayOfWeekText(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Monday => "Thứ Hai",
                DayOfWeek.Tuesday => "Thứ Ba",
                DayOfWeek.Wednesday => "Thứ Tư",
                DayOfWeek.Thursday => "Thứ Năm",
                DayOfWeek.Friday => "Thứ Sáu",
                DayOfWeek.Saturday => "Thứ Bảy",
                _ => "Chủ nhật"
            };
=======
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
            frmDangNhap f = new frmDangNhap();
            f.Show();
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
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
        }
    }
}
