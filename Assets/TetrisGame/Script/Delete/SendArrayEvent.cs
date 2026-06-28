
/// <summary>
/// 配列を渡すイベント
/// </summary>
public readonly struct SendArrayEvent : IGameEvent
{
    public readonly MinoArraySetting[,] _existPosition;

    public SendArrayEvent(MinoArraySetting[,] existPosition)
    {
        _existPosition = existPosition;
    }
}
