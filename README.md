# DỰ ÁN QUẢN LÝ NHÂN VIÊN & CHẤM CÔNG

Dự án môn học C#/.NET - Nhóm 4 thành viên. 
Kiến trúc: **3-Layer Architecture (GUI - BUS - DAL - DTO)**.

---

## 1. Cấu trúc thư mục (Folder Structure)

| Thư mục | Loại file | Vai trò |  
| :--- | :--- | :--- |
| **GUI** | UserControl (.cs) | Chứa giao diện kéo thả của từng chức năng. |
| **BUS** | Class (.cs) | Xử lý logic nghiệp vụ (Tính lương, check điều kiện). |
| **DAL** | Class (.cs) | Thực hiện truy vấn SQL (SELECT, INSERT, UPDATE, DELETE). |
| **DTO** | Class (.cs) | Khai báo các đối tượng (Nhân viên, Bảng lương,...) để truyền dữ liệu. |

---

## 2. Phân công nhiệm vụ (Assignments)

Dự án được chia theo chức năng để tránh xung đột khi làm việc trên Git:

1. **Phân hệ Hệ thống (Nguyễn Minh Đức):**
   - Thiết kế `MainForm.cs`, giao diện điều hướng.
   - Quản lý đăng nhập và file `DbContext.cs` (Kết nối Database).
2. **Phân hệ Nhân sự (Vũ Ngọc Sơn): **
   - Quản lý hồ sơ, phòng ban (Thêm/Sửa/Xóa nhân viên).
   - Tệp chính: `GUI/ucNhanVien.cs`.
3. **Phân hệ Chấm công (Lê Ngọc Duy): **
   - Ghi nhận giờ vào/ra, tính ngày công, xử lý nghỉ phép.
   - Tệp chính: `GUI/ucChamCong.cs`.
4. **Phân hệ Tiền lương (Phạm Viết Tuấn): **
   - Tính lương tổng (Lương cứng + Thưởng - Phạt theo ngày nghỉ).
   - Tệp chính: `GUI/ucBangLuong.cs`.

---

## 3. Quy trình phối hợp trên Git

Để dự án không bị lỗi giao diện, các thành viên tuân thủ:
* **Pull code:** Thực hiện Pull trước khi bắt đầu làm việc để cập nhật code mới nhất.
* **Làm việc trên UserControl:** Chỉ thiết kế giao diện trong file `uc...` mình được giao trong folder GUI.
* **Commit & Push:** Đẩy code thường xuyên sau khi hoàn thành một hàm hoặc một nút chức năng.
* **Scaling:** Chỉnh màn hình Windows về **100%** khi kéo thả giao diện.

---

## 4. Cơ chế tính lương (Tham khảo)

* **Đi làm đủ (>= 30 ngày):** Lương cứng + Thưởng (1,000,000 VNĐ).
* **Nghỉ 1 ngày:** (Lương cứng - 300,000 VNĐ), không có thưởng.
* **Nghỉ nhiều (< 20 ngày đi làm):** `(Lương cứng / 30 * Số ngày làm) - (Số ngày nghỉ * 300,000 VNĐ)`.

---
*Dự án được khởi tạo và quản lý bởi Duc (HAU - IT).*
