using System.IO;
using System.Text.Json;

namespace PW_DropBox
{    
    public static class Configuration
    {
        private const string configFileName = @"\config.txt";

        public static void Load(string directory)
        {
            var jsonString = File.ReadAllText(directory + configFileName);
            var configurationFile = JsonSerializer.Deserialize<ConfigurationFile>(jsonString);
            UserName = configurationFile.UserName;
            LocalFolderDirectory = configurationFile.LocalFolderDirectory + @"\" + UserName;
        }

        public static string UserName { get; set; }
        public static string LocalFolderDirectory { get; set; }

        private class ConfigurationFile
        {
            public string UserName { get; set; }
            public string LocalFolderDirectory { get; set; }
        }
    }
}
