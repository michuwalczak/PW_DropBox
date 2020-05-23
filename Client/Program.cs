using System;
using System.Collections.Generic;
using System.IO;
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
            Configuration.Load(Application.StartupPath);

            connection.LogIn();

            var serverFiles = connection.GetFileList();
            var clientFiles = GetFilesList();
            var newServerFiles = serverFiles.Except(clientFiles);

            foreach (var file in newServerFiles)
                connection.DownloadFile(file);

            // Start client thread
            var fileWatcher = new FileWatcher(Configuration.LocalFolderDirectory);

            fileWatcher.Changed += File_Changed;
            fileWatcher.Deleted += File_Deleted;

            var mainForm = new Form1();
            mainForm.Text = Configuration.UserName;
            mainForm.FormClosing += MainForm_FormClosing;
            Application.Run(mainForm);
        }

        private static void File_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            var serverFiles = connection.GetFileList();
            if(!serverFiles.Contains(e.Name))
                connection.UploadFile(e.Name);
        }

        private static void File_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            connection.DeleteFile(e.Name);
        }

        private static string[] GetFilesList()
        {
            return Directory.GetFiles(Configuration.LocalFolderDirectory).Select(Path.GetFileName).ToArray();
        }

        private static void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                connection.LogOut();
            }
            catch
            {
                Console.WriteLine("Connection log out error.");
            }          
        }
    }
}
