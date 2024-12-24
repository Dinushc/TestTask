using System;
using System.Collections.Generic;
using UI.Abstractions.Windows;
using UI.MainScenes.MainMunuPanels.MenuButtons;
using UI.MainScenes.Windows;
using UniRx;
using Zenject;

namespace UI.MainScenes.MainMunuPanels.MenuHudControllers
{
    public class MenuHudController : IInitializable, IDisposable
    {
        private readonly DiContainer _diContainer;
        private readonly MenuButtonsController _menuButtonsController;

        private readonly CompositeDisposable _disposables = new();

        private int _currentPageIndex;
        private List<Action> _windowOpenActions = new();
        private List<IWindow> _windows = new();
        private ReactiveCommand<int> _onPageIndexChange = new();

        public IObservable<int> OnPageIndexChange => _onPageIndexChange;

        public MenuHudController(DiContainer diContainer, MenuButtonsController menuButtonsController)
        {
            _diContainer = diContainer;
            _menuButtonsController = menuButtonsController;
        }

        public void Initialize()
        {
            AddMenuWindow<WeatherWindow>();
            AddMenuWindow<FactWindow>();
            OpenPage<WeatherWindow>(0);

            _menuButtonsController.OnSwitchButton.Subscribe(PageSwitch).AddTo(_disposables);
        }

        private void AddMenuWindow<T>() where T : IWindow
        {
            var window = _diContainer.Resolve<T>();
            var numWindow = _windowOpenActions.Count;

            _windows.Add(window);
            Action action = () => OpenPage<T>(numWindow);
            _windowOpenActions.Add(action);
            _menuButtonsController.AddMenuButton<T>(window.Name);
        }

        private void OpenPage<T>(int index) where T : IWindow
        {
            _menuButtonsController.SwitchMenuButton(index);

            if (_currentPageIndex >= 0)
                _windows[_currentPageIndex].Close();

            _windows[index].Open();
            _onPageIndexChange.Execute(index);
        }

        public void PageSwitch(int newPageIndex)
        {
            _windowOpenActions[newPageIndex]();
            _currentPageIndex = newPageIndex;
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
