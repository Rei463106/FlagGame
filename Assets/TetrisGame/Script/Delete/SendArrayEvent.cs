
/// <summary>
/// 消す時の処理があるものにつける
/// </summary>
public readonly struct SendArrayEvent : IGameEvent
{
    public readonly MinoArraySetting[,] _existPosition;

    public SendArrayEvent(MinoArraySetting[,] existPosition)
    {
        _existPosition = existPosition;
    }
}
