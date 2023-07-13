using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.InformationPanel
{
    [Serializable]
    public class InformationPanelModel
    {
        #region MEMBERS

        private IDataServer _dataServer;
        private IList<DataItem> _availableDataCollection = new List<DataItem>();
        private int _availableDataCount;
        private int _pageIndex;

        #endregion

        #region METHODS
        
        public void InjectDependencies(IDataServer dataServer)
        {
            _dataServer = dataServer;
        }

        public async Task RequestData(CancellationToken cancellationToken)
        {
            _availableDataCollection = await _dataServer.RequestData(_pageIndex, _availableDataCount, cancellationToken);
        }

        public async Task RefreshDataAvailableCount(CancellationToken cancellationToken)
        {
            _availableDataCount = await _dataServer.DataAvailable(cancellationToken);
        }
        
        public IList<DataItem> GetDataSinglePage()
        {
            throw new NotImplementedException();
        }

        public void NextPage()
        {
            _pageIndex++;
        }

        public void PrevPage()
        {
            _pageIndex--;
        }

        #endregion
    }
}