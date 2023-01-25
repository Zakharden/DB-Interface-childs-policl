using System;
using System.Windows.Forms;

namespace Childs_Polic
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Database data = new Database();
            data.OpenConnection();
            //data.CloseConnection();
            Application.Run(new Main_Menu());
        }
    }
}
