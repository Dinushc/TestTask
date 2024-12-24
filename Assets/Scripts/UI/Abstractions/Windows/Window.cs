using System.Collections.Generic;
using UI.Abstractions.Controllers;
using UI.Abstractions.Windows;
using Zenject;

namespace Abstractions.Windows
{
    public abstract class Window : IWindow, IInitializable
    {
        private readonly List<IUiController> _controllers = new();

        public string Name => GetType().Name;

        public abstract void Initialize();

        public void Open()
        {
            foreach (var controller in _controllers)
            {
                if (controller.IsAutoShow)
                    controller.Show();
            }
        }

        public void Close()
        {
            foreach (var controller in _controllers)
            {
                controller.Hide();
            }
        }

        protected void AddController<T>(T controller)
            where T : IUiController
        {
            _controllers.Add(controller);
        }
    }
}
