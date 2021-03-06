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
    [SerializeField] bool _individualProperty = false;

    [Header("Scale Animation Info")]
    [SerializeField][ConditionalEnable("_individualProperty")] Vector3 _originSize = Vector3.one;
    [SerializeField][ConditionalEnable("_individualProperty")] Vector3 _tweenSize = Vector3.one;
    [SerializeField][ConditionalEnable("_individualProperty")] float _tweenTime = 0.1f;

    [Header("Animation Target")]
    [SerializeField] GameObject _targetObj = null;

    Button _button = null;
    #endregion

    #region public variables
    #endregion

    #region unity function
    void Awake()
    {
        SetProperties();

        _button = GetComponent<Button>();
        if (_button != null)
        {
            EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
            if (trigger)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((data) => { PointerDown(); });
                trigger.triggers.Add(entry);

                entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerUp;
                entry.callback.AddListener((data) => { PointerUp(); });
                trigger.triggers.Add(entry);
            }
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
            _originSize = UIProperties.Instance.Properties.buttonOriginSize;
            _tweenSize = UIProperties.Instance.Properties.buttonAnimSize;
            _tweenTime = UIProperties.Instance.Properties.buttonAnimTime;
        }
    }
    void PointerDown()
    {
        if (_targetObj)
        {
            SetProperties();

            _targetObj.transform.DOScale(_tweenSize, _tweenTime);
        }
    }

    void PointerUp()
    {
        if (_targetObj)
        {
            _targetObj.transform.DOScale(_originSize, _tweenTime);
        }
    }
    #endregion

    #region public function
    #endregion
}
