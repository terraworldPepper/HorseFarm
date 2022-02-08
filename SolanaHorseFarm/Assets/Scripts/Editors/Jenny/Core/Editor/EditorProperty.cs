using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Text;

public class EditorProperty : EditorWindow
{
    #region static variables
    #endregion

    #region private variables
    string filePath;
    EditorUIProperty properties = null;
    #endregion

    #region public variables
    #endregion

    #region unity function
    private void OnEnable()
    {
        if (properties == null)
        {
            filePath = string.Format("{0}/UIProperties.prop", Application.persistentDataPath);
            LoadData();
        }
    }

    private void OnGUI()
    {
        if (properties == null)
            return;

        EditorGUIUtility.wideMode = true;

        EditorGUILayout.Space(10);
        GUILayout.Label("1. 버튼 세팅", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        properties.buttonOriginSize = EditorGUILayout.Vector3Field("기본 크기", properties.buttonOriginSize);
        properties.buttonAnimSize = EditorGUILayout.Vector3Field("터치 시 크기", properties.buttonAnimSize);
        properties.buttonAnimTime = EditorGUILayout.FloatField("애니메이션 속도", properties.buttonAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(10);
        GUILayout.Label("2. 전체 화면 UI 페이지 세팅", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        properties.pageStartSize = EditorGUILayout.Vector3Field("시작 크기", properties.pageStartSize);
        properties.pageMaxSize = EditorGUILayout.Vector3Field("완료 크기", properties.pageMaxSize);
        properties.pageAnimCurve = EditorGUILayout.CurveField("애니메이션 그래프", properties.pageAnimCurve);
        properties.pageAnimTime = EditorGUILayout.FloatField("애니메이션 속도", properties.pageAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(10);
        GUILayout.Label("3. 팝업 윈도우 세팅", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        properties.popupStartSize = EditorGUILayout.Vector3Field("시작 크기", properties.popupStartSize);
        properties.popupMaxSize = EditorGUILayout.Vector3Field("완료 크기", properties.popupMaxSize);
        properties.popupAnimCurve = EditorGUILayout.CurveField("애니메이션 그래프", properties.popupAnimCurve);
        properties.popupAnimTime = EditorGUILayout.FloatField("애니메이션 속도", properties.popupAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(10);
        GUILayout.Label("4. 토스트 메시지 세팅", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 1;
        EditorGUILayout.LabelField("▷ 크기 변화 ");
        EditorGUI.indentLevel += 1;
        properties.toastStartSize = EditorGUILayout.Vector3Field("시작 크기", properties.toastStartSize);
        properties.toastEndSize = EditorGUILayout.Vector3Field("완료 크기", properties.toastEndSize);
        properties.toastAnimStartCurveSize = EditorGUILayout.CurveField("시작 애니메이션 그래프", properties.toastAnimStartCurveSize);
        properties.toastAnimEndCurveSize = EditorGUILayout.CurveField("종료 애니메이션 그래프", properties.toastAnimEndCurveSize);
        properties.toastAnimTimeSize = EditorGUILayout.FloatField("애니메이션 속도", properties.toastAnimTimeSize);
        EditorGUI.indentLevel -= 1;

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("▷ 위치 변화 ");
        EditorGUI.indentLevel += 1;
        properties.toastStartPos = EditorGUILayout.Vector3Field("시작 위치", properties.toastStartPos);
        properties.toastEndPos = EditorGUILayout.Vector3Field("완료 위치", properties.toastEndPos);
        properties.toastAnimStartCurvePos = EditorGUILayout.CurveField("시작 애니메이션 그래프", properties.toastAnimStartCurvePos);
        properties.toastAnimEndCurvePos = EditorGUILayout.CurveField("종료 애니메이션 그래프", properties.toastAnimEndCurvePos);
        properties.toastAnimTimePos = EditorGUILayout.FloatField("애니메이션 속도", properties.toastAnimTimePos);
        EditorGUI.indentLevel -= 1;

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("▷ 유지 시간 ");
        EditorGUI.indentLevel += 1;
        properties.toastStayTime = EditorGUILayout.FloatField("토스트 유지 시간", properties.toastStayTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space();
        Rect lastRect = GUILayoutUtility.GetLastRect();
        if (GUI.Button(new Rect(lastRect.x, lastRect.y + EditorGUIUtility.singleLineHeight, position.width, 20), "Save"))
        {
            SaveData();
        }
    }
    #endregion

    #region static function
    [MenuItem("TeamHorn/UI Properties")]
    public static void ShowWindow()
    {
        EditorProperty window = (EditorProperty)EditorWindow.GetWindowWithRect(typeof(EditorProperty), new Rect(200, 200, 500, 700));
        window.Show();
    }
    #endregion

    #region private function
    private void SaveData()
    {
        string json = properties.DataToJsonString();
        using (StreamWriter w = File.CreateText(filePath))
        {
            w.WriteLine(json);
        }

        SetUIProperties();
    }
    private void LoadData()
    {
        if (File.Exists(filePath))
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                if (!string.IsNullOrEmpty(json))
                    properties = EditorUIProperty.CreateInstanceFromJson(json);
                else
                    properties = new EditorUIProperty();
            }
        }
        else
        {
            properties = new EditorUIProperty();
        }

        SetUIProperties();
    }

    private void SetUIProperties()
    {
        if (UIProperties.Instance != null)
        {
            UIProperties.Instance.buttonOriginSize = properties.buttonOriginSize;
            UIProperties.Instance.buttonAnimSize = properties.buttonAnimSize;
            UIProperties.Instance.buttonAnimTime = properties.buttonAnimTime;

            UIProperties.Instance.pageStartSize = properties.pageStartSize;
            UIProperties.Instance.pageMaxSize = properties.pageMaxSize;
            UIProperties.Instance.pageAnimCurve = properties.pageAnimCurve;
            UIProperties.Instance.pageAnimTime = properties.pageAnimTime;

            UIProperties.Instance.popupStartSize = properties.popupStartSize;
            UIProperties.Instance.popupMaxSize = properties.popupMaxSize;
            UIProperties.Instance.popupAnimCurve = properties.popupAnimCurve;
            UIProperties.Instance.popupAnimTime = properties.popupAnimTime;

            UIProperties.Instance.toastStartSize = properties.toastStartSize;
            UIProperties.Instance.toastEndSize = properties.toastEndSize;
            UIProperties.Instance.toastAnimStartCurveSize = properties.toastAnimStartCurveSize;
            UIProperties.Instance.toastAnimEndCurveSize = properties.toastAnimEndCurveSize;
            UIProperties.Instance.toastAnimTimeSize = properties.toastAnimTimeSize;

            UIProperties.Instance.toastStartPos = properties.toastStartPos;
            UIProperties.Instance.toastEndPos = properties.toastEndPos;
            UIProperties.Instance.toastAnimStartCurvePos = properties.toastAnimStartCurvePos;
            UIProperties.Instance.toastAnimEndCurvePos = properties.toastAnimEndCurvePos;
            UIProperties.Instance.toastAnimTimePos = properties.toastAnimTimePos;

            UIProperties.Instance.toastStayTime = properties.toastStayTime;
        }
    }
    #endregion

    #region public function
    #endregion

    private class EditorUIProperty
    {
        public Vector3 buttonOriginSize = Vector3.one;
        public Vector3 buttonAnimSize = Vector3.one;
        public float buttonAnimTime = 0.1f;

        public Vector3 pageStartSize = Vector3.zero;
        public Vector3 pageMaxSize = Vector3.one;
        public AnimationCurve pageAnimCurve = null;
        public float pageAnimTime = 0.1f;

        public Vector3 popupStartSize = Vector3.zero;
        public Vector3 popupMaxSize = Vector3.one;
        public AnimationCurve popupAnimCurve = null;
        public float popupAnimTime = 0.1f;

        public Vector3 toastStartSize = Vector3.zero;
        public Vector3 toastEndSize = Vector3.one;
        public AnimationCurve toastAnimStartCurveSize = null;
        public AnimationCurve toastAnimEndCurveSize = null;
        public float toastAnimTimeSize = 0.1f;

        public Vector3 toastStartPos = Vector3.zero;
        public Vector3 toastEndPos = Vector3.zero;
        public AnimationCurve toastAnimStartCurvePos = null;
        public AnimationCurve toastAnimEndCurvePos = null;
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

        public static EditorUIProperty CreateInstanceFromJson(string json)
        {
            return JsonUtility.FromJson<EditorUIProperty>(json);
        }
    }
}
