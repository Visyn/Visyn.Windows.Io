using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a long integer
    /// </summary>
    internal sealed class Int64Converter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a long integer
        /// </summary>
        public Int64Converter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a value to a long integer
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public Int64Converter(string decimalSep)
            : base(typeof(long), decimalSep) { }

        /// <summary>
        /// Convert a string to an integer long
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>Long value</returns>
        protected override object ParseString(string from)
        {
            long res;
            if (
                !long.TryParse(StringHelper.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    Culture,
                    out res))
                throw new ConvertException(from, Type);
            return res;
        }
    }
}