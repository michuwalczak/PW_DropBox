using System.IO;

namespace PW_DropBox
{
    class FileWatcher : FileSystemWatcher
    {
        public FileWatcher(string path)
        {
            this.Path = path;
            this.Filter = "*.*";
            this.EnableRaisingEvents = true;
        }
    }
}
