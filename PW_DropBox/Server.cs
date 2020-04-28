using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.ServiceModel;

namespace PW_DropBox
{
    public static class Server
    {
        private static ServiceHost host;
        public static readonly Thread[] threads = new Thread[2];//Thread[Configuration.MaxThreads];

        public static CommunicationState State { get { return host.State; } }
        public static ThreadState[] ThreadStates { get { return threads.Select(t => t.ThreadState).ToArray(); } }

        public static void Open()
        {
            for(int i=0; i < threads.Length; i++)
                threads[i] = new Thread(new ThreadStart(DoWork));

            Uri adres = new Uri("http://localhost:2222/Server");
            host = new ServiceHost(typeof(ConnectionHandler), adres);
            host.AddServiceEndpoint(typeof(Interface.IDropBox), new BasicHttpBinding(), adres);
            host.Open();

            while (host.State == CommunicationState.Opened)
                Thread.Sleep(100);
        }

        private static void DoWork()
        {
            System.Threading.Thread.Sleep(50000);
        }

        public static void Close()
        {
            host.Close();
        }

        public static Thread GetThread()
        {
            return threads.First(t => t.ThreadState == ThreadState.Unstarted);
        }
    }
}
