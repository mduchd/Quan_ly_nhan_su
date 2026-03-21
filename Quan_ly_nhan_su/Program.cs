
using Quan_ly_nhan_su.GUI;

namespace Quan_ly_nhan_su

{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Form mayChamCong = new frmChamCong();
            mayChamCong.Show();
            Application.Run(new frmDangNhap());
        }
    }
}