using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全てのイベントの購読・解除・発火を司るクラス
/// </summary>
public static class EventBus
{
    private static readonly Dictionary<Type, List<Delegate>> _eventDic = new Dictionary<Type, List<Delegate>>();
    private static readonly Dictionary<object, List<MemorizeEvent>> _ownerDic = new Dictionary<object, List<MemorizeEvent>>();

    /// <summary>
    /// イベントを購読する
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="owner"></param>
    /// <param name="action"></param>
    public static void Subscribe<T>(object owner, Action<T> action) where T :  IGameEvent
    {
        Type type = typeof(T);
        if (!_eventDic.TryGetValue(type, out List<Delegate> list))
        {
            list = new List<Delegate>();
            _eventDic[type] = list;
        }
        if (!_ownerDic.TryGetValue(owner, out List<MemorizeEvent> memo))
        {
            memo = new List<MemorizeEvent>();
            _ownerDic[owner] = memo;
        }
        list.Add(action);
        memo.Add(new MemorizeEvent(type, action));
    }

    /// <summary>
    /// イベントの購読を解除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="owner"></param>
    public static void UnSubscribe<T>(Action<T> action) where T :  IGameEvent
    {
        Type type = typeof(T);
        if (_eventDic.TryGetValue(type, out List<Delegate> list))
        {
            list.Remove(action);

            if (list.Count == 0)
            {
                _eventDic.Remove(type);
            }
        }      
    }

    /// <summary>
    /// そのオブジェクト内の購読を一斉解除
    /// </summary>
    /// <param name="owner"></param>
    public static void AllUnSubscribe(object owner)
    {
        if (!_ownerDic.TryGetValue(owner, out var memo))
            return;

        foreach (var v in memo)
        {
            if (_eventDic.TryGetValue(v.MType, out var list))
            {
                list.Remove(v.MDelegate);

                if (list.Count == 0)
                {
                    _eventDic.Remove(v.MType);
                }
            }
        }
        _ownerDic.Remove(owner);
    }

    /// <summary>
    /// 発火処理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ev"></param>
    public static void Publish<T>(T ev) where T : ScriptableObject, IGameEvent
    {
        Type t = typeof(T);
        if (_eventDic.TryGetValue(t, out List<Delegate> list))
        {
            var copy = list.ToArray();
            foreach (var v in copy)
            {
                ((Action<T>)v)(ev);
            }
        }
    }
}

/// <summary>
/// 一斉解除を作るための構造体
/// </summary>
public readonly struct MemorizeEvent
{
    public Type MType { get; }
    public Delegate MDelegate { get; }

    public MemorizeEvent(Type type, Delegate @delegate)
    {
        MType = type;
        MDelegate = @delegate;
    }
}


