using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Presentation.InformationPanel
{
    [System.Serializable]
    public class InformationPanelController : MonoBehaviour
    {
        #region MEMBERS

        [SerializeField] private InformationPanelView _view;
        [SerializeField] private InformationPanelModel _model;

        private CancellationToken _cancellationTokenForDataCountRequest;
        private CancellationToken _cancellationTokenForDataRequest;

        #endregion

        #region UNITY_METHODS

        private void Awake()
        {
            Initialize();
        }

        private async void Start()
        {
            await RefreshModel();
            RefreshView();
        }

        #endregion

        #region METHODS

        [Inject]
        private void InjectDependencies(IDataServer dataServer)
        {
            _model.InjectDependencies(dataServer);
        }

        private void Initialize()
        {
            _cancellationTokenForDataCountRequest = new CancellationToken();
            _cancellationTokenForDataRequest = new CancellationToken();
            _view.Initialize();
        }

        private async Task RefreshModel()
        {
            await _model.RefreshDataAvailableCount(_cancellationTokenForDataCountRequest);
            await _model.RequestData(_cancellationTokenForDataRequest);
        }

        private void RefreshView()
        {
            _view.Refresh(_model.GetDataSinglePage());
        }

        #endregion
    }
}
