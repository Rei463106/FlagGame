using System;
using UnityEngine;

/// <summary>
/// Minoの入退室管理
/// </summary>
public class DoorMino : MonoBehaviour
{
    private event Action EnterAction;
    protected event Action InsideEnterAction;
    protected bool _isHold;

    /// <summary>
    /// 入室
    /// </summary>
    /// <param name="action"></param>
    public void Enter(bool hold, Action action)
    {
        _isHold = hold;
        InsideEnterAction?.Invoke();
        EnterAction += action;
    }

    /// <summary>
    /// 退出
    /// </summary>
    protected void Delete()
    {
        EnterAction?.Invoke();
        EnterAction = null;
    }
}
