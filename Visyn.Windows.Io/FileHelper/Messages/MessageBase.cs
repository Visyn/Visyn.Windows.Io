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

namespace Visyn.Windows.Io.FileHelper.Messages
{
    /// <summary>
    /// Base class of all messages
    /// </summary>
    /// <remarks>
    /// Messages may have substitution of $var$ applied
    /// </remarks>
    public abstract class MessageBase
    {
        /// <summary>
        /// Create a message for given text
        /// </summary>
        /// <param name="text">Text of the message, may have $var$ variables</param>
        protected MessageBase(string text)
        {
            SourceText = text;
        }

        /// <summary>
        /// Base of messages without substitution
        /// </summary>
        protected string SourceText { get; private set; }

        /// <summary>
        /// Message text after substitution happens
        /// </summary>
        public string Text
        {
            get { return GenerateText(); }
        }

        /// <summary>
        /// Convert the source text using substitution
        /// </summary>
        /// <returns></returns>
        protected abstract string GenerateText();

        /// <summary>
        /// Send message out with conversion applied
        /// </summary>
        /// <returns>message after substitution</returns>
        public sealed override string ToString()
        {
            return Text;
        }
    }
}
