using DataSenders.Managers;
using DataSenders.Senders;
using Zenject;

namespace Infrastructures
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<RequestSender>().AsSingle();
            Container.BindInterfacesTo<RequestsController>().AsSingle();
        }
    }
}
