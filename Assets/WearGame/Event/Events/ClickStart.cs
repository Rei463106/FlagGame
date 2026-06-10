#if UNITY_EDITOR
using UnityEditor;
#endif 
using UnityEngine;

internal readonly struct ClickStart : IGameEvent
{
    public ClothItem Setting { get; }

    public ClickStart(ClothItem setting)
    {
        Setting = setting;
    }
}