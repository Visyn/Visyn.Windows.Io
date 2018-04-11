using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Visyn.Windows.Io.Files
{
    public class FileFinder
    {
        public string[] FileFilters { get; }
        public Func<DirectoryInfo, bool> DirectoryFilterFunction { get; }

        public Func<FileInfo, bool> FileFiltersFunction { get; }

        public FileFinder(string fileFilter, Func<FileInfo, bool> fileFilterfunction, Func<DirectoryInfo, bool> directoryFilterfunction) 
            : this(new[] { fileFilter }, fileFilterfunction, directoryFilterfunction)
        { }

        public FileFinder(IEnumerable<string> fileFilters ,Func<FileInfo, bool> fileFilterfunction, Func<DirectoryInfo, bool> directoryFilterfunction)
        {
            FileFilters = fileFilters.ToArray();
            FileFiltersFunction = fileFilterfunction;
            DirectoryFilterFunction = directoryFilterfunction;
        }

        public IEnumerable<FileInfo> FindFiles(DirectoryInfo directory, int recurseDepth=0)
        {
            if (directory == null) yield break;

            foreach (var filter in FileFilters)
            {
                foreach (var fi in directory.EnumerateFiles(filter))
                {
                    if (fi == null) continue;
                    if (FileFiltersFunction == null || FileFiltersFunction.Invoke(fi))
                        yield return fi;
                }
            }

            if (recurseDepth <= 0) yield break;

            foreach (var dir in directory.GetSubdirectories(DirectoryFilterFunction))
            {
                foreach (var file in FindFiles(dir, recurseDepth - 1))
                    yield return file;
            }
        }
    }
}