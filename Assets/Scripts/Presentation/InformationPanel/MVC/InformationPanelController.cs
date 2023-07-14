using System.Threading;
using System.Threading.Tasks;
using Presentation.InformationPanel.MVC.ViewElements;
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
            RefreshPageView();
        }

        #endregion

        #region METHODS
        
        [Inject]
        private void InjectDependencies(IDataServer dataServer, InformationElement.Factory informationElementFactory)
        {
            _model.InjectDependencies(dataServer);
            _view.InjectDependencies(informationElementFactory);
        }

        private void Initialize()
        {
            _cancellationTokenForDataCountRequest = new CancellationToken();
            _cancellationTokenForDataRequest = new CancellationToken();
            _view.Initialize();

            _view.RefreshStartUpView();
            
            _view.AddListenerToNextButton(NextPage);
            _view.AddListenerToPreviousButton(PrevPage);
        }

        private void NextPage()
        {
            _model.NextPage();
            RefreshPageView();
        }

        private void PrevPage()
        {
            _model.PrevPage();
            RefreshPageView();
        }

        private async Task RefreshModel()
        {
            await _model.RefreshDataAvailableCount(_cancellationTokenForDataCountRequest);
            await _model.RequestData(_cancellationTokenForDataRequest);
        }

        private void RefreshPageView()
        {
            _view.RefreshViewOnLoadedData(_model.GetDataCurrentPage());
            _view.RefreshButtons(_model.ChangingPageIsPossible());
        }

        #endregion
    }
}
