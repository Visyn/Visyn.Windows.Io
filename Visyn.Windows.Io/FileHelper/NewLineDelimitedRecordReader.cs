using System;
using System.IO;
using FileHelpers;

namespace Visyn.Windows.Io.FileHelper
{
    /// <summary>
    /// Read a record that is delimited by a newline
    /// </summary>
    public sealed class TextReaderWrapper : IRecordReader, IDisposable
    {
        private readonly TextReader _reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextReaderWrapper"/> class.
        /// </summary>
        /// <param name="reader">The text reader to use.</param>
        public TextReaderWrapper(TextReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        /// Read a record from the data source
        /// </summary>
        /// <returns>A single record for parsing</returns>
        public string ReadRecordString() => _reader.ReadLine();

        /// <summary>
        /// Close the reader
        /// </summary>
        public void Close()
        {
            _reader.Close();
        }

        #region IDisposable

        public void Dispose()
        {
            _reader?.Dispose();
        }

        #endregion
    }
}