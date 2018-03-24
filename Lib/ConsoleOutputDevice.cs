using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Visyn.Collection;
using Visyn.Io;
using Visyn.Log;

namespace Visyn.Util.IO
{
    public class ConsoleOutputDevice : IOutputDevice
    {
        #region Implementation of IOutputDevice

        public void AppendText(string text)
        {
            Console.Write(text);
        }

        public void AppendLine(string line)
        {
            Console.WriteLine(line);
        }

        public void AppendLines(IEnumerable<string> lines)
        {
            foreach(var line in lines)
            {
                AppendLine(line);
            }
        }

        public void AppendDictionary(IDictionary dict, string format=DictionaryExtensions.KEY_VALUE_FORMAT_STRING)
        {
            AppendLines(dict.FormattedText(format));
        }

        #endregion

        public void Write(string text)
        {
            Console.Write(text);
        }

        void IOutputDevice.WriteLine(string line)
        {
            WriteLine(line);
        }

        public void Write(Func<string> func)
        {
            if (func != null)
            {
                Console.Write(func.Invoke());
            }
        }

        public static void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public static void WriteLine(string text, ConsoleColor foreColor)
        {
            var currentForeground = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = foreColor;
                Console.WriteLine(text);
            }
            finally
            {
                Console.ForegroundColor = currentForeground;
            }
        }

        public static void WriteLine(string text, ConsoleColor foreColor, ConsoleColor backColor)
        {
            var currentForeground = Console.ForegroundColor;
            var currentBackground = Console.BackgroundColor;
            try
            {
                Console.ForegroundColor = foreColor;
                Console.BackgroundColor = backColor;
                Console.WriteLine(text);
            }
            finally
            {
                Console.ForegroundColor = currentForeground;
                Console.BackgroundColor = currentBackground;
            }
        }

        public static ConsoleColor Color(SeverityLevel level)
        {
            switch (level)
            {
                case SeverityLevel.LogAlways: return ConsoleColor.Red;
                case SeverityLevel.Critical: return ConsoleColor.DarkRed;
                case SeverityLevel.Error: return ConsoleColor.Red;
                case SeverityLevel.Warning: return ConsoleColor.Yellow;
                case SeverityLevel.Informational: return ConsoleColor.White;
                case SeverityLevel.Verbose: return ConsoleColor.Cyan;
                default: return ConsoleColor.White;
            }
        }

        [Obsolete("Use level.GetConsoleColor()", true)]
        public static ConsoleColor Color(EventLevel level) => throw new NotImplementedException("Une another method");

        //public static ConsoleColor Color(EventLevel level) => level.GetConsoleColor();
    }
}