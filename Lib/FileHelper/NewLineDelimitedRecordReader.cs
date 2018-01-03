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
using Visyn.Serialize;
using Visyn.Windows.Io.FileHelper.Core;

namespace Visyn.Windows.Io.FileHelper
{
    /// <summary>
    /// Read a record that is delimited by a newline
    /// </summary>
    public sealed class TextReaderWrapper : IRecordReader, IDisposable
    {
        private readonly TextReader _reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextReaderWrapper"/> class.
        /// </summary>
        /// <param name="reader">The text reader to use.</param>
        public TextReaderWrapper(TextReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        /// Read a record from the data source
        /// </summary>
        /// <returns>A single record for parsing</returns>
        public string ReadRecordString() => _reader.ReadLine();

        /// <summary>
        /// Close the reader
        /// </summary>
        public void Close()
        {
            _reader.Close();
        }

        #region IDisposable

        public void Dispose()
        {
            _reader?.Dispose();
        }

        #endregion
    }
}
