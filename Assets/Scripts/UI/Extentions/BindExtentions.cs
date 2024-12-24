using UI.Abstractions.Controllers;
using UI.Abstractions.Views;
using UnityEngine;
using Zenject;

namespace UI.Extentions
{
    public static class BindExtentions
    {
        public static void BindUiView<TController, TView>
            (
                this DiContainer container,
                TView viewPrefab,
                Transform parent,
                bool isStartActive = false
            )
            where TController : IUiController
            where TView : UiView
        {
            container
                .BindInterfacesAndSelfTo<TController>()
                .AsSingle();

            var view = Object.Instantiate(viewPrefab, parent);
            container.BindInstance(view);

            if (!isStartActive)
                view.gameObject.SetActive(false);
        }
    }
}
