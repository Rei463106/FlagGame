using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 新生ObjectPool
/// </summary>
public class R_ObjectPool : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject _prefab;
    [Header("生成場所")]
    [SerializeField] private Transform _makePlace;
    [Header("SO")]
    [SerializeField] private ClothItem _itemSO;

    private ObjectPool<GameObject> _pool;
    private GameObject _currentObject;
    private CancellationTokenSource _source = new CancellationTokenSource();
    private CancellationToken _token;

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

        _token = _source.Token;
        WaitRevert(_token).Forget();
    }

    /// <summary>
    /// 作成
    /// </summary>
    /// <returns></returns>
    private GameObject OnCreate()
    {
        return Instantiate(_prefab);
    }

    /// <summary>
    /// プールから取り出す
    /// </summary>
    /// <param name="obj"></param>
    private void OnGetFromPool(GameObject obj)
    {
        Debug.Log(obj.GetInstanceID());
        obj.SetActive(true);
        if (obj.TryGetComponent<SpriteRenderer>(out var c))
        {
            c.enabled = false;
            obj.transform.position = _makePlace.position;
        }
        else
            Debug.Log($"{c}はありません");
    }

    /// <summary>
    /// プールに戻す
    /// </summary>
    /// <param name="obj"></param>
    private void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    /// <summary>
    /// 上限を超えたら消す
    /// </summary>
    /// <param name="obj"></param>
    private void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    /// <summary>
    /// オブジェクトのループ処理
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask WaitRevert(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            _currentObject = _pool.Get();

            if (_currentObject.TryGetComponent<R_ObjectSetting>(out var c))
            {
                c.ReceiveSetting(_itemSO);
            }
            else
                Debug.Log($"{c}はありません");

            await UniTask.WaitUntil(() => c.WaitUntilRevertMouse());   //awaitで、完了通知が来るまで待つ

            c.ReturnValue();
            var oldObject = _currentObject;
            _pool.Release(oldObject);

            await UniTask.Yield();
        }
    }
}
