
/// <summary>
/// 消す時の処理があるものにつける
/// </summary>
public readonly struct SendArrayEvent : IGameEvent
{
    public readonly bool[,] _existPosition;

    public SendArrayEvent(bool[,] existPosition)
    {
        _existPosition = existPosition;
    }
}
