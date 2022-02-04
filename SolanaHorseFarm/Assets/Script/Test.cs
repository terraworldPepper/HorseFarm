using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
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
    #endregion

    #region private function
    #endregion

    #region public function
    #endregion

    public void OpenSamplePage()
    {
        UIManager.Instance.OpenUI("Prefab/SamplePage");
    }

    public void OpenSamplePopup()
    {
        UIManager.Instance.OpenUI("Prefab/SamplePopup");
    }
}
