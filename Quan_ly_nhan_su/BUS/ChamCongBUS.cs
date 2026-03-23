<<<<<<< HEAD
using System;
using System.Collections.Generic;
using Quan_ly_nhan_su.DAL;
using Quan_ly_nhan_su.DTO;

=======
﻿using DocumentFormat.OpenXml.Bibliography;
using Quan_ly_nhan_su.DAL;
using Quan_ly_nhan_su.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
// Xử lý nghiệp vụ 
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
namespace Quan_ly_nhan_su.BUS
{
    internal class ChamCongBUS
    {
<<<<<<< HEAD
        private readonly ChamCongDAL _chamCongDAL = new();
=======
        private ChamCongDAL dal = new ChamCongDAL();
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120

        public int KiemTraTrangThai(string maNV)
        {
<<<<<<< HEAD
            if (yeuCau.TuNgay > yeuCau.DenNgay)
            {
                return "Ngày kết thúc không được nhỏ hơn ngày bắt đầu";
            }

            if (string.IsNullOrWhiteSpace(yeuCau.LyDo))
            {
                return "Lý do không được để trống";
            }

            bool thanhCong = _chamCongDAL.ThemYeuCau(yeuCau);
            return thanhCong ? "Thành công" : "Thất bại";
        }

        public NhanVienDTO? LayThongTinNhanVien(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
            {
                return null;
            }

            return _chamCongDAL.LayThongTinNhanVien(maNV.Trim());
        }

        public List<LichSuCongDTO> LayLichSuGanDay(string maNV, int soBanGhi = 3)
        {
            if (string.IsNullOrWhiteSpace(maNV))
            {
                return new List<LichSuCongDTO>();
            }

            return _chamCongDAL.LayLichSuChamCongGanDay(maNV.Trim(), soBanGhi);
        }

        public string CheckIn(string maNV, DateTime thoiDiem)
        {
            return _chamCongDAL.CheckIn(maNV, thoiDiem, out var message) ? "Thành công" : message;
        }

        public string CheckOut(string maNV, DateTime thoiDiem)
        {
            return _chamCongDAL.CheckOut(maNV, thoiDiem, out var message) ? "Thành công" : message;
=======
            return dal.KiemTraTrangThai(maNV);
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
        }

        // Hàm bắc cầu số 2
        public bool CapNhatTrangThaiCheckIn(string maNV, int trangThai)
        {
            return dal.CapNhatTrangThaiCheckIn(maNV, trangThai);
        }
        public bool CapNhatTrangThaiCheckOut(string maNV,double tongGio)
        {
            return dal.CapNhatTrangThaiCheckOut(maNV, tongGio);
        }
        public ChamCongDTO LayThongTinChamCong(string maNV)
        {
            return dal.LayThongTinChamCong(maNV);
        }
        public DataTable LayLichSuChamCong(string maNV)
        {
            return dal.LayLichSuChamCong(maNV);
        }
        public (string tenNV, string SDT, string DiaChi) LayThongTinNhanVien(string maNV)
        {
            return dal.LayThongTinNhanVien(maNV);
        }
        public string XuLyCheckIn(string maNV)
        {
            if (string.IsNullOrEmpty(maNV)) return "Vui lòng nhập mã nhân viên!";

            int trangThai = dal.KiemTraTrangThai(maNV);
            if (trangThai == -1) return "Mã nhân viên không tồn tại trong hệ thống!";
            if (trangThai == 1) return "Nhân viên này đã Check-in rồi!";

            bool isSuccess = dal.CapNhatTrangThaiCheckIn(maNV, 1);
            if (isSuccess) return "Thành công";
            return "Check-in thất bại. Vui lòng thử lại!";
        }

        // Nhận vào mã NV, tự động lấy giờ, trừ giờ, rồi gọi DAL lưu xuống DB
        public string XuLyCheckOut(string maNV, out TimeSpan gioVao, out TimeSpan gioRa, out TimeSpan tongThoiGian)
        {
            // Gán giá trị mặc định cho các biến out
            gioVao = TimeSpan.Zero;
            gioRa = TimeSpan.Zero;
            tongThoiGian = TimeSpan.Zero;

            if (string.IsNullOrEmpty(maNV)) return "Vui lòng nhập mã nhân viên!";

            int trangThai = dal.KiemTraTrangThai(maNV);
            if (trangThai == -1) return "Mã nhân viên không tồn tại trong hệ thống!";
            if (trangThai == 0) return "Bạn chưa Check-In. Vui lòng Check-In trước khi Check-Out!";

            ChamCongDTO thongtin = dal.LayThongTinChamCong(maNV);
            if (thongtin != null && thongtin.GioVao != null)
            {
                // Thực hiện tính toán thời gian ở tầng BUS
                gioRa = DateTime.Now.TimeOfDay;
                gioVao = thongtin.GioVao.Value;
                tongThoiGian = gioRa - gioVao;
                double tongGioLam = tongThoiGian.TotalHours;

                // Ra lệnh cho DAL cập nhật xuống SQL
                bool isSuccess = dal.CapNhatTrangThaiCheckOut(maNV, tongGioLam);
                if (isSuccess) return "Thành công";
                return "Check-out thất bại. Vui lòng thử lại!";
            }
            return "Không lấy được giờ vào của nhân viên này để tính toán!";
        }


    }
}
