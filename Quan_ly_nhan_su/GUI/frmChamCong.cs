using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Quan_ly_nhan_su.BUS;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.GUI
{
    public partial class frmChamCong : Form
    {
        private readonly ChamCongBUS _chamCongBUS = new();
        private string _currentEmployeeId = string.Empty;

        public frmChamCong()
        {
            InitializeComponent();
            timerClock.Interval = 1000;
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
        }

        private void SetCheckOutState(bool enabled)
        {
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
            }

            if (!string.IsNullOrWhiteSpace(maNVNhap))
            {
                return maNVNhap;
            }

            return _currentEmployeeId;
        }

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
        }
    }
}
