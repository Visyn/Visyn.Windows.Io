#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

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
