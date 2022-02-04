using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    #region static variables
    #endregion

    #region private variables
    Stack<GameObject> _stackUI = new Stack<GameObject>();
    Transform _parent = null;
    #endregion

    #region public variables
    #endregion

    #region unity function
    protected UIManager() {} // 비 싱글톤 생성자 사용 방지

    private void Awake()
    {
        if (_parent == null)
        {
            GameObject go = GameObject.Find("ui_root");
            if (go)
                _parent = go.transform;
        }

        _stackUI.Clear();
    }
    #endregion

    #region static function
    #endregion

    #region private function
    #endregion

    #region public function
    public void OpenUI(string path, bool isThisPopup = false)
    {
        if (string.IsNullOrEmpty(path))
            return;

        GameObject prefab = Resources.Load(path) as GameObject;
        GameObject newObj = GameObject.Instantiate(prefab, _parent);
        if (newObj)
        {
            Util.SetActive(newObj, true);
            _stackUI.Push(newObj);

            if (isThisPopup)
            {
                UIBase baseScript = newObj.GetComponent<UIBase>();
                if (baseScript)
                    baseScript.SetLayerRecursive(newObj.transform, "PopupUI");
            }
        }
    }

    public T OpenUI<T>(string path, bool isThisPopup = false)
    {
        if (string.IsNullOrEmpty(path))
            return default(T);

        GameObject prefab = Resources.Load(path) as GameObject;
        T script = default(T);
        GameObject newObj = GameObject.Instantiate(prefab, _parent);
        if (newObj)
        {
            Util.SetActive(newObj, true);
            _stackUI.Push(newObj);
            script = newObj.GetComponent<T>();

            if (isThisPopup)
            {
                UIBase baseScript = script as UIBase;
                baseScript.SetLayerRecursive(newObj.transform, "PopupUI");
            }
        }

        return script;
    }

    public void CloseUI()
    {
        if (_stackUI.Count == 0)
            return;

        GameObject go = _stackUI.Pop();
        Destroy(go);
    }

    public void CloseUI<T>()
    {
        if (_stackUI.Count == 0)
            return;

        Stack<GameObject> reverseStack = new Stack<GameObject>();
        reverseStack.Clear();

        while (_stackUI.Count > 0)
        {
            GameObject go = _stackUI.Pop();
            T script = go.GetComponent<T>();
            if (script == null)
                reverseStack.Push(go);
            else
                Destroy(go);
        }

        while (reverseStack.Count > 0)
        {
            _stackUI.Push(reverseStack.Pop());
        }
    }
    #endregion
}
