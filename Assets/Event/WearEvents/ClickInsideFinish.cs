using UnityEngine;

/// <summary>
/// コライダーの中でマウスが離された時のイベント
/// </summary>
public readonly struct ClickInsideFinish : IGameEvent
{
    public GameObject PrefabObject { get; }

    public ClickInsideFinish(GameObject prefabObject)
    {
        PrefabObject = prefabObject;
    }
}
