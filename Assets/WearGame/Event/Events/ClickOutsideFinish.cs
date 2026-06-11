using UnityEngine;

/// <summary>
/// 外側でマウスを離した時のイベント
/// </summary>
public readonly struct ClickOutsideFinish : IGameEvent
{
    public GameObject PrefabObject { get; }

    public ClickOutsideFinish(GameObject prefabObject)
    {
        PrefabObject = prefabObject;
    }
}
