using System;
using System.Collections.Generic;
using Presentation.InformationPanel.MVC.ViewElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Presentation.InformationPanel.MVC
{
    [Serializable]

    public class InformationPanelView : MonoBehaviour
    {
        #region MEMBERS

        [Header("Buttons")]
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;

        [SerializeField] private LoadingPanel _loadingPanel;
        private CollectionPanel _collectionPanel;

        #endregion

        #region METHODS

        [Inject]
        public void InjectDependencies(CollectionPanel collectionPanel)
        {
            _collectionPanel = collectionPanel;
        }

        public void Initialize()
        {
            _loadingPanel.InitializeRotationAnimation();
        }

        public void AddListenerToPreviousButton(UnityAction onClick)
        {
            _previousButton.onClick.AddListener(onClick);
        }

        public void AddListenerToNextButton(UnityAction onClick)
        {
            _nextButton.onClick.AddListener(onClick);
        }

        public void RefreshViewOnLoadedData(IList<DataItem> data, int startIndex)
        {
            _loadingPanel.TurnOffLoadingAnimation();
            _collectionPanel.Refresh(data, startIndex);
        }

        public void RefreshStartUpView()
        {
            _loadingPanel.TurnOnLoadingAnimation();

            _nextButton.interactable = false;
            _previousButton.interactable = false;
        }

        public void RefreshButtons((bool prevPageIsPossible, bool nextPageIsPossible) changingPageIsPossible)
        {
            _nextButton.interactable = changingPageIsPossible.nextPageIsPossible;
            _previousButton.interactable = changingPageIsPossible.prevPageIsPossible;
        }

        #endregion
    }
}