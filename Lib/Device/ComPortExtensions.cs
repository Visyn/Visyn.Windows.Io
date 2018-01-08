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
using Visyn.Device;

namespace Visyn.Windows.Io.Device
{
    public static class ComPortExtensions
    {
        private static IComparer<ComPort> _comPortComparer;

        public static IComparer<ComPort> ComPortComparer =>
            _comPortComparer ?? (_comPortComparer = new ComPortComparerClass());

        private static IComparer<string> _comNameComparer;

        public static IComparer<string> ComNameComparer =>
            _comNameComparer ?? (_comNameComparer = new ComNameComparerClass());

        public static int ComparedTo(this ComPort portA, ComPort portB)
            => ComNameComparer.Compare(portA.PortName,portB.PortName);
        public static int ComparedTo(this IComPort portA, IComPort portB)
            => ComNameComparer.Compare(portA.PortName, portB.PortName);

        public static bool ContainsPortName(this IEnumerable<ComPort> ports, string portName )
        {
            return ports.Any(port => port.PortName == portName);
        }


        private class ComPortComparerClass : IComparer<ComPort>, IEqualityComparer<ComPort>, IComparer<IComPort>, IEqualityComparer<IComPort>
        {
            #region Implementation of IComparer<in ComPort>

            public int Compare(ComPort a, ComPort b) => ComPortExtensions.ComNameComparer.Compare(a?.PortName, b?.PortName);
            public int Compare(IComPort a, IComPort b) => ComPortExtensions.ComNameComparer.Compare(a?.PortName, b?.PortName);

            #endregion

            #region Implementation of IEqualityComparer<in ComPort>

            public bool Equals(ComPort x, ComPort y)
            {
                if (x == null) return y == null;
                if (y == null) return false;
                if (x.PortName != y.PortName) return false;
                return x.Name == y.Name;
            }

            public int GetHashCode(ComPort obj)
            {
                unchecked
                {
                    return (obj.PortName.GetHashCode() * 12377 ^ (obj.Name.GetHashCode() * 397));
                }
            }

            public bool Equals(IComPort x, IComPort y)
            {
                if (x == null) return y == null;
                if (y == null) return false;
                if (x.PortName != y.PortName) return false;
                return x.Name == y.Name;
            }

            public int GetHashCode(IComPort obj)
            {
                unchecked
                {
                    return (obj.PortName.GetHashCode() * 12377 ^ (obj.Name.GetHashCode() * 397));
                }
            }
            #endregion
        }


        private class ComNameComparerClass : IComparer<string>, IEqualityComparer<string>
        {
            #region Implementation of IComparer<in string>

            public int Compare(string a, string b)
            {
                var aLength = a?.Length ?? 0;
                var bLength = b?.Length ?? 0;
                return (aLength != bLength) ? aLength.CompareTo(bLength) :
                    string.Compare(a, b, StringComparison.InvariantCulture);
            }

            #endregion

            #region Implementation of IEqualityComparer<in ComPort>

            public bool Equals(string x, string y)
            {
                if (x == null) return y == null;
                return x.Equals(y);
            }

            public int GetHashCode(string obj) => obj.GetHashCode();

            #endregion
        }
    }
}