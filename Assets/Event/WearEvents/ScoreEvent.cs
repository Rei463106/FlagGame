
/// <summary>
/// 現在のスコアを受け取れるイベント
/// </summary>
public readonly struct ScoreEvent : IGameEvent
{
    public readonly int _score;

    public ScoreEvent(int score)
    {
        _score = score;
    }
}
