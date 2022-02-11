using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastObj : MonoBehaviour
{
    #region static variables
    #endregion

    #region private variables
    [SerializeField] Text _text = null;

    Coroutine _cor = null;
    ToastType _myType = ToastType.Toast;
    Button _button = null;
    #endregion

    #region public variables
    public Coroutine cor
    {
        get { return _cor; }
        set { _cor = value; }
    }
    #endregion

    #region unity function
    private void Awake()
    {
        _button = gameObject.GetComponentInChildren<Button>();
        if (_button)
            _button.onClick.AddListener(delegate { ReturnMyObject(); });
    }
    #endregion

    #region static function
    #endregion

    #region private function
    IEnumerator NoAnimation()
    {
        yield return new WaitForSeconds(3f);

        ReturnMyObject();
    }    
    #endregion

    #region public function
    public void SetTextMessage(ToastType type, string msg)
    {
        this._myType = type;
        Util.SetLabel(_text, msg);
        Util.SetActive(gameObject, true);

        if (_cor != null)
        {
            StopCoroutine(_cor);
            _cor = null;
        }

        GeneralToast general = GetComponent<GeneralToast>();
        if (general == null)
            _cor = StartCoroutine(NoAnimation());
    }

    public void ReturnMyObject()
    {
        if (_cor != null)
            StopCoroutine(_cor);
        _cor = null;

        Util.SetLabel(_text, string.Empty);
        Util.SetActive(gameObject, false);
        UIManager.Instance.CloseToast(_myType, gameObject);
    }
    #endregion
}
