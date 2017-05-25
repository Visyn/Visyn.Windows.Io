using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a date time value
    /// </summary>
    internal sealed class DateTimeConverter : ConverterBase<DateTime>
    {
        private readonly string _format;
        private readonly CultureInfo _culture;

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        public DateTimeConverter() : this(DefaultDateTimeFormat) { }

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        /// <param name="format">date format see .Net documentation</param>
        public DateTimeConverter(string format) : this(format, null) { }

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        /// <param name="format">date format see .Net documentation</param>
        /// <param name="culture">The culture used to parse the Dates</param>
        public DateTimeConverter(string format, string culture)
        {
            if (string.IsNullOrEmpty(format))
                throw new BadUsageException("The format of the DateTime Converter cannot be null or empty.");

            try
            {   // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                DateTime.Now.ToString(format);
            }
            catch
            {
                throw new BadUsageException($"The format: '{format}' is invalid for the DateTime Converter.");
            }

            _format = format;
            if (culture != null)
                _culture = CultureInfo.GetCultureInfo(culture);
        }
        /// <summary>
        /// Convert a string to a date time value
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>DateTime value</returns>
        public override object StringToField(string text)
        {
            if (text == null) text = string.Empty;

            DateTime val;
            if (DateTime.TryParseExact(text.Trim(), _format, _culture, DateTimeStyles.None, out val)) return val;

            if (text.Length > _format.Length)
                throw new ConvertException(text, typeof(DateTime),
                    $" There are more chars in the Input String than in the Format string: '{_format}'");
            if (text.Length < _format.Length)
                throw new ConvertException(text, typeof(DateTime),
                    $" There are fewer chars in the Input String than in the Format string: '{ _format}'");
            throw new ConvertException(text, typeof(DateTime), $" Using the format: '{ _format }'");
        }

        /// <summary>
        /// Convert a date time value to a string
        /// </summary>
        /// <param name="dateTime">DateTime value to convert</param>
        /// <returns>string DateTime value</returns>
        public override string FieldToString(object dateTime)
        {
            return dateTime == null ? string.Empty : Convert.ToDateTime(dateTime).ToString(_format, _culture);
        }
    }
}