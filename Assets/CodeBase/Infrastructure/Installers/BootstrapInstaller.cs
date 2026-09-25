using CodeBase.Network.Services;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ServerMessageService>().AsSingle();
            Container.BindInterfacesTo<ClientMessageService>().AsSingle();
        }
    }
}