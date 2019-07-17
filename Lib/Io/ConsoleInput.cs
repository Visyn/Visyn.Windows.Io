using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Visyn.Collection;
using Visyn.Exceptions;
using Visyn.Threads;

namespace Visyn.Windows.Io
{
    public class ConsoleEventHandler : ProcessQueuedDataTask<ConsoleKeyInfo>
//: IDisposable
    {
        public override string Name { get; } = "ConsoleInput";

        private StringBuilder builder;
        public ConsoleEventHandler(IExceptionHandler handler, int bufferSize = 1000) : base(startAction,null,handler)
        {
            builder = new StringBuilder(bufferSize);
        }

        private static readonly Action<ConcurrentQueue<ConsoleKeyInfo>> startAction = new Action<ConcurrentQueue<ConsoleKeyInfo>>((q) =>
        {
            //var task = new Task(() =>
            //{
            //    while (true)
            //    {
            //        var key = Console.ReadKey();
            //        q.Enqueue(key);
            //    }
            //});
            //task.Start();
        });

        public override void Execute()
        {
            using (var task = new Task(() =>
                {
                    while (true)
                    {
                        var key = Console.ReadKey();
                        Add(key);
                    }
                }))
            {
                task.Start();

                base.Execute();
            }
        }


        public EventHandler<ConsoleKeyInfo> KeyPress;
        public EventHandler<string> LineReceived;

        public string LineTerminator { get; set; } = Environment.NewLine;
        protected override int ProcessData()
        {
            int count = 0;
            while (Count > 0)
            {
                var key = Dequeue();

                count++;
                KeyPress?.BeginInvoke(this, key,null,null);

                if (LineReceived != null)
                {
                    builder.Append(key.KeyChar);
                    var line = builder.ToString();
                    if (line.EndsWith("\r"))
                    {
                        LineReceived.BeginInvoke(this, line,null,null);
                        builder.Clear();
                    }
                }

                return count;
            }
            return 0;
        }
    }

    public class ConsoleInput :  IDisposable
    {
        public CancellationToken Token { get; }
        public Stream InputStream { get; }
        public int BufferSize { get; }

        public ConsoleInput(CancellationToken token, int bufferSize=1000)
        {
            Token = token;
            InputStream = Console.OpenStandardInput(bufferSize);
            BufferSize = bufferSize;
        }

        private async Task<string> ReadUntil( char until)
        {
            var buffer = new byte[BufferSize];
            var builder = new StringBuilder(BufferSize);
            int offset = 0;
            if(Token != null) Token.ThrowIfCancellationRequested();
            while (true)
            {
                int result = 0;
                try
                {
                    result = await InputStream.ReadAsync(buffer, offset, 1, Token);

                }
                catch (Exception e)
                {
                    return builder.ToString();
                }



                if (result > 0)
                {
                    char ch = (char)buffer[offset];
                    builder.Append(ch);
                    offset += result;
                    if (ch == until) return builder.ToString();
                }
                if (result > 1)
                    throw new Exception("Expected result ==0 or == 1");
                if (Token.IsCancellationRequested) return builder.ToString();
            }
        }


        public async Task<string> ReadLine()
        {
            return await ReadUntil('\n');
        }



        #region IDisposable Support
        private bool isDisposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    InputStream.Dispose();
                
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                isDisposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ConsoleInput() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
