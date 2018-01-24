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
using System.IO;
using System.Reflection;
using System.Threading;

namespace Visyn.Windows.Io.Assemblies
{
    public static class Directories
    {
        public static string ProgramDataDirectory => LazyProgramDataDirectory.Value;

        private static readonly Lazy<string> LazyProgramDataDirectory = new Lazy<string>(new Func<string>(() =>
        {
            // GetEntryAssembly() works properly for applications, but fails unit test. Use GetCallingAssembly for unit test.
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            var attributes = AssemblyInfoAttributes.Get(assembly.Name());

            var program = Path.GetFileNameWithoutExtension(assembly.Location);
            if (!string.IsNullOrWhiteSpace(attributes?.Title)) program = attributes.Title;

            var company = attributes?.Company;
            if (!string.IsNullOrWhiteSpace(company))
            {
                var split = company.Split(',', '.');
                company = string.IsNullOrWhiteSpace(split[0]) ? null : split[0].Trim();
            }
            if (string.IsNullOrWhiteSpace(company)) { company = "Visyn"; }

            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), company, program);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            return directory;
        }), LazyThreadSafetyMode.ExecutionAndPublication);


        public static string CompanyDataDirectory => LazyCompanyDataDirectory.Value;

        public static readonly Lazy<string> LazyCompanyDataDirectory = new Lazy<string>(new Func<string>(() =>
        {
            // GetEntryAssembly() works properly for applications, but fails unit test. Use GetCallingAssembly for unit test.
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            var attributes = AssemblyInfoAttributes.Get(assembly.Name());

            var company = attributes?.Company;
            if (!string.IsNullOrWhiteSpace(company))
            {
                var split = company.Split(',', '.');
                company = string.IsNullOrWhiteSpace(split[0]) ? null : split[0].Trim();
            }
            if (string.IsNullOrWhiteSpace(company)) { company = "Visyn"; }

            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), company);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            return directory;
        }), LazyThreadSafetyMode.ExecutionAndPublication);


        public static string UserAppDataDirectory => LazyUserAppDataDirectory.Value;

        public static readonly Lazy<string> LazyUserAppDataDirectory = new Lazy<string>(new Func<string>(() =>
        {
            // GetEntryAssembly() works properly for applications, but fails unit test. Use GetCallingAssembly for unit test.
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            var attributes = AssemblyInfoAttributes.Get(assembly.Name());

            var program = Path.GetFileNameWithoutExtension(assembly.Location);
            if (!string.IsNullOrWhiteSpace(attributes?.Title)) program = attributes.Title;

            var company = attributes?.Company;
            if (!string.IsNullOrWhiteSpace(company))
            {
                var split = company.Split(',', '.');
                company = string.IsNullOrWhiteSpace(split[0]) ? null : split[0].Trim();
            }
            if (string.IsNullOrWhiteSpace(company)) { company = "Visyn"; }

            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), company, program);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            return directory;
        }), LazyThreadSafetyMode.ExecutionAndPublication);
    }
}