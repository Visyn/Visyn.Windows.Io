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

using System.Linq;
using Visyn.Collection;
using Visyn.Io;
using Visyn.Threads;

namespace Visyn.Windows.Io.Device
{
    /// <summary>
    /// Make sure you create this watcher in the UI thread if you are using the com port list in the UI
    /// </summary>
    public sealed class SerialPortWatcher : DeviceWatcherBase<ComPort>
    {


        public SerialPortWatcher(IOutputDevice output) 
            : this(new WpfInvoker(System.Windows.Threading.Dispatcher.CurrentDispatcher), output)
        {
        }

        public SerialPortWatcher(IInvoker invoker, IOutputDevice output) 
            : base(invoker, output)
        {
            Devices.AddRange(ComPort.GetComPorts());
        }


        protected override void SearchForDevices()
        {
            var ports = ComPort.GetComPorts();

            // Search for items to remove
            var remove = Devices.Where(comPort => !ports.Contains(comPort)).ToList();

            RemoveDevices(remove);

            // Search for items to add
            AddDevices(ports.Where(port => !Devices.Contains(port)).ToList());
        }
        

        #region Overrides of DeviceWatcherBase<string>

        protected override void AddDevice(ComPort device)
        {
            for (var j = 0; j <= Devices.Count; j++)
            {
                if (j < Devices.Count && ComPortExtensions.ComNameComparer.Compare(device.PortName, Devices[j].PortName) >= 0) continue;
                Devices.Insert(j, device);
                break;
            }
        }

        #endregion

        #region Overrides of DeviceWatcherBase<string>

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

//        private readonly ComNameComparer _comparer;
    }


}
