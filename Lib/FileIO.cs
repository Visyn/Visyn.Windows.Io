using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Visyn.Collection;
using Visyn.Exceptions;
using Visyn.Io;

namespace Visyn.Util.IO
{
    public class FileIO
    {
        private readonly ExceptionHandler _exceptionHandler;

        public string Filename { get; set; }
        public bool OpenWithDefaultProgram { get; set; }

        public FileIO(string filename, ExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
            Filename = filename;
        }

        public bool SaveFileToDisk(string fileData)
        {
            var __return = SaveFileToDisk(Filename, fileData,_exceptionHandler);
            if (OpenWithDefaultProgram) Process.Start(Filename);
            return __return;
        }

        public bool SaveDelimitedFileToDisk(ICollection fileData, string delimiter = ",")
        {
            var __return = SaveDelimitedFileToDisk(Filename, fileData, _exceptionHandler, delimiter);
            if (OpenWithDefaultProgram) Process.Start(Filename);
            return __return;
        }

        public bool SaveDelimitedFileToDisk(IDictionary fileData, string delimiter = ",")
        {
            var __return = SaveDelimitedFileToDisk(Filename, fileData, _exceptionHandler, delimiter);
            if (OpenWithDefaultProgram) Process.Start(Filename);
            return __return;
        }

        public bool SaveDelimitedFileToDisk(IList<IReadOnlyDictionary<string, object>> fileData, IEnumerable<string> keys, string delimiter = ",")
        {
            var result = SaveDelimitedFileToDisk(Filename, fileData, keys, _exceptionHandler, delimiter);
            if (OpenWithDefaultProgram) Process.Start(Filename);
            return result;
        }



