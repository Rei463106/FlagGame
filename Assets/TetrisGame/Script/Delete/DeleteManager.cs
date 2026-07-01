using Cysharp.Threading.Tasks;
using System;

public class DeleteManager : OperateBlock, IStateEvent
{
    public StateEnum State => StateEnum.Delete;

    public event Action<StateEnum> StateChanged;

    private void Awake() => StateMachine.Entry<DeleteManager>(this);

    public void Starter() => DeleteDirection().Forget();

    private async UniTask DeleteDirection()
    {
        DeleteArray();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        FallDown();
        EventBus.Publish<UpdatePositionEvent>(new UpdatePositionEvent(SendPosition()));
        StateChanged?.Invoke(StateEnum.Spawn);
    }
}
