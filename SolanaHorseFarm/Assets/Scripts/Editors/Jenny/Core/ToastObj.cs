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

    GeneralToast _general = null;
    Coroutine _cor = null;
    ToastType _myType = ToastType.Toast;
    #endregion

    #region public variables
    #endregion

    #region unity function
    private void Awake()
    {
        _general = GetComponent<GeneralToast>();
    }
    #endregion

    #region static function
    #endregion

    #region private function
    IEnumerator FadeInAndOut(float stayTime, float animTime)
    {
        _general.StartSizeAnimation();
        _general.StartPosAnimation();
        yield return new WaitForSeconds(animTime);

        yield return new WaitForSeconds(stayTime);

        _general.EndSizeAnimation();
        _general.EndPosAnimation();
        yield return new WaitForSeconds(animTime);

        ReturnMyObject();
    }

    IEnumerator NoAnimation()
    {
        yield return new WaitForSeconds(3f);

        ReturnMyObject();
    }

    void ReturnMyObject()
    {
        _cor = null;
        Util.SetLabel(_text, string.Empty);
        Util.SetActive(gameObject, false);
        UIManager.Instance.CloseToast(_myType, gameObject);
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

        if (_general != null)
            _cor = StartCoroutine(FadeInAndOut(_general.stayTime, _general.animTime));
        else
            _cor = StartCoroutine(NoAnimation());
    }
    #endregion
}