        public static string CleanFileName(string fileName) =>
            Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c, '_'));

        public static string CreateDirectoryName(string parentDirectory, string requestedDirectory, bool create, bool yyyyMMddStamp = false, bool hhmmStamp = false)
        {
            if(requestedDirectory.Contains("\\")) throw new NotImplementedException("Currently this only works on final filename, not entire path");

            var now = DateTime.Now;
            var actualParent = parentDirectory;// != null ? CleanFileName(parentDirectory) : null;
            var actual = CleanFileName(requestedDirectory);
            var builder = actualParent != null ? new StringBuilder(actualParent + '\\') : new StringBuilder();
            if (yyyyMMddStamp) builder.Append($"{now.Year}-{now.Month:D2}-{now.Day:D2} ");
            if (hhmmStamp) builder.Append($"{now.Hour:D2}-{now.Minute:D2}-{now.Second:D2} ");
            builder.Append(actual);
            var directory  = builder.ToString();
            if (create && !Directory.Exists(directory)) Directory.CreateDirectory(directory);
            return directory;
        }

        /// <exclude />
        public static string GetTempFileName(string extension = null)
        {
            if (string.IsNullOrEmpty(extension)) return Path.GetTempFileName();
            if (!extension.StartsWith(".")) extension = "." + extension;
            return Path.GetTempFileName().Replace(".tmp", extension);
        }

        public static string GetTempFileName(string requested, string extension, bool yyyyMMddStamp = false, bool hhmmStamp = false)
        {
            var actual = CleanFileName(requested);

            var now = DateTime.Now;
            var builder = new StringBuilder($"{Path.GetTempPath()}\\");
            if (yyyyMMddStamp) builder.Append($"{now.Year}-{now.Month:D2}-{now.Day:D2} ");
            if (hhmmStamp) builder.Append($"{now.Hour:D2}-{now.Minute:D2}-{now.Second:D2} ");
            builder.Append($"{actual}.{extension.Trim(new[] { ' ', '.' })}");
            return builder.ToString();
        }

        public static bool SaveDataToDisk(string filePath, byte[] data, ExceptionHandler exceptionHandler)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    var dir = Path.GetDirectoryName(filePath);

                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    if (filePath != "")
                    {
                        File.WriteAllBytes(filePath, data);
                        return true;
                    }
                }
            }
            catch (Exception exc)
            {
                if (!HandleException($"Error saving file '{filePath}': {exc.Message}", exc, exceptionHandler)) throw;
            }
            return false;
        }

        public static bool SaveFileToDisk(string filePath, string fileData, ExceptionHandler exceptionHandler)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    var dir = Path.GetDirectoryName(filePath);

                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    if (filePath != "")
                    {
                        File.WriteAllText(filePath, fileData);
                        return true;
                    }
                }
            }
            catch (Exception exc)
            {
                if (!HandleException($"Error saving file '{filePath}': {exc.Message}", exc, exceptionHandler)) throw;
            }
            return false;
        }

        public static bool AppendFileOnDisk(string filePath, string fileData, ExceptionHandler exceptionHandler)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    var dir = Path.GetDirectoryName(filePath);

                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    if (filePath != "")
                    {
                        File.AppendAllText(filePath, fileData);
                        return true;
                    }
                }
            }
            catch (Exception exc)
            {
                if(!HandleException($"Error appending file '{filePath}': {exc.Message}", exc, exceptionHandler)) throw;
            }
            return false;
        }

        public static bool AppendFileOnDisk(string filePath, ICollection fileData, ExceptionHandler exceptionHandler)
        {
            try
            {
                IEnumerable<string> strings = fileData as IEnumerable<string> ?? fileData.ToList<string>();
                if (!string.IsNullOrEmpty(filePath))
                {
                    var dir = Path.GetDirectoryName(filePath);

                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    if (filePath != "")
                    {
                        File.AppendAllLines(filePath, strings);
                        return true;
                    }
                }
            }
            catch (Exception exc)
            {
                if(!HandleException($"Error appending file '{filePath}': {exc.Message}", exc,exceptionHandler)) throw;
            }
            return false;
        }

        public static IList<List<string>> ReadDelimitedFile(string filePath, char[] delimiter, ExceptionHandler exceptionHandler)
        {
            if (!File.Exists(filePath)) return null;
            try
            {
                var lines = File.ReadAllLines(filePath);
                IList<List<string>> list = new List<List<string>>();
                foreach (var line in lines)
                {
                    if (!string.IsNullOrEmpty(line)) list.Add(new List<string>(line.Split(delimiter)));
                }
                return list;
            }
            catch (Exception exc)
            {
                if (!HandleException($"Error appending file '{filePath}': {exc.Message}", exc, exceptionHandler)) throw;
            }
            return null;
        }


        public static bool SaveDelimitedFileToDisk(string filePath, IEnumerable data, ExceptionHandler exceptionHandler, string delimiter=",")
        {
            try
            {
                var builder = new StringBuilder(1024);
                bool first = true;
                foreach (var line in data)
                {
                    var delimited = line as IDelimitedData;
                    if (delimited != null)
                    {
                        if (first) builder.AppendLine(delimited.DelimitedHeader(delimiter));
                        builder.AppendLine(delimited.ToDelimitedString(delimiter));
                    }
                    else
                    {
                        var collection = (line is string) ? null : line as IEnumerable;
                        if (collection != null)
                            builder.AppendLine(string.Join(delimiter, collection.Cast<object>()));
                            //builder.AppendDelimitedString(collection, delimiter).Append(Environment.NewLine);
                        else
                            builder.AppendLine(line.ToString());//.Append(Environment.NewLine);
                    }
                    first = false;
                }

                return SaveFileToDisk(filePath, builder.ToString(), exceptionHandler);
            }
            catch (Exception exc)
            {
                if (!HandleException($"Error appending file '{filePath}': {exc.Message}", exc, exceptionHandler)) throw;
            }
            return false;
        }

        public static bool SaveDelimitedFileToDisk(string filePath, IDictionary fileData, ExceptionHandler exceptionHandler, string delimiter = ",")
        {
            try
            {
                var dataString = new StringBuilder(1024);
                bool first = true;
                foreach (var key in fileData.Keys)
                {
                    dataString.Append(key).Append(delimiter);
                    if (fileData[key] != null)
                    {
                        var delimited = fileData[key] as IDelimitedData;
                        if (delimited != null)
                        {
                            if (first) dataString.AppendLine(delimited.DelimitedHeader(delimiter));
                            dataString.AppendLine(delimited.ToDelimitedString(delimiter));
                        }
                        else
                        {
                            var enumeration = fileData[key] as IEnumerable;
                            var collectionValue = fileData[key] as ICollection;
                            if (collectionValue != null) dataString.Append(string.Join(delimiter,collectionValue));//.AppendDelimitedString(collectionValue, delimiter);
                            else
                            {
                                var dictionaryValue = fileData[key] as IDictionary;
                                if (dictionaryValue != null)
                                {
                                    dataString.Append(string.Join(delimiter, dictionaryValue.ToString()));//.Cast<object>()));
                                    //dataString.AppendDelimitedString(dictionary, delimiter);
                                }
                                else
                                {
                                    var rodict = fileData[key] as IReadOnlyDictionary<object, object>;
                                    dataString.Append(fileData[key].ToString());
                                }
                            }
                            dataString.Append(Environment.NewLine);
                        }
                    }
                    first = false;
                }
                return SaveFileToDisk(filePath, dataString.ToString(), exceptionHandler);
            }
            catch (Exception exc)
            {
                if(!HandleException($"Error saving file '{filePath}': {exc.Message}", exc, exceptionHandler)) throw;
            }
            return false;
        }


        public static bool SaveDelimitedFileToDisk(string filePath, IList<IReadOnlyDictionary<string, object>> fileData, IEnumerable<string> keys, ExceptionHandler exceptionHandler, string delimiter = ",")
        {
            try
            {
                var builder = new StringBuilder();
                builder.AppendLine(string.Join(delimiter, keys));

                foreach (var row in fileData)
                {
                    List<object> keylist = keys.Select(key => row[key]).ToList();
                }
                foreach (var row in fileData)
                {
                    var line2 = string.Join(delimiter, keys.Select(key => row[key]));
                    builder.AppendLine(line2);
                }
                return SaveFileToDisk(filePath, builder.ToString(), exceptionHandler);
            }
            catch (Exception exc)
            {
                if (!HandleException($"Error saving file '{filePath}': {exc.Message}", exc, exceptionHandler)) throw;
            }
            return false;
        }

        public static bool SaveDelimitedFileToDisk(string filePath, IEnumerable<List<object>> fileData, ExceptionHandler exceptionHandler, string delimiter = ",")
        {
            try
            {
                var builder = new StringBuilder();

                foreach (var row in fileData)
                {
                    var line = string.Join(delimiter, row);
                    builder.AppendLine(line);
                }
                return SaveFileToDisk(filePath, builder.ToString(), exceptionHandler);
            }
            catch (Exception exc)
            {
                if (!HandleException($"Error saving file '{filePath}': {exc.Message}", exc, exceptionHandler)) throw;
            }
            return false;
        }

        public static bool SaveDelimitedFileToDisk(string filePath, IEnumerableColumns data , IEnumerable<string> columnNames, ExceptionHandler exceptionHandler, string delimiter = ",")
        {
            return SaveDelimitedFileToDisk(filePath, data.ColumnData(columnNames, true), exceptionHandler);
        }

        private static bool HandleException(string message, Exception exc, ExceptionHandler exceptionHandler, [CallerMemberName] string caller="")
        {
            if (exceptionHandler == null) return false;
            exceptionHandler.Invoke($"{typeof(FileIO).FullName}.{caller}", string.IsNullOrEmpty(message) ? exc : new Exception(message, exc));
            return true;
        }

        /// <summary>
        /// Convert a url path into windows valid path 
        /// </summary>
        /// <returns> directory structure</returns>
        public static string UrlToUnc(string url)
        {
            var directoryPath = Regex.Replace(url, "[^/]*$", "");
            directoryPath = Regex.Replace(directoryPath, "^(ht|f)tp(s?):", "");
            directoryPath = Regex.Replace(directoryPath, @"/", @"\");

            return directoryPath;
        }

        /// <summary>
        /// extract the page from a url. Example :"http://microsoft.com/index.html" > "index.html"
        /// </summary>
        /// <param name="url"></param>
        /// <returns>the page from the full url path</returns>
        public static string FilenameFromUrl(string url)
        {
            string file = Regex.Match(url, @"[^/]*$").Value;
            return file;
        }
    }
}
