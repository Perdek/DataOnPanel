using System;
using DG.Tweening;
using UnityEngine;

namespace Presentation.InformationPanel.MVC.ViewElements
{
    [Serializable]
    public class LoadingPanel
    {
        #region MEMBERS

        [Header("Loading object")]
        [SerializeField] private GameObject _loadingGameObject;
        [SerializeField] private float _rotationTime = 1;

        #endregion

        #region METHODS

        public void InitializeRotationAnimation()
        {
            _loadingGameObject.transform.DORotate(new Vector3(0, 0, 360),
                _rotationTime).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
        }
        
        public void TurnOnLoadingAnimation()
        {
            _loadingGameObject.SetActive(true);
        }
        
        public void TurnOffLoadingAnimation()
        {
            _loadingGameObject.SetActive(false);
        }

        #endregion
    }
}