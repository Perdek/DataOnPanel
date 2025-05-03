using System;
using System.Threading;
using System.Threading.Tasks;
using Zenject;

namespace Presentation.InformationPanel.MVC
{
    [Serializable]
    public class InformationPanelController : IInitializable, IDisposable
    {
        #region MEMBERS

        private InformationPanelView _view;
        private InformationPanelModel _model;

        private CancellationTokenSource _cancellationTokenForDataCountRequest;
        private CancellationTokenSource _cancellationTokenForDataRequest;

        #endregion

        #region ZENJECT_METHODS

        public void Initialize()
        {
            _cancellationTokenForDataCountRequest = new CancellationTokenSource();
            _cancellationTokenForDataRequest = new CancellationTokenSource();

            _view.Initialize();
            _view.RefreshStartUpView();

            _view.AddListenerToNextButton(NextPage);
            _view.AddListenerToPreviousButton(PrevPage);

            RefreshData().ConfigureAwait(false);
        }

        public void Dispose()
        {
            _cancellationTokenForDataCountRequest.Cancel();
            _cancellationTokenForDataRequest.Cancel();
        }

        #endregion

        #region METHODS

        private InformationPanelController(InformationPanelView view, InformationPanelModel model)
        {
            _view = view;
            _model = model;
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

        private async Task RefreshData()
        {
            await _model.RefreshDataAvailableCount(_cancellationTokenForDataCountRequest.Token);
            await _model.RequestData(_cancellationTokenForDataRequest.Token);

            RefreshPageView();
        }

        private void RefreshPageView()
        {
            _view.RefreshViewOnLoadedData(_model.GetDataCurrentPage(), _model.GetFirstElementIndexInPage());
            _view.RefreshButtons(_model.ChangingPageIsPossible());
        }

        #endregion
    }
}