using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presentation.InformationPanel.MVC.ViewElements
{
    public class InformationElement : MonoBehaviour
    {
        #region MEMBERS

        [SerializeField] private GameObject _glowEffectImage;
        [SerializeField] private Image _categoryImage;
        [SerializeField] private TMPro.TMP_Text _indexText;
        [SerializeField] private TMPro.TMP_Text _nameText;

        #endregion


        #region FACTORY

        public class Factory : PlaceholderFactory<InformationElement>
        {
            
        }

        #endregion
    }
}