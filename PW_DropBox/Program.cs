using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PW_DropBox
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load configuration
            Configuration.ServerDirectory = @"D:\Studia\Semestr_2\Programowanie wspolbiezne\Laboratorium\Projekt\Server";

            // Start server thread
            var serverThread = new Thread(new ThreadStart(Server.Open));
            serverThread.Start();

            var mainForm = new Form1();
            mainForm.FormClosing += MainForm_FormClosing;
            Application.Run(mainForm);
        }

        private static void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Server.Close();
        }
    }
}
