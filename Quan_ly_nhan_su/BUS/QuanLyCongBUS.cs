using Quan_ly_nhan_su.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Quan_ly_nhan_su.BUS
{
    internal class QuanLyCongBUS
    {
        private QuanLyCongDAL dal = new QuanLyCongDAL();
        public DataTable LayDanhSachLichSuCong(string tuKhoa, DateTime tuNgay, DateTime denNgay)
        {
            // xoa khoang trang thua o dau va cuoi chuoi tuKhoa
            if (!string.IsNullOrEmpty(tuKhoa)) {
                tuKhoa = tuKhoa.Trim();
            }

            // neu chon tu ngay > den ngay thi tu dong dao nguoc lai
            if (tuNgay > denNgay)
            {
                DateTime temp = tuNgay;
                tuNgay = denNgay;
                denNgay = temp;
            }

            if ((denNgay - tuNgay).TotalDays > 365)
            {
                throw new Exception("He thong chi cho phep xem lich su cham cong trong vong 1 nam. Vui long chon lai khoang thoi gian.");
            }
            return dal.LayDanhSachLichSuCong(tuKhoa, tuNgay, denNgay);
        }
    }
}
