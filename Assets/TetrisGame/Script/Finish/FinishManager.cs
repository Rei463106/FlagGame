using Cysharp.Threading.Tasks;
using System;

public class FinishManager : FinishDirection, IStateEvent
{
    public StateEnum State => StateEnum.Finish;

    public event Action<StateEnum> StateChanged;

    public void Starter() => WaitDirection().Forget();

    private void Awake() => StateMachine.Entry<FinishManager>(this);

    private async UniTask WaitDirection()
    {
        Direction();
        await UniTask.WaitUntil(() => _isFinish);
        //画面暗転するなりなんなり
    }
}
