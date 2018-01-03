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

namespace Visyn.Windows.Io.FileHelper
{
    /// <summary>
    /// add validation exceptions
    /// </summary>
    internal static class ExHelper
    {
        /// <summary>
        /// Check the string is null or empty and throw an exception
        /// </summary>
        /// <param name="val">value to test</param>
        /// <param name="paramName">name of parameter to check</param>
        public static void CheckNullOrEmpty(string val, string paramName)
        {
            if (string.IsNullOrEmpty(val))
                throw new ArgumentNullException(paramName, "Value can't be null or empty");
        }

        /// <summary>
        /// Check that parameter is not null or empty and throw an exception
        /// </summary>
        /// <param name="param">value to check</param>
        /// <param name="paramName">parameter name</param>
        public static void CheckNullParam(string param, string paramName)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentNullException(paramName, paramName + " can't be neither null nor empty");
        }

        /// <summary>
        /// Check that parameter is not null and throw an exception
        /// </summary>
        /// <param name="param">value to check</param>
        /// <param name="paramName">parameter name</param>
        public static void CheckNullParam(object param, string paramName)
        {
            if (param == null)
                throw new ArgumentNullException(paramName, paramName + " can't be null");
        }

        /// <summary>
        /// check that parameter 1 is different from parameter 2
        /// </summary>
        /// <param name="param1">value 1 to test</param>
        /// <param name="param1Name">name of value 1</param>
        /// <param name="param2">value 2 to test</param>
        /// <param name="param2Name">name of vlaue 2</param>
        public static void CheckDifferentsParams(object param1, string param1Name, object param2, string param2Name)
        {
            if (param1 == param2) {
                throw new ArgumentException(param1Name + " can't be the same as " + param2Name,
                    param1Name + " and " + param2Name);
            }
        }

        /// <summary>
        /// Check an integer value is positive (0 or greater)
        /// </summary>
        /// <param name="val">Integer to test</param>
        public static void PositiveValue(int val)
        {
            if (val < 0)
                throw new ArgumentException("The value must be greater than or equal to 0.");
        }
    }
}
