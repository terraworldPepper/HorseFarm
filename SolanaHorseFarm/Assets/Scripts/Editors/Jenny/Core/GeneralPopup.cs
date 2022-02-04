using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GeneralPopup : MonoBehaviour
{
    #region static variables
    #endregion

    #region private variables
    [Header("Scale Animation Info")]
    [SerializeField] AnimationCurve _curve = null;
    [SerializeField] float _duration = 0f;

    [Header("Scale Animation Target")]
    [SerializeField] GameObject _targetObj = null;

    Vector3 _startScale = new Vector3(0f, 0f, 0f);
    Vector3 _endScale = new Vector3(1f, 1f, 1f);
    #endregion

    #region public variables
    #endregion

    #region unity function
    private void Awake()
    {
        _curve = Properties.Instance.popupAnimCurve;
        _duration = Properties.Instance.popupAnimDuration;

        if (_targetObj)
        {
            _targetObj.transform.localScale = _startScale;
            _targetObj.transform.DOScale(_endScale, _duration).SetEase(_curve);
        }
    }
    #endregion

    #region static function
    #endregion

    #region private function
    #endregion

    #region public function
    #endregion
}
