
using UnityEngine;

/// <summary>
/// リセット処理につける
/// </summary>
public readonly struct CallPositionEvent : IGameEvent
{
    public readonly Vector2[] _positions;

    public CallPositionEvent(Vector2[] positions)
    {
        _positions = positions;
    }
}
