using MainScenes.FactPanels.FactDataPanels;
using UI.Extentions;
using UI.MainScenes.FactPanels.FactDataPanels;
using UI.MainScenes.FactPanels.MainPanels;
using UI.MainScenes.MainMunuPanels.MenuButtons;
using UI.MainScenes.MainMunuPanels.MenuHudControllers;
using UI.MainScenes.WeatherPanels;
using UI.MainScenes.Windows;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructures
{
    public class MainSceneUiInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;

        [Header("Panels")]
        [SerializeField] private WeatherPanelView _weatherPanelView;
        [FormerlySerializedAs("_factsPanelView")] [SerializeField] private FactsPanel factsPanel;
        [FormerlySerializedAs("_factDataPanelView")] [SerializeField] private FactDetailsView factDetailsView;
        [SerializeField] private MenuButtonsView _menuButtonsView;

        public override void InstallBindings()
        {
            BindPanels(_canvas);
            BindWindows();
            BindControllers();
        }

        private void BindControllers()
        {
            Container.BindInterfacesTo<MenuHudController>().AsSingle();
        }

        private void BindPanels(Canvas canvas)
        {
            Container.BindUiView<WeatherPanelController, WeatherPanelView>(_weatherPanelView, canvas.transform);

            Container.BindUiView<FactsPanelController, FactsPanel>(factsPanel, canvas.transform);
            Container.BindUiView<FactDataPanelController, FactDetailsView>(factDetailsView, canvas.transform);

            Container.BindUiView<MenuButtonsController, MenuButtonsView>(_menuButtonsView, canvas.transform, true);
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<WeatherWindow>().AsSingle();
            Container.BindInterfacesAndSelfTo<FactWindow>().AsSingle();
        }
    }
}
