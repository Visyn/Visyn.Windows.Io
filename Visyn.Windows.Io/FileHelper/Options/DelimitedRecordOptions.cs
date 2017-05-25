

using Visyn.Windows.Io.FileHelper.Core;
using Visyn.Windows.Io.FileHelper.Fields;

namespace Visyn.Windows.Io.FileHelper.Options
{
    /// <summary>
    /// This class allows you to set some options of the delimited records
    /// at runtime. With options the library is more flexible.
    /// </summary>
    public sealed class DelimitedRecordOptions : RecordOptions
    {
        /// <summary>
        /// This class allows you to set some options of the delimited records
        /// at runtime. With options the library is more flexible.
        /// </summary>
        /// <param name="info">Record information</param>
        public DelimitedRecordOptions(IRecordInfo info) : base(info) {}

        /// <summary>
        /// The delimiter used to identify each field in the data.
        /// </summary>
        public string Delimiter
        {
            get { return ((DelimitedField) mRecordInfo.Fields[0]).Separator; }
            set
            {
                for (var i = 0; i < mRecordInfo.FieldCount; i++)
                    ((DelimitedField) mRecordInfo.Fields[i]).Separator = value;
            }
        }
    }
}