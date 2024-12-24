using Cysharp.Threading.Tasks;
using DataSenders.Managers;
using DataSenders.Requests;
using Models.ServerAnswers.Weathers;
using Models.SO.NetworkSettings;
using System;
using System.Collections.Generic;
using System.Threading;
using UI.Abstractions.Controllers;
using UniRx;
using UnityEngine;
using Utils.AsyncExtensions;

namespace UI.MainScenes.WeatherPanels
{
    public class WeatherPanelController : UiController<WeatherPanelView>, IDisposable
    {
        private readonly IRequestsController _requestsController;
        private readonly int _getDataRepeatDelay;

        private CancellationTokenSource _repeatGetDataCancellationTokenSource;
        private CancellationTokenSource _cancellationTokenRequest;
        private IDisposable _disposable;

        private Dictionary<string, WeatherItem> _weatherItems = new();

        public WeatherPanelController(IRequestsController requestsController, INetworkSettingSo networkSettingSo)
        {
            _requestsController = requestsController;
            _getDataRepeatDelay = Mathf.RoundToInt(networkSettingSo.WeatherRepeateDelay * 1000);
        }

        public override void Show()
        {
            _repeatGetDataCancellationTokenSource = new();
            SendingRequests(_repeatGetDataCancellationTokenSource.Token).Forget();
            base.Show();
        }

        public override void Hide()
        {
            ClearRequest();
            CancelMainToken();
            base.Hide();
        }

        private async UniTaskVoid SendingRequests(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                ClearRequest();

                _cancellationTokenRequest = CancellationTokenSource
                    .CreateLinkedTokenSource(token);

                var weatherRequest = new GetWeatherRequest(_cancellationTokenRequest);
                _requestsController.AddRequest(weatherRequest);
                _disposable = weatherRequest.OnRequestDone.Subscribe(OnLoadWeatherData);

                await UniTask.Delay(_getDataRepeatDelay, cancellationToken: token); //задержка 5 сек
            }
        }

        private void OnLoadWeatherData(WeatherServerAnswer answer)
        {
            var length = answer.Properties.Periods.Count;
            if (length == 0)
            {
                Debug.LogError("Not found periods.");
                return;
            }
            
            var periodIndex = GetTodayIndex();
            
            for (int i = 0; i < length; i++)
            {
                var period = answer.Properties.Periods[i];

                var weatherInfo = $"{period.Name} {period.Temperature} {period.TemperatureUnit}";
                if(!_weatherItems.ContainsKey(period.Name))
                {
                    var item = View.GetForecastData(weatherInfo);
                    GetForecastIcon(item, period.Icon);
                    _weatherItems.Add(period.Name, item);
                }
                else
                {
                    //update info if changes
                    _weatherItems[period.Name].SetData(weatherInfo);
                    GetForecastIcon(_weatherItems[period.Name], period.Icon);
                }
                if (i == periodIndex)
                {
                    //set current weather view
                    _weatherItems[period.Name].SetAsCurrentForecast();
                }
            }
        }

        private async void GetForecastIcon(WeatherItem item, string iconUrl)
        {
            ClearRequest();
            _cancellationTokenRequest = CancellationTokenSource
                .CreateLinkedTokenSource(_repeatGetDataCancellationTokenSource.Token);
            await UniTask.Delay(200);
            var iconRequest = new GetRemoteSpriteRequest(iconUrl, _cancellationTokenRequest);
            _requestsController.AddRequest(iconRequest);
            item.SetIcon(iconRequest);
        }

        private int GetTodayIndex()
        {
            var currentDate = DateTime.Now;
            int dayOfWeekIndex = (int)currentDate.DayOfWeek;
            dayOfWeekIndex = (dayOfWeekIndex == 0) ? 6 : dayOfWeekIndex - 1;
            bool isNight = currentDate.Hour < 6 || currentDate.Hour >= 18;
            int periodIndex = dayOfWeekIndex * 2 - (isNight ? 1 : 0);
            return periodIndex;
        }

        private void ClearRequest()
        {
            if (_disposable != null)
            {
                _disposable.Dispose();
                _disposable = null;
            }
            _cancellationTokenRequest.TryCancel();
        }

        private void CancelMainToken()
        {
            _repeatGetDataCancellationTokenSource.TryCancel();
        }

        public void Dispose()
        {
            ClearRequest();
            CancelMainToken();
        }
    }
}