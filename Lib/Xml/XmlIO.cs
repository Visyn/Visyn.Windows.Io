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
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Visyn.Exceptions;
using Visyn.JetBrains;
using Visyn.Xml;

namespace Visyn.Windows.Io.Xml
{
    public class XmlIO
    {

        public static void Serialize<T>([NotNull]T data, [NotNull]string filename, ExceptionHandler exceptionHandler)
        {
            Debug.Assert(filename != null, "filename != null");
            using (var writer = new StreamWriter(filename))
            {
                XmlSerialize.Serialize(data, writer, exceptionHandler);
            }
        }

        public static T Deserialize<T>([NotNull]string fileName, ExceptionHandler exceptionHandler)
        {
            if (!File.Exists(fileName)) return default(T);

            using (var reader = new StreamReader(fileName))
            {
                return XmlSerialize.Deserialize<T>(reader, exceptionHandler);
            }
        }
        

        public static void WriteObject<T>(T data, string fileName, ExceptionHandler exceptionHandler)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create))
                {
                    var settings = new XmlWriterSettings {Indent = true};
                    using (var writer = XmlWriter.Create(fs, settings))
                    {
                        var ser = new NetDataContractSerializer();
                        ser.WriteObject(writer, data);
                        writer.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                if (exceptionHandler != null) exceptionHandler($"{nameof(XmlIO)}.{nameof(ReadObject)}<{typeof(T).Name}>", exc);
                else throw;
            }
        }

        public static T ReadObject<T>(string fileName, ExceptionHandler exceptionHandler)
        {
            var data = default(T);
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    using (var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
                    {
                        var ser = new NetDataContractSerializer();

                        // Deserialize the data and read it from the instance.
                        data = (T) ser.ReadObject(reader, true);
                        fs.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                if (exceptionHandler != null)
                {
                    exceptionHandler($"{nameof(XmlIO)}.{nameof(ReadObject)}<{typeof(T).Name}>", exc);
                }
                else throw;
            }

            return data;
        }
    }
}
    
