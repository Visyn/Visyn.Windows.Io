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

using System.Diagnostics;
using System.Linq;
using Visyn.Windows.Io.FileHelper.Core;
using Visyn.Windows.Io.FileHelper.Enums;
using Visyn.Windows.Io.FileHelper.Fields;

namespace Visyn.Windows.Io.FileHelper.Options
{
    /// <summary>
    /// This class allows you to set some options of the fixed length records
    /// but at runtime.
    /// With this options the library is more flexible than never.
    /// </summary>
    public sealed class FixedRecordOptions : RecordOptions
    {
        /// <summary>
        /// This class allows you to set some options of the fixed length
        /// records but at runtime.
        /// With this options the library is more flexible than never.
        /// </summary>
        /// <param name="info">Record information</param>
        public FixedRecordOptions(IRecordInfo info) : base(info) {}


        /// <summary>
        /// Indicates the behavior when variable length records are found in a
        /// [<see cref="FileHelper.Attributes.FixedLengthRecordAttribute"/>]. (Note: nothing in
        /// common with [FieldOptional])
        /// </summary>
        public FixedMode FixedMode
        {
            get { return ((FixedLengthField) mRecordInfo.Fields[0]).FixedMode; }
            set
            {
                for (var i = 0; i < mRecordInfo.FieldCount; i++)
                    ((FixedLengthField) mRecordInfo.Fields[i]).FixedMode = value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int mRecordLength = int.MinValue;

        /// <summary>
        /// The sum of the individual field lengths.
        /// </summary>
        public int RecordLength
        {
            get
            {
                if (mRecordLength != int.MinValue) return mRecordLength;

                mRecordLength = mRecordInfo.Fields.Sum(fieldBase => ((FixedLengthField) fieldBase).FieldLength);

                return mRecordLength;
            }
        }
    }
}
