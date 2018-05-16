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
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Visyn.Exceptions;
using Visyn.Threads;


namespace Visyn.Windows.Io.Threads
{
    /// <summary>
    /// Class CircularBufferLogger.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Visyn.Threads.ProcessQueuedDataTask{T}" />
    public abstract class CircularBufferLogger<T> : ProcessQueuedDataTask<T>
    {
        /// <summary>
        /// Gets the capacity of the log.
        /// </summary>
        /// <value>The capacity.</value>
        public int Capacity { get; }
        /// <summary>
        /// Gets the path to the log file.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; }

        /// <summary>
        /// The logging status backing field
        /// </summary>
        [Obsolete("Backing Field, do not access")]
        private bool _logging;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CircularBufferLogger{T}"/> is logging.
        /// </summary>
        /// <value><c>true</c> if logging; otherwise, <c>false</c>.</value>
        public bool Logging
        {
#pragma warning disable 618
            get { return _logging; }
            set
            {
                _logging = value;
                Wait.Set();
            }
#pragma warning restore 618
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="CircularBufferLogger{T}"/> is appending.
        /// </summary>
        /// <value><c>true</c> if append; otherwise, <c>false</c>.</value>
        public bool Append { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularBufferLogger{T}"/> class.
        /// </summary>
        /// <param name="path">The path to the log file.</param>
        /// <param name="capacity">The capacity of the log in lines.</param>
        /// <param name="append">if set to <c>true</c> to [append] if log file already exists.</param>
        /// <param name="handler">The exception handler.</param>
        /// <exception cref="System.NullReferenceException">T</exception>
        protected CircularBufferLogger(string path, int capacity, bool append, IExceptionHandler handler)
            : base(handler)
        {
            if (string.IsNullOrEmpty(path))
                throw new NullReferenceException($"{nameof(CircularBufferLogger<T>)} {nameof(path)} is not a valid path!");
            if (!append && File.Exists(path))
            {
                File.Delete(path);
            }

            Path = path;
            Capacity = capacity;
            Append = append;
            RateLimitTimeSpan = TimeSpan.FromSeconds(2);
            TaskStartedAction = (a) =>
            {
                Thread.CurrentThread.Name = GetType().Name;
                Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            };
        }
        #region Overrides of ProcessQueuedDataTask<OptimizationResults>

        /// <summary>
        /// Processes the data.
        /// </summary>
        protected override int ProcessData()
        {
            int count = 0;
            if (Logging)
            {
                if (Count > 0)
                {
                    count = LogItems(DequeueMany());
                }
            }

            if (Count > Capacity * 1.1)
            {
                count += OverCapacity();
            }
            return count;
        }

        #endregion

        /// <summary>
        /// Logs the specified items.
        /// </summary>
        /// <param name="items">The items to log.</param>
        protected abstract int LogItems(IEnumerable<T> items);

        /// <summary>
        /// Checks log capacity and removes items if capacity exceeded.
        /// </summary>
        protected virtual int OverCapacity()
        {
            List<T> items;
            return TryDequeueMany(Count - Capacity, out items);
        }
    }
}