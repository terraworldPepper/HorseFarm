using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamplePage : UIBase
{
    #region static variables
    #endregion

    #region private variables
    [SerializeField] Text _text = null;
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
    public void SetInfo()
    {
        Util.SetLabel(_text, "This is a sample Popup!");
    }
    #endregion
}
