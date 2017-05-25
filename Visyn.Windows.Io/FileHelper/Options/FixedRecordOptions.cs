using System.Diagnostics;
using System.Linq;
using Visyn.Windows.Io.FileHelper.Core;
using Visyn.Windows.Io.FileHelper.Enums;
using Visyn.Windows.Io.FileHelper.Fields;

namespace Visyn.Windows.Io.FileHelper.Options
{
    /// <summary>
    /// This class allows you to set some options of the fixed length records
    /// but at runtime.
    /// With this options the library is more flexible than never.
    /// </summary>
    public sealed class FixedRecordOptions : RecordOptions
    {
        /// <summary>
        /// This class allows you to set some options of the fixed length
        /// records but at runtime.
        /// With this options the library is more flexible than never.
        /// </summary>
        /// <param name="info">Record information</param>
        public FixedRecordOptions(IRecordInfo info) : base(info) {}


        /// <summary>
        /// Indicates the behavior when variable length records are found in a
        /// [<see cref="FileHelper.Attributes.FixedLengthRecordAttribute"/>]. (Note: nothing in
        /// common with [FieldOptional])
        /// </summary>
        public FixedMode FixedMode
        {
            get { return ((FixedLengthField) mRecordInfo.Fields[0]).FixedMode; }
            set
            {
                for (var i = 0; i < mRecordInfo.FieldCount; i++)
                    ((FixedLengthField) mRecordInfo.Fields[i]).FixedMode = value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int mRecordLength = int.MinValue;

        /// <summary>
        /// The sum of the individual field lengths.
        /// </summary>
        public int RecordLength
        {
            get
            {
                if (mRecordLength != int.MinValue) return mRecordLength;

                mRecordLength = mRecordInfo.Fields.Sum(fieldBase => ((FixedLengthField) fieldBase).FieldLength);

                return mRecordLength;
            }
        }
    }
}