using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Visyn.Windows.Io
{
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
            while (true)
            {
              
                var result = await InputStream.ReadAsync(buffer, offset, 1, Token);

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
