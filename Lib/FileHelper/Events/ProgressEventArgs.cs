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

namespace Visyn.Windows.Io.FileHelper.Events
{

    /// <summary>Class used to notify the current progress position and other context info.</summary>
    public class ProgressEventArgs
        : EventArgs
    {
        /// <summary>
        /// Percentage of the file complete (estimate or completion time)
        /// </summary>
        public double Percent { get; private set; }

        /// <summary>
        /// Number of the record being processed
        /// </summary>
        public int CurrentRecord { get; private set; }

        //public int ReadBytes { get; private set; }

        /// <summary>
        /// Total records in the file  (-1 is unknown)
        /// </summary>
        public int TotalRecords { get; private set; }

        /// <summary>
        /// Current position in the file
        /// </summary>
        public long CurrentBytes { get; private set; }

        /// <summary>
        /// Total bytes in the file
        /// </summary>
        public long TotalBytes { get; private set; }

        /// <summary>
        /// Create a progress event argument
        /// </summary>
        /// <param name="currentRecord">Current record in file</param>
        /// <param name="totalRecords">Total records in file</param>
        public ProgressEventArgs(int currentRecord, int totalRecords)
            : this(currentRecord, totalRecords, -1, -1) {}

        /// <summary>
        /// Create a progress event argument
        /// </summary>
        /// <param name="currentRecord">Current record in file</param>
        /// <param name="totalRecords">Total number of records in file</param>
        /// <param name="currentBytes">Current position in bytes</param>
        /// <param name="totalBytes">Total bytes in file</param>
        public ProgressEventArgs(int currentRecord, int totalRecords, long currentBytes, long totalBytes)
        {
            CurrentRecord = currentRecord;
            TotalRecords = totalRecords;
            CurrentBytes = currentBytes;
            TotalBytes = totalBytes;

            if (totalRecords > 0)
                Percent = currentRecord/(double) totalRecords*100.0;
            else if (totalBytes > 0)
                Percent = currentBytes/(double) totalBytes*100.0;
            else

                Percent = -1;
        }
    }


}
