using System;
using System.IO;
using Visyn.Exceptions;
using Visyn.VisynApp;
using Visyn.Windows.Io.Assemblies;
using Visyn.Windows.Io.Xml;

namespace Visyn.Windows.Io.Settings
{
    public class VisynAppSettings
    {
        public static string AppSettingsPath => Path.Combine(Directories.ProgramDataDirectory, $"{AssemblyInfoAttributes.EntryAssembly().Title}.xml");

        public static TSettings Load<TSettings>(IExceptionHandler handler) where TSettings : IVisynAppSettings
        {
            TSettings settings = XmlIO.Deserialize<TSettings>(AppSettingsPath, handler);
            if (settings == null)
            {
                settings = Activator.CreateInstance<TSettings>();
                settings.InitializeDefaultSettings(null);
            }
            return settings;
        }

        public static TSettings Load<TSettings>(ExceptionHandler handler) where TSettings : IVisynAppSettings
        {
            TSettings settings = XmlIO.Deserialize<TSettings>(AppSettingsPath, handler);
            if (settings == null)
            {
                settings = Activator.CreateInstance<TSettings>();
                settings.InitializeDefaultSettings(null);
            }
            return settings;
        }

        public static void Save<TSettings>(TSettings settings, ExceptionHandler handler) where TSettings : IVisynAppSettings
        {
            if (settings == null) return;
            var path = AppSettingsPath;
            XmlIO.Serialize<TSettings>(settings, path, handler);
        }
    }
}