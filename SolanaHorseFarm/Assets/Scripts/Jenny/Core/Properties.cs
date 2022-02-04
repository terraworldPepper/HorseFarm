using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : Singleton<Properties>
{
    #region static variables
    #endregion

    #region private variables
    [Header("1. Button Setting")]
    [SerializeField] float _buttonSize = 1f;
    [SerializeField] float _buttonAnimationTime = 0.1f;

    [Header("2. Page Open Animation")]
    [SerializeField] AnimationCurve _pageAnimCurve = null;
    [SerializeField] float _pageAnimDuration = 0.5f;
    [SerializeField] float _pageAnimStartDelay = 0f;
    #endregion

    #region public variables
    public float buttonSize
    {
        get
        {
            return _buttonSize;
        }
    }

    public float buttonAnimationTime
    {
        get
        {
            return _buttonAnimationTime;
        }
    }
    public AnimationCurve pageAnimCurve
    {
        get
        {
            return _pageAnimCurve;
        }
    }
    public float pageAnimDuration
    {
        get
        {
            return _pageAnimDuration;
        }
    }
    public float pageAnimStartDelay
    {
        get
        {
            return _pageAnimStartDelay;
        }
    }
    #endregion

    #region unity function
    protected Properties() { } // 비 싱글톤 생성자 사용 방지
    #endregion

    #region static function
    #endregion

    #region private function
    #endregion

    #region public function
    #endregion
}
