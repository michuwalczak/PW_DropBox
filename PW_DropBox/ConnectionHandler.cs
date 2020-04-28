using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PW_DropBox
{
    public class ConnectionHandler : Interface.IDropBox
    {
        // Download file from server to target directory.
        public void DownloadFile(string clientName, string fileName, string targetDirectory)
        {
            string sourcePath = Configuration.ServerDirectory + "\\" + clientName + "\\" + fileName;
            string destinationPath = targetDirectory + "\\" + fileName;
                          
            File.Copy(sourcePath, destinationPath);
        }

        // Send list of files in directory.
        public string[] FileList(string clientName)
        {
            var thread = Server.GetThread();
            thread.Start();

            string path = Configuration.ServerDirectory + "\\" + clientName;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Directory.GetFiles(path);
        }

        // Upload file to server from source directory.
        public void UploadFile(string clientName, string fileName, string sourceDirectory)
        {            
            string sourcePath = sourceDirectory + "\\" + fileName;
            string destinationPath = Configuration.ServerDirectory + "\\" + clientName + "\\" + fileName;

            File.Copy(sourcePath, destinationPath);
        }
    }
}
