using System;
using System.Text;

namespace Visyn.Windows.Io.FileHelper.Core
{
    public sealed class ForwardReader : IDisposable
    {
        /// <summary>
        /// Return file record by record
        /// </summary>
        private readonly IRecordReader _reader;

        /// <summary>
        /// records already read
        /// </summary>
        private readonly string[] _fowardStrings;

        private int _forwardIndex = 0;

        #region "  Constructors  "

        /// <summary>
        /// Read a Record handler forward,  optionally skipping n lines and starting at a record number > 0
        /// </summary>
        /// <param name="reader">Reader to get records</param>
        /// <param name="forwardLines">NUmber of lines to skip before reading</param>
        public ForwardReader(IRecordReader reader, int forwardLines)
            : this(reader, forwardLines, 0) {}

        /// <summary>
        /// Read a Record handler forward,  optionally skipping n lines and starting at a record number > 0
        /// </summary>
        /// <param name="reader">Reader to get records</param>
        /// <param name="forwardLines">Number of lines to skip before reading</param>
        /// <param name="startLine">Lines already read from file</param>
        public ForwardReader(IRecordReader reader, int forwardLines, int startLine)
        {
            _reader = reader;

            FowardLines = forwardLines;
            _lineNumber = startLine;

            _fowardStrings = new string[FowardLines + 1];
            RemainingLines = FowardLines + 1;

            for (var i = 0; i < FowardLines + 1; i++)
            {
                _fowardStrings[i] = _reader.ReadRecordString();
                _lineNumber++;
                if (_fowardStrings[i] != null) continue;
                RemainingLines = i;
                break;
            }
        }

        #endregion


        /// <summary>
        /// Number of lines in the buffer
        /// </summary>
        public int RemainingLines { get; private set; } = 0;


        private int _lineNumber = 0;

        /// <summary>
        /// Record number within the file - normally the line number
        /// </summary>
        public int LineNumber => _lineNumber - 1 - FowardLines;


        //		
        //		int mPos = 0;
        //		int MaxRecordSize = 1024 * 8;
        //		char[] mBuffer;
        //	



        public bool DiscardForward { get; set; }

        public int FowardLines { get; } = 0;



        #region "  ReadNextLine  "

        public string ReadNextLine()
        {
            if (RemainingLines <= 0) return null;

            var res = _fowardStrings[_forwardIndex];

            if (RemainingLines == (FowardLines + 1))
            {
                _fowardStrings[_forwardIndex] = _reader.ReadRecordString();
                _lineNumber++;

                if (_fowardStrings[_forwardIndex] == null) RemainingLines--;
            }
            else
            {
                RemainingLines--;
                if (DiscardForward) return null;
            }

            _forwardIndex = (_forwardIndex + 1)%(FowardLines + 1);

            return res;
        }

        #endregion

        #region "  RemainingText  "

        public string RemainingText
        {
            get
            {
                var sb = new StringBuilder(100);

                for (var i = 0; i < RemainingLines + 1; i++)
                    sb.Append(_fowardStrings[(_forwardIndex + i)%(FowardLines + 1)] + Environment.NewLine);

                return sb.ToString();
            }
        }

        #endregion

        #region "  Close  "

        /// <summary>
        /// Close the record reader, which should in turn close the stream
        /// </summary>
        public void Close()
        {
            _reader?.Close();
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}