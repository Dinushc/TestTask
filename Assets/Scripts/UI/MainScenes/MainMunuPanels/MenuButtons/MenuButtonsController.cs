using Models.SO.MenuButtons;
using System;
using System.Collections.Generic;
using UI.Abstractions.Controllers;
using UI.Abstractions.Windows;
using UniRx;
using UnityEngine.UI;

namespace UI.MainScenes.MainMunuPanels.MenuButtons
{
    public class MenuButtonsController : UiController<MenuButtonsView>, IDisposable
    {
        private readonly IMenuButtonsDataSo _menuButtonsData;

        private readonly List<MenuButton> _menuButtons = new();
        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveCommand<int> _onSwitchButton = new();

        private MenuButton _currentButton;

        public IObservable<int> OnSwitchButton => _onSwitchButton;

        public MenuButtonsController(IMenuButtonsDataSo menuButtonsData)
        {
            _menuButtonsData = menuButtonsData;
        }

        public void AddMenuButton<T>(string id)
            where T : IWindow
        {
            var buttonView = UnityEngine.Object.Instantiate(View.MenuButtonPrefab, View.Content);
            var buttonIndex = _menuButtons.Count;

            _menuButtons.Add(buttonView);
            if (_menuButtonsData.ButtonDatas.TryGetValue(id, out var data))
            {
                buttonView.ButtonName.text = data.Text;
            }
            else
            {
                UnityEngine.Debug.LogError("Button data not found.");
            }

            buttonView.Button.OnClickAsObservable()
                .Subscribe(_ => _onSwitchButton.Execute(buttonIndex))
                .AddTo(_disposables);

            LayoutRebuilder.ForceRebuildLayoutImmediate(View.Content);
        }

        public void SwitchMenuButton(int index)
        {
            if (_menuButtons[index] == _currentButton)
                return;

            _menuButtons[index].OnSelect();

            if (_currentButton != null)
                _currentButton.OnDiselect();

            _currentButton = _menuButtons[index];
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
