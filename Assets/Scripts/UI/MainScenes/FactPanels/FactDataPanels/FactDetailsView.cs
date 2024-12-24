using TMPro;
using UI.Abstractions.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScenes.FactPanels.FactDataPanels
{
    public class FactDetailsView : UiView
    {
        [field: SerializeField] public TMP_Text MainLabel { get; private set; }
        [field: SerializeField] public TMP_Text Description { get; private set; }
        [field: SerializeField] public Button CloseButton { get; private set; }
        [field: SerializeField] public Button DimmedButton { get; private set; }
        [field: SerializeField] public ScrollRect ScrollRect { get; private set; }

        [SerializeField] private float _scrollDefaultVerticalPosition;

        public void SetDefaultScrollPosition()
        {
            ScrollRect.verticalNormalizedPosition = _scrollDefaultVerticalPosition;
        }
    }
}
