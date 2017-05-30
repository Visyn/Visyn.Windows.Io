using System;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    [Obsolete("Not used",true)]
    public static class ConverterExtensions
    {
        /// <summary>
        /// Throws a ConvertException with the passed values
        /// </summary>
        /// <param name="converter">IField converter instance</param>
        /// <param name="from">The source string.</param>
        /// <param name="errorMessage" >The custom error msg.</param>
        /// <exception cref="ConvertException">Throw exception with values</exception>
        public static void ThrowConvertException(this IFieldConverter converter, string from, string errorMessage)
        {
            throw new ConvertException(from, converter?.Type, errorMessage);
        }
    }
}
