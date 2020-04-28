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
        private static Connection connection = new Connection();
        
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load configuration
            Configuration.FileDirectory = @"D:\Studia\Semestr_2\Programowanie wspolbiezne\Laboratorium\Projekt\Client";

            connection.GetFileList();

            // Start client thread
            var fileWatcher = new FileWatcher(Configuration.FileDirectory);

            fileWatcher.Changed += FileWatcher_Changed;


            


            Application.Run(new Form1());
        }

        private static void FileWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            connection.UploadFile(e.Name);
        }
    }
}
