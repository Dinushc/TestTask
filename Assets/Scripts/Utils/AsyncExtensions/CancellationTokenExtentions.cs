using System.Threading;

namespace Utils.AsyncExtensions
{
    public static class CancellationTokenExtentions
    {
        public static void TryCancel(this CancellationTokenSource cancellationTokenSource)
        {
            if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
        }
    }
}
