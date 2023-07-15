using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Presentation.InformationPanel
{
    [Serializable]
    public class InformationPanelModel
    {
        #region MEMBERS

        private IDataServer _dataServer;
        private List<DataItem> _availableDataCollection = new List<DataItem>();
        private int _availableDataCount;
        private int _pageIndex;
        private int _firstVisibleCollectionElementIndex;
        
        private const int ELEMENTS_COUNT_ON_PAGE = 5;

        #endregion

        #region METHODS
        
        public void InjectDependencies(IDataServer dataServer)
        {
            _dataServer = dataServer;
        }

        public async Task RequestData(CancellationToken cancellationToken)
        {
            //prepare for cancellation token
            var requestedData = await _dataServer.RequestData(_pageIndex, _availableDataCount, cancellationToken);

            _availableDataCollection = requestedData.ToList();
        }

        public async Task RefreshDataAvailableCount(CancellationToken cancellationToken)
        {
            _availableDataCount = await _dataServer.DataAvailable(cancellationToken);
        }
        
        public IList<DataItem> GetDataCurrentPage()
        {
            int startIndex = _firstVisibleCollectionElementIndex;
            int count = Mathf.Min(ELEMENTS_COUNT_ON_PAGE, _availableDataCollection.Count - startIndex);
            
            return _availableDataCollection.GetRange(_firstVisibleCollectionElementIndex, count);
        }

        public void NextPage()
        {
            if ((_pageIndex + 1) * ELEMENTS_COUNT_ON_PAGE > _availableDataCount)
            {
                return;
            }
            
            _pageIndex++;
            _firstVisibleCollectionElementIndex += ELEMENTS_COUNT_ON_PAGE;
        }

        public void PrevPage()
        {
            if (_pageIndex == 0)
            {
                return;
            }
            
            _pageIndex--;
            _firstVisibleCollectionElementIndex -= ELEMENTS_COUNT_ON_PAGE;
        }

        public (bool prevPageIsPossible, bool nextPageIsPossible) ChangingPageIsPossible()
        {
            bool nextPageIsPossible = (_pageIndex + 1) * ELEMENTS_COUNT_ON_PAGE < _availableDataCount;
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