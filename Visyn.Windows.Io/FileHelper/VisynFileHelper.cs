using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using FileHelpers;
using FileHelpers.Events;

namespace Visyn.Windows.Io.FileHelper
{
    /// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngine/*'/>
    /// <include file='Examples.xml' path='doc/examples/FileHelperEngine/*'/>
    /// <typeparam name="T">The record type.</typeparam>
    [DebuggerDisplay("FileHelperEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
    public class VisynFileHelper<T> : EventEngineBase<T>, IFileHelperEngine<T>
        where T : class
    {
        internal const int DefaultReadBufferSize = 16 * 1024;
        internal const int DefaultWriteBufferSize = 16 * 1024;

        private readonly bool mObjectEngine = typeof(T) == typeof(object);

        #region "  Constructor  "

        public VisynFileHelper() : this(Encoding.Default) { }

        public VisynFileHelper(Encoding encoding) : base(typeof(T), encoding) { }

        /// <summary>
        /// Define an event based on a record with a specific encoding
        /// </summary>
        /// <param name="recordType">Type of record we are reading</param>
        /// <param name="encoding">The Encoding used by the engine.</param>
        /// <include file="FileHelperEngine.docs.xml" path="doc/FileHelperEngineCtr/*" />
        protected VisynFileHelper(Type recordType, Encoding encoding) : base(recordType, encoding) { }

#if false   // internal
        /// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
        /// <param name="ri">Record information</param>
        internal VisynFileHelper(RecordInfo ri) : base(ri) { }
#endif
        #endregion

        #region "  ReadFile  "

        /// <summary>
        /// Reads the file from the specified path.
        /// </summary>
        /// <param name="fileName">Path of the file to read.</param>
        /// <returns>T[].</returns>
        /// <include file="FileHelperEngine.docs.xml" path="doc/ReadFile/*" />
        public override T[] ReadFile(string fileName) => ReadFile(fileName, int.MaxValue);

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="fileName">Path of the file to read.</param>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        /// <returns>T[].</returns>
        public override T[] ReadFile(string fileName, int maxRecords)
        {
            using (var fs = new InternalStreamReader(fileName, Encoding, true, DefaultReadBufferSize))
            {
                var tempRes = ReadStream(fs, maxRecords);
                fs.Close();

                return tempRes;
            }
        }

     

#endregion

#region "  ReadStream  "

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadStream/*'/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override T[] ReadStream(TextReader reader) => ReadStream(reader, int.MaxValue);


        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadStream/*'/>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override T[] ReadStream(TextReader reader, int maxRecords)
        {
            var result = ReadStreamAsList(reader, maxRecords, null);

            if (mObjectEngine) return (T[])((ArrayList)result).ToArray(RecordInfo.RecordType);
            return ((List<T>)result).ToArray();
        }

    

        private IList ReadStreamAsList(TextReader reader, int maxRecords, DataTable dt)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader), "The reader of the Stream can´t be null");
     
            HeaderText = string.Empty;
            FooterText = string.Empty;

            IList result;

            using (var recordReader = new TextReaderWrapper(reader))
            {
                ResetFields();


                if (mObjectEngine)
                    result = new ArrayList();
                else
                    result = new List<T>();

                var currentRecord = 0;

                var streamInfo = new StreamInfoProvider(reader);
                using (var freader = new ForwardReader(recordReader, RecordInfo.IgnoreLast))
                {
                    freader.DiscardForward = true;

                    string currentLine, completeLine;

                    LineNumber = 1;

                    completeLine = freader.ReadNextLine();
                    currentLine = completeLine;

                    if (MustNotifyProgress) // Avoid object creation
                        OnProgress(new ProgressEventArgs(0, -1, streamInfo.Position, streamInfo.TotalBytes));

                    if (RecordInfo.IgnoreFirst > 0)
                    {
                        for (var i = 0; i < RecordInfo.IgnoreFirst && currentLine != null; i++)
                        {
                            HeaderText += currentLine + Environment.NewLine;
                            currentLine = freader.ReadNextLine();
                            LineNumber++;
                        }
                    }

                    var byPass = false;

                    if (maxRecords < 0)
                        maxRecords = int.MaxValue;

                    var line = new LineInfo(currentLine)
                    {
                        mReader = freader
                    };

                    var values = new object[RecordInfo.FieldCount];

                    while (currentLine != null &&
                           currentRecord < maxRecords)
                    {

                        completeLine = currentLine;

                        try
                        {
                            TotalRecords++;
                            currentRecord++;

                            line.ReLoad(currentLine);

                            var skip = false;

                            var record = (T) RecordInfo.Operations.CreateRecordHandler();

                            if (MustNotifyProgress) // Avoid object creation
                            {
                                OnProgress(new ProgressEventArgs(currentRecord,
                                    -1,
                                    streamInfo.Position,
                                    streamInfo.TotalBytes));
                            }

                            BeforeReadEventArgs<T> e = null;
                            if (MustNotifyRead)
                            {
                                e = new BeforeReadEventArgs<T>(this, record, currentLine, LineNumber);
                                skip = OnBeforeReadRecord(e);
                                if (e.RecordLineChanged)
                                    line.ReLoad(e.RecordLine);
                            }


                            if (skip == false)
                            {
                                if (RecordInfo.Operations.StringToRecord(record, line, values))
                                {

                                    if (MustNotifyRead) // Avoid object creation
                                        skip = OnAfterReadRecord(currentLine, record, e.RecordLineChanged, LineNumber);

                                    if (skip == false)
                                    {

                                        if (dt == null)
                                            result.Add(record);
                                        else
                                            dt.Rows.Add(RecordInfo.Operations.RecordToValues(record));
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            switch (ErrorManager.ErrorMode)
                            {
                                case ErrorMode.ThrowException:
                                    byPass = true;
                                    throw;
                                case ErrorMode.IgnoreAndContinue:
                                    break;
                                case ErrorMode.SaveAndContinue:
                                    var err = new ErrorInfo
                                    {
                                        LineNumber = freader.LineNumber,
                                        ExceptionInfo = ex,
                                        RecordString = completeLine
                                    };

                                    ErrorManager.AddError(err);
                                    break;
                            }
                        }
                        finally
                        {
                            if (byPass == false)
                            {
                                currentLine = freader.ReadNextLine();
                                LineNumber++;
                            }
                        }
                    }

                    if (RecordInfo.IgnoreLast > 0)
                        FooterText = freader.RemainingText;
                }
            }
            return result;
        }

        #endregion

        #region "  ReadString  "

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadString/*'/>
        public override T[] ReadString(string source) => ReadString(source, int.MaxValue);

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadString/*'/>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        public override T[] ReadString(string source, int maxRecords)
        {
            if (source == null) source = string.Empty;

            using (var reader = new InternalStringReader(source))
            {
                var res = ReadStream(reader, maxRecords);
                reader.Close();
                return res;
            }
        }

        #endregion

        #region "  WriteFile  "

        public override void WriteFile(string fileName, IEnumerable<T> records)
        {
            WriteFile(fileName,records,int.MaxValue);
        }

        /// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile2/*'/>
        public override void WriteFile(string fileName, IEnumerable<T> records, int maxRecords)
        {
            using (var fs = new StreamWriter(fileName, false, Encoding, DefaultWriteBufferSize))
            {
                WriteStream(fs, records, maxRecords);
                fs.Close();
            }
        }

        #endregion

        #region "  WriteStream  "

        public override void WriteStream(TextWriter writer, IEnumerable<T> records)
        {
            WriteStream(writer, records, int.MaxValue);
        }

        /// <summary>
        /// Writes the stream.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="records">The records.</param>
        /// <param name="maxRecords">The maximum records.</param>
        /// <exception cref="System.ArgumentNullException">
        /// writer - The writer of the Stream can be null
        /// or
        /// records - The records can be null. Try with an empty array.
        /// </exception>
        /// <exception cref="BadUsageException">
        /// This engine works with record of type " +
        ///                                                         RecordInfo.RecordType.Name + " and you use records of type " +
        ///                                                         rec.GetType().Name
        /// </exception>
        /// <include file="FileHelperEngine.docs.xml" path="doc/WriteStream2/*" />
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void WriteStream(TextWriter writer, IEnumerable<T> records, int maxRecords)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer), "The writer of the Stream can be null");

            if (records == null) throw new ArgumentNullException(nameof(records), "The records can be null. Try with an empty array.");

            ResetFields();

            writer.NewLine = NewLineForWrite;

            if (!string.IsNullOrEmpty(HeaderText))
            {
                if (HeaderText.EndsWith(NewLineForWrite)) writer.Write(HeaderText);
                else  writer.WriteLine(HeaderText);
            }

            var max = records is IList ? Math.Min(((IList)records).Count, maxRecords) : maxRecords;

            if (MustNotifyProgress) // Avoid object creation
                OnProgress(new ProgressEventArgs(0, max));

            var recIndex = 0;

            var first = true;
            string currentLine = null;
            foreach (var rec in records)
            {
                if (recIndex == maxRecords)
                    break;

                LineNumber++;
                try
                {
                    if (rec == null) throw new BadUsageException($"The record at index {recIndex} is null.");

                    if (first)
                    {
                        first = false;
                        if (RecordInfo.RecordType.IsInstanceOfType(rec) == false)
                        {
                            throw new BadUsageException($"This engine works with record of type {RecordInfo.RecordType.Name} and you use records of type {rec.GetType().Name}");
                        }
                    }

                    var skip = false;

                    if (MustNotifyProgress) // Avoid object creation
                        OnProgress(new ProgressEventArgs(recIndex + 1, max));

                    if (MustNotifyWrite)
                        skip = OnBeforeWriteRecord(rec, LineNumber);

                    if (skip == false)
                    {
                        currentLine = RecordInfo.Operations.RecordToString(rec);
                        if (MustNotifyWrite)
                            currentLine = OnAfterWriteRecord(currentLine, rec);
                        writer.WriteLine(currentLine);
                    }
                }
                catch (Exception ex)
                {
                    switch (ErrorManager.ErrorMode)
                    {
                        case ErrorMode.ThrowException:
                            throw;
                        case ErrorMode.IgnoreAndContinue:
                            break;
                        case ErrorMode.SaveAndContinue:
                            var err = new ErrorInfo
                            {
                                LineNumber = LineNumber,
                                ExceptionInfo = ex,
                                RecordString = currentLine
                            };
                            //							err.mColumnNumber = mColumnNum;
                            ErrorManager.AddError(err);
                            break;
                    }
                }
                recIndex++;
            }

            TotalRecords = recIndex;

            if (string.IsNullOrEmpty(FooterText)) return;
            if (FooterText.EndsWith(NewLineForWrite)) writer.Write(FooterText);
            else writer.WriteLine(FooterText);
        }

        #endregion

        #region "  WriteString  "


        public override string WriteString(IEnumerable<T> records) => WriteString(records, int.MaxValue);

        public override string WriteString(IEnumerable<T> records, int maxRecords)
        {
            using (var writer = new StringWriter(new StringBuilder()))
            {
                WriteStream(writer, records, maxRecords);
                return writer.ToString();
            }
        }

        #endregion

        #region "  AppendToFile  "

        public override void AppendToFile(string fileName, T record)
        {
            AppendToFile(fileName, new T[] { record });
        }

        /// <include file='FileHelperEngine.docs.xml' path='doc/AppendToFile2/*'/>
        public override void AppendToFile(string fileName, IEnumerable<T> records)
        {
            using (
                var writer = StreamHelper.CreateFileAppender(fileName,
                    Encoding,
                    true,
                    false,
                    DefaultWriteBufferSize))
            {
                HeaderText = string.Empty;
                FooterText = string.Empty;

                WriteStream(writer, records);
                writer.Close();
            }
        }


        #endregion

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void WriteStreamMulti(TextWriter writer, IEnumerable<T> records, int maxRecords)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer), "The writer of the Stream can be null");

            if (records == null) throw new ArgumentNullException(nameof(records), "The records can be null. Try with an empty array.");

            ResetFields();

            writer.NewLine = NewLineForWrite;

            if (!string.IsNullOrEmpty(HeaderText))
            {
                if (HeaderText.EndsWith(NewLineForWrite)) writer.Write(HeaderText);
                else writer.WriteLine(HeaderText);
            }

            var max = records is IList ? Math.Min(((IList)records).Count, maxRecords) : maxRecords;

            if (MustNotifyProgress) // Avoid object creation
                OnProgress(new ProgressEventArgs(0, max));

            var recIndex = 0;

            var first = true;
            string currentLine = null;
            foreach (var rec in records)
            {
                if (recIndex == maxRecords) break;

                LineNumber++;
                try
                {
                    if (rec == null) throw new BadUsageException($"The record at index {recIndex} is null.");

                    if (first)
                    {
                        first = false;
                        if (RecordInfo.RecordType.IsInstanceOfType(rec) == false)
                        {
                            throw new BadUsageException($"This engine works with record of type {RecordInfo.RecordType.Name} and you use records of type {rec.GetType().Name}");
                        }
                    }

                    var skip = false;

                    if (MustNotifyProgress) // Avoid object creation
                        OnProgress(new ProgressEventArgs(recIndex + 1, max));

                    if (MustNotifyWrite)
                        skip = OnBeforeWriteRecord(rec, LineNumber);

                    if (skip == false)
                    {
                        currentLine = RecordInfo.Operations.RecordToString(rec);
                        if (MustNotifyWrite)
                            currentLine = OnAfterWriteRecord(currentLine, rec);
                        writer.WriteLine(currentLine);
                    }
                }
                catch (Exception ex)
                {
                    switch (ErrorManager.ErrorMode)
                    {
                        case ErrorMode.ThrowException: throw;
                        case ErrorMode.IgnoreAndContinue: break;
                        case ErrorMode.SaveAndContinue:
                            var err = new ErrorInfo
                            {
                                LineNumber = LineNumber,
                                ExceptionInfo = ex,
                                RecordString = currentLine
                            };
                            ErrorManager.AddError(err);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                recIndex++;
            }

            TotalRecords = recIndex;

            if (string.IsNullOrEmpty(FooterText)) return;
            if (FooterText.EndsWith(NewLineForWrite)) writer.Write(FooterText);
            else writer.WriteLine(FooterText);
        }
    }
}
