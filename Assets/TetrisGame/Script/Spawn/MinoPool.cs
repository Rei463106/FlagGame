using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Minoのオブジェクトプール
/// </summary>
public class MinoPool : MinoSelect
{
    protected ObjectPool<GameObject> _pool;

    void Awake()
    {
        _pool = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,true,100,200);
    }

    GameObject OnCreatePooledObject()
    {
        return Instantiate(MakeMino());
    }

    void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }
}
