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
    /// <summary>Indicates the behavior of quoted fields.</summary>
    public enum QuoteMode
    {
        /// <summary>
        /// The engines expects that the field must always be surrounded with
        /// quotes when reading and always adds the quotes when writing.
        /// </summary>
        AlwaysQuoted = 0,

        /// <summary>
        /// The engine can handle a field even if it is not surrounded with
        /// quotes while reading but it always add the quotes when writing.
        /// </summary>
        OptionalForRead,

        /// <summary>
        /// The engine always expects a quote when read and it will only add
        /// the quotes when writing only if the field contains quotes, new
        /// lines or the separator char.
        /// </summary>
        OptionalForWrite,

        /// <summary>
        /// The engine can handle a field even if it is not surrounded with
        /// quotes while reading and it will only add the quotes when writing
        /// if the field contains quotes, new lines or the separator char.
        /// </summary>
        OptionalForBoth
    }
}
