using System;
using System.Diagnostics;
using Visyn.Core.Io;
using Visyn.Public.Io;

namespace Visyn.Windows.Io
{
    public class ConsoleOutput : TraceListener, IOutputDevice
    {
        public override void WriteLine(string line) => System.Console.WriteLine(line);
        public override void Write(string text) => System.Console.Write(text);

        #region Implementation of IOutputDevice

        public void Write(Func<string> func) => System.Console.Write(func.Invoke());

        #endregion
    }
}
