using System;
using System.Text;

namespace Visyn.Windows.Io.FileHelper
{
    /// <summary>
    /// Basic engine to read record by record
    /// </summary>
    public class VisynFileHelperEngine : VisynFileHelper<object>
    {
        #region "  Constructor  "

        /// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
        public VisynFileHelperEngine(Type recordType) : this(recordType, Encoding.Default) {}

        /// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
        /// <param name="recordType">The record mapping class.</param>
        /// <param name="encoding">The Encoding used by the engine.</param>
        public VisynFileHelperEngine(Type recordType, Encoding encoding) : base(recordType, encoding) {}

        #endregion
    }
}