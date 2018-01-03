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

namespace Visyn.Windows.Io.FileHelper.Enums
{
    /// <summary>
    /// Indicates the behavior when variable length records are found in a
    /// [<see cref="FileHelper.Attributes.FixedLengthRecordAttribute"/>]. (Note: nothing in
    /// common with [FieldOptional])
    /// </summary>
    public enum FixedMode
    {
        /// <summary>
        /// The records must have the length equals to the sum of each
        /// field length. Default Behavior.
        /// </summary>
        ExactLength = 0,

        /// <summary>
        /// The records can contain more chars after the last field
        /// </summary>
        AllowMoreChars,

        /// <summary>
        /// The records can contain less chars. Based on the
        /// combination with FieldOptional the records can contain less
        /// fields in the last, or if it is marked as optional, in the
        /// previous field.
        /// </summary>
        AllowLessChars,

        /// <summary>
        /// The records can contain more or less chars in the last
        /// field
        /// </summary>
        AllowVariableLength
    }
}
