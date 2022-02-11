using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIProperties : Singleton<UIProperties>
{
    #region static variables
    #endregion

    #region private variables
    UIProperty _properties = null;

    #endregion

    #region public variables
    public UIProperty Properties
    {
        get 
        { 
            if (_properties == null)
                _properties = new UIProperty();
            return _properties;
        }
    }

    //// 1. ��ư ����
    //[HideInInspector] public Vector3 buttonOriginSize = Vector3.one;
    //[HideInInspector] public Vector3 buttonAnimSize = Vector3.one;
    //[HideInInspector] public float buttonAnimTime = 0.1f;

    //// 2. ��ü ȭ�� UI ������ ����
    //[HideInInspector] public Vector3 pageStartSize = Vector3.one;
    //[HideInInspector] public Vector3 pageMaxSize = Vector3.one;
    //[HideInInspector] public AnimationCurve pageAnimCurve = null;
    //[HideInInspector] public float pageAnimTime = 0.1f;

    //// 3. �˾� ������ ����
    //[HideInInspector] public Vector3 popupStartSize = Vector3.one;
    //[HideInInspector] public Vector3 popupMaxSize = Vector3.one;
    //[HideInInspector] public AnimationCurve popupAnimCurve = null;
    //[HideInInspector] public float popupAnimTime = 0.1f;

    //// 4. �佺Ʈ �޽��� ���� - ������
    //[HideInInspector] public Vector3 toastStartSize = Vector3.one;
    //[HideInInspector] public Vector3 toastEndSize = Vector3.one;
    //[HideInInspector] public AnimationCurve toastAnimStartCurveSize = null;
    //[HideInInspector] public AnimationCurve toastAnimEndCurveSize = null;
    //[HideInInspector] public float toastAnimTimeSize = 0.5f;

    //// 5. �佺Ʈ �޽��� ���� - ��ġ
    //[HideInInspector] public Vector3 toastStartPos = Vector3.one;
    //[HideInInspector] public Vector3 toastEndPos = Vector3.one;
    //[HideInInspector] public AnimationCurve toastAnimStartCurvePos = null;
    //[HideInInspector] public AnimationCurve toastAnimEndCurvePos = null;
    //[HideInInspector] public float toastAnimTimePos = 0.5f;

    //// 6. �佺Ʈ �޽��� ���� - Stay
    //[HideInInspector] public float toastStayTime = 3f;

    #endregion

    #region unity function
    protected UIProperties() { } // �� �̱��� ������ ��� ����

    private void Awake()
    {
        string filePath = string.Format("{0}/UIProperties.prop", Application.persistentDataPath);
        if (File.Exists(filePath))
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                if (!string.IsNullOrEmpty(json))
                    _properties = UIProperty.CreateInstanceFromJson(json);
                else
                    _properties = new UIProperty();
            }
        }
        else
        {
            _properties = new UIProperty();
        }
    }
    #endregion

    #region static function
    #endregion

    #region private function
    #endregion

    #region public function
    #endregion

    public class UIProperty
    {
        public Vector3 buttonOriginSize = Vector3.one;
        public Vector3 buttonAnimSize = Vector3.one;
        public float buttonAnimTime = 0.1f;

        public AnimationCurve pageAnimCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        public float pageAnimTime = 0.1f;

        public AnimationCurve popupAnimCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        public float popupAnimTime = 0.1f;

        public AnimationCurve toastAnimStartCurveSize = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        public AnimationCurve toastAnimEndCurveSize = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        public float toastAnimTimeSize = 0.1f;

        public Vector3 toastStartPos = Vector3.zero;
        public Vector3 toastEndPos = Vector3.zero;
        public AnimationCurve toastAnimStartCurvePos = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        public AnimationCurve toastAnimEndCurvePos = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        public float toastAnimTimePos = 0.1f;

        public float toastStayTime = 3f;

        public string DataToJsonString()
        {
            return JsonUtility.ToJson(this);
        }

        public void JsonStringToData(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }

        public static UIProperty CreateInstanceFromJson(string json)
        {
            return JsonUtility.FromJson<UIProperty>(json);
        }
    }
}
