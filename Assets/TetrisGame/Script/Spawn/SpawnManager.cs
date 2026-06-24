using System;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Minoをスポーンさせる処理
/// </summary>
public class SpawnManager : MinoPool, IStateEvent
{
    private IObjectPool<GameObject> _iPool;

    public event Action<StateEnum> StateChanged;

    public StateEnum State => StateEnum.Spawn;

    private void Awake()
    {
        StateMachine.Entry<SpawnManager>(this);
        _iPool = _pool;
    }

    public void SpawnTurn()
    {
        var o = _iPool.Get();

        if (o.TryGetComponent<DoorMino>(out var d))
        {
            if (d == null) return;
            var enterMino = d;
            enterMino.Enter(() => _iPool.Release(o));
        }

        StateChanged?.Invoke(StateEnum.Confirm);
    }

    public void Starter()
    {
        SpawnTurn();
    }
}
