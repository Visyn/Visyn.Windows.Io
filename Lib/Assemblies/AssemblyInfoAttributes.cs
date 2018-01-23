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
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Visyn.Collection;

namespace Visyn.Windows.Io.Assemblies
{
    public class AssemblyInfoAttributes
    {
        public string Company { get; set; }
        public string Product { get; set; }
        //[Obsolete("Was never populated",true)]
        //public string Application { get; set; }
        public string Version { get; set; }
        public string FileVersion { get; set; }
        public string Configuration { get; set; }
        public string Copyright { get; set; }
        public string Title { get; set; }
        public string Guid { get; set; }

        protected AssemblyInfoAttributes(Assembly assembly)
        {
            if (assembly == null) throw new NullReferenceException($"{nameof(assembly)} must not be null!");
            var company = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).Cast<AssemblyCompanyAttribute>().FirstOrDefault();
            Company = company?.Company;
            var product = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false).Cast<AssemblyProductAttribute>().FirstOrDefault();
            Product = product?.Product;
            var copyright = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false).Cast<AssemblyCopyrightAttribute>().FirstOrDefault();
            Copyright = copyright?.Copyright;
            var version = assembly.GetCustomAttributes(typeof(AssemblyVersionAttribute), false).Cast<AssemblyVersionAttribute>().FirstOrDefault();
            Version = version?.Version;
            var fileVersion = assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false).Cast<AssemblyFileVersionAttribute>().FirstOrDefault();
            FileVersion = fileVersion?.Version;
            var config = assembly.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false).Cast<AssemblyConfigurationAttribute>().FirstOrDefault();
            Configuration = config?.Configuration;
            var title = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute),false).Cast<AssemblyTitleAttribute>().FirstOrDefault();
            Title = title?.Title;
            var guid = assembly.GetCustomAttributes(typeof(GuidAttribute), false).Cast<GuidAttribute>().FirstOrDefault();
            Guid = guid?.Value;
        }

        public static SimpleCache<string,AssemblyInfoAttributes> AssemblyInfoCache = 
            new SimpleCache<string,AssemblyInfoAttributes>(new Func<string, AssemblyInfoAttributes>(
                (key) =>
                {
                    if (string.IsNullOrEmpty(key)) return null;
                    var assembly = AssemblyExtensions.GetAssemblyByName(key);
                    return assembly != null ? new AssemblyInfoAttributes(assembly) : null;
                }));

        public static AssemblyInfoAttributes EntryAssembly()
        {
            var entry = Assembly.GetEntryAssembly();
            return AssemblyInfoCache.Get(entry.Name());
        }

        public static AssemblyInfoAttributes ExecutingAssembly()
        {
            var executing = Assembly.GetExecutingAssembly();
            return AssemblyInfoCache.Get(executing.Name());
        }

        public static AssemblyInfoAttributes Get(string name)
        {
            return AssemblyInfoCache.Get(name);
        }

        [Obsolete("Use AssemblyInfoExtensions.AssemblyInfo(this Assembly assembly) extension method intead")]
        public static AssemblyInfoAttributes Get(Assembly assembly)
        {
            return AssemblyInfoCache.Get(assembly.Name());
        }
    }
}
