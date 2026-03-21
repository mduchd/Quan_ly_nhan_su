# DỰ ÁN QUẢN LÝ NHÂN VIÊN & CHẤM CÔNG

Dự án môn học C#/.NET - Nhóm 4 thành viên. 
Kiến trúc: **3-Layer Architecture (GUI - BUS - DAL - DTO)**.
Mô hình triển khai: **1 Cơ sở dữ liệu - 2 Luồng ứng dụng (Kiosk & Admin)**.

---

## 1. Cấu trúc thư mục (Folder Structure)

| Thư mục | Loại file | Vai trò |  
| :--- | :--- | :--- |
| **GUI** | UserControl / Form (.cs) | Chứa giao diện kéo thả của từng chức năng. |
| **BUS** | Class (.cs) | Xử lý logic nghiệp vụ (Tính lương, check điều kiện). |
| **DAL** | Class (.cs) | Thực hiện truy vấn SQL (SELECT, INSERT, UPDATE, DELETE). |
| **DTO** | Class (.cs) | Khai báo các đối tượng (Nhân viên, Bảng lương,...) để truyền dữ liệu. |

---

## 2. Phân công nhiệm vụ (Assignments)

Dự án được chia theo chức năng để tránh xung đột (Conflict) khi làm việc trên Git:

1. **Phân hệ Hệ thống & Quản lý Chấm công (Nguyễn Minh Đức):**
   - Thiết kế `MainForm.cs`, giao diện điều hướng tổng.
   - Quản lý màn hình Đăng nhập và file cấu hình kết nối `DbContext.cs`.
   - Xây dựng màn hình danh sách lịch sử chấm công cho Admin (xem, lọc theo ngày/tháng, xuất dữ liệu).
   - Tệp chính: `MainForm.cs`, `frmDangNhap.cs`, `GUI/ucDanhSachChamCong.cs`.

2. **Phân hệ Nhân sự (Vũ Ngọc Sơn): **
   - Quản lý hồ sơ, phòng ban (Thêm/Sửa/Xóa nhân viên).
   - Tệp chính: `GUI/ucNhanVien.cs`.

3. **Phân hệ Kiosk Điểm danh (Lê Ngọc Duy): **
   - Xây dựng Form giả lập Máy chấm công (chế độ Full-screen Kiosk).
   - Xử lý logic nhân viên nhập mã để Check-in/Check-out và đẩy thẳng dữ liệu xuống Database.
   - Tệp chính: `GUI/frmMayChamCong.cs` (hoặc `frmKiosk.cs`).

4. **Phân hệ Tiền lương & Nghỉ phép (Phạm Viết Tuấn): **
   - Tính lương tổng (Lương cứng + Thưởng - Phạt theo ngày nghỉ).
   - Quản lý đơn xin nghỉ phép của nhân viên.
   - Tệp chính: `GUI/ucBangLuong.cs`, `GUI/ucNghiPhep.cs`.

---

## 3. Quy trình phối hợp trên Git

Để dự án không bị lỗi giao diện, các thành viên tuân thủ:
* **Pull code:** Thực hiện lệnh `git pull` trước khi bắt đầu làm việc để cập nhật code mới nhất từ nhánh chung.
* **Làm việc độc lập:** Chỉ thiết kế và viết code trong file `.cs` mình được giao. Tránh tự ý sửa file của người khác.
* **Commit & Push:** Đẩy code thường xuyên (kèm message rõ ràng) sau khi hoàn thành một hàm hoặc một nút chức năng.
* **Quy chuẩn UI:** Chỉnh Scale màn hình Windows về **100%** trước khi kéo thả giao diện WinForms để tránh lỗi vỡ layout trên các máy khác nhau.

---

## 4. Cơ chế tính lương (Tham khảo)

* **Đi làm đủ (>= 30 ngày):** Lương cứng + Thưởng (1,000,000 VNĐ).
* **Nghỉ 1 ngày:** (Lương cứng - 300,000 VNĐ), không có thưởng.
* **Nghỉ nhiều (< 20 ngày đi làm):** `(Lương cứng / 30 * Số ngày làm) - (Số ngày nghỉ * 300,000 VNĐ)`.

---
*Dự án được khởi tạo và quản lý bởi Duc (HAU - IT).*
