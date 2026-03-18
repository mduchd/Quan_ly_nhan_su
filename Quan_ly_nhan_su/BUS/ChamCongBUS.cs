using System;
using System.Collections.Generic;
using System.Text;
using Quan_ly_nhan_su.DAL;
using Quan_ly_nhan_su.DTO;
// Xử lý nghiệp vụ 
namespace Quan_ly_nhan_su.BUS
{
    internal class ChamCongBUS
    {
        private ChamCongDAL yeuCauDal = new ChamCongDAL();

        public string TaoYeuCauNghiPhep(ChamCongDTO yeuCau)
        {
            if(yeuCau.TuNgay > yeuCau.DenNgay)
            {
                return "Ngày kết thúc không được nhỏ hơn ngày bắt đầu";
            }
            if(string.IsNullOrEmpty(yeuCau.LyDo))
            {
                return "Lý do không được để trống";
            }
            bool thanhCong = yeuCauDal.ThemYeuCau(yeuCau);
            if (thanhCong)
            {
                return "Thành công";
            }
            else
            {
                return "Thất bại";
            }
        }
    }
}
