using UnityEngine;
using UnityEngine.Pool;

internal abstract class PrefabPool : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject _prefab;
    protected ObjectPool<GameObject> _pool;
    private GameObject _currentObject;

    private void Start()
    {
        _pool = new ObjectPool<GameObject>(OnCreate,
        OnGetFromPool,
        OnReleaseToPool,
        OnDestroyPooledObject,
        true,
        1,
        5);
    }

    //作成
    private GameObject OnCreate()
    {
        return Instantiate(_prefab);
    }

    //プールから取り出す
    private void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
        obj.GetComponent<SpriteRenderer>().enabled = false;//最初見た目を透明にする
    }

    //プールに戻す
    private void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    //上限を超えたら消す
    private void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    private void OnGet()
    {
        _currentObject = _pool.Get();//生成したものの情報が入る
    }

    private void OnRelease()
    {
        _pool.Release(_currentObject);
    }
}
