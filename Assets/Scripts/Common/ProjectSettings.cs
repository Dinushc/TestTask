using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Assets.Scripts.Common
{
    public class ProjectSettings
    {
        public class Common
        {
            public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,

                Error = delegate (object sender, ErrorEventArgs args)
                {
                    UnityEngine.Debug.LogError(args.ErrorContext.Error.Message);
                    args.ErrorContext.Handled = true;
                }
            };
        }

        public class Debug
        {
            public const float DelayBeforeSendRequest = 0f;
        }
    }
}
