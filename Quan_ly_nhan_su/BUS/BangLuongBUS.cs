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
            const decimal daysInMonth = 30m;
            int ngayLam = nv.SoNgayLam < 0 ? 0 : nv.SoNgayLam;
            // Calculate proportional salary based on days worked
            decimal tong = nv.LuongCung * (ngayLam / daysInMonth);
            return Math.Round(tong, 0);
        }
    }
}
