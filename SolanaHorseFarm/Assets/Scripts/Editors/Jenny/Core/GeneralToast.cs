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
    [SerializeField][ConditionalEnable("_individualProperty")] AnimationCurve _animStartCurveSize = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField][ConditionalEnable("_individualProperty")] AnimationCurve _animEndCurveSize = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField][ConditionalEnable("_individualProperty")] float _animTimeSize = 0.1f;

    [Header("Position Animation Info")]
    [SerializeField][ConditionalEnable("_individualProperty")] Vector3 _startPos = Vector3.zero;
    [SerializeField][ConditionalEnable("_individualProperty")] Vector3 _endPos = Vector3.zero;
    [SerializeField][ConditionalEnable("_individualProperty")] AnimationCurve _animStartCurvePos = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField][ConditionalEnable("_individualProperty")] AnimationCurve _animEndCurvePos = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField][ConditionalEnable("_individualProperty")] float _animTimePos = 0.1f;

    [Header("Stay Info")]
    [SerializeField][ConditionalEnable("_individualProperty")] float _stayTime = 3f;
    
    [Header("Animation Target")]
    [SerializeField] GameObject _targetObj = null;

    Vector3 _startSize = Vector3.zero;
    Vector3 _endSize = Vector3.one;
    ToastObj _parentObj = null;
    Sequence _seq = null;
    #endregion

    #region public variables
    public float animTime { get { return _animTimeSize > _animTimePos ? _animTimeSize : _animTimePos; } } // 제일 긴 시간으로
    public float stayTime { get { return _stayTime; } }
    #endregion

    #region unity function
    private void Awake()
    {
        _parentObj = GetComponent<ToastObj>();
        _seq = DOTween.Sequence();
        SetProperties();

        if (_targetObj == null)
            return;

        _seq.Append(_targetObj.transform.DOScale(_endSize, _animTimeSize).SetEase(_animStartCurveSize))
            .Join(_targetObj.transform.DOLocalMove(_endPos, _animTimePos).SetEase(_animStartCurvePos))
            .Append(_targetObj.transform.DOScale(_startSize, _animTimeSize).SetEase(_animEndCurveSize).SetDelay(stayTime))
            .Join(_targetObj.transform.DOLocalMove(_startPos, _animTimePos).SetEase(_animEndCurvePos))
            .OnComplete(() => { _parentObj.ReturnMyObject(); })
            .SetAutoKill(false).Pause();
    }

    private void OnEnable()
    {
        SetProperties();

        if (_targetObj && _parentObj)
        {
            _targetObj.transform.localScale = _startSize;
            _targetObj.transform.localPosition = _startPos;

            if (gameObject.activeSelf)
            {
                _seq.Restart();
            }
        }
    }

    private void OnDisable()
    {
        _seq.Rewind();
    }
    #endregion

    #region static function
    #endregion

    #region private function
    void SetProperties()
    {
        if (!_individualProperty)
        {
            _animStartCurveSize = UIProperties.Instance.Properties.toastAnimStartCurveSize;
            _animEndCurveSize = UIProperties.Instance.Properties.toastAnimEndCurveSize;
            _animTimeSize = UIProperties.Instance.Properties.toastAnimTimeSize;

            _startPos = UIProperties.Instance.Properties.toastStartPos;
            _endPos = UIProperties.Instance.Properties.toastEndPos;
            _animStartCurvePos = UIProperties.Instance.Properties.toastAnimStartCurvePos;
            _animEndCurvePos = UIProperties.Instance.Properties.toastAnimEndCurvePos;
            _animTimePos = UIProperties.Instance.Properties.toastAnimTimePos;

            _stayTime = UIProperties.Instance.Properties.toastStayTime;
        }
    }
    #endregion

    #region public function
    #endregion
}
