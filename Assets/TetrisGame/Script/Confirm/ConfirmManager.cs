using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// 確認マネージャー
/// </summary>
public class ConfirmManager : MinoConfirm, IStateEvent
{
    private bool _isStart;
    public StateEnum State => StateEnum.Confirm;

    public event Action<StateEnum> StateChanged;

    public void Starter() => WaitFinish().Forget();

    private void Awake()
    {
        PleaseAwake();
        StateMachine.Entry<ConfirmManager>(this);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<SendPositionEvent>(this, ReceivePosition);
        EventBus.Subscribe<UpdatePositionEvent>(this, UpdatePosition);
    }

    private void OnDisable() => EventBus.AllUnSubscribe(this);

    /// <summary>
    /// 置いたのを受け取る
    /// </summary>
    /// <param name="c"></param>
    private void ReceivePosition(SendPositionEvent c)
    {
        foreach (var v in c._positions)
            UpdateArray(v);
        _isStart = true;
    }


    /// <summary>
    /// 全ての位置を受け取り、更新
    /// </summary>
    /// <param name="u"></param>
    private void UpdatePosition(UpdatePositionEvent u)
    {
        ResetArray();
        foreach (var e in u._update)
            UpdateArray(e);
    }

    private async UniTask WaitFinish()
    {
        await UniTask.WaitUntil(() => _isStart);
        _isStart = false;
        StateChanged?.Invoke(StateEnum.Delete);//次は消す処理へ
    }
}
