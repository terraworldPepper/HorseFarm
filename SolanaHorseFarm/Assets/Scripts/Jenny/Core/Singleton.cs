using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// protected <ClassName>() {} 을 선언하여 비 싱글톤 생성자 사용을 방지할 것.
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region static variables
    protected static T _instance;

    public static T Instance
    {
        get
        {
            // 프로그램 종료 시 싱글톤 객체가 먼저 삭제될 수 있다. 이 때 삭제된 싱글톤 객체를 참조하는 것을 방지.
            if (_shutdown)
                return null;

            lock (_lockObj)
            {
                if (_instance == null)
                    _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<T>();
                    go.name = typeof(T).ToString();

                    DontDestroyOnLoad(go);
                }
            }

            return _instance;
        }
    }
    #endregion

    #region private variables
    private static bool _shutdown = false;
    private static object _lockObj = new object();
    #endregion

    #region public variables
    #endregion

    #region unity function
    private void OnApplicationQuit()
    {
        _shutdown = true;
    }

    private void OnDestroy()
    {
        _shutdown = true;
    }
    #endregion

    #region static function
    #endregion

    #region private function
    #endregion

    #region public function
    #endregion
}
