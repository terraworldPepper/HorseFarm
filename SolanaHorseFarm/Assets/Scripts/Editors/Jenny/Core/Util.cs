using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Util : MonoBehaviour
{
    #region static variables
    #endregion

    #region private variables
    #endregion

    #region public variables
    #endregion

    #region unity function
    #endregion

    #region static function
    public static void SetActive(GameObject go, bool isActive)
    {
        if (go == null)
            return;

        go.SetActive(isActive);
    }

    public static void SetLabel(Text component, string text)
    {
        if (component == null)
            return;

        component.text = text;
    }
    #endregion

    #region private function
    #endregion

    #region public function
    #endregion
}
