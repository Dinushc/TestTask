using UI.Abstractions.Views;
using UnityEngine;
using Zenject;

namespace UI.Abstractions.Controllers
{
    public class UiController<T> : IUiController where T : UiView
    {
        [Inject] protected readonly T View;

        public bool IsAutoShow => View.AutoShow;

        public virtual void Show()
        {
            if (ReferenceEquals(View, null))
            {
                Debug.LogError($"View is null. type: {GetType().Name}");
                return;
            }
            View?.Show();
        }

        public virtual void Hide()
        {
            if (ReferenceEquals(View, null))
            {
                Debug.LogError($"View is null. type: {GetType().Name}");
                return;
            }
            View?.Hide();
        }
    }
}
