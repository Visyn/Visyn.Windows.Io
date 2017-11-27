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
    /// <summary>Indicates the <see cref="TrimMode"/> used after reading to truncate the field. </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldTrimAttribute : Attribute
    {
        /// <summary>A string of chars used to trim.</summary>
        public Char[] TrimChars { get; private set; }

        /// <summary>The TrimMode used after read.</summary>
        public TrimMode TrimMode { get; private set; }
        
        #region "  Constructors  "

        /// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. By default trims the blank spaces and tabs.</summary>
        /// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
        public FieldTrimAttribute(TrimMode mode)
            : this(mode, LineInfo.WhitespaceChars) {}

        /// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. </summary>
        /// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
        /// <param name="chars">A list of chars used to trim.</param>
        public FieldTrimAttribute(TrimMode mode, params char[] chars)
        {
            TrimMode = mode;
            Array.Sort(chars);
            TrimChars = chars;
        }

        /// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. </summary>
        /// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
        /// <param name="trimChars">A string of chars used to trim.</param>
        public FieldTrimAttribute(TrimMode mode, string trimChars)
            : this(mode, trimChars.ToCharArray()) {}

        #endregion
    }
}
