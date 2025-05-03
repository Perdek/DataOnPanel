using Configuration.InformationPanel;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "InformationPanelConfigurationInstaller", menuName = "Installers/InformationPanelConfigurationInstaller")]
    public class InformationPanelConfigurationInstaller : ScriptableObjectInstaller<InformationPanelConfigurationInstaller>
    {
        #region MEMBERS

        [SerializeField] private InformationPanelConfiguration _informationPanelConfiguration;

        #endregion

        #region METHODS

        public override void InstallBindings()
        {
            Container.BindInstance(_informationPanelConfiguration);
        }

        #endregion
    }
}