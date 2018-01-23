using System.Reflection;

namespace Visyn.Windows.Io.Assemblies
{
    public static class AssemblyInfoExtensions
    {
        public static AssemblyInfoAttributes AssemblyInfo(this Assembly assembly) 
            => AssemblyInfoAttributes.AssemblyInfoCache.Get(assembly.Name());

        public static string Version(this Assembly assembly)
        {
            var assemblyInfo = AssemblyInfoAttributes.AssemblyInfoCache.Get(assembly.Name());
            if(assemblyInfo != null)
            {
                if (!string.IsNullOrWhiteSpace(assemblyInfo.Version)) return assemblyInfo.Version;
                if(!string.IsNullOrWhiteSpace(assemblyInfo.FileVersion)) return assemblyInfo.FileVersion;
            }
            return null;
        }

        public static string Product(this Assembly assembly) 
            => AssemblyInfoAttributes.AssemblyInfoCache.Get(assembly.Name()).Product;

        public static string Company(this Assembly assembly)
            => AssemblyInfoAttributes.AssemblyInfoCache.Get(assembly.Name()).Company;
    }
}