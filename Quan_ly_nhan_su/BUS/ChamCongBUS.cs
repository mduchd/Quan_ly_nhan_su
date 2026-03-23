using System;
using System.Collections.Generic;
using Quan_ly_nhan_su.DAL;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.BUS
{
    internal class ChamCongBUS
    {
        private readonly ChamCongDAL _chamCongDAL = new();

        public string TaoYeuCauNghiPhep(ChamCongDTO yeuCau)
        {
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
        }
    }
}
