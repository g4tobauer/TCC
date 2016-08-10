using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerApplication
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new System.Threading.Thread(abrirServer).Start();
            //new System.Threading.Thread(abrirClient).Start();
        }
        static void abrirServer()
        {
            Application.Run(new Forms.ServerForm());
        }
    }
}
