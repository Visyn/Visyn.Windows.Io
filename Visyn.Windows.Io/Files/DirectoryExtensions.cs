
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Visyn.Windows.Io.Files
{
    public static class DirectoryExtensions
    {
        public static IEnumerable<FileInfo> FindFiles(string directory, string fileFilter = "*.txt")
        {
            return Directory.Exists(directory) ? new DirectoryInfo(directory).FindFiles(fileFilter) : null;
        }

        public static IEnumerable<FileInfo> FindFiles(this DirectoryInfo directory, string fileFilter = "*.txt")
        {
            if (directory == null) yield break;

            var files = Directory.GetFiles(directory.FullName, fileFilter);
            foreach (var fi in directory.EnumerateFiles())
            {
                if (files.Contains(fi.FullName))
                    yield return fi;
            }
        }
    }
}