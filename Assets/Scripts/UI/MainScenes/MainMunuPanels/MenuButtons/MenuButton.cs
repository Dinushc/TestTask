using TMPro;
using UI.Abstractions.Views;
using UnityEngine.UI;

namespace UI.MainScenes.MainMunuPanels.MenuButtons
{
    public class MenuButton : UiView
    {
        public Button Button;
        public TMP_Text ButtonName;

        public void OnSelect()
        {
            Button.interactable = false;
        }

        public void OnDiselect()
        {
            Button.interactable = true;
        }
    }
}
