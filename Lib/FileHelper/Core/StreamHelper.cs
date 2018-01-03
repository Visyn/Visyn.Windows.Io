#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

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
