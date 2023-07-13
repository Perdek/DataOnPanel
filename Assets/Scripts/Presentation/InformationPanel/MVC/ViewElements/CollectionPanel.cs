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
        [SerializeField] private InformationElement _collectionElementPrefab;

        private List<InformationElement> _informationElements = new List<InformationElement>();

        #endregion

        #region METHODS

        public void Refresh(IList<DataItem> data)
        {
            ClearCollection();
            FillCollection(data);
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

        private void FillCollection(IList<DataItem> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                AddCollectionElement(data[i]);   
            }
        }

        private void AddCollectionElement(DataItem dataItem)
        {
            //spawn by zenject
        }

        #endregion
    }
}