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
    UIProperties.UIProperty properties = null;
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

        EditorGUILayout.Space(12);
        GUILayout.Label("1. 버튼 세팅", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        properties.buttonOriginSize = EditorGUILayout.Vector3Field("기본 크기", properties.buttonOriginSize);
        properties.buttonAnimSize = EditorGUILayout.Vector3Field("터치 시 크기", properties.buttonAnimSize);
        properties.buttonAnimTime = EditorGUILayout.FloatField("애니메이션 속도", properties.buttonAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(12);
        GUILayout.Label("2. 전체 화면 UI 페이지 세팅", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        // 시작 크기와 목표 크기의 갭 만큼을 수행??
        properties.pageAnimCurve = EditorGUILayout.CurveField("애니메이션 그래프", properties.pageAnimCurve);
        properties.pageAnimTime = EditorGUILayout.FloatField("애니메이션 속도", properties.pageAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(12);
        GUILayout.Label("3. 팝업 윈도우 세팅", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        properties.popupAnimCurve = EditorGUILayout.CurveField("애니메이션 그래프", properties.popupAnimCurve);
        properties.popupAnimTime = EditorGUILayout.FloatField("애니메이션 속도", properties.popupAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(12);
        GUILayout.Label("4. 토스트 메시지 세팅", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 1;
        EditorGUILayout.LabelField("▷ 크기 변화 ");
        EditorGUI.indentLevel += 1;
        properties.toastAnimStartCurveSize = EditorGUILayout.CurveField("시작 애니메이션 그래프", properties.toastAnimStartCurveSize);
        properties.toastAnimEndCurveSize = EditorGUILayout.CurveField("종료 애니메이션 그래프", properties.toastAnimEndCurveSize);
        properties.toastAnimTimeSize = EditorGUILayout.FloatField("애니메이션 속도", properties.toastAnimTimeSize);
        EditorGUI.indentLevel -= 1;

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("▷ 위치 변화 ");
        EditorGUI.indentLevel += 1;
        properties.toastStartPos = EditorGUILayout.Vector3Field("시작 위치", properties.toastStartPos);
        properties.toastEndPos = EditorGUILayout.Vector3Field("완료 위치", properties.toastEndPos);
        properties.toastAnimStartCurvePos = EditorGUILayout.CurveField("시작 애니메이션 그래프", properties.toastAnimStartCurvePos);
        properties.toastAnimEndCurvePos = EditorGUILayout.CurveField("종료 애니메이션 그래프", properties.toastAnimEndCurvePos);
        properties.toastAnimTimePos = EditorGUILayout.FloatField("애니메이션 속도", properties.toastAnimTimePos);
        EditorGUI.indentLevel -= 1;

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("▷ 유지 시간 ");
        EditorGUI.indentLevel += 1;
        properties.toastStayTime = EditorGUILayout.FloatField("토스트 유지 시간", properties.toastStayTime);
        EditorGUI.indentLevel -= 1;

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("▷ 토스트 오브젝트 풀 초기화 ");
        EditorGUI.indentLevel += 1;
        Rect lastRect = GUILayoutUtility.GetLastRect();
        if (GUI.Button(new Rect(lastRect.x + 30, lastRect.y + EditorGUIUtility.singleLineHeight, 200, 20), "토스트 오브젝트 풀 비우기"))
        {
            EmptyObjectPool();
        }
        EditorGUILayout.Space(18);
        EditorGUILayout.HelpBox("런타임 중에 토스트 프리팹의 Individual Property 설정을 바꾸려면 오브젝트 풀을 비워줘야 합니다. ", MessageType.Info);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(6);
        lastRect = GUILayoutUtility.GetLastRect();
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
                    properties = UIProperties.UIProperty.CreateInstanceFromJson(json);
                else
                    properties = new UIProperties.UIProperty();
            }
        }
        else
        {
            properties = new UIProperties.UIProperty();
        }

        SetUIProperties();
    }
    private void EmptyObjectPool()
    {
        ObjectPool.Instance.EmptyObjectPool();
    }

    private void SetUIProperties()
    {
        if (UIProperties.Instance != null)
        {
            UIProperties.Instance.Properties.buttonOriginSize = properties.buttonOriginSize;
            UIProperties.Instance.Properties.buttonAnimSize = properties.buttonAnimSize;
            UIProperties.Instance.Properties.buttonAnimTime = properties.buttonAnimTime;

            UIProperties.Instance.Properties.pageAnimCurve = properties.pageAnimCurve;
            UIProperties.Instance.Properties.pageAnimTime = properties.pageAnimTime;

            UIProperties.Instance.Properties.popupAnimCurve = properties.popupAnimCurve;
            UIProperties.Instance.Properties.popupAnimTime = properties.popupAnimTime;

            UIProperties.Instance.Properties.toastAnimStartCurveSize = properties.toastAnimStartCurveSize;
            UIProperties.Instance.Properties.toastAnimEndCurveSize = properties.toastAnimEndCurveSize;
            UIProperties.Instance.Properties.toastAnimTimeSize = properties.toastAnimTimeSize;

            UIProperties.Instance.Properties.toastStartPos = properties.toastStartPos;
            UIProperties.Instance.Properties.toastEndPos = properties.toastEndPos;
            UIProperties.Instance.Properties.toastAnimStartCurvePos = properties.toastAnimStartCurvePos;
            UIProperties.Instance.Properties.toastAnimEndCurvePos = properties.toastAnimEndCurvePos;
            UIProperties.Instance.Properties.toastAnimTimePos = properties.toastAnimTimePos;

            UIProperties.Instance.Properties.toastStayTime = properties.toastStayTime;
        }
    }
    #endregion

    #region public function
    #endregion
}
