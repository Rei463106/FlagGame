using UnityEngine;
using System;

/// <summary>
/// Minoをスポーンさせる処理
/// </summary>
public class SpawnManager : MinoPool, IStateEvent
{
    public event Action<StateEnum> StateChanged;

    public StateEnum State => StateEnum.Spawn;

    private void Awake()
    {
        PleaseAwake();
        StateMachine.Entry<SpawnManager>(this);
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
