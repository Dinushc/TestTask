using Abstractions.Windows;
using UI.MainScenes.WeatherPanels;

namespace UI.MainScenes.Windows
{
    public class WeatherWindow : Window
    {
        private readonly WeatherPanelController _weatherPanelController;

        public WeatherWindow(WeatherPanelController weatherPanelController)
        {
            _weatherPanelController = weatherPanelController;
        }

        public override void Initialize()
        {
            AddController(_weatherPanelController);
        }
    }
}
