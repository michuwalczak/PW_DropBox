using System.ServiceModel;

namespace Library
{
    [ServiceContract]
    public interface IDropBox
    {
        [OperationContract]
        void DownloadFile(Cookie cookie, string fileName);

        [OperationContract]
        string[] FileList(Cookie cookie);

        [OperationContract]
        void UploadFile(Cookie cookie, string fileName);

        [OperationContract]
        Cookie LogIn(string clientName, string folderPath);

        [OperationContract]
        void LogOut(Cookie cookie);
    }
}
