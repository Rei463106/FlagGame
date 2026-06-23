using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Minoをスポーンさせる処理
/// </summary>
public class MinoSpawn : MinoPool
{
    private IObjectPool<GameObject> _iPool;

    private void Awake()
    {
        _iPool = _pool;
    }

    public void SpawnTurn()
    {
        var o = _iPool.Get();

        if (o.TryGetComponent<EnterMino>(out var d))
        {
            if (d == null) return;
            var enterMino = d;
            enterMino.Enter(() => _iPool.Release(o));
        }
    }
}
