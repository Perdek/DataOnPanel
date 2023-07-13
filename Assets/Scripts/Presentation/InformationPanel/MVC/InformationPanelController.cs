using UnityEngine;

namespace Presentation.InformationPanel
{
    [System.Serializable]
    public class InformationPanelController : MonoBehaviour
    {
        #region MEMBERS

        [SerializeField] private InformationPanelView _view;
        [SerializeField] private InformationPanelModel _model;

        #endregion

        #region UNITY_METHODS

        private void Awake()
        {
            Initialize();
            RefreshView();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            _view.Initialize();
        }

        private void RefreshView()
        {
            //_view.Refresh(_model.GetDataForPanel());
        }

        #endregion
    }
}
