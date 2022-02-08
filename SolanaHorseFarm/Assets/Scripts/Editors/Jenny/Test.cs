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
        SamplePage cs = UIManager.Instance.OpenUI<SamplePage>("Prefabs/SamplePage");
        if (cs)
            cs.SetInfo();
    }

    public void OpenSamplePopup()
    {
        SamplePopup cs = UIManager.Instance.OpenUI<SamplePopup>("Prefabs/SamplePopup", true);
        if (cs)
            cs.SetInfo();
    }

    public void OpenToast()
    {
        UIManager.Instance.OpenToast(ToastType.Toast, "This is a first toast Test!");
    }

    public void OpenToast2()
    {
        UIManager.Instance.OpenToast(ToastType.Toast2, "This is a second toast Test!");
    }

    public void OpenToast3()
    {
        UIManager.Instance.OpenToast(ToastType.Toast3, "This is a third toast Test!");
    }

    public void OpenToast4()
    {
        UIManager.Instance.OpenToast(ToastType.Toast4, "This is a forth toast Test!");
    }
}
