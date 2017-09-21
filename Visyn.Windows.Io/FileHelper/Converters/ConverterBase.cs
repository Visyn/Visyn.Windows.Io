using System;
using Visyn.Types;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    public abstract class ConverterBase<T> : ConverterBase
    {
        protected ConverterBase() : base(typeof(T))
        {
        }
    }

    /// <summary>
    /// Base class to provide bi-directional
    /// Field - String conversion.
    /// </summary>
    public abstract class ConverterBase : IFieldConverter
    {
        /// <summary>
        /// The default date time format string
        /// Note: yyyy-MM-dd HH:mm:ss.FFF is recognized by Excel as a Date/Time
        /// </summary>
        private static string _defaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss.FFF";

        private static string _defaultDateFormat = "yyyy-MM-dd";
        /// <summary>
        /// If the class returns false the engines don't pass null values to the converter. 
        /// If true the engines pass all the values to the converter.
        /// </summary>
        public virtual bool CustomNullHandling => false;
        public Type Type { get; }

        /// <summary>
        /// <para>Allow you to set the default Date Format used for the Converter.</para>
        /// <para>using the same CustomDateTimeFormat that is used in the .NET framework.</para>
        /// <para>By default: "ddMMyyyy"</para>
        /// </summary>
        public static string DefaultDateTimeFormat
        {
            get { return _defaultDateTimeFormat; }
            set
            {
                try
                {   // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                    DateTime.Now.ToString(value);
                }
                catch
                {
                    throw new BadUsageException("The format: '" + value + " is invalid for the DateTime Converter.");
                }
                _defaultDateTimeFormat = value;
            }
        }



        public static string DefaultDateFormat
        {
            get { return _defaultDateFormat; }
            set
            {
                try
                {   // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                    DateTime.Now.ToString(value);
                }
                catch
                {
                    throw new BadUsageException($"The format: '{value} is invalid for the {nameof(DateConverter)}.");
                }
                _defaultDateFormat = value;
            }
        }

        protected ConverterBase(Type converterType)
        {
            Type = converterType;
        }

        /// <summary>
        /// Convert a string in the file to a field value.
        /// </summary>
        /// <param name="text">The string to convert.</param>
        /// <returns>The Field value.</returns>
        public abstract object StringToField(string text);

        /// <summary>
        /// Convert a field value to an string to write this to the file.
        /// </summary>
        /// <remarks>The basic implementation just returns  from.ToString();</remarks>
        /// <param name="field">The field values to convert.</param>
        /// <returns>The string representing the field value.</returns>
        public virtual string FieldToString(object field) => field?.ToString() ?? string.Empty;

        /// <summary>
        /// Throws a ConvertException with the passed values
        /// </summary>
        /// <param name="from">The source string.</param>
        /// <param name="errorMsg" >The custom error msg.</param>
        /// <exception cref="ConvertException">Throw exception with values</exception>
        [Obsolete("Use extension method in ConverterExtensions")]
        public void ThrowConvertException(string from, string errorMsg)
        {
            throw new ConvertException(from, Type, errorMsg);
        }
    }
}