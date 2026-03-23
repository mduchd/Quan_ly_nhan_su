using System;
using System.Collections.Generic;
using Quan_ly_nhan_su.DAL;
using Quan_ly_nhan_su.DTO;

namespace Quan_ly_nhan_su.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAL _nhanVienDAL = new();

        public List<NhanVienDTO> LayDanhSachNhanVien(string tuKhoa = "")
        {
            return _nhanVienDAL.LayDanhSachNhanVien(tuKhoa);
        }

        public List<NhanVienDTO> LayDanhSachNhanVien(string tuKhoa, int pageNumber, int pageSize, out int totalRecords)
        {
            return _nhanVienDAL.LayDanhSachNhanVien(tuKhoa, pageNumber, pageSize, out totalRecords);
        }

        public bool ThemNhanVien(NhanVienDTO nhanVien)
        {
            return _nhanVienDAL.ThemNhanVien(nhanVien);
        }

        public bool CapNhatNhanVien(NhanVienDTO nhanVien)
        {
            return _nhanVienDAL.CapNhatNhanVien(nhanVien);
        }

        public bool XoaNhanVien(string maNV)
        {
            return _nhanVienDAL.XoaNhanVien(maNV);
        }

        public bool KiemTraTrungSoDienThoaiHoacEmail(string soDienThoai, string email, string? maNVHienTai = null)
        {
            return _nhanVienDAL.KiemTraTrungSoDienThoaiHoacEmail(soDienThoai, email, maNVHienTai);
        }

        public string TaoMaNhanVienMoi()
        {
            return _nhanVienDAL.TaoMaNhanVienMoi();
        }

        public List<string> LayDanhSachPhongBan()
        {
            return _nhanVienDAL.LayDanhSachPhongBan();
        }

        public bool DoiTenPhongBan(string tenCu, string tenMoi)
        {
            return _nhanVienDAL.DoiTenPhongBan(tenCu, tenMoi);
        }

        public bool ThemPhongBan(string tenPhongBan)
        {
            return _nhanVienDAL.ThemPhongBan(tenPhongBan);
        }

        public bool XoaPhongBan(string tenPhongBan)
        {
            return _nhanVienDAL.XoaPhongBan(tenPhongBan);
        }

        public bool KiemTraPhongBanDangDuocSuDung(string tenPhongBan)
        {
            return _nhanVienDAL.KiemTraPhongBanDangDuocSuDung(tenPhongBan);
        }
    }
}
