using System;
using UnityEngine;

/// <summary>
/// Minoをスポーンさせる処理
/// </summary>
public class SpawnManager : MinoSelect, IStateEvent
{
    public event Action<StateEnum> StateChanged;

    public StateEnum State => StateEnum.Spawn;

    private void Awake() => StateMachine.Entry<SpawnManager>(this);


    public void DebugSubscribers()
    {
        Debug.Log(StateChanged?.GetInvocationList().Length ?? 0);
    }

    public void Starter()
    {
        var o = MakeMino();

        if (o.TryGetComponent<DoorMino>(out var d))
        {
            if (d == null) return;
            var enterMino = d;
            enterMino.Enter(() => Destroy(o));
        }
        StateChanged?.Invoke(StateEnum.Confirm);
    }
}
