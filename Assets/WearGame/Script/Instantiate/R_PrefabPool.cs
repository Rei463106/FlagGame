using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

internal class R_PrefabPool : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject _prefab;

    private ObjectPool<GameObject> _pool;
    private GameObject _currentObject;

    private void OnEnable()
    {
        EventBus.Subscribe<ClickStart>(this, OnGet);
        EventBus.Subscribe<ClickInsideFinish>(this, OnRelease);
        EventBus.Subscribe<ClickOutsideFinish>(this, OnRelease);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

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
        
        //obj.GetComponent<SpriteRenderer>().enabled = false;//最初見た目を透明にする
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

    private void OnGet(ClickStart c)
    {
        _currentObject = _pool.Get();//生成したものの情報が入る
        var mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 mousePos = new Vector2(mouse.x, mouse.y);
        _currentObject.transform.position = mousePos;
        Debug.Log(mousePos);
    }

    private void OnRelease(ClickInsideFinish c)
    {
        _pool.Release(_currentObject);
    }

    private void OnRelease(ClickOutsideFinish c)
    {
        _pool.Release(_currentObject);
    }
}
