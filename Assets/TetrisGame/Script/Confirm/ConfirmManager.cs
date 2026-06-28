using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

/// <summary>
/// 確認マネージャー
/// </summary>
public class ConfirmManager : MinoConfirm, IStateEvent
{
    private bool _isStart;
    public StateEnum State => StateEnum.Confirm;

    public event Action<StateEnum> StateChanged;

    public void Starter()
    {
        WaitFinish().Forget();
    }

    private void Awake()
    {
        PleaseAwake();
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
            UpdateArray(e, true);
        }
        EventBus.Publish<SendArrayEvent>(new SendArrayEvent(MArraySetting));
        _isStart = true;
    }

    private async UniTask WaitFinish()
    {
        await UniTask.WaitUntil(() => _isStart);
        _isStart = false;
        StateChanged?.Invoke(StateEnum.Delete);//次は消す処理へ
    }
}
