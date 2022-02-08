using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GeneralToast : MonoBehaviour
{
    #region static variables
    #endregion

    #region private variables
    [SerializeField] bool _individualProperty = false;

    [Header("Scale Animation Info")]
    [SerializeField][ConditionalEnable("_individualProperty")] Vector3 _startSize = Vector3.zero;
    [SerializeField][ConditionalEnable("_individualProperty")] Vector3 _endSize = Vector3.one;
    [SerializeField][ConditionalEnable("_individualProperty")] AnimationCurve _animStartCurveSize = null;
    [SerializeField][ConditionalEnable("_individualProperty")] AnimationCurve _animEndCurveSize = null;
    [SerializeField][ConditionalEnable("_individualProperty")] float _animTimeSize = 0.1f;

    [Header("Position Animation Info")]
    [SerializeField][ConditionalEnable("_individualProperty")] Vector3 _startPos = Vector3.zero;
    [SerializeField][ConditionalEnable("_individualProperty")] Vector3 _endPos = Vector3.zero;
    [SerializeField][ConditionalEnable("_individualProperty")] AnimationCurve _animStartCurvePos = null;
    [SerializeField][ConditionalEnable("_individualProperty")] AnimationCurve _animEndCurvePos = null;
    [SerializeField][ConditionalEnable("_individualProperty")] float _animTimePos = 0.1f;

    [Header("Stay Info")]
    [SerializeField][ConditionalEnable("_individualProperty")] float _stayTime = 3f;
    
    [Header("Animation Target")]
    [SerializeField] GameObject _targetObj = null;
    #endregion

    #region public variables
    public float animTime { get { return _animTimeSize > _animTimePos ? _animTimeSize : _animTimePos; } } // 제일 긴 시간으로
    public float stayTime { get { return _stayTime; } }
    #endregion

    #region unity function
    private void OnEnable()
    {
        SetProperties();

        if (_targetObj)
        {
            _targetObj.transform.localScale = _startSize;
            _targetObj.transform.localPosition = _startPos;
        }
    }
    #endregion

    #region static function
    #endregion

    #region private function
    void SetProperties()
    {
        if (!_individualProperty)
        {
            _startSize = UIProperties.Instance.toastStartSize;
            _endSize = UIProperties.Instance.toastEndSize;
            _animStartCurveSize = UIProperties.Instance.toastAnimStartCurveSize;
            _animEndCurveSize = UIProperties.Instance.toastAnimEndCurveSize;
            _animTimeSize = UIProperties.Instance.toastAnimTimeSize;

            _startPos = UIProperties.Instance.toastStartPos;
            _endPos = UIProperties.Instance.toastEndPos;
            _animStartCurvePos = UIProperties.Instance.toastAnimStartCurvePos;
            _animEndCurvePos = UIProperties.Instance.toastAnimEndCurvePos;
            _animTimePos = UIProperties.Instance.toastAnimTimePos;

            _stayTime = UIProperties.Instance.toastStayTime;
        }
    }
    #endregion

    #region public function
    public void StartSizeAnimation()
    {
        SetProperties();

        if (_targetObj)
            _targetObj.transform.DOScale(_endSize, _animTimeSize).SetEase(_animStartCurveSize);
    }

    public void EndSizeAnimation()
    {
        if (_targetObj)
            _targetObj.transform.DOScale(_startSize, _animTimeSize).SetEase(_animEndCurveSize);
    }

    public void StartPosAnimation()
    {
        if (_targetObj)
            _targetObj.transform.DOLocalMove(_endPos, _animTimePos).SetEase(_animStartCurvePos);
    }

    public void EndPosAnimation()
    {
        if (_targetObj)
            _targetObj.transform.DOLocalMove(_startPos, _animTimePos).SetEase(_animEndCurvePos);
    }
    #endregion
}
