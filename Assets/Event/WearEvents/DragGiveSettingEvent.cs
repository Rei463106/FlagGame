#if UNITY_EDITOR
using UnityEditor;
#endif 
using UnityEngine;

internal readonly struct DragGiveSettingEvent : IGameEvent
{
    public ClothItem Setting { get; }

    public DragGiveSettingEvent(ClothItem setting)
    {
        Setting = setting;
    }
}