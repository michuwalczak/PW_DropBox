using System.Threading;

namespace PW_DropBox
{
    public class ServerTask
    {
        public delegate void ServerTaskHandler(ServerTask serverTask);
        public event ServerTaskHandler ProgressChanged;
        public event ServerTaskHandler StatusChanged;
        public event ServerTaskHandler PriorityChanged;

        private bool operationSuspension = false;
        private readonly object operationLock = new object();
        private bool OperationSuspension
        {
            get
            {
                lock(operationLock)
                {
                    return operationSuspension;
                }
            }

            set
            {
                lock (operationLock)
                {
                    operationSuspension = value;
                }
            }
        }
        private static int lastId = 0;


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
        public TaskStatus Status { get; private set; }
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

        public void Resume()
        {
            OperationSuspension = false;
            ChangeStatus(TaskStatus.Running);
        }

        public void Suspend()
        {
            OperationSuspension = true;
            ChangeStatus(TaskStatus.Suspended);
        }

        public void ChangePriority(int priority)
        {
            if(this.Priority != priority)
            {
                this.Priority = priority;
                PriorityChanged?.Invoke(this);
            }
        }


        private void CopyFile()
        {
            ChangeStatus(TaskStatus.Running);
            
            Progress = 0;
            for (int i = 1; i <= Duration; i++)
            {
                StopTask();
                Thread.Sleep(1000);
                ChangeProgress(i * 100 / Duration);
            }

            Account.AssignedTasksCount--;
            ChangeStatus(TaskStatus.Finished);
        }

        private void ChangeStatus(TaskStatus taskStatus)
        {
            Status = taskStatus;
            if(taskStatus == TaskStatus.Running)
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

        private void StopTask()
        {
            while (OperationSuspension) ;
        }
    }
}
