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
    [SerializeField][ConditionalEnable("_individualProperty")] AnimationCurve _animCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField][ConditionalEnable("_individualProperty")] float _animTime = 0.1f;

    [Header("Animation Target")]
    [SerializeField] GameObject _targetObj = null;

    Vector3 _startSize = Vector3.zero;
    Vector3 _maxSize = Vector3.one;
    #endregion

    #region public variables
    #endregion

    #region unity function
    private void Awake()
    {
        if (!_individualProperty)
        {
            _animCurve = UIProperties.Instance.Properties.pageAnimCurve;
            _animTime = UIProperties.Instance.Properties.pageAnimTime;
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
