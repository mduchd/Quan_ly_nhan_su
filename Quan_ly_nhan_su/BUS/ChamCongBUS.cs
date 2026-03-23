using DocumentFormat.OpenXml.Bibliography;
using Quan_ly_nhan_su.DAL;
using Quan_ly_nhan_su.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
// Xử lý nghiệp vụ 
namespace Quan_ly_nhan_su.BUS
{
    internal class ChamCongBUS
    {
        private ChamCongDAL dal = new ChamCongDAL();

        public int KiemTraTrangThai(string maNV)
        {
            return dal.KiemTraTrangThai(maNV);
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

        public string XuLyCheckOut(string maNV, out TimeSpan gioVao, out TimeSpan gioRa, out TimeSpan tongThoiGian)
        {
          
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
                gioRa = DateTime.Now.TimeOfDay;
                gioVao = thongtin.GioVao.Value;
                tongThoiGian = gioRa - gioVao;
                double tongGioLam = tongThoiGian.TotalHours;

              
                bool isSuccess = dal.CapNhatTrangThaiCheckOut(maNV, tongGioLam);
                if (isSuccess) return "Thành công";
                return "Check-out thất bại. Vui lòng thử lại!";
            }
            return "Không lấy được giờ vào của nhân viên này để tính toán!";
        }


    }
}
