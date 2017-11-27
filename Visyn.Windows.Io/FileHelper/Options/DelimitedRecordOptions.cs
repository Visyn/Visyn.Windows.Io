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

using Visyn.Windows.Io.FileHelper.Core;
using Visyn.Windows.Io.FileHelper.Fields;

namespace Visyn.Windows.Io.FileHelper.Options
{
    /// <summary>
    /// This class allows you to set some options of the delimited records
    /// at runtime. With options the library is more flexible.
    /// </summary>
    public sealed class DelimitedRecordOptions : RecordOptions
    {
        /// <summary>
        /// This class allows you to set some options of the delimited records
        /// at runtime. With options the library is more flexible.
        /// </summary>
        /// <param name="info">Record information</param>
        public DelimitedRecordOptions(IRecordInfo info) : base(info) {}

        /// <summary>
        /// The delimiter used to identify each field in the data.
        /// </summary>
        public string Delimiter
        {
            get { return ((DelimitedField) mRecordInfo.Fields[0]).Separator; }
            set
            {
                for (var i = 0; i < mRecordInfo.FieldCount; i++)
                    ((DelimitedField) mRecordInfo.Fields[i]).Separator = value;
            }
        }
    }
}
