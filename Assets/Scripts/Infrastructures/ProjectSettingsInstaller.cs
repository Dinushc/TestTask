using Models.SO.MenuButtons;
using Models.SO.NetworkSettings;
using UnityEngine;
using Zenject;

namespace Infrastructures
{
    [CreateAssetMenu(fileName = nameof(ProjectSettingsInstaller),
        menuName = "Custom Installers/" + nameof(ProjectSettingsInstaller))]
    public class ProjectSettingsInstaller : ScriptableObjectInstaller<ProjectSettingsInstaller>
    {
        [SerializeField] private MenuButtonsDataSo _menuButtonsDataSo;
        [SerializeField] private NetworkSettingsSo _networkSettingsSo;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<NetworkSettingsSo>().FromInstance(_networkSettingsSo).AsSingle();
            Container.BindInterfacesTo<MenuButtonsDataSo>().FromInstance(_menuButtonsDataSo).AsSingle();
        }
    }
}
