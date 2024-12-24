using Cysharp.Threading.Tasks;
using DataSenders.Senders;
using System.Threading;
using UnityEngine;

namespace DataSenders.Requests
{
    public class GetRemoteSpriteRequest : AbstractRequest<Sprite>
    {
        private readonly string _urlIconPath;

        protected override string Route => _urlIconPath;

        public GetRemoteSpriteRequest(string urlIconPath, CancellationTokenSource cancellationTokenSource)
        {
            _urlIconPath = urlIconPath;
            IternalCancellationTokenSource = cancellationTokenSource;
        }

        public override async UniTask Execute(IRequestSender requestSender, CancellationToken token)
        {
            var texture = await requestSender.GetRemoteTexture(Route, token);
            if (texture == null)
            {
                UnityEngine.Debug.LogError($"Request {GetType()} on route {Route} complete with null texture.");
                return;
            }

            var icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            OnResultDone.Execute(icon);
        }
    }
}
