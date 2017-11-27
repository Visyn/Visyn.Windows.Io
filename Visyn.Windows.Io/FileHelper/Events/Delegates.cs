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

namespace Visyn.Windows.Io.FileHelper.Events
{
    // ----  Read Operations  ----
    /// <summary>
    /// Called in read operations just before the record string is translated to a record.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void BeforeReadHandler<T>(IFileHelperEngine engine, BeforeReadEventArgs<T> e) where T : class;

    /// <summary>
    /// Called in read operations just after the record was created from a record string.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void AfterReadHandler<T>(IFileHelperEngine engine, AfterReadEventArgs<T> e) where T : class;


    // ----  Write Operations  ----

    /// <summary>
    /// Called in write operations just before the record is converted to a string to write it.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void BeforeWriteHandler<T>(IFileHelperEngine engine, BeforeWriteEventArgs<T> e) where T : class;

    /// <summary>
    /// Called in write operations just after the record was converted to a string.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void AfterWriteHandler<T>(IFileHelperEngine engine, AfterWriteEventArgs<T> e) where T : class;
}
