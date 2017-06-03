using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visyn.Windows.Io.Dropbox
{
    class DropboxFolder
    {
        public string Directory ()
        {
            var infoPath = @"Dropbox\info.json";

            var jsonPath = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), infoPath);

            if (!File.Exists(jsonPath)) jsonPath = Path.Combine(Environment.GetEnvironmentVariable("AppData"), infoPath);

            if (!File.Exists(jsonPath)) throw new Exception("Dropbox could not be found!");

            var dropboxPath = File.ReadAllText(jsonPath).Split('\"')[5].Replace(@"\\", @"\");
            return dropboxPath;
        }
    }
}
