using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PwdBox
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            settings settings = new settings();
            password password = new password();

            settings.SelectStartPassword();

            // По умолчанию установить аторизацию
            //Application.Run(password);
            //if (password.DialogResult == DialogResult.OK)
            //Application.Run(new Form1());

        }
    }
}
