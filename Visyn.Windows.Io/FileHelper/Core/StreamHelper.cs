using System;
using System.IO;
using System.Text;

namespace Visyn.Windows.Io.FileHelper.Core
{
    public static class StreamHelper
    {
        /// <summary>
        /// open a stream with optional trim extra blank lines
        /// </summary>
        /// <param name="fileName">Filename to open</param>
        /// <param name="encode">encoding of the file</param>
        /// <param name="correctEnd">do we trim blank lines from end?</param>
        /// <param name="disposeStream">do we close stream after trimming</param>
        /// <param name="bufferSize">Buffer size to read</param>
        /// <returns>TextWriter ready to write to</returns>
        public static TextWriter CreateFileAppender(string fileName,
            Encoding encode,
            bool correctEnd,
            bool disposeStream,
            int bufferSize)
        {
            TextWriter res;

            if (correctEnd)
            {
                FileStream fs = null;

                try
                {
                    fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                    //bool CarriageReturn = false;
                    //bool LineFeed = false;

                    // read the file backwards using SeekOrigin.Begin...
                    long offset;
                    for (offset = fs.Length - 1; offset >= 0; offset--)
                    {
                        fs.Seek(offset, SeekOrigin.Begin);
                        var value = fs.ReadByte();
                        if (value == '\r')
                        {
                            // Console.Write("\\r");
                            //CarriageReturn = true;
                        }
                        else if (value == '\n')
                        {
                            // Console.Write("\\n");
                            //LineFeed = true;
                        }
                        else
                            break;
                    }
                    if (offset >= 0) // read something else other than line ends...
                    {
                        //LineEnd ending;

                        //if( CarriageReturn )
                        //    if( LineFeed )
                        //        ending = LineEnd.Dos;
                        //    else
                        //        ending=LineEnd.Macintosh;
                        //else
                        //    if( LineFeed )
                        //        ending=LineEnd.Unix;
                        //else
                        //        ending=LineEnd.other;

                        var newline = new byte[Environment.NewLine.Length];
                        int count = 0;
                        foreach (var ch in Environment.NewLine) {
                            newline[count] = Convert.ToByte(ch);
                            count++;
                        }
                        // Console.WriteLine(" value {0} count {1}\n", newline.Length, count);

                        fs.Write(newline, 0, count);
                    }
                    res = new StreamWriter(fs, encode, bufferSize);
                }
                finally
                {
                    if (disposeStream) fs?.Close();
                }
            }
            else
                res = new StreamWriter(fileName, true, encode, bufferSize);
            return res;
        }
    }
}