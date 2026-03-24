using System;
using System.Collections.Generic;
using System.Text;

namespace Quan_ly_nhan_su.DTO
{
    public class BangLuongDTO
    {
        public string MaNV { get; set; } = string.Empty;
        public string TenNV { get; set; } = string.Empty;
        public decimal LuongCung { get; set; }
        public int SoNgayLam { get; set; }
        public int SoNgayChuan { get; set; } = 30;
        public int SoNgayNghi
        {
            get { return Math.Max(SoNgayChuan - SoNgayLam, 0); }
        }
        public decimal TongLuong { get; set; }
    }
}