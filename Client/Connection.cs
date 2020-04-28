using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace PW_DropBox
{
    public class Connection
    {
        public void DownloadFile(string fileName)
        {
            Uri adres = new Uri("http://localhost:2222/Server");
            using (var channelFactory = new ChannelFactory<Interface.IDropBox>(
              new BasicHttpBinding(),
              new EndpointAddress(adres)))
            {
                var channel = channelFactory.CreateChannel();
                channel.DownloadFile(Configuration.UserName, fileName, Configuration.FileDirectory);
            }
        }

        public string[] GetFileList()
        {
            Uri adres = new Uri("http://localhost:2222/Server");
            using (var channelFactory = new ChannelFactory<Interface.IDropBox>(
              new BasicHttpBinding(),
              new EndpointAddress(adres)))
            {
                var channel = channelFactory.CreateChannel();
                return channel.FileList(Configuration.UserName);
            }
        }

        public void UploadFile(string fileName)
        {
            Uri adres = new Uri("http://localhost:2222/Server");
            using (var channelFactory = new ChannelFactory<Interface.IDropBox>(
              new BasicHttpBinding(),
              new EndpointAddress(adres)))
            {
                var channel = channelFactory.CreateChannel();
                channel.UploadFile(Configuration.UserName, fileName, Configuration.FileDirectory);
            }
        }
    }
}
