using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPlayerPrefs : PlayerPrefs
{
    #region Setter
    public static void SetBool(EditorUIProperties key, bool value)
    {
        if (value)
            SetInt(key.ToString(), 1);
        else
            SetInt(key.ToString(), 0);
    }

    public static void SetInt(EditorUIProperties key, int value)
    {
        SetInt(key.ToString(), value);
    }

    public static void SetFloat(EditorUIProperties key, float value)
    {
        SetFloat(key.ToString(), value);
    }

    public static void SetString(EditorUIProperties key, string value)
    {
        SetString(key.ToString(), value);
    }

    public static void SetVector2(EditorUIProperties key, Vector2 value)
    {
        SetFloat(string.Format("{0}X", key.ToString()), value.x);
        SetFloat(string.Format("{0}Y", key.ToString()), value.y);
    }

    public static void SetVector3(EditorUIProperties key, Vector3 value)
    {
        SetFloat(string.Format("{0}X", key.ToString()), value.x);
        SetFloat(string.Format("{0}Y", key.ToString()), value.y);
        SetFloat(string.Format("{0}Z", key.ToString()), value.z);
    }

    public static void SetVector4(EditorUIProperties key, Vector4 value)
    {
        SetFloat(string.Format("{0}X", key.ToString()), value.x);
        SetFloat(string.Format("{0}Y", key.ToString()), value.y);
        SetFloat(string.Format("{0}Z", key.ToString()), value.z);
        SetFloat(string.Format("{0}W", key.ToString()), value.w);
    }
    #endregion

    #region Getter
    public static bool GetBool(EditorUIProperties key)
    {
        int value = GetInt(key.ToString());
        if (value == 0)
            return false;
        else
            return true;
    }

    public static int GetInt(EditorUIProperties key)
    {
        return GetInt(key.ToString());
    }

    public static float GetFloat(EditorUIProperties key)
    {
        return GetFloat(key.ToString());
    }

    public static string GetString(EditorUIProperties key)
    {
        return GetString(key.ToString());
    }

    public static Vector2 GetVector2(EditorUIProperties key)
    {
        Vector2 value = Vector2.zero;
        value.x = GetFloat(string.Format("{0}X", key.ToString()));
        value.y = GetFloat(string.Format("{0}Y", key.ToString()));
        return value;
    }

    public static Vector3 GetVector3(EditorUIProperties key)
    {
        Vector3 value = Vector3.zero;
        value.x = GetFloat(string.Format("{0}X", key.ToString()));
        value.y = GetFloat(string.Format("{0}Y", key.ToString()));
        value.z = GetFloat(string.Format("{0}Z", key.ToString()));
        return value;
    }

    public static Vector4 GetVector4(EditorUIProperties key)
    {
        Vector4 value = Vector4.zero;
        value.x = GetFloat(string.Format("{0}X", key.ToString()));
        value.y = GetFloat(string.Format("{0}Y", key.ToString()));
        value.z = GetFloat(string.Format("{0}Z", key.ToString()));
        value.w = GetFloat(string.Format("{0}W", key.ToString()));
        return value;
    }
    #endregion

    #region Else
    public static void DeleteKey(EditorUIProperties key)
    {
        DeleteKey(key.ToString());
    }

    public static bool HasKey(EditorUIProperties key)
    {
        return HasKey(key.ToString());
    }
    #endregion
}
