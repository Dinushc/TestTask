using System;

namespace DataSenders.Requests.Interfaces
{
    public interface IRequestCommand<T> : IBaseRequestCommand
    {
        IObservable<T> OnRequestDone { get; }
    }
}
