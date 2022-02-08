using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionalEnableAttribute), true)]
public class ConditionalEnableAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalEnableAttribute condition = (ConditionalEnableAttribute)attribute;
        GUI.enabled = GetConditionAttributeResult(condition, property);
        EditorGUI.PropertyField(position, property, label, true);
    }

    private bool GetConditionAttributeResult(ConditionalEnableAttribute condition, SerializedProperty property)
    {
        bool enabled = true;
        string propertyPath = property.propertyPath;
        string conditionPath = propertyPath.Replace(property.name, condition.ConditionalField);
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

        if (sourcePropertyValue != null)
            enabled = sourcePropertyValue.boolValue;
        else
            Debug.LogWarning("Not matched with boolean condition: " + condition.ConditionalField);

        return enabled;
    }
}
