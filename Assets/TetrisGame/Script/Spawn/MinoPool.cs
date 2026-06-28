using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Minoのオブジェクトプール
/// </summary>
public class MinoPool : MinoSelect
{
    [SerializeField] private GameObject _obj;
    protected IObjectPool<GameObject> _iPool;

    protected void PleaseAwake()
    {
        _iPool = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, 100, 200);
    }

    private GameObject OnCreatePooledObject()
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
