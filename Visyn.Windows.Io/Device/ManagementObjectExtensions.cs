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
using System.Management;

namespace Visyn.Windows.Io.Device
{
    public static class ManagementObjectExtensions
    {
        public static DateTime TimeCreated(this EventArrivedEventArgs args) => TimeCreated(args.NewEvent);
        public static DateTime TimeCreated(this ManagementBaseObject newEvent)
        {
            var created = (ulong)newEvent["TIME_CREATED"];
            var createdSecMs = Convert.ToDouble(created) * 1e-7 * 1e+3;
            var timeSpan = TimeSpan.FromMilliseconds(createdSecMs);
            var createdTime = new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc) + timeSpan;
            return createdTime;
        }
    }
}