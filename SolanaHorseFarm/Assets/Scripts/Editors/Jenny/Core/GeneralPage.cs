using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GeneralPage : MonoBehaviour
{
    #region static variables
    #endregion

    #region private variables
    [SerializeField] bool _individualProperty = false;

    [Header("Scale Animation Info")]
    [SerializeField][ConditionalEnable("_individualProperty")] Vector3 _startSize = Vector3.zero;
    [SerializeField][ConditionalEnable("_individualProperty")] Vector3 _maxSize = Vector3.one;
    [SerializeField][ConditionalEnable("_individualProperty")] AnimationCurve _animCurve = null;
    [SerializeField][ConditionalEnable("_individualProperty")] float _animTime = 0.1f;

    [Header("Animation Target")]
    [SerializeField] GameObject _targetObj = null;
    #endregion

    #region public variables
    #endregion

    #region unity function
    private void Awake()
    {
        if (!_individualProperty)
        {
            _startSize = UIProperties.Instance.pageStartSize;
            _maxSize = UIProperties.Instance.pageMaxSize;
            _animCurve = UIProperties.Instance.pageAnimCurve;
            _animTime = UIProperties.Instance.pageAnimTime;
        }

        if (_targetObj)
        {
            _targetObj.transform.localScale = _startSize;
            _targetObj.transform.DOScale(_maxSize, _animTime).SetEase(_animCurve);
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
