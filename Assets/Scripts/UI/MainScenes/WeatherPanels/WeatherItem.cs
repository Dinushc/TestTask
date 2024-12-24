using System;
using DataSenders.Requests;
using TMPro;
using UI.Abstractions.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScenes.WeatherPanels
{
    public class WeatherItem : UiView, IDisposable
    {
        [SerializeField] private TextMeshProUGUI _weatherText;
        [SerializeField] private Image _weatherImage;
        private IDisposable _disposable;

        public void SetData(string weatherInfo)
        {
            _weatherText.text = weatherInfo;
        }

        public void SetIcon(GetRemoteSpriteRequest request)
        {
            _disposable = request.OnRequestDone.Subscribe(SetForecastIcon);
        }

        private void SetForecastIcon(Sprite icon)
        {
            _weatherImage.sprite = icon;
        }

        public void SetAsCurrentForecast()
        {
            _weatherText.color = Color.green;
            _weatherText.text += " Current";
        }

        public void Dispose()
        {
            if (_disposable != null)
            {
                _disposable.Dispose();
                _disposable = null;
            }
        }
    }
}