using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a long integer
    /// </summary>
    internal sealed class Int64Converter : CultureConverter<long>
    {
        /// <summary>
        /// Convert a value to a long integer
        /// </summary>
        public Int64Converter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a value to a long integer
        /// </summary>
        /// <param name="decimalSeparator">dot or comma for separator</param>
        public Int64Converter(string decimalSeparator) : base( decimalSeparator) { }

        /// <summary>
        /// Convert a string to an integer long
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>Long value</returns>
        protected override object ParseString(string text)
        {
            long result;
            if (long.TryParse(StringHelper.RemoveBlanks(text), NumberStyles.Number | NumberStyles.AllowExponent,
                Culture, out result))
                    return result;
            throw new ConvertException(text, Type);
        }
    }
}