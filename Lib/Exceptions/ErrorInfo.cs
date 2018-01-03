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

using System;
using System.Diagnostics;
using Visyn.Serialize;
using Visyn.Serialize.Converters;
using Visyn.Windows.Io.FileHelper;
using Visyn.Windows.Io.FileHelper.Attributes;
using Visyn.Windows.Io.FileHelper.Enums;

namespace Visyn.Windows.Io.Exceptions
{
    /// <summary>
    /// Contains error information of the <see cref="VisynFileHelperEngine"/> class.
    /// </summary>
    [DelimitedRecord("|")]
    [IgnoreFirst(2)]
    [DebuggerDisplay("Line: {LineNumber}. Error: {ExceptionInfo.Message}.")]
    public sealed class ErrorInfo
    {
        /// <summary>The line number of the error</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int LineNumber { get; set; }

        /// <summary>The string of the record of the error.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string Text { get; set; } = string.Empty;

        /// <summary>The exception that indicates the error.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldConverter(typeof (ExceptionConverter))]
        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public Exception ExceptionInfo { get; set; }
    }
}
