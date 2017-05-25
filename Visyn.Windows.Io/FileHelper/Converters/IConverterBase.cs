namespace Visyn.Windows.Io.FileHelper.Converters
{
    public interface IConverter
    {
        string FieldToString(object from);
        object StringToField(string from);
    }
}