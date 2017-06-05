using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Visyn.Windows.Io.Files
{
    public static class FileExtensions
    {



    }

    public static class DirectoryExtensions
    {
        public static IEnumerable<FileInfo> FindFiles(string directory, string fileFilter = "*.txt")
        {
            return Directory.Exists(directory) ? new DirectoryInfo(directory).FindFiles(fileFilter) : null;
        }

        public static IEnumerable<FileInfo> FindFiles(this DirectoryInfo directory, string fileFilter = "*.txt")
        {
            if (directory == null) yield break;

            var files = System.IO.Directory.GetFiles(directory.FullName, fileFilter);
            foreach (var fi in directory.EnumerateFiles())
            {
                if (files.Contains(fi.FullName))
                    yield return fi;
            }
        }
    }
}
