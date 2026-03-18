using System;
using System.Collections.Generic;
using System.Text;
using Quan_ly_nhan_su.DAL;

namespace Quan_ly_nhan_su.BUS
{
    internal class TaiKhoanBUS
    {
        private TaiKhoanDAL dal = new TaiKhoanDAL();
        public string DangNhap(string taikhoan, string matkhau)
        {
            if (string.IsNullOrEmpty(taikhoan) || string.IsNullOrEmpty(matkhau))
            {
                System.Windows.Forms.MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập.");
            }
            return dal.KiemTraDangNhap(taikhoan, matkhau);
        }
    }
}
