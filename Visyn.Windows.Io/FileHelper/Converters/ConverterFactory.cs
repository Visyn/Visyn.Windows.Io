using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;
using Visyn.Windows.Io.FileHelper.Attributes;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Class that provides static methods that returns a default 
    /// <see cref="ConverterBase">Converter</see> to the basic types.
    /// </summary>
    /// <remarks>
    ///     Used by the <see cref="FieldConverterAttribute"/>.
    /// </remarks>
    internal static class ConverterFactory
    {
        public const string DefaultDecimalSeparator = ".";

        #region "  CreateCulture  "

        /// <summary>
        /// Return culture information for with comma decimal separator or comma decimal separator
        /// </summary>
        /// <param name="decimalSeperator">Decimal separator string</param>
        /// <returns>Cultural information based on separator</returns>
        internal static CultureInfo CreateCulture(string decimalSeperator)
        {
            var ci = new CultureInfo(CultureInfo.CurrentCulture.LCID);

            switch (decimalSeperator)
            {
                case ".":
                    ci.NumberFormat.NumberDecimalSeparator = ".";
                    ci.NumberFormat.NumberGroupSeparator = ",";
                    break;
                case ",":
                    ci.NumberFormat.NumberDecimalSeparator = ",";
                    ci.NumberFormat.NumberGroupSeparator = ".";
                    break;
                default:
                    throw new BadUsageException("You can only use '.' or ',' as decimal or group separators");
            }
            return ci;
        }

        #endregion

        #region "  GetDefaultConverter  "

        /// <summary>
        /// Check the type of the field and then return a converter for that particular type
        /// </summary>
        /// <param name="fieldName">Field name to check</param>
        /// <param name="fieldType">Type of the field to check</param>
        /// <returns>Converter for this particular field</returns>
        [Obsolete("This should be re-written to find converters via reflection")]
        internal static IFieldConverter GetDefaultConverter(string fieldName, Type fieldType)
        {
            if (fieldType.IsArray)
            {
                if (fieldType.GetArrayRank() != 1) {
                    throw new BadUsageException($"The array field: '{fieldName}' has more than one dimension and is not supported by the library.");
                }

                fieldType = fieldType.GetElementType();

                if (fieldType.IsArray) {
                    throw new BadUsageException($"The array field: '{ fieldName}' is a jagged array and is not supported by the library.");
                }
            }

            if (fieldType.IsValueType &&
                fieldType.IsGenericType &&
                fieldType.GetGenericTypeDefinition() == typeof (Nullable<>))
                fieldType = fieldType.GetGenericArguments()[0];


            // Try to assign a default Converter
            if (fieldType == typeof (string)) return null;
            if (fieldType == typeof (Int16)) return new Int16Converter();

            if (fieldType == typeof (Int32)) return new Int32Converter();

            if (fieldType == typeof (Int64))  return new Int64Converter();

            if (fieldType == typeof (SByte)) return new SByteConverter();

            if (fieldType == typeof (UInt16)) return new UInt16Converter();

            if (fieldType == typeof (UInt32)) return new UInt32Converter();

            if (fieldType == typeof (UInt64)) return new UInt64Converter();

            if (fieldType == typeof (byte)) return new ByteConverter();

            if (fieldType == typeof (decimal)) return new DecimalConverter();

            if (fieldType == typeof (double)) return new DoubleConverter();

            if (fieldType == typeof (Single)) return new SingleConverter();

            if (fieldType == typeof (DateTime)) return new DateTimeConverter();
            if (fieldType == typeof(TimeSpan)) return new TimeSpanToSecondsConverter();

            if (fieldType == typeof (bool)) return new BooleanConverter();

            if (fieldType == typeof (char)) return new CharConverter();
            if (fieldType == typeof (Guid)) return new GuidConverter();
            if (fieldType.IsEnum) return new EnumConverter(fieldType);

            throw new BadUsageException($"The field: '{ fieldName}' has the type: {fieldType.Name} that is not a system type, so this field need a CustomConverter ( Please Check the docs for more Info).");
        }

        #endregion
    }


    //// Added by Alexander Obolonkov 2007.11.08
    //internal sealed class StringConverter : ConverterBase
    //{
    //    string mFormat;

    //    public StringConverter()
    //        : this(null)
    //    {
    //    }

    //    public StringConverter(string format)
    //    {
    //            //throw new BadUsageException("The format of the String Converter can be null or empty.");

    //        if (String.IsNullOrEmpty(format))
    //            mFormat = null;
    //        else
    //        {
    //            mFormat = format;

    //            try
    //            {
    //                string tmp = String.Format(format, "Any String");
    //            }
    //            catch
    //            {
    //                throw new BadUsageException(
    //                    String.Format("The format: '{0}' is invalid for the String Converter.", format));
    //            }
    //        }
    //    }

    //    public override object StringToField(string from)
    //    {
    //        if (from == null)
    //            return string.Empty;
    //        if (from.Length == 0)
    //            return string.Empty;

    //        try
    //        {
    //            if (mFormat == null)
    //                return from;
    //            else
    //                return String.Format(mFormat, from);

    //            //if (m_intMaxLength > 0)
    //            //    strRet = strRet.Substring(0, m_intMaxLength);
    //        }
    //        catch
    //        {
    //            throw new ConvertException(from, typeof(String), "TODO Extra Info");
    //        }

    //    }

    //    public override string FieldToString(object from)
    //    {
    //        if (from == null)
    //            return string.Empty;
    //        else
    //            return String.Format(mFormat, from);
    //    }
    //}

}