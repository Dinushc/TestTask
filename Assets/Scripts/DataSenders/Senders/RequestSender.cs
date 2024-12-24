using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace DataSenders.Senders
{
    public class RequestSender : IRequestSender
    {
        public async UniTask<string> GetInfo(string route, CancellationToken token)
        {
            var result = string.Empty;
            using (var request = UnityWebRequest.Get(route))
            {
                var asyncRequest = await request.SendWebRequest().WithCancellation(token);
                if (asyncRequest.result == UnityWebRequest.Result.ConnectionError
                    || asyncRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log($"Request json error: {request.error}");
                    result = string.Empty;
                }
                else
                {
                    result = asyncRequest.downloadHandler.text;
                }

                request.Dispose();
            }
            return result;
        }

        public async UniTask<Texture2D> GetRemoteTexture(string url, CancellationToken token)
        {
            Texture2D result = null;
            using (var request = UnityWebRequestTexture.GetTexture(url))
            {
                var asyncRequest = await request.SendWebRequest().WithCancellation(token);

                if (asyncRequest.result == UnityWebRequest.Result.ConnectionError
                    || asyncRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log($"Request load image error: {request.error}");
                    result = null;
                }
                else
                {
                    result = DownloadHandlerTexture.GetContent(asyncRequest);
                }

                request.Dispose();
            }

            return result;
        }
    }
}
