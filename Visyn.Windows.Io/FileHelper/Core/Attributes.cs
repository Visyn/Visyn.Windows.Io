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
using System.Reflection;

namespace Visyn.Windows.Io.FileHelper.Core
{
    internal static class Attributes
    {
        /// <summary>
        /// Get the first attribute that matches the type specification (not inherited)
        /// </summary>
        /// <typeparam name="T">Type of attribute to find</typeparam>
        /// <param name="type">Member info to look at for attributes</param>
        /// <returns>attribute or null</returns>
        public static T GetFirst<T>(MemberInfo type) where T : Attribute
        {
            return GetFirstCore<T>(type, false);
        }

        /// <summary>
        /// Get the first attribute of this type (or its children)
        /// </summary>
        /// <typeparam name="T">Type of attribute</typeparam>
        /// <param name="type">member info containing attributes</param>
        /// <returns>Found attribute or null</returns>
        public static T GetFirstInherited<T>(MemberInfo type) where T : Attribute
        {
            return GetFirstCore<T>(type, true);
        }

        /// <summary>
        /// Worker to get attributes from MemberInfo details
        /// </summary>
        /// <typeparam name="T">Type to create</typeparam>
        /// <param name="type">Member Info details</param>
        /// <param name="inherited">Allow inherited version of attribute?</param>
        /// <returns>Attribute found or null</returns>
        private static T GetFirstCore<T>(MemberInfo type, bool inherited) where T : Attribute
        {
            var attribs = type.GetCustomAttributes(typeof (T), inherited);
            if (attribs.Length == 0)
                return null;
            else
                return (T) attribs[0];
        }

        /// <summary>
        /// Locate an attribute and perform an Action on it, do nothing if you don't find it
        /// </summary>
        /// <typeparam name="T">Type of attribute we are looking for</typeparam>
        /// <param name="type">Field type we are analysing</param>
        /// <param name="action">action we want to perform</param>
        public static void WorkWithFirst<T>(MemberInfo type, Action<T> action) where T : Attribute
        {
            var attribs = type.GetCustomAttributes(typeof (T), false);
            if (attribs.Length == 0)
                return;

            action((T) attribs[0]);
        }
    }
}
