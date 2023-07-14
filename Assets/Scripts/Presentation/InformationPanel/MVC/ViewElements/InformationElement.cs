using System;
using UnityEngine;
using Zenject;

namespace Presentation.InformationPanel.MVC.ViewElements
{
    public class InformationElement : MonoBehaviour
    {
        #region MEMBERS

        [Header("Categories")]
        [SerializeField] private GameObject _redCategoryImage;
        [SerializeField] private GameObject _blueCategoryImage;
        [SerializeField] private GameObject _greenCategoryImage;
        
        [Space]
        [SerializeField] private GameObject _glowEffectImage;
        [SerializeField] private TMPro.TMP_Text _indexText;
        [SerializeField] private TMPro.TMP_Text _nameText;

        #endregion

        #region METHODS

        public void RefreshView(DataItem dataItem, int index)
        {
            RefreshCategory(dataItem.Category);
            RefreshGlowEffect(dataItem.Special);
            RefreshDescription(dataItem.Description);
            RefreshIndex(index);
        }

        private void RefreshIndex(int index)
        {
            _indexText.text = index.ToString();
        }

        private void RefreshDescription(string dataItemDescription)
        {
            _nameText.text = dataItemDescription;
        }

        private void RefreshGlowEffect(bool isSpecial)
        {
            _glowEffectImage.SetActive(isSpecial);
        }

        private void RefreshCategory(DataItem.CategoryType dataItemCategory)
        {
            _redCategoryImage.SetActive(false);
            _greenCategoryImage.SetActive(false);
            _blueCategoryImage.SetActive(false);
            
            switch (dataItemCategory)
            {
                case DataItem.CategoryType.RED:
                    _redCategoryImage.SetActive(true);
                    break;
                case DataItem.CategoryType.GREEN:
                    _greenCategoryImage.SetActive(true);
                    break;
                case DataItem.CategoryType.BLUE:
                    _blueCategoryImage.SetActive(true);
                    break;
            }
        }

        #endregion

        #region FACTORY

        public class Factory : PlaceholderFactory<InformationElement>
        {
            
        }

        #endregion
    }
}