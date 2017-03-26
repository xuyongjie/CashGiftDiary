using System;
using System.Net.Http;

namespace CashGiftDiary.Client
{
    public class CashGiftDiaryClient : IDisposable
    {
        public HttpClient HttpClient { get; private set; }
        private bool disposed;
        public CashGiftDiaryClient(HttpClient client)
        {
            this.HttpClient = client ?? throw new ArgumentNullException();
        }

        public void Dispose()
        {
            if(!disposed)
            {
                HttpClient.Dispose();
                disposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
