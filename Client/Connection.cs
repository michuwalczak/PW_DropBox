using System;
using System.ServiceModel;
using Library;

namespace PW_DropBox
{
    public class Connection
    {
        private Uri adres = new Uri("http://localhost:2222/Server");

        public void DownloadFile(string fileName)
        {           
            using (var channelFactory = new ChannelFactory<IDropBox>(
              new BasicHttpBinding(),
              new EndpointAddress(adres)))
            {
                var channel = channelFactory.CreateChannel();
                channel.DownloadFile(Client.Cookie, fileName);
            }
        }

        public string[] GetFileList()
        {
            using (var channelFactory = new ChannelFactory<IDropBox>(
              new BasicHttpBinding(),
              new EndpointAddress(adres)))
            {
                var channel = channelFactory.CreateChannel();
                return channel.FileList(Client.Cookie);
            }
        }

        public void UploadFile(string fileName)
        {
            using (var channelFactory = new ChannelFactory<IDropBox>(
              new BasicHttpBinding(),
              new EndpointAddress(adres)))
            {
                var channel = channelFactory.CreateChannel();
                channel.UploadFile(Client.Cookie, fileName);
            }
        }

        public void LogIn()
        {
            using (var channelFactory = new ChannelFactory<IDropBox>(
              new BasicHttpBinding(),
              new EndpointAddress(adres)))
            {
                var channel = channelFactory.CreateChannel();
                Client.Cookie = channel.LogIn(Configuration.UserName, Configuration.LocalFolderDirectory);
            }
        }

        public void LogOut()
        {
            using (var channelFactory = new ChannelFactory<IDropBox>(
              new BasicHttpBinding(),
              new EndpointAddress(adres)))
            {
                var channel = channelFactory.CreateChannel();
                channel.LogOut(Client.Cookie);
            }
        }
    }
}
