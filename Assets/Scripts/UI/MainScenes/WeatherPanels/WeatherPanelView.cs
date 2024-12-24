using TMPro;
using UI.Abstractions.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScenes.WeatherPanels
{
    public class WeatherPanelView : UiView
    {
        [SerializeField] private WeatherItem _weatherItem;
        [SerializeField] private Transform _content;

        public WeatherItem GetForecastData(string forecastInfo)
        {
            var item = Instantiate(_weatherItem, _content);
            item.SetData(forecastInfo);
            return item;
        }
    }
}
