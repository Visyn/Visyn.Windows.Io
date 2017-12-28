using System;
using System.ComponentModel;
using Visyn.Threads;

namespace Visyn.Windows.Io.Threads
{
    public class WinFormsInvoker : IInvoker
    {
        public ISynchronizeInvoke SynchronizeInvoke { get; }

        public WinFormsInvoker(ISynchronizeInvoke uiItem)
        {
            SynchronizeInvoke = uiItem;
        }
        #region Implementation of IInvoker

        public void Invoke(Delegate method, object[] args)
        {
            if (SynchronizeInvoke.InvokeRequired)
            {
                SynchronizeInvoke.Invoke(method, args);
            }
            else
            {
                Invoke(method, args);
            }
        }

        public void Invoke(Action method)
        {
            if (SynchronizeInvoke.InvokeRequired) SynchronizeInvoke.Invoke(method, null);
            else method();
        }

        public void Invoke<T>(Action<T> method, T param)
        {
            
            if (SynchronizeInvoke.InvokeRequired)
            {
                SynchronizeInvoke.Invoke(method, new object[] { param });
            }
            else
            {
                method(param);
            }
        }
        public void Invoke<T>(EventHandler<T> handler, object sender, T param)
        {
            if (handler == null) return;
            if (SynchronizeInvoke.InvokeRequired)
            {
                SynchronizeInvoke.Invoke(handler, new[] { sender, param });
            }
            else
            {
                handler.Invoke(sender, param);
            }
        }


        public void BeginInvoke(Action action)
        {
#if DEBUG
            if (action == null) throw new NullReferenceException($"{nameof(action)} can not be null!");
#else
            if(action == null) return;
#endif
            SynchronizeInvoke.BeginInvoke(action, new object []{});
        }

        public void BeginInvoke<T>(Action<T> action, T param)
        {
#if DEBUG
            if (action == null) throw new NullReferenceException($"{nameof(action)} can not be null!");
#else
            if(action == null) return;
#endif
            SynchronizeInvoke.BeginInvoke(action, new object[] { param });
        }
        #endregion
    }
}