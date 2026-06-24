using System;
using UnityEngine;

/// <summary>
/// 確認マネージャー
/// </summary>
public class ConfirmManager : MonoBehaviour, IStateEvent
{
    public StateEnum State => throw new NotImplementedException();

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
        EventBus.Subscribe<CallPositionEvent>(this, ReceivePosition);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void ReceivePosition(CallPositionEvent c)
    {
        //位置情報を受け取って、配列に代入。位置を送信する
        Starter();
    }
}
