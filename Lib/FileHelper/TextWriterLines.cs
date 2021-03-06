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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Visyn.Windows.Io.FileHelper
{

    public class TextWriterMerger : IEnumerable<TextWriterLines>, IDisposable
    {
        public int LineCount => Columns.Select(col => col.Count).Concat(new[] {0}).Max();

        public int Count => Columns.Count;
        protected List<TextWriterLines> Columns { get; }

        public TextWriterLines this[int index] => ((IReadOnlyList<TextWriterLines>)Columns)[index];

        public TextWriterMerger(int count)
        {
            Columns = new List<TextWriterLines>(count);//Enumeration.CountTo(count, (i) => new TextWriterLines()));
            while (count-- > 0)
            {
                Columns.Add(new TextWriterLines());
            }
        }

        public List<string> GetLines(string delimiter=null)
        {
            if (delimiter == null) delimiter = string.Empty;
            var lineCount = LineCount;

            var lines = new List<string>(lineCount);
           
            for(var y=0;y<lineCount;y++)
            {
                lines.Add(string.Join(delimiter, Columns.Select(col => col[y])));
            }
            return lines;
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            foreach(var col in Columns)
                col.Dispose();
            Columns.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Overrides of Object

        public IEnumerator<TextWriterLines> GetEnumerator() => Columns.GetEnumerator();

        public override string ToString() => string.Join(Environment.NewLine, GetLines());
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Columns).GetEnumerator();

        #endregion
    }

    /// <summary>
    /// TextWriterLines - Stores text to a list
    /// </summary>
    public class TextWriterLines : TextWriter, IReadOnlyList<string>
    {
        public int Count => Lines.Count;
        public bool IsReadOnly => true;

        protected List<string> Lines { get; } = new List<string>();
        private StringBuilder partialLine = new StringBuilder(1024);
        #region Overrides of TextWriter

        public override Encoding Encoding { get; } = new ASCIIEncoding();
        public string DefaultText { get; set; } = string.Empty;

        public override void Write(string valuex)
        {
            partialLine.Append(valuex);

            if (!valuex.Contains(Environment.NewLine)) return;

            var split = partialLine.ToString().Split(new [] { Environment.NewLine }, StringSplitOptions.None);
            partialLine.Clear();
            for(var i=0;i<split.Length-1;i++)
            {
                Lines.Add(split[i]);
            }
            var last = split.Last();
            if (!string.IsNullOrEmpty(last))
            {
                partialLine.Append(last);
            }
            else { }    // ends with a linefeed: drop 
        }

        public override void WriteLine(string value)
        {
            if (partialLine != null)
            {
                partialLine.Append(value);
                value = partialLine.ToString();
                partialLine = null;
            }
            if (value.Contains(Environment.NewLine))
            {
                Lines.AddRange(value.Split(new[] {Environment.NewLine}, StringSplitOptions.None));
            }
            else
            {
                Lines.Add(value);
            }
        }

        public override void Write(char[] chars)
        {
            throw new NotImplementedException(" Write(char[] chars) not implemented yet...");
        }

        public override void Write(char[] chars, int index, int length)
        {
            throw new NotImplementedException(" Write(char[] chars) not implemented yet...");
        }

        #endregion

        public IEnumerator<string> GetEnumerator() => Lines.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Lines).GetEnumerator();

        #region Implementation of IReadOnlyList<out string>

        public string this[int index] => index < Lines.Count ? Lines[index] : DefaultText;

        #endregion

        #region Partial Implementation of ICollection<string>
        public void Clear()
        {
            partialLine.Clear();
            Lines.Clear();
        }

        public bool Contains(string item) => Lines.Contains(item);

        public void CopyTo(string[] array, int arrayIndex)
        {
            Lines.CopyTo(array, arrayIndex);
  
        }

        #endregion

        public override string ToString() => string.Join(Environment.NewLine, Lines);

        #region Overrides of TextWriter

        public override void Flush()
        {
            Clear();
            base.Flush();
        }

        #endregion
    }
}
