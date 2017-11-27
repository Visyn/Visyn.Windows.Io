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
using System.Diagnostics;
using System.Reflection;

namespace Visyn.Windows.Io.FileHelper.Core
{
    internal class FieldInfoCacheManipulator
    {
        private static PropertyInfo mCacheProperty;

        /// <summary>
        /// Very importat to avoid out of order reflection
        /// The CLR caches previous fields access to speed up reflection but can return the fields in wrong order
        /// Clearing the m_fieldInfoCache of the Cache property resolves the issue
        /// </summary>
        /// <param name="type">Type of Object</param>
        public static void ResetFieldInfoCache(Type type)
        {
            if (mCacheProperty == null) {
                mCacheProperty = type.GetType().GetProperty("Cache",
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Instance |
                    BindingFlags.NonPublic);
            }
            Debug.Assert(mCacheProperty != null, "There is no Cache property in the RuntimeType: " + type.GetType().Name);

            if (mCacheProperty != null) {
                var cacheObject = mCacheProperty.GetValue(type, null);

                Debug.Assert(cacheObject != null,
                    "There is no value for the Cache property in the RuntimeType: " + type.Name);
                var cacheField = cacheObject.GetType().GetField("m_fieldInfoCache",
                    BindingFlags.FlattenHierarchy | BindingFlags.Instance |
                    BindingFlags.NonPublic);

                Debug.Assert(cacheField != null,
                    "There is no m_fieldInfoCache field for the RuntimeTypeCache: " + type.Name);
                if (cacheField != null)
                    cacheField.SetValue(cacheObject, null);
            }
        }
    }
}
