using System;
using System.Threading;
using System.Threading.Tasks;
using Configuration.InformationPanel;
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

        private CancellationTokenSource _cancellationTokenForDataCountRequest;
        private CancellationTokenSource _cancellationTokenForDataRequest;

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

        private void OnDestroy()
        {
            _cancellationTokenForDataCountRequest.Cancel();
            _cancellationTokenForDataRequest.Cancel();
        }

        #endregion

        #region METHODS
        
        [Inject]
        private void InjectDependencies(IDataServer dataServer, InformationElement.Pool informationElementPool, InformationPanelConfiguration informationPanelConfiguration)
        {
            _model.InjectDependencies(dataServer, informationPanelConfiguration);
            _view.InjectDependencies(informationElementPool);
        }

        private void Initialize()
        {
            _cancellationTokenForDataCountRequest = new CancellationTokenSource();
            _cancellationTokenForDataRequest = new CancellationTokenSource();
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
            await _model.RefreshDataAvailableCount(_cancellationTokenForDataCountRequest.Token);
            await _model.RequestData(_cancellationTokenForDataRequest.Token);
        }

        private void RefreshPageView()
        {
            _view.RefreshViewOnLoadedData(_model.GetDataCurrentPage(), _model.GetFirstElementIndexInPage());
            _view.RefreshButtons(_model.ChangingPageIsPossible());
        }

        #endregion
    }
}
