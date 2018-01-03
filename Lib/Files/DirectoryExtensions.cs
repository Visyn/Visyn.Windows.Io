#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
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
            if (fileFilter == null) throw new NullReferenceException($"{nameof(FindFiles)}({nameof(fileFilter)}) can not be null!");
            if (directory == null) yield break;

            var files = Directory.GetFiles(directory.FullName, fileFilter);
            foreach (var fi in directory.EnumerateFiles())
            {
                if (fi == null) continue;
                if (files.Contains(fi.FullName))
                    yield return fi;
            }
        }
    }
}
