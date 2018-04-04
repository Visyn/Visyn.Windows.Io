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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Management;
using System.Threading;
using Visyn.Device;
using Visyn.Exceptions;
using Visyn.Io;
using Visyn.JetBrains;
using Visyn.Threads;

namespace Visyn.Windows.Io.Device
{
    /// <summary>
    /// Class DeviceWatcherBase.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="Visyn.Exceptions.IExceptionHandler" />
    public abstract class DeviceWatcherBase<T> : IDisposable, IExceptionHandler
    {
        /// <summary>
        /// The win32 device change event query string for event: Win32_DeviceChangeEvent
        /// </summary>
        public const string Win32DeviceChangeEventQueryString = "select * from Win32_DeviceChangeEvent";
        /// <summary>
        /// Gets the event query string.
        /// </summary>
        /// <value>The event query string.</value>
        public string EventQueryString { get; }
        /// <summary>
        /// Gets a list of the available devices.
        /// </summary>
        /// <value>The devices.</value>
        [NotNull]
        public ObservableCollection<T> Devices { get; }

        /// <summary>
        /// The device added event
        /// </summary>
        public EventHandler<DeviceAddedRemovedEventArgs<T>> DeviceAdded;
        /// <summary>
        /// The device devicesToRemove event
        /// </summary>
        public EventHandler<DeviceAddedRemovedEventArgs<T>> DeviceRemoved;

        // ReSharper disable once PossibleNullReferenceException
        /// <summary>
        /// Gets the count of currently available devices.
        /// </summary>
        /// <value>The count.</value>
        public int Count => Devices.Count;
        /// <summary>
        /// The output device for error handling
        /// </summary>
        protected IOutputDevice Output;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceWatcherBase{T}"/> class.
        /// </summary>
        /// <param name="invoker">The invoker.</param>
        /// <param name="output">The output.</param>
        protected DeviceWatcherBase(IInvoker invoker, IOutputDevice output)
            : this(Win32DeviceChangeEventQueryString, invoker, output) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceWatcherBase{T}"/> class.
        /// </summary>
        /// <param name="eventQueryString">The event query string.</param>
        /// <param name="invoker">The invoker.</param>
        /// <param name="output">The output.</param>
        /// <exception cref="System.NullReferenceException">T</exception>
        protected DeviceWatcherBase(string eventQueryString, IInvoker invoker, IOutputDevice output)
        {
            EventQueryString = eventQueryString;
            if (invoker == null)
                throw new NullReferenceException($"{nameof(DeviceWatcherBase<T>)} {nameof(invoker)} can not be null!");
            Invoker = invoker;
            Output = output;
            Devices = new ObservableCollection<T>();
            var query = new WqlEventQuery(EventQueryString);
            _watcher = new ManagementEventWatcher(query);
            
            _watcher.EventArrived += (sender, eventArgs) => SearchForDevices(eventArgs);
            _watcher.Start();
            Debug.Assert(_watcher.Query.QueryString == EventQueryString);
        }

        /// <summary>
        /// Searches for devices.
        /// </summary>
        protected abstract void SearchForDevices();

        /// <summary>
        /// Searches for devices.
        /// </summary>
        /// <param name="args">The <see cref="EventArrivedEventArgs"/> instance containing the event data.</param>
        protected virtual void SearchForDevices(EventArrivedEventArgs args)
        {
            try
            {
                var eventCreationTime = args.TimeCreated();

                // Delay/event skipping significantly reduces number of polls (~10x)
                if (eventCreationTime < _lastCreateTime + TimeSpan.FromMilliseconds(_delayMs))
                {
                    _skipped++;
                }
                else
                {
                    _lastCreateTime = eventCreationTime;
                    Thread.Sleep(_delayMs);
                    eventCount++;
                    SearchForDevices();
                }
            }
            catch (Exception e)
            {
                HandleException(this, e);
            }
        }


        /// <summary>
        /// Removes the specified devices.
        /// </summary>
        /// <param name="devicesToRemove">List of devices to remove.</param>
        protected virtual void RemoveDevices(IList<T> devicesToRemove)
        {
            if (devicesToRemove?.Count > 0)
            {
                Invoker.Invoke(() =>
                {
                    foreach (var device in devicesToRemove)
                    {
                        RemoveDevice(device);
                    }
                });
            }
        }

        /// <summary>
        /// Removes the specified device.
        /// </summary>
        /// <param name="device">The device to remove.</param>
        protected virtual void RemoveDevice(T device)
        {
            Devices.Remove(device);
            DeviceRemoved?.Invoke(this, new DeviceAddedRemovedEventArgs<T>(device.ToString(), false, device));
        }


        /// <summary>
        /// Adds the specified devices to the list of currently available devices.
        /// </summary>
        /// <param name="devicesToAdd">The devices to add.</param>
        protected virtual void AddDevices(IList<T> devicesToAdd)
        {
            if (devicesToAdd?.Count > 0)
            {
                Invoker.Invoke(() =>
                {
                    foreach (var device in devicesToAdd)
                    {
                        AddDevice(device);
                    }
                });
            }
        }

        /// <summary>
        /// Adds the specified device to the currently available devices list.
        /// </summary>
        /// <param name="device">The device.</param>
        protected virtual void AddDevice(T device)
        {
            Devices.Add(device);
            DeviceAdded?.Invoke(this, new DeviceAddedRemovedEventArgs<T>(device.ToString(), true,device));
        }

        #region IDisposable

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _watcher.Stop();
                Devices.Clear();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// The skipped
        /// </summary>
        private int _skipped;

        /// <summary>
        /// The event count
        /// </summary>
        private int eventCount;

        /// <summary>
        /// The last create time
        /// </summary>
        private DateTime _lastCreateTime = DateTime.MinValue;
        /// <summary>
        /// The delay ms
        /// </summary>
        private int _delayMs = 200;
        /// <summary>
        /// The watcher
        /// </summary>
        private readonly ManagementEventWatcher _watcher;

        /// <summary>
        /// The invoker
        /// </summary>
        [NotNull]
        protected readonly IInvoker Invoker;

        #region Implementation of IExceptionHandler

        /// <summary>
        /// Gets or sets the exception handler.
        /// </summary>
        /// <value>The exception handler.</value>
        public IExceptionHandler ExceptionHandler { get; set; }
        /// <summary>
        /// Handles the exception
        /// If false is returned, sender should throw the exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <returns><c>true</c> if exception was handled, <c>false</c> otherwise.</returns>
        public bool HandleException(object sender, Exception exception)
        {
            if(ExceptionHandler?.HandleException(sender,exception) == true)
                return true;
            if (sender == null) sender = this;
            Output?.WriteLine($"{sender.GetType().Name} Exception: {exception.Message}");
            return true;
        }
        

        #endregion
    }
}