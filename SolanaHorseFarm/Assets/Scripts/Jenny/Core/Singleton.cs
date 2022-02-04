using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// protected <ClassName>() {} �� �����Ͽ� �� �̱��� ������ ����� ������ ��.
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region static variables
    protected static T _instance;

    public static T Instance
    {
        get
        {
            // ���α׷� ���� �� �̱��� ��ü�� ���� ������ �� �ִ�. �� �� ������ �̱��� ��ü�� �����ϴ� ���� ����.
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
