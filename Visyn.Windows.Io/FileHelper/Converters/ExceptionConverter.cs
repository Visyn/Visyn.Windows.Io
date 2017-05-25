using System;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Converter exception
    /// </summary>
    public class ExceptionConverter : ConverterBase<ExceptionConverter>
    {
        /// <summary>
        /// Convert a field definition to a string
        /// </summary>
        /// <param name="exception">Convert exception object</param>
        /// <returns>Field as a string or null</returns>
        public override string FieldToString(object exception)
        {
            if (exception == null) return string.Empty;
            var f = exception as ConvertException;
            if (f != null)
            {
                return $"In the field '{ f.FieldName }': {f.Message.Replace(Environment.NewLine, " -> ")}";
            }
            return ((Exception)exception).Message.Replace(Environment.NewLine, " -> ");
        }

        /// <summary>
        /// Convert a general exception to a string
        /// </summary>
        /// <param name="text">exception to convert</param>
        /// <returns>Exception from field</returns>
        public override object StringToField(string text)
        {
            return new Exception(text);
        }
    }
}