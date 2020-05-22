namespace PW_DropBox
{
    public class ClientAccount
    {
        public string Name { get; set; }
        public bool IsPremium { get; set; }
        public int PerformedOperations { get; set; }
        public int TransferTotalSize { get; set; }
        public int MaxTasksCount { get; set; }
        public int AssignedTasksCount { get; set; }
        public int RunningTasksCount { get; set; }

        public ClientAccount()
        {
            this.PerformedOperations = 0;
            this.TransferTotalSize = 0;
            this.MaxTasksCount = 0;
            this.AssignedTasksCount = 0;
            this.RunningTasksCount = 0;
        }
    }
}
