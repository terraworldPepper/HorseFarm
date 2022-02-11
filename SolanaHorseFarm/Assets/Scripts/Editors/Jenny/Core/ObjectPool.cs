using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    #region static variables
    #endregion

    #region private variables
    [SerializeField] GameObject[] _prefabs;

    Dictionary<ObjectPoolItems, Queue<GameObject>> _dicObjectPool = new Dictionary<ObjectPoolItems, Queue<GameObject>>();
    #endregion

    #region public variables
    #endregion

    #region unity function
    private void Awake()
    {
        _dicObjectPool.Clear();

        InitializeObjectPool();
    }
    #endregion

    #region static function
    #endregion

    #region private function
    #endregion

    #region public function
    public void InitializeObjectPool()
    {
        if (_prefabs == null)
            return;

        if (_prefabs.Length != (int)ObjectPoolItems.End)
        {
            Debug.LogError("Please match with prefabs and enums");
            return;
        }
    }

    public void EmptyObjectPool()
    {
        foreach (KeyValuePair<ObjectPoolItems, Queue<GameObject>> pair in _dicObjectPool)
        {
            if (pair.Value != null && pair.Value.Count > 0)
            {
                while (pair.Value.Count > 0)
                {
                    GameObject obj = pair.Value.Dequeue();
                    Destroy(obj);
                }
            }
        }
    }

    public GameObject GetObject(ObjectPoolItems key)
    {
        GameObject obj = null;
        if (_dicObjectPool.ContainsKey(key))
        {
            Queue<GameObject> childQueue = _dicObjectPool[key];
            if (childQueue != null && childQueue.Count > 0)
                obj = childQueue.Dequeue();
            else
                obj = Instantiate(_prefabs[(int)key], transform);
        }
        else
        {
            obj = Instantiate(_prefabs[(int)key], transform);
        }

        return obj;
    }

    public void ReturnObject(ObjectPoolItems key, GameObject obj)
    {
        obj.transform.SetParent(transform);

        if (_dicObjectPool.ContainsKey(key))
        {
            Queue<GameObject> childQueue = _dicObjectPool[key];
            childQueue.Enqueue(obj);
        }
        else
        {
            Queue<GameObject> childQueue = new Queue<GameObject>();
            childQueue.Clear();
            childQueue.Enqueue(obj);
            _dicObjectPool.Add(key, childQueue);
        }
    }
    #endregion
}
