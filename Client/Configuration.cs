using System.IO;
using System.Text.Json;

namespace PW_DropBox
{    
    public static class Configuration
    {
        private const string configFileName = @"\config.txt";

        private static string fileDirectory;

        private static void Load(string directory)
        {
            fileDirectory = directory;
            var jsonString = File.ReadAllText(directory + configFileName);
            var configurationFile = JsonSerializer.Deserialize<ConfigurationFile>(jsonString);
            MaxThreads = configurationFile.MaxThreads;
            UserName = configurationFile.UserName;
        }

        public static int MaxThreads { get; set; }
        public static string UserName { get; set; }
        public static string FileDirectory { get { return fileDirectory; } set { Load(value); } }

        private class ConfigurationFile
        {
            public int MaxThreads { get; set; }
            public string UserName { get; set; }
        }
    }
}
