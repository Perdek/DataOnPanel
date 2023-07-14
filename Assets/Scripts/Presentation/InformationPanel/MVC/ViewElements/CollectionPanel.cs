using System;
using System.Collections.Generic;
using UnityEngine;

namespace Presentation.InformationPanel.MVC.ViewElements
{
    [Serializable]
    public class CollectionPanel
    {
        #region MEMBERS

        [SerializeField] private Transform _collectionElementParent;

        private List<InformationElement> _informationElements = new List<InformationElement>();
        private InformationElement.Factory _informationElementFactory;

        #endregion

        #region METHODS

        public void InjectDependencies(InformationElement.Factory informationElementFactory)
        {
            _informationElementFactory = informationElementFactory;
        }

        public void Refresh(IList<DataItem> data, int startIndex)
        {
            ClearCollection();
            FillCollection(data, startIndex);
        }

        private void ClearCollection()
        {
            for (int i = _informationElements.Count - 1; i >= 0; i--)
            {
                RemoveCollectionElement(i);
            }
        }

        private void RemoveCollectionElement(int index)
        {
            GameObject.Destroy(_informationElements[index].gameObject);
            _informationElements.RemoveAt(index);
        }

        private void FillCollection(IList<DataItem> data, int startIndex)
        {
            for (int i = 0; i < data.Count; i++)
            {
                AddCollectionElement(data[i], startIndex + i);   
            }
        }

        private void AddCollectionElement(DataItem dataItem, int index)
        {
            InformationElement newInformationElement = _informationElementFactory.Create();
            newInformationElement.transform.parent = _collectionElementParent;
            newInformationElement.RefreshView(dataItem, index);
            _informationElements.Add(newInformationElement);
        }

        #endregion
    }
}