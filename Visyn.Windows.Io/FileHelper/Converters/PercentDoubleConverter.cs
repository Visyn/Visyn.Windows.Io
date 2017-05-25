using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// This Class is specialized version of the Double Converter
    /// The main difference being that it can handle % sign at the end of the number
    /// It gives a value which is basically number / 100.
    /// </summary>
    /// <remarks>Edited : Shreyas Narasimhan (17 March 2010) </remarks>
    internal sealed class PercentDoubleConverter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a floating point from a percentage
        /// </summary>
        public PercentDoubleConverter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a value to a floating point from a percentage
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public PercentDoubleConverter(string decimalSep)
            : base(typeof(Double), decimalSep) { }

        /// <summary>
        /// Convert a string to an floating point from percentage
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>floating point value</returns>
        protected override object ParseString(string from)
        {
            double res;
            var blanksRemoved = StringHelper.RemoveBlanks(from);
            if (blanksRemoved.EndsWith("%"))
            {
                if (
                    !Double.TryParse(blanksRemoved,
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        Culture,
                        out res))
                    throw new ConvertException(from, Type);
                return res / 100.0;
            }
            else
            {
                if (
                    !Double.TryParse(blanksRemoved,
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        Culture,
                        out res))
                    throw new ConvertException(from, Type);
                return res;
            }
        }
    }
}