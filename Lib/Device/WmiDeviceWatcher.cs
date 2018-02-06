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
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Visyn.Collection;
using Visyn.Io;
using Visyn.JetBrains;
using Visyn.Threads;

namespace Visyn.Windows.Io.Device
{
    /// <summary>
    /// Make sure you create this watcher in the UI thread if you are using the com port list in the UI
    /// </summary>
    public sealed class WmiDeviceWatcher : DeviceWatcherBase<string>
    {
        [NotNull]
        private readonly IEnumerable<string> _deviceInstancePaths;
        public WmiDeviceWatcher(IEnumerable<string> deviceInstancePaths, IOutputDevice output) 
            : this(deviceInstancePaths,new WpfInvoker(System.Windows.Threading.Dispatcher.CurrentDispatcher),output)
        {
        }

        public WmiDeviceWatcher(IEnumerable<string> deviceInstancePaths ,IInvoker invoker, IOutputDevice output) 
            : base(invoker, output)
        {
            if (deviceInstancePaths == null) throw new NullReferenceException($"{nameof(deviceInstancePaths)} can not be null!");
            _deviceInstancePaths = deviceInstancePaths;
            var task = new Task(SearchForDevices, TaskCreationOptions.LongRunning);
            task.Start();
        }



        protected override void SearchForDevices()
        {
            var devices = new Dictionary<string, string>();

            foreach (var deviceInstancePath in _deviceInstancePaths)
            {
                try
                {
                    var squery = $"SELECT * FROM Win32_PnPEntity WHERE DeviceID LIKE '%{deviceInstancePath}%'";
                    var mos = new ManagementObjectSearcher(new SelectQuery(squery));

                    // search through all objects for sensors
                    var managementObjects = mos.Get();
                    foreach (var obj in managementObjects)
                    {
                        try
                        {
                            // decode the friendly device name and parse out the COM port assignment
                            var deviceName = obj["Name"] as string;
                            if (deviceName == null) continue;
                            var deviceId = obj["DeviceId"] as string;
                            var com = new Regex(@".?\((COM\d+)\)$");
                            if (!com.IsMatch(deviceName)) continue;

                            var comPort = com.Match(deviceName).Groups[1].ToString();
                            
                            devices.Add(comPort, deviceInstancePath);
                        }
                        catch (Exception ex)
                        {
                            Output?.WriteLine($"{nameof(WmiDeviceWatcher)}.{nameof(SearchForDevices)} Exception: {ex.Message}");
                        }
                        finally
                        {
                            obj?.Dispose();
                        }
                    }
                    mos?.Dispose();
                }
                catch (Exception ex)
                {
                    Output?.WriteLine($"{nameof(WmiDeviceWatcher)}.{nameof(SearchForDevices)} Exception: {ex.Message}");
                }
            }
            // Search for items to remove
            RemoveDevices(Devices.Where(port => !devices.ContainsKey(port)).ToList());

            // Search for items to add
            AddDevices(devices.Keys.Where(port => !Devices.Contains(port)).ToList());
        }
        

        #region Overrides of DeviceWatcherBase<string>

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

#if DEBUG
        public static List<ManagementObject> QueryUsbDevices()
        {
            var allPnP = "SELECT * FROM Win32_PnPEntity";
            var pnpMo = new ManagementObjectSearcher(new SelectQuery(allPnP));
            var pnpDev = pnpMo.Get().ToList<ManagementObject>();

            var usb = (from pnp in pnpDev
                 let pnpDevId = pnp["DeviceId"] as string
                 where pnpDevId?.Contains("USB") == true
                 select pnp).ToList();

            return usb;
        }
#endif
    }


}
