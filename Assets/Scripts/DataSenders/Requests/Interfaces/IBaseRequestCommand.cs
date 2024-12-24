using Cysharp.Threading.Tasks;
using DataSenders.Senders;
using System.Threading;

namespace DataSenders.Requests.Interfaces
{
    public interface IBaseRequestCommand
    {
        public CancellationTokenSource CancellationTokenSource { get; }

        public UniTask Execute(IRequestSender requestSender, CancellationToken token);
    }
}
