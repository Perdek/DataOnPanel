using System;
using System.Collections.Generic;

namespace Presentation.InformationPanel.MVC.ViewElements
{
    public class CollectionPanel
    {
        #region MEMBERS

        private readonly List<InformationElement> _informationElements = new();
        private readonly InformationElement.Pool _informationElementPool;

        #endregion

        #region METHODS

        public CollectionPanel(InformationElement.Pool informationElementPool)
        {
            _informationElementPool = informationElementPool;
        }

        public void Refresh(IList<DataItem> data, int startIndex)
        {
            int existingCount = _informationElements.Count;
            int newCount = data.Count;

            for (int i = 0; i < Math.Min(existingCount, newCount); i++)
            {
                _informationElements[i].RefreshView(data[i], startIndex + i);
            }

            for (int i = existingCount - 1; i >= newCount; i--)
            {
                RemoveCollectionElement(i);
            }

            for (int i = existingCount; i < newCount; i++)
            {
                AddCollectionElement(data[i], startIndex + i);
            }
        }

        private void RemoveCollectionElement(int index)
        {
            _informationElementPool.Despawn(_informationElements[index]);
            _informationElements.RemoveAt(index);
        }

        private void AddCollectionElement(DataItem dataItem, int index)
        {
            InformationElement newInformationElement = _informationElementPool.Spawn();
            newInformationElement.RefreshView(dataItem, index);
            _informationElements.Add(newInformationElement);
        }

        #endregion
    }
}