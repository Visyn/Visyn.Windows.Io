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
using System.IO.Ports;
using System.Linq;
using Visyn.Device;
using Visyn.JetBrains;

namespace Visyn.Windows.Io.Device
{
    public class ComPort : IComPort, IComparable<ComPort>, IComparable<IComPort>, IComparable
    {
        [NotNull]
        public string PortName { get; }

        [NotNull]
        public string Name { get; }

        public bool IsValid => PortName.StartsWith("COM");

        public ComPort(string portName) : this(portName, portName)
        { }

        public ComPort(string name, string portName)
        {
            PortName = portName?.ToUpper() ?? "";
            Name = name ?? PortName;
        }

        #region Overrides of Object

        public override string ToString() => Name;
        #endregion

        #region Implementation of IComparable
        public int CompareTo(ComPort other) => this.ComparedTo(other);
        public int CompareTo(IComPort other) => this.ComparedTo(other);

        public int CompareTo(object obj) => this.ComparedTo(obj as ComPort);

        #endregion

        public static IEnumerable<ComPort> GetComPorts() 
            => SerialPort.GetPortNames().OrderBy(s => s, ComPortExtensions.ComNameComparer).Select(port => new ComPort(port));

        #region Implementation of IComparable<in IComPort>



        #endregion
    }
}
