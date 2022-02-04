using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class GeneralButton : MonoBehaviour
{
    #region static variables
    #endregion

    #region private variables
    [Header("Scale Variables")]
    [SerializeField] float _buttonScale = 0f;
    [SerializeField] float _animationTime = 0f;

    Button _button = null;
    EventTrigger _trigger = null;
    Vector3 _originSize = new Vector3(1f, 1f, 1f);
    #endregion

    #region public variables
    #endregion

    #region unity function
    void Awake()
    {
        _buttonScale = Properties.Instance.buttonSize;
        _animationTime = Properties.Instance.buttonAnimationTime;

        _button = GetComponent<Button>();
        if (_button != null)
        {
            _trigger = gameObject.AddComponent<EventTrigger>();
            if (_trigger)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((data) => { PointerDown(); });
                _trigger.triggers.Add(entry);

                entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerUp;
                entry.callback.AddListener((data) => { PointerUp(); });
                _trigger.triggers.Add(entry);
            }

            _originSize = new Vector3(_button.transform.localScale.x, _button.transform.localScale.y, _button.transform.localScale.z);
        }
    }
    #endregion

    #region static function
    #endregion

    #region private function
    void PointerDown()
    {
        if (_button)
        {
            transform.
            iTween.ScaleTo(gameObject, new Vector3(_buttonScale, _buttonScale, _originSize.z), _animationTime);
        }
    }

    void PointerUp()
    {
        if (_button)
        {
            iTween.ScaleTo(gameObject, new Vector3(_originSize.x, _originSize.y, _originSize.z), _animationTime);
        }
    }
    #endregion

    #region public function
    #endregion
}
