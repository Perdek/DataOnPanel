using UnityEngine;

[System.Serializable]
public class InformationPanelConfiguration
{
    #region MEMBERS

    [SerializeField] private int _maxElementCount = 5;

    #endregion

    #region PROPERTIES

    public int MaxElementCount => _maxElementCount;

    #endregion
}
