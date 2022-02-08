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
    private Transform _ui_root = null;
    private Transform _ui_toast = null;
    #endregion

    #region public variables
    public Transform ui_root
    {
        get
        {
            if (_ui_root == null)
            {
                GameObject go = GameObject.Find("ui_root");
                if (go)
                    _ui_root = go.transform;
            }
            return _ui_root;
        }
    }
    public Transform ui_toast
    {
        get
        {
            if (_ui_toast == null)
            {
                GameObject go = GameObject.Find("ui_toast");
                if (go)
                    _ui_toast = go.transform;
            }
            return _ui_toast;
        }
    }
    #endregion

    #region unity function
    protected UIManager() {} // 비 싱글톤 생성자 사용 방지

    private void Awake()
    {
        _stackUI.Clear();
    }
    #endregion

    #region static function
    #endregion

    #region private function
    private ObjectPoolItems GetObjectPoolItemType(ToastType type)
    {
        ObjectPoolItems item = ObjectPoolItems.Toast;
        switch (type)
        {
            case ToastType.Toast:
                item = ObjectPoolItems.Toast;
                break;
            case ToastType.Toast2:
                item = ObjectPoolItems.Toast2;
                break;
            case ToastType.Toast3:
                item = ObjectPoolItems.Toast3;
                break;
            case ToastType.Toast4:
                item = ObjectPoolItems.Toast4;
                break;
        }
        return item;
    }
    #endregion

    #region public function
    public void OpenUI(string path, bool isThisPopup = false)
    {
        if (string.IsNullOrEmpty(path))
            return;

        GameObject prefab = Resources.Load(path) as GameObject;
        if (prefab == null)
            return;

        GameObject newObj = GameObject.Instantiate(prefab, ui_root);
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
        if (prefab == null)
            return default(T);

        T script = default(T);
        GameObject newObj = GameObject.Instantiate(prefab, ui_root);
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

    public void OpenToast(ToastType toastType, string msg)
    {
        if (string.IsNullOrEmpty(msg))
            return;

        if (ObjectPool.Instance == null)
            return;
        
        ObjectPoolItems itemType = GetObjectPoolItemType(toastType);
        GameObject obj = ObjectPool.Instance.GetObject(itemType);
        if (obj)
        {
            obj.transform.SetParent(ui_root);
            obj.transform.localPosition = Vector3.zero;
            ToastObj cs = obj.GetComponent<ToastObj>();
            if (cs)
                cs.SetTextMessage(toastType, msg);
        }
    }

    public void CloseToast(ToastType toastType, GameObject obj)
    {
        if (obj == null)
            return;

        ObjectPoolItems itemType = GetObjectPoolItemType(toastType);
        ObjectPool.Instance.ReturnObject(itemType, obj);
    }
    #endregion
}
