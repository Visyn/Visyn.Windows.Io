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

    /// <summary>Arguments for the <see cref="BeforeWriteHandler{T}"/></summary>
    public class BeforeWriteEventArgs
        : WriteEventArgs
    {
        /// <summary>
        /// Check record just before processing.
        /// </summary>
        /// <param name="engine">Engine that will parse record</param>
        /// <param name="lineNumber">line number to be parsed</param>
        internal BeforeWriteEventArgs(IFileHelperEngine engine, int lineNumber)
            : base(engine, lineNumber)
        {
            SkipThisRecord = false;
        }

        /// <summary>Set this property as true if you want to bypass the current record.</summary>
        public bool SkipThisRecord { get; set; }
    }

    /// <summary>Arguments for the <see cref="BeforeWriteHandler{T}"/></summary>
    /// <typeparam name="T">Object type we are writing from</typeparam>
    public sealed class BeforeWriteEventArgs<T> : BeforeWriteEventArgs
        where T : class
    {
        /// <summary>
        /// Check record just before processing.
        /// </summary>
        /// <param name="engine">Engine that will parse record</param>
        /// <param name="record">object to be created</param>
        /// <param name="lineNumber">line number to be parsed</param>
        public BeforeWriteEventArgs(IFileHelperEngine engine, T record, int lineNumber)
            : base(engine, lineNumber)
        {
            Record = record;
        }

        /// <summary>The current record.</summary>
        public T Record { get; private set; }

    }
}
