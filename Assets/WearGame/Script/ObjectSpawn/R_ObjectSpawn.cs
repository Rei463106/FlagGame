using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// ObjectSpawnのランタイム処理
/// </summary>
public class R_ObjectSpawn : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] private ClothItem _setting;
    [Header("Prefab")]
    [SerializeField] private GameObject _prefab;
    [Header("生成場所")]
    [SerializeField] private Transform _spawnTransform;

    private ObjectPool<GameObject> _pool;
    private HashSet<string> _prefabIDList = new HashSet<string>();
    private GameObject _oldPrefab;
    private GameObject _currentPrefab;

    private void OnEnable()
    {
        EventBus.Subscribe<DragGiveObject>(this, ObjectDrag);
        EventBus.Subscribe<ClickInsideFinish>(this, ObjectMouseUpInside);
        EventBus.Subscribe<ClickOutsideFinish>(this, ObjectMouseUpOutSide);

    }
    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void Start()
    {
        _pool = new ObjectPool<GameObject>(
        OnCreate,
        OnGetFromPool,
        OnReleaseToPool,
        OnDestroyPooledObject,
        true,
        10,
        50);

        InitializeObject();
    }

    //初期化
    private void InitializeObject()
    {
        _oldPrefab = _currentPrefab;
        _currentPrefab = _pool.Get();//生成したものの情報が入る
        _currentPrefab.GetComponent<SpriteRenderer>().sprite = _setting.Sprite;
        _prefabIDList.Add(_currentPrefab.GetInstanceID().ToString());
        _currentPrefab.transform.position = _spawnTransform.position;
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

    /// <summary>今の物体がドラッグされたかを購読</summary>
    private void ObjectDrag(DragGiveObject d)
    {
        if (_prefabIDList.Contains(d.PrefabObject.GetInstanceID().ToString()))
        {
            EventBus.Publish<DragGiveSetting>(new DragGiveSetting(_setting));//動きが確認出来たら設定も渡す
            _currentPrefab.GetComponent<SpriteRenderer>().enabled = true;
            InitializeObject();
        }
    }

    /// <summary>今の物体に対しマウスが離されたかを購読</summary>
    private void ObjectMouseUpInside(ClickInsideFinish c)
    {
        if (_oldPrefab != null)
        {
            _pool.Release(_oldPrefab);
            _oldPrefab = null;
        }
    }

    /// <summary>今の物体に対しマウスが離されたかを購読</summary>
    private void ObjectMouseUpOutSide(ClickOutsideFinish c)
    {
        if (_oldPrefab != null)
        {
            _pool.Release(_oldPrefab);
            _oldPrefab = null;
        }
    }
}
