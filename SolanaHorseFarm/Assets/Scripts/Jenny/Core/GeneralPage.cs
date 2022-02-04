using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPage : MonoBehaviour
{
    #region static variables
    #endregion

    #region private variables
    [Header("Scale Animation Info")]
    [SerializeField] AnimationCurve _curve = null;
    [SerializeField] float _duration = 0.5f;
    [SerializeField] float _startDelay = 0f;

    [Header("Scale Animation Target")]
    [SerializeField] GameObject _targetObj = null;

    Vector3 _startScale = new Vector3(0f, 0f, 0f);
    Vector3 _endScale = new Vector3(1f, 1f, 1f);
    float _playTime = 1f;
    float _playTimer = 0f;
    #endregion

    #region public variables
    #endregion

    #region unity function
    private void Awake()
    {
        if (_targetObj)
            _targetObj.transform.localScale = _startScale;

        _curve = Properties.Instance.pageAnimCurve;
        _duration = Properties.Instance.pageAnimDuration;
        _startDelay = Properties.Instance.pageAnimStartDelay;
    }

    private void Update()
    {
        if (_playTimer <= _playTime)
        {
            if (_targetObj)
                _targetObj.transform.localScale = Vector3.Lerp(_startScale, _endScale, _curve.Evaluate(_playTimer / _playTime));
            _playTimer += Time.deltaTime;
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
