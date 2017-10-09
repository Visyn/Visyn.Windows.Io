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