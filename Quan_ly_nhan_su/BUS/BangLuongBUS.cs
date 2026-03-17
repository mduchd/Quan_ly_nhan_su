using Quan_ly_nhan_su.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quan_ly_nhan_su.BUS
{
    public  class BangLuongBUS
    {
        public decimal TinhTongLuong(BangLuongDTO nv)
        {
            decimal tongLuong = 0;

            if(nv.SoNgayLam >= 30)
            {
                tongLuong = nv.LuongCung + 1000000;
            }
            else if(nv.SoNgayLam >= 20)
            {
                tongLuong = nv.LuongCung - (nv.SoNgayNghi * 300000);
            }
            else
            {
                tongLuong = (nv.LuongCung / 30 * nv.SoNgayLam) - (nv.SoNgayNghi * 300000);
            }
            return tongLuong < 0 ? 0 : tongLuong;
        }
        public List<BangLuongDTO> TimKiemNhanVien(List<BangLuongDTO> dsGoc, string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                return dsGoc;
            }
            tuKhoa = tuKhoa.ToLower();
            var dsKetQua = dsGoc.Where(nv => nv.MaNV.ToLower().Contains(tuKhoa) || nv.TenNV.ToLower().Contains(tuKhoa)).ToList();
            return dsKetQua;        
        }
    }
}
