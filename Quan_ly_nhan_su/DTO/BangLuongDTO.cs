using System;
using System.Collections.Generic;
using System.Text;

namespace Quan_ly_nhan_su.DTO
{
    public  class BangLuongDTO
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public decimal LuongCung { get; set; }
        public int SoNgayLam { get; set; }
        public int SoNgayNghi
        {
            get { return (30 - SoNgayLam) > 0 ? (30 - SoNgayLam) : 0; }
        }
        public decimal TongLuong { get; set; }
    }
}
