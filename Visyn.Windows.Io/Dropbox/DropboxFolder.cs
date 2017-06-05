using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visyn.Windows.Io.Dropbox
{
    public static class DropboxFolder
    {
        public static string Directory ()
        {
            var infoPath = @"Dropbox\info.json";

            var jsonPath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), infoPath);

            if (!System.IO.File.Exists(jsonPath)) jsonPath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("AppData"), infoPath);

            if (!System.IO.File.Exists(jsonPath)) throw new System.IO.DirectoryNotFoundException("Dropbox directory could not be found!");

            var dropboxPath = System.IO.File.ReadAllText(jsonPath).Split('\"')[5].Replace(@"\\", @"\");
          
            return dropboxPath; 
        }

        public static string Directory(string relativeDirectory)
        {
            var dropboxPath = DropboxFolder.Directory();

            if (System.IO.Directory.Exists(dropboxPath))
            {
                var dropboxDirectory = System.IO.Path.Combine(dropboxPath, relativeDirectory);
                if (System.IO.Directory.Exists(dropboxDirectory))
                {
                    return dropboxDirectory;
                }
            }
            return dropboxPath;
        }

        public static System.IO.DirectoryInfo DirectoryInfo(string relativePath)
        {
            var directory = Directory(relativePath);
           
            return System.IO.Directory.Exists(directory) ? new  System.IO.DirectoryInfo(directory) : null;
        }
    }
}
