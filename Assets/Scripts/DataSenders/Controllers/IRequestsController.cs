using DataSenders.Requests.Interfaces;

namespace DataSenders.Managers
{
    public interface IRequestsController
    {
        void AddRequest(IBaseRequestCommand request);
    }
}
