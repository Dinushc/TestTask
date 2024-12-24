using Models.MainMenuButtons;
using System.Collections.Generic;

namespace Models.SO.MenuButtons
{
    public interface IMenuButtonsDataSo
    {
        Dictionary<string, MenuButtonData> ButtonDatas { get; }
    }
}
