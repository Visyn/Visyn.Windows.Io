using System;
using System.Collections.Generic;
using System.Linq;
using Visyn.Core.Mathematics;
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
            new Dictionary<Type, ITypedRecordAttribute>() { { typeof(Statistics), new DelimitedRecordAttribute(",") } };

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