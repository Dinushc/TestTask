using Sirenix.OdinInspector;
using UnityEngine;

namespace Models.MainMenuButtons
{
    public class MenuButtonData
    {
        [HorizontalGroup("Group 1", LabelWidth = 40)]
        public string Text;

        [HorizontalGroup("Group 1", LabelWidth = 40)]
        [PreviewField]
        public Sprite Icon;

    }
}