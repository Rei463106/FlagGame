
/// <summary>
/// スコア情報を送信するイベント
/// </summary>
public readonly struct SendScoreEvent : IGameEvent
{
    public readonly int _line;
    public readonly int _combo;

    public SendScoreEvent(int line, int combo)
    {
        _line = line;
        _combo = combo;
    }
}
