using System;
using DG.Tweening;
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

        #endregion
        
        #region METHODS

        public void Initialize()
        {
            InitializeLoadingAnimation();
        }

        public void Refresh(object getDataForPanel)
        {
            
        }

        public void AddListenerToPreviousButton(UnityAction onClick)
        {
            _previousButton.onClick.AddListener(onClick);
        }

        public void AddListenerToNextButton(UnityAction onClick)
        {
            _nextButton.onClick.AddListener(onClick);
        }

        private void InitializeLoadingAnimation()
        {
            _loadingPanel.InitializeRotationAnimation();
        }

        #endregion
    }
}
