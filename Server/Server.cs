using System;
using System.Collections.Generic;
using System.Threading;
using System.ServiceModel;
using System.Linq;

namespace PW_DropBox
{
    public static class Server
    { 
        public delegate void ServerClientListEventsHandler(List<ClientAccount> clientAccounts);
        public static event ServerClientListEventsHandler ClientListChanged;
        public static event ServerClientListEventsHandler ClientLoggedIn;
        public static event ServerClientListEventsHandler ClientLoggedOut;

        public delegate void ServerTaslListEventsHandler(List<ServerTask> serverTasks);
        public static event ServerTaslListEventsHandler TasksListChanged;

        public delegate void ServerTaskEventsHandler(ServerTask serverTask);
        public static event ServerTaskEventsHandler TasksStatusChanged;

        private static readonly Thread loadBalancerThread = new Thread(new ThreadStart(LoadBalancer.Run));

        public static readonly List<ClientAccount> loggedClients = new List<ClientAccount>();
        private static readonly object clientsLock = new object();
        public static List<ClientAccount> LoggedClients
        {
            get
            {
                lock (clientsLock)
                {
                    return loggedClients;
                }
            }
        }

        private static readonly List<ServerTask> tasksBuffer = new List<ServerTask>();
        private static readonly object tasksLock = new object();
        public static List<ServerTask> Tasks
        {
            get
            {
                lock(tasksLock)
                {
                    return tasksBuffer;
                }
            }
        }


        public static int AssignedTasksCount
        {
            get
            {
                return Tasks.Where(t => t.Status != ServerTask.TaskStatus.Waiting).Count();
            }
        }

        public static int RunningTasksCount
        {
            get
            {
                return Tasks.Where(t => t.Status == ServerTask.TaskStatus.Running).Count();
            }
        }


        private static ServiceHost host;
        public static CommunicationState State { get { return host.State; } }

        public static void Open()
        {
            Uri adres = new Uri("http://localhost:2222/Server");
            host = new ServiceHost(typeof(HttpRequestHandler), adres);
            host.AddServiceEndpoint(typeof(Library.IDropBox), new BasicHttpBinding(), adres);
            host.Open();

            loadBalancerThread.Name = "LoadBalancer";
            loadBalancerThread.Start();

            while (host.State == CommunicationState.Opened)
                Thread.Sleep(100);    
        }

        public static void Close()
        {
            host.Close();
        }


        public static void LogClientIn(ClientAccount clientAccount)
        {
            LoggedClients.Add(clientAccount);
            CalculateMaxTasksPerClient();
            ClientLoggedIn?.Invoke(LoggedClients);
        }

        public static void LogClientOut(ClientAccount clientAccount)
        {
            LoggedClients.Remove(clientAccount);
            CalculateMaxTasksPerClient();
            ClientLoggedOut?.Invoke(LoggedClients);
        }


        public static void AddTask(ServerTask task)
        {
            task.PriorityChanged += OnTaskPriorityChanged;
            task.StatusChanged += OnTaskStatusChanged;

            Tasks.Add(task);
            TasksListChanged?.Invoke(Tasks);
        }

        public static void RemoveTask(ServerTask task)
        {
            Tasks.Remove(task);
            TasksListChanged?.Invoke(Tasks);
        }


        public static void CalculateMaxTasksPerClient()
        {
            var premiumClientFactor = 1.5;

            var clientsStandardCount = LoggedClients.Where(c => !c.IsPremium).Count();
            var clientsPremiumCount = LoggedClients.Where(c => c.IsPremium).Count();

            foreach (var client in LoggedClients)
            {
                if (client.IsPremium)
                {
                    client.MaxTasksCount = (int)(Configuration.MaxThreads / (clientsStandardCount / premiumClientFactor + clientsPremiumCount));
                }
                else
                {
                    client.MaxTasksCount = (int)(Configuration.MaxThreads / (clientsStandardCount + clientsPremiumCount * premiumClientFactor));
                }
            }
        }

        private static void OnTaskPriorityChanged(ServerTask serverTask)
        {
            TasksListChanged?.Invoke(Tasks);
        }

        private static void OnTaskStatusChanged(ServerTask serverTask)
        {
            if (serverTask.Status == ServerTask.TaskStatus.Finished)
                RemoveTask(serverTask);

            var taskCopy = (ServerTask)serverTask.FlatCopy();

            TasksListChanged?.Invoke(Tasks);
            TasksStatusChanged?.Invoke(taskCopy);
            ClientListChanged?.Invoke(LoggedClients);
        }
    }
}
