using System;

namespace Visyn.Windows.Io.FileHelper.Attributes
{
    /// <summary>Indicates that this class represents a delimited record. </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class DelimitedRecordAttribute : TypedRecordAttribute, ITypedRecordAttribute
    {
        /// <summary>The string used as a field separator.</summary>
        public string Separator { get; private set; }

        /// <summary>Indicates that this class represents a delimited record. </summary>
        /// <param name="delimiter">The separator string used to split the fields of the record.</param>
        public DelimitedRecordAttribute(string delimiter)
        {
            if (string.IsNullOrEmpty(delimiter))
                throw new ArgumentException("Given delimiter cannot be <> \"\"", nameof(delimiter));
            
            Separator = delimiter;
        }
    }
}