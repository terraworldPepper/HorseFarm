using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalEnableAttribute : PropertyAttribute
{
    public string ConditionalField = string.Empty;
    public ConditionalEnableAttribute(string condition)
    {
        this.ConditionalField = condition;
    }
}
