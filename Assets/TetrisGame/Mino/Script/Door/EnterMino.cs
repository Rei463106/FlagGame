using System;
using UnityEngine;

/// <summary>
/// Minoの入退室管理
/// </summary>
public class EnterMino : MonoBehaviour, IDelete
{
    private event Action EnterAction;

    /// <summary>
    /// 入室
    /// </summary>
    /// <param name="action"></param>
    public void Enter(Action action)
    {
        EnterAction += action;
    }

    /// <summary>
    /// 退出
    /// </summary>
    void IDelete.Delete()
    {
        EnterAction?.Invoke();
        EnterAction = null;
    }
}
