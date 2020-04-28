using System.ServiceModel;

namespace Interface
{
    [ServiceContract]
    public interface IDropBox
    {
        [OperationContract]
        void DownloadFile(string ClientName, string fileName, string targetDirectory);

        [OperationContract]
        string[] FileList(string ClientName);

        [OperationContract]
        void UploadFile(string ClientName, string fileName, string sourceDirectory);
    }
}
