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
using Visyn.Exceptions;
using Visyn.Serialize;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Attributes
{
    /// <summary>Indicates a different delimiter for this field. </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldDelimiterAttribute : FieldAttribute
    {
        /// <summary>
        /// Gets the Delimiter for this field
        /// </summary>
        public string Delimiter { get; private set; }

        /// <summary>Indicates a different delimiter for this field. </summary>
        /// <param name="separator">The separator string used to split the fields of the record.</param>
        public FieldDelimiterAttribute(string separator)
        {
            if (string.IsNullOrEmpty(separator))
            {
                throw new BadUsageException(  "The separator parameter of the FieldDelimiter attribute can't be null or empty");
            }
            Delimiter = separator;
        }
    }
}
