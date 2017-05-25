using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a decimal value
    /// </summary>
    internal sealed class DecimalConverter : CultureConverter<decimal>
    {
        /// <summary>
        /// Convert a value to a decimal value
        /// </summary>
        public DecimalConverter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a value to a decimal value
        /// </summary>
        /// <param name="decimalSeparator">dot or comma for separator</param>
        public DecimalConverter(string decimalSeparator) : base( decimalSeparator) { }

        /// <summary>
        /// Convert a string to a decimal
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>decimal value</returns>
        protected override object ParseString(string text)
        {
            decimal res;
            if (
                !decimal.TryParse(StringHelper.RemoveBlanks(text),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    Culture,
                    out res))
                throw new ConvertException(text, Type);
            return res;
        }
    }
}