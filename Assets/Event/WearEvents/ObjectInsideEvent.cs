using UnityEngine;

/// <summary>
/// コライダーの中でマウスが離された時のイベント
/// </summary>
public readonly struct ObjectInsideEvent : IGameEvent
{
    public GameObject PrefabObject { get; }

    public ObjectInsideEvent(GameObject prefabObject)
    {
        PrefabObject = prefabObject;
    }
}
