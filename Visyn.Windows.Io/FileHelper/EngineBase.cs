using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FileHelpers;
using FileHelpers.Events;
using FileHelpers.Options;

//using Container=FileHelpers.Container;

namespace Visyn.Windows.Io.FileHelper
{
    /// <summary>Abstract Base class for the engines of the library: 
    /// <see cref="FileHelperEngine"/> and 
    /// <see cref="FileHelperAsyncEngine"/></summary>
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
                throw new BadUsageException(Messages.Errors.NullRecordClass.Text);

            if (recordType.IsValueType) {
                throw new BadUsageException(Messages.Errors.StructRecordClass
                    .RecordType(recordType.Name)
                    .Text);
            }

            mRecordType = recordType;
            RecordInfo = FileHelpers.RecordInfo.Resolve(recordType); // Container.Resolve<IRecordInfo>(recordType);
            Encoding = encoding;

            CreateRecordOptions();
        }

        /// <summary>
        /// Create an engine on the record info provided
        /// </summary>
        /// <param name="ri">Record information</param>
        protected EngineBase(IRecordInfo ri)
        {
            mRecordType = ri.RecordType;
            RecordInfo = ri;

            CreateRecordOptions();
        }

        #endregion

        #region "  LineNumber  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int _lineNumber;



        /// <include file='FileHelperEngine.docs.xml' path='doc/LineNum/*'/>
        public int LineNumber
        {
            get { return _lineNumber; }
            protected set { _lineNumber = value; }
        }

        #endregion

        #region "  TotalRecords  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int _totalRecords;

        /// <include file='FileHelperEngine.docs.xml' path='doc/TotalRecords/*'/>
        public int TotalRecords
        {
            get { return _totalRecords; }
            protected set { _totalRecords = value; }
        }

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

        #region "  RecordType  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type mRecordType;

        /// <include file='FileHelperEngine.docs.xml' path='doc/RecordType/*'/>
        public Type RecordType => mRecordType;

        #endregion

        #region "  HeaderText  "


        /// <summary>The Read Header in the last Read operation. If any.</summary>
        public string HeaderText { get; set; }

        #endregion

        #region "  FooterText"


        /// <summary>The Read Footer in the last Read operation. If any.</summary>
        public string FooterText { get; set; }

        #endregion

        #region "  Encoding  "

        /// <summary>
        /// The encoding to Read and Write the streams. 
        /// Default is the system's current ANSI code page.
        /// </summary>
        /// <value>Default is the system's current ANSI code page.</value>
        public Encoding Encoding { get; set; }

        #endregion

        #region "  NewLineForWrite  "
        /// <summary>
        /// Newline string to be used when engine writes to file. 
        /// Default is the system's newline setting (System.Environment.NewLine).
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string mNewLineForWrite = Environment.NewLine;

        /// <summary>
        /// Newline string to be used when engine writes to file. 
        /// Default is the system's newline setting (System.Environment.NewLine).
        /// </summary>
        /// <value>Default is the system's newline setting.</value>
        public string NewLineForWrite
        {
            get { return mNewLineForWrite; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("NewLine string must not be null or empty");
                mNewLineForWrite = value;
            }
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
        ///   <seealso cref="FileHelpers.ErrorManager"/>
        public ErrorManager ErrorManager => _errorManager;


        /// <summary>
        /// Indicates the behavior of the engine when it finds an error.
        /// {Shortcut for <seealso cref="FileHelpers.ErrorManager.ErrorMode"/>)
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
            _lineNumber = 0;
            _errorManager.ClearErrors();
            _totalRecords = 0;
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