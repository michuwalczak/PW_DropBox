using System.Threading;

namespace PW_DropBox
{
    public class ServerTask
    {
        public delegate void ServerTaskHandler(ServerTask serverTask);
        public event ServerTaskHandler ProgressChanged;
        public event ServerTaskHandler StatusChanged;
        public event ServerTaskHandler PriorityChanged;

        private readonly object statusLock = new object();
        public Semaphore semaphore = new Semaphore(1, 1);

        private static int lastId = 0;
        private TaskStatus status;
        private ServerTask SuspendedTask;

        public enum TaskStatus
        {
            Waiting,
            NotStarted,
            Started,
            Running,
            Suspended,
            Finished
        }

        public enum TaskType
        {
            Download,
            Upload
        }

        public ServerTask(TaskType taskType)
        {
            this.Type = taskType;
            this.Id = lastId++;
        }

        public int Id { get; set; }
        public ClientAccount Account { get; set; }
        public string FileName { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public int Priority { get; private set; }
        public int Duration { get; set; }
        public int Progress { get; private set; }
        public TaskStatus Status
        {
            get
            {
                lock(statusLock)
                {
                    return status;
                }
            }
            private set
            {
                lock (statusLock)
                {
                    status = value;
                }
            }
        }
        public TaskType Type { get; private set; }

        private Thread ServerThread { get; set; }


        public void AssignServerThread()
        {
            this.ServerThread = new Thread(new ThreadStart(CopyFile));
            Account.AssignedTasksCount++;
            ChangeStatus(TaskStatus.NotStarted);
        }

        public void Start()
        {
            this.ServerThread.Start();
            ChangeStatus(TaskStatus.Started);
        }

        public void SuspendLowerPriorityTask(ServerTask task)
        {
            SuspendedTask = task;
            SuspendedTask.semaphore.WaitOne();
            SuspendedTask.ChangeStatus(TaskStatus.Suspended);
        }

        private void ResumeLowerPriorityTask()
        {
            SuspendedTask.semaphore.Release();
            SuspendedTask.ChangeStatus(TaskStatus.Running);
        }

        public void ChangePriority(int priority)
        {
            if(this.Priority != priority)
            {
                this.Priority = priority;
                PriorityChanged?.Invoke(this);
            }
        }

        public object FlatCopy()
        {
            return this.MemberwiseClone();
        }

        private void CopyFile()
        {
            ChangeStatus(TaskStatus.Running);
            Progress = 0;
            for (int i = 1; i <= Duration; i++)
            {
                try
                {
                    semaphore.WaitOne();
                    Thread.Sleep(1000);
                }
                finally
                {
                    semaphore.Release();
                }
                ChangeProgress(i * 100 / Duration);
            }
            System.IO.File.Copy(SourcePath, DestinationPath, true);

            Account.AssignedTasksCount--;
            ChangeStatus(TaskStatus.Finished);

            if (SuspendedTask != null)
                ResumeLowerPriorityTask();
        }

        private void ChangeStatus(TaskStatus taskStatus)
        {
            Status = taskStatus;
            if (taskStatus == TaskStatus.Running)
                Account.RunningTasksCount++;

            if (taskStatus == TaskStatus.Suspended)
                Account.RunningTasksCount--;

            if (taskStatus == TaskStatus.Finished)
                Account.RunningTasksCount--;
            
            StatusChanged?.Invoke(this);
        }

        private void ChangeProgress(int value)
        {
            Progress = value;
            ProgressChanged?.Invoke(this);
        }
    }
}
