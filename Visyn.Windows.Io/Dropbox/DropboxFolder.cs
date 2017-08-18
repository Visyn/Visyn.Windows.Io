using System;
using System.IO;

namespace Visyn.Windows.Io.Dropbox
{
    public static class DropboxFolder
    {
        public static string Directory ()
        {
            var infoPath = @"Dropbox\info.json";

            var jsonPath = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), infoPath);

            if (!File.Exists(jsonPath)) jsonPath = Path.Combine(Environment.GetEnvironmentVariable("AppData"), infoPath);

            if (!File.Exists(jsonPath)) throw new DirectoryNotFoundException("Dropbox directory could not be found!");

            var dropboxPath = File.ReadAllText(jsonPath).Split('\"')[5].Replace(@"\\", @"\");
          
            return dropboxPath; 
        }

        public static string Directory(string relativeDirectory)
        {
            var dropboxPath = Directory();

            if (System.IO.Directory.Exists(dropboxPath))
            {
                var dropboxDirectory = Path.Combine(dropboxPath, relativeDirectory);
                if (System.IO.Directory.Exists(dropboxDirectory))
                {
                    return dropboxDirectory;
                }
            }
            return dropboxPath;
        }

        public static DirectoryInfo DirectoryInfo(string relativePath)
        {
            var directory = Directory(relativePath);
           
            return System.IO.Directory.Exists(directory) ? new  DirectoryInfo(directory) : null;
        }
    }
}
