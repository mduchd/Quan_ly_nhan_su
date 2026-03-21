using System;

namespace Quan_ly_nhan_su.DTO
{
    public class NhanVienDTO
    {
        public string MaNV { get; set; } = string.Empty;
        public string TenNV { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; } = "Nam";
        public string ChucVu { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public DateTime NgayVaoLam { get; set; }
        public bool TrangThai { get; set; } = true;
        public string PhongBan { get; set; } = string.Empty;
        public decimal LuongCung { get; set; }
    }
}
