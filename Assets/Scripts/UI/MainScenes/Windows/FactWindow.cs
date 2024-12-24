using Abstractions.Windows;
using MainScenes.FactPanels.FactDataPanels;
using UI.MainScenes.FactPanels.MainPanels;

namespace UI.MainScenes.Windows
{
    public class FactWindow : Window
    {
        private FactsPanelController _factsPanelController;
        private FactDataPanelController _factDataPanelController;

        public FactWindow(FactsPanelController factsPanelController, FactDataPanelController factDataPanelController)
        {
            _factsPanelController = factsPanelController;
            _factDataPanelController = factDataPanelController;
        }

        public override void Initialize()
        {
            AddController(_factsPanelController);
            AddController(_factDataPanelController);
        }
    }
}
