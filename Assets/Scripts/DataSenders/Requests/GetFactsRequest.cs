using System.Threading;
using Models.ServerAnswers.Breeds;

namespace DataSenders.Requests
{
    public class GetFactsRequest : AbstractRequest<BreedsServerAnswer>
    {
        protected override string Route => "https://dogapi.dog/api/v2/breeds";

        public GetFactsRequest(CancellationTokenSource cancellationTokenSource)
        {
            IternalCancellationTokenSource = cancellationTokenSource;
        }
    }
}
