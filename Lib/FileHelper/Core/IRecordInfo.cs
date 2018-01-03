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
using System.Reflection;
using System.Text.RegularExpressions;
using Visyn.Windows.Io.FileHelper.Enums;
using Visyn.Windows.Io.FileHelper.Fields;

namespace Visyn.Windows.Io.FileHelper.Core
{
    /// <summary>
    /// Record information,  whether it is delimited or other details
    /// </summary>
    public interface IRecordInfo : ICloneable
    {
        /// <summary>
        /// Is the input delimited or fixed length
        /// </summary>
        bool IsDelimited { get; }

        /// <summary>
        /// Number of fields defined
        /// </summary>
        int FieldCount { get; }

        /// <summary>
        /// List of fields in order read from record
        /// </summary>
        FieldBase[] Fields { get; }

        /// <summary>
        /// Number of records to skip before starting processing (header records)
        /// </summary>
        int IgnoreFirst { get; set; }

        /// <summary>
        /// Number of records to skip when processing end of file (Trailer records)
        /// </summary>
        int IgnoreLast { get; set; }

        /// <summary>
        /// Whether the Notify read event is set or something else needs to be notified of change
        /// </summary>
        bool NotifyRead { get; }
        
        /// <summary>
        /// Whether the notify write event is hooked or something else needs to be notified on write
        /// </summary>
        bool NotifyWrite { get; }

        /// <summary>
        /// Buffer beginning size hint
        /// </summary>
        int SizeHint { get; }

        /// <summary>
        /// Type of object to be created
        /// </summary>
        Type RecordType { get; }

        /// <summary>
        /// Do we skip empty lines?
        /// </summary>
        bool IgnoreEmptyLines { get; set; }

        /// <summary>
        /// Do we skip lines that are visually empty,  spaces only
        /// </summary>
        bool IgnoreEmptySpaces { get; }

        /// <summary>
        /// String that prefixes a comment,  eg a # in shell script
        /// </summary>
        string CommentMarker { get; set; }

        /// <summary>
        /// Can the comment marker be proceeded with spaces.
        /// </summary>
        bool CommentAnyPlace { get; set; }

        /// <summary>
        /// Selection condition when reading records, allows skipping unused data
        /// </summary>
        RecordCondition RecordCondition { get; set; }

        /// <summary>
        /// Selection of records to process by regex
        /// </summary>
        Regex RecordConditionRegEx { get; }

        /// <summary>
        /// Selection of records to process by prefix
        /// </summary>
        string RecordConditionSelector { get; set; }

        /// <summary>
        /// Get the position in the list of FieldName
        /// </summary>
        /// <param name="fieldName">name to look up</param>
        /// <returns>Position in list</returns>
        int GetFieldIndex(string fieldName);

        /// <summary>
        /// /Get the complete information about this field
        /// </summary>
        /// <param name="name">FieldName to look up for information</param>
        /// <returns>FieldInfo on the name</returns>
        FieldInfo GetFieldInfo(string name);

        /// <summary>
        /// Cache of routines to handle various operations on the record
        /// </summary>
        RecordOperations Operations { get; }

        void RemoveField(string fieldname);
    }
}
