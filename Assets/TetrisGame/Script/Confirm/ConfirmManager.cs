using System;

/// <summary>
/// 確認マネージャー
/// </summary>
public class ConfirmManager : MinoConfirm, IStateEvent
{
    public StateEnum State => StateEnum.Confirm;

    public event Action<StateEnum> StateChanged;

    public void Starter()
    {
        StateChanged?.Invoke(StateEnum.Delete);//次は消す処理へ
    }

    private void Awake()
    {
        StateMachine.Entry<ConfirmManager>(this);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<UpdatePositionEvent>(this, ReceivePosition);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    /// <summary>
    /// もらったVector2の配列を代入し送信
    /// </summary>
    /// <param name="c"></param>
    private void ReceivePosition(UpdatePositionEvent c)
    {
        foreach (var e in c._positions)
        {
            UpdateArray((int)e.x, (int)e.y);
        }
        EventBus.Publish<SendArrayEvent>(new SendArrayEvent(MinoArray));
        Starter();
    }
}
