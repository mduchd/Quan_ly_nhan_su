using System;
using Quan_ly_nhan_su.DAL;

namespace Quan_ly_nhan_su.BUS
{
    internal class TaiKhoanBUS
    {
        private TaiKhoanDAL dal = new TaiKhoanDAL();
        public bool DangNhap(string taikhoan, string matkhau)
        {
            if (string.IsNullOrWhiteSpace(taikhoan) || string.IsNullOrWhiteSpace(matkhau))
            {
                return false;
            }

            return dal.KiemTraDangNhap(taikhoan.Trim(), matkhau);
        }

        public string DoiMatKhau(string taikhoan, string matkhauCu, string matkhauMoi, string xacNhanMatKhauMoi)
        {
            if (string.IsNullOrWhiteSpace(matkhauCu) || string.IsNullOrWhiteSpace(matkhauMoi))
                return "Vui lòng nhập đầy đủ thông tin mật khẩu!";

            if (matkhauMoi != xacNhanMatKhauMoi)
                return "Mật khẩu mới và xác nhận mật khẩu không khớp!";

            if (matkhauMoi.Length < 6)
                return "Mật khẩu mới phải có ít nhất 6 ký tự!";

            bool ketQua = dal.DoiMatKhau(taikhoan.Trim(), matkhauCu, matkhauMoi);
            
            if (ketQua)
                return "Thành công";
            else
                return "Mật khẩu cũ không chính xác!";
        }
    }
}
