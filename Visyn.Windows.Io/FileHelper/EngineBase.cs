using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Visyn.Windows.Io.Exceptions;
using Visyn.Windows.Io.FileHelper.Enums;
using Visyn.Windows.Io.FileHelper.Core;
using Visyn.Windows.Io.FileHelper.Options;
using Visyn.Windows.Io.FileHelper.Events;

namespace Visyn.Windows.Io.FileHelper
{
    /// <summary>Abstract Base class for the engines of the library: 
    /// <see cref="VisynFileHelperEngine"/> and 
    /// <see cref="FileHelpers.FileHelperAsyncEngine{T}"/></summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class EngineBase
    {
        // The default is 4k we use 16k
        internal const int DefaultReadBufferSize = 16 * 1024;
        internal const int DefaultWriteBufferSize = 16 * 1024;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal IRecordInfo RecordInfo { get; private set; }

        //private readonly IRecordInfo mRecordInfo;

        #region "  Constructor  "

        /// <summary>
        /// Create an engine on record type, with default encoding
        /// </summary>
        /// <param name="recordType">Class to base engine on</param>
        protected EngineBase(Type recordType) : this(recordType, Encoding.Default) {}

        /// <summary>
        /// Create and engine on type with specified encoding
        /// </summary>
        /// <param name="recordType">Class to base engine on</param>
        /// <param name="encoding">encoding of the file</param>
        protected EngineBase(Type recordType, Encoding encoding)
        {
            if (recordType == null)
                throw new BadUsageException(Messages.Messages.Errors.NullRecordClass.Text);

            if (recordType.IsValueType) {
                throw new BadUsageException(Messages.Messages.Errors.StructRecordClass
                    .RecordType(recordType.Name)
                    .Text);
            }

            RecordType = recordType;
            RecordInfo = VisynRecordInfo.Resolve(recordType); // Container.Resolve<IRecordInfo>(recordType);
            Encoding = encoding;

            CreateRecordOptions();
        }

        /// <summary>
        /// Create an engine on the record info provided
        /// </summary>
        /// <param name="ri">Record information</param>
        protected EngineBase(IRecordInfo ri)
        {
            RecordType = ri.RecordType;
            RecordInfo = ri;

            CreateRecordOptions();
        }

        #endregion

        #region "  LineNumber  "

        /// <include file='FileHelperEngine.docs.xml' path='doc/LineNum/*'/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int LineNumber { get; protected set; }

        #endregion

        #region "  TotalRecords  "

        /// <include file='FileHelperEngine.docs.xml' path='doc/TotalRecords/*'/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int TotalRecords { get; protected set; }

        #endregion

        /// <summary>
        /// Builds a line with the name of the fields, for a delimited files it
        /// uses the same delimiter, for a fixed length field it writes the
        /// fields names separated with tabs
        /// </summary>
        /// <returns>field names structured for the heading of the file</returns>
        public string GetFileHeader()
        {
            var delimiter = "\t";

            if (RecordInfo.IsDelimited)
                delimiter = ((DelimitedRecordOptions) Options).Delimiter;

            var res = new StringBuilder();
            for (var i = 0; i < RecordInfo.Fields.Length; i++) {
                if (i > 0)
                    res.Append(delimiter);

                var field = RecordInfo.Fields[i];
                res.Append(field.FieldCaption ?? field.FieldFriendlyName);
            }

            return res.ToString();
        }


        /// <include file='FileHelperEngine.docs.xml' path='doc/RecordType/*'/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Type RecordType { get; }

        /// <summary>The Read Header in the last Read operation. If any.</summary>
        public string HeaderText { get; set; }

        /// <summary>The Read Footer in the last Read operation. If any.</summary>
        public string FooterText { get; set; }
        

        /// <summary>
        /// The encoding to Read and Write the streams. 
        /// Default is the system's current ANSI code page.
        /// </summary>
        /// <value>Default is the system's current ANSI code page.</value>
        public Encoding Encoding
        { get;
            set; }


        #region "  NewLineForWrite  "
        /// <summary>
        /// Newline string to be used when engine writes to file. 
        /// Default is the system's newline setting (System.Environment.NewLine).
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [Obsolete("Backing field, do not use...")]
        private string _newLineForWrite = Environment.NewLine;

        /// <summary>
        /// Newline string to be used when engine writes to file. 
        /// Default is the system's newline setting (System.Environment.NewLine).
        /// </summary>
        /// <value>Default is the system's newline setting.</value>
        public string NewLineForWrite
        {
#pragma warning disable 618
            get { return _newLineForWrite; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("NewLine string must not be null or empty");
                _newLineForWrite = value;
            }
#pragma warning restore 618
        }

        #endregion

        #region "  ErrorManager"

        /// <summary>This is a common class that manage the errors of the library.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ErrorManager _errorManager = new ErrorManager();

        /// <summary>This is a common class that manages the errors of the library.</summary>
        /// <remarks>
        ///   You can find complete information about the errors encountered while processing.
        ///   For example, you can get the errors, their number and save them to a file, etc.
        ///   </remarks>
        ///   <seealso cref="Exceptions.ErrorManager"/>
        public ErrorManager ErrorManager => _errorManager;


        /// <summary>
        /// Indicates the behavior of the engine when it finds an error.
        /// {Shortcut for <seealso cref="Exceptions.ErrorManager.ErrorMode"/>)
        /// </summary>
        public ErrorMode ErrorMode
        {
            get { return _errorManager.ErrorMode; }
            set { _errorManager.ErrorMode = value; }
        }

        #endregion

        #region "  ResetFields  "

        /// <summary>
        /// Reset back to the beginning
        /// </summary>
        internal void ResetFields()
        {
            LineNumber = 0;
            _errorManager.ClearErrors();
            TotalRecords = 0;
        }

        #endregion


        /// <summary>Event handler called to notify progress.</summary>
        public event EventHandler<ProgressEventArgs> Progress;

        /// <summary>
        /// Determine whether a progress call is needed
        /// </summary>
        protected bool MustNotifyProgress => Progress != null;

        /// <summary>
        /// Raises the Progress Event
        /// </summary>
        /// <param name="e">The Event Args</param>
        protected void OnProgress(ProgressEventArgs e)
        {
            Progress?.Invoke(this, e);
        }


        private void CreateRecordOptions()
        {
            Options = CreateRecordOptionsCore(RecordInfo);
        }

        internal static RecordOptions CreateRecordOptionsCore(IRecordInfo info)
        {
            var options = info.IsDelimited ?  new DelimitedRecordOptions(info) : new FixedRecordOptions(info) as RecordOptions;

            var i = 0;
            foreach (var field in options.Fields)
            {
                field.Parent = options;
                field.ParentIndex = i++;
            }
            return options;
        }


        /// <summary>
        /// Allows you to change some record layout options at runtime
        /// </summary>
        public RecordOptions Options { get; private set; }
    }
}