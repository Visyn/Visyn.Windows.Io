
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Enums
{
    /// <summary>Indicates the behavior when the engine classes like <see cref="FileHelperEngine"/> class found an error.</summary>
    public enum ErrorMode
    {
        /// <summary>Default value, this simply Rethrow the original exception.</summary>
        ThrowException = 0,

        /// <summary>Add an <see cref="Exceptions.ErrorInfo"/> to the array of <see cref="Exceptions.ErrorManager.Errors"/>.</summary>
        SaveAndContinue,

        /// <summary>Simply ignores the exception and continues</summary>
        IgnoreAndContinue
    }
}