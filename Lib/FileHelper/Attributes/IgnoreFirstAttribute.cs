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
    /// <summary>Indicates the number of lines at beginning of the file to be ignored.</summary>
    /// <remarks>
    /// Useful to ignore header records that you are not interested in.
    /// <para/>
    /// See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more
    /// information and examples of each one.
    /// </remarks>

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IgnoreFirstAttribute : Attribute
    {
        /// <summary>The number of lines at beginning of the file to be ignored.</summary>
        public int NumberOfLines { get; private set; }

        /// <summary>Indicates that the first line of the file is ignored.</summary>
        public IgnoreFirstAttribute()
            : this(1) {}

        /// <summary>Indicates the number of lines at beginning of the file to be ignored.</summary>
        /// <param name="numberOfLines">The number of lines to be ignored.</param>
        public IgnoreFirstAttribute(int numberOfLines)
        {
            NumberOfLines = numberOfLines;
        }
    }
}
