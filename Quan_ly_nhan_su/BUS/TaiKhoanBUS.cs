using System;
using Quan_ly_nhan_su.DAL;

namespace Quan_ly_nhan_su.BUS
{
    internal class TaiKhoanBUS
    {
        private TaiKhoanDAL dal = new TaiKhoanDAL();
        public string DangNhap(string taikhoan, string matkhau)
        {
            if (string.IsNullOrWhiteSpace(taikhoan) || string.IsNullOrWhiteSpace(matkhau))
            {
                return string.Empty;
            }

            return dal.KiemTraDangNhap(taikhoan.Trim(), matkhau);
        }
    }
}
