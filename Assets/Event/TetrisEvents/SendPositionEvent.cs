using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リセット処理につける
/// </summary>
public readonly struct SendPositionEvent : IGameEvent
{
    public readonly List<Vector2> _positions;

    public SendPositionEvent(List<Vector2> positions)
    {
        _positions = positions;
    }
}
