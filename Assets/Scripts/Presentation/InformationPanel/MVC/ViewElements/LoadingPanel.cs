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

        private Tween _rotationTween;

        #endregion

        #region UNITY_METHODS

        private void OnDestroy()
        {
            Cleanup();
        }

        #endregion

        #region METHODS

        public void InitializeRotationAnimation()
        {
            _rotationTween = _loadingGameObject.transform.DORotate(new Vector3(0, 0, 360), _rotationTime)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1)
                .Pause()
                .SetAutoKill(false);
        }

        public void TurnOnLoadingAnimation()
        {
            _loadingGameObject.SetActive(true);
            _rotationTween?.Play();
        }

        public void TurnOffLoadingAnimation()
        {
            _rotationTween?.Pause();
            _loadingGameObject.SetActive(false);
        }

        public void Cleanup()
        {
            _rotationTween?.Kill();
            _rotationTween = null;
        }

        #endregion
    }
}