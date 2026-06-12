using UnityEngine;

/// <summary>
/// マウスドラッグ時のイベント
/// </summary>
internal readonly struct DragGiveObject : IGameEvent
{ 
    public GameObject PrefabObject { get; }

    public DragGiveObject(GameObject @object)
    {
        PrefabObject = @object;
    }
}
