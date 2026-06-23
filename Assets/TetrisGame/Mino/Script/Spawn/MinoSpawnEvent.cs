
/// <summary>
/// スポーン時に設定を渡すイベント
/// </summary>
public readonly struct MinoSpawnEvent : IGameEvent
{
    public readonly MinoSetting _mSetting;

    public MinoSpawnEvent(MinoSetting m)
    {
        _mSetting = m;
    }
}
