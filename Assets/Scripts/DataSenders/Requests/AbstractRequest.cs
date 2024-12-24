using System;
using System.Threading;
using Assets.Scripts.Common;
using Cysharp.Threading.Tasks;
using DataSenders.Requests.Interfaces;
using DataSenders.Senders;
using Newtonsoft.Json;
using UniRx;

namespace DataSenders.Requests
{
    public abstract class AbstractRequest<T> : IRequestCommand<T>
    {
        protected readonly ReactiveCommand<T> OnResultDone = new();
        protected CancellationTokenSource IternalCancellationTokenSource;

        protected abstract string Route { get; }
        public IObservable<T> OnRequestDone => OnResultDone;
        public CancellationTokenSource CancellationTokenSource => IternalCancellationTokenSource;

        public virtual async UniTask Execute(IRequestSender requestSender, CancellationToken token)
        {
            var result = await requestSender.GetInfo(Route, token);
            DeserializeAnswer(result);
        }

        protected void DeserializeAnswer(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                UnityEngine.Debug.LogError($"Request {GetType()} on route {Route} complete with error.");
                return;
            }

            var answer = JsonConvert.DeserializeObject<T>(json, ProjectSettings.Common.SerializerSettings);
            if (answer == null)
            {
                UnityEngine.Debug.LogError($"Failed to deserialize object");
                return;
            }
            OnResultDone.Execute(answer);
        }
    }
}
