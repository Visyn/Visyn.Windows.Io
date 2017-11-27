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
using Visyn.Windows.Io.FileHelper.Enums;

namespace Visyn.Windows.Io.FileHelper.Attributes
{
    /// <summary>Indicates the <see cref="AlignMode"/> used for <b>write</b> operations.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldAlignAttribute : Attribute
    {
        /// <summary>The position of the alignment.</summary>
        public AlignMode Align { get; private set; }

        /// <summary>The character used to align.</summary>
        public char AlignChar { get; private set; }

        #region "  Constructors  "

        /// <summary>Uses the ' ' char to align.</summary>
        /// <param name="align">The position of the alignment.</param>
        public FieldAlignAttribute(AlignMode align)
            : this(align, ' ') {}

        /// <summary>You can indicate the align character.</summary>
        /// <param name="align">The position of the alignment.</param>
        /// <param name="alignChar">The character used to align.</param>
        public FieldAlignAttribute(AlignMode align, char alignChar)
        {
            Align = align;
            AlignChar = alignChar;
        }

        #endregion
    }
}
