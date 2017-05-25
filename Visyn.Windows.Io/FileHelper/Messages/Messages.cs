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
