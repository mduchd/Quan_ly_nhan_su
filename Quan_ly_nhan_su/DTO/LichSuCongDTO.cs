using System;
using System.Collections.Generic;
using System.Text;

namespace Quan_ly_nhan_su.DTO
{
    internal class LichSuCongDTO
    {
        public string maNV { get; set; }
        public string hoTen { get; set; }
        public DateTime ngay { get; set; }
        public TimeSpan? gioVao { get; set; }
        public TimeSpan? gioRa { get; set; }
        public string trangThai { get; set; }


    }
}
