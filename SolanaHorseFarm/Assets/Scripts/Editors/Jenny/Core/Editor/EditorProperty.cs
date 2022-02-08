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
        GUILayout.Label("1. ��ư ����", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        properties.buttonOriginSize = EditorGUILayout.Vector3Field("�⺻ ũ��", properties.buttonOriginSize);
        properties.buttonAnimSize = EditorGUILayout.Vector3Field("��ġ �� ũ��", properties.buttonAnimSize);
        properties.buttonAnimTime = EditorGUILayout.FloatField("�ִϸ��̼� �ӵ�", properties.buttonAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(10);
        GUILayout.Label("2. ��ü ȭ�� UI ������ ����", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        properties.pageStartSize = EditorGUILayout.Vector3Field("���� ũ��", properties.pageStartSize);
        properties.pageMaxSize = EditorGUILayout.Vector3Field("�Ϸ� ũ��", properties.pageMaxSize);
        properties.pageAnimCurve = EditorGUILayout.CurveField("�ִϸ��̼� �׷���", properties.pageAnimCurve);
        properties.pageAnimTime = EditorGUILayout.FloatField("�ִϸ��̼� �ӵ�", properties.pageAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(10);
        GUILayout.Label("3. �˾� ������ ����", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        properties.popupStartSize = EditorGUILayout.Vector3Field("���� ũ��", properties.popupStartSize);
        properties.popupMaxSize = EditorGUILayout.Vector3Field("�Ϸ� ũ��", properties.popupMaxSize);
        properties.popupAnimCurve = EditorGUILayout.CurveField("�ִϸ��̼� �׷���", properties.popupAnimCurve);
        properties.popupAnimTime = EditorGUILayout.FloatField("�ִϸ��̼� �ӵ�", properties.popupAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(10);
        GUILayout.Label("4. �佺Ʈ �޽��� ����", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 1;
        EditorGUILayout.LabelField("�� ũ�� ��ȭ ");
        EditorGUI.indentLevel += 1;
        properties.toastStartSize = EditorGUILayout.Vector3Field("���� ũ��", properties.toastStartSize);
        properties.toastEndSize = EditorGUILayout.Vector3Field("�Ϸ� ũ��", properties.toastEndSize);
        properties.toastAnimStartCurveSize = EditorGUILayout.CurveField("���� �ִϸ��̼� �׷���", properties.toastAnimStartCurveSize);
        properties.toastAnimEndCurveSize = EditorGUILayout.CurveField("���� �ִϸ��̼� �׷���", properties.toastAnimEndCurveSize);
        properties.toastAnimTimeSize = EditorGUILayout.FloatField("�ִϸ��̼� �ӵ�", properties.toastAnimTimeSize);
        EditorGUI.indentLevel -= 1;

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("�� ��ġ ��ȭ ");
        EditorGUI.indentLevel += 1;
        properties.toastStartPos = EditorGUILayout.Vector3Field("���� ��ġ", properties.toastStartPos);
        properties.toastEndPos = EditorGUILayout.Vector3Field("�Ϸ� ��ġ", properties.toastEndPos);
        properties.toastAnimStartCurvePos = EditorGUILayout.CurveField("���� �ִϸ��̼� �׷���", properties.toastAnimStartCurvePos);
        properties.toastAnimEndCurvePos = EditorGUILayout.CurveField("���� �ִϸ��̼� �׷���", properties.toastAnimEndCurvePos);
        properties.toastAnimTimePos = EditorGUILayout.FloatField("�ִϸ��̼� �ӵ�", properties.toastAnimTimePos);
        EditorGUI.indentLevel -= 1;

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("�� ���� �ð� ");
        EditorGUI.indentLevel += 1;
        properties.toastStayTime = EditorGUILayout.FloatField("�佺Ʈ ���� �ð�", properties.toastStayTime);
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
