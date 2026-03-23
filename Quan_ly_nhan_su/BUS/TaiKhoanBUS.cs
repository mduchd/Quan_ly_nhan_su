<<<<<<< HEAD
﻿using System;
=======
using System;
using System.Collections.Generic;
using System.Text;
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
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
<<<<<<< HEAD
                return null;
=======
                System.Windows.Forms.MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập.");
                return false;
>>>>>>> d26ba5df7ff7f0fdc50b5959ad37b4224078c120
            }

            return dal.KiemTraDangNhap(taikhoan.Trim(), matkhau);
        }
    }
}
