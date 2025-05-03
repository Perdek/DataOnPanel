using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Configuration.InformationPanel;
using UnityEngine;

namespace Presentation.InformationPanel.MVC
{
    public class InformationPanelModel
    {
        #region MEMBERS

        private readonly IDataServer _dataServer;
        private readonly InformationPanelConfiguration _informationPanelConfiguration;

        private List<DataItem> _availableDataCollection = new();
        private int _availableDataCount;
        private int _pageIndex;
        private int _firstVisibleCollectionElementIndex;

        #endregion

        #region METHODS

        public InformationPanelModel(IDataServer dataServer, InformationPanelConfiguration informationPanelConfiguration)
        {
            _dataServer = dataServer;
            _informationPanelConfiguration = informationPanelConfiguration;
        }

        public async Task RequestData(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                IList<DataItem> requestedData = await _dataServer.RequestData(_pageIndex, _availableDataCount, cancellationToken);

                _availableDataCollection = requestedData.ToList();
            }
            catch (OperationCanceledException exception)
            {
                Debug.LogError(exception);
            }
        }

        public async Task RefreshDataAvailableCount(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                _availableDataCount = await _dataServer.DataAvailable(cancellationToken);
            }
            catch (OperationCanceledException exception)
            {
                Debug.LogError(exception);
            }
        }

        public IList<DataItem> GetDataCurrentPage()
        {
            int startIndex = _firstVisibleCollectionElementIndex;
            int count = Mathf.Min(_informationPanelConfiguration.MaxElementCount, _availableDataCollection.Count - startIndex);

            return _availableDataCollection.GetRange(_firstVisibleCollectionElementIndex, count);
        }

        public void NextPage()
        {
            if ((_pageIndex + 1) * _informationPanelConfiguration.MaxElementCount > _availableDataCount)
            {
                return;
            }

            _pageIndex++;
            _firstVisibleCollectionElementIndex += _informationPanelConfiguration.MaxElementCount;
        }

        public void PrevPage()
        {
            if (_pageIndex == 0)
            {
                return;
            }

            _pageIndex--;
            _firstVisibleCollectionElementIndex -= _informationPanelConfiguration.MaxElementCount;
        }

        public (bool prevPageIsPossible, bool nextPageIsPossible) ChangingPageIsPossible()
        {
            bool nextPageIsPossible = (_pageIndex + 1) * _informationPanelConfiguration.MaxElementCount < _availableDataCount;
            bool prevPageIsPossible = _pageIndex > 0;

            return (prevPageIsPossible, nextPageIsPossible);
        }


        /// <summary>Returning index of first element, but order is not count from 0</summary>

        public int GetFirstElementIndexInPage()
        {
            return _firstVisibleCollectionElementIndex + 1;
        }

        #endregion
    }
}