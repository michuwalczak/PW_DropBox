using System.Linq;
using System.IO;
using Library;

namespace PW_DropBox
{
    public class HttpRequestHandler : IDropBox
    {
        // Download file from server to target directory.
        public void DownloadFile(Cookie cookie, string fileName)
        {
            var clientAccount = Server.LoggedClients.Where(c => c.Name == cookie.ClientName).ToArray()[0];

            var serverTask = new ServerTask(ServerTask.TaskType.Download)
            {
                Account = clientAccount,
                FileName = fileName,
                SourcePath = Configuration.ServerDirectory + "\\" + cookie.ClientName + "\\" + fileName,
                DestinationPath = cookie.FolderPath + "\\" + fileName,
                Duration = FileOperation.ParseFileName(fileName)
            };


            serverTask.Account.PerformedOperations++;
            serverTask.Account.TransferTotalSize += serverTask.Duration;

            Server.AddTask(serverTask);
        }

        // Send list of files in directory.
        public string[] FileList(Cookie cookie)
        {
            string path = Configuration.ServerDirectory + "\\" + cookie.ClientName;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Directory.GetFiles(path).Select(Path.GetFileName).ToArray();
        }

        // Upload file to server from source directory.
        public void UploadFile(Cookie cookie, string fileName)
        {
            var clientAccount = Server.LoggedClients.Where(c => c.Name == cookie.ClientName).ToArray()[0];

            var serverTask = new ServerTask(ServerTask.TaskType.Upload)
            {
                Account = clientAccount,
                FileName = fileName,
                SourcePath = cookie.FolderPath + "\\" + fileName,
                DestinationPath = Configuration.ServerDirectory + "\\" + cookie.ClientName + "\\" + fileName,
                Duration = FileOperation.ParseFileName(fileName)
            };


            serverTask.Account.PerformedOperations++;
            serverTask.Account.TransferTotalSize += serverTask.Duration;

            Server.AddTask(serverTask);
        }

        // Delete file from server.
        public void DeleteFile(Cookie cookie, string fileName)
        {
            var filePath = Configuration.ServerDirectory + "\\" + cookie.ClientName + "\\" + fileName;
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        // Login client
        public Cookie LogIn(string clientName, string folderPath) 
        {
            var clientAccount = new ClientAccount()
            {
                Name = clientName,
                IsPremium = Configuration.PremiumClients.Contains(clientName),
                PerformedOperations = 0,
                TransferTotalSize = 0
            };

            Server.LogClientIn(clientAccount);

            return new Cookie(clientName, folderPath);
        }

        // Logout client
        public void LogOut(Cookie cookie)
        {
            var clientAccount = Server.LoggedClients.Where(c => c.Name == cookie.ClientName).ToArray()[0];
            Server.LogClientOut(clientAccount);
        }
    }
}
