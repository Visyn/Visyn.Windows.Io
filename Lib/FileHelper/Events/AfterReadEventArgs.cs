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

namespace Visyn.Windows.Io.FileHelper.Events
{
    /// <summary>Arguments for the <see cref="AfterReadHandler{T}"/></summary>
    public class AfterReadEventArgs
        : ReadEventArgs
        
    {
        /// <summary>
        /// After the record is read,  allow details to be inspected.
        /// </summary>
        /// <param name="engine">Engine that parsed the record</param>
        /// <param name="line">Record that was analysed</param>
        /// <param name="lineChanged">Was it changed before</param>
        /// <param name="lineNumber">Record number read</param>
        public AfterReadEventArgs(IFileHelperEngine engine,
            string line,
            bool lineChanged,
            int lineNumber)
            : base(engine, line, lineNumber)
        {
            SkipThisRecord = false;
            RecordLineChanged = lineChanged;
        }

    }

    /// <summary>Arguments for the <see cref="AfterReadHandler{T}"/></summary>
    public sealed class AfterReadEventArgs<T>
        : AfterReadEventArgs
        where T : class
    {
        /// <summary>
        /// After the record is read,  allow details to be inspected.
        /// </summary>
        /// <param name="engine">Engine that parsed the record</param>
        /// <param name="line">Record that was analysed</param>
        /// <param name="lineChanged">Was it changed before</param>
        /// <param name="newRecord">Object created</param>
        /// <param name="lineNumber">Record number read</param>
        public AfterReadEventArgs(IFileHelperEngine engine,
            string line,
            bool lineChanged,
            T newRecord,
            int lineNumber)
            : base(engine, line, lineChanged, lineNumber)
        {
            Record = newRecord;
        }

        /// <summary>The current record.</summary>
        public T Record { get; set; }
    }
}
