using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    #region static variables
    #endregion

    #region private variables
    [SerializeField] Button _exitButton = null;
    [SerializeField] Button _backButton = null;
    #endregion

    #region public variables
    #endregion

    #region unity function
    private void Awake()
    {
        if (_exitButton)
            _exitButton.onClick.AddListener(OnExitButtonClick);

        if (_backButton)
            _backButton.onClick.AddListener(OnBackButtonClick);
    }
    #endregion

    #region static function
    #endregion

    #region private function
    void OnExitButtonClick()
    {
        UIManager.Instance.CloseUI();
    }

    void OnBackButtonClick()
    {
        UIManager.Instance.CloseUI();
    }
    #endregion

    #region public function
    public void SetLayerRecursive(Transform tr, string layerName)
    {
        if (tr == null)
            return;

        tr.gameObject.layer = LayerMask.NameToLayer(layerName);
        foreach (Transform child in transform)
            SetLayerRecursive(child, layerName);
    }
    #endregion
}
