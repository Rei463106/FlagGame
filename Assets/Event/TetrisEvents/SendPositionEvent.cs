using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リセット処理につける
/// </summary>
public readonly struct SendPositionEvent : IGameEvent
{
    public readonly List<Vector2> _positions;
    public readonly Sprite _sprite;

    public SendPositionEvent(List<Vector2> positions, Sprite sprite)
    {
        _positions = positions;
        _sprite = sprite;
    }
}
