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
        GUILayout.Label("1. ��ư ����", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        properties.buttonOriginSize = EditorGUILayout.Vector3Field("�⺻ ũ��", properties.buttonOriginSize);
        properties.buttonAnimSize = EditorGUILayout.Vector3Field("��ġ �� ũ��", properties.buttonAnimSize);
        properties.buttonAnimTime = EditorGUILayout.FloatField("�ִϸ��̼� �ӵ�", properties.buttonAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(12);
        GUILayout.Label("2. ��ü ȭ�� UI ������ ����", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        // ���� ũ��� ��ǥ ũ���� �� ��ŭ�� ����??
        properties.pageAnimCurve = EditorGUILayout.CurveField("�ִϸ��̼� �׷���", properties.pageAnimCurve);
        properties.pageAnimTime = EditorGUILayout.FloatField("�ִϸ��̼� �ӵ�", properties.pageAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(12);
        GUILayout.Label("3. �˾� ������ ����", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 2;
        properties.popupAnimCurve = EditorGUILayout.CurveField("�ִϸ��̼� �׷���", properties.popupAnimCurve);
        properties.popupAnimTime = EditorGUILayout.FloatField("�ִϸ��̼� �ӵ�", properties.popupAnimTime);
        EditorGUI.indentLevel -= 2;

        EditorGUILayout.Space(12);
        GUILayout.Label("4. �佺Ʈ �޽��� ����", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 1;
        EditorGUILayout.LabelField("�� ũ�� ��ȭ ");
        EditorGUI.indentLevel += 1;
        properties.toastAnimStartCurveSize = EditorGUILayout.CurveField("���� �ִϸ��̼� �׷���", properties.toastAnimStartCurveSize);
        properties.toastAnimEndCurveSize = EditorGUILayout.CurveField("���� �ִϸ��̼� �׷���", properties.toastAnimEndCurveSize);
        properties.toastAnimTimeSize = EditorGUILayout.FloatField("�ִϸ��̼� �ӵ�", properties.toastAnimTimeSize);
        EditorGUI.indentLevel -= 1;

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("�� ��ġ ��ȭ ");
        EditorGUI.indentLevel += 1;
        properties.toastStartPos = EditorGUILayout.Vector3Field("���� ��ġ", properties.toastStartPos);
        properties.toastEndPos = EditorGUILayout.Vector3Field("�Ϸ� ��ġ", properties.toastEndPos);
        properties.toastAnimStartCurvePos = EditorGUILayout.CurveField("���� �ִϸ��̼� �׷���", properties.toastAnimStartCurvePos);
        properties.toastAnimEndCurvePos = EditorGUILayout.CurveField("���� �ִϸ��̼� �׷���", properties.toastAnimEndCurvePos);
        properties.toastAnimTimePos = EditorGUILayout.FloatField("�ִϸ��̼� �ӵ�", properties.toastAnimTimePos);
        EditorGUI.indentLevel -= 1;

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("�� ���� �ð� ");
        EditorGUI.indentLevel += 1;
        properties.toastStayTime = EditorGUILayout.FloatField("�佺Ʈ ���� �ð�", properties.toastStayTime);
        EditorGUI.indentLevel -= 1;

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("�� �佺Ʈ ������Ʈ Ǯ �ʱ�ȭ ");
        EditorGUI.indentLevel += 1;
        Rect lastRect = GUILayoutUtility.GetLastRect();
        if (GUI.Button(new Rect(lastRect.x + 30, lastRect.y + EditorGUIUtility.singleLineHeight, 200, 20), "�佺Ʈ ������Ʈ Ǯ ����"))
        {
            EmptyObjectPool();
        }
        EditorGUILayout.Space(18);
        EditorGUILayout.HelpBox("��Ÿ�� �߿� �佺Ʈ �������� Individual Property ������ �ٲٷ��� ������Ʈ Ǯ�� ������ �մϴ�. ", MessageType.Info);
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
