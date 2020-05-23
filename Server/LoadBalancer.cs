using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PW_DropBox
{
    static class LoadBalancer
    {
        public static int sleepTime = 100;

        public static void Run()
        {
            while(Server.State != System.ServiceModel.CommunicationState.Closed)
            {
                Balance();
                Thread.Sleep(sleepTime);
            }
        }


        private static void Balance()
        {
            // Asess priority for each task in buffer
            ChangeTasksPriorities(Server.Tasks);

            // Create sorted lists of tasks
            var tasks = Server.Tasks.OrderByDescending(t => t.Priority).ToList();
            var busyTasks = tasks.Where(t => t.Status != ServerTask.TaskStatus.Waiting).ToList();

            // Assign threads to tasks
            var busyThreadsCount = busyTasks.Count();
            AssignFreeThreads(tasks, busyThreadsCount);

            busyTasks = tasks.Where(t => t.Status != ServerTask.TaskStatus.Waiting).ToList();


            // Get tasks with low priority
            var lowPriorityTasks = GetLowPriorityTasks(busyTasks);

            // Get tasks with high priority
            var highPriorityTasks = GetHighPriorityTasks(busyTasks);

            // Suspend tasks with low priority
            if(lowPriorityTasks.Count > 0)
                SuspendTasks(lowPriorityTasks, highPriorityTasks);

            // Start waiting tasks
            StartTasks(highPriorityTasks);
    }

        private static int AsessTaskPriority(ServerTask serverTask, int taskIndex)
        {
            // Initial priority rate
            int priority = 100;

            // Asessment by:
            // - account type
            if (serverTask.Account.IsPremium) priority += 100;

            // - performed operations
            priority -= serverTask.Account.PerformedOperations * 5;

            // - transfer size
            priority -= serverTask.Duration * 2;

            // - transfer total size
            priority -= (serverTask.Account.TransferTotalSize / Server.LoggedClients.Count )* 5;

            // - transfer progress
            priority += serverTask.Progress * 1;

            // - arrival order
            priority -= taskIndex * 1;

            return priority;
        }

        private static void ChangeTasksPriorities(List<ServerTask> tasks)
        {
            for (int i = 0; i < tasks.Count; i++)
                tasks[i].ChangePriority(AsessTaskPriority(tasks[i], i));
        }

        private static void AssignFreeThreads(List<ServerTask> tasks, int busyThreadsCount)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Status == ServerTask.TaskStatus.Waiting)
                {
                    if(busyThreadsCount < Configuration.MaxThreads)
                    {
                        if (tasks[i].Account.AssignedTasksCount < tasks[i].Account.MaxTasksCount)
                            tasks[i].AssignServerThread();
                    }
                }
            }
        }

        private static List<ServerTask> GetLowPriorityTasks(List<ServerTask> tasks)
        {
            List<ServerTask> lowPriorityTasks = new List<ServerTask>();
            for (int i = Configuration.MaxRunningThreads; i < tasks.Count; i++)
            {
                if (tasks[i].Status == ServerTask.TaskStatus.Running)
                    lowPriorityTasks.Add(tasks[i]);
            }

            return lowPriorityTasks;
        }

        private static List<ServerTask> GetHighPriorityTasks(List<ServerTask> tasks)
        {
            List<ServerTask> highPriorityTasks = new List<ServerTask>();
            var threadLimit = Configuration.MaxRunningThreads > tasks.Count ? tasks.Count : Configuration.MaxRunningThreads;
            for (int i = 0; i < threadLimit; i++)
            {
                if (tasks[i].Status != ServerTask.TaskStatus.Running)
                    highPriorityTasks.Add(tasks[i]);
            }

            return highPriorityTasks;
        }
    
        private static void StartTasks(List<ServerTask> tasks)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Status == ServerTask.TaskStatus.NotStarted)
                    tasks[i].Start();
            }
        }

        private static void SuspendTasks(List<ServerTask> lowPriorityTasks, List<ServerTask> highPriorityTasks)
        {
            for (int i = 0; i < highPriorityTasks.Count; i++)
                if (highPriorityTasks[i].Status != ServerTask.TaskStatus.Suspended)
                    highPriorityTasks[i].SuspendLowerPriorityTask(lowPriorityTasks[i]);
        }
    }
}