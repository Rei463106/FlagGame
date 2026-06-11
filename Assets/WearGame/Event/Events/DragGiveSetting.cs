#if UNITY_EDITOR
using UnityEditor;
#endif 
using UnityEngine;

internal readonly struct DragGiveSetting : IGameEvent
{
    public ClothItem Setting { get; }

    public DragGiveSetting(ClothItem setting)
    {
        Setting = setting;
    }
}