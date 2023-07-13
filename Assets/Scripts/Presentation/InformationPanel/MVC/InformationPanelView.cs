using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.InformationPanel.MVC.ViewElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Presentation.InformationPanel
{
    [Serializable]

    public class InformationPanelView
    {
        #region MEMBERS

        [Header("Buttons")]
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;

        [SerializeField] private LoadingPanel _loadingPanel;
        [SerializeField] private CollectionPanel _collectionPanel;

        #endregion
        
        #region METHODS

        public void Initialize()
        {
            InitializeLoadingAnimation();
        }

        public void AddListenerToPreviousButton(UnityAction onClick)
        {
            _previousButton.onClick.AddListener(onClick);
        }

        public void AddListenerToNextButton(UnityAction onClick)
        {
            _nextButton.onClick.AddListener(onClick);
        }
        
        public void Refresh(IList<DataItem> data)
        {
            _collectionPanel.Refresh(data);
        }

        private void InitializeLoadingAnimation()
        {
            _loadingPanel.InitializeRotationAnimation();
        }

        #endregion
    }
}
