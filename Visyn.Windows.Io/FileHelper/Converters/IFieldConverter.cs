using System;
using Visyn.Core.Types;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    public interface IFieldConverter : IType
    {
        bool CustomNullHandling { get; }

        string FieldToString(object from);
        object StringToField(string text);
    }
}