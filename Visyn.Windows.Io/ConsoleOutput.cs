using System;
using System.Collections.Generic;
using System.Diagnostics;
using Visyn.Io;

namespace Visyn.Windows.Io
{
    public class ConsoleOutput : TraceListener, IOutputDeviceMultiline
    {
        public override void WriteLine(string line) => Console.WriteLine(line);
        public override void Write(string text) => Console.Write(text);

        #region Implementation of IOutputDevice

        public void Write(Func<string> func) => Console.Write(func.Invoke());

        #endregion

        public static BackgroundOutputDevice CreateBackgroundWriter()
        {
            return new BackgroundOutputDevice(new ConsoleOutput(), (s) => s, null);
        }

        #region Implementation of IOutputDeviceMultiline

        public void Write(IEnumerable<string> lines)
        {
            WriteLine(string.Join(Environment.NewLine, lines));
        }

        #endregion
    }
}
