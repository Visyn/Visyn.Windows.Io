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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Visyn.Exceptions;
using Visyn.Io;
using Visyn.Log;
using Visyn.Windows.Io.Assemblies;

namespace Visyn.Windows.Io.Log
{
  
    public class FileLogger : IOutputDeviceMultiline, IExceptionHandler, IOutputDevice<SeverityLevel>
    {
        private string Filename { get; }

        public FileLogger(bool deleteIfPresent) 
            : this(Path.Combine(Directories.ProgramDataDirectory,"Log.txt"),deleteIfPresent)
        { }

        public FileLogger(string filename, bool deleteIfPresent=false)
        {
            Filename = filename;
            if(deleteIfPresent && File.Exists(Filename))
                File.Delete(Filename);
        }

        #region Implementation of IOutputDevice

        public void Write(string text) =>  Write(text,SeverityLevel.Informational);

        public void WriteLine(string line) => WriteLine(line, SeverityLevel.Informational);

        public void Write(Func<string> func) => WriteLine(func() , SeverityLevel.Informational);

        public void Write(IEnumerable<string> lines) =>  WriteLines(lines, SeverityLevel.Informational);

        public void WriteLines(IEnumerable<string> lines, SeverityLevel severity)
        {
            try
            {
                lock (this)
                {
                    File.AppendAllLines(Filename, lines.Select(line => lineText(line, severity)));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        #endregion

        #region Implementation of IExceptionHandler

        public bool HandleException(object sender, Exception exception)
        {
            WriteLine($"{sender?.GetType().Name}: {exception.Message}", SeverityLevel.Critical);
            return true;
        }


        #endregion

        #region Implementation of IOutputDevice<SeverityLevel>

        public void Write(string text, SeverityLevel level) => WriteLines(new[] { text }, level);

        public void WriteLine(string line, SeverityLevel level) => WriteLines(new [] { line }, level);

        #endregion

        private static string lineText(string text , SeverityLevel level) => $"{DateTime.Now}, {level}, {text}";
    }
}
