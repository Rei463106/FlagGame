
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

/// <summary>
/// スタートを司る
/// </summary>
public class StartManager : StartDirection, IStateEvent
{
    //実装部分
    public StateEnum State => StateEnum.Start;

    public event Action<StateEnum> StateChanged;

    public void Starter()
    {
        CancellationTokenSource source = new();
        CancellationToken token = source.Token;
        WaitDirection(token).Forget();
    }

    //ゲーム部分
    private void Awake()
    {
        StateMachine.Entry<StartManager>(this);
    }

    private async UniTask WaitDirection(CancellationToken token)
    {
        await UniTask.WaitUntil(() => _isFinish);
        StateChanged?.Invoke(StateEnum.Spawn);//次のステートへ…
    }
}
