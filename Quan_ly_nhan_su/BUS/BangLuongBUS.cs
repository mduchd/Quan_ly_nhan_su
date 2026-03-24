using Quan_ly_nhan_su.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quan_ly_nhan_su.BUS
{
    public class BangLuongBUS
    {
        public decimal TinhTongLuong(BangLuongDTO nv)
        {
            if (nv == null) return 0m;
            var soNgayChuan = nv.SoNgayChuan > 0 ? nv.SoNgayChuan : 30;
            var ngayLam = Math.Clamp(nv.SoNgayLam, 0, soNgayChuan);
            decimal tong = nv.LuongCung * ngayLam / soNgayChuan;
            return Math.Round(tong, 0);
        }
    }
}
