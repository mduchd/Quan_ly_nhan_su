using Quan_ly_nhan_su.GUI;

namespace Quan_ly_nhan_su
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmDangNhap());
        }
    }
}
