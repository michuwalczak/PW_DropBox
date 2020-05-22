using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PW_DropBox
{    
    public static class Configuration
    {
        private const string configFileName = @"\config.txt";

        private static string fileDirectory;

        public static void Reload()
        {
            Load(fileDirectory);
        }

        private static void Load(string directory)
        {
            fileDirectory = directory;
            var jsonString = File.ReadAllText(directory + configFileName);
            var configurationFile = JsonSerializer.Deserialize<ConfigurationFile>(jsonString);
            MaxThreads = configurationFile.MaxThreads;
            MaxRunningThreads = configurationFile.MaxRunningThreads;
            PremiumClients = configurationFile.PremiumClients;
        }

        public static int MaxThreads { get; set; }
        public static int MaxRunningThreads { get; set; }
        public static string ServerDirectory { get { return fileDirectory; } set { Load(value); } }
        public static List<string> PremiumClients { get; private set; }

        private class ConfigurationFile
        {
            public int MaxThreads { get; set; }
            public int MaxRunningThreads { get; set; }
            public List<string> PremiumClients { get; set; }
        }
    }
}
