using Quan_ly_nhan_su.GUI.ChamCongNghiPhep;

namespace Quan_ly_nhan_su
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClickTest_Click(object sender, EventArgs e)
        {
            pnContainer.Controls.Clear();
            ucChamCong uc = new ucChamCong();

            // 3. Thiết lập cho ucChamCong tự động phóng to lấp đầy cái Panel
            uc.Dock = DockStyle.Fill;

            // 4. Nhúng ucChamCong vào Panel để nó hiện lên
            pnContainer.Controls.Add(uc);

            // 5. (Tùy chọn) Gọi hàm load dữ liệu nếu bạn muốn thấy data ảo ngay lập tức
            uc.loadDanhSachNghiPhep();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pnContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnContainer.Controls.Clear();
           
        }
    }
}
