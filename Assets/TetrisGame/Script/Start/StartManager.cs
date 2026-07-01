using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// スタートを司る
/// </summary>
public class StartManager : StartDirection, IStateEvent
{
    //実装部分
    public StateEnum State => StateEnum.Start;

    public event Action<StateEnum> StateChanged;

    public void Starter() => WaitDirection().Forget();

    //ゲーム部分
    private void Awake() => StateMachine.Entry<StartManager>(this);

    private async UniTask WaitDirection()
    {
        await UniTask.WaitUntil(() => _isFinish);

        _isFinish = false;
        StateChanged?.Invoke(StateEnum.Spawn);
    }
}
