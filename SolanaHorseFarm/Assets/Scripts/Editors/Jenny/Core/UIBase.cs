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

        SetBackgroundClickBlocker();
    }
    #endregion

    #region static function
    #endregion

    #region private function
    void SetBackgroundClickBlocker()
    {
        GameObject blocker = Resources.Load("Prefabs/Core/ClickBlocker") as GameObject;
        if (blocker == null)
            return;

        GameObject child = GameObject.Instantiate(blocker, transform);
        if (child)
        {
            child.transform.SetAsFirstSibling();
            RectTransform rect = child.GetComponent<RectTransform>();
            if (rect)
                rect.sizeDelta = new Vector2(Screen.width, Screen.height);
        }
    }
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
        foreach (Transform child in tr)
            SetLayerRecursive(child, layerName);
    }
    #endregion
}
