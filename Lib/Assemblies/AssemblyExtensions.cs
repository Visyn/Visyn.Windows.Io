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
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;

namespace Visyn.Windows.Io.Assemblies
{
    public static class AssemblyExtensions
    {
        private static readonly Lazy<ConcurrentDictionary<string, Assembly>> _assembliesByName = new Lazy<ConcurrentDictionary<string, Assembly>>(() =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var dictionary = new ConcurrentDictionary<string, Assembly>();

            foreach(var assembly in assemblies)
            {
                dictionary.GetOrAdd(assembly.GetName().Name, assembly);
            }
            return dictionary;
        },LazyThreadSafetyMode.ExecutionAndPublication );

        public static Assembly GetAssemblyByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            Assembly assembly = null;
            if(_assembliesByName.Value.TryGetValue(name, out assembly))
                return assembly;
            return null;
        }

        public static string Name(this Assembly assembly)
        {
            return assembly != null ? assembly.GetName().Name : "";
        }
    }
}