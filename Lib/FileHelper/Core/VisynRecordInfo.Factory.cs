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
using System.Collections.Generic;
using System.Linq;
using Visyn.Mathematics;
using Visyn.Serialize;
using Visyn.Windows.Io.FileHelper.Attributes;

namespace Visyn.Windows.Io.FileHelper.Core
{
    /// <summary>An internal class used to store information about the Record Type.</summary>
    public sealed partial class VisynRecordInfo
    {

        public void RemoveField(string fieldname)
        {
            var index = Fields.ToList().IndexOf(Fields.Single(f => f.FieldFriendlyName == fieldname));
            Fields[index] = null;
            Fields = Array.FindAll(Fields, x => x != null);

            AdjustParentIndex();
        }

        private void AdjustParentIndex()
        {
            for (var i = 0; i < Fields.Length; i++)
            {
                Fields[i].ParentIndex = i;
            }
        }
    }

    public static class VisynRecordInfoFactory
    {
        private static readonly Dictionary<Type, VisynRecordInfo> _recordInfoCache = new Dictionary<Type, VisynRecordInfo>();

        internal static Dictionary<Type, ITypedRecordAttribute> KnownTypes { get; } =
            new Dictionary<Type, ITypedRecordAttribute>() { { typeof(Statistics), new DelimitedRecordAttribute(",") }, { typeof(double[]), new DelimitedRecordAttribute(",") } };

        /// <summary>
        /// Return the record information for the type
        /// </summary>
        /// <param name="type">Type we want settings for</param>
        /// <remarks>Threadsafe</remarks>
        /// <returns>Record Information (settings and functions)</returns>
        public static IRecordInfo Resolve(Type type)
        {
            lock (type)
            {
                VisynRecordInfo res;
                lock (_recordInfoCache)
                {
                    if (_recordInfoCache.TryGetValue(type, out res))
                        return (IRecordInfo)res.Clone();
                }

                // class check cache / lock / check cache  and create if null algorythm
                res = KnownTypes.ContainsKey(type) ?
                    new VisynRecordInfo(type, KnownTypes[type]) :
                    new VisynRecordInfo(type);
                lock (_recordInfoCache)
                {
                    if (!_recordInfoCache.ContainsKey(type))
                        _recordInfoCache.Add(type, res);
                }

                return (IRecordInfo)res.Clone();
            }
        }
    }
}
