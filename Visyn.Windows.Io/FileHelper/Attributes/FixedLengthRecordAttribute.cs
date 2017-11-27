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
using Visyn.Serialize;
using Visyn.Windows.Io.FileHelper.Enums;

namespace Visyn.Windows.Io.FileHelper.Attributes
{
    /// <summary>Indicates that this class represents a fixed length record.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class FixedLengthRecordAttribute : TypedRecordAttribute, ITypedRecordAttribute
    {
        /// <summary>Indicates the behavior when variable length records are found.</summary>
        public FixedMode FixedMode { get; private set; }

        /// <summary>Indicates that this class represents a fixed length
        /// record. By default fixed length files require the records to have
        /// equal length.
        /// (ie the record length equals the sum of each field length.
        /// </summary>
        public FixedLengthRecordAttribute()
            : this(FixedMode.ExactLength) {}

        /// <summary>
        /// Indicates that this class represents a fixed length record with the
        /// specified variable length record behavior.
        /// </summary>
        /// <param name="fixedMode">The <see cref="FileHelpers.FixedMode"/> used for variable length records. By Default is FixedMode.ExactLength</param>
        public FixedLengthRecordAttribute(FixedMode fixedMode)
        {
            FixedMode = fixedMode;
        }
    }
}
