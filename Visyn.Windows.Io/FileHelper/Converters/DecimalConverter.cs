using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a decimal value
    /// </summary>
    internal sealed class DecimalConverter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a decimal value
        /// </summary>
        public DecimalConverter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a value to a decimal value
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public DecimalConverter(string decimalSep)
            : base(typeof(decimal), decimalSep) { }

        /// <summary>
        /// Convert a string to a decimal
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>decimal value</returns>
        protected override object ParseString(string from)
        {
            decimal res;
            if (
                !decimal.TryParse(StringHelper.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    Culture,
                    out res))
                throw new ConvertException(from, Type);
            return res;
        }
    }
}