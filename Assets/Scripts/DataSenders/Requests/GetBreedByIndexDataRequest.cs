using System.Threading;
using Models.ServerAnswers.Breeds;

namespace DataSenders.Requests
{
    public class GetBreedByIndexDataRequest : AbstractRequest<BreedByIndexServerIndexAnswer>
    {
        private readonly string _id;

        protected override string Route => string.Concat("https://dogapi.dog/api/v2/breeds/", _id);

        public GetBreedByIndexDataRequest(string index, CancellationTokenSource cancellationTokenSource)
        {
            _id = index;
            IternalCancellationTokenSource = cancellationTokenSource;
        }
    }
}
