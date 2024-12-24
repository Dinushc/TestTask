using Models.MainMenuButtons;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Models.SO.MenuButtons
{
    [CreateAssetMenu(fileName = nameof(MenuButtonsDataSo), menuName = "UI/" + nameof(MenuButtonsDataSo))]

    public class MenuButtonsDataSo : SerializedScriptableObject, IMenuButtonsDataSo
    {
        [SerializeField] private Dictionary<string, MenuButtonData> _buttonDatas;

        public Dictionary<string, MenuButtonData> ButtonDatas => _buttonDatas;
    }
}
