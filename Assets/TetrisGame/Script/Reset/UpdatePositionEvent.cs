
using UnityEngine;

/// <summary>
/// リセット処理につける
/// </summary>
public readonly struct UpdatePositionEvent : IGameEvent
{
    public readonly Vector2[] _positions;

    public UpdatePositionEvent(Vector2[] positions)
    {
        _positions = positions;
    }
}
