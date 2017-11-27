#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
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
using System.Runtime.InteropServices;

namespace Visyn.Windows.Io.FileHelper.Streams
{
    /// <summary>
    /// Enable reading a string as a stream
    /// </summary>
    [Serializable]
    public sealed class InternalStringReader : TextReader
    {
        /// <summary>
        /// String we are analysing
        /// </summary>
        private string mS;
		
        /// <summary>
        /// Create a stream based on the string
        /// </summary>
        /// <param name="s"></param>
        public InternalStringReader(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            this.mS = s;
            this.Length = s.Length;
        }

        /// <summary>
        /// Length of the stream
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Position within the stream
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Close the stream
        /// </summary>
        public override void Close()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Close the stream and release resources
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            this.mS = null;
            this.Position = 0;
            this.Length = 0;
            base.Dispose(disposing);
        }

        /// <summary>
        /// Peek at the next byte along in the stream
        /// </summary>
        /// <returns>Next character as an integer</returns>
        public override int Peek()
        {
            if (this.mS == null)
                throw new ObjectDisposedException(null, "The Reader is Closed");
            if (this.Position == this.Length)
                return -1;
            return this.mS[this.Position];
        }

        /// <summary>
        /// Read the next byte along in the string
        /// </summary>
        /// <returns>next character as an integer</returns>
        public override int Read()
        {
            if (this.mS == null)
                throw new ObjectDisposedException(null, "The Reader is Closed");
            if (this.Position == this.Length)
                return -1;
            return this.mS[this.Position = this.Position + 1];
        }

        /// <summary>
        /// Replaces text into the buffer at index for count characters
        /// </summary>
        /// <param name="buffer">
        /// An array of Unicode characters to which characters in this instance
        /// are copied.
        /// </param>
        /// <param name="index">
        /// The index in destination at which the copy operation begins.
        /// </param>
        /// <param name="count">
        /// The number of characters in this instance to copy to destination.
        /// </param>
        /// <returns>number of characters actually copied</returns>
        public override int Read([In, Out] char[] buffer, int index, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if ((buffer.Length - index) < count)
                throw new ArgumentException("offset");
            if (this.mS == null)
                throw new ObjectDisposedException(null, "The Reader is Closed");
            int num = this.Length - this.Position;
            if (num > 0) {
                if (num > count)
                    num = count;
                this.mS.CopyTo(this.Position, buffer, index, num);
                this.Position = this.Position + num;
            }
            return num;
        }

        /// <summary>
        /// Read a line from the buffer
        /// </summary>
        /// <returns>Line without line end</returns>
        public override string ReadLine()
        {
            if (this.mS == null)
                throw new ObjectDisposedException(null, "The Reader is Closed");
            int num = this.Position;
            while (num < this.Length) {
                char ch = this.mS[num];
                switch (ch) {
                    case '\r':
                    case '\n':
                    {
                        string str = this.mS.Substring(this.Position, num - this.Position);
                        this.Position = num + 1;
                        if (((ch == '\r') && (this.Position < this.Length)) &&
                            (this.mS[this.Position] == '\n'))
                            this.Position = this.Position + 1;
                        return str;
                    }
                }
                num++;
            }
            if (num > this.Position) {
                string str2 = this.mS.Substring(this.Position, num - this.Position);
                this.Position = num;
                return str2;
            }
            return null;
        }

        /// <summary>
        /// Read the buffer to the end
        /// </summary>
        /// <returns>String containing all remaining text</returns>
        public override string ReadToEnd()
        {
            string str;
            if (this.mS == null)
                throw new ObjectDisposedException(null, "The Reader is Closed");
            if (this.Position == 0)
                str = this.mS;
            else
                str = this.mS.Substring(this.Position, this.Length - this.Position);
            this.Position = this.Length;
            return str;
        }
    }
}
