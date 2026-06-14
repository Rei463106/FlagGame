using UnityEngine;

/// <summary>
/// 外側でマウスを離した時のイベント
/// </summary>
public readonly struct ObjectOutsideEvent : IGameEvent
{
    public GameObject PrefabObject { get; }

    public ObjectOutsideEvent(GameObject prefabObject)
    {
        PrefabObject = prefabObject;
    }
}
