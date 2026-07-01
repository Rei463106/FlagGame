using System.Collections.Generic;
using UnityEngine;

public readonly struct UpdatePositionEvent : IGameEvent
{
    public readonly List<Vector2> _update;

    public UpdatePositionEvent(List<Vector2> update) => _update = update;
}
