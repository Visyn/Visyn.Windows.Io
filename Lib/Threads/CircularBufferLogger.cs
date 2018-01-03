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
    public abstract class CircularBufferLogger<T> : ProcessQueuedDataTask<T>
    {
        public int Capacity { get; }
        public string Path { get; }

        [Obsolete("Backing Field, do not access")]
        private bool _logging;

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

        public bool Append { get; }

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

        protected override void ProcessData()
        {
            if (Logging)
            {
                if (Count > 0)
                {
                    LogItems(DequeueMany());
                }
            }

            if (Count > Capacity * 1.1)
            {
                OverCapacity();
            }
        }

        #endregion

        protected abstract void LogItems(IEnumerable<T> items);
        protected virtual void OverCapacity()
        {
            List<T> items;
            TryDequeueMany(Count - Capacity, out items);
        }
    }
}