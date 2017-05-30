using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Signed byte converter (8 bit signed integer)
    /// </summary>
    internal sealed class SByteConverter : CultureConverter<short>
    {
        /// <summary>
        /// Signed byte converter (8 bit signed integer)
        /// </summary>
        public SByteConverter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Signed byte converter (8 bit signed integer)
        /// </summary>
        /// <param name="decimalSeparator">dot or comma for separator</param>
        public SByteConverter(string decimalSeparator) : base( decimalSeparator) { }

        /// <summary>
        /// Convert a string to an signed byte
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>Signed byte value</returns>
        protected override object ParseString(string text)
        {
            short res;
            if (short.TryParse(StringHelper.RemoveBlanks(text), NumberStyles.Number, Culture, out res))
                return res;
            throw new ConvertException(text, Type);
        }
    }
}