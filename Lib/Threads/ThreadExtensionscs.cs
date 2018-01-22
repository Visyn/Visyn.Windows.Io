using System.Threading;

namespace Visyn.Windows.Io.Threads
{
    public static class ThreadExtensionscs
    {
        public static string Rename(this Thread thread, string name)
        {
            if (string.IsNullOrEmpty(thread.Name)) thread.Name = name;
            return thread.Name;
        }
    }
}
