using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IDataServer>().To<DataServerMock>().AsSingle();
        }
    }
}