using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// This Class is specialized version of the Double Converter
    /// The main difference being that it can handle % sign at the end of the number
    /// It gives a value which is basically number / 100.
    /// </summary>
    internal sealed class PercentDoubleConverter : CultureConverter<double>
    {
        /// <summary>
        /// Convert a value to a floating point from a percentage
        /// </summary>
        public PercentDoubleConverter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a value to a floating point from a percentage
        /// </summary>
        /// <param name="decimalSeparator">dot or comma for separator</param>
        public PercentDoubleConverter(string decimalSeparator) : base( decimalSeparator) { }

        /// <summary>
        /// Convert a string to an floating point from percentage
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>floating point value</returns>
        protected override object ParseString(string text)
        {
            double result;
            var blanksRemoved = StringHelper.RemoveBlanks(text);
            if (blanksRemoved.EndsWith("%"))
            {
                if (double.TryParse(blanksRemoved, NumberStyles.Number | NumberStyles.AllowExponent, Culture, out result))
                    return result / 100.0;
                throw new ConvertException(text, Type);
            }
            else
            {
                if (double.TryParse(blanksRemoved, NumberStyles.Number | NumberStyles.AllowExponent, Culture, out result))
                    return result;
                throw new ConvertException(text, Type);
            }
        }
    }
}