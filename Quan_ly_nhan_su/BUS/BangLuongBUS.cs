using Quan_ly_nhan_su.DTO;
using Quan_ly_nhan_su.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quan_ly_nhan_su.BUS
{
    public class BangLuongBUS
    {
        BangLuongDAL dal = new BangLuongDAL();

        public decimal TinhTongLuong(BangLuongDTO nv)
        {
            if (nv == null) return 0m;
            var soNgayChuan = nv.SoNgayChuan > 0 ? nv.SoNgayChuan : 30;
            var ngayLam = Math.Clamp(nv.SoNgayLam, 0, soNgayChuan);
            decimal tong = nv.LuongCung * ngayLam / soNgayChuan;
            return Math.Round(tong, 0);
        }

        // HÀM MỚI: Validate và gọi DAL
        public bool CapNhatLuongCung(string maNV, decimal luongMoi)
        {
            if (luongMoi < 0) return false;
            return dal.CapNhatLuongCung(maNV, luongMoi);
        }
    }
}