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

namespace Visyn.Windows.Io.FileHelper.Core
{
    /// <summary>
    /// extensions to help with types
    /// </summary>
    internal static class TypeHelper
    {
        /// <summary>
        /// Is this type any sort of numeric
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [Obsolete("Use Visyn.Mathematics.IsNumericType.IsNumeric",true)]
        public static bool IsNumericType(Type type)
        {
            return type == typeof (Int16) ||
                   type == typeof (Int32) ||
                   type == typeof (Int64) ||
                   type == typeof (UInt16) ||
                   type == typeof (UInt32) ||
                   type == typeof (UInt64) ||
                   type == typeof (byte) ||
                   type == typeof (sbyte) ||
                   type == typeof (decimal) ||
                   type == typeof (float) ||
                   type == typeof (double) ||
                   type == typeof (Int16?) ||
                   type == typeof (Int32?) ||
                   type == typeof (Int64?) ||
                   type == typeof (UInt16?) ||
                   type == typeof (UInt32?) ||
                   type == typeof (UInt64?) ||
                   type == typeof (byte?) ||
                   type == typeof (sbyte?) ||
                   type == typeof (decimal?) ||
                   type == typeof (float?) ||
                   type == typeof (double?);
        }
    }
}
