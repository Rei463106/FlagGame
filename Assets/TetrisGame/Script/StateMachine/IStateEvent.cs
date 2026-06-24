using System;

/// <summary>
/// 順番に関わる処理は必ず実装する
/// </summary>
public interface IStateEvent
{
    /// <summary>自分のステート</summary>
    public StateEnum State { get; }

    /// <summary>登録してもらう用・終了したかどうかをもらう</summary>
    public event Action<StateEnum> StateChanged;

    /// <summary>ステート側から発火する</summary>
    public void Starter();
}
