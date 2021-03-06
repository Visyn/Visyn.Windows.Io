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

using Visyn.Windows.Io.FileHelper.Events;

namespace Visyn.Windows.Io.FileHelper.Interfaces
{
    /// <summary>
    /// Interface used to provide In record notification of read operations.
    /// </summary>
    /// <example>
    /// <code>
    /// private class SampleType: INotifyRead, INotifyWrite
    /// { ....
    /// 
    ///		public void AfterRead(AfterReadEventArgs e)
    ///		{
    ///			// Your Code Here
    ///		}
    ///		public void BeforeWrite(BeforeReadEventArgs e)
    ///		{
    /// 		// Your Code Here
    ///		}
    /// 
    /// }
    /// </code>
    /// </example>
    public interface INotifyRead
    {
      
        /// <summary>
        /// Method called by the engines before fill the info of the record and
        /// after read the source line.
        /// </summary>
        /// <param name="e">The Event Info</param>
        void BeforeRead(BeforeReadEventArgs e);

        /// <summary>
        /// Method called by the engines after read a record from the source data.
        /// </summary>
        /// <param name="e">The Event Info</param>
        void AfterRead(AfterReadEventArgs e);

    }
}
