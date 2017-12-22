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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management;
using System.Threading;
using System.Windows.Threading;
using Visyn.Io;
using Visyn.Threads;

namespace Visyn.Windows.Io.Device
{
    public abstract class DeviceWatcherBase<T> : IDisposable
    {
        public ObservableCollection<T> Devices { get; }

        public EventHandler<DeviceAddedRemovedEventArgs> DeviceAdded;
        public EventHandler<DeviceAddedRemovedEventArgs> DeviceRemoved;
        public int Count => Devices.Count;
        protected IOutputDevice _Output;
        protected DeviceWatcherBase(IInvoker dispatcher, IOutputDevice output)
        {
            _dispatcher = dispatcher;
            _Output = output;
            Devices = new ObservableCollection<T>();

            _watcher = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent"));
            _watcher.EventArrived += (sender, eventArgs) => SearchForDevices(eventArgs);
            _watcher.Start();
        }

        protected abstract void SearchForDevices();
		
        protected virtual void SearchForDevices(EventArrivedEventArgs args)
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


        protected virtual void RemoveDevices(IList<T> removed)
        {
            if (removed.Count > 0)
            {
                _dispatcher.Invoke(() =>
                {
                    foreach (var device in removed)
                    {
                        RemoveDevice(device);
                    }
                });
            }
        }

        protected virtual void RemoveDevice(T device)
        {
            DeviceRemoved?.Invoke(this, new DeviceAddedRemovedEventArgs(device.ToString(), false));
            Devices.Remove(device);
        }


        protected virtual void AddDevices(IList<T> devicesToAdd)
        {
            if (devicesToAdd.Count > 0)
            {
                _dispatcher.Invoke(() =>
                {
                    foreach (var device in devicesToAdd)
                    {
                        AddDevice(device);
                    }
                });
            }
        }

        protected virtual void AddDevice(T device)
        {
            DeviceAdded?.Invoke(this, new DeviceAddedRemovedEventArgs(device.ToString(), true));
            Devices.Add(device);
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _watcher.Stop();
                Devices.Clear();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
        private bool triggered = false;
        private int _skipped;
        private int eventCount;

        private DateTime _lastCreateTime = DateTime.MinValue;
        private int _delayMs = 200;
        private readonly ManagementEventWatcher _watcher;
        protected readonly IInvoker _dispatcher;
    }
}