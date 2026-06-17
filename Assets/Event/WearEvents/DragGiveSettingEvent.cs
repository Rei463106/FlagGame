
/// <summary>
/// ドラッグ時に設定を渡す
/// </summary>
internal readonly struct DragGiveSettingEvent : IGameEvent
{
    public ClothItem Setting { get; }

    public DragGiveSettingEvent(ClothItem setting)
    {
        Setting = setting;
    }
}