using System.ComponentModel;

namespace Visyn.Windows.Io.FileHelper.Events
{
    /// <summary>Base class of <see cref="BeforeWriteEventArgs{T}"/> and <see cref="AfterWriteEventArgs{T}"/></summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class WriteEventArgs : FileHelpersEventArgs
    {
        /// <summary>
        /// Write events are based on this
        /// </summary>
        /// <param name="engine">Engine parsing data</param>
        /// <param name="lineNumber">Record number</param>
        internal WriteEventArgs(IFileHelperEngine engine, int lineNumber)
            : base(engine, lineNumber)
        {
        }

    }
}