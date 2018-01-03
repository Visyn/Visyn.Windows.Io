#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Visyn.Exceptions;
using Visyn.Serialize;
using Visyn.Serialize.Converters;
using Visyn.Types;

/*
namespace Visyn.Windows.Io.FileHelper.Attributes
{
    /// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">Complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldConverterAttribute : Attribute
    {
        #region "  Constructors  "

        /// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations. </summary>
        /// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
        public FieldConverterAttribute(ConverterKind converter)
            : this(converter, new string[] {}) {}

        /// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations. </summary>
        /// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(ConverterKind converter, string arg1)
            : this(converter, new string[] {arg1}) {}

        /// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations. </summary>
        /// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        /// <param name="arg2">The second param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(ConverterKind converter, string arg1, string arg2)
            : this(converter, new string[] {arg1, arg2}) {}

        /// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations. </summary>
        /// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        /// <param name="arg2">The second param passed directly to the Converter Constructor.</param>
        /// <param name="arg3">The third param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(ConverterKind converter, string arg1, string arg2, string arg3)
            : this(converter, new string[] {arg1, arg2, arg3}) {}


        /// <summary>
        /// Indicates the <see cref="ConverterKind"/> used for read/write operations. 
        /// </summary>
        /// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
        /// <param name="args">An array of parameters passed directly to the Converter</param>
        private FieldConverterAttribute(ConverterKind converter, params string[] args)
        {
            Kind = converter;

            Type convType;

            switch (converter)
            {
                case ConverterKind.Date:
                    convType = typeof (DateTimeConverter);
                    break;

                case ConverterKind.DateMultiFormat:
                    convType = typeof (DateTimeMultiFormatConverter);
                    break;

                case ConverterKind.Byte:
                    convType = typeof (ByteConverter);
                    break;

                case ConverterKind.SByte:
                    convType = typeof (SByteConverter);
                    break;

                case ConverterKind.Int16:
                    convType = typeof (Int16Converter);
                    break;
                case ConverterKind.Int32:
                    convType = typeof (Int32Converter);
                    break;
                case ConverterKind.Int64:
                    convType = typeof (Int64Converter);
                    break;

                case ConverterKind.UInt16:
                    convType = typeof (UInt16Converter);
                    break;
                case ConverterKind.UInt32:
                    convType = typeof (UInt32Converter);
                    break;
                case ConverterKind.UInt64:
                    convType = typeof (UInt64Converter);
                    break;

                case ConverterKind.Decimal:
                    convType = typeof (DecimalConverter);
                    break;
                case ConverterKind.Double:
                    convType = typeof (DoubleConverter);
                    break;
                case ConverterKind.PercentDouble:
                    convType = typeof (PercentDoubleConverter);
                    break;
                case ConverterKind.Single:
                    convType = typeof (SingleConverter);
                    break;
                case ConverterKind.Boolean:
                    convType = typeof (BooleanConverter);
                    break;
                    // Added by Alexander Obolonkov 2007.11.08
                case ConverterKind.Char:
                    convType = typeof (CharConverter);
                    break;
                    // Added by Alexander Obolonkov 2007.11.08
                case ConverterKind.Guid:
                    convType = typeof (GuidConverter);
                    break;
                default:
                    throw new BadUsageException($"Converter '{ converter}' not found, you must specify a valid converter.");
            }

            CreateConverter(convType, args);
        }

        /// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
        /// <param name="customConverter">The Type of your custom converter.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(Type customConverter, string arg1)
            : this(customConverter, new object[] {arg1}) {}

        /// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
        /// <param name="customConverter">The Type of your custom converter.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        /// <param name="arg2">The second param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(Type customConverter, string arg1, string arg2)
            : this(customConverter, new object[] {arg1, arg2}) {}

        /// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
        /// <param name="customConverter">The Type of your custom converter.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        /// <param name="arg2">The second param passed directly to the Converter Constructor.</param>
        /// <param name="arg3">The third param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(Type customConverter, string arg1, string arg2, string arg3)
            : this(customConverter, new object[] {arg1, arg2, arg3}) {}

        /// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
        /// <param name="customConverter">The Type of your custom converter.</param>
        /// <param name="args">A list of params passed directly to your converter constructor.</param>
        public FieldConverterAttribute(Type customConverter, params object[] args)
        {
            CreateConverter(customConverter, args);
        }

        /// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
        /// <param name="customConverter">The Type of your custom converter.</param>
        public FieldConverterAttribute(Type customConverter)
        {
            CreateConverter(customConverter, new object[] {});
        }

        #endregion

        #region "  Converter  "


        /// <summary>The final concrete converter used for FieldToString and StringToField operations </summary>
        public IFieldConverter Converter { get; private set; }

        /// <summary>The <see cref="ConverterKind"/> if a default converter is used </summary>
        public ConverterKind Kind { get; private set; }

        #endregion
         
        #region "  CreateConverter  "

        private void CreateConverter(Type convType, object[] args)
        {
            if (typeof (IFieldConverter).IsAssignableFrom(convType))
            {
                var constructor = convType.GetConstructor(
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    ArgsToTypes(args).ToArray(),
                    null);

                if (constructor == null)
                {
                    if (args.Length == 0) 
                        throw new BadUsageException($"Empty constructor for converter: {convType.Name} was not found. You must add a constructor without args (can be public or private)");
                    
                    throw new BadUsageException($"Constructor for converter: {convType.Name} with these arguments: ({ ArgsDesc(args)}) was not found. You must add a constructor with this signature (can be public or private)");
                }

                try
                {
                    Converter = (IFieldConverter)constructor.Invoke(args) ;
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
            }
            else if (convType.IsEnum)
                Converter = new EnumConverter(convType);

            else
                throw new BadUsageException("The custom converter must inherit from ConverterBase");
        }

        #endregion

        #region "  ArgsToTypes  "

        private static IList<Type> ArgsToTypes(IEnumerable<object> args)
        {
            if (args == null)  throw new BadUsageException( "The args to the constructor can be null, if you do not want to pass anything into them.");

            return new List<Type>(args.Select(arg => arg?.GetType() ?? typeof(object)));
        }

        private static string ArgsDesc(IEnumerable<object> args)
        {
            return string.Join(", ",args.Select(DisplayType));
            //var res = DisplayType(args[0]);

            //for (var i = 1; i < args.Length; i++)
            //    res += ", " + DisplayType(args[i]);

            //return res;
        }

        private static string DisplayType(object o) => o?.GetType().Name ?? "Object";

        #endregion

        internal void ValidateTypes(FieldInfo fi)
        {
            var valid = false;

            var fieldType = fi.FieldType;

            if (fieldType.IsValueType &&
                fieldType.IsGenericType &&
                fieldType.GetGenericTypeDefinition() == typeof (Nullable<>))
                fieldType = fieldType.GetGenericArguments()[0];

            switch (Kind) {
                case ConverterKind.None:
                    valid = true;
                    break;

                case ConverterKind.Date:
                case ConverterKind.DateMultiFormat:
                    valid = typeof (DateTime) == fieldType;
                    break;

                case ConverterKind.Byte:
                case ConverterKind.SByte:
                case ConverterKind.Int16:
                case ConverterKind.Int32:
                case ConverterKind.Int64:
                case ConverterKind.UInt16:
                case ConverterKind.UInt32:
                case ConverterKind.UInt64:
                case ConverterKind.Decimal:
                case ConverterKind.Double:
                case ConverterKind.Single:
                case ConverterKind.Boolean:
                case ConverterKind.Char:
                case ConverterKind.Guid:
                    valid = Kind.ToString() == fieldType.UnderlyingSystemType.Name;
                    break;
                case ConverterKind.PercentDouble:
                    valid = typeof (double) == fieldType;
                    break;
            }

            if (valid == false) {
                throw new BadUsageException(
                    "The converter of the field: '" + fi.Name + "' is wrong. The field is of Type: " + fieldType.Name +
                    " and the converter is for type: " + Kind);
            }
        }
    }
    
}*/
