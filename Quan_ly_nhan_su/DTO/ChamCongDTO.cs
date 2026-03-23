using System;
using System.Collections.Generic;
using System.Text;


namespace Quan_ly_nhan_su.DTO
{
    internal class ChamCongDTO
    {
        public string MaNV {  get; set; }
        public string Hoten { get; set; }
        public string SDT { get; set; }
        public string Diachi { get; set; }
        public int Trangthai { get; set; }
        public DateTime? NgayChamCong { get; set; }
        public TimeSpan? GioVao { get; set; }
        public TimeSpan? GioRa { get; set; }
    }
}
