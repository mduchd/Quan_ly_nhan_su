-- 1. Tạo Database nếu chưa có
IF DB_ID(N'QL_Nhansu') IS NULL
    CREATE DATABASE QL_Nhansu;
GO
USE QL_Nhansu;
GO

-- 2. XÓA BẢNG CON TRƯỚC (Chứa FOREIGN KEY tham chiếu đến bảng NhanVien)
IF OBJECT_ID(N'dbo.ChiTietChamCong', N'U') IS NOT NULL DROP TABLE dbo.ChiTietChamCong;
IF OBJECT_ID(N'dbo.ChamCong', N'U') IS NOT NULL DROP TABLE dbo.ChamCong;
GO

-- 3. XÓA CÁC BẢNG KHÁC (Không tham chiếu hoặc không bị tham chiếu qua khóa ngoại)
IF OBJECT_ID(N'dbo.TAI_KHOAN', N'U') IS NOT NULL DROP TABLE dbo.TAI_KHOAN;
IF OBJECT_ID(N'dbo.PhongBan', N'U') IS NOT NULL DROP TABLE dbo.PhongBan;
GO

-- 4. XÓA BẢNG CHA (NhanVien)
IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NOT NULL DROP TABLE dbo.NhanVien;
GO

---------------------------------------------------------
-- TẠO CÁC BẢNG THEO THỨ TỰ (Bảng cha trước, bảng con sau)
---------------------------------------------------------

-- 1) PhongBan
CREATE TABLE dbo.PhongBan
(
    MaPhongBan  INT IDENTITY(1,1) PRIMARY KEY,
    TenPhongBan NVARCHAR(100) NOT NULL UNIQUE
);
GO

-- 2) NhanVien
CREATE TABLE dbo.NhanVien
(
    MaNV        VARCHAR(20) PRIMARY KEY,
    TenNV       NVARCHAR(150) NOT NULL,
    NgaySinh    DATE NULL,
    GioiTinh    NVARCHAR(10) NULL,
    ChucVu      NVARCHAR(100) NULL,
    SoDienThoai VARCHAR(20) NULL,
    Email       VARCHAR(150) NULL,
    DiaChi      NVARCHAR(255) NULL,
    NgayVaoLam  DATE NULL,
    TrangThai   BIT NOT NULL DEFAULT(1),
    PhongBan    NVARCHAR(100) NULL,
    LuongCung   DECIMAL(18,2) NOT NULL DEFAULT(0)
);
GO

-- 3) ChamCong (Dữ liệu chấm công thô / tổng hợp)
CREATE TABLE dbo.ChamCong
(
    ID           INT IDENTITY(1,1) PRIMARY KEY,
    MaNV         VARCHAR(20)     NOT NULL,
    NgayChamCong DATE            NOT NULL,
    GioVao       TIME(7)         NULL,
    GioRa        TIME(7)         NULL,
    TongGio      DECIMAL(10,2)   NULL,
    CONSTRAINT FK_ChamCong_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV),
    CONSTRAINT UQ_ChamCong_MaNV_Ngay UNIQUE (MaNV, NgayChamCong)
);
GO

-- 4) ChiTietChamCong (Dữ liệu lịch sử quét thẻ)
CREATE TABLE dbo.ChiTietChamCong
(
    Id         INT IDENTITY(1,1) PRIMARY KEY,
    MaNV       VARCHAR(20) NOT NULL,
    Ngay       DATE NOT NULL,
    GioVao     TIME(7) NULL,
    GioRa      TIME(7) NULL,
    SoNgayLam  DECIMAL(5,2) NULL,
    ThangNam   CHAR(7) NULL, -- Format YYYY-MM
    TrangThai  BIT NOT NULL DEFAULT(1),
    CreatedAt  DATETIME2 NOT NULL DEFAULT(SYSDATETIME()),
    CONSTRAINT FK_ChiTietChamCong_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV)
);
GO

-- 5) TAI_KHOAN
CREATE TABLE dbo.TAI_KHOAN
(
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau     VARCHAR(255) NOT NULL,
    Quyen       NVARCHAR(30) NOT NULL
);
GO

---------------------------------------------------------
-- CHÈN DỮ LIỆU MẪU
---------------------------------------------------------

INSERT INTO dbo.TAI_KHOAN (TenDangNhap, MatKhau, Quyen)
VALUES ('admin', '123456', N'Admin');

INSERT INTO dbo.NhanVien (MaNV, TenNV, ChucVu, SoDienThoai, LuongCung)
VALUES 
('NV001', N'Admin Test', N'Quản trị viên', '0123456789', 20000000),
('NV002', N'Nhân Viên A', N'Công nhân', '0987654321', 10000000);
GO

PRINT 'Hoàn thành việc cấu hình DB QL_Nhansu thành công!';
