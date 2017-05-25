using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a integer
    /// </summary>
    internal sealed class Int32Converter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a integer
        /// </summary>
        public Int32Converter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a value to a integer
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public Int32Converter(string decimalSep) : base(typeof(int), decimalSep) { }

        /// <summary>
        /// Convert a string to an integer
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>integer value</returns>
        protected override object ParseString(string from)
        {
            int res;
            if (
                !int.TryParse(StringHelper.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    Culture,
                    out res))
                throw new ConvertException(from, Type);

            return res;
        }
    }
}