using System;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    ///  Convert a GUID to and from a field value
    /// </summary>
    internal sealed class GuidConverter : ConverterBase<Guid>
    {
        /// <summary>
        /// D or N or B or P (default is D: see Guid.ToString(string format))
        /// </summary>
        private readonly string _format;

        /// <summary>
        /// Create a GUID converter with the default format code "D"
        /// </summary>
        public GuidConverter() : this("D") // D or N or B or P (default is D: see Guid.ToString(string format))
        { }

        /// <summary>
        /// Create a GUID converter with formats as defined for GUID
        /// N, D, B or P
        /// </summary>
        /// <param name="format">Format code for GUID</param>
        public GuidConverter(string format)
        {
            if (string.IsNullOrEmpty(format)) format = "D";

            format = format.Trim().ToUpper();

            if (!(format == "N" || format == "D" || format == "B" || format == "P"))
                throw new BadUsageException("The format of the Guid Converter must be N, D, B or P.");

            _format = format;
        }

        /// <summary>
        /// Convert a GUID string to a GUID object for the record object
        /// </summary>
        /// <param name="text">String representation of the GUID</param>
        /// <returns>GUID object or GUID empty</returns>
        public override object StringToField(string text)
        {
            if (string.IsNullOrEmpty(text)) return Guid.Empty;

            try
            {
                return new Guid(text);
            }
            catch
            {
                throw new ConvertException(text, typeof(Guid));
            }
        }

        /// <summary>
        /// Output GUID as a string field
        /// </summary>
        /// <param name="guid">Guid object</param>
        /// <returns>GUID as a string depending on format</returns>
        public override string FieldToString(object guid) => ((Guid?)guid)?.ToString(_format) ?? string.Empty;
    }
}