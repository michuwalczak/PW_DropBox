using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PW_DropBox
{
    public partial class Form1 : Form
    {
        private static readonly object tasksListViewLock = new object();

        public Form1()
        {
            Server.ClientListChanged += OnLoggedClientsListChanged;
            Server.ClientLoggedIn += OnLoggedClientsListChanged;
            Server.ClientLoggedOut += OnLoggedClientsListChanged;

            Server.TasksListChanged += OnTasksListChanged;
            Server.TasksStatusChanged += OnTaskStatusChanged;

            InitializeComponent();

            AdjustListViewColumnsWidth(this.lvwThreadsDetails);
            AdjustListViewColumnsWidth(this.lvwLoggedClients);

            UpdateAssignedTasksTextBox(0);
            UpdateRunningTasksTextBox(0);
        }

        private void AdjustListViewColumnsWidth(ListView listView)
        {
            for (int i = 0; i < listView.Columns.Count; i++)
                listView.Columns[i].Width = -2;
        }


        private ListViewItem CreateTaskItem(ServerTask task)
        {
            var item = new ListViewItem(task.Id.ToString());
            item.Tag = task.Id;
            ListViewItem.ListViewSubItem[] subItems = new ListViewItem.ListViewSubItem[]
            {
                new ListViewItem.ListViewSubItem(item, task.Account.Name),
                new ListViewItem.ListViewSubItem(item, task.Type.ToString()),
                new ListViewItem.ListViewSubItem(item, task.FileName),
                new ListViewItem.ListViewSubItem(item, task.Status.ToString()),
                new ListViewItem.ListViewSubItem(item, task.Progress.ToString() + "%"),
                new ListViewItem.ListViewSubItem(item, task.Priority.ToString()),
            };
            item.SubItems.AddRange(subItems);
            if (item.SubItems[4].Text == ServerTask.TaskStatus.Running.ToString()) item.BackColor = Color.LightGreen;
            return item;
        }

        private void UpdateTaskItem(ListViewItem item, ServerTask task)
        {
            item.SubItems[1].Text = task.Account.Name;
            item.SubItems[2].Text = task.Type.ToString();
            item.SubItems[3].Text = task.FileName;
            item.SubItems[4].Text = task.Status.ToString();
            item.SubItems[5].Text = task.Progress.ToString() + "%";
            item.SubItems[6].Text = task.Priority.ToString();
            if (item.SubItems[4].Text == ServerTask.TaskStatus.Running.ToString())
            {
                item.BackColor = Color.LightGreen;
            }
            else
            {
                item.BackColor = Color.White;
            }
        }

        private void AddNewTaskItems(List<ServerTask> tasks)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                if (this.lvwThreadsDetails.Items.Count > 0)
                {
                    bool found = false;
                    for (int j = 0; j < this.lvwThreadsDetails.Items.Count; j++)
                    {
                        if (this.lvwThreadsDetails.Items[j].Text == task.Id.ToString())
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found) this.lvwThreadsDetails.Items.Add(CreateTaskItem(task));
                }
                else
                {
                    this.lvwThreadsDetails.Items.Add(CreateTaskItem(task));
                }
            }
        }

        private void ModifyTaskItems(List<ServerTask> tasks)
        {
            for(int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                for (int j = 0; j < this.lvwThreadsDetails.Items.Count; j++)
                {
                    if (this.lvwThreadsDetails.Items[j].Text == task.Id.ToString())
                        UpdateTaskItem(this.lvwThreadsDetails.Items[j], task);
                }
            }
        }

        private void DeleteTaskItems(List<ServerTask> tasks)
        {
            foreach (ListViewItem item in this.lvwThreadsDetails.Items)
            {
                if (!tasks.Select(t => t.Id).ToList().Contains(Convert.ToInt32(item.Text)))
                    this.lvwThreadsDetails.Items.Remove(item);
            }
        }

        private void UpdateTaskListView(List<ServerTask> serverTasks)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<List<ServerTask>>(UpdateTaskListView), new object[] { serverTasks });
                return;
            }

            //var runningTasks = serverTasks.Where(t => t.Status == ServerTask.TaskStatus.Running).ToList().OrderByDescending(t => t.Priority).ToList();
            //var notRunningTasks = serverTasks.Where(t => t.Status != ServerTask.TaskStatus.Running).ToList().OrderByDescending(t => t.Priority).ToList();

            //var tasks = runningTasks.Concat(notRunningTasks).ToList();

            AddNewTaskItems(serverTasks);
            ModifyTaskItems(serverTasks);
            DeleteTaskItems(serverTasks);
        }

        private void UpdateClientListView(List<ClientAccount> clientAccounts)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<List<ClientAccount>>(UpdateClientListView), new object[] { clientAccounts });
                return;
            }

            this.lvwLoggedClients.Items.Clear();
            foreach (var account in clientAccounts)
            {
                var item = new ListViewItem(account.Name);
                if (account.IsPremium) item.BackColor = Color.LightGreen;
                ListViewItem.ListViewSubItem[] subItems = new ListViewItem.ListViewSubItem[]
                {
                    new ListViewItem.ListViewSubItem(item, account.IsPremium.ToString()),
                    new ListViewItem.ListViewSubItem(item, account.AssignedTasksCount.ToString() + "/" + account.MaxTasksCount.ToString()),
                    new ListViewItem.ListViewSubItem(item, account.RunningTasksCount.ToString()),
                    new ListViewItem.ListViewSubItem(item, account.PerformedOperations.ToString()),
                    new ListViewItem.ListViewSubItem(item, account.TransferTotalSize.ToString())
                };
                item.SubItems.AddRange(subItems);
                this.lvwLoggedClients.Items.Add(item);
            }
        }

        private void UpdateAssignedTasksTextBox(int assignedTasksCount)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(UpdateAssignedTasksTextBox), new object[] { assignedTasksCount });
                return;
            }

            this.tbxAssignedTasks.Text = assignedTasksCount.ToString() + "/" + Configuration.MaxThreads;
        }

        private void UpdateRunningTasksTextBox(int runningTasksCount)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(UpdateRunningTasksTextBox), new object[] { runningTasksCount });
                return;
            }

            this.tbxRunningTasks.Text = runningTasksCount.ToString() + "/" + Configuration.MaxRunningThreads;
        }

        private void UpdateLog(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateLog), new object[] { message });
                return;
            }
            this.tbxLog.AppendText(message + "\r\n");
        }



        private void OnTasksListChanged(List<ServerTask> serverTasks)
        {
            UpdateTaskListView(serverTasks);
            UpdateAssignedTasksTextBox(Server.AssignedTasksCount);
            UpdateRunningTasksTextBox(Server.RunningTasksCount);
        }

        private void OnLoggedClientsListChanged(List<ClientAccount> clientAccounts)
        {
            UpdateClientListView(clientAccounts);
        }

        private void OnTaskStatusChanged(ServerTask serverTask)
        {
            UpdateLog(string.Format("Task status changed Client:{0} File:{1} Status:{2} Priority:{3} " +
                "Running tasks:{4}/{5} Assigned tasks:{6}/{7}", 
                serverTask.Account.Name, 
                serverTask.FileName, 
                serverTask.Status.ToString(), 
                serverTask.Priority.ToString(),
                Server.RunningTasksCount,
                Configuration.MaxRunningThreads,
                serverTask.Account.AssignedTasksCount.ToString(),
                serverTask.Account.MaxTasksCount.ToString()
                ));
        }

        private void ReloadConfig_OnClick(object sender, EventArgs e)
        {
            Configuration.Reload();
            UpdateLog("Configuration reoloaded");
            Server.CalculateMaxTasksPerClient();
            UpdateClientListView(Server.LoggedClients);
            UpdateTaskListView(Server.Tasks);
            UpdateAssignedTasksTextBox(Server.AssignedTasksCount);
            UpdateRunningTasksTextBox(Server.RunningTasksCount);
        }
    }
}
