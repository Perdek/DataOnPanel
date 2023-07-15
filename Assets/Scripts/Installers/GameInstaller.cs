using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        #region METHODS

        public override void InstallBindings()
        {
            Container.Bind<IDataServer>().To<DataServerMock>().AsSingle();
        }

        #endregion
    }
}