using Presentation.InformationPanel.MVC;
using Presentation.InformationPanel.MVC.ViewElements;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InformationPanelInstaller : MonoInstaller
    {
        #region MEMBERS

        [SerializeField] private InformationElement _informationElementPrefab;
        [SerializeField] private Transform _informationElementsParent;
        [SerializeField] private InformationPanelView _informationPanelView;

        #endregion

        #region METHODS

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InformationPanelController>().AsSingle().WithArguments(_informationPanelView);
            Container.BindMemoryPool<InformationElement, InformationElement.Pool>().FromComponentInNewPrefab(_informationElementPrefab).UnderTransform(_informationElementsParent);
            Container.Bind<InformationPanelModel>().AsSingle();
            Container.Bind<CollectionPanel>().AsTransient();
        }

        #endregion
    }
}