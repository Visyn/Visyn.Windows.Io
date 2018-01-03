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

namespace Visyn.Windows.Io.FileHelper.Attributes
{
    /// <summary>
    /// Allows you to set the length or bounds that the target array field must have.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldArrayLengthAttribute : Attribute
    {
        #region "  Constructors  "

        /// <summary>
        /// Allows you to set the bounds that the target array field must have.
        /// </summary>
        /// <param name="minLength">The lower bound</param>
        /// <param name="maxLength">The upper bound</param>
        public FieldArrayLengthAttribute(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        /// <summary>
        /// Allow you to set the exact length that the target array field must have.
        /// </summary>
        /// <param name="length">The exact length of the array field.</param>
        public FieldArrayLengthAttribute(int length)
            : this(length, length) {}

        #endregion

        /// <summary>Array lower bound.</summary>
        public int MinLength { get; private set; }

        /// <summary>Array upper bound.</summary>
        public int MaxLength { get; private set; }
    }
}
