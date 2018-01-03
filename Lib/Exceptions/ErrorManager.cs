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
using System.Diagnostics;
using Visyn.Windows.Io.FileHelper;
using Visyn.Windows.Io.FileHelper.Enums;

namespace Visyn.Windows.Io.Exceptions
{
    /// <summary>
    /// This is the class that handles the errors of the engines process.
    /// </summary>
    /// <remarks>
    /// All the engines and DataStorage utilities contains an ErrorManager.
    /// </remarks>
    [DebuggerDisplay("{ErrorsDescription()}. ErrorMode: {ErrorMode.ToString()}")]
    public sealed class ErrorManager
        : IEnumerable
    {
        private int _errorLimit = 10000;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorManager"/> class.
        /// </summary>
        public ErrorManager() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorManager"/> class.
        /// with the specified <see cref="ErrorMode"/>.
        /// </summary>
        /// <param name="mode">Indicates the error behavior of the class.</param>
        public ErrorManager(ErrorMode mode)
        {
            ErrorMode = mode;
        }

        /// <summary>Maximum number of recorded errors. After this limit is reached, successive errors are ignored.</summary>
        /// <remarks>Default error limit is 10000.</remarks>
        public int ErrorLimit
        {
            get { return _errorLimit; }
            set { _errorLimit = value; }
        }

        private string ErrorsDescription()
        {
            if (ErrorCount == 0) return "No Errors";
            return $"{ErrorCount} Error" + (ErrorCount > 0 ? "s" : string.Empty);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<ErrorInfo> mErrorsArray = new List<ErrorInfo>();

        /// <summary>
        /// Is an array of <see cref="ErrorInfo"/> that contains the
        /// errors of the last operation in this class.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public ErrorInfo[] Errors => mErrorsArray.ToArray();


        /// <summary>
        /// Indicates the behavior of the <see cref="VisynFileHelperEngine"/>
        /// when it found an error.
        /// </summary>
        /// <remarks>Default error mode is ThrowException.</remarks>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ErrorMode ErrorMode { get; set; } = ErrorMode.ThrowException;


        /// <summary>Number of contained errors.</summary>
        public int ErrorCount => mErrorsArray.Count;

        /// <summary>Indicates if contains one or more errors.</summary>
        public bool HasErrors => mErrorsArray.Count > 0;

        /// <summary>Clears the error collection.</summary>
        public void ClearErrors()
        {
            mErrorsArray.Clear();
        }

        /// <summary>Add the specified ErrorInfo to the contained collection.</summary>
        /// <param name="error"></param>
        public void AddError(ErrorInfo error)
        {
            if (mErrorsArray.Count <= _errorLimit) mErrorsArray.Add(error);
        }

        /// <summary>Add the specified ErrorInfo to the contained collection.</summary>
        internal void AddErrors(ErrorManager errors)
        {
            if (mErrorsArray.Count <= _errorLimit) mErrorsArray.AddRange(errors.mErrorsArray);
        }


        /// <summary>Saves the contained errors to the specified file.</summary>
        /// <param name="fileName">The file that contains the errors.</param>
        public void SaveErrors(string fileName)
        {
            var header = ErrorCount > 0 ? "FileHelpers - Errors Saved " : "FileHelpers - NO Errors Found ";

            header += "at " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            header += Environment.NewLine + "LineNumber | LineString |ErrorDescription";

            SaveErrors(fileName, header);
        }

        /// <summary>Saves the contained errors to the specified file.</summary>
        /// <param name="fileName">The file that contains the errors.</param>
        /// <param name="header">The header line of the errors file.</param>
        public void SaveErrors(string fileName, string header)
        {
            var engine = new VisynFileHelperEngine(typeof (ErrorInfo));

            if (header.IndexOf(Environment.NewLine, StringComparison.Ordinal) == header.LastIndexOf(Environment.NewLine))
                header += Environment.NewLine;

            engine.HeaderText = header;
            engine.WriteFile(fileName, Errors);
        }

        /// <summary>Load errors from a file.</summary>
        /// <param name="fileName">The file that contains the errors.</param>
        public static ErrorInfo[] LoadErrors(string fileName)
        {
            var engine = new VisynFileHelperEngine(typeof (ErrorInfo));
            return (ErrorInfo[]) engine.ReadFile(fileName);
        }

        ///<summary>
        /// Returns an enumerator that iterates through a collection.
        ///</summary>
        ///<returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see>
        /// object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public IEnumerator GetEnumerator()
        {
            return mErrorsArray.GetEnumerator();
        }
    }
}
