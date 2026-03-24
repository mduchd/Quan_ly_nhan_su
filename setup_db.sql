-- 1. Xóa các bảng hiện có (Xóa bảng con trước, bảng cha sau)
IF OBJECT_ID('dbo.ChiTietChamCong', 'U') IS NOT NULL DROP TABLE dbo.ChiTietChamCong;
IF OBJECT_ID('dbo.ChamCong', 'U') IS NOT NULL DROP TABLE dbo.ChamCong;
IF OBJECT_ID('dbo.TaiKhoan', 'U') IS NOT NULL DROP TABLE dbo.TaiKhoan;
IF OBJECT_ID('dbo.NhanVien', 'U') IS NOT NULL DROP TABLE dbo.NhanVien;
GO

-- 2. Tạo bảng NhanVien
CREATE TABLE dbo.NhanVien (
    MaNV VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(100),
    TenNV NVARCHAR(100),
    SDT VARCHAR(20),
    SoDienThoai VARCHAR(20), -- Một số chỗ trong code gọi SoDienThoai thay vì SDT
    DiaChi NVARCHAR(255),
    LuongCung DECIMAL(18,2),
    Trangthai INT DEFAULT 0 -- 0: Đã ra, 1: Đang làm
);
GO

-- 3. Tạo bảng TaiKhoan
CREATE TABLE dbo.TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(50),
    MaNV VARCHAR(20) FOREIGN KEY REFERENCES dbo.NhanVien(MaNV)
);
GO

-- 4. Tạo bảng ChamCong (Lưu tổng hợp tháng/năm)
CREATE TABLE dbo.ChamCong (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaNV VARCHAR(20) FOREIGN KEY REFERENCES dbo.NhanVien(MaNV),
    Ngay DATE,
    GioVao TIME(7),
    GioRa TIME(7),
    TrangThai INT,
    SoNgayLam INT,
    ThangNam VARCHAR(20)
);
GO

-- 5. Tạo bảng ChiTietChamCong (Lưu vết check-in/out hàng ngày)
CREATE TABLE dbo.ChiTietChamCong (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaNV VARCHAR(20) FOREIGN KEY REFERENCES dbo.NhanVien(MaNV),
    NgayChamCong DATE,
    GioVao TIME(7),
    GioRa TIME(7),
    TongGio DECIMAL(5,2)
);
GO

-- 6. Chèn dữ liệu mẫu
INSERT INTO dbo.NhanVien (MaNV, HoTen, TenNV, SDT, SoDienThoai, DiaChi, LuongCung, Trangthai) 
VALUES 
('NV001', N'Nguyễn Văn A', N'Văn A', '0912345678', '0912345678', N'Hà Nội', 10000000, 0),
('NV002', N'Trần Thị B', N'Thị B', '0987654321', '0987654321', N'Hải Phòng', 12000000, 0);

INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau, MaNV)
VALUES 
('admin', '123', 'NV001'),
('user', '123', 'NV002');

-- Chèn dữ liệu chấm công mẫu cho tháng 03/2026
INSERT INTO dbo.ChamCong (MaNV, Ngay, GioVao, GioRa, TrangThai, SoNgayLam, ThangNam)
VALUES 
('NV001', '2026-03-23', '08:00:00', '17:00:00', 1, 20, '03/2026'),
('NV002', '2026-03-23', '08:30:00', '17:30:00', 1, 18, '03/2026');

PRINT 'DB Setup Completed Successfully!';
