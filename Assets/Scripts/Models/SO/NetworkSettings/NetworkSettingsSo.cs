using Sirenix.OdinInspector;
using UnityEngine;

namespace Models.SO.NetworkSettings
{
    [CreateAssetMenu(fileName = nameof(NetworkSettingsSo), menuName = "Settings/" + nameof(NetworkSettingsSo))]

    public class NetworkSettingsSo : SerializedScriptableObject, INetworkSettingSo
    {
        [SerializeField][Min(0f)] private float _weatherRepeateDelay;
        [SerializeField][Min(1)] private int _factContainersShowCount;

        public float WeatherRepeateDelay => _weatherRepeateDelay;
        public int FactContainersShowCount => _factContainersShowCount;
    }
}
