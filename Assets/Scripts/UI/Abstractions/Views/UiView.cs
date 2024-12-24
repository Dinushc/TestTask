using UnityEngine;

namespace UI.Abstractions.Views
{
    public class UiView : MonoBehaviour
    {
        [field: SerializeField] public bool AutoShow { get; private set; }

        public virtual void Show()
        {
            gameObject?.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject?.SetActive(false);
        }

    }
}
