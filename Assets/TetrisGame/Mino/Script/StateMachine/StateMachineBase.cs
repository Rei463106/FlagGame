using System.Collections.Generic;
using UnityEngine;

public class StateMachineBase : MonoBehaviour
{
    private static Dictionary<StateEnum, IStateEvent> _stateDic = new();
    private StateEnum _currentState;

    /// <summary>
    ///登録用
    ///あくまでそのステートを発火させるだけ用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public static void Entry<T>(T obj) where T : class, IStateEvent
    {
        _stateDic?.TryAdd(obj._state, obj);
    }

    /// <summary>
    /// 一番最初に発火させる
    /// </summary>
    private void Awake()
    {
        _currentState = StateEnum.Start;
        _stateDic[_currentState].StateChanged += Progress;//向こうのイベントを購読
        _stateDic[_currentState].Starter();//ステートの処理開始
    }

    /// <summary>
    /// この中のnextは次のステートが入る
    /// </summary>
    /// <param name="next"></param>
    private void Progress(StateEnum next)
    {
        if (next != StateEnum.None)
        {
            _stateDic[next].StateChanged += Progress;
            _stateDic[next].Starter();
            _stateDic[_currentState].StateChanged -= Progress;
            _currentState = next;
        }
        else
        {
            _stateDic[_currentState].StateChanged -= Progress;
            _currentState = next;
        }
    }
}
