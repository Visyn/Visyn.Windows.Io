#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
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

namespace Visyn.Windows.Io.FileHelper.Messages
{
    public class Messages
    {
        public class Errors
        {
            public static ErrorMessages.FieldOptionalClass FieldOptional { get; } = new ErrorMessages.FieldOptionalClass();

            public static ErrorMessages.InvalidIdentifierClass InvalidIdentifier { get; } = new ErrorMessages.InvalidIdentifierClass();

            public static ErrorMessages.EmptyClassNameClass EmptyClassName { get; } = new ErrorMessages.EmptyClassNameClass();

            public static ErrorMessages.EmptyFieldNameClass EmptyFieldName { get; } = new ErrorMessages.EmptyFieldNameClass();

            public static ErrorMessages.EmptyFieldTypeClass EmptyFieldType { get; } = new ErrorMessages.EmptyFieldTypeClass();

            public static ErrorMessages.ClassWithOutRecordAttributeClass ClassWithOutRecordAttribute { get;
            } = new ErrorMessages.ClassWithOutRecordAttributeClass();

            public static ErrorMessages.ClassWithOutDefaultConstructorClass ClassWithOutDefaultConstructor
            { get; } = new ErrorMessages.ClassWithOutDefaultConstructorClass();

            public static ErrorMessages.ClassWithOutFieldsClass ClassWithOutFields { get; } = new ErrorMessages.ClassWithOutFieldsClass();

            public static ErrorMessages.ExpectingFieldOptionalClass ExpectingFieldOptional { get; } = new ErrorMessages.ExpectingFieldOptionalClass();

            public static ErrorMessages.SameFieldOrderClass SameFieldOrder { get; } = new ErrorMessages.SameFieldOrderClass();

            public static ErrorMessages.PartialFieldOrderClass PartialFieldOrder { get; } = new ErrorMessages.PartialFieldOrderClass();

            public static ErrorMessages.PartialFieldOrderInAutoPropertyClass PartialFieldOrderInAutoProperty
            { get; } = new ErrorMessages.PartialFieldOrderInAutoPropertyClass();

            public static ErrorMessages.MissingFieldArrayLenghtInNotLastFieldClass MissingFieldArrayLenghtInNotLastField
            { get; } = new ErrorMessages.MissingFieldArrayLenghtInNotLastFieldClass();

            public static ErrorMessages.SameMinMaxLengthForArrayNotLastFieldClass SameMinMaxLengthForArrayNotLastField
            { get; } = new ErrorMessages.SameMinMaxLengthForArrayNotLastFieldClass();

            public static ErrorMessages.FieldNotFoundClass FieldNotFound { get; } = new ErrorMessages.FieldNotFoundClass();

            public static ErrorMessages.WrongConverterClass WrongConverter { get; } = new ErrorMessages.WrongConverterClass();

            public static ErrorMessages.NullRecordClassClass NullRecordClass { get; } = new ErrorMessages.NullRecordClassClass();

            public static ErrorMessages.StructRecordClassClass StructRecordClass { get; } = new ErrorMessages.StructRecordClassClass();

            public static ErrorMessages.TestQuoteClass TestQuote { get; } = new ErrorMessages.TestQuoteClass();

            public static ErrorMessages.MixOfStandardAndAutoPropertiesFieldsClass MixOfStandardAndAutoPropertiesFields
            { get; } = new ErrorMessages.MixOfStandardAndAutoPropertiesFieldsClass();
        }
    }
}
