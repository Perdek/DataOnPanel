using Presentation.InformationPanel.MVC.ViewElements;
using UnityEngine;
using Zenject;

public class InformationPanelInstaller : MonoInstaller
{
    #region MEMBERS

    [SerializeField] private InformationElement _informationElementPrefab;
    [SerializeField] private Transform _informationElementsParent;

    #endregion

    #region METHODS

    public override void InstallBindings()
    {
        Container.BindMemoryPool<InformationElement, InformationElement.Pool>().FromComponentInNewPrefab(_informationElementPrefab).UnderTransform(_informationElementsParent);
    }

    #endregion
}