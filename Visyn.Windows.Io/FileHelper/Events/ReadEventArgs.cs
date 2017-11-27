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

using System.ComponentModel;

namespace Visyn.Windows.Io.FileHelper.Events
{
    /// <summary>
    /// Base class of 
    /// <see cref="BeforeReadEventArgs"/>
    /// and 
    /// <see cref="AfterReadEventArgs"/>
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class ReadEventArgs
        : FileHelpersEventArgs
    {
        /// <summary>
        /// Create a read event argument, contains line number and record read
        /// </summary>
        /// <param name="engine">Engine used to parse data</param>
        /// <param name="line">record to be analysed</param>
        /// <param name="lineNumber">record count read</param>
        internal ReadEventArgs(IFileHelperEngine engine, string line, int lineNumber)
            : base(engine, lineNumber)
        {
            RecordLineChanged = false;
            mRecordLine = line;
        }

        string mRecordLine;

        /// <summary>The record line just read.</summary>
        public string RecordLine
        {
            get { return mRecordLine; }
            set
            {
                if (mRecordLine == value)
                    return;

                mRecordLine = value;
                RecordLineChanged = true;
            }
        }

        /// <summary>Whether the RecordLine property has been written-to.</summary>
        public bool RecordLineChanged { get; protected set; }

        /// <summary>Set this property to true if you want to bypass the current record.</summary>
        public bool SkipThisRecord { get; set; }
    }
}
