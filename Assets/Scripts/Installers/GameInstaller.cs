using Presentation.InformationPanel.MVC.ViewElements;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        #region MEMBERS

        [SerializeField] private InformationElement _informationElementPrefab;

        #endregion

        #region METHODS

        public override void InstallBindings()
        {
            Container.Bind<IDataServer>().To<DataServerMock>().AsSingle();
            Container.BindFactory<InformationElement, InformationElement.Factory>().FromComponentInNewPrefab(_informationElementPrefab);
        }

        #endregion
    }
}