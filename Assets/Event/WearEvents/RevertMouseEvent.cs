using UnityEngine;

internal readonly struct RevertMouseEvent : IGameEvent
{
    public readonly Vector2 _vector;

    public RevertMouseEvent(Vector2 vector)
    {
        _vector = vector;
    }
}
