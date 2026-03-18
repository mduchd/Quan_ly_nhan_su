using System;
using System.Collections.Generic;
using System.Text;


namespace Quan_ly_nhan_su.DTO
{
    internal class ChamCongDTO
    {
        public string TenNhanVien { get; set; }
        public string LoaiNghi { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay {  get; set; }
        public string LyDo {  get; set; }
        public string PhongBan {  get; set; }
        public string TrangThai { get; set; }
        public string ThoiGianNghi => $"{TuNgay:dd/MM} - {DenNgay:dd/MM}";
    }
}
